using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaTeknopark_MVC5.Models;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Globalization;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class FinansController : Controller
    {
        public ActionResult Kasa()
        {
            ViewBag.KasaIslemID = 0;
            using (SqlConnection con45 = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
            {
                con45.Open();

                using (SqlCommand kasacik = new SqlCommand("select ID from Kasa", con45))
                {
                    using (SqlDataReader dr = kasacik.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                            {
                                conp.Open();
                                using (SqlCommand kasasay = new SqlCommand("BakiyeKasa", conp))
                                {
                                    kasasay.CommandType = CommandType.StoredProcedure;
                                    kasasay.Parameters.AddWithValue("@KasaID", dr["ID"].ToString());
                                    kasasay.Parameters.AddWithValue("@ParaBirimi", "TL");
                                    kasasay.ExecuteNonQuery();
                                }

                                using (SqlCommand kasasay = new SqlCommand("BakiyeKasa", conp))
                                {
                                    kasasay.CommandType = CommandType.StoredProcedure;
                                    kasasay.Parameters.AddWithValue("@KasaID", dr["ID"].ToString());
                                    kasasay.Parameters.AddWithValue("@ParaBirimi", "EUR");
                                    kasasay.ExecuteNonQuery();


                                }
                                using (SqlCommand kasasay = new SqlCommand("BakiyeKasa", conp))
                                {
                                    kasasay.CommandType = CommandType.StoredProcedure;
                                    kasasay.Parameters.AddWithValue("@KasaID", Convert.ToInt32(dr["ID"].ToString()));
                                    kasasay.Parameters.AddWithValue("@ParaBirimi", "USD");
                                    kasasay.ExecuteNonQuery();

                                }

                                using (SqlCommand kasasay = new SqlCommand("BakiyeKasa", conp))
                                {
                                    kasasay.CommandType = CommandType.StoredProcedure;
                                    kasasay.Parameters.AddWithValue("@KasaID", Convert.ToInt32(dr["ID"].ToString()));
                                    kasasay.Parameters.AddWithValue("@ParaBirimi", "GBP");
                                    kasasay.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                }
            }
            // AyarMetot.Siradaki("", "Stok", "StokKodu");
            // ViewBag.StokKoduSiradaki = AyarMetot.GetNumara;


            AyarMetot.Siradaki("", "KASA", "KasaKodu", Session["FirmaID"].ToString());
            ViewBag.KasaKoduSiradaki = AyarMetot.GetNumara;



            return View();
        }

        public ActionResult Banka()
        {
            ViewBag.BankaIslemID = 0;
            using (SqlConnection con45 = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
            {
                con45.Open();

                using (SqlConnection con = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                {
                    con.Open();


                    using (SqlCommand kasacik = new SqlCommand("select ID from Banka", con))
                    {
                        using (SqlDataReader rdr1 = kasacik.ExecuteReader())
                        {
                            while (rdr1.Read())
                            {
                                using (SqlCommand BN = new SqlCommand("BakiyeBanka", con45))
                                {
                                    BN.CommandType = CommandType.StoredProcedure;
                                    BN.Parameters.AddWithValue("@BankaID", rdr1["ID"].ToString());
                                    BN.Parameters.AddWithValue("@ParaBirimi", "TL");
                                    BN.ExecuteNonQuery();
                                }
                                using (SqlCommand BN = new SqlCommand("BakiyeBanka", con45))
                                {
                                    BN.CommandType = CommandType.StoredProcedure;
                                    BN.Parameters.AddWithValue("@BankaID", rdr1["ID"].ToString());
                                    BN.Parameters.AddWithValue("@ParaBirimi", "EUR");
                                    BN.ExecuteNonQuery();
                                }

                                using (SqlCommand BN = new SqlCommand("BakiyeBanka", con45))
                                {
                                    BN.CommandType = CommandType.StoredProcedure;
                                    BN.Parameters.AddWithValue("@BankaID", rdr1["ID"].ToString());
                                    BN.Parameters.AddWithValue("@ParaBirimi", "USD");
                                    BN.ExecuteNonQuery();
                                }

                            }
                        }
                    }
                }
            }



            AyarMetot.Siradaki("", "BANKA", "BankaKodu", Session["FirmaID"].ToString());
            ViewBag.BankaKoduSıradaki = AyarMetot.GetNumara;
            return View();
        }

        public ActionResult FinansHareketleri(int KasaID = -1, int BankaID = -1)
        {
            if (KasaID == 0) KasaID = -1;
            if (BankaID == 0) BankaID = -1;

            ViewBag.BankaIslemID = 0;
            ViewBag.KasaIslemID = 0;
            ViewBag.KasaID = KasaID;
            ViewBag.BankaID = BankaID;
            return View();
        }

        public ActionResult GetKasa()
        {


            string FirmaID = Session["FirmaID"].ToString();

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<Kasa1> yonetim = new List<Kasa1>();
            string sorg = @"SET DATEFORMAT dmy; select ID,KasaKodu,KasaAdi,KayitT,ParaBirimi,coalesce(Bakiye,0 ) as Bakiye,(0) as Bakiye_EUR,(0) as Bakiye_USD,(0) as Bakiye_GBP from Kasa Where FirmaID=" + FirmaID;

            int personelid = Convert.ToInt32(Session["PersonelID"].ToString());
            if (Session["Grubu"].ToString() == "Teknik Servis Kullanıcısı")
            {
                sorg = @"SET DATEFORMAT dmy; select ID,KasaKodu,KasaAdi,KayitT,ParaBirimi,coalesce(Bakiye,0 ) as Bakiye,(0) as Bakiye_EUR,(0) as Bakiye_USD,(0) as Bakiye_GBP from Kasa where ID= " + Session["vKasaID"].ToString() + "And FirmaID =" + FirmaID;
            }
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand kasagetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = kasagetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                            {
                                conp.Open();
                                using (SqlCommand kasasay = new SqlCommand("BakiyeKasa", conp))
                                {
                                    kasasay.CommandType = CommandType.StoredProcedure;
                                    kasasay.Parameters.AddWithValue("@KasaID", dr["ID"].ToString());
                                    kasasay.Parameters.AddWithValue("@ParaBirimi", "TL");
                                    kasasay.ExecuteNonQuery();
                                }

                                using (SqlCommand kasasay = new SqlCommand("BakiyeKasa", conp))
                                {
                                    kasasay.CommandType = CommandType.StoredProcedure;
                                    kasasay.Parameters.AddWithValue("@KasaID", dr["ID"].ToString());
                                    kasasay.Parameters.AddWithValue("@ParaBirimi", "EUR");
                                    kasasay.ExecuteNonQuery();


                                }
                                using (SqlCommand kasasay = new SqlCommand("BakiyeKasa", conp))
                                {
                                    kasasay.CommandType = CommandType.StoredProcedure;
                                    kasasay.Parameters.AddWithValue("@KasaID", Convert.ToInt32(dr["ID"].ToString()));
                                    kasasay.Parameters.AddWithValue("@ParaBirimi", "USD");
                                    kasasay.ExecuteNonQuery();

                                }


                            }


                            Kasa1 yt = new Kasa1();



                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.KasaKodu = dr["KasaKodu"].ToString();
                            //yt.Aciklama = dr["Aciklama"].ToString();
                            yt.KasaAdi = dr["KasaAdi"].ToString();
                            yt.ParaBirimi = dr["ParaBirimi"].ToString();

                            try
                            {
                                yt.Bakiye = Convert.ToDecimal(dr["Bakiye"]);

                                if (yt.ParaBirimi == "USD")
                                {
                                    yt.Bakiye_USD = yt.Bakiye;
                                    yt.Bakiye_EUR = 0;
                                }

                                if (yt.ParaBirimi == "EUR")
                                {
                                    yt.Bakiye_EUR = yt.Bakiye;
                                    yt.Bakiye_USD = 0;
                                }

                                if (yt.ParaBirimi == "TL")
                                {
                                    yt.Bakiye_USD = 0;
                                    yt.Bakiye_EUR = 0;
                                }
                            }
                            catch (Exception e)
                            {

                                yt.Bakiye = 0;
                            }
                            try
                            {
                                yt.KayitT = Convert.ToDateTime(dr["KayitT"]);
                            }
                            catch (Exception)
                            {

                                yt.KayitT = DateTime.Now;
                            }

                            //yt.Bakiye_EUR = Convert.ToDecimal(dr["Bakiye_EUR"]);
                            //yt.Bakiye_USD = Convert.ToDecimal(dr["Bakiye_USD"]);
                            //yt.Bakiye_GBP = Convert.ToDecimal(dr["Bakiye_GBP"]);

                            yonetim.Add(yt);

                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteKasa(int id)
        {
            if (Session["Grubu"].ToString() == "Teknik Servis Kullanıcısı")
            {
                return Json(new { success = false, message = "Yetkiniz Yoktur." }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                using (sayazilimEntities db = new sayazilimEntities())
                {
                    Kasa emp = db.Kasa.Where(x => x.ID == id).FirstOrDefault<Kasa>();
                    if (emp.Merkez == true)
                    {
                        return Json(new { success = false, message = "Merkez Kasa Silinemez" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                        {
                            conp.Open();
                            SqlCommand cmd = new SqlCommand("IslemKontrolKasa", conp);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@KasaID", emp.ID);  // input parameter

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
                                    return Json(new { success = false, message = "Bu kasaya ait " + average.Value.ToString() + " işlem bulunmaktadır.Silinemez" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, message = "Seçili Kasa Bakiyesi 0'dan Büyüktür.İşlemlerinizi Kontrol Ediniz" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                db.Kasa.Remove(emp);
                                db.SaveChanges();
                                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
        }

        sayazilimEntities db = new sayazilimEntities();
        [HttpPost]
        public JsonResult YeniKasa(Kasa dk)
        {
            Kasa car = null;
            string Message = "Kayıt Eklendi";
            var result = new { sonuc = 1, Message = Message };
            int firmaidsrg = Convert.ToInt32(Session["FirmaID"].ToString());


            if (dk.ID == -1)
            {
                var kasalist = db.Kasa.Where(x => x.KasaKodu == dk.KasaKodu && x.FirmaID == firmaidsrg).ToList();
                if (kasalist.Count == 0)
                {
                    car = new Kasa();
                    dk.KayitT = DateTime.Now.ToString("");
                    car = dk;
                    DateTime now = DateTime.Now;
                    dk.KayitT = now.Day.ToString() + "." + now.Month.ToString() + "." + now.Year.ToString();
                    if (dk.ParaBirimi == "")
                    {
                        car.ParaBirimi = "TL";
                    }

                    dk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                    dk.Merkez = false;
                    string firmaid = Session["FirmaID"].ToString();
                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                    dk.Company_Code = company_code;



                    db.Kasa.Add(car);
                    db.SaveChanges();


                    var list = db.Kasa.OrderByDescending(x => x.ID).Take(1).ToList();
                    foreach (var item in list)
                    {
                        decimal tutar = Convert.ToDecimal(dk.Bakiye);
                        Bakiyeekle("KAF", item.ID, tutar, dk.ParaBirimi);
                    }
                }
                else
                {
                    result = new { sonuc = 0, Message = "Aynı Kasa Kodu İle Kayıt Yapamazsınız." };
                }
            }
            else
            {
                car = db.Kasa.Where(x => x.ID == dk.ID).FirstOrDefault<Kasa>();

                car.KasaKodu = dk.KasaKodu;
                car.KasaAdi = dk.KasaAdi;
                car.ParaBirimi = dk.ParaBirimi;
                //car.Aciklama = dk.Aciklama;
                DateTime now = DateTime.Now;
                dk.KayitT = now.Day.ToString() + "." + now.Month.ToString() + "." + now.Year.ToString();
                car.KayitT = dk.KayitT;


                car.ID = dk.ID;


                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }


            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult KasaBilgi(int id)
        {


            ViewBag.KasaIslemID = 0;
            using (sayazilimEntities db = new sayazilimEntities())
            {

                Kasa emp = db.Kasa.Where(x => x.ID == id).FirstOrDefault<Kasa>();
                if (emp != null)
                {
                    ViewBag.KasaTutar = emp.Bakiye.ToString();
                    ViewBag.KasaID = id;
                }

                //if (emp.ParaBirimi == "TL")
                //{
                //    emp.Bakiye = emp.
                //}



                return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult GetFinans(int KasaID = -1, int BankaID = -1)
        {

            string kasa = "";
            string banka = "";

            if (KasaID != -1)
            {
                kasa = " and KasaID='" + KasaID + "'";

            }

            if (BankaID != -1)
            {
                banka = " and BankaID='" + BankaID + "'";

            }


            List<Finans> yonetim = new List<Finans>();

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;


            string firmaid = Session["FirmaID"].ToString();

            string sorg1 = @"set dateformat dmy; select ID,IslemTipi,IslemTuru,IslemNo,AB,OzelKodAdi,Cari,Kasa,Banka,Aciklama,KrediKart,Tutar,PB,PersonelID,Tarih,Personel,Tablo, (0) as Giris,(0) as Cikis,(0) as Bakiye from TAHO   Where ID IS NOT NULL" + kasa + banka + " And FirmaID=" + firmaid + " ";

            string sorg = sorg1 + " ORDER BY ID ";

            
            try
            {
                decimal GBakiye = 0;
                using (SqlConnection con = new SqlConnection(strcon))
                {

                    con.Open();
                    using (SqlCommand kasagetir = new SqlCommand(sorg, con))

                    {

                        using (SqlDataReader dr = kasagetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Finans yt = new Finans();

                                if (dr["IslemTipi"].ToString() != "CAF" && dr["IslemTipi"].ToString() != "CBF")
                                {
                                    try
                                    {
                                        yt.IslemTuru = dr["IslemTuru"].ToString();
                                        yt.IslemNo = dr["IslemNo"].ToString();
                                        yt.ID = dr["ID"].ToString();
                                        yt.Cari = dr["Cari"].ToString();
                                        string tutarkisa = dr["Tutar"].ToString()
                                            .Substring(0, dr["Tutar"].ToString().Length - 4);
                                        //yt.Aciklama = dr["Aciklama"].ToString();
                                        yt.Kasa = dr["Kasa"].ToString();
                                        yt.Banka = dr["Banka"].ToString();
                                        yt.KrediKart = dr["KrediKart"].ToString();
                                        yt.YerelTutar = tutarkisa;
                                        yt.Personel = dr["Personel"].ToString();
                                        yt.OzelKodAdi = "";
                                        yt.IslemTipi = dr["IslemTipi"].ToString();

                                        if (dr["AB"].ToString() == "(A)")
                                        {
                                            yt.Cikis = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                                            yt.Giris = "0.00";
                                            yt.AB = "B";
                                            GBakiye += -Convert.ToDecimal(dr["Tutar"]);
                                           
                                        }
                                        else if (dr["AB"].ToString() == "(B)")
                                        {
                                            yt.Giris = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                                            yt.Cikis = "0.00";
                                            yt.AB = "A";
                                            GBakiye += Convert.ToDecimal(dr["Tutar"]);
                                           
                                        }
                                        if (dr["OzelKodAdi"].ToString() != "") { 
                                            yt.OzelKodAdi = dr["OzelKodAdi"].ToString();
                                        }
                                        yt.Tablo = dr["Tablo"].ToString();
                                        yt.PB = dr["PB"].ToString();
                                        yt.Aciklama = dr["Aciklama"].ToString();
                                        yt.PersonelID = dr["PersonelID"].ToString();
                                        yt.TarihF2 = Convert.ToDateTime(dr["Tarih"]).ToString("dd.MM.yyyy");
                                        yt.ID = dr["ID"].ToString();
                                        yt.Bakiye = GBakiye.ToString("N2");
                                        yonetim.Add(yt);

                                    }
                                    catch (Exception e)
                                    {
                                        System.IO.File.WriteAllText(
                                            Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "alp.xml"),
                                            e.ToString());
                                    }

                                }

                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText(
                    Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "alp.xml"),
                    e.ToString());
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBanka()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            string FirmaID = Session["FirmaID"].ToString();
            List<Banka1> yonetim = new List<Banka1>();
            string sorg = @"SET DATEFORMAT dmy; select ID,BankaKodu,Iban,SubeAdi,HesapNo,KayitT,BankaAdi,hnParaBirimi,coalesce(Bakiye,0 ) as Bakiye,(0) as Bakiye_EUR,(0) as Bakiye_USD,(0) as Bakiye_GBP from Banka where FirmaID =" + FirmaID;

            int personelid = Convert.ToInt32(Session["PersonelID"].ToString());
            if (Session["Grubu"].ToString() == "Teknik Servis Kullanıcısı")
            {
                //sorg = @"SET DATEFORMAT dmy; select ID,BankaKodu,Iban,SubeAdi,HesapNo,KayitT,BankaAdi,hnParaBirimi,coalesce(Bakiye,0 ) as Bakiye,(0) as Bakiye_EUR,(0) as Bakiye_USD,(0) as Bakiye_GBP from Banka where ID= " + Session["vBankaID"].ToString() + " And FirmaID= " + FirmaID;
                sorg = @"SET DATEFORMAT dmy; select ID,BankaKodu,Iban,SubeAdi,HesapNo,KayitT,BankaAdi,hnParaBirimi,coalesce(Bakiye,0 ) as Bakiye,(0) as Bakiye_EUR,(0) as Bakiye_USD,(0) as Bakiye_GBP from Banka where FirmaID= " + FirmaID;
            }




            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand bankagetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = bankagetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Banka1 yt = new Banka1();

                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.BankaKodu = dr["BankaKodu"].ToString();
                            yt.SubeAdi = dr["SubeAdi"].ToString();
                            yt.HesapNo = dr["HesapNo"].ToString();
                            yt.Iban = dr["Iban"].ToString();

                            //yt.Telefon = dr["Telefon"].ToString();
                            //yt.Aciklama = dr["Aciklama"].ToString();
                            yt.BankaAdi = dr["BankaAdi"].ToString();
                            yt.hnParaBirimi = dr["hnParaBirimi"].ToString();
                            try
                            {
                                yt.Bakiye = Convert.ToDecimal(dr["Bakiye"]);
                            }
                            catch (Exception)
                            {

                                yt.Bakiye = 0;
                            }
                            try
                            {
                                yt.KayitT = Convert.ToDateTime(dr["KayitT"]);
                            }
                            catch (Exception)
                            {

                                yt.KayitT = DateTime.Now;
                            }

                            yt.Bakiye_EUR = Convert.ToDecimal(dr["Bakiye_EUR"]);
                            yt.Bakiye_USD = Convert.ToDecimal(dr["Bakiye_USD"]);
                            yt.Bakiye_GBP = Convert.ToDecimal(dr["Bakiye_GBP"]);

                            yonetim.Add(yt);

                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteBanka(int id)
        {
            if (Session["Grubu"].ToString() == "Teknik Servis Kullanıcısı")
            {
                return Json(new { success = false, message = "Yetkiniz Yoktur." }, JsonRequestBehavior.AllowGet);
            }
            else
            {


                using (sayazilimEntities db = new sayazilimEntities())
                {

                    Banka emp = db.Banka.Where(x => x.ID == id).FirstOrDefault<Banka>();

                    if (emp.Merkez == true)
                    {
                        return Json(new { success = false, message = "Merkez Banka Silinemez." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                        {
                            conp.Open();
                            SqlCommand cmd = new SqlCommand("IslemKontrolBanka", conp);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BankaID", emp.ID);  // input parameter

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
                                    return Json(new { success = false, message = "Seçili Bankaya Ait " + average.Value.ToString() + " bulunmaktadır.Silinemez." }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(
                                        new
                                        {
                                            success = false,
                                            message = "Seçili Kasa Bakiyesi 0'dan Büyüktür.İşlemlerinizi Kontrol Ediniz."
                                        }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                db.Banka.Remove(emp);
                                db.SaveChanges();
                                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
        }

        [HttpPost]
        public JsonResult YeniBanka(Banka dk)
        {
            Banka car = null;
            string Message = "Kayıt Eklendi";
            int firmaidsrg = Convert.ToInt32(Session["FirmaID"].ToString());

            var result = new { sonuc = 1, Message = Message };

            if (dk.ID == -1)
            {
                var bankalistesi = db.Banka.Where(x => x.BankaKodu == dk.BankaKodu && x.FirmaID == firmaidsrg).ToList();
                if (bankalistesi.Count == 0)
                {
                    car = new Banka();
                    DateTime now = DateTime.Now;
                    dk.KayitT = now.Day.ToString() + "." + now.Month.ToString() + "." + now.Year.ToString();
                    car = dk;
                    car.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                    car.Merkez = false;
                    string firmaid = Session["FirmaID"].ToString();
                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                    car.Company_Code = company_code;
                    db.Banka.Add(car);
                    db.SaveChanges();
                    var list = db.Banka.OrderByDescending(x => x.ID).Take(1).ToList();
                    foreach (var item in list)
                    {
                        decimal tutar = Convert.ToDecimal(dk.Bakiye);
                        Bakiyeekle("BAF", item.ID, tutar, dk.hnParaBirimi);
                    }
                }
                else
                {
                    result = new { sonuc = 0, Message = "Aynı Banka Kodu İle Kayıt Yapılamaz" };
                }
            }
            else
            {


                car = db.Banka.Where(x => x.ID == dk.ID).FirstOrDefault<Banka>();

                car.BankaKodu = dk.BankaKodu;
                car.BankaAdi = dk.BankaAdi;
                car.hnParaBirimi = dk.hnParaBirimi;
                car.Bakiye = dk.Bakiye;
                car.SubeAdi = dk.SubeAdi;
                car.HesapNo = dk.HesapNo;
                car.Iban = dk.Iban;
                DateTime now = DateTime.Now;
                dk.KayitT = now.Day.ToString() + "." + now.Month.ToString() + "." + now.Year.ToString();
                car.KayitT = dk.KayitT;




                car.ID = dk.ID;


                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }



            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult BankaBilgi(int id)
        {
            ViewBag.BankaIslemID = 0;
            int FirmaID = Convert.ToInt32(Session["FirmaID"].ToString());
            using (sayazilimEntities db = new sayazilimEntities())
            {
                Banka emp = db.Banka.Where(x => x.ID == id).FirstOrDefault<Banka>();
                if (emp != null)
                {
                    ViewBag.BankaID = emp.ID;
                    ViewBag.BankaBakiye = emp.Bakiye.ToString();
                }

                return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);

            }
        }

        private void Bakiyeekle(string Tipi, int cID, decimal tutar = 0, string parabirimi = "TL")
        {
            using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
            {
                conp.Open();
                using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from CASH_paY", conp))
                {
                    using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                    {
                        DataSet ds1 = new DataSet();
                        da.Fill(ds1, "CASH_paY");

                        DataRow dt = ds1.Tables["CASH_paY"].NewRow();

                        dt["IslemTipi"] = Tipi;
                        dt["IslemNo"] = cID.ToString() + Tipi;

                        try
                        {
                            using (SqlConnection conn = new SqlConnection(AyarMetot.conString))
                            {
                                conn.Open();
                                using (SqlCommand cu = new SqlCommand("SELECT MAx(ID)+1 FROM CASH_PAY", conn))
                                {
                                    if (cu.ExecuteScalar() != DBNull.Value) dt["IslemNo"] = "CB" + cu.ExecuteScalar().ToString();
                                    else dt["IslemNo"] = "CB1";
                                }

                            }
                        }
                        catch { }

                        dt["IslemTarih"] = DateTime.Now.Date;
                        dt["CariID"] = -1;
                        dt["PersonelID"] = AyarMetot.PersonelID;
                        if (Tipi == "KAF")
                        {
                            dt["KasaID"] = cID;
                            dt["BankaID"] = -1;
                        }
                        else
                        {
                            dt["BankaID"] = cID;
                            dt["KasaID"] = -1;

                        }

                        dt["BankaID"] = -1;
                        dt["KrediKartiID"] = -1;
                        dt["TaksitSayisi"] = -1;
                        dt["OzelKodID"] = -1;
                        dt["Aciklama"] = "Cari Bakiye Açılışı Yapıldı";
                        dt["ParaBirimi"] = parabirimi;
                        if (parabirimi == "")
                        {
                            dt["ParaBirimi"] = "TL";
                        }

                        dt["Tutar"] = tutar;

                        dt["gTutar"] = 0;
                        dt["gParaBirimi"] = "";
                        dt["aTutar"] = 0;
                        dt["aParaBirimi"] = "";
                        dt["exRate"] = 1;
                        dt["gonderenID"] = -1;
                        dt["gonderenType"] = "";
                        dt["alanID"] = -1;
                        dt["alanType"] = "";
                        dt["Donem"] = AyarMetot.Donem;
                        dt["AlanCariID"] = -1;
                        dt["HavaleMasrafID"] = -1;
                        dt["AdisyonTahsilatID"] = -1;
                        dt["KayitPersonelID"] = AyarMetot.PersonelID;
                        dt["KayitTarih"] = DateTime.Now;



                        ds1.Tables["CASH_paY"].Rows.Add(dt);
                        da.Update(ds1, "CASH_paY");

                    }
                }
            }
        }


        [HttpPost]
        public ActionResult DeleteFinans(int id, string tablo)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string tabloadi = "";
            using (sayazilimEntities db = new sayazilimEntities())
            {


                string sorg2 = "DELETE FROM " + tablo + " WHERE ID = " + id.ToString();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    conp1.Open();
                    using (SqlCommand cu = new SqlCommand(sorg2, conp1))
                    {
                        cu.ExecuteNonQuery().ToString();
                    }
                }


                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult YeniMasraf(int id = 0)
        {
            AyarMetot.Siradaki("", "MASRAF", "IslemNo", Session["FirmaID"].ToString());
            ViewBag.MasrafSiradakiNo = AyarMetot.GetNumara;
            if (Session["Grubu"].ToString() != "Teknik Servis Kullanıcısı")
            {
                if (id != 0)
                {
                    CASH_PAY cp = db.CASH_PAY.Where(x => x.ID == id).FirstOrDefault<CASH_PAY>();
                    if (cp.IslemTipi != "KAF" && cp.IslemTipi != "BAF")
                    {
                        return View(cp);
                    }
                    else
                    {
                        return RedirectToAction("FinansHareketleri", "Finans");
                    }
                }
                else
                {
                    return View(new CASH_PAY());

                }
            }
            else
            {
                try
                {
                    TECHNICAL tech = db.TECHNICAL.Where(x => x.ID == id).FirstOrDefault<TECHNICAL>();
                    ViewBag.ServisCariID = tech.CariID;
                }
                catch
                {

                }

                try
                {
                    ViewBag.ServisPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                    int idpersonel = Convert.ToInt32(Session["PersonelID"].ToString());
                    Personel pr = db.Personel.Where(x => x.ID == idpersonel).FirstOrDefault<Personel>();
                    ViewBag.PersonelKasaId = pr.vKasaID;
                    ViewBag.PersonelBankaId = pr.vBankaID;

                }
                catch
                {

                }


                return View(new CASH_PAY());

            }

        }

        public ActionResult YeniGelir(int id = 0)
        {
            AyarMetot.Siradaki("", "MASRAF", "IslemNo", Session["FirmaID"].ToString());
            ViewBag.MasrafSiradakiNo = AyarMetot.GetNumara;
            if (Session["Grubu"].ToString() != "Teknik Servis Kullanıcısı")
            {
                if (id != 0)
                {
                    CASH_PAY cp = db.CASH_PAY.Where(x => x.ID == id).FirstOrDefault<CASH_PAY>();
                    if (cp.IslemTipi != "KAF" && cp.IslemTipi != "BAF")
                    {
                        return View(cp);
                    }
                    else
                    {
                        return RedirectToAction("FinansHareketleri", "Finans");
                    }
                }
                else
                {
                    return View(new CASH_PAY());

                }
            }
            else
            {
                try
                {
                    TECHNICAL tech = db.TECHNICAL.Where(x => x.ID == id).FirstOrDefault<TECHNICAL>();
                    ViewBag.ServisCariID = tech.CariID;
                }
                catch
                {

                }

                try
                {
                    ViewBag.ServisPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                    int idpersonel = Convert.ToInt32(Session["PersonelID"].ToString());
                    Personel pr = db.Personel.Where(x => x.ID == idpersonel).FirstOrDefault<Personel>();
                    ViewBag.PersonelKasaId = pr.vKasaID;
                    ViewBag.PersonelBankaId = pr.vBankaID;

                }
                catch
                {

                }


                return View(new CASH_PAY());

            }
        }

        [HttpPost]
        public JsonResult yeniMasraf(
            string IslemTipi,
            string IslemNo,
            int id,
            int OzelKodID,
            string Aciklama,
            string Tutar,
            string ThisKasaID,
            string ThisBankaID,
            string parabirimi,
            string Personel,
            int durum = -1

            )
        {

            var result = new { sonuc = 0, Message = "Eklenmedi", cid = -1 };

            decimal TutarDecimal = decimal.Parse(Tutar, CultureInfo.InvariantCulture);
            CASH_PAY cs = new CASH_PAY();
            if (durum == -1)
            {


                cs.IslemTipi = IslemTipi;
                cs.IslemNo = "1";
                cs.IslemTarih = DateTime.Now.ToString();
                cs.OzelKodID = OzelKodID;
                cs.CariID = -1;

                cs.ParaBirimi = parabirimi;
                if (Personel != "")
                {
                    cs.PersonelID = Convert.ToInt32(Personel);
                }
                else
                {
                    cs.PersonelID = Convert.ToInt32(Session["PersonelID"]);
                }
                cs.KayitPersonelID = 1;
                cs.exRate = 1;
                cs.PrimOr = 0;
                cs.KasaID = -1;
                cs.BankaID = -1;
                cs.KrediKartiID = -1;
                cs.AdisYonTahsilatID = -1;



                cs.OzelKodKdv = -1;

                cs.Aciklama = Aciklama;
                cs.TaksitSayisi = 1;
                cs.ParaBirimi = parabirimi;
                if (parabirimi == "")
                {
                    cs.ParaBirimi = "TL";
                }

                cs.Tutar = TutarDecimal;

                int kasaID = -1;
                int bankaID = -1;
                int kkID = -1;
                if (IslemTipi == "A" || IslemTipi == "B")
                {
                    if (ThisBankaID != "")
                    {
                        bankaID = Convert.ToInt32(ThisBankaID);
                    }
                }
                else if (IslemTipi == "E" || IslemTipi == "M")
                {

                    if (ThisKasaID != "")
                    {
                        kasaID = Convert.ToInt32(ThisKasaID);
                    }

                }




                if (IslemTipi == "E" && IslemTipi == "M")
                {

                    kasaID = Convert.ToInt32(ThisKasaID);
                    bankaID = -1;
                    kkID = -1;

                }
                else if (IslemTipi == "A" || IslemTipi == "B")
                {

                    kasaID = -1;
                    bankaID = Convert.ToInt32(ThisBankaID);
                    kkID = -1;

                }
                cs.KasaID = kasaID;
                cs.BankaID = bankaID;
                cs.KrediKartiID = kkID;
                cs.OzelKodKdv = 0;
                cs.TaksitSayisi = 1;
                cs.ParaBirimi = parabirimi;
                cs.Tutar = TutarDecimal;
                cs.gTutar = 0;
                cs.gParaBirimi = parabirimi;
                cs.aTutar = 0;
                cs.aParaBirimi = "";
                cs.gonderenID = -1;
                cs.gonderenType = "";
                cs.alanID = -1;
                cs.alanType = "";
                cs.AlanCariID = -1;


                cs.KayT = DateTime.Now.ToString();
                cs.DegT = DateTime.Now.ToString();
                cs.Donem = DateTime.Now.Year.ToString();
                cs.HavaleMasrafID = -1;
                cs.CekSenetID = -1;
                cs.CariBankaKID = -1;
                cs.ProjeID = -1;
                cs.SantiyeID = -1;
                cs.AracPlaka = "";
                cs.IslemNo = IslemNo;
                cs.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());

                try
                {
                    cs.Plaka2 = "";
                }
                catch { }

                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                cs.Company_Code = company_code;





                db.CASH_PAY.Add(cs);
                db.SaveChanges();


                int cid = -1;
                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(@"select top (1) ID FROM 
               CASH_PAY where PersonelID=" + Session["PersonelID"] + " Order BY ID Desc", conp1))
                    {
                        cid = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }



                result = new { sonuc = 1, Message = "Kayıt Eklendi", cid = cid };
            }


            else if (durum == 0)
            {
                var tahsilatlist = db.CASH_PAY.Where(x => x.ID == id).ToList();
                foreach (var item in tahsilatlist)
                {
                    item.IslemNo = IslemNo;
                    item.IslemTipi = IslemTipi;
                    item.OzelKodID = OzelKodID;
                    item.Tutar = TutarDecimal;
                    int kasaID = -1;
                    int bankaID = -1;
                    int kkID = -1;
                    if (IslemTipi == "G" || IslemTipi == "KKT" || IslemTipi == "H")
                    {
                        if (ThisBankaID != "")
                        {
                            bankaID = Convert.ToInt32(ThisBankaID);
                            item.BankaID = bankaID;
                        }
                    }
                    else if (IslemTipi == "T" || IslemTipi == "O")
                    {

                        if (ThisKasaID != "")
                        {
                            kasaID = Convert.ToInt32(ThisKasaID);
                            item.KasaID = kasaID;
                        }

                    }

                    item.ParaBirimi = parabirimi;

                }
                int cid = -1;
                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(@"select top (1) ID FROM 
               CASH_PAY where PersonelID=" + Session["PersonelID"] + " Order BY ID Desc", conp1))
                    {
                        cid = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

                db.SaveChanges();
                result = new { sonuc = 1, Message = "Kayıt Güncellendi", cid = cid };




            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}


