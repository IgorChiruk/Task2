using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Task2.Models
{
    public class ApplicationManager : UserManager<User>
    {
        public ApplicationManager(IUserStore<User> store) : base(store)
        {
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
        }
        public static ApplicationManager Create(IdentityFactoryOptions<ApplicationManager> options,IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationManager manager = new ApplicationManager(new UserStore<User>(db));
            
            return manager;
        }

        public static void Update(User user,IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationManager manager = new ApplicationManager(new UserStore<User>(db));
            manager.Update(user);
            db.SaveChanges();
        }


    }
}