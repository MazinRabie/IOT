using AutoMapper;
using IOT.Models;
using IOT.Models.DTOs;

namespace IOT
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Client, CreateClientDTO>().ReverseMap();
            CreateMap<Client, UpdateClientDTO>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
