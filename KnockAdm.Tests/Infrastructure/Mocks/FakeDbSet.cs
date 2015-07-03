using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LucAdm.Tests
{
    /// <summary>
    /// Based on following gist: https://gist.github.com/taschmidt/9663503
    /// It allows me to mock DbSets when async operations are required
    /// </summary>
    public class FakeDbSet<T> : DbSet<T>, IEnumerable<T>, IQueryable, IDbAsyncEnumerable<T> where T : class
    {
        private readonly ObservableCollection<T> _data;
        private readonly IQueryable _queryable;

        public FakeDbSet(IList<T> data)
        {
            _data = new ObservableCollection<T>();
            foreach (var item in data)
            {
                _data.Add(item);
            }
            _queryable = _data.AsQueryable();
        }

        public override T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public override Task<T> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public override T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public override T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public override T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public override T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<T> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _queryable.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _queryable.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new AsyncQueryProviderWrapper<T>(_queryable.Provider); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public int Count
        {
            get { return _data.Count; }
        }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new AsyncEnumeratorWrapper<T>(_data.GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }
    }
}