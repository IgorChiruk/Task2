using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Task2.Models
{
    public class RegisterModel
    {       
        [Required(ErrorMessage = "Пожалуйста, введите свой логин")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Вы ввели некорректный email")]
        public string Email { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        [Required]
        public DateTime LastLoginDate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }


    }
}