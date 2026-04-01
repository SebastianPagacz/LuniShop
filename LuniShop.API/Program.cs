using LuniShop.API.Middleware;
using LuniShop.Application;
using LuniShop.Application.Products.DTO;
using LuniShop.Application.Services;
using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using LuniShop.Infrastructure;
using LuniShop.Infrastructure.Context;
using LuniShop.Infrastructure.Repository;
using LuniShop.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
            builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IQueryService<ProductDto>, ProductQueryService>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>(); // Placing registration of Middleware above does matter, now it has "visibility" on other parts of pipeline

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
