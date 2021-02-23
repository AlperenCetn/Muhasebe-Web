using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using SaTeknopark_MVC5;
using IronBarCode;


namespace WebcamMVC.Controllers
{
    public class PhotoController : Controller
    {
       


        public ActionResult Index()
        {
            if(ViewBag.Barkod == null) { 
            ViewBag.Barkod = "";
            }

            return View();
        }

        [HttpPost]
        public ActionResult PhotoCapture(string resim)
        {

            if (!System.IO.Directory.Exists(Server.MapPath("~/Captures")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Captures"));
            }


            byte[] data = null;
            bool png = false;
            string cleandata = resim.Replace("data:image/png;base64,", "");
            try
            {

                data = System.Convert.FromBase64String(cleandata);
                png = true;
            }
            catch
            {
                png = false;
                cleandata = resim.Replace("data:image/jpeg;base64,", "");
                data = System.Convert.FromBase64String(cleandata);
            }
            MemoryStream ms = new MemoryStream(data);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

            Image res = AyarMetot.ByteArrayToImage(data);
            var eee = Image.FromStream(ms);
            string photoVal = "";

            if (png)
            {
                res.Save(Path.Combine(@"C:\Program Files (x86)\IIS Express", "Barkod.png"), ImageFormat.Png);
                photoVal = "Barkod.png";
            }
            else
            {
                res.Save(Path.Combine(@"C:\Program Files (x86)\IIS Express", "Barkod.jpg"), ImageFormat.Jpeg);
                photoVal = "Barkod.jpg";
            }

            string barkod = BarkodOku("111.png");

            if (ViewBag.Barkod == "")
            {
                ViewBag.Barkod = barkod;
            }


            var result = new { sonuc = barkod, };
            ViewBag.OkunmusBarkod = barkod;
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public string BarkodOku(string sFileName)
        {
            BarcodeResult Result = BarcodeReader.QuicklyReadOneBarcode(sFileName);
            if (Result != null)
            {
                ViewBag.Barkod = Result.ToString();
                return Result.ToString();

            }
            else
            {
                ViewBag.Barkod = "okunmadı";
                return "Okunamadı.";
            }
        }
    }
}
