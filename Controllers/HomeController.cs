using Razer_View.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Razer_View.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["my_connnection"].ConnectionString;
        //readonly string[] allowedExtensions = new string[] { ".pdf", ".jpeg", ".jpg" };
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //get view
        public ActionResult imageUpload() 
        {
            return View();
        }

        //post view
        public ActionResult InsertUserRecordWithImage(ImageUploadModel upload) 
        {
            try
            {
                //string extension = Path.GetExtension(upload.imagefile.FileName);

                //if (!allowedExtensions.Contains(extension)) {
                //    return "Invalid file";
                //}

                string filepath = "";
                if(upload.imagefile!=null)
                {
                    string filename = Path.GetFileName(upload.imagefile.FileName);
                    filepath = @"E://Content//UploadedImages//"+ filename;//for physical path
                    //filepath = @"E:\MVC Basic\Razer_View 1\Content\UploadedImages\" + filename;//for virtual path
                    upload.imagefile.SaveAs(filepath);
                    
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}