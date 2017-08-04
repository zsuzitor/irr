using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace irr
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                          name: "id_1",
                          url: "/id{id}",
                          defaults: new { controller = "Home", action = "Show_one_ad",id = UrlParameter.Optional }
                      );

            routes.MapRoute(
                name: "Up1",
                url: "up",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                           name: "Real_estate_next1_1",
                           url: "Real-estate/{type}/{type2}/page{pg}/id{id}",
                           defaults: new { controller = "Home", action = "Show_one_ad" }
                       );
            routes.MapRoute(
                           name: "Real_estate_next1",
                           url: "Real-estate/{type}/{type2}/page{pg}",
                           defaults: new { controller = "Home", action = "list_ad" }
                       );
            routes.MapRoute(
                          name: "Real_estate_next2_2",
                          url: "Real-estate/{type}/{type2}/id{id}",
                          defaults: new { controller = "Home", action = "Show_one_ad" }
                      );
            routes.MapRoute(
                           name: "Real_estate_next2",
                           url: "Real-estate/{type}/{type2}",
                           defaults: new { controller = "Home", action = "list_ad" }
                       );
            routes.MapRoute(
                           name: "Real_estate_next3_3",
                           url: "Real-estate/{type}/id{id}",
                           defaults: new { controller = "Home", action = "Show_one_ad" }
                       );
            routes.MapRoute(
                           name: "Real_estate_next3",
                           url: "Real-estate/{type}",
                           defaults: new { controller = "Home", action = "list_ad" }
                       );
           /* routes.MapRoute(
                          name: "Real_estate_next3",
                          url: "Real-estate/{type}/{type2}",
                          defaults: new { controller = "Home", action = "list_ad", type2 = UrlParameter.Optional }
                      );*/

            /* routes.MapRoute(
                 name: "Real_estate_next1",
                 url: "Real-estate/{type}/page{pg}",
                 defaults: new { controller = "Home", action = "list_ad",  pg = UrlParameter.Optional }
             );*/



            routes.MapRoute(
                name: "Real_estate1",
                url: "Real-estate",
                defaults: new { controller = "Home", action = "Real_estate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                           name: "Search_1",
                           url: "Search",
                           defaults: new { controller = "Home", action = "Extended_search" }
                       );
           




            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Categories", id = UrlParameter.Optional }
            );
        }
    }
}
