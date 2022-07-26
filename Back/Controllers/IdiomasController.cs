using Back.Helpers;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Controllers
{
    public class IdiomasController : Controller
    {
        private ApplicationContext db;
        public IdiomasController(ApplicationContext db)
        {
            this.db = db;
        }
        public IActionResult ChangeLanguage(string selectedLanguage)
        {
            if (selectedLanguage != null)
            {
                // buscar el idioma en la tabla de la base de datos
                //Language language = db.Language.FirstOrDefault(l => l.DescriptionToken == selectedLanguage);

                //Grabacion usuario
                //int idUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                //User usuario = db.User.FirstOrDefault(a => a.Id == idUsuario);
                //usuario.Language = language;
                //db.Update(usuario);
                //db.SaveChanges();

                LanguageHelper.ChangeLanguage(selectedLanguage);

                this.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(selectedLanguage)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                });

            }

            RequestHeaders header = Request.GetTypedHeaders();
            Uri uriReferer = header.Referer;

            if (uriReferer != null)
            {
                //return RedirectToAction("Index", uriReferer.AbsolutePath);
                return Redirect(uriReferer.AbsoluteUri);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
