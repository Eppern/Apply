using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apply.Models {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class YearGreaterOrEqualToAttribute : ValidationAttribute {
        string otherPropertyName;

        public YearGreaterOrEqualToAttribute(string otherPropertyName, string errorMessage)
            : base(errorMessage) {
            this.otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            ValidationResult validationResult = ValidationResult.Success;
            try {
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);
                if (otherPropertyInfo.PropertyType == typeof(int)) {
                    int toValidate = (int)value;
                    int referenceProperty = (int)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                    if (toValidate < referenceProperty) {
                        validationResult = new ValidationResult(ErrorMessageString);
                    }
                }
                else {
                    validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type int");
                }
            }
            catch (Exception ex) {
                throw ex;
            }

            return validationResult;
        }
    }
}