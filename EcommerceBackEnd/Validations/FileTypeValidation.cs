﻿using System.ComponentModel.DataAnnotations;

namespace EcommerceBackEnd.Validations
{
    public class FileTypeValidation : ValidationAttribute
    {
        private readonly string[] validTypes;

        public FileTypeValidation(string[] validTypes)
        {
            this.validTypes = validTypes;
        }

        public FileTypeValidation(FileTypeEnum fileTypeEnum)
        {
            if(fileTypeEnum == FileTypeEnum.Image)
            {
                validTypes = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // we just want to validate the size, so if it is null, return
            if (value == null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile == null) return ValidationResult.Success;


            if (!validTypes.Contains(formFile.ContentType) )
            {
                return new ValidationResult($"File type must be one of the following: {string.Join(", ", validTypes)}");
            }

            return ValidationResult.Success;
        }
    }
}
