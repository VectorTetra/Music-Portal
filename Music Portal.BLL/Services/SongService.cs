using Music_Portal.BLL.DTO;
using Music_Portal.BLL.Interfaces;
using Music_Portal.DAL.Entities;
using Music_Portal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music_Portal.BLL.Infrastructure;
using AutoMapper;

namespace Music_Portal.BLL.Services
{
    public class SongService : ISongService
    {
        IUnitOfWork Database;
        public SongService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task AddSong(SongDTO songDto)
        {
            var Song = new Song
            {
                Id = songDto.Id,
                Name = songDto.Name,
                Singers = songDto.Singers,
                Path = songDto.Path,
                GenreId = songDto.GenreId
            };
            await Database.Songs.Create(Song);
            await Database.Save();
        }
        public async Task UpdateSong(SongDTO songDto)
        {
            var Song = await Database.Songs.Get(songDto.Id);
            if (Song == null)
            {
                throw new ValidationException("Такої пісні не знайдено!", "");
            }
            else
            {
                Song.Name = songDto.Name;
                Song.GenreId = songDto.GenreId;
                Song.Singers = songDto.Singers;
                Song.Path = songDto.Path;
                Database.Songs.Update(Song);
                await Database.Save();
            }
        }
        public async Task DeleteSong(int id)
        {
            var Song = await Database.Songs.Get(id);
            if (Song == null)
            {
                throw new ValidationException("Такої пісні не знайдено!", "");
            }
            else
            {
                await Database.Songs.Delete(id);
                await Database.Save();
            }
        }
        public async Task<SongDTO> GetSong(int id)
        {
            var Song = await Database.Songs.Get(id);
            if (Song == null)
            {
                throw new ValidationException("Такої пісні не знайдено!", "");
            }
            else
            {
                return new SongDTO
                {
                    Name = Song.Name,
                    Path = Song.Path,
                    Id = Song.Id,
                    GenreId = Song.GenreId,
                    Singers = Song.Singers
                };
            }
        }
        public async Task<IEnumerable<SongDTO>> GetAllSongs() 
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.GetAll());
        }
        public async Task<IEnumerable<SongDTO>> GetSongsByName(string searchName) 
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.GetByName(searchName));
        }
        public async Task<IEnumerable<SongDTO>> GetSongsByGenre(string searchGenre) 
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.GetByGenre(searchGenre));
        }
        public async Task<IEnumerable<SongDTO>> GetSongsBySinger(string searchSinger)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.GetBySinger(searchSinger));
        }
    }
}
