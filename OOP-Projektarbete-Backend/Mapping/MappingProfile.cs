using AutoMapper;
using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Models;

namespace OOP_Projektarbete_Backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Message, GetMessageDTO>()
                .ForMember(dest => dest.Message, act => act.MapFrom(src => src.MessageContent))
                .ForMember(dest => dest.From, act => act.MapFrom(src => src.SentBy));
    
        }
    }
}
