using AutoMapper;
using Jago.Application.ViewModel;
using Jago.domain.Core.Entities;

namespace Jago.Application.AutoMapper
{
    public class DomainVMMapping : Profile
    {
        public DomainVMMapping()
        {
            CreateMap<Passenger, PassengerViewModel>();
            CreateMap<Trip, TripViewModel>();
        }
    }
}
