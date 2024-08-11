using Comati3.Models;
using Microsoft.EntityFrameworkCore;
using Comati3.Services;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
namespace Comati3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<Cookies>(); //instance of dealing with cookies, Cookies file created by user
            builder.Services.AddScoped<PasswordHasher<object>>(); //instance of hashing password, MS's Identity class used.
            // Configure the DbContext
            builder.Services.AddDbContext<ComatiContext>(options =>
                options.UseMySQL("Server=localhost;Database=comati;Uid=root;Pwd=L-v11wK8XyIadp4g;"));

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            
            
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
