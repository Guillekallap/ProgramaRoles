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
                name: "ObtenerRoles",
                url: "{controller}/{action}/{rrol}",
                defaults: new { controller = "Rol" , action = "ObtenerRoles", rrol= UrlParameter.Optional }
            );
        }
    }
}
