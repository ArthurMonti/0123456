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
    public class ClienteController : Controller
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

        public IActionResult BuscarCliente(int cod_cliente)
        {
           
            return Json(new Models.Client().BuscarCliente(cod_cliente));
        }

        public JsonResult GravarCliente([FromBody] Dictionary<string, object> dados)
        {
            bool ok = true;
            Controls.ClienteControl ctl = new Controls.ClienteControl();
            ok = ctl.GravarCliente(dados);

            return Json(new { ok });
        }

        public JsonResult ObterClientes()
        {
            return Json(new Models.Client().ObterClientes(""));
        }

        public JsonResult AlterarCliente([FromBody] Dictionary<string, object> dados)
        {
            

            return Json(new { ok = new Controls.ClienteControl().AlterarCliente(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult Excluir(int cod_cliente)
        {
            bool ok;

            Controls.ClienteControl ctl = new Controls.ClienteControl();

            ok = ctl.Excluir(cod_cliente);

            return Json(new { ok });

        }
    }
}
