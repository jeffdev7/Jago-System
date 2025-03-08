using FluentValidation.Results;

namespace Jago.CrossCutting.Helper
{
    public static class ErrorCatalog
    {
        public static ValidationResult CustomErrors()
        {
            var customErrorResult = new ValidationResult();
            var customErrors = new ValidationFailure();
            customErrors.ErrorMessage = "Departure or arrival date is invalid.";

            customErrorResult.Errors.Add(customErrors);

            return customErrorResult;
        }
    }
}
