using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdgasSystem.Controls;
using System.Diagnostics;

namespace EdgasSystem.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: HomeController1

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Alterar()
        {
            return View();
        }


        public IActionResult Consulta()
        {
            return View();
        }

        public IActionResult BuscarUsuario(int cod_fun)
        {
           
            return Json(new Models.Funcionario().BuscarFuncionario(cod_fun));
        }

        public JsonResult GravarUsuario([FromBody] Dictionary<string, object> dados)
        {

            return Json(new { ok = new Controls.UsuarioControl().GravarUsuario(dados),errorMessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult ObterUsuarios()
        {
            return Json(new Models.Usuario().ObterUsuarios(""));
        }

        public JsonResult AlterarUsuario([FromBody] Dictionary<string, object> dados)
        {
            

            return Json(new { ok = new Controls.UsuarioControl().AlterarUsuario(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult Excluir(string login)
        {
            bool ok;

            Controls.UsuarioControl ctl = new Controls.UsuarioControl();

            ok = ctl.ExcluirUsuario(login);

            return Json(new { ok });

        }
    }
}
