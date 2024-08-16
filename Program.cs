using Comati3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace Comati3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddScoped<Cookies>(); //instance of dealing with cookies, Cookies file created by user
            builder.Services.AddScoped<PasswordHasher<object>>(); //instance of hashing password, MS's Identity class used.
            // Configure the DbContext
            builder.Services.AddDbContext<ComatiContext>(options =>
                options.UseMySQL("Server=localhost;Database=comati;Uid=root;Pwd=L-v11wK8XyIadp4g;"));

            // Add CORS
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins("https://comati.tekmobile.online")
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .AllowAnyHeader();
                });
            });
            /*builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                //serverOptions.Listen(System.Net.IPAddress.Any, 5000);
                serverOptions.Listen(System.Net.IPAddress.Any, 5000, listenOptions =>
            {
                listenOptions.UseHttps();
            });

            });*/

            var app = builder.Build();

            // Apply migrations at startup
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ComatiContext>();
                dbContext.Database.Migrate(); // This line applies pending migrations
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            // Enable Authentication and Authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
