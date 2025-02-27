using AutoMapper;
using FluentValidation.Results;
using Jago.Application.Services;
using Jago.CrossCutting.Dto;
using Jago.domain.Core.Entities;
using Jago.domain.Interfaces.Repositories;
using Moq;

namespace Jago.Test.Application
{
    public class TripServicesTests
    {
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<ITripServices> _tripServices = new Mock<ITripServices>();
        private readonly Mock<ITripRepository> _tripRepository = new Mock<ITripRepository>();
        private readonly Mock<IPassengerServices> _paxServices = new Mock<IPassengerServices>();

        [Fact]
        public void SHOULD_GET_ALLTRIPS()
        {
            //arrange
            var listResult = new List<TripViewModel>()
            {
                new TripViewModel
                {
                    PaxName = "",
                    PassengerId = Guid.NewGuid(),
                    Arrival = DateTime.Now,
                    Departure = DateTime.Now,
                    Destine = "",
                    Origin = ""
                },
                new TripViewModel
                {
                    PaxName = "",
                    PassengerId = Guid.NewGuid(),
                    Arrival = DateTime.Now,
                    Departure = DateTime.Now,
                    Destine = "",
                    Origin = ""
                }
            };

            _tripServices.Setup(_ => _.GetAll())
                .Returns(listResult);
            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = _tripServices.Object.GetAll();

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<TripViewModel>>(result);
        }

        [Fact]
        public void SHOULD_GET_SORTEDTRIPS()
        {
            //arrange
            var listResult = new List<TripViewModel>()
            {
                new TripViewModel
                {
                   PaxName = "John",
                   PassengerId = Guid.NewGuid(),
                   Arrival = DateTime.Now.AddDays(2),
                   Departure = DateTime.Now.AddDays(1),
                   Destine = "MDV",
                   Origin = "GRU"
                },
                new TripViewModel
                {
                    PaxName = "John",
                    PassengerId = Guid.NewGuid(),
                    Arrival = DateTime.Now.AddDays(2),
                    Departure = DateTime.Now.AddDays(1),
                    Destine = "MDV",
                    Origin = "GRU"
                }
            };

            _tripServices.Setup(_ => _.GetSortedTrips())
                .Returns(listResult);
            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = _tripServices.Object.GetSortedTrips();

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<TripViewModel>>(result);
        }

        [Theory]
        [InlineData("e13e6434-88f1-4bec-8300-4a3991b7107b")]
        public void SHOULD_GET_TRIPDETAILS(string tripId)
        {
            //arrange
            var id = Guid.Parse(tripId);
            var detailedTrip = new TripViewModel
            {
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now.AddDays(2),
                Departure = DateTime.Now.AddDays(1),
                Destine = "MDV",
                Origin = "GRU"
            };
            
            _tripServices.Setup(_ => _.GetTripDetails(It.IsAny<Guid>()))
                .Returns(detailedTrip);
            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = _tripServices.Object.GetTripDetails(id);

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TripViewModel>(result);
        }

        [Theory]
        [InlineData("e13e6434-88f1-4bec-8300-4a3991b7107b")]
        public void SHOULD_GET_TRIPBYID(string tripId)
        {
            //arrange
            var id = Guid.Parse(tripId);
            var expectedTrip = new TripViewModel
            {
                Id = id,
                PaxName = "",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now,
                Departure = DateTime.Now,
                Destine = "",
                Origin = ""
            };

            _tripServices.Setup(_ => _.GetById(It.IsAny<Guid>()))
                .Returns(expectedTrip);
            _mapper.Setup(map => map.Map<TripViewModel>(It.IsAny<Trip>())).Returns(expectedTrip);

            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = projectServiceMock.GetById(id);

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TripViewModel>(result);
            Assert.Equal(expectedTrip.Id, result.Id);
            Assert.Equal(expectedTrip.Origin, result.Origin);
            Assert.Equal(expectedTrip.Destine, result.Destine);
            Assert.Equal(expectedTrip.Arrival, result.Arrival);
            Assert.Equal(expectedTrip.Departure, result.Departure);
            Assert.Equal(expectedTrip.PaxName, result.PaxName);
            Assert.Equal(expectedTrip.PassengerId, result.PassengerId);
        }

        [Theory]
        [InlineData("e13e6434-88f1-4bec-8300-4a3991b7107b")]
        public async Task SHOULD_REMOVE_TRIPBYID(string tripId)
        {
            //arrange
            var id = Guid.Parse(tripId);

            _tripRepository.Setup(_ => _.RemoveTripAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = await projectServiceMock.Remove(id);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void SHOULD_ADD_TRIP()
        {
            //arrange
            var trip = new TripViewModel
            {
                Id = Guid.NewGuid(),
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now.AddDays(2),
                Departure = DateTime.Now.AddDays(1),
                Destine = "MVD",
                Origin = "GRU"
            };
            var expectedTrip = new TripViewModel
            {
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now.AddDays(2),
                Departure = DateTime.Now.AddDays(1),
                Destine = "MDV",
                Origin = "GRU"
            };
            var tripEntity = Trip.Create("MVD", "GRU", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), trip.PassengerId);

            _mapper.Setup(map => map.Map<Trip>(It.IsAny<TripViewModel>())).Returns(tripEntity);
            _mapper.Setup(map => map.Map<TripViewModel>(It.IsAny<Trip>())).Returns(expectedTrip);
            _tripRepository.Setup(_ => _.Add(It.IsAny<Trip>()));

            _tripServices.Setup(_ => _.Add(It.IsAny<TripViewModel>())).Returns(new ValidationResult());

            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = projectServiceMock.Add(trip);

            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void SHOULD_UPDATE_TRIP()
        {
            //arrange
            var trip = new TripViewModel
            {
                Id = Guid.NewGuid(),
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now.AddDays(2),
                Departure = DateTime.Now.AddDays(1),
                Destine = "MVD",
                Origin = "GRU"
            };
            var expectedTrip = new TripViewModel
            {
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now.AddDays(2),
                Departure = DateTime.Now.AddDays(1),
                Destine = "MDV",
                Origin = "FOR"
            };
            var tripEntity = Trip.Create("MVD", "GRU", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), trip.PassengerId);

            _mapper.Setup(map => map.Map<Trip>(It.IsAny<TripViewModel>())).Returns(tripEntity);
            _mapper.Setup(map => map.Map<TripViewModel>(It.IsAny<Trip>())).Returns(expectedTrip);
            _tripRepository.Setup(_ => _.Update(It.IsAny<Trip>()));

            _tripServices.Setup(_ => _.Update(It.IsAny<TripViewModel>()))
                .Returns(new ValidationResult());

            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = projectServiceMock.Update(expectedTrip);

            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void SHOULD_GET_PAXLIST()
        {
            //arrange
            var listResult = new List<PaxListModel>()
            {
                new PaxListModel
                {
                   Id = Guid.NewGuid(),
                   Name = "John"
                }
            }.AsQueryable();

            _tripServices.Setup(_ => _.GetPaxList())
                .Returns(listResult);
            _tripRepository.Setup(_ => _.GetPaxList()).Returns((IQueryable<PaxListModel>)listResult);
            var tripServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = tripServiceMock.GetPaxList();

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PaxListModel>>(result);
        }

        [Fact]
        public void SHOULDNOT_ADD_TRIP_RETURNS_DATEVALITIONERRORS()
        {
            //arrange
            var trip = new TripViewModel
            {
                Id = Guid.NewGuid(),
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now,
                Departure = DateTime.Now,
                Destine = "MVD",
                Origin = "GRU"
            };
            var expectedTrip = new TripViewModel
            {
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now.AddDays(2),
                Departure = DateTime.Now.AddDays(1),
                Destine = "MDV",
                Origin = "GRU"
            };
            var tripEntity = Trip.Create("MVD", "GRU", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), trip.PassengerId);

            _mapper.Setup(map => map.Map<Trip>(It.IsAny<TripViewModel>())).Returns(tripEntity);
            _mapper.Setup(map => map.Map<TripViewModel>(It.IsAny<Trip>())).Returns(expectedTrip);
            _tripRepository.Setup(_ => _.Add(It.IsAny<Trip>()));

            _tripServices.Setup(_ => _.Add(It.IsAny<TripViewModel>())).Returns(new ValidationResult());

            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = projectServiceMock.Add(trip);

            //assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void SHOULDNOT_ADD_TRIP_WHEN_DEPARTUREISTODAY()
        {
            //arrange
            var trip = new TripViewModel
            {
                Id = Guid.NewGuid(),
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now.AddDays(1),
                Departure = DateTime.Now,
                Destine = "MVD",
                Origin = "GRU"
            };
            var expectedTrip = new TripViewModel
            {
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now.AddDays(2),
                Departure = DateTime.Now.AddDays(1),
                Destine = "MDV",
                Origin = "GRU"
            };
            var tripEntity = Trip.Create("MVD", "GRU", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), trip.PassengerId);

            _mapper.Setup(map => map.Map<Trip>(It.IsAny<TripViewModel>())).Returns(tripEntity);
            _mapper.Setup(map => map.Map<TripViewModel>(It.IsAny<Trip>())).Returns(expectedTrip);
            _tripRepository.Setup(_ => _.Add(It.IsAny<Trip>()));

            _tripServices.Setup(_ => _.Add(It.IsAny<TripViewModel>())).Returns(new ValidationResult());

            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = projectServiceMock.Add(trip);

            //assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void SHOULDNOT_ADD_TRIP_WITHOUTPASSENGER()
        {
            //arrange
            var trip = new TripViewModel
            {
                Id = Guid.NewGuid(),
                PaxName = "John",
                PassengerId = Guid.Empty,
                Arrival = DateTime.Now.AddDays(2),
                Departure = DateTime.Now.AddDays(1),
                Destine = "MVD",
                Origin = "GRU"
            };
            var expectedTrip = new TripViewModel
            {
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now.AddDays(2),
                Departure = DateTime.Now.AddDays(1),
                Destine = "MDV",
                Origin = "GRU"
            };
            var tripEntity = Trip.Create("MVD", "GRU", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), trip.PassengerId);

            _mapper.Setup(map => map.Map<Trip>(It.IsAny<TripViewModel>())).Returns(tripEntity);
            _mapper.Setup(map => map.Map<TripViewModel>(It.IsAny<Trip>())).Returns(expectedTrip);
            _tripRepository.Setup(_ => _.Add(It.IsAny<Trip>()));

            _tripServices.Setup(_ => _.Add(It.IsAny<TripViewModel>())).Returns(new ValidationResult());

            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = projectServiceMock.Add(trip);

            //assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void SHOULDNOT_UPDATE_TRIP_WHEN_EQUALDATES()
        {
            //arrange
            DateTime fixedDateTime = new DateTime(2024, 2, 27, 14, 30, 0);
            DateTime anotherDateTime = fixedDateTime;

            var trip = new TripViewModel
            {
                Id = Guid.NewGuid(),
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = DateTime.Now,
                Departure = DateTime.Now,
                Destine = "MVD",
                Origin = "GRU"
            };
            var expectedTrip = new TripViewModel
            {
                PaxName = "John",
                PassengerId = Guid.NewGuid(),
                Arrival = fixedDateTime,
                Departure = anotherDateTime,
                Destine = "MDV",
                Origin = "FOR"
            };
            var tripEntity = Trip.Create("MVD", "GRU", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), trip.PassengerId);

            _mapper.Setup(map => map.Map<Trip>(It.IsAny<TripViewModel>())).Returns(tripEntity);
            _mapper.Setup(map => map.Map<TripViewModel>(It.IsAny<Trip>())).Returns(expectedTrip);
            _tripRepository.Setup(_ => _.Update(It.IsAny<Trip>()));

            _tripServices.Setup(_ => _.Update(It.IsAny<TripViewModel>()))
                .Returns(new ValidationResult());

            var projectServiceMock = new TripServices(_mapper.Object, _tripRepository.Object);

            //act
            var result = projectServiceMock.Update(expectedTrip);

            //assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}
