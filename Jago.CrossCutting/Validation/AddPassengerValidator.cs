using FluentValidation;
using Jago.CrossCutting.Dto;
using System.Text.RegularExpressions;

namespace Jago.CrossCutting.Validation
{
    public class PassengerValidator : AbstractValidator<PassengerViewModel>
    {
        public PassengerValidator()
        {
            RuleFor(j => j.Name).NotEmpty();
            RuleFor(j => j.Name).NotNull();

            RuleFor(j => j.Phone).NotEmpty();
            RuleFor(j => j.Phone).NotNull();

            RuleFor(j => j.Email).NotNull();
            RuleFor(j => j.Email).NotEmpty().EmailAddress();

            RuleFor(j => j.DocumentNumber).NotNull();
            RuleFor(j => j.DocumentNumber).NotEmpty().Must(ValidDocument)
                .WithMessage("Invalid Document");
        }
        private static bool ValidDocument(string document)
        {
            var patternRg = @"(^\d{1,2}).?(\d{3}).?(\d{3})-?(\d{1}|X|x$)";
            var patternCpf = @"(^\d{3}\.\d{3}\.\d{3}\-\d{2}$)";

            if (Regex.IsMatch(document, patternRg) || Regex.IsMatch(document, patternCpf))
                return true;

            return false;
        }
    }

}
