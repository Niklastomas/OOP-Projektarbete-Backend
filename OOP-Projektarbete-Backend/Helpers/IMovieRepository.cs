using OOP_Projektarbete_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Helpers
{
    public interface IMovieRepository
    {
        Task<MovieInfo> GetTrendingMovies();

        Task<MovieInfo> GetPopularMovies(string page);

        Task<MovieInfo> GetTopRatedMovies(string page);

        Task<MovieInfo> GetUpcomingMovies(string page);

        Task<MovieInfo> GetQueriedMovies(string query);

        Task<Movie> GetMovieDetails(string id);

        Task<Trailer> GetMovieTrailer(string id);
    }
}