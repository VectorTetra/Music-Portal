﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Portal.BLL.DTO;
using Music_Portal.BLL.Infrastructure;
using Music_Portal.BLL.Interfaces;
using Music_Portal.Models;
using System.Diagnostics;
using System.Linq;

namespace Music_Portal.Controllers
{
    // Необхідно підключити 
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userServ)
        {
            userService = userServ;
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers(string collectionType, string searchedLogin)
        {
            IEnumerable<UserDTO?> collec = null;
            switch (collectionType)
            {
                case "Authorized":
                    { collec = await userService.GetAuthorizedUsers(); }
                    break;
                case "NonAuthorized":
                    { collec = await userService.GetNonAuthorizedUsers(); }
                    break;
                case "Blocked":
                    { collec = await userService.GetBlockedUsers(); }
                    break;
                case "Searching":
                    {
                        collec = await userService.GetUsersByLogin(searchedLogin);
                    }
                    break;
                default:
                    { // Якщо немає правильного варіанту - повернути пусту колекцію
                        collec = new List<UserDTO>();
                    }
                    break;
            }

            return collec?.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await userService.GetUser(id);
                return new ObjectResult(user);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpPut]
        public async Task<ActionResult<UserDTO>> PutUser(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await userService.GetUser(userDTO.Id);
                await userService.UpdateUser(userDTO);
                switch (userDTO.RoleId)
                {
                    case 1: { await userService.BlockUser(userDTO); } break;
                    case 3: { await userService.ActivateUser(userDTO); } break;
                    default:
                        break;
                }
                return new ObjectResult(userDTO);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await userService.GetUser(id);
                await userService.DeleteUser(id);
                return new ObjectResult(user);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
