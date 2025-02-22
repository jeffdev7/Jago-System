using FluentValidation.Results;
using Jago.CrossCutting.Dto;
using Jago.domain.Core.Entities;


namespace Jago.Application.Services
{
    public interface IPassengerServices : IDisposable
    {
        IEnumerable<PassengerViewModel> GetAll();
        Passenger GetById(Guid id);
        IEnumerable<PassengerViewModel> GetAllBy(Func<Passenger, bool> exp);
        ValidationResult Add(PassengerViewModel vm);
        ValidationResult Update(PassengerViewModel vm);
        Task<bool> Remove(Guid id);
        IEnumerable<PassengerViewModel> GetPax();

    }
}
