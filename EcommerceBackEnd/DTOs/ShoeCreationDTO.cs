using EcommerceBackEnd.Validations;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackEnd.DTOs
{
    public class ShoeCreationDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        // IformFile allow us to recieve any type of files, so we create custom validations
        [FileSizeValidation(maxSizeMegaBytes: 4)]
        [FileTypeValidation(fileTypeEnum: FileTypeEnum.Image)]
        public IFormFile? Photo { get; set; }
    }
}
