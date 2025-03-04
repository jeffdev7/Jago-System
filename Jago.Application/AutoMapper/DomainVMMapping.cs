using AutoMapper;
using Jago.CrossCutting.Dto;
using Jago.domain.Entities;

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
