using System.ComponentModel.DataAnnotations;

namespace EcommerceBackEnd.Validations
{
    public class FileSizeValidation: ValidationAttribute
    {
        private readonly int maxSizeMegaBytes;

        public FileSizeValidation(int maxSizeMegaBytes)
        {
            this.maxSizeMegaBytes = maxSizeMegaBytes;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            // we just want to validate the size, so if it is null, return
            if (value == null) return ValidationResult.Success; 
            
            IFormFile formFile = value as IFormFile;

            if (formFile == null) return ValidationResult.Success;

            if (formFile.Length > maxSizeMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"File size cannot exceeed {maxSizeMegaBytes}mb");
            }

            return ValidationResult.Success;
            
        }
    }
}
