using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace TodoApi.Security
{
    // Quando alguém faça uma chamada para a API,
    // eu verifique se foi passado um par de tenant id e tenant key válidos
    public class ApiTenantFilter : IAsyncResourceFilter
    {
        const string TENANT_ID_HEADER = "X-Tenant-Id";
        const string TENANT_KEY_HEADER = "X-Tenant-Key";
        private Dictionary<string, string> tenants;

        public ApiTenantFilter(IOptions<ApiAccessOptions> options)
        {
            tenants = options.Value?.Tenants ??  new Dictionary<string, string>();    
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (context.HttpContext.Request.Headers.ContainsKey(TENANT_ID_HEADER) == false ||
                context.HttpContext.Request.Headers.ContainsKey(TENANT_KEY_HEADER) == false)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.HttpContext.Response.WriteAsync("Tenant ID and Tenant Key headers are required.");
                return;
            }

            var tenantId = context.HttpContext.Request.Headers[TENANT_ID_HEADER].ToString();
            var tenantKey = context.HttpContext.Request.Headers[TENANT_KEY_HEADER].ToString();

            var isValidCredentials = tenants.Any(x =>   x.Key.Equals(tenantId, StringComparison.OrdinalIgnoreCase) &&
                                                        x.Value.Equals(tenantKey, StringComparison.OrdinalIgnoreCase));

            if (!isValidCredentials)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.HttpContext.Response.WriteAsync("The combination of tenant id and tenant key is invalid");

                return;
            }
            
            await next();
        }
    }
}
