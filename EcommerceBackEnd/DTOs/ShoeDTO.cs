using System.ComponentModel.DataAnnotations;

namespace EcommerceBackEnd.DTOs
{
    public class ShoeDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        // Photo is a string cause we are returning the url where the image is store in our DB
        public string? Photo { get; set; }
    }
}
