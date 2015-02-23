using System.Web;
using System.Web.Mvc;
using MyNotebook.Site.Filters;

namespace MyNotebook.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyNotebookHandleErrorAttribute());
        }
    }
}
