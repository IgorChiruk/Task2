using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Task2.Models
{
    public class User : IdentityUser
    {
        public User()
        { }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public bool isBlocked { get; set; }

    }
}