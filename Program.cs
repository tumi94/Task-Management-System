
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Task_Management_System__Server_.Data;
using Task_Management_System__Server_.Interfaces;
using Task_Management_System__Server_.Model;
using Task_Management_System__Server_.Repositories;

namespace Task_Management_System__Server_
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

            // Add DbContext with options
       
            builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));


            // Add the reference to TaskRepository
            builder.Services.AddScoped<TaskRepository>();
            // Add the reference to IUnitOfWork
        
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Register your custom middleware
            app.UseMiddleware<ExceptionMiddleware>();

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