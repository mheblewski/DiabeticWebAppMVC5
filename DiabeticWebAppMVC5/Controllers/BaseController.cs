using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiabeticWebAppMVC5.Controllers
{
    public class BaseController : Controller
    {
        protected void ClearAndAddModelError(string message)
        {
            ModelState.Clear();
            ModelState.AddModelError("", message);
        }
    }
}