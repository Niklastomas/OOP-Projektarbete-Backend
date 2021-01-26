using Microsoft.Extensions.Caching.Memory;
using OOP_Projektarbete_Backend.Caching;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Repositories.Interfaces;
using OOP_Projektarbete_Backend.Services.Communication;
using OOP_Projektarbete_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _cache;
 
         public UserService(IUserRepository userRepository,
             IMemoryCache cache)
         {
            _userRepository = userRepository;
            _cache = cache;
         }

        public async Task<UsersResponse> GetAllAsync(string currentUser)
        {
            try
            {
                //var users = await _userRepository.GetAllAsync(currentUser);
                var users = await _cache.GetOrCreateAsync(CacheKeys.UserList, async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                    return await _userRepository.GetAllAsync(currentUser);
                });
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
                //var users = await _userRepository.GetAllAsync();
                var users = await _cache.GetOrCreateAsync(CacheKeys.UserList, async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                    return await _userRepository.GetAllAsync();
                });
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
