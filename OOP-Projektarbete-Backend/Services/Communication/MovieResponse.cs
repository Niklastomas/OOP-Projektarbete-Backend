using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Communication
{
    public class MovieResponse : BaseResponse<Movie>
    {
        //Success response
        public MovieResponse(Movie movie) : base(movie)
        {

        }

        //Error response
        public MovieResponse(string message) : base(message)
        {

        }
    }
}
