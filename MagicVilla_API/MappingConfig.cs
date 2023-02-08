using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;

namespace MagicVilla_API

{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        }
    }
}
