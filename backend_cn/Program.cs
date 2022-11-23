using backend_cn.Context;
using backend_cn.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string connectionString = builder.Configuration.GetConnectionString("PosDbContext");
builder.Services.AddDbContext<PosDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddScoped<UnitRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ReceiptRepository>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
