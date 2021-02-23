using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaTeknopark_MVC5.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using Newtonsoft.Json;
using System.IO;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class OrderOfferController : Controller
    {
        // GET: OrderOffer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Teklif()
        {
            return View();
        }

        public ActionResult Siparis()
        {
            return View();
        }

        public ActionResult YeniTeklif(int id = 0)
        {
           
            AyarMetot.Siradaki("", "Stok", "StokKodu",Session["FirmaID"].ToString()) ;
            ViewBag.StokKoduSiradaki = AyarMetot.GetNumara;
            AyarMetot.Siradaki("", "Teklif", "TeklifNo", Session["FirmaID"].ToString());
            ViewBag.TeklifKoduSiradaki = AyarMetot.GetNumara;


            AyarMetot.Siradaki("", "Cari", "FirmaKodu", Session["FirmaID"].ToString());
            ViewBag.CariKoduSiradaki = AyarMetot.GetNumara;
            if (id == 0)
            {

                return View(new OFFER());
            }
            else
            {
                var ser = db.OFFER.Where(x => x.ID == id).FirstOrDefault<OFFER>();
                var list = db.OFFER_DETAIL.Where(x => x.teklifID == id).ToList();



                ViewBag.OfferList = list.ToList();

                return View(ser);
            }

        }

        public ActionResult YeniSiparis(int id = 0)
        {
            AyarMetot.Siradaki("", "Stok", "StokKodu", Session["FirmaID"].ToString());
            ViewBag.StokKoduSiradaki = AyarMetot.GetNumara;
            AyarMetot.Siradaki("", "Cari", "FirmaKodu", Session["FirmaID"].ToString());
            ViewBag.CariKoduSiradaki = AyarMetot.GetNumara;
            AyarMetot.Siradaki("", "Siparis", "SiparisNo", Session["FirmaID"].ToString());
            ViewBag.SiparisKoduSiradaki = AyarMetot.GetNumara;

            if (id == 0)
            {

                return View(new ORDERS());
            }
            else
            {
                var ser = db.ORDERS.Where(x => x.ID == id).FirstOrDefault<ORDERS>();
                var list = db.ORDERS_DETAIL.Where(x => x.SiparisID == id).ToList();



                ViewBag.OrderList = list.ToList();

                return View(ser);
            }
        }

        [HttpPost]
        public ActionResult YeniSiparis(ORDERS data, string json, string KdvDh)
        {
           
            int SiparisID = -1;

            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                ORDERS tk = new ORDERS();
                tk = data;

                if (tk.VadeTarihi < tk.IslemTarihi)
                {
                    var result2 = new { sonuc = 2, Message = "Vade Tarihi İşlem Tarihinden Önce Olamaz" };
                    return Json(result2, JsonRequestBehavior.AllowGet);
                }
                else if (tk.KayitTarih < tk.IslemTarihi)
                {
                    var result2 = new { sonuc = 2, Message = "Onay Tarihi İşlem Tarihinden Önce Olamaz" };
                    return Json(result2, JsonRequestBehavior.AllowGet);
                }

                if (data.ParaBirimi == "")
                {
                    tk.ParaBirimi = "TL";
                }

                if (data.VadeTarihi.ToString() == "")
                {
                    tk.VadeTarihi = DateTime.Now;
                }

                if (tk.PersonelID == null)
                {
                    tk.PersonelID = Convert.ToInt32(Session["PersonelID"]);
                }

                if (tk.KayitPersonelID == null)
                {
                    tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"]);
                }
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;

                db.ORDERS.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM ORDERS Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        SiparisID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                ORDERS tk = db.ORDERS.Where(x => x.ID == data.ID).FirstOrDefault<ORDERS>();

                if (data.VadeTarihi < data.IslemTarihi)
                {
                    var result2 = new { sonuc = 2, Message = "Vade Tarihi İşlem Tarihinden Önce Olamaz" };
                    return Json(result2, JsonRequestBehavior.AllowGet);
                }
                else if (data.KayitTarih < data.IslemTarihi)
                {
                    var result2 = new { sonuc = 2, Message = "Onay Tarihi İşlem Tarihinden Önce Olamaz" };
                    return Json(result2, JsonRequestBehavior.AllowGet);
                }

                tk.IslemTipi = data.IslemTipi;
                tk.CariID = data.CariID;
                SiparisID = tk.ID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.CariID = data.CariID;
                tk.PersonelID = data.PersonelID;
                tk.IslemTarihi = data.IslemTarihi;
                tk.VadeTarihi = data.VadeTarihi;
                tk.KayitTarih = data.KayitTarih;
                tk.SiparisNo = data.SiparisNo;
                tk.Aciklama = data.Aciklama;
                tk.Hafta = data.Hafta;



                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            json = "[" + json + "]";
            
            List<ORDERS_DETAIL> items = JsonConvert.DeserializeObject<List<ORDERS_DETAIL>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                ORDERS_DETAIL er = items[i];
     
                decimal Fiyat = Convert.ToDecimal(er.Fiyat);
                decimal Kdv = Convert.ToDecimal(er.KDV);
                try
                {
                    if (er.ID.ToString() == "-1" || er.ID.ToString() == "0")
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from ORDERS_DETAIL", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "ORDERS_DETAIL");
                                    DataRow df = ds.Tables["ORDERS_DETAIL"].NewRow();

                                    df["Durumu"] = "Aktif";
                                    df["SiparisID"] = SiparisID;
                                    try
                                    {
                                        df["IslemTarihi"] = data.IslemTarihi;
                                    }
                                    catch (Exception)
                                    { df["IslemTarihi"] = DateTime.Now; }

                                    df["IslemTuru"] = data.IslemTipi;
                                    df["UrunID"] = er.UrunID;
                                    df["SeriNo"] = "-";
                                    df["depoID"] = Session["vDepoID"];
                                    df["PersonelID"] = Session["PersonelID"];
                                    df["Birim"] = er.Birim;

                                    df["Miktar"] = er.Miktar;
                                    df["gBirimMiktar"] = er.Miktar;
                                    df["BirimAdet"] = 1;

                                    df["Fiyat"] = Fiyat;
                                    df["AdetFiyati"] = Fiyat;

                                    df["mf"] = 0;
                                    df["OIV"] = 0;
                                    if (KdvDh == "D")
                                    {
                                        df["frtFiyat"] = (Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                        df["FtrSonDesc"] = Fiyat;
                                        df["actTutar"] = Fiyat * er.Miktar;
                                        df["Tutar"] = (Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                    }
                                    else
                                    {

                                        df["Tutar"] = Fiyat * er.Miktar;
                                        df["actTutar"] = (Fiyat * er.Miktar) + ((Fiyat * er.Miktar) * Kdv / 100);
                                        df["frtFiyat"] = Fiyat;
                                        df["FtrSonDesc"] = (Fiyat) + ((Fiyat) * Kdv / 100);
                                    }

                                    df["pBirimi"] = AyarMetot.PB_Short;

                                    df["VadeTarih"] = DateTime.Now;

                                    df["IadeSuresi"] = DateTime.Now;


                                    df["KDV"] = Kdv;
                                    df["Tevfikat"] = 0;
                                    df["Variyant"] = "";
                                    df["VariyantSel"] = -1;
                                    df["Iskonta"] = 0;
                                    df["Iskonta2"] = 0;
                                    df["Iskonta3"] = 0;
                                    df["Iskonta4"] = 0;
                                    df["Iskonta5"] = 0;
                                    df["SeriNoList"] = "";
                                    df["Kur"] = 1;

                                    df["FaturaMaliyeti"] = "Otomatik Maliyet";
                                    df["Maliyet"] = 0;
                                    df["GarantiSuresi"] = 0;
                                    df["Donem"] = DateTime.Now.Year;
                                    df["CihazID"] = -1;
                                    df["EkAciklama"] = "";
                                    df["Fparabirimi"] = AyarMetot.PB_Short;
                                    df["SatirOzelKodID"] = -1;
                                    df["BirimAdet"] = 1;
                                    df["FiyatGuncelle"] = 0;
                                    df["IrsaliyedenGecis"] = false;
                                    df["Genislik"] = 1;
                                    df["Uzunluk"] = 1;
                                    df["Derinlik"] = 1;
                                    df["MiktarBasinaOTV"] = 0;

                                    df["MetreMiktarAdet"] = 0;



                                    ds.Tables["ORDERS_DETAIL"].Rows.Add(df);
                                    da.Update(ds, "ORDERS_DETAIL");
                                }
                            }
                        }
                    }
                    else
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da =
                                new System.Data.SqlClient.SqlDataAdapter(
                                    "select * from ORDERS_DETAIL where ID='" + er.ID + "'", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "ORDERS_DETAIL");
                                    DataRow[] adf = ds.Tables["ORDERS_DETAIL"].Select("ID='" + er.ID + "'");
                                    if (adf.Length != 0)
                                    {
                                        DataRow df = adf[0];
                                        df["Durumu"] = "Aktif";
                                        df["SiparisID"] = SiparisID;
                                        try
                                        {
                                            df["IslemTarihi"] = Convert.ToDateTime(data.IslemTarihi);

                                        }
                                        catch (Exception)
                                        {
                                            df["IslemTarihi"] = DateTime.Now;
                                        }


                                        df["IslemTuru"] = data.IslemTipi;
                                        df["UrunID"] = er.UrunID;
                                        df["SeriNo"] = "-";
                                        df["depoID"] = Session["vDepoID"];
                                        df["PersonelID"] = Session["PersonelID"];
                                        df["Birim"] = er.Birim;

                                        df["Miktar"] = er.Miktar;

                                        df["gBirimMiktar"] = er.Miktar;

                                        df["Fiyat"] = Fiyat;
                                        df["AdetFiyati"] = Fiyat;

                                        df["mf"] = 0;
                                        df["OIV"] = 0;
                                        if (KdvDh == "D")
                                        {
                                            df["frtFiyat"] = (Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                            df["FtrSonDesc"] = Fiyat;
                                            df["actTutar"] = Fiyat * er.Miktar;
                                            df["Tutar"] = (Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                        }
                                        else
                                        {

                                            df["Tutar"] = Fiyat * er.Miktar;
                                            df["actTutar"] = (Fiyat * er.Miktar) + ((Fiyat * er.Miktar) * Kdv / 100);
                                            df["frtFiyat"] = Fiyat;
                                            df["FtrSonDesc"] = (Fiyat) + ((Fiyat) * Kdv / 100);
                                        }

                                        df["pBirimi"] = AyarMetot.PB_Short;

                                        try
                                        {
                                            df["VadeTarih"] = DateTime.Now;
                                        }
                                        catch (Exception)
                                        { df["VadeTarih"] = DateTime.Now; }


                                        df["IadeSuresi"] = DateTime.Now;


                                        df["KDV"] = Kdv;
                                        df["Tevfikat"] = 0;
                                        df["Variyant"] = "";
                                        df["VariyantSel"] = -1;
                                        df["Iskonta"] = 0;
                                        df["Iskonta2"] = 0;
                                        df["Iskonta3"] = 0;
                                        df["Iskonta4"] = 0;
                                        df["Iskonta5"] = 0;
                                        df["SeriNoList"] = "";
                                        df["Kur"] = 1;

                                        df["FaturaMaliyeti"] = "Otomatik Maliyet";
                                        df["Maliyet"] = 0;
                                        df["GarantiSuresi"] = 0;
                                        df["Donem"] = DateTime.Now.Year;
                                        df["CihazID"] = -1;
                                        df["EkAciklama"] = "";
                                        df["Fparabirimi"] = AyarMetot.PB_Short;
                                        df["SatirOzelKodID"] = -1;
                                        df["BirimAdet"] = 1;
                                        df["FiyatGuncelle"] = 0;
                                        df["IrsaliyedenGecis"] = false;
                                        df["Genislik"] = 1;
                                        df["Uzunluk"] = 1;
                                        df["Derinlik"] = 1;
                                        df["MiktarBasinaOTV"] = 0;

                                        df["MetreMiktarAdet"] = 0;



                                        df["BirimAdet"] = 1;

                                        da.Update(ds, "ORDERS_DETAIL");

                                    }
                                }
                            }
                        }
                    }


                }
                catch (Exception E1)
                {


                    try
                    {
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\ilhan\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }
            try
            {
                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    conp1.Open();

                    if (KdvDh == "D")
                    {
                        using (SqlCommand d6 = new SqlCommand(@"  update ORDERS SET 
                        GenelToplam=(Select coalesce( SUM(Miktar*Fiyat),0) from ORDERS_DETAIL WHERE SiparisID=ORDERS.ID)
                        ,NetToplam=(Select coalesce( SUM((Miktar*Fiyat) / ((1+ (Convert(decimal(18,2),kdv)/100)))),0) from ORDERS_DETAIL WHERE SiparisID=ORDERS.ID) ,
                        ToplamKdv=(Select (coalesce( SUM((Miktar*Fiyat*Convert(decimal(18,2),kdv)) / (100+ (Convert(decimal(18,2),kdv)))),0)) from ORDERS_DETAIL WHERE SiparisID=ORDERS.ID)
                        where ID=" + SiparisID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand d6 = new SqlCommand(@"  update ORDERS SET 
                        netToplam=(Select coalesce( SUM(Miktar*Fiyat),0) from ORDERS_DETAIL WHERE SiparisID=ORDERS.ID)
                        ,GenelToplam=(Select coalesce( SUM((Miktar*Fiyat)+(Miktar*Fiyat)*(Convert(decimal(18,2),kdv)/100)),0)from ORDERS_DETAIL WHERE SiparisID=ORDERS.ID) ,
                        ToplamKdv=(Select coalesce( SUM((Miktar*Fiyat)*(Convert(decimal(18,2),kdv)/100)),0) from ORDERS_DETAIL WHERE SiparisID=ORDERS.ID)

                         where ID=" + SiparisID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }

                    }

                    using (SqlCommand d6 = new SqlCommand("update ORDERS SET brutToplam=netToplam,YerelTutar=GenelToplam where ID=" + SiparisID, conp1))
                    {
                        d6.ExecuteNonQuery();
                    }
                }
            }
            catch { }

            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetOffer()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<Teklif> yonetim = new List<Teklif>();
            string sorg = @"select (select CariUnvan from Cari where ID=CariID) as CariUnvan,(select Adi + ' '+ Soyadi from Personel where ID=PersonelID) as Personel,* from OFFER where FirmaID="+Session["FirmaID"].ToString();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Teklif yt = new Teklif();


                            yt.Durumu = dr["Durumu"].ToString();
                            yt.IslemTipi = Convert.ToString(dr["IslemTipi"]);
                            yt.TeklifNo = dr["TeklifNo"].ToString();
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
                                yt.TeklifSonTarihi = Convert.ToDateTime(dr["TeklifSonTarihi"]);
                            }
                            catch (Exception)
                            {

                                yt.TeklifSonTarihi = DateTime.Now;
                            }

                            try
                            {
                                yt.CariUnvan = dr["CariUnvan"].ToString();
                            }
                            catch (Exception)
                            { yt.CariUnvan = ""; }
                           
                            try
                            {
                                yt.YerelTutar = Convert.ToDecimal(Convert.ToDecimal(dr["YerelTutar"].ToString()).ToString("N2"));
                            }
                            catch (Exception e1)
                            {
                                yt.YerelTutar = 0;
                            }

                            try
                            {
                                yt.Personel = dr["Personel"].ToString();
                            }
                            catch { yt.Personel = ""; }

                            yt.Aciklama = Convert.ToString(dr["Aciklama"]);
                            yt.ID = Convert.ToInt32(dr["ID"].ToString());

                            yonetim.Add(yt);


                        }
                    }
                }
            }


            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOfferDetail(int id)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<TEKLIF_SATIR> yonetim = new List<TEKLIF_SATIR>();
            string sorg = @"Select * from OFFER_DETAIL WHERE TeklifID='" + id + "'";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TEKLIF_SATIR yt = new TEKLIF_SATIR();


                            yt.Birim = dr["Birim"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.Miktar = Convert.ToDecimal(dr["Miktar"].ToString());
                            yt.Fiyat = Convert.ToDecimal(dr["Fiyat"].ToString());
                            yt.KDV = Convert.ToInt32(dr["Kdv"].ToString());
                            yt.UrunID = Convert.ToInt32(dr["UrunID"].ToString());
                            try
                            {
                                yt.Tutar = Convert.ToDecimal(dr["Tutar"]);
                            }
                            catch (Exception)
                            {

                                yt.Tutar = 0;
                            }


                            using (SqlConnection cons = new SqlConnection(strcon))
                            {
                                cons.Open();
                                using (SqlCommand ss = new SqlCommand("SELECT UrunAdi from STOK WHERE ID='" + dr["UrunID"] + "'", cons))
                                {
                                    using (SqlDataReader ds = ss.ExecuteReader())
                                    {
                                        while (ds.Read())
                                        {
                                            yt.UrunAdi = ds["UrunAdi"].ToString();
                                        }
                                    }
                                }
                            }
                            yonetim.Add(yt);


                        }
                    }
                }
            }


            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }



        sayazilimEntities db = new sayazilimEntities();
        [HttpPost]
        public JsonResult YeniTeklif(OFFER data, string json, string KdvDh)
        {
           
            int TeklifID = -1;

            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                OFFER tk = new OFFER();
                tk = data;

                if (tk.VadeTarihi < tk.IslemTarihi)
                {
                    var result2 = new { sonuc = 2, Message = "Vade Tarihi İşlem Tarihinden Önce Olamaz" };
                    return Json(result2, JsonRequestBehavior.AllowGet);
                }
                else if (tk.KayitTarih < tk.IslemTarihi)
                {
                    var result2 = new { sonuc = 2, Message = "Onay Tarihi İşlem Tarihinden Önce Olamaz" };
                    return Json(result2, JsonRequestBehavior.AllowGet);
                }

                if (data.ParaBirimi == "")
                {
                    tk.ParaBirimi = "TL";
                }

                if (data.VadeTarihi.ToString() == "")
                {
                    tk.VadeTarihi = DateTime.Now;
                }

                if (tk.PersonelID == null)
                {
                    tk.PersonelID = Convert.ToInt32(Session["PersonelID"]);


                }

                if (tk.KayitPersonelID == null)
                {
                    tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"]);


                }

                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                db.OFFER.Add(tk);
                db.SaveChanges();



                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM OFFER Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        TeklifID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                OFFER tk = db.OFFER.Where(x => x.ID == data.ID).FirstOrDefault<OFFER>();



                try
                {
                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Hatice\Appdata\Local\Sayazilim", "sonuç.xml"), json.ToString());
                }
                catch
                {
                }
            


            if (data.VadeTarihi < data.IslemTarihi)
                {
                    var result2 = new { sonuc = 2, Message = "Vade Tarihi İşlem Tarihinden Önce Olamaz" };
                    return Json(result2, JsonRequestBehavior.AllowGet);
                }
                else if (data.KayitTarih < data.IslemTarihi)
                {
                    var result2 = new { sonuc = 2, Message = "Onay Tarihi İşlem Tarihinden Önce Olamaz" };
                    return Json(result2, JsonRequestBehavior.AllowGet);
                }
                

                tk.IslemTipi = data.IslemTipi;
                tk.CariID = data.CariID;
                TeklifID = tk.ID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.CariID = data.CariID;
                tk.PersonelID = data.PersonelID;
                tk.IslemTarihi = data.IslemTarihi;
                tk.VadeTarihi = data.VadeTarihi;
                tk.KayitTarih = data.KayitTarih;
                tk.TeklifNo = data.TeklifNo;
                tk.Aciklama = data.Aciklama;
                tk.TOdemeSekli = data.TOdemeSekli;
                tk.TeklifSuresi = data.TeklifSuresi;
                tk.Teslimat = data.Teslimat;

                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }

            json = "[" + json + "]";


            List<OFFER_DETAIL> items = JsonConvert.DeserializeObject<List<OFFER_DETAIL>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                OFFER_DETAIL er = items[i];
                decimal Fiyat = Convert.ToDecimal(er.Fiyat);
                decimal Kdv = Convert.ToDecimal(er.KDV);
                try
                {
                    if (er.ID.ToString() == "-1" || er.ID.ToString() == "0")
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from OFFER_DETAIL", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "OFFER_DETAIL");
                                    DataRow df = ds.Tables["OFFER_DETAIL"].NewRow();

                                    df["Durumu"] = "Aktif";
                                    df["teklifID"] = TeklifID;
                                    try
                                    {
                                        df["IslemTarihi"] = data.IslemTarihi;
                                    }
                                    catch (Exception)
                                    { df["IslemTarihi"] = DateTime.Now; }

                                    df["IslemTuru"] = data.IslemTipi;
                                    df["UrunID"] = er.UrunID;
                                    df["SeriNo"] = "-";
                                    df["depoID"] = Session["vDepoID"];
                                    df["PersonelID"] = Session["PersonelID"];
                                    df["Birim"] = er.Birim;

                                    df["Miktar"] = er.Miktar;
                                    df["gBirimMiktar"] = er.Miktar;
                                    df["BirimAdet"] = 1;

                                    df["Fiyat"] = Fiyat;
                                    df["AdetFiyati"] = Fiyat;

                                    df["mf"] = 0;
                                    df["OIV"] = 0;
                                    if (KdvDh == "D")
                                    {
                                        df["frtFiyat"] = (Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                        df["FtrSonDesc"] = Fiyat;
                                        df["actTutar"] = Fiyat * er.Miktar;
                                        df["Tutar"] = (Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                    }
                                    else
                                    {

                                        df["Tutar"] = Fiyat * er.Miktar;
                                        df["actTutar"] = (Fiyat * er.Miktar) + ((Fiyat * er.Miktar) * Kdv / 100);
                                        df["frtFiyat"] = Fiyat;
                                        df["FtrSonDesc"] = (Fiyat) + ((Fiyat) * Kdv / 100);
                                    }

                                    df["pBirimi"] = AyarMetot.PB_Short;

                                    df["VadeTarih"] = DateTime.Now;

                                    df["IadeSuresi"] = DateTime.Now;


                                    df["KDV"] = Kdv;
                                    df["Tevfikat"] = 0;
                                    df["Variyant"] = "";
                                    df["VariyantSel"] = -1;
                                    df["Iskonta"] = 0;
                                    df["Iskonta2"] = 0;
                                    df["Iskonta3"] = 0;
                                    df["Iskonta4"] = 0;
                                    df["Iskonta5"] = 0;
                                    df["SeriNoList"] = "";
                                    df["Kur"] = 1;

                                    df["FaturaMaliyeti"] = "Otomatik Maliyet";
                                    df["Maliyet"] = 0;
                                    df["GarantiSuresi"] = 0;
                                    df["Donem"] = DateTime.Now.Year;
                                    df["CihazID"] = -1;
                                    df["EkAciklama"] = "";
                                    df["Fparabirimi"] = AyarMetot.PB_Short;
                                    df["SatirOzelKodID"] = -1;
                                    df["BirimAdet"] = 1;
                                    df["FiyatGuncelle"] = 0;
                                    df["IrsaliyedenGecis"] = false;
                                    df["Genislik"] = 1;
                                    df["Uzunluk"] = 1;
                                    df["Derinlik"] = 1;
                                    df["MiktarBasinaOTV"] = 0;

                                    df["MetreMiktarAdet"] = 0;



                                    ds.Tables["OFFER_DETAIL"].Rows.Add(df);
                                    da.Update(ds, "OFFER_DETAIL");
                                }
                            }
                        }
                    }
                    else
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da =
                                new System.Data.SqlClient.SqlDataAdapter(
                                    "select * from OFFER_DETAIL where ID='" + er.ID + "'", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "OFFER_DETAIL");
                                    DataRow[] adf = ds.Tables["OFFER_DETAIL"].Select("ID='" + er.ID + "'");
                                    if (adf.Length != 0)
                                    {
                                        DataRow df = adf[0];
                                        df["Durumu"] = "Aktif";
                                        df["TeklifID"] = TeklifID;
                                        try
                                        {
                                            df["IslemTarihi"] = Convert.ToDateTime(data.IslemTarihi);

                                        }
                                        catch (Exception)
                                        {
                                            df["IslemTarihi"] = DateTime.Now;
                                        }


                                        df["IslemTuru"] = data.IslemTipi;
                                        df["UrunID"] = er.UrunID;
                                        df["SeriNo"] = "-";
                                        df["depoID"] = Session["vDepoID"];
                                        df["PersonelID"] = Session["PersonelID"];
                                        df["Birim"] = er.Birim;

                                        df["Miktar"] = er.Miktar;

                                        df["gBirimMiktar"] = er.Miktar;

                                        df["Fiyat"] = Fiyat;
                                        df["AdetFiyati"] = Fiyat;

                                        df["mf"] = 0;
                                        df["OIV"] = 0;
                                        if (KdvDh == "D")
                                        {
                                            df["frtFiyat"] = (Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                            df["FtrSonDesc"] = Fiyat;
                                            df["actTutar"] = Fiyat * er.Miktar;
                                            df["Tutar"] = (Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                        }
                                        else
                                        {

                                            df["Tutar"] = Fiyat * er.Miktar;
                                            df["actTutar"] = (Fiyat * er.Miktar) + ((Fiyat * er.Miktar) * Kdv / 100);
                                            df["frtFiyat"] = Fiyat;
                                            df["FtrSonDesc"] = (Fiyat) + ((Fiyat) * Kdv / 100);
                                        }

                                        df["pBirimi"] = AyarMetot.PB_Short;

                                        try
                                        {
                                            df["VadeTarih"] = DateTime.Now;
                                        }
                                        catch (Exception)
                                        { df["VadeTarih"] = DateTime.Now; }


                                        df["IadeSuresi"] = DateTime.Now;


                                        df["KDV"] = Kdv;
                                        df["Tevfikat"] = 0;
                                        df["Variyant"] = "";
                                        df["VariyantSel"] = -1;
                                        df["Iskonta"] = 0;
                                        df["Iskonta2"] = 0;
                                        df["Iskonta3"] = 0;
                                        df["Iskonta4"] = 0;
                                        df["Iskonta5"] = 0;
                                        df["SeriNoList"] = "";
                                        df["Kur"] = 1;

                                        df["FaturaMaliyeti"] = "Otomatik Maliyet";
                                        df["Maliyet"] = 0;
                                        df["GarantiSuresi"] = 0;
                                        df["Donem"] = DateTime.Now.Year;
                                        df["CihazID"] = -1;
                                        df["EkAciklama"] = "";
                                        df["Fparabirimi"] = AyarMetot.PB_Short;
                                        df["SatirOzelKodID"] = -1;
                                        df["BirimAdet"] = 1;
                                        df["FiyatGuncelle"] = 0;
                                        df["IrsaliyedenGecis"] = false;
                                        df["Genislik"] = 1;
                                        df["Uzunluk"] = 1;
                                        df["Derinlik"] = 1;
                                        df["MiktarBasinaOTV"] = 0;

                                        df["MetreMiktarAdet"] = 0;



                                        df["BirimAdet"] = 1;

                                        da.Update(ds, "OFFER_DETAIL");

                                    }
                                }
                            }
                        }
                    }


                }
                catch (Exception E1)
                {


                    try
                    {
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\ilhan\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }

            try
            {
                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    conp1.Open();

                    if (KdvDh == "D")
                    {
                        using (SqlCommand d6 = new SqlCommand(@"  update OFFER SET 
                        GenelToplam=(Select coalesce( SUM(Miktar*Fiyat),0) from OFFER_DETAIL WHERE SiparisID=ORDERS.ID)
                        ,NetToplam=(Select coalesce( SUM((Miktar*Fiyat) / ((1+ (Convert(decimal(18,2),kdv)/100)))),0) from  OFFER_DETAIL WHERE teklifID=OFFER.ID) ,
                        ToplamKdv=(Select (coalesce( SUM((Miktar*Fiyat*Convert(decimal(18,2),kdv)) / (100+ (Convert(decimal(18,2),kdv)))),0)) from OFFER_DETAIL WHERE teklifID=OFFER.ID)
                        where ID=" + TeklifID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand d6 = new SqlCommand(@"  update OFFER SET 
                        netToplam=(Select coalesce( SUM(Miktar*Fiyat),0) from OFFER_DETAIL WHERE teklifID=OFFER.ID)
                        ,GenelToplam=(Select coalesce( SUM((Miktar*Fiyat)+(Miktar*Fiyat)*(Convert(decimal(18,2),kdv)/100)),0)from OFFER_DETAIL WHERE teklifID=OFFER.ID) ,
                        ToplamKdv=(Select coalesce( SUM((Miktar*Fiyat)*(Convert(decimal(18,2),kdv)/100)),0) from OFFER_DETAIL WHERE teklifID=OFFER.ID)

                         where ID=" + TeklifID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }

                    }

                    using (SqlCommand d6 = new SqlCommand("update OFFER SET brutToplam=netToplam,YerelTutar=GenelToplam where ID=" + TeklifID, conp1))
                    {
                        d6.ExecuteNonQuery();
                    }
                }
            }
            catch { }
            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult GetOrder()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<Siparis> yonetim = new List<Siparis>();
            string sorg = @"select (select CariUnvan from Cari where ID=CariID) as CariUnvan,(select Adi + ' '+ Soyadi from Personel where ID=PersonelID) as Personel,* from ORDERS where FirmaID=" + Session["FirmaID"].ToString();



            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Siparis yt = new Siparis();

                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.Durumu = dr["Durumu"].ToString();
                            yt.IslemTipi = Convert.ToString(dr["IslemTipi"]);
                            yt.SiparisNo = dr["SiparisNo"].ToString();

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
                                yt.CariUnvan = dr["CariUnvan"].ToString();
                            }
                            catch (Exception)
                            { yt.CariUnvan = ""; }
                         
                            try
                            {
                                yt.YerelTutar = Convert.ToDecimal(Convert.ToDecimal(dr["YerelTutar"].ToString()).ToString("N2"));
                            }
                            catch (Exception)
                            { yt.YerelTutar = 0; }

                            try
                            {
                                yt.TeslimEdilenTutar = Convert.ToDecimal(Convert.ToDecimal(dr["TeslimEdilenTutar"].ToString()).ToString("N2"));
                            }
                            catch (Exception)
                            { yt.TeslimEdilenTutar = 0; }

                            try
                            {
                                yt.KalanTutar = Convert.ToInt32(dr["KalanTutar"].ToString());

                            }

                            catch (Exception)
                            {
                                yt.KalanTutar = 0;
                            }
                            try
                            {
                                yt.Personel = dr["Personel"].ToString();
                            }
                            catch (Exception)
                            {
                                yt.Personel = "";
                            }
                            yt.Aciklama = Convert.ToString(dr["Aciklama"]);
                            yt.Hafta = Convert.ToString(dr["Hafta"]);
                            yonetim.Add(yt);


                        }
                    }
                }
            }


            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteSiparis(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE ORDERS_DETAIL  where SiparisID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                }


                ORDERS emp = db.ORDERS.Where(x => x.ID == id).FirstOrDefault<ORDERS>();
                db.ORDERS.Remove(emp);

                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult DeleteTeklif(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE OFFER_DETAIL  where TeklifID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                }


                OFFER emp = db.OFFER.Where(x => x.ID == id).FirstOrDefault<OFFER>();
                db.OFFER.Remove(emp);

                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteTeklifDetay(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE OFFER_DETAIL  where ID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                }


                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteSiparisDetay(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE ORDERS_DETAIL  where ID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                }


                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}