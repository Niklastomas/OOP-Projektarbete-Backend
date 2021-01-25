using OOP_Projektarbete_Backend.Helpers;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Repositories.Interfaces;
using OOP_Projektarbete_Backend.Services.Communication;
using OOP_Projektarbete_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services
{
    public class UsersMoviesService : IUsersMoviesService
    {
        private readonly IUsersMoviesRepository _usersMoivesReposiotry;
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UsersMoviesService(IUsersMoviesRepository usersMoviesRepository,
            IMovieRepository movieRepository,
            IUnitOfWork unitOfWork)
        {
            _usersMoivesReposiotry = usersMoviesRepository;
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;

        }


        public async Task<MoviesResponse> GetAllAsync(string userId)
        {
            try
            {
                var movies = new List<Movie>();
                var usersMovies = await _usersMoivesReposiotry.GetAllAsync(userId);
                foreach (var item in usersMovies)
                {
                    var movie = await _movieRepository.GetMovieDetails(item.MovieId);
                    if (movie != null)
                    {
                        movies.Add(movie);
                    }
                }
                return new MoviesResponse(movies);
            }
            catch (Exception ex)
            {
                return new MoviesResponse($"Error occured when geting movies: {ex.Message}");
            }
        }

        public async Task<MovieResponse> AddAsync(string movieId, string userId)
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
                await _usersMoivesReposiotry.AddAsync(usersMovies);
                await _unitOfWork.CompleteAsync();
                return new MovieResponse(movie);
            }
            catch (Exception ex)
            {
                return new MovieResponse($"Error occured when adding movie: {ex.Message}");
            }

        }

        public async Task<MovieResponse> DeleteAsync(string movieId, string userId)
        {
            try
            {
                var movie = await _movieRepository.GetMovieDetails(movieId);
                var usersMovies = await _usersMoivesReposiotry.GetByIdAsync(movieId, userId);
                _usersMoivesReposiotry.Delete(usersMovies);
                await _unitOfWork.CompleteAsync();
                return new MovieResponse(movie);
            }
            catch (Exception ex)
            {
                return new MovieResponse($"Error occured when trying to delete movie: {ex.Message}");
            }
        }
    }
}
