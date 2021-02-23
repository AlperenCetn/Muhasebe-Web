using SaTeknopark_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class OzelController : Controller
    {
        public ActionResult Ozel()
        {
            //using (SqlCommand says = new SqlCommand("BALANCEDUZELT", AyarMetot.conString))
            //{
            //    says.CommandType = CommandType.StoredProcedure;
            //    says.Parameters.AddWithValue("@OzelKodID", rdrs1["ID"].ToString());
            //    says.Parameters.AddWithValue("@Donem", "-1");
            //    says.Parameters.AddWithValue("@ParaBirimi", rdrs1["paraBirimi"].ToString());
            //    says.ExecuteNonQuery();
            //}
            AyarMetot.Siradaki("", "Ozel", "Kod", Session["FirmaID"].ToString());
            ViewBag.OzelKodSiradaki = AyarMetot.GetNumara;
            return View();
        }

        public class OzelModel
        {
            public string KodAdi { get; set; }
            public string bakiye { get; set; }
            public string paraBirimi { get; set; }
            public string Borc { get; set; }
            public string Alacak { get; set; }
            public string Aciklama { get; set; }
            public int ID { get; set; }


        }

        [HttpGet]
        public ActionResult GetOzel()
        {
            string FirmaID = Session["FirmaID"].ToString();
            int frmid = Convert.ToInt32(FirmaID);

            var ozellist = db.OzelKod.Where(x => x.FirmaID == frmid).ToList();
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con1 = new SqlConnection(strcon);
            try { 

            foreach (var item in ozellist)
            {
                using (SqlCommand says = new SqlCommand("BALANCEDUZELT", con1))
                {
                    if (con1.State == ConnectionState.Closed) con1.Open();
                    says.CommandType = CommandType.StoredProcedure;
                    says.Parameters.AddWithValue("@OzelKodID", item.ID);
                    says.Parameters.AddWithValue("@Donem", "-1");
                    says.Parameters.AddWithValue("@ParaBirimi", item.paraBirimi);
                    says.ExecuteNonQuery();
                }
            }
            }
            catch { }



            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    using (SqlCommand carigetir = new SqlCommand(@"Select top 1 * from OZELKOD WHERE ID NOT IN (SELECT OZELKODID FROM KOD_BALANCE) and FirmaID= " + FirmaID, con))
                    {
                        using (SqlDataReader dr = carigetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                while (dr.Read())
                                {
                                    using (SqlConnection con2 = new SqlConnection(strcon))
                                    {
                                        if (con2.State == ConnectionState.Closed) con2.Open();
                                        using (SqlCommand says = new SqlCommand("BALANCEDUZELT", con2))
                                        {
                                            says.CommandType = CommandType.StoredProcedure;
                                            says.Parameters.AddWithValue("@OzelKodID", dr["ID"].ToString());
                                            says.Parameters.AddWithValue("@Donem", "-1");
                                            says.Parameters.AddWithValue("@ParaBirimi", dr["paraBirimi"].ToString());
                                            says.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception e1)
            { System.IO.File.WriteAllText(Path.Combine(@"C:\Users\ilhan\AppData\Local\Sayazilim", "sonuç.xml"), e1.ToString()); }





            List<OzelModel> yonetim = new List<OzelModel>();
            string sorg = @"Select KodAdi,Aciklama,KOD_BALANCE.bakiye,KOD_BALANCE.paraBirimi,Borc, Alacak ,OzelKod.ID from OzelKod,KOD_BALANCE WHERE ozelKodID=OzelKod.ID and OzelKod.FirmaID= " + FirmaID;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand ozelgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = ozelgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            OzelModel yt = new OzelModel();
                            yt.KodAdi = dr["KodAdi"].ToString();
                            yt.Borc = dr["Borc"].ToString();
                            yt.bakiye = dr["bakiye"].ToString();
                            yt.paraBirimi = dr["paraBirimi"].ToString();
                            yt.Alacak = dr["Alacak"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();

                            yt.ID = Convert.ToInt32(dr["ID"]);

                            yonetim.Add(yt);


                        }
                    }
                }
            }


            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }
        sayazilimEntities db = new sayazilimEntities();
        [HttpPost]
        public JsonResult YeniOzel(OzelKod dk)
        {
            OzelKod car = null;
            string Message = "Kayıt Eklendi";
            if (dk.ID == -1)
            {
                car = new OzelKod();
                car = dk;
                car.GrupAdi = dk.KodAdi;
                if (dk.ustKodID == null)
                {
                    car.ustKodID = -1;
                }
                car.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                car.Company_Code = company_code;




                db.OzelKod.Add(car);


                db.SaveChanges();

                OzelKod car2 = db.OzelKod.OrderByDescending(x => x.ID).FirstOrDefault<OzelKod>();
                KOD_BALANCE kd = new KOD_BALANCE();
                kd.paraBirimi = "TL";
                kd.Borc = 0;
                kd.Alacak = 0;
                kd.bakiye = 0;
                kd.ozelKodID = car2.ID;
                kd.FirmaID = -1;
                kd.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                firmaid = Session["FirmaID"].ToString();
                company_code = "SA01" + firmaid.PadLeft(3, '0');
                kd.Company_Code = company_code;
                db.KOD_BALANCE.Add(kd);
                db.SaveChanges();

            }
            else
            {
                car = db.OzelKod.Where(x => x.ID == dk.ID).FirstOrDefault<OzelKod>();

                car.KodAdi = dk.KodAdi;
                car.Kod = dk.Kod;
                car.paraBirimi = dk.paraBirimi;
                car.Kdv = dk.Kdv;
                car.ustKodID = dk.ustKodID;
                car.Aciklama = dk.Aciklama;
                car.ID = dk.ID;




                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }

            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult OzelDelete(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {

                OzelKod emp = db.OzelKod.Where(x => x.ID == id).FirstOrDefault<OzelKod>();

                using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                {
                    conp.Open();
                    SqlCommand cmd = new SqlCommand("IslemKontrolOKod", conp);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OzelKodID", emp.ID);  // input parameter

                    // output parameter
                    SqlParameter average = new SqlParameter("@deger", SqlDbType.NVarChar, 50);
                    average.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(average);

                    // return value
                    SqlParameter count = cmd.CreateParameter();
                    count.Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(count);


                    cmd.ExecuteNonQuery();

                    if (Convert.ToInt32(count.Value) != 0)
                    {
                        if (average.Value.ToString() == "İşlem")
                        {
                            return Json(new { success = false, message = "Seçili Özel Koda Ait " + average.Value.ToString() + " bulunmaktadır" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, message = "Seçili Özel Kodun Bakiyesi 0'dan Büyüktür.İşlemlerinizi Kontrol Ediniz" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        db.OzelKod.Remove(emp);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
        }

        public ActionResult OzelBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                OzelKod emp = db.OzelKod.Where(x => x.ID == id).FirstOrDefault<OzelKod>();


                return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult OzelKodHareketler(int id)
        {
            Session["OzelKodID"] = id;
            
            return View();
        }

        [HttpGet]
        public ActionResult GetOzelKodList()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            int ozelkodid = Convert.ToInt32(Session["OzelKodID"].ToString());
            List<OzelKodHareketler> yonetim = new List<OzelKodHareketler>();
            string FirmaID = Session["FirmaID"].ToString();
            string sorg = @"set dateformat dmy;select FaturaType,IslemNo,Tarih,Vade,(Select KodAdi from OzelKod o Where o.ID = kd.OzelKodID) as OzelKodAdi,

(select CariUnvan from Cari c where c.ID = kd.CariID)as Firma,Tutar,paraBirimi,

(select (p.Adi + p.Soyadi) as Personel from Personel p where p.ID = kd.personelID) as Personel

from KOD_HAREKETLERI kd where kd.OzelKodID =" + ozelkodid;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand dekontgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = dekontgetir.ExecuteReader())
                    {
                        int i = 1;
                        while (dr.Read())
                        {


                            OzelKodHareketler yt = new OzelKodHareketler();
                            yt.Counter = i.ToString();
                            yt.IslemBilgisi = dr["FaturaType"].ToString();
                            yt.IslemNumarasi = dr["IslemNo"].ToString();
                            yt.Tarih = Convert.ToDateTime(dr["Tarih"].ToString()).ToString("dd.MM.yyyy");
                            yt.VadeTarihi = Convert.ToDateTime(dr["Vade"].ToString()).ToString("dd.MM.yyyy");
                            if (dr["Firma"].ToString() != null)
                            {
                                yt.Firma = dr["Firma"].ToString();
                            }
                            else
                            {
                                yt.Firma = "";
                            }

                            yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                            yt.PB = dr["paraBirimi"].ToString();
                            yt.Personel = dr["Personel"].ToString();
                            i++;
                            yonetim.Add(yt);

                        }
                    }
                }
            }


            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }







    }
}