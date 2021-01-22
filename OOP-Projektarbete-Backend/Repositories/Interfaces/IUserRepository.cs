using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync(string currentUser);
        Task<IEnumerable<UsersMovies>> ListMoviesAsync(string userId);
        Task AddMovieAsync(UsersMovies usersMoives);
    }
}
