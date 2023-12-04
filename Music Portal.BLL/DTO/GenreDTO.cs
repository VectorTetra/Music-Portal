using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.BLL.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Назва жанру")]
        public string Name { get; set; }
        [Display(Name = "Опис жанру")]
        public string? Description { get; set; }
    }
}
