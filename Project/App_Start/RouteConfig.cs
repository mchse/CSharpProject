using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Project.Models;

namespace Project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DeleteSong",
                url: "Customers/DeleteSong/{id}/{songId}",
                defaults: new
                {
                    controller = "Customers",
                    action = "DeleteSong" 
                }
            );

            routes.MapRoute(
                name: "RegisterSong",
                url: "Song/RegisterSong/{id}/{custId}",
                defaults: new
                {
                    controller = "Song",
                    action = "RegisterSong"
                }
            );

            routes.MapRoute(
                name: "SongLibrary",
                url: "Song/SongLibrary",
                defaults: new
                {
                    controller = "Song",
                    action = "SongLibrary",
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home",
                                action = "Index",
                                id = UrlParameter.Optional }
            );
        }
    }
}
