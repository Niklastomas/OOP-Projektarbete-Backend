using OOP_Projektarbete_Backend.Repositories.Interfaces;
using OOP_Projektarbete_Backend.Services.Communication;
using OOP_Projektarbete_Backend.Services.Interfaces;
using System;
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

        public async Task<UsersResponse> GetAllAsync(string currentUser)
        {
            try
            {
                var users = await _userRepository.GetAllAsync(currentUser);
                if (users != null)
                {
                    return new UsersResponse(users);
                }
                return new UsersResponse("Users not found");        
            }
            catch (Exception ex)
            {
                return new UsersResponse($"Error occurred when geting users: {ex.Message} ");
            }
        }

        public async Task<UsersResponse> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                if (users != null)
                {
                    return new UsersResponse(users);
                }
                return new UsersResponse("Users not found");     
            }
            catch (Exception ex)
            {
                return new UsersResponse($"Error occurred when geting users: {ex.Message} ");
            }
        }

       
    }
}
