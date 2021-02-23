using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaTeknopark_MVC5.Controllers
{
    public class HizliErisimController : Controller
    {
        // GET: HizliErisim
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HizliCari()
        {
            AyarMetot.Siradaki("", "Cari", "FirmaKodu", Session["FirmaID"].ToString());
            ViewBag.CariKoduSiradaki3 = AyarMetot.GetNumara;


            return View();
        }

        public ActionResult HizliStok()
        {
            AyarMetot.Siradaki("", "Stok", "StokKodu", Session["FirmaID"].ToString());
            ViewBag.StokKoduSiradaki = AyarMetot.GetNumara;
            return View();
        }

        public ActionResult HizliFatura()
        {
            return View();
        }


    }
}