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
        public async Task<ActionResult> Login(string _usuario, string contrasenia)
        {
            Usuario usuario = (await UsuarioAD.Instancia.ObtenerListaUsuarioAsync()).FirstOrDefault(x => x._Usuario == _usuario && x.Contrasenia == Encriptar.GetSHA256(contrasenia));
            if (usuario == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrecta";
                return View();
            }

            Session["Usuario"] = usuario;

            return RedirectToAction("Index", "Home");
        }
    }
}