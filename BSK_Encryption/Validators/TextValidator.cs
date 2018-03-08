using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BSK_Encryption.Validators
{
    public class TextValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty(value as string))
                return new ValidationResult(false, "Empty Path");
            else
                return new ValidationResult(true, null);
        }
    }
}
