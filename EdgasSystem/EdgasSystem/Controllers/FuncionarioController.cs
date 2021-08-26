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
    public class FuncionarioController : Controller
    {
        // GET: HomeController1
        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        public IActionResult Alterar(int id)
        {
            ViewBag.MyRouteId = id;
            return View();
        }

        public IActionResult BuscarFuncionario(int cod_fun)
        {
           
            return Json(new Models.Funcionario().BuscarFuncionario(cod_fun));
        }

        public JsonResult GravarFuncionario([FromBody] Dictionary<string, object> dados)
        {
            bool ok = true;
            Controls.FuncionarioControl ctl = new Controls.FuncionarioControl();
            ok = ctl.GravarFuncionario(dados);

            return Json(new { ok });
        }

        public JsonResult ObterFuncionarios()
        {
            return Json(new Models.Funcionario().ObterFuncionarios(""));
        }

        public JsonResult ObterFuncionariosSemUsuario()
        {
            return Json(new Models.Funcionario().ObterFuncionariosSemUsuario());
        }

        public JsonResult AlterarFuncionario([FromBody] Dictionary<string, object> dados)
        {
            

            return Json(new { ok = new Controls.FuncionarioControl().AlterarFuncionario(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult Excluir(int cod_fun)
        {
            bool ok;

            Controls.FuncionarioControl ctl = new Controls.FuncionarioControl();

            ok = ctl.Excluir(cod_fun);

            return Json(new { ok, controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });

        }
    }
}
