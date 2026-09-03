using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.Validation
{
    public class DateGreaterThanAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string otherProperty;

        public DateGreaterThanAttribute(string _otherProperty)
        {
            this.otherProperty = _otherProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var otherProp = context.ObjectType.GetProperty(otherProperty);
            if (otherProp == null)
                return new ValidationResult($"Unknown property: {otherProperty}");

            var otherValue = (DateOnly)otherProp.GetValue(context.ObjectInstance);
            var thisValue = (DateOnly)value;

            if (thisValue > otherValue)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessageString, new[] { context.MemberName });
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-dategreaterthan"] = ErrorMessageString;
            context.Attributes["data-val-dategreaterthan-otherproperty"] = otherProperty;
        }
    }
}