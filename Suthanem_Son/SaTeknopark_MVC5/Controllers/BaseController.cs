using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaTeknopark_MVC5.Controllers
{
    public class BaseController : Controller
    {
        protected string con = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
    }
}