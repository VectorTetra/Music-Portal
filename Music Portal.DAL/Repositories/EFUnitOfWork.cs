using Music_Portal.DAL.EF;
using Music_Portal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly MusicPortalContext _context;
        private SongRepository songRepository;
        private UserRepository userRepository;
        private GenreRepository genreRepository;

        public EFUnitOfWork(MusicPortalContext context)
        {
            _context = context;
        }

        public ISongRepository Songs {
            get {
                if (songRepository == null)
                    songRepository = new SongRepository(_context);
                return songRepository;
            } 
        }

        public IUsersRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(_context);
                return userRepository;
            }
        }

        public IGenreRepository Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(_context);
                return genreRepository;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
