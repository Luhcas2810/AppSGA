﻿using CapaWeb.Permisos;
using System.Web;
using System.Web.Mvc;

namespace CapaWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
