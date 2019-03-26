using CommonLibs;
using ModemModule;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyMobile.Controllers
{
    public class ConfigsController : Controller
    {
        // GET: Configs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sendsubmit()
        {
            return RedirectToAction("Index");
        }
    }
}