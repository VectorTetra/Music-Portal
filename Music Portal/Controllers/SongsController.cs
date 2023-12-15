﻿using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Portal.BLL.DTO;
using Music_Portal.BLL.Infrastructure;
using Music_Portal.BLL.Interfaces;
using Music_Portal.BLL.Services;
using Music_Portal.Models;
using System.Diagnostics;
using System.Linq;
using System.Diagnostics;
using System.IO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Music_Portal.Controllers
{
    [Route("api/Songs")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongService songService;
        IWebHostEnvironment _appEnvironment;
        public SongsController(ISongService songServ, IWebHostEnvironment hostEnvironment)
        {
            songService = songServ;
            _appEnvironment = hostEnvironment;
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
                    { collec = await songService.GetSongsBySinger(searchParameter); }
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
        [HttpGet("{id}")]
        public async Task<ActionResult<SongDTO>> GetSong(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var songDTO = await songService.GetSong(id);
                return new ObjectResult(songDTO);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }



        // POST api/<SongsController>
        [HttpPost]
        public async Task<ActionResult<string>> UploadSong([FromForm]IFormFile FormFile)
        {
            try
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(FormFile.FileName);

                // Путь к папке Files
                string path = "/Files/" + fileName; // имя файла

                // Сохраняем файл в папку Files в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot
                // применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await FormFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                return new ObjectResult(path);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<SongDTO>> PostSong(SongDTO songDTO)
        {
            try
            {
                await songService.AddSong(songDTO);
                return new ObjectResult(songDTO);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //// PUT api/<SongsController>/5
        [HttpPut]
        public async Task<ActionResult<string>> EditSong([FromForm] IFormFile FormFile)
        {
            try
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(FormFile.FileName);

                // Путь к папке Files
                string path = "/Files/" + fileName; // имя файла

                // Сохраняем файл в папку Files в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot
                // применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await FormFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                return new ObjectResult(path);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SongDTO>> PutSong(SongDTO songDTO)
        {
            try
            {
                await songService.UpdateSong(songDTO);
                return new ObjectResult(songDTO);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SongDTO>> DeleteSong(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var songDTO = await songService.GetSong(id);
                await songService.DeleteSong(id);
                return new ObjectResult(songDTO);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
