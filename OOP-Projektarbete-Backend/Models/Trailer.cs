using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Models
{
    public class Trailer
    {
        public int Id { get; set; }
        public TrailerResult[] Results { get; set; }

        public class TrailerResult
        {
            public string Id { get; set; }
            public string Key { get; set; }
            public string Name { get; set; }
            public string Site { get; set; }
        }
    }
}