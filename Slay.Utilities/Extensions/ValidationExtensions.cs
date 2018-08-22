namespace Slay.Utilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentValidation.Results;

    using Slay.Utilities.ServiceResult;

    public static class ValidationExtensions
    {
        public static IEnumerable<Error> ToServiceResultErrors(this IEnumerable<ValidationFailure> validationFailures)
        {
            if (validationFailures == null)
            {
                throw new ArgumentNullException(nameof(validationFailures));
            }

            return validationFailures.Select(validationFailure => new Error { Code = validationFailure.ErrorMessage });
        }
    }
}