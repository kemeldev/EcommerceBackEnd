using System.ComponentModel.DataAnnotations;

namespace EcommerceBackEnd.Entity
{
    public class ShoesEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        // Photo is a string cause we gonna save the url of the image
        public string? Photo { get; set; }
    }
}
