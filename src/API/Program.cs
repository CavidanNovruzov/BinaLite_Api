using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.Validations.PropertyAd;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using Persistance.Repositories;
using Persistance.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePropertyAdRequestValidator>();

// DbContext
builder.Services.AddDbContext<BinaLiteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// AutoMapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Repositories
builder.Services.AddScoped<IPropertyAdRepository, PropertyAdRepository>();
builder.Services.AddScoped<IPropertyMediaRepository, PropertyMediaRepository>();

// Services
builder.Services.AddScoped<IPropertyAdService, PropertyAdService>();
builder.Services.AddScoped<IPropertyMediaService, PropertyMediaService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "BinaLite API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
