using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;

namespace Jeans.Language.Filters
{
    public class LanguageAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var language = string.Empty;
            if (filterContext.RouteData.Values["language"] != null &&
                !string.IsNullOrWhiteSpace(filterContext.RouteData.Values["language"].ToString()))
            {
                language = filterContext.RouteData.Values["language"].ToString();
                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
            else
            {
                language = Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant();
                filterContext.RouteData.Values["language"] = language;

                string url = $"/{language}/{filterContext.RouteData.Values["controller"]}/{filterContext.RouteData.Values["action"]}";
                filterContext.Result = new RedirectResult(url);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}