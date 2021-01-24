using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories.Interfaces
{
    public interface IUsersMoviesRepository
    {
        Task<IEnumerable<UsersMovies>> GetAllAsync(string userId);
        Task AddAsync(UsersMovies usersMovies);
        void Delete(UsersMovies usersMovies);
        Task<UsersMovies> GetByIdAsync(string movieId, string userId);
    }
}
