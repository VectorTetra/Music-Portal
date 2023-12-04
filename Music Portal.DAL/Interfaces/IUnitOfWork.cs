using Music_Portal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ISongRepository Songs { get; }
        IUsersRepository Users { get; }
        IGenreRepository Genres { get; }
        Task Save();
    }
}
