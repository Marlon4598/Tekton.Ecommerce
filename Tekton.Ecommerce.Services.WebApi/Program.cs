using Swashbuckle.AspNetCore.SwaggerUI;
using Tekton.Ecommerce.Application.Interface;
using Tekton.Ecommerce.Application.Main;
using Tekton.Ecommerce.Domain.Core;
using Tekton.Ecommerce.Domain.Interface;
using Tekton.Ecommerce.Infrastructure.Data;
using Tekton.Ecommerce.Infrastructure.Interface;
using Tekton.Ecommerce.Infrastructure.Repository;
using Tekton.Ecommerce.Transversal.Common;
using Tekton.Ecommerce.Transversal.Logging;
using Tekton.Ecommerce.Transversal.Mapper;
using Tekton.Ecommerce.Services.WebApi.Modules.Validator;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;
using FluentValidation;
using Tekton.Ecommerce.Application.DTO;
using Tekton.Ecommerce.Application.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient("DiscountApi", (client) => {
client.BaseAddress = new Uri("https://63f774dee40e087c958f494d.mockapi.io");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidator();
builder.Services.AddScoped<IDiscountProductRepository, DiscountProductRepository>();
builder.Services.AddScoped<IDiscountProductDomain, DiscountProductDomain>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


builder.Services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IProductsApplication, ProductsApplication>();
builder.Services.AddScoped<IProductsDomain, ProductsDomain>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<AbstractValidator<ProductsDto>, ProductDtoValidator>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "tekton");
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { };
