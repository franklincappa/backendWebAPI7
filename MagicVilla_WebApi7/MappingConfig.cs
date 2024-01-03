using AutoMapper;
using MagicVilla_WebApi7.Model;
using MagicVilla_WebApi7.Model.DTO;

namespace MagicVilla_WebApi7
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();

            CreateMap<NumeroVilla, NumeroVillaDto>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaCreateDto>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaUpdateDto>().ReverseMap();

        }
    }
}
