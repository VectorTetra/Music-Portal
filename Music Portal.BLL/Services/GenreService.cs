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
    public class GenreService : IGenreService
    {
        IUnitOfWork Database;
        public GenreService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task AddGenre(GenreDTO genreDto)
        {
            var Genre = new Genre
            {
                Id = genreDto.Id,
                Name = genreDto.Name,
                Description = genreDto.Description
            };
            await Database.Genres.Create(Genre);
            await Database.Save();
        }
        public async Task UpdateGenre(GenreDTO genreDto) 
        {
            var Genre = await Database.Genres.Get(genreDto.Id);
            if (Genre == null)
            {
                throw new ValidationException("Такого жанру не знайдено!", "");
            }
            else
            {
                Genre.Name = genreDto.Name;
                Genre.Description = genreDto.Description;
                Database.Genres.Update(Genre);
                await Database.Save();
            }
        }
        public async Task DeleteGenre(int id) 
        {
            var Genre = await Database.Genres.Get(id);
            if (Genre == null)
            {
                throw new ValidationException("Такого жанру не знайдено!", "");
            }
            else
            {
                await Database.Genres.Delete(id);
                await Database.Save();
            }
        }
        public async Task<GenreDTO> GetGenre(int id) 
        {
            var Genre = await Database.Genres.Get(id);
            if (Genre == null)
            {
                throw new ValidationException("Такого жанру не знайдено!", "");
            }
            else
            {
                return new GenreDTO
                {
                    Name = Genre.Name,
                    Id = Genre.Id,
                    Description = Genre.Description
                };
            }
        }
        public async Task<IEnumerable<GenreDTO>> GetAllGenres() 
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(await Database.Genres.GetAll());
        }
        public async Task<IEnumerable<GenreDTO>> GetGenresByName(string searchName) 
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(await Database.Genres.GetByName(searchName));
        }
    }
}
