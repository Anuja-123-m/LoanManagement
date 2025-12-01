using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Database;
using LoanManagementAPI.Models;

namespace LoanManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<LoanDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<LoanDbContext>()
                .AddDefaultTokenProviders();

            // Register Repositories
            builder.Services.AddScoped<Repository.IAdminRepository, Repository.AdminRepository>();
            builder.Services.AddScoped<Repository.CustomerRepo.ICustomerRepository, Repository.CustomerRepo.CustomerRepository>();
            builder.Services.AddScoped<Repository.LoanOfficerRepo.ILoanOfficerRepository, Repository.LoanOfficerRepo.LoanOfficerRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.MapIdentityApi<User>();

            app.UseAuthorization();

            app.UseCors("AllowAngular");

            app.MapControllers();

            app.Run();
        }
    }
}
