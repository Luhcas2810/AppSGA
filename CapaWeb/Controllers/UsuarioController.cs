using CapaDatos;
using CapaModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaWeb.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Crear()
        {
            return View();
        }

        public JsonResult ObtenerListaUsuario()
        {
            List<Usuario> oListaUsuario = UsuarioAD.Instancia.ObtenerListaUsuario();
            return Json(new { data = oListaUsuario }, JsonRequestBehavior.AllowGet);
        }
    }
}