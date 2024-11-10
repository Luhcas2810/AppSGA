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
        public async Task<JsonResult> AcrtualizarUsuario(Usuario usuario)
        {
            usuario.Contrasenia = string.IsNullOrEmpty(usuario.Contrasenia) ? "" : Encriptar.GetSHA256(usuario.Contrasenia);
            Resultado resultado = new Resultado();
            if (usuario.Codigo == 0)
            {
                resultado = await UsuarioAD.Instancia.AgregarUsuarioAsync(usuario);
            }
            else
            {
                resultado = await UsuarioAD.Instancia.ModificarUsuarioAsync(usuario);
            }
            return Json(new { data = resultado });
        }

        [HttpPost]
        public async Task<JsonResult> CambiarEstado(int CodigoUsuario)
        {
            Usuario usuario = (await UsuarioAD.Instancia.ObtenerListaUsuarioAsync()).FirstOrDefault(x => x.Codigo == CodigoUsuario);
            usuario.Activo = usuario.Activo ? false : true;
            Resultado resultado = await UsuarioAD.Instancia.ModificarUsuarioAsync(usuario);
            return Json(new { data = resultado });
        }
    }
}
