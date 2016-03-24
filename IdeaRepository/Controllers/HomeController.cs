using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdeaRepository.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (User.Identity.Name.Equals("admin"))
                    return View("~/Views/Account/AdminPage.cshtml");
                else
                    return View("~/Views/Account/UserPage.cshtml");
            }
            return View();
        }
    }
}