using FluentValidation;
using Jago.CrossCutting.Dto;
using Jago.domain.Core.Entities;
using Jago.domain.Interfaces.Repositories;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Jago.CrossCutting.Validation
{
    public class UpdatePassengerValidator : AbstractValidator<PassengerViewModel>
    {
        private readonly IPassengerRepository _passengerRepository;
        public UpdatePassengerValidator(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;

            RuleFor(j => j.Name).NotEmpty();
            RuleFor(j => j.Name).NotNull();

            RuleFor(j => j.Phone).NotEmpty();
            RuleFor(j => j.Phone).NotNull();

            RuleFor(j => j.Email).NotNull();
            RuleFor(j => j.Email).NotEmpty().EmailAddress();

            RuleFor(j => j.DocumentNumber).NotNull();
            RuleFor(j => j.DocumentNumber).NotEmpty()
                .Must(ValidDocument)
                .WithMessage("Invalid Document");


        }
        private static bool ValidDocument(string document)//EXTENT THIS METHOD
        {
            var patternRg = @"(^\d{1,2}).?(\d{3}).?(\d{3})-?(\d{1}|X|x$)";
            var patternCpf = @"(^\d{3}\.\d{3}\.\d{3}\-\d{2}$)";

            if (Regex.IsMatch(document, patternRg) || Regex.IsMatch(document, patternCpf))
                return true;

            return false;
        }
    }

}
