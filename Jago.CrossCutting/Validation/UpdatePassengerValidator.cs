using FluentValidation;
using Jago.CrossCutting.Dto;
using Jago.CrossCutting.Helper;
using Jago.domain.Interfaces.Repositories;
using System.Text.RegularExpressions;

namespace Jago.CrossCutting.Validation
{
    public class UpdatePassengerValidator : AbstractValidator<PassengerViewModel>
    {
        private readonly IPassengerRepository _passengerRepository;
        public UpdatePassengerValidator(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;

            RuleFor(j => j.Name).NotNull();
            RuleFor(j => j.Name).NotEmpty()
                .Matches("^[A-Za-z ]+$");

            RuleFor(j => j.Phone).NotNull();
            RuleFor(j => j.Phone).NotEmpty();

            RuleFor(j => j.Email).NotNull();
            RuleFor(j => j.Email).NotEmpty()
                .EmailAddress();

            RuleFor(j => j.DocumentNumber).NotNull();
            RuleFor(j => j.DocumentNumber)
                .Must(ValidDocument)
                .WithMessage("Invalid Document");

            RuleFor(j => j.DocumentNumber)
               .Must(IsDocumentUnique)
               .WithMessage("This document already exists in our base");


        }
        private static bool ValidDocument(string document) => document.IsValidDocument();
        private bool IsDocumentUnique(string document)
        {
            var result = _passengerRepository.GetPax().Where(_ => _.DocumentNumber == document);
            if (result.Count() == 1 || !result.Any())
                return true;

            return false;
        }
    }

}
