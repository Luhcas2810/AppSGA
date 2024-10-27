using CapaDatos;
using CapaModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CapaWeb.Controllers
{
    public class ProgramaController : Controller
    {
        // GET: Programa
        public ActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaPrograma()
        {
            List<Programa> oListaPrograma = await ProgramaAD.Instancia.ObtenerListaProgramaAsync();
            if(oListaPrograma == null)
            {
                oListaPrograma = new List<Programa>();
            }
            return Json(new { data = oListaPrograma }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarPrograma(Programa programa)
        {
            bool resultado;
            if (programa.IdPrograma == 0)
            {
                resultado = await ProgramaAD.Instancia.CrearProgramaAsync(programa);
            }
            else
            {
                return Json(new { data = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }
    }
}