using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapaDatos;
using CapaModelos;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CapaWeb.Controllers
{
    public class PeriodoController : Controller
    {
        // GET: Periodo
        public ActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaPeriodo()
        {
            List<Periodo> oListaPeriodo = await PeriodoAD.Instancia.ObtenerListaPeriodoAsync();
            if (oListaPeriodo == null)
            {
                oListaPeriodo = new List<Periodo>();
            }
            return Json(new { data = oListaPeriodo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> CrearPeriodo(Periodo periodo)
        {
            bool resultado = await PeriodoAD.Instancia.CrearPeriodoAsync(periodo);
            return Json(new { respuesta = resultado });
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarPeriodo(Periodo periodo)
        {
            bool resultado;
            if (periodo.IdPeriodo == 0)
            {
                resultado = await PeriodoAD.Instancia.CrearPeriodoAsync(periodo);
            }
            else
            {
                resultado = await PeriodoAD.Instancia.ModificarPeriodoAsync(periodo);
            }
            return Json(new { respuesta = resultado });
        }
    }
}
