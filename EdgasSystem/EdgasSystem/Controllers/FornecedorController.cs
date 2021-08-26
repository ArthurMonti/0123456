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
    public class FornecedorController : Controller
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

        public IActionResult BuscarFornecedor(int cod_fornecedor)
        {
            return Json(new Models.Fornecedor().BuscarFornecedor(cod_fornecedor));
        }

        public JsonResult GravarFornecedor([FromBody] Dictionary<string, object> dados)
        {
            bool ok = true;
            Controls.FornecedorControl ctl = new Controls.FornecedorControl();
            ok = ctl.GravarFornecedor(dados);

            return Json(new { ok, controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult ObterFornecedores()
        {
            return Json(new Models.Fornecedor().ObterFornecedores(""));
        }

        public JsonResult AlterarFornecedor([FromBody] Dictionary<string, object> dados)
        {
            

            return Json(new { ok = new Controls.FornecedorControl().AlterarFornecedor(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult Excluir(int cod_fornecedor)
        {
            bool ok;

            Controls.FornecedorControl ctl = new Controls.FornecedorControl();

            ok = ctl.Excluir(cod_fornecedor);

            return Json(new { ok });

        }
    }
}
