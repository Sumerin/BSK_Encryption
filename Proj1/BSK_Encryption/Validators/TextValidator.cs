using System;
using System.Globalization;
using System.Windows.Controls;

namespace BSK_Encryption.Validators
{
    /// <summary>
    /// Validation for path if aren't empty or null.
    /// </summary>
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
