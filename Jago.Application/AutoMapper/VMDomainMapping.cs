using AutoMapper;
using Jago.CrossCutting.Dto;
using Jago.domain.Entities;

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
