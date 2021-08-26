using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Controllers
{
    public class Tipo_DespesaController : Controller
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

        public IActionResult BuscarTipo_Despesa(int cod_tipo_despesa)
        {

            return Json(new Models.Tipo_Despesa().BuscarTipo_Despesa(cod_tipo_despesa));
        }

        public JsonResult GravarTipo_Despesa([FromBody] Dictionary<string, object> dados)
        {
            
            bool ok = true;
            Controls.Tipo_DespesaControl ctl = new Controls.Tipo_DespesaControl();
            ok = ctl.GravarTipo_Despesa(dados);

            return Json(new { ok });
        }

        public JsonResult ObterTipo_Despesas()
        {


            return Json(new Models.Tipo_Despesa().ObterTipo_Despesas(""));
        }

        public JsonResult AlterarTipo_Despesa([FromBody] Dictionary<string, object> dados)
        {


            return Json(new { ok = new Controls.Tipo_DespesaControl().AlterarTipo_Despesa(dados), controlerrormessage = GlobalExceptionHandler.ControlErrorMessage });
        }

        public JsonResult Excluir(int cod_tipo_despesa)
        {
            bool ok;

            Controls.Tipo_DespesaControl ctl = new Controls.Tipo_DespesaControl();

            ok = ctl.ExcluirTipo_Despesa(cod_tipo_despesa);

            return Json(new { ok });

        }
    }
}
