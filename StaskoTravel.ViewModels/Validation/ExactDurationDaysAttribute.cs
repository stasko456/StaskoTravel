using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.Validation
{
    public class ExactDurationDaysAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string startProperty;

        public ExactDurationDaysAttribute(string _startProperty)
        {
            this.startProperty = _startProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var startDate = context.ObjectType.GetProperty(startProperty);
            if (startDate == null)
                return new ValidationResult($"Unknown property: {startProperty}");

            var startDateValue = (DateOnly)startDate.GetValue(context.ObjectInstance);
            var endDateValue = (DateOnly)value;

            if ((endDateValue.DayNumber - startDateValue.DayNumber) <= 14)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessageString, new[] { context.MemberName });
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-exactdurationdays"] = ErrorMessageString;
            context.Attributes["data-val-exactdurationdays-startproperty"] = startProperty;
        }
    }
}