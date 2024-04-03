using AutoMapper;
using EcommerceBackEnd.DTOs;
using EcommerceBackEnd.Entity;

namespace EcommerceBackEnd.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Mapping DTOs to entities and viceversa
            CreateMap<ShoesEntity, ShoeDTO>().ReverseMap();
            CreateMap<ShoeCreationDTO, ShoesEntity>();
        }
    }
}
