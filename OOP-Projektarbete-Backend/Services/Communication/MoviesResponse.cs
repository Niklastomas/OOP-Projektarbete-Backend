using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Services.Communication
{
    public class MoviesResponse : BaseResponse<IEnumerable<Movie>>
    {
        //Success response
        public MoviesResponse(IEnumerable<Movie> movies) : base(movies)
        {

        }

        //Error response
        public MoviesResponse(string message) : base(message)
        {

        }
    }
}
