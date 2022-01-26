using FluentValidation.Results;
using Jago.Application.ViewModel;
using Jago.domain.Core.Entities;


namespace Jago.Application.Services
{
    public interface IPassengerServices : IDisposable
    {
        IEnumerable<PassengerViewModel> GetAll();
        PassengerViewModel GetById(Guid id);
        IEnumerable<PassengerViewModel> GetAllBy(Func<Passenger, bool> exp);
        ValidationResult Add(PassengerViewModel vm);
        ValidationResult Update(PassengerViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<PassengerViewModel> GetPax();
       
    }
}
