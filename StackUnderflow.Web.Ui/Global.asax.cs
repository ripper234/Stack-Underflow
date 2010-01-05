using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StackUnderflow.Bootstrap;
using StackUnderflow.Persistence.Repositories;
using StackUnderflow.Web.Ui.Controllers;
using StackUnderflow.Web.Ui.Utils;

namespace StackUnderflow.Web.Ui
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("{*css}", new { favicon = @"(.*/)?Site.css(/.*)?" });

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

            
        }

        protected void Application_Start()
        {
            var container = Bootstrapper.Instance.CreateContainer(typeof(HomeController).Assembly);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            // ugly - TODO: http://stackoverflow.com/questions/1992984/getting-the-current-username-in-asp-net-mvc
            UserContainer.UserRepository = container.Resolve<IUserRepository>();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}