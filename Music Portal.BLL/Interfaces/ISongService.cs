using Music_Portal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.BLL.Interfaces
{
    public interface ISongService
    {
        Task AddSong(SongDTO songDto);
        Task UpdateSong(SongDTO songDto);
        Task DeleteSong(int id);
        Task<SongDTO> GetSong(int id);
        Task<IEnumerable<SongDTO>> GetAllSongs();
        Task<IEnumerable<SongDTO>> GetSongsByName(string searchName);
        Task<IEnumerable<SongDTO>> GetSongsByGenre(string searchGenre);
        Task<IEnumerable<SongDTO>> GetSongsBySinger(string searchSinger);
        
    }
}
