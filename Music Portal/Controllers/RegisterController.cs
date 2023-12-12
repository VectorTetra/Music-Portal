using Microsoft.AspNetCore.Mvc;
using Music_Portal.BLL.DTO;
using Music_Portal.BLL.Infrastructure;
using Music_Portal.BLL.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Music_Portal.Controllers
{
    [Route("api/Register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService userService;

        public RegisterController(IUserService userServ)
        {
            userService = userServ;
        }
        // POST api/<RegisterController>
        [HttpPost]
        public async Task<ActionResult<RegisterUserDTO>> RegisterUser(RegisterUserDTO userDTO)
        {
            try
            {
                await userService.TryToRegister(userDTO);
                return new ObjectResult(userDTO);
            }
            catch (ValidationException ex)
            {
                return new ObjectResult(ex.Message);
            }
           
        }
    }
}
