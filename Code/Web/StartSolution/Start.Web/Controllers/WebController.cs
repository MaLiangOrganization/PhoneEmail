using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Start.Web.Controllers
{
    public class WebController : Controller
    {
        //
        // GET: /Web/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Email()
        {
            return View(); 
        }
        public ActionResult Personal()
        {
            return View();
        }
        public ActionResult Phone()
        {
            return View();
        }
        public ActionResult SendEmail()
        {
            return View();
        }
    }
}
