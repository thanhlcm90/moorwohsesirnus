using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Frontend
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            "DefaultListNews", // URL LISTNEWS
                "{tin-tuc}/{danh-sach}/{chuyen-muc}/{id1}-{id2}",
                new { controller = "News", action = "NewsbyCatalogue", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            "DefaultListProduct", // URL LISTPRODUCT
                "{san-pham}/{hang-sp}/{id1}/{id2}",
                new { controller = "Product", action = "Product", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DefaultDetailsProduct", // URL DETAILSPRODUCT
                "{chuyen-muc}/{chi-tiet}/{tieu-de-khong-dau}-{id}",
                new { controller = "Product", action = "DetailProduct", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DefaultDetailsNew", // URL DETAILSNEWS
                "{chuyen-muc}/{tieu-de-khong-dau}-{id}",
                new { controller = "News", action = "NewsDetails", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DefaultProduct", // URL DETAILSPRODUCT
                "{controller}/{action}/{id1}/{id2}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // Thêm Roles Admin
            if (!Roles.RoleExists("admin"))
            {
                Roles.CreateRole("admin");
                Roles.AddUserToRole("ADMIN", "admin");
            }

            // Đọc toàn bộ danh sách các Controller, sau đó đưa vào Roles với những Controller có chữ Admin ở trước
            var listController = GetControllerNames();
            foreach (String s in listController)
            {
                if (s.ToUpper().StartsWith("ADMIN"))
                {
                    if (!Roles.RoleExists(s))
                    {
                        Roles.CreateRole(s);
                    }
                }
            }
        }

        private static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(
                type => type.IsSubclassOf(typeof(T))).ToList();
        }

        public List<string> GetControllerNames()
        {
            List<string> controllerNames = new List<string>();
            GetSubClasses<Controller>().ForEach(
                type => controllerNames.Add(type.Name.Substring(0, type.Name.Length - 10)));
            return controllerNames;
        }
    }
}