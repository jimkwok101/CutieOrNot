using CutieOrNot.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CutieOrNot.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PasteImage result = new PasteImage();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                int randnum = 5;
                string selectQuery = @"SELECT * FROM pasteImage WHERE pasteId = @pasteId";

                result = db.Query<PasteImage>(selectQuery, new { pasteId = randnum }).SingleOrDefault();
                return View(result);
            }
          
        }
        public JsonResult GetNextRandomImage()
        {
            PasteImage result = new PasteImage();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //Random rnd = new Random();
                //int randnum = rnd.Next(1, 10);
                string selectQuery = @"SELECT TOP 1 pasteURL,pasteDesc FROM pasteImage ORDER BY NEWID()"; //this sql will generate a random pasteId from existing

                result = db.Query<PasteImage>(selectQuery).SingleOrDefault();
                return Json(result, JsonRequestBehavior.AllowGet);

            }
         
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}