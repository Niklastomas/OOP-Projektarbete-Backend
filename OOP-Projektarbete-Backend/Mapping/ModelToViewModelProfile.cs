using AutoMapper;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Mapping
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<User, UserViewModel>();
    
        }
    }
}
