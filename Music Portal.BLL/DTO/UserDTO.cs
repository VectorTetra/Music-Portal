using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Логін")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        public int RoleId { get; set; }
    }
}
