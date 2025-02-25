using AutoMapper;
using FluentValidation.Results;
using Jago.Application.Services;
using Jago.CrossCutting.Dto;
using Jago.domain.Core.Entities;
using Jago.domain.Interfaces.Repositories;
using Moq;

namespace Jago.Test.Application
{
    public class PassengerServicesTests
    {
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IPassengerServices> _paxServices = new Mock<IPassengerServices>();
        private readonly Mock<IPassengerRepository> _paxRepository = new Mock<IPassengerRepository>();

        [Fact]
        public void SHOULD_GET_ALLPASSENGERS()
        {
            //arrange
            var listResult = new List<PassengerViewModel>()
            {
                new PassengerViewModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Pax1",
                    DocumentNumber = "documentRG",
                    Phone = "",
                    Email = "pax@hotmail.com",
                },
                new PassengerViewModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Pax2",
                    DocumentNumber = "documentCPF",
                    Phone = "",
                    Email = "pax@hotmail.com",
                }
            };

            var paxService = new Mock<IPassengerServices>();

            _paxServices.Setup(_ => _.GetAll())
                .Returns(listResult);
            var projectServiceMock = new PassengerServices(_mapper.Object, _paxRepository.Object);

            //act
            var result =  _paxServices.Object.GetAll();

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PassengerViewModel>>(result);
        }

        [Theory]
        [InlineData("e13e6434-88f1-4bec-8300-4a3991b7107b")]
        public void SHOULD_GET_PASSENGERBYID(string paxId)
        {
            //arrange
            var id = Guid.Parse(paxId);
            var expectedProduct = new PassengerViewModel
            {
                Id = id,
                Name = "Pax1",
                DocumentNumber = "documentRG",
                Phone = "",
                Email = "pax@hotmail.com",
            };

            _paxServices.Setup(_ => _.GetById(It.IsAny<Guid>()))
                .Returns(expectedProduct);
            _mapper.Setup(map => map.Map<PassengerViewModel>(It.IsAny<Passenger>())).Returns(expectedProduct);

            var projectServiceMock = new PassengerServices(_mapper.Object, _paxRepository.Object);
       
            //act
            var result = projectServiceMock.GetById(id);

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<PassengerViewModel>(result);
            Assert.Equal(expectedProduct.Id, result.Id);
            Assert.Equal(expectedProduct.Name, result.Name);
            Assert.Equal(expectedProduct.DocumentNumber, result.DocumentNumber);
            Assert.Equal(expectedProduct.Phone, result.Phone);
            Assert.Equal(expectedProduct.Email, result.Email);
        }

        [Theory]
        [InlineData("e13e6434-88f1-4bec-8300-4a3991b7107b")]
        public async Task SHOULD_REMOVE_PASSENGERBYID(string paxId)
        {
            //arrange
            var id = Guid.Parse(paxId);
            var expectedProduct = new PassengerViewModel
            {
                Id = id,
                Name = "Pax1",
                DocumentNumber = "documentRG",
                Phone = "",
                Email = "pax@hotmail.com",
            };

            _paxRepository.Setup(_ => _.RemovePassengerAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            var projectServiceMock = new PassengerServices(_mapper.Object, _paxRepository.Object);

            //act
            var result = await projectServiceMock.Remove(id);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void SHOULD_ADD_PASSENGER()
        {
            //arrange
            var pax  = new PassengerViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Pax",
                DocumentNumber = "35.967.900-6",
                Phone = "12355",
                Email = "pax@hotmail.com",
            };
            var expectedPassenger = new PassengerViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Pax1",
                DocumentNumber = "documentRG",
                Phone = "1425",
                Email = "pax@hotmail.com",
            };
            var passengerEntity = Passenger.Create("pax", "","","");

            _mapper.Setup(map => map.Map<Passenger>(It.IsAny<PassengerViewModel>())).Returns(passengerEntity);
            _mapper.Setup(map => map.Map<PassengerViewModel>(It.IsAny<Passenger>())).Returns(expectedPassenger);
            _paxRepository.Setup(_ => _.Add(It.IsAny<Passenger>()));

            _paxServices.Setup(_ => _.Add(It.IsAny<PassengerViewModel>())).Returns(new ValidationResult());

            var projectServiceMock = new PassengerServices(_mapper.Object, _paxRepository.Object);

            //act
            var result = projectServiceMock.Add(pax);

            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void SHOULD_UPDATE_PASSENGER()
        {
            //arrange
            var pax = new PassengerViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Pax",
                DocumentNumber = "35.967.900-6",
                Phone = "12355",
                Email = "pax@hotmail.com",
            };
            var expectedPassenger = new PassengerViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Pax",
                DocumentNumber = "35.967.900-6",
                Phone = "1425",
                Email = "pax@hotmail.com",
            };
            var passengerEntity = Passenger.Create("pax", "", "", "");

            _mapper.Setup(map => map.Map<Passenger>(It.IsAny<PassengerViewModel>())).Returns(passengerEntity);
            _mapper.Setup(map => map.Map<PassengerViewModel>(It.IsAny<Passenger>())).Returns(expectedPassenger);
            _paxRepository.Setup(_ => _.Update(It.IsAny<Passenger>()));

            _paxServices.Setup(_ => _.Update(It.IsAny<PassengerViewModel>()))
                .Returns(new ValidationResult());

            var projectServiceMock = new PassengerServices(_mapper.Object, _paxRepository.Object);

            //act
            var result =  projectServiceMock.Update(expectedPassenger);

            //assert
            Assert.True(result.IsValid);
        }

    }
}
