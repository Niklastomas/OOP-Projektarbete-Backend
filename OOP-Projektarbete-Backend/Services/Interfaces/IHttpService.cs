using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Helpers
{
    public interface IHttpService
    {
        Task<T> Get<T>(string url);
    }
}