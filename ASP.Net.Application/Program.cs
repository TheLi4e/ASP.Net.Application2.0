using ASP.Net.Application;
using ASP.Net.Application.Interfaces;
using ASP.Net.Application.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using sdk = ASP.Net.Application.SDK;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AppProfile));
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IStorageService, StorageService>();
builder.Services.AddMemoryCache();



builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var config = new ConfigurationBuilder();
config.AddJsonFile("appsettings.json");
var cfg = config.Build();

builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.Register(c => new sdk.AppDbContext(cfg.GetConnectionString("DefaultConnection"))).InstancePerDependency();
});

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