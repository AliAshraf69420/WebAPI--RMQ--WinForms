using Streamer.Models;
using Streamer.Data;
using Streamer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Streamer.Hubs;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IproductService, productService>();
builder.Services.AddDbContext<dbContextClass>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapHub<MyHub>("/Test");
app.Run();
