using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sayazilim_Teknopark;

namespace SaTeknopark_MVC5.Controllers
{
    public class SMSController : Controller
    {
        // GET: SMS
        public ActionResult Index()
        {

            Mesaj.SMSGonder("Deneme Mesajı", "05073979280", "TESCOM", AyarMetot.SMSUser, AyarMetot.SMSPass,
                AyarMetot.SMSSender);

           return View();
        }
    }
}