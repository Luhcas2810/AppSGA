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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Crear()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerListaUsuario()
        {
            List<Usuario> oListaUsuario = await UsuarioAD.Instancia.ObtenerListaUsuarioAsync();
            return Json(new { data = oListaUsuario }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DetalleUsuario(int IdUsuario)
        {
            List<Usuario> oListaUsuario = await UsuarioAD.Instancia.ObtenerListaUsuarioAsync();
            Usuario oUsuario = oListaUsuario.FirstOrDefault(x => x.IdUsuario == IdUsuario);
            return Json(new { data = oUsuario }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ActualizarUsuario(Usuario usuario)
        {
            bool resultado;
            if(usuario.IdUsuario == 0)
            {
                resultado = await UsuarioAD.Instancia.CrearUsuarioAsync(usuario);
            }
            else
            {
                resultado = await UsuarioAD.Instancia.ModificarUsuarioAsync(usuario);
            }
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }
    }
}