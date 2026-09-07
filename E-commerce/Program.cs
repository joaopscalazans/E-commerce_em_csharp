using E_commerce.Infa;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();
app.UseHttpsRedirection();
app.Run();