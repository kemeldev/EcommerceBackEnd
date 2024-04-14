using Microsoft.AspNetCore.Mvc.Filters;

namespace EcommerceBackEnd.Helpers
{
    public class ErrorsFilter: ExceptionFilterAttribute
    {
        private readonly ILogger<ErrorsFilter> logger;

        public ErrorsFilter(ILogger<ErrorsFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context )
        {
            logger.LogInformation(context.Exception, context.Exception.Message);

            base.OnException(context);
        }
    }
}
