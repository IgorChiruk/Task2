using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Task2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.Owin;


namespace Task2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.Login,
                    Email = model.Email,
                    RegisterDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                    isBlocked = false
                };


                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("UserPage", "Page");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        

        public ActionResult Login(string returnUrl)
        {
            AuthenticationManager.SignOut();
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]     
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(model.Login, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {                 
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();

                    
                    user.LastLoginDate = DateTime.Now;
                    ApplicationManager.Update(user, HttpContext.GetOwinContext());
                    
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);


                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("UserPage", "Page");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Account");
        }
    }
}
