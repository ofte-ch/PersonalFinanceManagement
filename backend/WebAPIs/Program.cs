using DataAccess.Configuration;
using Application.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
namespace WebAPIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // Cấu hình dịch vụ Cookie Authentication
                        services.AddAuthentication("Cookies")
                            .AddCookie("Cookies", options =>
                            {
                                options.LoginPath = "/api/auth/login"; 
                                options.LogoutPath = "/api/auth/logout";
                            });

                        // Cấu hình dịch vụ controllers
                        services.AddControllers();

                        // Cấu hình dịch vụ cho UserService
                        services.AddScoped<IUserService, UserService>();
                    });
                });
    }
}
