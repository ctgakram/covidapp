using AppProj.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Controllers
{
    public class FileUploadController : Controller
    {


        public ActionResult FileUpload()
        {



            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(VmFileUpload upload)
        {
            try
            {
                if (upload.file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(upload.file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Uploads"), _FileName);
                    upload.file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }



        }



    }
}
