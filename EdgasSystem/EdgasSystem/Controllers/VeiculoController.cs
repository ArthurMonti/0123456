using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Controllers
{
    public class VeiculoController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        public IActionResult Alterar(string id)
        {
            ViewBag.MyRouteId = id;
            return View();
        }

        public JsonResult LoadInicialView()
        {
            return Json(new
            {
                funcionario = new Models.Funcionario().ObterFuncionarios(""),
            });
        }

        public IActionResult BuscarVeiculo(string placa)
        {

            return Json(new Models.Veiculo().BuscarVeiculo(placa));
        }

        public JsonResult GravarVeiculo([FromBody] Dictionary<string, object> dados)
        {

            return Json(new { ok = new Controls.VeiculoControl().GravarVeiculo(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult ObterVeiculos()
        { 

            return Json(new Models.Veiculo().ObterVeiculos(""));
        }

        public JsonResult AlterarVeiculo([FromBody] Dictionary<string, object> dados)
        {

            return Json(new { ok = new Controls.VeiculoControl().AlterarVeiculo(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult Excluir(string placa)
        {

            return Json(new { ok = new Controls.VeiculoControl().ExcluirVeiculo(placa) });

        }
    }
}
