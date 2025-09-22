using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Razer_View.Models
{
    public class ImageUploadModel
    {
        public int userid { get; set; }
        public string username { get; set; }
        public int userage { get; set; }
        public string usergender { get; set; }
        public string imagename { get; set; }
        public HttpPostedFileBase  imagefile { get; set; }
    }
}