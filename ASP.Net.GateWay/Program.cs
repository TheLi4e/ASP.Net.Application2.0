using ASP.Net.Application.SDK;
using ASP.Net.GateWay;
using ASP.Net.GateWay.Query;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IConfiguration cfg = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();

builder.Services.AddOcelot(cfg);
builder.Services.AddSwaggerForOcelot(cfg);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
{
    cb.Register(c => new AppDbContext(builder.Configuration.GetConnectionString("DefaultConnection"))).InstancePerDependency();
});

builder.Services
            .AddSingleton<IStorageService, StorageService>()
            .AddGraphQLServer()
            .AddQueryType<GetProducts>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
}).UseOcelot().Wait();

app.UseHttpsRedirection();
app.MapGraphQL();

app.Run();
