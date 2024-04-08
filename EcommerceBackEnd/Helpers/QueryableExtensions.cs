using EcommerceBackEnd.DTOs;

namespace EcommerceBackEnd.Helpers
{
    public static class QueryableExtensions
    {
        // TODO: investigate about this method
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable
                .Skip((paginationDTO.Page - 1) * paginationDTO.AmountRecordsPerPage)
                .Take(paginationDTO.AmountRecordsPerPage);
                
        }
    }
}
