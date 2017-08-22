using System;
using System.ComponentModel.DataAnnotations;

namespace backend_tcc.lib
{
    public class CheckDecimalGreaterThanAttribute : ValidationAttribute
    {
        public CheckDecimalGreaterThanAttribute(string PropertyName)
        {
            this.PropertyName = PropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectInstance.GetType().GetProperty(PropertyName);

            var valor = Convert.ToDecimal(property.GetValue(validationContext.ObjectInstance));

            if (Convert.ToDecimal(value) < valor)
                return new ValidationResult(string.Format("O campo {0} deverá ser menor que {1}", PropertyName, validationContext.MemberName));

            return base.IsValid(value, validationContext);
        }


        public string PropertyName { get; set; }
    }
}

