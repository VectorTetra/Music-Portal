using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.BLL.DTO
{
    public class LoginUserDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Логін")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
