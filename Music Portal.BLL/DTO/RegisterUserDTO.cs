using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.BLL.DTO
{
    public class RegisterUserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Логін")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Введіть пароль повторно")]
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        public int RoleId { get; set; }
    }
}
