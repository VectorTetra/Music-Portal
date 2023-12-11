using Microsoft.AspNetCore.Mvc;
using Music_Portal.BLL.DTO;
using Music_Portal.BLL.Infrastructure;
using Music_Portal.BLL.Interfaces;
using Music_Portal.BLL.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Music_Portal.Controllers
{
    [Route("api/Genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreServ)
        {
            genreService = genreServ;
        }
        // GET: api/<GenresController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres(string collectionType, string searchedName)
        {
            IEnumerable<GenreDTO?> collec = null;
            switch (collectionType)
            {
                case "All":
                    { collec = await genreService.GetAllGenres(); }
                    break;
                case "Searching":
                    {
                        collec = await genreService.GetGenresByName(searchedName);
                    }
                    break;
                default:
                    { // Якщо немає правильного варіанту - повернути пусту колекцію
                        collec = new List<GenreDTO>();
                    }
                    break;
            }

            return collec?.ToList();
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var genreDTO = await genreService.GetGenre(id);
                return new ObjectResult(genreDTO);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<ActionResult<GenreDTO>> PostGenre(GenreDTO genreDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await genreService.AddGenre(genreDTO);
                return new ObjectResult(genreDTO);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/<GenresController>/5
        [HttpPut]
        public async Task<ActionResult<GenreDTO>> Put(GenreDTO genreDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await genreService.GetGenre(genreDTO.Id);
                await genreService.UpdateGenre(genreDTO);
                return new ObjectResult(genreDTO);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreDTO>> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var genreDTO = await genreService.GetGenre(id);
                await genreService.DeleteGenre(id);
                return new ObjectResult(genreDTO);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
