using Microsoft.EntityFrameworkCore;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

      

        public async Task<IEnumerable<User>> ListAsync(string currentUser)
        {
            return await _context.Users.Where(x => x.Email != currentUser).ToListAsync();

        }

        public async Task<IEnumerable<UsersMovies>> ListMoviesAsync(string userId)
        {
            return await _context.UsersMovies.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task AddMovieAsync(UsersMovies movie)
        {
            await _context.UsersMovies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }
    }
}
