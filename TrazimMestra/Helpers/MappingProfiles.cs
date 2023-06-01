using AutoMapper;
using Core.Entities;
using Core.Models;
using TrazimMestra.Dtos;

namespace TrazimMestra.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Condition(src => src.Password != null))
                .ForMember(dest => dest.City, opt => opt.Ignore());

            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Condition(src => src.Password != null))
                .ForMember(dest => dest.City, opt => opt.Ignore());

            CreateMap<User, MestarDto>();

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Mestar, MestarDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.MestarCategories.Select(mc => mc.Category)))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));

            CreateMap<Natjecaj, NatjecajDto>();
            CreateMap<User, BasicUserDto>();
            CreateMap<OfferDto, Offer>();
            CreateMap<NewNatjecajDto, Natjecaj>();
        }
    }
}
