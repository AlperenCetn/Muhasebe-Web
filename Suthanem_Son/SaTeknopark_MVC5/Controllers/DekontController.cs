using SaTeknopark_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class DekontController : Controller
    {
        public ActionResult Dekont()
        {
            AyarMetot.Siradaki("", "DEKONT", "IslemNumarası",Session["FirmaID"].ToString());
            ViewBag.DekontKoduSiradaki = AyarMetot.GetNumara;
            return View();
        }
    
        public class DekontModel
        {
            public string IslemTipi { get; set; }
            public string IslemNo { get; set; }
            public string CariUnvan { get; set; }
            public DateTime IslemTarih { get; set; }
            public DateTime VadeTarihi { get; set; }
            public string Tutar { get; set; }
            public string Aciklama { get; set; }
            public string CariID { get; set; }
            public string OzelKodID { get; set; }

            public string KodAdi { get; set; }
            public string ID { get; set; }
            public string PersonelID { get; set; }
            public string TarihF2 { get; set; }

            public string ParaBirimi { get; set; }



        }

    
   
        [HttpGet]
        public ActionResult GetDekont()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<DekontModel> yonetim = new List<DekontModel>();
            string FirmaID = Session["FirmaID"].ToString();
            string sorg = @"SET DATEFORMAT dmy; Select CariID,ParaBirimi,IslemTarih,IslemNo ,IslemTipi,ID,VadeTarihi,PersonelID,Tutar,Aciklama,(select CariUnvan from Cari where CariID=Cari.ID) as CariUnvan,(select KodAdi from OzelKod where ID = Dekont.OzelKodID) as KodAdi from Dekont where FirmaID = " + FirmaID;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand dekontgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = dekontgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                          
                            
                             DekontModel yt = new DekontModel();
                            yt.KodAdi = dr["KodAdi"].ToString();
                            yt.CariUnvan = dr["CariUnvan"].ToString();
                            yt.IslemTipi = dr["IslemTipi"].ToString();
                            yt.IslemNo = dr["IslemNo"].ToString();
                            yt.CariID = dr["CariID"].ToString();
                            yt.PersonelID = dr["CariID"].ToString();
                            yt.ParaBirimi = dr["ParaBirimi"].ToString();
                            yt.TarihF2 = dr["IslemTarih"].ToString();

                            if (dr["ParaBirimi"].ToString() == "")
                            {
                                yt.ParaBirimi = "TL";
                            }
                          
                            
                            


                            try
                            {
                                yt.IslemTarih = Convert.ToDateTime(dr["IslemTarih"]);
                            }
                            catch (Exception)
                            {

                                yt.IslemTarih = DateTime.Now;
                            }

                            try
                            {
                                yt.VadeTarihi = Convert.ToDateTime(dr["VadeTarihi"]);

                            }
                            catch (Exception)
                            {

                                yt.VadeTarihi = DateTime.Now;
                            }
                       

                            yt.Tutar = dr["Tutar"].ToString();

                            yt.Aciklama = dr["Aciklama"].ToString();
                            yt.ID = dr["ID"].ToString();

                            yonetim.Add(yt);

                        }
                    }
                }
            }
       

            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }



        sayazilimEntities db = new sayazilimEntities();
        [HttpPost]
        public JsonResult YeniDekont(Dekont dk)
        {
            
            Dekont car = null;
            string Message = "Kayıt Eklendi";
            if (dk.ID == -1)
            {
                
                car = new Dekont();
                car = dk;
                if (dk.ParaBirimi == "")
                {
                    car.ParaBirimi = "TL";
                }

                if (dk.IslemTarih == "")
                {
                    car.IslemTarih = DateTime.Now.ToString("dd.MM.yyyy");
                }
                else
                {
                    car.IslemTarih = Convert.ToDateTime(car.IslemTarih).ToString("dd.MM.yyyy");

                }

                if (dk.VadeTarihi.ToString() == "")
                {
                    car.VadeTarihi = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                }

                car.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());

                car.PersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                car.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                car.Donem = AyarMetot.Donem;
                car.Kur = 1;
                car.TLUSDKUR = 1;
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01"+ firmaid.PadLeft(3,'0');
                car.Company_Code = company_code;


                db.Dekont.Add(car);
                db.SaveChanges();
            }
            else
            {
                car = db.Dekont.Where(x => x.ID == dk.ID).FirstOrDefault<Dekont>();
            
                car.IslemNo = dk.IslemNo;
                car.IslemTipi = dk.IslemTipi;
                car.ParaBirimi = dk.ParaBirimi;
                if (dk.ParaBirimi == "")
                {
                    car.ParaBirimi = "TL";
                }
                car.OzelKodID = dk.OzelKodID;
                if (dk.IslemTarih == "")
                {
                    car.IslemTarih = DateTime.Now.ToString("dd.MM.yyyy");
                }
                else
                {
                    car.IslemTarih = Convert.ToDateTime(car.IslemTarih).ToString("dd.MM.yyyy");

                }

                if (dk.VadeTarihi.ToString() == "")
                {
                    car.VadeTarihi = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                }


                car.Tutar = dk.Tutar;
         
                car.Aciklama = dk.Aciklama;
                car.ID = dk.ID;


                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }

            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetCari(string Prefix)
        {
            int firmaid = Convert.ToInt32(Session["FirmaID"]);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                var distinctCountries1 = db.Cari.Where(x=>x.FirmaID == firmaid).ToList();

                return Json(distinctCountries1.ToList().Distinct(), JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetOzel(string Prefix)
        {
            int firmaid = Convert.ToInt32(Session["FirmaID"]);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                var distinctCountries1 = (from c in db.OzelKod.Where(x=>x.FirmaID == firmaid)
                                          select new { c.KodAdi });

                return Json(distinctCountries1.ToList().Distinct(), JsonRequestBehavior.AllowGet);
            }

        }

        
        [HttpPost]
        public ActionResult DeleteDekont(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                Dekont emp = db.Dekont.Where(x => x.ID == id).FirstOrDefault<Dekont>();
                db.Dekont.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DekontBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                Dekont emp = db.Dekont.Where(x => x.ID == id).FirstOrDefault<Dekont>();
              
                emp.IslemTarih = Convert.ToDateTime(emp.IslemTarih).ToString("yyyy-MM-dd");
                string vadetarihi = Convert.ToDateTime(emp.VadeTarihi).ToString("yyyy-MM-dd");

                Cari cr = db.Cari.Where(x => x.ID == emp.CariID).FirstOrDefault();
                
               
                return Json(new { success = true, data = emp,vadetarihi = vadetarihi }, JsonRequestBehavior.AllowGet);
            
            }
        }



    }
}