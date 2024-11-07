using CapaDatos;
using CapaModelos;
using CapaWeb.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CapaWeb.Controllers
{
    [ValidarSesion]
    public class EstudianteController : Controller
    {
        // GET: Estudiante
        public ActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaEstudiante()
        {
            List<Estudiante> oListaEstudiante = await EstudianteAD.Instancia.ObtenerListaEstudianteAsync();
            if (oListaEstudiante == null)
            {
                oListaEstudiante = new List<Estudiante>();
            }
            return Json(new { data = oListaEstudiante }, JsonRequestBehavior.AllowGet);
        }
    }
}