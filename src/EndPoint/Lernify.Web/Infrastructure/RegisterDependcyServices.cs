using Lernify.Web.Infrastructure.RazorUtil;

namespace Lernify.Web.Infrastructure
{
    public static class RegisterDependcyServices
    {
        public static IServiceCollection RegisterApiServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandlers>();
            services.AddScoped<IRenderViewToString, RenderViewToString>();

            services.AddAutoMapper(a =>
            {
                a.AddProfile<MapperProfile>();
            });

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
