using Back.Helpers;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Back.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext db;
        public AccountController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (user.Username != null && user.Password != null)
            {
                User validUser = null;
                // user.Password = HashHelper.GetMD5Hash(user.Password);
                // User validUser = db.User.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

                // temporal, comprobar credenciales en la base de datos
                if (user.Username == "innotu" && user.Password == "innotu")
                {
                    validUser = user;
                }

                if (validUser != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, "User"));

                    claims.Add(new Claim(ClaimTypes.Role, "UserRole"));

                    // añadir claim de admin
                    if (validUser.Username == "innotu")
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "AdminRole"));
                    }

                    var appIdentity = new ClaimsIdentity(claims);
                    HttpContext.User.AddIdentity(appIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,

                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        RedirectUri = "/login"
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };

                    var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    claimsIdentity.Label = user.Id.ToString();

                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);    //authProperties

                    HttpContext.Session.SetInt32("id", validUser.Id);
                    HttpContext.Session.SetString("userId", validUser.Id.ToString());
                    HttpContext.Session.SetString("username", validUser.Username);

                    LanguageHelper.ChangeLanguage("es");

                    return Redirect("/");
                }
            }

            return RedirectToAction(nameof(Login), new { message = "usuario no valido" });
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login), new { message = "logout" });

        }
    }
}
