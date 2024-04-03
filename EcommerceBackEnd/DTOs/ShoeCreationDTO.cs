using System.ComponentModel.DataAnnotations;

namespace EcommerceBackEnd.DTOs
{
    public class ShoeCreationDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
