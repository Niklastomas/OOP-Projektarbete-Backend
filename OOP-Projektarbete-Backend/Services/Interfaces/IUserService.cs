using OOP_Projektarbete_Backend.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetAllAsync();
        Task<UserResponse> GetAllAsync(string currentUser);
        //Task<MoviesResponse> ListMoviesAsync(string userId);
        //Task<MovieResponse> AddMovieAsync(string movieId, string userId);
    }
}
