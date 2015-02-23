using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyNotebook.Site.App_Start;
using MyNotebook.Site.Logging;
using MyNotebook.Site.Settings;
using MyNotebook.Domain.Logging;
using System.Data.Entity;
using MyNotebook.Domain.Entities;

namespace MyNotebook.Site
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<MyNotebookDbContext>(new MyNotebookDbInitializer());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            LoggingConfig.RegisterLoggingSources(new List<EventSource> { SiteEventSource.Log, DomainEventSource.Log },
                (ISiteSettings)NinjectWebCommon.Kernel.GetService(typeof(ISiteSettings)));
        }
    }
}
