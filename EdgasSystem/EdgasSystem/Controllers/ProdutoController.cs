using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Controllers
{
    public class ProdutoController : Controller
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

        public JsonResult LoadInicialView()
        {
            return Json(new
            {
                tipo_produto = new Models.Tipo_Produto().ObterTipo_Produtos(""),
            });
        }

        public IActionResult BuscarProduto(int cod_produto)
        {

            return Json(new Models.Produto().BuscarProduto(cod_produto));
        }

        public JsonResult GravarProduto([FromBody] Dictionary<string, object> dados)
        {

            return Json(new { ok = new Controls.ProdutoControl().GravarProduto(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult ObterProdutos()
        { 

            return Json(new Models.Produto().ObterProdutos(""));
        }

        public JsonResult AlterarProduto([FromBody] Dictionary<string, object> dados)
        {

            return Json(new { ok = new Controls.ProdutoControl().AlterarProduto(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult Excluir(int cod_produto)
        {

            return Json(new { ok = new Controls.ProdutoControl().ExcluirProduto(cod_produto) });

        }
    }
}
