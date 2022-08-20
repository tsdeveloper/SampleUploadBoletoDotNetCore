using API.Dtos.UploadBoletos;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.UploadBoletos;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Infrastructure.Services;

namespace API.Extensions;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUploadBoletoService, UploadBoletoService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
        services.AddScoped<IValidator<UploadBoleto>, BoletosValidator>();
        services.AddScoped<IValidator<UploadBoletoDto>, UploadBoletosDtoValidator>();
        return services;
    }
}