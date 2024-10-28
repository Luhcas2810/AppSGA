﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapaDatos;
using CapaModelos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CapaWeb.Controllers
{
    public class PlanEstudioController : Controller
    {
        // GET: PlanEstudio
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CrearPlanEstudio(PlanEstudio planEstudio)
        {
            bool resultado = await PlanEstudioAD.Instancia.CrearPlanEstudioAsync(planEstudio);
            return Json(new { respuesta = resultado }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> CambiarEstadoPlanEstudio(int idPlan, bool habilitar)
        {
            bool resultado = await PlanEstudioAD.Instancia.CambiarEstadoPlanEstudioAsync(idPlan, habilitar);
            return Json(new { respuesta = resultado }, JsonRequestBehavior.AllowGet);
        }
    }
}
