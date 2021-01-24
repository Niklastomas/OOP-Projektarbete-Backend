using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Interfaces
{
    public interface IUsersMoviesService
    {
        Task<MoviesResponse> GetAllAsync(string userId);
        Task<MovieResponse> AddAsync(string movieId, string userId);
        Task<MovieResponse> DeleteAsync(string movieId, string userId);
    }
}

