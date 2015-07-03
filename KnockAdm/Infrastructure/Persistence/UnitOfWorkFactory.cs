using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace KnockAdm
{
    public class UnitOfWorkFactory
    {
        private readonly DbContext _context;

        public UnitOfWorkFactory(DbContext context)
        {
            _context = context;
        }

        public UnitOfWork Create()
        {
            return new UnitOfWork(_context);
        }

        /// <summary>
        /// Executes action as a unit of work
        /// </summary>
        public Task DoAsync<T>(Func<T> action)
        {
            return DoAsync(work => action());
        }

        /// <summary>
        /// Executes action as a unit of work
        /// </summary>
        public Task DoAsync(Action action)
        {
            return DoAsync(work => action());
        }

        /// <summary>
        /// Executes action as a unit of work
        /// </summary>
        public Task DoAsync(Action<UnitOfWork> action)
        {
            return Create().DoAsync(action);
        }

        /// <summary>
        /// Executes action as a unit of work
        /// </summary>
        public Task<T> DoAsync<T>(Func<UnitOfWork, T> action)
        {
            return Create().DoAsync(action);
        }
    }
}