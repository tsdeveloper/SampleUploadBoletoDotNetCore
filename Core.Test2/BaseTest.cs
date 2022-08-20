using API.Dtos.UploadBoletos;
using AutoMapper;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Hosting;
using Moq;

namespace Core.Test2;

public abstract class BaseTest
{
    protected  IMapper _mapper; 
    protected IHostingEnvironment HostingEnvironment { get; private set; }
    
    public void Inicializador()
    {
        var mockEnvironment = new Mock<IHostingEnvironment>();
        
        var serviceProvider = new ServiceCollection()
            .BuildServiceProvider();
        
        //auto mapper configuration
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        });
        _mapper = mockMapper.CreateMapper();
        mockEnvironment.Setup(m => m.EnvironmentName)
            .Returns("Hosting:UnitTestEnvironment");
        HostingEnvironment = mockEnvironment.Object;
    }
}

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UploadBoletoDto, UploadBoleto>().ReverseMap();
    }
}