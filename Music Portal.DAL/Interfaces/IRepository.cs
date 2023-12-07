using Music_Portal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.DAL.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> Get(int id);
        Task<IEnumerable<User>> GetByLogin(string login);
        Task<IEnumerable<User>> GetNotAuthorizedUsers();
        Task<IEnumerable<User>> GetAuthorizedUsers();
        Task<IEnumerable<User>> GetBlockedUsers();
        Task Create(User user);
        void Update(User user);
        Task Delete(int id);
    }

    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre?> Get(int id);
        Task<IEnumerable<Genre>> GetByName(string name);
        Task Create(Genre genre);
        void Update(Genre genre);
        Task Delete(int id);
    }

    public interface ISongRepository
    {
        Task<IEnumerable<Song>> GetAll();
        Task<Song?> Get(int id);
        Task<IEnumerable<Song>> GetByName(string name);
        Task<IEnumerable<Song>> GetByGenre(string searchGenre);
        Task<IEnumerable<Song>> GetBySinger(string searchSinger);
        Task Create(Song song);
        void Update(Song song);
        Task Delete(int id);
    }
}
