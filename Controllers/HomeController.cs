using Razer_View.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
                string filepath = "";//Declare it globable to use it in sql query
                if (upload.imagefile!=null && upload.imagefile.ContentLength>0)
                {
                    string[] allowedExtensions = new string[] { ".pdf", ".jpeg", ".jpg" };
                    string fileExtension = Path.GetExtension(upload.imagefile.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return Json(new {success="false",message= "Only PDF, JPEG and JPG files are allowed!" },JsonRequestBehavior.AllowGet);
                    }
                    int maxsize = 5*1024*1024;
                    if (upload.imagefile.ContentLength>maxsize)
                    {
                        return Json(new { success = "false", message = "Size should be less or equal to 5mb" }, JsonRequestBehavior.AllowGet);
                    }
                    //check directory
                    string path1=@"E:/UploadedImages";
                    if (!Directory.Exists(path1))
                    {
                        DirectoryInfo Directory = new DirectoryInfo(path1);
                        Directory.Create();
                    }
                    //for unique file name
                    string filename = Path.GetFileNameWithoutExtension(upload.imagefile.FileName);
                    string finalfilename =filename+"_"+DateTime.Now.ToString("yyyyMMddHHmmss")+fileExtension;//for physical path
                    filepath = Path.Combine(path1, finalfilename);
                    upload.imagefile.SaveAs(filepath);
                    
                }
                using (SqlConnection sqlcon=new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_insertImagewithuserdata", sqlcon);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", upload.username);
                    cmd.Parameters.AddWithValue("@userage", upload.userage);
                    cmd.Parameters.AddWithValue("@usergender", upload.usergender);
                    cmd.Parameters.AddWithValue("@imagename", upload.imagename);
                    cmd.Parameters.AddWithValue("@imagefile", filepath);
                    sqlcon.Open();
                    int i=cmd.ExecuteNonQuery();
                    if (i>0)
                    {
                        return Json(new { success = "true", message = "Record inserted successfully!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = "false", message = "Server error" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}