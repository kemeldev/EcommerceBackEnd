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
            // // We need to add the for member, cause photo in shoeCreationDto comes from an Iformfile, but in entity photo is a string
            CreateMap<ShoeCreationDTO, ShoesEntity>()
                .ForMember(x => x.Photo, options => options.Ignore());
        }
    }
}
