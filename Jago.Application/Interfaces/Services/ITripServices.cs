using FluentValidation.Results;
using Jago.CrossCutting.Dto;

namespace Jago.Application.Services
{
    public interface ITripServices : IDisposable
    {
        IEnumerable<TripViewModel> GetAll();
        TripViewModel GetById(Guid id);
        ValidationResult Add(TripViewModel vm);
        ValidationResult Update(TripViewModel vm);
        Task<bool> Remove(Guid id);
        IEnumerable<PassengerViewModel> GetPax();
        IEnumerable<PaxListModel> GetPaxList();
        IEnumerable<TripViewModel> GetOrder();
    }
}
