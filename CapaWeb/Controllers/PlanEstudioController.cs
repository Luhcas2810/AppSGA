using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapaDatos;
using CapaModelos;

using System.Threading.Tasks;
using System.Web.Mvc;
using CapaWeb.Permisos;

namespace CapaWeb.Controllers
{
    [ValidarSesion]
    public class PlanEstudioController : Controller
    {
        // GET: PlanEstudio
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CrearPlanEstudio(PlanEstudio planEstudio)
        {
            Resultado resultado = new Resultado(); 
            resultado = await PlanEstudioAD.Instancia.AgregarPlanEstudioAsync(planEstudio);
            return Json(new { data = resultado });
        }

        //[HttpPost]
        //public async Task<JsonResult> CambiarEstadoPlanEstudio(int idPlan, bool habilitar)
        //{
        //    bool resultado = await PlanEstudioAD.Instancia.CambiarEstadoPlanEstudioAsync(idPlan, habilitar);
        //    return Json(new { respuesta = resultado }, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public async Task<JsonResult> ObtenerListaPlanEstudio()
        {
            List<PlanEstudio> oListaPlanEstudio = await PlanEstudioAD.Instancia.ObtenerListaPlanEstudioAsync();
            if (oListaPlanEstudio == null)
            {
                oListaPlanEstudio = new List<PlanEstudio>();
            }
            return Json(new { data = oListaPlanEstudio }, JsonRequestBehavior.AllowGet);
        }
    }
}
