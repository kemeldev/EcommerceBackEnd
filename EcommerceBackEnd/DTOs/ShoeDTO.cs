using System.ComponentModel.DataAnnotations;

namespace EcommerceBackEnd.DTOs
{
    public class ShoeDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
