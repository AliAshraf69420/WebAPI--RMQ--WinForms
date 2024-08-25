var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IproductService, productService>();
builder.Services.AddDbContext<dbContextClass>();
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
app.MapHub<MyHub>("/test");
app.Run();