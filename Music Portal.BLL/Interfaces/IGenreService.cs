using Music_Portal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.BLL.Interfaces
{
    public interface IGenreService
    {
        Task AddGenre(GenreDTO GenreDto);
        Task UpdateGenre(GenreDTO userDto);
        Task DeleteGenre(int id);
        Task<GenreDTO> GetGenre(int id);
        Task<IEnumerable<GenreDTO>> GetAllGenres();
        Task<IEnumerable<GenreDTO>> GetGenresByName(string searchName);
    }
}
