using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace API.Configurations
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Cliente, ClienteResponse>().ReverseMap();
            CreateMap<Cliente, ClienteRequest>().ReverseMap();

            CreateMap<Profissao, ProfissaoResponse>().ReverseMap();
            CreateMap<Profissao, ProfissaoRequest>().ReverseMap();
        }
    }
}
