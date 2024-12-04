using Application.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                        // Cấu hình dịch vụ controllers
                        services.AddControllers();

                        // Cấu hình dịch vụ cho UserService
                        services.AddScoped<IUserService, UserService>();
                        services.AddScoped<ITokenService, TokenService>();
                    });
                });
    }
}
