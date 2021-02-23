using SaTeknopark_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class MaasController : Controller
    {
        public ActionResult Maas()
        {
            AyarMetot.Siradaki("", "Maas", "IslemKodu", Session["FirmaID"].ToString());
            ViewBag.SiradakiMaas = AyarMetot.GetNumara;
            return View();
        }


        public class MaasModel
        {
            public string Personel { get; set; }



            public string Tutar { get; set; }
            public string IslemNo { get; set; }
            public DateTime IslemTarihi { get; set; }
            public DateTime VadeTarihi { get; set; }

            public string IslemTipi { get; set; }
            public string AlacakBorcTipi { get; set; }
            public string Aciklama { get; set; }
            public string KodAdi { get; set; }


            public string ID { get; set; }


        }



        [HttpGet]
        public ActionResult GetMaas()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<MaasModel> yonetim = new List<MaasModel>();
            string sorg = @"Select Tutar, IslemNo, IslemTarihi, VadeTarihi, IslemTipi, AlacakBorcTipi ,Aciklama, ID ,(select Adi + ' ' + Soyadi from Personel where ID = SALARYPERSON.OdenenPersonel)as Personel,(select KodAdi from OzelKod where ID = SALARYPERSON.OzelKodID) as KodAdi from SALARYPERSON Where FirmaID = "+FirmaID;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            MaasModel yt = new MaasModel();

                            yt.Personel = dr["Personel"].ToString();
                            yt.KodAdi = dr["KodAdi"].ToString();
                            yt.Tutar = dr["Tutar"].ToString();

                            yt.IslemNo = dr["IslemNo"].ToString();
                            try
                            {
                                yt.IslemTarihi = Convert.ToDateTime(dr["IslemTarihi"]);
                            }
                            catch (Exception)
                            {

                                yt.IslemTarihi = DateTime.Now;
                            }
                            try
                            {
                                yt.VadeTarihi = Convert.ToDateTime(dr["VadeTarihi"]);
                            }
                            catch (Exception)
                            {

                                yt.VadeTarihi = DateTime.Now;
                            }
                           
                            yt.IslemTipi = dr["IslemTipi"].ToString();
                            yt.AlacakBorcTipi = dr["AlacakBorcTipi"].ToString();
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
        public JsonResult YeniMaas(SALARYPERSON dk)
        {
            SALARYPERSON car = null;
            string Message = "Kayıt Eklendi";
            if (dk.ID == -1)
            {
                car = new SALARYPERSON();
                car = dk;
                car.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                car.Company_Code = company_code;
                db.SALARYPERSON.Add(car);
                db.SaveChanges();
            }
            else
            {
              
                car = db.SALARYPERSON.Where(x => x.ID == dk.ID).FirstOrDefault<SALARYPERSON>();
                if (dk.ParaBirimi == "")
                {
                    car.ParaBirimi = "TL";
                }
                car.OdenenPersonel = dk.OdenenPersonel;
                car.OzelKodID = dk.OzelKodID;
                car.IslemNo = dk.IslemNo;
                car.IslemTarihi = dk.IslemTarihi;
                car.VadeTarihi = dk.VadeTarihi;
                car.IslemTipi = dk.IslemTipi;
                car.Tutar = dk.Tutar;
                car.AlacakBorcTipi = dk.AlacakBorcTipi;
                car.Aciklama = dk.Aciklama;
                car.ID = dk.ID;
                car.ParaBirimi = dk.ParaBirimi;

                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }

            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult DeleteMaas(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                SALARYPERSON emp = db.SALARYPERSON.Where(x => x.ID == id).FirstOrDefault<SALARYPERSON>();
                db.SALARYPERSON.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MaasBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
 
                SALARYPERSON emp = db.SALARYPERSON.Where(x => x.ID == id).FirstOrDefault<SALARYPERSON>();
                string vade = Convert.ToDateTime(emp.VadeTarihi).ToString("yyyy-MM-dd");
                string islem = Convert.ToDateTime(emp.IslemTarihi).ToString("yyyy-MM-dd");
        
                   return Json(new { success = true,  data = emp ,vade=vade,islem=islem }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}