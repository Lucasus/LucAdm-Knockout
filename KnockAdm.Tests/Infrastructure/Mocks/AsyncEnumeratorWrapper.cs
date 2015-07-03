using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace LucAdm.Tests
{
    /// <summary>
    /// Based on following gist: https://gist.github.com/taschmidt/9663503
    /// </summary>
    public class AsyncEnumeratorWrapper<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public AsyncEnumeratorWrapper(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }
}