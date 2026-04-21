using Microsoft.EntityFrameworkCore;
using UserService.Application;
using UserService.Application.Services;
using UserService.Domain;
using UserService.Domain.Models;
using UserService.Domain.Repository;
using UserService.Infrastructure;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repository;
using UserService.Infrastructure.Services;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("testDb"));

        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        // Repository
        builder.Services.AddScoped<IRepository<UserModel>, UserRepository>();
        // Query services
        builder.Services.AddScoped<IUserQueryService, UserQueryService>();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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