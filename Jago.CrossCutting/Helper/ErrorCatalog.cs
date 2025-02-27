using FluentValidation.Results;

namespace Jago.CrossCutting.Helper
{
    public static class ErrorCatalog
    {
        public static ValidationResult CustomErrors()
        {
            var customErrorResult = new ValidationResult();
            var customErrors = new ValidationFailure();
            customErrors.ErrorMessage = "THINK ABOUT A MESSAGE";

            customErrorResult.Errors.Add(customErrors);

            return customErrorResult;
        }
    }
}
