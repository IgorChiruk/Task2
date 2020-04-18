using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Task2.Models
{
    public class LoginModel
    {     
        [Required(ErrorMessage = "Неверный логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}