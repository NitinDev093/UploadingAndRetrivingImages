using Newtonsoft.Json;
using Razer_View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Razer_View.Controllers
{
    public class UserController : Controller
    {
        string baseurl="https://localhost:7176/api/";
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> insertUser(UserModel user)
        {
            try
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress= new Uri(baseurl);
                    var userpayload=JsonConvert.SerializeObject(user);
                    var content=new StringContent(userpayload,Encoding.UTF8,"application/json");
                    HttpResponseMessage response=await client.PostAsync("User/InsertUser",content);
                    if (response.IsSuccessStatusCode) { 
                    return Json(new { success = true, message = "Data saved successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Something went wrong" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> ReadUser()
        {
            try
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress= new Uri(baseurl);
                    HttpResponseMessage response = await client.GetAsync("User/GetUser");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var users =JsonConvert.DeserializeObject<List<UserModel>>(jsonString);
                        return Json(new { success = true, data = users }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message="Unable to load data" }, JsonRequestBehavior.AllowGet);
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