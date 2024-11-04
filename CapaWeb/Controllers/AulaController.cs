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
    public class AulaController : Controller
    {
        // GET: Aula
        public ActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaAula()
        {
            List<Aula> listaAulas = await AulaAD.Instancia.ObtenerListaAulaAsync();
            if (listaAulas == null)
            {
                listaAulas = new List<Aula>();
            }
            return Json(new { data = listaAulas }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DetalleAula(int idAula)
        {
            Aula aula = (await AulaAD.Instancia.ObtenerListaAulaAsync()).FirstOrDefault(x => x.IdAula == idAula);
            return Json(new { data = aula }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarAula(Aula aula)
        {
            bool resultado;
            if (aula.IdAula == 0)
            {
                // Crear nueva aula
                resultado = await AulaAD.Instancia.CrearAulaAsync(aula);
            }
            else
            {
                // Modificar aula existente
                resultado = await AulaAD.Instancia.ModificarAulaAsync(aula); // Asegúrate de tener este método en AulaAD.cs
            }
            return Json(new { respuesta = resultado }, JsonRequestBehavior.AllowGet);
        }
    }
}
