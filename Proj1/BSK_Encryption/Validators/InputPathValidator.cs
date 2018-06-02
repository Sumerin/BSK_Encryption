using System.Globalization;
using System.IO;
using System.Windows.Controls;

namespace BSK_Encryption.Validators
{
    /// <summary>
    /// Validator for path
    /// </summary>
    public class InputPathValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!File.Exists(value as string))
                return new ValidationResult(false, string.Format("This file doesn't Exist {0}",value as string));
            else
                return new ValidationResult(true, null);

        }
    }
}
