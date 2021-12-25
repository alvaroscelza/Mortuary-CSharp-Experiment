using System;
using System.Threading;
using System.Web.Mvc;

namespace mortuary.Controllers
{
    public class InternationalizationController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string languageCode = (string)ControllerContext.RouteData.Values["languageCode"];
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return base.BeginExecuteCore(callback, state);
        }
    }
}