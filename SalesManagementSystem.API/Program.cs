using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Infrastructure.Data;
using SalesManagementSystem.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using SalesManagementSystem.Application.Pipelines;
using SalesManagementSystem.API.Middleware;
using SalesManagementSystem.Application.Interfaces.Queries;
using SalesManagementSystem.Infrastructure.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(connectionString));


builder.Services.AddMediatR(typeof(IUnitOfWork).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddValidatorsFromAssembly(typeof(IUnitOfWork).Assembly);
builder.Services.AddScoped<IAdvancedCustomerQueryService, AdvancedCustomerQueryService>();
//builder.Services.AddScoped<IAdvancedProductQueryService, AdvancedProductQueryService>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();