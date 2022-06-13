using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.AutoMapper
{
    public static class AttributeValidator
    {
        /// <summary>
        /// Validates an object by scanning all properties and validating against validation attributes associated to each one
        /// </summary>
        /// <param name="objectToValidate">Object to be validated</param>
        public static void Validate(object objectToValidate)
        {
            if (!(objectToValidate is IEnumerable list))
                ValidateSingleObject(objectToValidate);
            else
                ValidateEnumerableObject(list);
        }

        #region Helper methods
        private static void ValidateEnumerableObject(IEnumerable enumerable)
        {
            var recursiveResultList = new List<ValidationResult>();
            var isObjectValid = true;

            foreach (var item in enumerable)
            {
                var results = GetValidationResults(item);
                var isItemValid = !results.Any();

                if (!isItemValid && isObjectValid)
                    isObjectValid = false;

                recursiveResultList.AddRange(results);
            }

            if (isObjectValid) return;

            var message = string.Join(" ", recursiveResultList.Select(r => r.ErrorMessage));
            throw new ValidationException(message);
        }

        private static void ValidateSingleObject(object objectToValidate)
        {
            var results = GetValidationResults(objectToValidate);

            if (!results.Any()) return;

            var message = string.Join(" ", results.Select(r => r.ErrorMessage));
            throw new ValidationException(message);
        }

        private static List<ValidationResult> GetValidationResults(object objectToValidate)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(objectToValidate, serviceProvider: null, items: null);

            Validator.TryValidateObject(objectToValidate, context, results, true);

            return results;
        }

        #endregion
    }
}
