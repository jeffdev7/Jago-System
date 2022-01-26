using AutoMapper;
using Jago.Application.ViewModel;
using Jago.domain.Core.Entities;

namespace Jago.Application.AutoMapper
{
    public class VMDomainMapping : Profile
    {
        public VMDomainMapping()
        {
            CreateMap<PassengerViewModel, Passenger>();
            CreateMap<TripViewModel, Trip>();
        }
    }
}
