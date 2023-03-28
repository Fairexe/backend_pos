using backend_cn.BusinessLogic.Product;
using backend_cn.BusinessLogic.receipt;
using backend_cn.BusinessLogic.Receipt;
using backend_cn.BusinessLogic.Unit;
using backend_cn.Context;
using backend_cn.Repositories.Product;
using backend_cn.Repositories.receipt;
using backend_cn.Repositories.unit;
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
builder.Services.AddScoped<IUnitRepository, UnitRepositoryMySql>();
builder.Services.AddScoped<UnitManage>();
builder.Services.AddScoped<UnitDetail>();
builder.Services.AddScoped<IProductRepository, ProductRepositoryMySql>();
builder.Services.AddScoped<ProductManage>();
builder.Services.AddScoped<ProductDetail>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepositoryMySql>();
builder.Services.AddScoped<ReceiptDetail>();
builder.Services.AddScoped<ReceiptManage>();

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
