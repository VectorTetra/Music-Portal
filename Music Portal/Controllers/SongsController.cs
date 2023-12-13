using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Portal.BLL.DTO;
using Music_Portal.BLL.Infrastructure;
using Music_Portal.BLL.Interfaces;
using Music_Portal.Models;
using System.Diagnostics;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Music_Portal.Controllers
{
    [Route("api/Songs")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongService songService;

        public SongsController(ISongService songServ)
        {
            songService = songServ;
        }

        // GET: api/<SongsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSongs(string collectionType, string searchParameter)
        {
            IEnumerable<SongDTO?> collec = null;
            switch (collectionType)
            {
                case "All":
                    { collec = await songService.GetAllSongs(); }
                    break;
                case "SearchByName":
                    { collec = await songService.GetSongsByName(searchParameter); }
                    break;
                case "SearchByGenre":
                    { collec = await songService.GetSongsByGenre(searchParameter); }
                    break;
                case "SearchBySinger":
                    { collec = await songService.GetSongsBySinger(searchParameter);}
                    break;
                default:
                    { // Якщо немає правильного варіанту - повернути пусту колекцію
                        collec = new List<SongDTO>();
                    }
                    break;
            }

            return collec?.ToList();
        }

        //// GET api/<SongsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<SongsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<SongsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SongsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
