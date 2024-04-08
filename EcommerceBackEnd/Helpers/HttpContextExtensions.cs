using Microsoft.EntityFrameworkCore;

namespace EcommerceBackEnd.Helpers
{
    //extentions method for httpcontext
    public static class HttpContextExtensions
    {
        // remember Iqueryable inherits from IEnumerable
        public async static Task InsertPaginationParams<T>(this HttpContext httpContext, IQueryable<T> queryable, int recordsPerPage)
        {
            double amount = await queryable.CountAsync();
            double amountPages = Math.Ceiling(amount / recordsPerPage);
            // Heards MUST not contain whitespaces or special characters
            httpContext.Response.Headers.Add("Amount-of-pages", amountPages.ToString());
        }
    }
}
