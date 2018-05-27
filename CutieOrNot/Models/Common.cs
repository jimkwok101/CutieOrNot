using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;

namespace CutieOrNot.Models
{
    public static class Common
    {
        /// <summary>
        /// pass in an image filename (extract from its url string) and return a unique random pastePageID
        /// </summary>
        /// <param name="sVidFileName"></param>
        /// <returns></returns>
        public static string GenerateImagePageID(string sVidFileName)
        {
            //----------------- Hashing a v_id for unique video ----------------//
            Random rand = new Random();
            int iSalt = rand.Next(1, 1000000);
            string v_id = CreateHashString(sVidFileName, iSalt.ToString());
            return v_id.Substring(0, 18);//just read 18 chars
        }
        public static string CreateHashString(string sStringToHash, string salt)
        {
            string saltAndPwd = String.Concat(sStringToHash, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA1");
            return hashedPwd;
        }
        public static string GetFileNameFromUrlString(string httpUrlString)
        {
            //var uri = new Uri(hreflink);
            //var filename = uri.Segments.Last(); //get the last part of the file path which is the filename w/ extension
            //Url = http://yahoo.com/v1/index.aspx
            //url.host = yahoo.com
            //url.AbsolutePath =/ v1 / index.aspx
            //Url.AbsoluteUri = the whole url
            //Url.IsAbsoluteUri = true
            //Url.IsLoopback = false
            //Url.LocalPath = AbsolutePath
            //Url.OriginalString = AbsoluteUri
            //Url.PathAndQuery = AbsolutePath = LocalPath
            //Url.port = 80
            //Url.Query = ""
            //Url.Scheme = "http"

            string filename = null;
            Uri uri = new Uri(httpUrlString);
            if (uri.IsAbsoluteUri)
            {
                 filename = System.IO.Path.GetFileName(uri.LocalPath);
              
            }
            return filename;
        }
       public static  bool IsImageUrl(string URL)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Method = "HEAD";
            using (var resp = req.GetResponse())
            {
                return resp.ContentType.ToLower(CultureInfo.InvariantCulture)
                           .StartsWith("image/");
            }
        }
    }
}

//public class ClientErrorHandler : FilterAttribute, IExceptionFilter
//{
//    public void OnException(ExceptionContext filterContext)
//    {
//        var response = filterContext.RequestContext.HttpContext.Response;
//        response.Write(filterContext.Exception.Message);
//        response.ContentType = MediaTypeNames.Text.Plain;
//        filterContext.ExceptionHandled = true;
//    }
//}

//[ClientErrorHandler]
//public class SomeController : Controller
//{
//    [HttpPost]
//    public ActionResult SomeAction()
//    {
//        throw new Exception("Error message");
//    }
//}
//View script:

//$.ajax({
//type: "post", url: "/SomeController/SomeAction",
//    success: function(data, text) {
//        //...
//    },
//    error: function(request, status, error) {
//        alert(request.responseText);
//    }
//});