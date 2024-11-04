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
    public class CursoController : Controller
    {
        // GET: Curso
        public ActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaCurso()
        {
            List<Curso> listaCursos = await CursoAD.Instancia.ObtenerListaCursoAsync();
            if (listaCursos == null)
            {
                listaCursos = new List<Curso>();
            }
            return Json(new { data = listaCursos }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DetalleCurso(int idCurso)
        {
            Curso curso = (await CursoAD.Instancia.ObtenerListaCursoAsync()).FirstOrDefault(x => x.Codigo == idCurso);
            return Json(new { data = curso }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarCurso(Curso curso)
        {
            bool resultado;
            if (curso.Codigo == 0)
            {
                // Crear nuevo curso
                resultado = await CursoAD.Instancia.CrearCursoAsync(curso);
            }
            else
            {
                // Modificar curso existente
                resultado = await CursoAD.Instancia.ModificarCursoAsync(curso);
            }
            return Json(new { respuesta = resultado }, JsonRequestBehavior.AllowGet);
        }
    }
}
