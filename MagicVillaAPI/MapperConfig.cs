using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;

namespace MagicVillaAPI
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Villa, VillaDTO>().PreserveReferences();
            CreateMap<Villa, VillaCreateDTO>().PreserveReferences();
            CreateMap<Villa, VillaUpdateDTO>().PreserveReferences();
        }
    }
}
