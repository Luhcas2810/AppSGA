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
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaRol()
        {
            List<Rol> oListaRol = await RolAD.Instancia.ObtenerListaRolAsync();
            if (oListaRol == null)
            {
                oListaRol = new List<Rol>();
            }
            return Json(new { data = oListaRol }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> CrearRol(Rol rol)
        {
            bool resultado = await RolAD.Instancia.CrearRolAsync(rol);
            return Json(new { data = resultado });
        }
    }
}