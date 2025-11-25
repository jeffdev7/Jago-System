using System.ComponentModel.DataAnnotations;

namespace Jago.Application.Interfaces.Core
{
    public interface IServices<TEntity, TViewModel> : IDisposable where TEntity :
        class where TViewModel : class, new()
    {
        Task<IEnumerable<TViewModel>> GetAll();
        Task<TViewModel> GetById(Guid id);
        Task<IEnumerable<TViewModel>> GetAllBy(Func<TEntity, bool> exp);
        Task<ValidationResult> Add(TViewModel vm);
        Task<ValidationResult> Update(TViewModel vm);
        Task<ValidationResult> Remove(TViewModel vm);
    }

}
