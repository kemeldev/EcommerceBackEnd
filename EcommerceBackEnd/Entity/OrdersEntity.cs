using System.ComponentModel.DataAnnotations;

namespace EcommerceBackEnd.Entity
{
    public class OrdersEntity
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

    }
}
