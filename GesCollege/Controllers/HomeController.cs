using GesCollege.Models;
using GesCollege.PlutoCollege;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace GesCollege.Controllers
{
    public class HomeController : Controller
    {
        private CollegeContext db = new CollegeContext();
        public ActionResult Index()
        {
            
            var Col = from ens in db.Colleges
                             select ens;
            return View(Col);
        }

        public ActionResult About()
        {
            ViewBag.Message = " Description page.";
            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = " contact page.";

            return View();
        }
    }
}