using API.Controllers;
using API.Model;
using AutoMapper;

namespace API.AutoMapping;

public class ConfigMapping : Profile
{
    public ConfigMapping()
    {
        CreateMap<ReturnUploadProcessDto, UploadProcess>().ReverseMap();
        CreateMap<Mensagem, Mensagem>().ReverseMap();
    }
}