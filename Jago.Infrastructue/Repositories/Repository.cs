using Jago.domain.Interfaces.Repositories;
using Jago.Infrastructure.DBConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Jago.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();

        }
        public void Add(TEntity obj)
        {
            _dbSet.Add(obj);
            SaveChanges();
        }

        public IQueryable<TEntity> GetAll() => _dbSet.AsNoTracking();

        public TEntity GetById(Guid id) => _dbSet.Find(id)!;

        public void Remove(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id)!);
            SaveChanges();
        }

        public int SaveChanges() => _context.SaveChanges();

        public void Update(TEntity obj)
        {
            _dbSet.Update(obj);
            SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
        public IQueryable<TEntity> GetAllBy(Func<TEntity, bool> exp)
        {
            return _dbSet.Where(exp).AsQueryable();
        }

        public TEntity GetBy(Func<TEntity, bool> exp)
        {
            throw new NotImplementedException();
        }
        public bool Any(Func<TEntity, bool> exp)
        {
            throw new NotImplementedException();
        }
    }
}
