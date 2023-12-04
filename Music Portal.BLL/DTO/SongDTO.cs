﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Аудіофайл")]
        public string Path { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення.")]
        [Display(Name = "Жанр пісні")]
        public int GenreId { get; set; }
        public string UploaderUserLogin { get; set; }
    }
}
