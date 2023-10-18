using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable // looks for dispose method in unit of work an when we finish ou transaction it disposed our context
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
} 