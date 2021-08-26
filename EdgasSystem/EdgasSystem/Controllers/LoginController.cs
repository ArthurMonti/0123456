using EdgasSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace EdgasSystem.Controllers
{
    public class LoginController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> LogarAsync(string logon, string senha)
        {
       

            if (new Controls.UsuarioControl().ValidaLogin(logon, senha))
            {
                var claims = new List<Claim>();
                claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, logon),
                        new Claim(ClaimTypes.Role, "Administrador")
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

                // **  Usado para a função permanecer logado
                /*/
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true
                    });
               /*/
                return Json(true);
            }
            else
                return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

    }
}
