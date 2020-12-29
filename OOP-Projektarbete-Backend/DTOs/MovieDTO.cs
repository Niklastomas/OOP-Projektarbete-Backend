using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.DTOs
{
    public class MovieDTO
    {
        public Movie Movie { get; set; }
        public Trailer Trailer { get; set; }
    }
}