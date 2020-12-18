using OOP_Projektarbete_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Helpers
{
    public interface IMovieRepository
    {
        Task<MovieInfo> GetTrendingMovies();

        Task<MovieInfo> GetPopularMovies();

        Task<MovieInfo> GetTopRatedMovies();

        Task<MovieInfo> GetUpcomingMovies();

        Task<MovieInfo> GetQueriedMovies(string query);
    }
}