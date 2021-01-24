using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
