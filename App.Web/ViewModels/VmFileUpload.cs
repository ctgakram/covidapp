using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.ViewModels
{
    public class VmFileUpload
    {
        public int type { get; set; }
        public HttpPostedFileBase file { get; set; }


    }
}