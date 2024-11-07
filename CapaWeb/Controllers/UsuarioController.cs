using CapaDatos;
using CapaModelos;
using CapaWeb.Permisos;
using CapaWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CapaWeb.Controllers
{
    [ValidarSesion]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaUsuario()
        {
            List<Usuario> oListaUsuario = await UsuarioAD.Instancia.ObtenerListaUsuarioAsync();
            if (oListaUsuario == null)
            {
                oListaUsuario = new List<Usuario>();
            }
            return Json(new { data = oListaUsuario }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> CrearUsuario(Usuario usuario)
        {
            usuario.Contrasenia = Encriptar.GetSHA256(usuario.Contrasenia);
            Resultado resultado = await UsuarioAD.Instancia.AgregarUsuarioAsync(usuario);
            return Json(new { data = resultado });
        }
    }
}
