using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KnockAdm
{
    public class Repository<TEntity> : Repository<TEntity, PersistenceContext>
        where TEntity : Entity, new()
    {
        public Repository(PersistenceContext context) : base(context)
        {

        }
    }


    public class Repository<TEntity, TContext>
        where TEntity : Entity, new()
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            var entity = new TEntity { Id = id };
            _context.Entry(entity).State = EntityState.Deleted;
        }
    }
}