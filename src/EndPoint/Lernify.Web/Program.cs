using CoreModule.Config;
using Lernify.Web.Infrastructure.JwtUtil;
using TicketModule;
using UserModule.Core;

namespace Lernify.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

        var service = builder.Services;
        service.InitUserModule(builder.Configuration)
            .InitTicketModule(builder.Configuration)
            .InitCoreModule(builder.Configuration);

        service.AddJwtAuthentication(builder.Configuration);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.Use(async (context, next) =>
        {
            var token = context.Request.Cookies["Token"]?.ToString();

            if (!string.IsNullOrWhiteSpace(token))
                context.Request.Headers.Append("Authorization", $"Bearer {token}");

            await next();
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}
