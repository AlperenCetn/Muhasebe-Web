using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaTeknopark_MVC5.Models;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class NotlarController : Controller
    {
        // GET: Notlar
        public ActionResult Notlar()
        {
                return View();
        }
        [HttpGet]
        public ActionResult GetNotlarList()
        {
            string FirmaID = Session["FirmaID"].ToString();
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            List<NotlarListesi> yonetim = new List<NotlarListesi>();
            string sorg = @"Select (select KategoriAdi from  Not_Kategori nk where nk.ID=nl.KategoriID ) as KategoriAdi,NotBasligi,ID,Tarih,NotIcerik from  Notlar_Listesi nl where nl.FirmaID= " + FirmaID;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand ozelgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = ozelgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            NotlarListesi yt = new NotlarListesi();

                            yt.KategoriAdi = dr["KategoriAdi"].ToString();
                            yt.NotBasligi = dr["NotBasligi"].ToString();
                            yt.Notİcerik = dr["NotIcerik"].ToString();
                                yt.KategoriID= Convert.ToInt32(dr["ID"].ToString());
                                yt.ID = dr["ID"].ToString();
                            yt.Tarih = Convert.ToDateTime(dr["Tarih"].ToString()).ToString("dd.MM.yyyy");
                            yt.TarihF2 = Convert.ToDateTime(dr["Tarih"]).ToString("dd.MM.yyyy");
                            yonetim.Add(yt);

                        }
                    }
                }
            }


            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }


        sayazilimEntities db = new sayazilimEntities();
        [HttpPost]
        public JsonResult YeniNot(Notlar_Listesi dk)
        {
            Notlar_Listesi car = null;
            string Message = "Kayıt Eklendi";
            if (dk.ID == -1)
            {
                car = new Notlar_Listesi();
                car = dk;
                car.KategoriID = dk.KategoriID;
                car.NotBasligi = dk.NotBasligi;
                car.NotIcerik = dk.NotIcerik;
                car.Tarih = dk.Tarih;
                car.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                car.Company_Code = company_code;
                db.Notlar_Listesi.Add(car);


                db.SaveChanges();

      
            }
            else
            {
                car = db.Notlar_Listesi.Where(x => x.ID == dk.ID).FirstOrDefault<Notlar_Listesi>();
                car.KategoriID = dk.KategoriID;
                car.NotBasligi = dk.NotBasligi;
                car.NotIcerik = dk.NotIcerik;
                car.Tarih = dk.Tarih;
                car.ID = dk.ID;
                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }

            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult NotlarDelete(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {

                Notlar_Listesi emp = db.Notlar_Listesi.Where(x => x.ID == id).FirstOrDefault<Notlar_Listesi>();
                db.Notlar_Listesi.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult NotlarBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                Notlar_Listesi emp = db.Notlar_Listesi.Where(x => x.ID == id).FirstOrDefault<Notlar_Listesi>();

                ViewBag.NotTarih = Convert.ToDateTime(emp.Tarih).ToString("yyyy-MM-dd");
                return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);

            }
        }

        public class NotlarListesi
        {
            public string KategoriAdi { get; set; }
            public string NotBasligi { get; set; }
            public string Tarih { get; set; }
            public string Notİcerik { get; set; }
            public string TarihF2 { get; set; }
            public string ID { get; set; }
            public int KategoriID { get; set; }


        }



        public JsonResult YeniNotKategori(Not_Kategori dk)
        {
            Not_Kategori car = null;
            string Message = "Kayıt Eklendi";
            if (dk.ID == -1)
            {
                car = new Not_Kategori();
                car = dk;
                car.KategoriAdi = dk.KategoriAdi;
                car.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                car.Company_Code = company_code;
                db.Not_Kategori.Add(car);


                db.SaveChanges();


            }
            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }






    }
}