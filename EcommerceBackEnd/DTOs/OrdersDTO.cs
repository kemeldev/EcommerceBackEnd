using System.ComponentModel.DataAnnotations;

namespace EcommerceBackEnd.DTOs
{
    public class OrdersDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
