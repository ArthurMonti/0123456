using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Controllers
{
    public class Tipo_ProdutoController : Controller
    {
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

        public IActionResult BuscarTipo_Produto(int cod_tipo_produto)
        {

            return Json(new Models.Tipo_Produto().BuscarTipo_Produto(cod_tipo_produto));
        }

        public JsonResult GravarTipo_Produto([FromBody] Dictionary<string, object> dados)
        {

            return Json(new { ok = new Controls.Tipo_ProdutoControl().GravarTipo_Produto(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult ObterTipo_Produtos()
        { 

            return Json(new Models.Tipo_Produto().ObterTipo_Produtos(""));
        }

        public JsonResult AlterarTipo_Produto([FromBody] Dictionary<string, object> dados)
        {

            return Json(new { ok = new Controls.Tipo_ProdutoControl().AlterarTipo_Produto(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult Excluir(int cod_tipo_produto)
        {

            return Json(new { ok = new Controls.Tipo_ProdutoControl().ExcluirTipo_Produto(cod_tipo_produto) });

        }
    }
}
