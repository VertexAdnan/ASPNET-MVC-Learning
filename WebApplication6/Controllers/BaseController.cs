using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (HttpContext.Session["UserId"] != null)
            {
                ViewBag.email = HttpContext.Session["email"].ToString();
                ViewBag.UserId = HttpContext.Session["UserId"];
            }
        }
    }
}