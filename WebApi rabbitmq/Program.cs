using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using WebApi_rabbitmq.Data;
using WebApi_rabbitmq.RabbitMQ;
using WebApi_rabbitmq.SIgnalR;
using WebApi_rabbitmq.Services;
using Microsoft.AspNetCore.SignalR;
namespace WebApi_rabbitmq
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSignalR();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddDbContext<DbContextClass>();
            builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var config = builder.Configuration;
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => { builder.AllowAnyMethod(); });
            }
             );

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
