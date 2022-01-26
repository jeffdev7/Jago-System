using AutoMapper;
using FluentValidation.Results;
using Jago.Application.ViewModel;
using Jago.domain.Core.Entities;
using Jago.domain.Interfaces.Repositories;
using Jago.domain.Validator;

namespace Jago.Application.Services
{
    public class PassengerServices : IPassengerServices
    {
        private readonly IMapper _mapper;
        private readonly IPassengerRepository _paxRepository;

        public PassengerServices(IMapper mapper, IPassengerRepository passengerRepository)
        {
            _mapper = mapper;
            _paxRepository = passengerRepository;
        }
        public IEnumerable<PassengerViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<PassengerViewModel>>(_paxRepository.GetAll());
        }
        public PassengerViewModel GetById(Guid id)
        {
            return _mapper.Map<PassengerViewModel>(_paxRepository.GetById(id));
        }
        public IEnumerable<PassengerViewModel> GetAllBy(Func<Passenger, bool> exp)
        {
            return _mapper.Map<IEnumerable<PassengerViewModel>>(_paxRepository.GetAllBy(exp));
        }
        public ValidationResult Add(PassengerViewModel vm)
        {
            var entity = _mapper.Map<Passenger>(vm);
            var validationResult = new PassengerValidator().Validate(entity);
            if(validationResult.IsValid)
            _paxRepository.Add(entity);

            return validationResult;
           
        }

        public void Dispose()
        {
           GC.SuppressFinalize(this);
        }
        public IEnumerable<PassengerViewModel> GetPax()
        {
            return _mapper.Map<IEnumerable<PassengerViewModel>>(_paxRepository.GetPax());
        }

        public ValidationResult Remove(Guid id)
        {
            var entity = _paxRepository.GetById(id);
            var validationResult = new PassengerValidator().Validate(entity);
            if(validationResult.IsValid)
            _paxRepository.Remove(id);
            //_paxRepository.Dispose();   
            return validationResult;
        }

        public ValidationResult Update(PassengerViewModel vm)
        {
            var entity = _mapper.Map<Passenger>(vm);
            var validationResult = new PassengerValidator().Validate(entity);

            if (validationResult.IsValid)
            {
                _paxRepository.Update(entity);
            }

            return validationResult;
        }
    }
}
