using API;
using API.AutoMapping;
using API.Controllers;
using API.Dtos.UploadBoletos;
using API.Extensions;
using API.Specifications.UploadBoletos;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.UploadBoletos;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repository;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var configConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IValidator<UploadBoleto>, BoletosValidator>();
builder.Services.AddScoped<IValidator<UploadBoletoDto>, UploadBoletosDtoValidator>();
builder.Services.AddSqlite<UploadBoletoContext>(configConnection);
builder.Services.AddScoped<IUploadBoletoService, UploadBoletoService>();
builder.Services.AddScoped<IBoletoSpecification, BoletoSpecification>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));


builder.Services.AddAutoMapper(typeof(ConfigMapping));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapControllers();


app.Run();