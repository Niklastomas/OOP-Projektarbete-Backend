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
        private readonly IMovieRepository _movieRepository;
         
         public UserService(IUserRepository userRepository, IMovieRepository movieRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }

    

        public async Task<UserResponse> ListAsync(string currentUser)
        {
            try
            {
                var userList = new List<UserViewModel>();
                var users = await _userRepository.ListAsync(currentUser);
                foreach (var u in users)
                {
                    var user = new UserViewModel()
                    {
                        Id = u.Id,
                        Username = u.UserName,
                        Email = u.Email
                    };
                    userList.Add(user);
                }
                if (userList.Count == 0)
                {
                    return new UserResponse("No users were found");
                }
                return new UserResponse(userList);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error occurred when geting users: {ex.Message} ");
            }
        }

        public async Task<MoviesResponse> ListMoviesAsync(string userId)
        {
            try
            {
                var movies = new List<Movie>();
                var usersMovies = await _userRepository.ListMoviesAsync(userId);
                foreach (var item in usersMovies)
                {
                    var movie = await _movieRepository.GetMovieDetails(item.MovieId);
                    if (movie != null)
                    {
                        movies.Add(movie);
                    }
                }
                if (movies.Count == 0)
                {
                    return new MoviesResponse("No movies found");
                }
                return new MoviesResponse(movies);
            }
            catch (Exception ex)
            {
                return new MoviesResponse($"Error occured when geting movies: {ex.Message}");
            }
        }

        public async Task<MovieResponse> AddMovieAsync(string movieId, string userId)
        {
            try
            {
                var movie = await _movieRepository.GetMovieDetails(movieId);
                var usersMovies = new UsersMovies()
                {
                    Id = Guid.NewGuid().ToString(),
                    MovieId = movieId,
                    UserId = userId
                };
                await _userRepository.AddMovieAsync(usersMovies);
                return new MovieResponse(movie);
            }
            catch (Exception ex)
            {

                return new MovieResponse($"Error occured when adding movie: {ex.Message}");
            }
            
        }
    }
}
