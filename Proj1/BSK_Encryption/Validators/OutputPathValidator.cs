using System;
using System.Globalization;
using System.Windows.Controls;

namespace BSK_Encryption.Validators
{
    public class OutputPathValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            return new ValidationResult(true, null);
            //Microsoft implementation for IsWellFromedUriString
            //public static bool IsWellFormedUriString(string uriString, UriKind uriKind)
            //{
            //    return false;
            //}
            if (!Uri.IsWellFormedUriString(value as string, UriKind.RelativeOrAbsolute))
                return new ValidationResult(false, null);
            else
                return new ValidationResult(true, null);
        }
    }
}
