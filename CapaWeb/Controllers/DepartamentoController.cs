using CapaDatos;
using CapaModelos;
using CapaWeb.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CapaWeb.Controllers
{
    [ValidarSesion]
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        public ActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaDepartamento()
        {
            List<Departamento> oListaPrograma = await DepartamentoAD.Instancia.ObtenerListaDepartamentoAsync();
            if(oListaPrograma == null)
            {
                oListaPrograma = new List<Departamento>();
            }
            return Json(new { data = oListaPrograma }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> CrearDepartamento(Departamento departamento)
        {
            bool resultado = await DepartamentoAD.Instancia.CrearDepartamentoAsync(departamento);
            return Json(new { data = resultado });
            //bool resultado;
            //if (departamento.Codigo == 0)
            //{
            //    resultado = await DepartamentoAD.Instancia.CrearDepartamentoAsync(departamento);
            //}
            //else
            //{
            //    return Json(new { data = false }, JsonRequestBehavior.AllowGet);
            //}
            //return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }
    }
}