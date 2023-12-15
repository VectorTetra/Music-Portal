using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music_Portal.BLL.DTO;
using Music_Portal.BLL.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Music_Portal.Controllers
{
    [Route("api/Session")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        // GET: api/<SessionController>
        // В цьому методі відбувається перевірка, чи є сесійні змінні після авторизаціі (для забезпечення доступу)
        // Якщо вони є - повернути об'єкт сесійних змінних
        // Якщо немає - псевдооб'єкт зі значенням None
        [HttpGet]
        public ActionResult<object> Get()
        {
            try
            {
                var login = HttpContext.Session.GetString("login");
                var roleId = HttpContext.Session.GetString("roleId");
                var fullName = HttpContext.Session.GetString("fullName");
                if (login != null)
                {
                    return new ObjectResult(new { Login = login, RoleId = roleId, FullName = fullName });
                }
                return new ObjectResult(new { None = "none" });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public ActionResult<UserDTO> Post(UserDTO userDTO)
        {
            try
            {
                HttpContext.Session.SetString("login", userDTO.Login);
                HttpContext.Session.SetString("roleId", userDTO.RoleId.ToString());
                HttpContext.Session.SetString("fullName", $"{userDTO.FirstName} {userDTO.LastName}");
                return new ObjectResult(userDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        

        // DELETE api/<SessionController>/5
        [HttpDelete]
        public void Delete()
        {
            HttpContext.Session.Clear();
        }
    }
}
