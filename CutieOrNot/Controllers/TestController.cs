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
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetPasteImage(string pasteDesc = "")
        {
            List<PasteImage> result = new List<PasteImage>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string selectQuery = @"SELECT * FROM [dbo].[PasteImage] WHERE pasteDesc = @pasteDesc";

                 result = db.Query<PasteImage>(selectQuery, new
                {
                    pasteDesc
                }).ToList(); //because it returns a list
                return PartialView("_getPasteImageList", result);
            }

              
          
        }
    }
}