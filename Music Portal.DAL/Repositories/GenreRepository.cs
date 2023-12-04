using Microsoft.EntityFrameworkCore;
using Music_Portal.DAL.EF;
using Music_Portal.DAL.Entities;
using Music_Portal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.DAL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MusicPortalContext _context;
        public GenreRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task<Genre?> Get(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(genre => genre.Id == id);
        }
        public async Task<IEnumerable<Genre>> GetByName(string name)
        {
            return await _context.Genres.Where(o => o.Name.Contains(name)).ToListAsync();
        }
        public async Task Create(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
        }
        public void Update(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Genre? genre = await _context.Genres.FindAsync(id);
            if (genre != null)
                _context.Genres.Remove(genre);
        }
    }
}
