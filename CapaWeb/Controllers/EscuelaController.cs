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
    public class EscuelaController : Controller
    {
        // GET: Programa
        public ActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaEscuela()
        {
            List<Escuela> oListaPrograma = await EscuelaAD.Instancia.ObtenerListaEscuelaAsync();
            if(oListaPrograma == null)
            {
                oListaPrograma = new List<Escuela>();
            }
            return Json(new { data = oListaPrograma }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> CrearEscuela(Escuela programa)
        {
            bool resultado = await EscuelaAD.Instancia.CrearEscuelaAsync(programa);
            return Json(new { respuesta = resultado });
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarEscuela(Escuela escuela)
        {
            bool resultado;
            if (escuela.Codigo == 0)
            {
                resultado = await EscuelaAD.Instancia.CrearEscuelaAsync(escuela);
            }
            else
            {
                return Json(new { data = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }
    }
}