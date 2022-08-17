using API.Controllers;
using API.Dtos.UploadBoletos;
using AutoMapper;
using Core.Entities;

namespace API.AutoMapping;

public class ConfigMapping : Profile
{
    public ConfigMapping()
    {
        CreateMap<UploadBoletoDto, UploadBoleto>().ReverseMap();
        CreateMap<Mensagem, Mensagem>().ReverseMap();
    }
}