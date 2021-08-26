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
    public class CaixaController : Controller
    {
        // GET: HomeController1
        public IActionResult AbrirCaixa()
        {
            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        public IActionResult FecharCaixa(int id)
        {
            ViewBag.MyRouteId = id;
            return View();
        }

        public JsonResult Abrir([FromBody] Dictionary<string, object> dados)
        {
            return Json(new { ok= new CaixaControl().AbrirCaixa(dados), errorMessage= GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult Fechar([FromBody] Dictionary<string, object> dados)
        {
            return Json(new { ok = new CaixaControl().FecharCaixa(dados)});
        }


        public JsonResult BuscarCaixa(int num_caixa,string abertura)
        {
           
            return Json(new Models.Caixa().BuscarCaixa(num_caixa,abertura));
        }

       
        public JsonResult ObterCaixasAbertosDESC()
        {
            return Json(new Models.Caixa().ObterCaixasAbertosDESC());
        }

        public JsonResult ObterCaixasFechadosDESC()
        {
            return Json(new Models.Caixa().ObterCaixasFechadosDESC());
        }

        public JsonResult ObterCaixasAbertosporData(string abertura)
        {
            return Json(new Models.Caixa().ObterCaixasAbertosporData(abertura));
        }
    }
}
