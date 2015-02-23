using MyNotebook.Site.Logging;

namespace MyNotebook.Site.Filters
{
    public class MyNotebookHandleErrorAttribute : System.Web.Http.Filters.ExceptionFilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        public void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            SiteEventSource.Log.Failure(filterContext.Exception.Message);
        }

        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext context)
        {
            SiteEventSource.Log.Failure(context.Exception.Message);
        }
    }
}