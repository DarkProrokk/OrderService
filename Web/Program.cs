using System.Reflection;
using Application.Commands;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Repository;
using Infrastructure.Context;
using Infrastructure.External;
using Infrastructure.Repository;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"appsettings.Development.json", true, true);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//Register Controller
var presentationAssembly = Assembly.Load("Presentation");
builder.Services.AddControllers().AddApplicationPart(presentationAssembly);

//Register OpenApi
builder.Services.AddOpenApi();

//add http clients
builder.Services.AddHttpClient("ProcessingService", client =>
{
    client.BaseAddress = new Uri("https://localhost:7179/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddScoped<IProcessingServiceClient, ProcessingServiceClient>();


//Add MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

//Register DbContext
builder.Services.AddDbContext<OrderContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

//Register Dependency
builder.Services.AddScoped<IOrderNumberGenerator, OrderNumberGenerator>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
