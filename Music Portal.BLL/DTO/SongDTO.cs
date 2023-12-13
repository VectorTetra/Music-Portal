using Music_Portal.BLL.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.BLL.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Назва пісні")]
        public string Name { get; set; }
        [MyAttributeNonEmptyList(ErrorMessage = "У пісні має бути хоча б один виконавець")]
        [Display(Name = "Виконавці")]
        public string Singers { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Аудіофайл")]
        public string Path { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Жанр пісні")]
        public int GenreId { get; set; }
    }
}
