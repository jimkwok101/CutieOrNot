using CutieOrNot.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace CutieOrNot.Controllers
{
    public class PostPicController : Controller
    {
        //[Authorize]
        public ActionResult Index()
        {
            //select ROUND(AVG(i.imgscore), 2 )
            //from pasteImage p, ImageDetails i
            //where p.pasteURL = i.imgURL
            //and p.pastePageID = 'page1' //round 2 decimal point
            return View();
        }
        [HttpPost]
        public ActionResult InsertPasteURL(string pasteURL, string pasteDesc)
        {
            try
            {
                bool isImage = Common.IsImageUrl(pasteURL);
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    string sqlQuery = "Insert Into PasteImage (pasteURL,pasteDesc, pastePageID) Values(@pasteURL,@pasteDesc, @pastePageID)";
                    string imgFileName = Common.GetFileNameFromUrlString(pasteURL); //borrowed codes from Tubays v_id
                    string pastePageID = Common.GenerateImagePageID(imgFileName);//need filename and salt value to generate random value more unlikely to repeat
                    int rowsAffected = db.Execute(sqlQuery, new {pasteURL,pasteDesc, pastePageID });
                  
                }
                return   new HttpStatusCodeResult(200, "Uploaded Successfully!");

                            
            }catch(WebException ex)
            {
                
                 return new HttpStatusCodeResult(((HttpWebResponse)ex.Response).StatusCode, "Upload Error: " + ex.Message);//will trigger onFailure js side w/ xhr.statusText
                //or using this below to do the same but need to use xhr.responseText to get error msg
                //return Content(HttpStatusCode.NotFound, "I am JKCustomNotFound Message"); //must use IHttpActionResult for method returned type
                //from client side: error:function(xhr,status)
                //alert("\n status text: " + xhr.statusText +
                //   "\n status code: " + xhr.status +
                //   "\n custom error: " + xhr.responseText); //xhr.responseText since server side is a string 
            }



        }
    }
}

/*
 [Select]
          using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["myDbConnection"].ConnectionString))
            {
                string selectQuery = @"SELECT * FROM [dbo].[Customer] WHERE FirstName = @FirstName";

                var result = db.Query(selectQuery, new
                {
                    customerModel.FirstName
                });
            }

[Insert]
using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["myDbConnection"].ConnectionString))
{
    string insertQuery = @"INSERT INTO [dbo].[Customer]([FirstName], [LastName], [State], [City], [IsActive], [CreatedOn]) VALUES (@FirstName, @LastName, @State, @City, @IsActive, @CreatedOn)";

    var result = db.Execute(insertQuery, new
    {
        customerModel.FirstName,
        customerModel.LastName,
        StateModel.State,
        CityModel.City,
        isActive,
        CreatedOn = DateTime.Now
    });
}

[Update]
    using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["myDbConnection"].ConnectionString))
    {
        string updateQuery = @"UPDATE [dbo].[Customer] SET IsActive = @IsActive WHERE FirstName = @FirstName AND LastName = @LastName";

        var result = db.Execute(updateQuery, new
        {
            isActive,
            customerModel.FirstName,
            customerModel.LastName
        });
    }      

    [Delete]

    using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["myDbConnection"].ConnectionString))
{
    string deleteQuery = @"DELETE FROM [dbo].[Customer] WHERE FirstName = @FirstName AND LastName = @LastName";

    var result = db.Execute(deleteQuery, new
    {
        customerModel.FirstName,
        customerModel.LastName
    });
}
          ->>cnn.Execute("insert Table(val) values(@val)", new {val}); //better way

          -->cnn.Execute("update Table set val = @val where Id = @id", new {val, id = 1});//better way
   */

