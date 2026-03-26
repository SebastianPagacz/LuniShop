using LuniShop.Application;
using LuniShop.Application.Services;
using LuniShop.Domain;
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

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductQueryService, ProductQueryService>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
