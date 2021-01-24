using Microsoft.EntityFrameworkCore;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories
{
    public class UsersMoviesRepository : BaseRepository, IUsersMoviesRepository
    {
        public UsersMoviesRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task AddAsync(UsersMovies usersMovies)
        {
            await _context.UsersMovies.AddAsync(usersMovies);
        }

        public void Delete(UsersMovies usersMovies)
        {
             _context.UsersMovies.Remove(usersMovies);
        }

        public async Task<IEnumerable<UsersMovies>> GetAllAsync(string userId)
        {
            return await _context.UsersMovies.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<UsersMovies> GetByIdAsync(string movieId, string userId)
        {
            return await _context.UsersMovies.FirstOrDefaultAsync(x => x.MovieId == movieId && x.UserId == userId);
        }
    }
}
