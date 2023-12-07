using Microsoft.EntityFrameworkCore;
using Music_Portal.DAL.EF;
using Music_Portal.DAL.Entities;
using Music_Portal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.DAL.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicPortalContext _context;
        public SongRepository(MusicPortalContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Song>> GetAll()
        {
            return await _context.Songs.ToListAsync();
        }
        public async Task<Song?> Get(int id)
        {
            return await _context.Songs.FirstOrDefaultAsync(song => song.Id == id);
        }
        public async Task<IEnumerable<Song>> GetByName(string name)
        {
            return await _context.Songs.Where(o => o.Name.Contains(name)).ToListAsync();
        }
        public async Task<IEnumerable<Song>> GetBySinger(string searchSinger)
        {
            return await _context.Songs.Where(o => o.Singers.Any(x=>x.Contains(searchSinger))).ToListAsync();
        }
        public async Task<IEnumerable<Song>> GetByGenre(string searchGenre)
        {
            return await _context.Songs.Where(o => o.Genre.Name.Contains(searchGenre)).ToListAsync();
        }
        public async Task Create(Song song)
        {
            await _context.Songs.AddAsync(song);
        }
        public void Update(Song song)
        {
            _context.Entry(song).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Song? song = await _context.Songs.FindAsync(id);
            if (song != null)
                _context.Songs.Remove(song);
        }
    }
}
