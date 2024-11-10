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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login(string _usuario, string contrasenia)
        {
            Usuario usuario = (await UsuarioAD.Instancia.ObtenerListaUsuarioAsync()).FirstOrDefault(x => x._Usuario == _usuario || x.Correo == _usuario && x.Contrasenia == Encriptar.GetSHA256(contrasenia));
            Resultado resultado = new Resultado()
            {
                Respuesta = true,
                Mensaje = "Inicio de sesión exitoso", 
                Redireccion = Url.Action("Index", "Home")
            };
            if (usuario == null)
            {
                resultado = new Resultado()
                {
                    Respuesta = false,
                    Mensaje = "Usuario o contraseña incorrecta"
                };
            }
            if (!usuario.Activo)
            {
                resultado = new Resultado()
                {
                    Respuesta = false,
                    Mensaje = "Usuario bloqueado"
                };
            }
            Session["Usuario"] = usuario;
            return Json(new { data = resultado });
        }

        [HttpPost]
        public async Task<JsonResult> Logout(int Codigo)
        {
            Usuario usuario = (await UsuarioAD.Instancia.ObtenerListaUsuarioAsync()).FirstOrDefault(x => x.Codigo == Codigo);
            Session["Usuario"] = null;
            Resultado resultado = new Resultado()
            {
                Respuesta = true,
                Mensaje = $"Vuelve pronto {usuario.Nombre} {usuario.Apellido}",
                Redireccion = Url.Action("Login", "Login")
            };
            return Json(new { data = resultado });
        }
    }
}