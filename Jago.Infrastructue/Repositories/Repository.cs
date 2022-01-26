using Jago.domain.Interfaces.Repositories;
using Jago.Infrastructure.DBConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Jago.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> DbSet;
        protected readonly ApplicationContext Db;

        public Repository(ApplicationContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
            
        }
        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
            SaveChanges();
        }

        public bool Any(Func<TEntity, bool> exp)
        {
            return DbSet.Any(exp);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IQueryable<TEntity> GetAllBy(Func<TEntity, bool> exp)
        {
            return DbSet.Where(exp).AsQueryable();
        }

        public TEntity GetBy(Func<TEntity, bool> exp)
        {
            return DbSet.FirstOrDefault(exp);
        }

        public TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
            SaveChanges();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            DbSet.Update(obj);
            SaveChanges();
        }
    }
}
