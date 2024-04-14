namespace EcommerceBackEnd.Entity
{
    public class ShoesOrdersEntity
    {
        public int ShoesId { get; set; }
        public int OrderId { get; set; }
        public OrdersEntity? Orders { get; set; }
        public ShoesEntity? Shoes { get; set; }  

    }
}
