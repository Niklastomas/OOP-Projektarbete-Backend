using OOP_Projektarbete_Backend.Helpers;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Repositories.Interfaces;
using OOP_Projektarbete_Backend.Services.Communication;
using OOP_Projektarbete_Backend.Services.Interfaces;
using OOP_Projektarbete_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

         
         public UserService(IUserRepository userRepository)
         {
            _userRepository = userRepository;

         }

    

        public async Task<UserResponse> GetAllAsync(string currentUser)
        {
            try
            {
                var users = await _userRepository.GetAllAsync(currentUser);
                if (users != null)
                {
                    return new UserResponse(users);
                }
                return new UserResponse("Users not found");
          
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error occurred when geting users: {ex.Message} ");
            }
        }

        public async Task<UserResponse> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                if (users != null)
                {
                    return new UserResponse(users);
                }
                return new UserResponse("Users not found");
         
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error occurred when geting users: {ex.Message} ");
            }
        }

       
    }
}
