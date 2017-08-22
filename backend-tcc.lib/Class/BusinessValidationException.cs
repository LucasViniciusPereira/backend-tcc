using System;
using System.Collections.Generic;

namespace backend_tcc.lib.Class
{
    public class BusinessValidationException : Exception
    {
        private static ICollection<ValidationRulesResult> lstEmpty = new List<ValidationRulesResult>();

        public static ICollection<ValidationRulesResult> EMPTY { get { return lstEmpty; } }

        public BusinessValidationException(ICollection<ValidationRulesResult> lstValidation)
        {
            ValidationRules = lstValidation;
        }

        public ICollection<ValidationRulesResult> ValidationRules { get; set; }
    }
}
