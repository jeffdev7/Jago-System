using AutoMapper;
using FluentValidation.Results;
using Jago.CrossCutting.Dto;
using Jago.CrossCutting.Validation;
using Jago.domain.Core.Entities;
using Jago.domain.Interfaces.Repositories;

namespace Jago.Application.Services
{
    public class PassengerServices : IPassengerServices
    {
        private readonly IMapper _mapper;
        private readonly IPassengerRepository _paxRepository;
        private bool _disposed;

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
            var validationResult = new AddPassengerValidator(_paxRepository).Validate(vm);
            if (validationResult.IsValid)
                _paxRepository.Add(entity);

            return validationResult;

        }

        public async Task<bool> Remove(Guid id)
        {
            return await _paxRepository.RemovePassengerAsync(id);
        }

        public ValidationResult Update(PassengerViewModel vm)
        {
            var entity = _mapper.Map<Passenger>(vm);
            var validationResult = new UpdatePassengerValidator(_paxRepository).Validate(vm);

            if (validationResult.IsValid)
            {
                _paxRepository.Update(entity);
            }

            return validationResult;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_paxRepository is IDisposable disposableRepository)
                    {
                        disposableRepository.Dispose();
                    }
                    if (_mapper is IDisposable disposableMapper)
                    {
                        disposableMapper.Dispose();
                    }
                }
                _disposed = true;
            }
        }
        public IEnumerable<PassengerViewModel> GetPax()
        {
            return _mapper.Map<IEnumerable<PassengerViewModel>>(_paxRepository.GetPax());
        }
    }
}
