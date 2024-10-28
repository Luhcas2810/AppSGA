using CapaDatos;
using CapaModelos;
using CapaWeb.Utilidades;
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

        [HttpGet]
        public async Task<JsonResult> ObtenerListaUsuario()
        {
            List<Usuario> oListaUsuario = await UsuarioAD.Instancia.ObtenerListaUsuarioAsync();
            if(oListaUsuario == null)
            {
                oListaUsuario = new List<Usuario>();
            }
            return Json(new { data = oListaUsuario }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DetalleUsuario(int IdUsuario)
        {
            Usuario oUsuario = (await UsuarioAD.Instancia.ObtenerListaUsuarioAsync()).FirstOrDefault(x => x.IdUsuario == IdUsuario);
            return Json(new { data = oUsuario }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarUsuario(Usuario usuario)
        {
            usuario.Contrasenia = Encriptar.GetSHA256(usuario.Contrasenia);
            usuario._Rol = (await RolAD.Instancia.ObtenerListaRolAsync()).FirstOrDefault(x => x.IdRol == usuario.IdRol);
            if (usuario.Contrasenia.Length > 256)
            {
                usuario.Contrasenia = usuario.Contrasenia.Substring(0, 256);
            }
            bool resultado;
            if(usuario.IdUsuario == 0)
            {
                resultado = await UsuarioAD.Instancia.CrearUsuarioAsync(usuario);
            }
            else
            {
                resultado = await UsuarioAD.Instancia.ModificarUsuarioAsync(usuario);
            }
            return Json(new { respuesta = resultado }, JsonRequestBehavior.AllowGet);
        }
    }
}