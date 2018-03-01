﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProgramaRoles
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "UsuariosSectores", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EditarUsuarioSector",
                url: "{controller}/{action}/{ddni}/{nnombreUsuario}/{nnombreSector}",
                defaults: new { controller = "Rol" , action = "EditarUsuarioSector", ddni = UrlParameter.Optional, nnombreUsuario = UrlParameter.Optional, nnombreSector = UrlParameter.Optional }
            );
        }
    }
}
