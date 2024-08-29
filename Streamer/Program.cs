using Streamer.Data;
using Streamer.Hubs;
using Streamer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Streamer.RabbitMQ_client;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IproductService, productService>();
builder.Services.AddDbContext<dbContextClass>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<RabbitMQReceivercs>(); // Register RabbitMQ receiver here

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapHub<MyHub>("/test");
app.Run();
