using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Models
{
    public class Movie
    {
        public int Budget { get; set; }
        public string Homepage { get; set; }
        public int Id { get; set; }
        public string Overview { get; set; }
        public float Popularity { get; set; }
        public string Poster_path { get; set; }

        public string Release_date { get; set; }

        public string Title { get; set; }

        public float Vote_average { get; set; }
        public int Vote_count { get; set; }
    }
}