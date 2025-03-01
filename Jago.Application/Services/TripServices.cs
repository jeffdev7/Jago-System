using AutoMapper;
using FluentValidation.Results;
using Jago.CrossCutting.Dto;
using Jago.CrossCutting.Helper;
using Jago.CrossCutting.Validation;
using Jago.domain.Core.Entities;
using Jago.domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<TripViewModel> GetSortedTrips()
        {
            var travel = _tripRepository.GetAll().OrderBy(j => j.Destine)
                  .Include(j => j.Passenger)
                  .Select(j => new TripViewModel
                  {
                      Id = j.Id,
                      Origin = j.Origin,
                      Destine = j.Destine,
                      Departure = j.Departure,
                      Arrival = j.Arrival,
                      PassengerId = j.PassengerId,
                      PaxName = j.Passenger.Name

                  }).AsNoTracking();
            Dispose();
            return travel;
        }
        public TripViewModel GetTripDetails(Guid id)
        {
            var travel = _tripRepository.GetAll()
                  .Include(j => j.Passenger)
                  .Where(_ => _.Id == id)
                  .Select(j => new TripViewModel
                  {
                      Id = j.Id,
                      Origin = j.Origin,
                      Destine = j.Destine,
                      Departure = j.Departure,
                      Arrival = j.Arrival,
                      PassengerId = j.PassengerId,
                      PaxName = j.Passenger.Name

                  }).AsNoTracking().FirstOrDefault();
            Dispose();
            return travel;
        }
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
            var arrivalAdjustment = DateTime.Now;

            if (vm.Departure <= vm.Arrival)
                arrivalAdjustment = entity.Arrival.AddHours(3);
            entity.Arrival = arrivalAdjustment;

            var validationResult = new AddTripValidator().Validate(vm);
            if (validationResult.IsValid)
                _tripRepository.Add(entity);

            return validationResult;
        }

        public async Task<bool> Remove(Guid id)
        {
            return await _tripRepository.RemoveTripAsync(id);
        }

        public ValidationResult Update(TripViewModel vm)
        {
            var entity = _mapper.Map<Trip>(vm);

            if (vm.Arrival.TimeOfDay == vm.Departure.TimeOfDay)
                return ErrorCatalog.CustomErrors();

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
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        } 
    }
}
