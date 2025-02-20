using AutoMapper;
using FluentValidation.Results;
using Jago.CrossCutting.Dto;
using Jago.CrossCutting.Validation;
using Jago.domain.Core.Entities;
using Jago.domain.Interfaces.Repositories;

namespace Jago.Application.Services
{
    public class TripServices : ITripServices
    {
        private readonly IMapper _mapper;
        private readonly ITripRepository _tripRepository;

        public TripServices(IMapper mapper, ITripRepository TripRepository)
        {
            _mapper = mapper;
            _tripRepository = TripRepository;
        }
        public IEnumerable<TripViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<TripViewModel>>(_tripRepository.GetAll());
        }
        //ORDER BY DESTINY
        //public IEnumerable<TripViewModel> GetOrder()
        //{
        //    var travel = _tripRepository.GetAll().OrderBy(j => j.Destine)
        //          .Include(j => j.Passenger)
        //          .Select(j => new TripViewModel
        //          {
        //              Id = j.Id,
        //              Origem = j.Origem,
        //              Destino = j.Destino,
        //              Departure = j.Departure,
        //              Arrival = j.Arrival,
        //              PassageiroId = j.PassengerId,
        //              PaxName = j.Passenger.Name

        //          }).AsNoTracking();
        //    Dispose();
        //    return travel;
        //}
        public TripViewModel GetById(Guid id)
        {
            return _mapper.Map<TripViewModel>(_tripRepository.GetById(id));
        }
        public IEnumerable<TripViewModel> GetAllBy(Func<Trip, bool> exp)
        {
            return _mapper.Map<IEnumerable<TripViewModel>>(_tripRepository.GetAllBy(exp));
        }
        public ValidationResult Add(TripViewModel vm)
        {
            var entity = _mapper.Map<Trip>(vm);
            var validationResult = new AddTripValidator().Validate(vm);
            if (validationResult.IsValid)
                _tripRepository.Add(entity);

            return validationResult;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public async Task<bool> Remove(Guid id)
        {
            return await _tripRepository.RemoveTripAsync(id);
        }

        public ValidationResult Update(TripViewModel vm)
        {
            var entity = _mapper.Map<Trip>(vm);
            var validationResult = new AddTripValidator().Validate(vm);
            if (validationResult.IsValid)
                _tripRepository.Update(entity);
            return validationResult;
        }
        IEnumerable<PassengerViewModel> ITripServices.GetPax()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<PaxListModel> GetPaxList()
        {
            return _tripRepository.GetPaxList();
        }

        public IEnumerable<TripViewModel> GetOrder()
        {
            throw new NotImplementedException();
        }
    }
}
