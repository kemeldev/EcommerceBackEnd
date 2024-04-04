
using EcommerceBackEnd.Services;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // some variables
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowOrigin = "http://localhost:5173"; // my react app localhost
            var configuration = builder.Configuration;
            //connection string from secrets
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //var connectionString = configuration.GetConnectionString("defaultConnection");

            Console.WriteLine(connectionString);


            // SERVICES

            builder.Services.AddControllers();

            // // dbcontext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            // // Automapper
            builder.Services.AddAutoMapper(typeof(Program));
            // // Azure Store service
            builder.Services.AddTransient<IFileStorage, StoreAzureFiles>();

            // // CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(MyAllowOrigin).AllowAnyMethod().AllowAnyHeader();
                });
            });
            // // swagger 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            // MIDDLEWARES

            if (app.Environment.IsDevelopment())
            {
                // swagger for development envriroment
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // // Cors policy
            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
