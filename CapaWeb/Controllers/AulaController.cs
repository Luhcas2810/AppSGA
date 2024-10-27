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
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaAula()
        {
            List<Aula> oListaAula = await AulaAD.Instancia.ObtenerListaAula();
            if (oListaAula == null)
            {
                oListaAula = new List<Aula>();
            }
            return Json(new { data = oListaAula }, JsonRequestBehavior.AllowGet);
        }
    }
}