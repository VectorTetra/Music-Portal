using Microsoft.AspNetCore.Mvc;
using Music_Portal.BLL.DTO;
using Music_Portal.BLL.Infrastructure;
using Music_Portal.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Music_Portal.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;

        public LoginController(IUserService userServ)
        {
            userService = userServ;
        }
        
        // POST api/<LoginController>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> LoginUser(LoginUserDTO userDTO)
        {
            try
            {
                var authorizedUser = await userService.TryToLogin(userDTO);
                return new ObjectResult(authorizedUser);
            }
            catch (ValidationException ex)
            {
                return new ObjectResult(ex.Message);
            }

        }
    }
}
