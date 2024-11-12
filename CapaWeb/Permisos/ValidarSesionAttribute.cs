using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CapaWeb.Permisos
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CapaModelos.Usuario usuario = (CapaModelos.Usuario)HttpContext.Current.Session["usuario"];
            if (usuario == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
            }
            else
            {
                //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                //string actionName = filterContext.ActionDescriptor.ActionName;
                //CapaModelos.Permiso permiso = CapaDatos.PermisoAD.Instancia.ObtenerListaPermiso()
                //    .Where(x => x.CodigoRol == usuario.CodigoRol && x._Submenu.Controlador == controllerName && x._Submenu.Vista == actionName && x.Activo)
                //    .FirstOrDefault();
                //if (controllerName != "Home" && permiso == null)
                //{
                //    filterContext.Result = new RedirectResult("~/Home/Index");
                //}
            }
            base.OnActionExecuting(filterContext);
        }
    }
}