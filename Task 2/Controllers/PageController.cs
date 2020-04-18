using System.Web;
using System.Web.Mvc;
using Task2.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;


namespace Task_2.Controllers
{
    public class PageController : Controller
    {
        private ApplicationManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        public async Task<ActionResult> UserPage()
        {
            User user = await UserManager.FindByNameAsync(this.User.Identity.Name.ToString());
            if (user==null)
            {
                ModelState.AddModelError("", "You are deleted");
                return RedirectToAction("Login", "Account");
            }

            if (user.isBlocked)
            {
                ModelState.AddModelError("", "You are blocked");
                return RedirectToAction("Login","Account");
            }

            return View(UserManager.Users);
        }

        public async Task< ActionResult>  Block(string[] usersid)
        {
            var users = UserManager.Users.ToList();

            foreach (string s in usersid)
            {
                foreach (User u in users)
                {
                    if (s == u.Id)
                    {
                        u.isBlocked = true;
                        await UserManager.UpdateAsync(u);
                    }
                }

            }          
            return RedirectToAction("UserPage");
        }

        public async Task<ActionResult> Delete(string[] usersid)
        {
            var users = UserManager.Users.ToList();

            foreach (string s in usersid)
            {
                foreach (User u in users)
                {
                    if (s == u.Id)
                    {
                        await UserManager.DeleteAsync(u);
                    }
                }

            }
            return RedirectToAction("UserPage");
        }

        public async Task<ActionResult> Unblock(string[] usersid)
        {
            var users = UserManager.Users.ToList();

            foreach (string s in usersid)
            {
                foreach (User u in users)
                {
                    if (s == u.Id)
                    {
                        u.isBlocked = false;
                        await UserManager.UpdateAsync(u);
                    }
                }

            }
            return RedirectToAction("UserPage");
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login","Account");
        }
    }
}