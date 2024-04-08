namespace EcommerceBackEnd.DTOs
{
    public class PaginationDTO
    {
        // default page
        public int Page { get; set; } = 1;
        private int recordsPerPage = 10;
        private readonly int maxAmountRecordsPerPage = 30;

        public int AmountRecordsPerPage
        {
            get => recordsPerPage;
            set
            {
                recordsPerPage = ( value > maxAmountRecordsPerPage ) ? maxAmountRecordsPerPage : value;
            }
        }
    }
}
