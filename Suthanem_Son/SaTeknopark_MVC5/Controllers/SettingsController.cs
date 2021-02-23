using Newtonsoft.Json;
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
    public class SettingsController : Controller
    {
        COMPANY_DETAIL cr = new COMPANY_DETAIL();
        Ayarlar ay = new Ayarlar();

        sayazilimEntities db = new sayazilimEntities();
        // GET: Settings
        public ActionResult Index()
        {
           

            FirmaDetail fd = new FirmaDetail();
            COMPANY_DETAIL cr = new COMPANY_DETAIL();
            Ayarlar ay = new Ayarlar();
            List<Kategoriler> kat = new List<Kategoriler>();
            List<Kategoriler> carikat = new List<Kategoriler>();
            List<Kategoriler> ServisKat = new List<Kategoriler>();
            List<Kategoriler> FaturaList = new List<Kategoriler>();
            List<CURRENCY_LIST> pbler = new List<CURRENCY_LIST>();
            

            using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand sayarlar = new SqlCommand("Select * From STK_KATEGORI Where FirmaID=" + Session["FirmaID"].ToString(), bag))
                {
                    using (SqlDataReader dr = sayarlar.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Kategoriler KT = new Kategoriler();
                            KT.ID = Convert.ToInt32(dr["ID"]);
                            KT.Name = dr["Name"].ToString();
                            kat.Add(KT);
                        }
                    }
                }
            }
            using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand sayarlar = new SqlCommand("Select * From SPECIAL_TECH Where FirmaID=" + Session["FirmaID"].ToString(), bag))
                {
                    using (SqlDataReader dr = sayarlar.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Kategoriler KT = new Kategoriler();
                            KT.ID = Convert.ToInt32(dr["ID"]);
                            KT.Name = dr["Name"].ToString();
                            ServisKat.Add(KT);
                        }
                    }
                }
            }
            using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand sayarlar = new SqlCommand("Select * From CARI_KATEGORI Where FirmaID=" + Session["FirmaID"].ToString(), bag))
                {
                    using (SqlDataReader dr = sayarlar.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Kategoriler KT = new Kategoriler();
                            KT.ID = Convert.ToInt32(dr["ID"]);
                            KT.Name = dr["Name"].ToString();
                            carikat.Add(KT);
                        }
                    }
                }
            }
            using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand sayarlar = new SqlCommand("Select * From INVOICE_OZEL Where FirmaID=" + Session["FirmaID"].ToString(), bag))
                {
                    using (SqlDataReader dr = sayarlar.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Kategoriler KT = new Kategoriler();
                            KT.ID = Convert.ToInt32(dr["ID"]);
                            KT.Name = dr["Name"].ToString();
                            FaturaList.Add(KT);
                        }
                    }
                }
            }
            using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
            {
                string srg = "Select * From COMPANY_DETAIL where FirmaID=" + Session["FirmaID"].ToString();
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand sayarlar = new SqlCommand(srg, bag))
                {
                    using (SqlDataReader dr = sayarlar.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cr.FirmaAdi = dr["FirmaAdi"].ToString();
                            cr.VergiDairesi = dr["VergiDairesi"].ToString();
                            cr.VergiNo = dr["VergiNo"].ToString();
                            cr.Telefon = dr["Telefon"].ToString();
                            cr.Faks = dr["Faks"].ToString();
                            cr.ePosta = dr["ePosta"].ToString();
                            cr.IlgiliKisi = dr["IlgiliKisi"].ToString();
                            cr.Sehir = dr["Sehir"].ToString();
                            cr.Ilce = dr["Ilce"].ToString();
                            cr.PostaKodu = dr["PostaKodu"].ToString();
                            cr.Adres = dr["Adres"].ToString();
                            cr.SicilNo = dr["SicilNo"].ToString();
                            cr.WebSite = dr["WebSite"].ToString();
                            cr.MersisNo = dr["MersisNo"].ToString();
                            cr.TicaretSicilNo = dr["TicaretSicilNo"].ToString();
                            

                        }
                    }
                }
            }
            using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand sayarlar = new SqlCommand("Select * From Ayarlar where FirmaID=" + Session["FirmaID"].ToString(), bag))
                {
                    using (SqlDataReader dr = sayarlar.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ay.KurumKodu = dr["KurumKodu"].ToString();
                            ay.Entegrator = dr["Entegrator"].ToString();
                            ay.EFatGecis = DateTime.Now;
                            ay.EFatKA = dr["EFatKA"].ToString();
                            ay.EFatSifre = dr["EFatSifre"].ToString();
                            ay.EFatGB = dr["EFatGB"].ToString();
                            ay.FaturaGonderimi = dr["FaturaGonderimi"].ToString();
                            ay.EIrsGB = dr["EIrsGB"].ToString();
                            ay.EIrsKA = dr["EIrsKA"].ToString();
                            ay.EIrsSF = dr["EIrsSF"].ToString();
                            ay.IrsaliyeAutoNo = dr["IrsaliyeAutoNo"].ToString();
                            ay.vrsPBCode = dr["vrsPBCode"].ToString();
                            ay.FPort = Convert.ToInt32(dr["FPort"].ToString());
                            ay.fMail = dr["fMail"].ToString();
                            ay.fMailSender = dr["fMailSender"].ToString();
                            if (dr["fMailSifre"].ToString() != "") { 
                            ay.fMailSifre = KODLA.Ac(dr["fMailSifre"].ToString(), AyarMetot.ilhan_Control);
                            }
                            else
                            {
                                ay.fMailSifre = "";
                            }
                            ay.PostaSunucu = dr["PostaSunucu"].ToString();
                            ay.SmsFirma = dr["SmsFirma"].ToString();
                            if(dr["SMSPass"].ToString() != "") { 
                            ay.SMSPass = KODLA.Ac(dr["SMSPass"].ToString(), AyarMetot.ilhan_Control);
                            }
                            else
                            {
                                ay.SMSPass = "";
                            }
                            ay.SMSSender = dr["SMSSender"].ToString();
                            ay.SMSUser = dr["SMSUser"].ToString();
                            ay.BorcMesajSonu = dr["BorcMesajSonu"].ToString();
                            ay.MesajSonu = dr["MesajSonu"].ToString();
                        }
                    }
                }
            }
            var parabirimi = db.CURRENCY_LIST.Where(x => x.Durumu == true).ToList();
            foreach (var item in parabirimi)
            {   
                 CURRENCY_LIST cl = new CURRENCY_LIST();
                cl.ParaBirimi = item.ParaBirimi;
                cl.ParaBirimiN = item.ParaBirimiN;
                pbler.Add(cl);

            }

            int FirmaID = Convert.ToInt32(Session["FirmaID"].ToString());
            var renklist = db.RENKLER.Where(x => x.FirmaID == FirmaID).ToList();
            ViewBag.RenklerList = renklist.ToList();
            fd.ParaBirimleri = pbler;
            fd.settings = ay;
            fd.detail = cr;
            fd.kat = kat;
            fd.carikat = carikat;
            fd.serviskat = ServisKat;
            fd.FaturaList = FaturaList;
            
            return View(fd);
        }

        [HttpPost]
        public JsonResult UpdateSetting(FirmaDetail dk, Array[] data, Array[] carikat, Array[] serviskat , Array[] faturakat)
        {

            string Message = "Kayıt Eklendi";
            int firmaid = Convert.ToInt32(Session["FirmaID"].ToString());
            Ayarlar car = db.Ayarlar.Where(x => x.FirmaID == firmaid).FirstOrDefault<Ayarlar>();
            COMPANY_DETAIL fr = db.COMPANY_DETAIL.Where(x => x.FirmaID == firmaid).FirstOrDefault<COMPANY_DETAIL>();


            dk.settings.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
            dk.detail.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());

            car.EFatKA = dk.settings.EFatKA;
            car.Entegrator = dk.settings.Entegrator;
            car.FaturaAutoNo = dk.settings.FaturaAutoNo;
            car.EFatSifre = dk.settings.EFatSifre;
            car.EFatGB = dk.settings.EFatGB;
            car.EIrsGB = dk.settings.EIrsGB;
            car.EIrsKA = dk.settings.EIrsKA;
            car.EIrsSF = dk.settings.EIrsSF;
            if (dk.settings.Version != null)
            {
                car.Version = KODLA.Kapa(dk.settings.Version, AyarMetot.ilhan_Control);
            }
            car.KurumKodu = dk.settings.KurumKodu;
            car.Terminalturu = dk.settings.Terminalturu;
            car.FPort = dk.settings.FPort;
            car.fMail = dk.settings.fMail;
            car.fMailSender = dk.settings.fMailSender;
            if(dk.settings.fMailSifre != null) { 
                    car.fMailSifre = KODLA.Kapa(dk.settings.fMailSifre, AyarMetot.ilhan_Control);
            }
            else
            {
                car.fMailSifre = "";
            }
            car.PostaSunucu = dk.settings.PostaSunucu;
            car.SmsFirma = dk.settings.SmsFirma;
            if(dk.settings.SMSPass != null) { 
                    car.SMSPass = KODLA.Kapa(dk.settings.SMSPass, AyarMetot.ilhan_Control);
            }
            else
            {
                car.SMSPass = "";
            }
            car.SMSSender = dk.settings.SMSSender;
            car.SMSUser = dk.settings.SMSUser;
            car.BorcMesajSonu = dk.settings.BorcMesajSonu;
            car.MesajSonu = dk.settings.MesajSonu;
            if(dk.detail.FirmaAdi != null) { 
            fr.FirmaAdi = dk.detail.FirmaAdi;
            }
            else
            {
                fr.FirmaAdi = "";
            }
            fr.VergiDairesi = dk.detail.VergiDairesi;
            fr.Telefon = dk.detail.Telefon;
            fr.IlgiliKisi = dk.detail.IlgiliKisi;
            fr.VergiNo = dk.detail.VergiNo;
            fr.Faks = dk.detail.Faks;
            fr.Sehir = dk.detail.Sehir;
            fr.Ilce = dk.detail.Ilce;
            fr.PostaKodu = dk.detail.PostaKodu;
            fr.Ilce = dk.detail.Ilce;
            fr.Adres = dk.detail.Adres;
            fr.SicilNo = dk.detail.SicilNo;
            fr.WebSite = dk.detail.WebSite;
            fr.MersisNo = dk.detail.MersisNo;
            fr.TicaretSicilNo = dk.detail.TicaretSicilNo;
            string Sifre = "";
            if ( car.SMSPass != "") { 
             Sifre = KODLA.Ac(car.SMSPass, AyarMetot.ilhan_Control);
            }
            
            if (dk.settings.vrsPBName == "")
            {
                car.vrsPBName = "Türk Lirası";
                car.vrsPBCode = "TL";
            }

            else if (dk.settings.vrsPBName == "TL")
            {
                car.vrsPBName = "Türk Lirası";
                car.vrsPBCode = "TL";
            }
            else if (dk.settings.vrsPBName == "EUR")
            {
                car.vrsPBName = "Euro Üye Ülkeler";
                car.vrsPBCode = "EUR";
            }
            else if (dk.settings.vrsPBName == "USD")
            {
                car.vrsPBName = "ABD Doları";
                car.vrsPBCode = "USD";
            }



            for (int i = 0; i < data.Length; i++)
            {
                string ID = "";
                string NAME = "";
                int kolon = 0;
                foreach (var veri in data[i])
                {

                    if (kolon == 0)
                    {
                        ID = veri.ToString();
                    }
                    else if (kolon == 1)
                    {
                        NAME = veri.ToString();
                    }

                    kolon++;
                }
                kolon = 0;

                using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select * from STK_KATEGORI where ID='" + ID + "'", con))
                    {
                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                        {
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1, "STK_KATEGORI");
                            DataRow[] adf = ds1.Tables["STK_KATEGORI"].Select("ID='" + ID + "'");
                            if (adf.Length != 0)
                            {
                                DataRow dr = adf[0];
                                dr["Name"] = NAME;
                                da.Update(ds1, "STK_KATEGORI");

                            }
                        }
                    }
                }
            }
            for (int i = 0; i < carikat.Length; i++)
            {
                string ID = "";
                string NAME = "";
                int kolon = 0;
                foreach (var veri in carikat[i])
                {

                    if (kolon == 0)
                    {
                        ID = veri.ToString();
                    }
                    else if (kolon == 1)
                    {
                        NAME = veri.ToString();
                    }

                    kolon++;
                }
                kolon = 0;

                using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select * from CARI_KATEGORI where ID='" + ID + "'", con))
                    {
                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                        {
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1, "CARI_KATEGORI");
                            DataRow[] adf = ds1.Tables["CARI_KATEGORI"].Select("ID='" + ID + "'");
                            if (adf.Length != 0)
                            {
                                DataRow dr = adf[0];
                                dr["Name"] = NAME;
                                da.Update(ds1, "CARI_KATEGORI");

                            }
                        }
                    }
                }
            }
            for (int i = 0; i < serviskat.Length; i++)
            {
                string ID = "";
                string NAME = "";
                int kolon = 0;
                foreach (var veri in serviskat[i])
                {

                    if (kolon == 0)
                    {
                        ID = veri.ToString();
                    }
                    else if (kolon == 1)
                    {
                        NAME = veri.ToString();
                    }

                    kolon++;
                }
                kolon = 0;

                using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select * from SPECIAL_TECH where ID='" + ID + "'", con))
                    {
                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                        {
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1, "SPECIAL_TECH");
                            DataRow[] adf = ds1.Tables["SPECIAL_TECH"].Select("ID='" + ID + "'");
                            if (adf.Length != 0)
                            {
                                DataRow dr = adf[0];
                                dr["Name"] = NAME;
                                da.Update(ds1, "SPECIAL_TECH");

                            }
                        }
                    }
                }
            }
            for (int i = 0; i < faturakat.Length; i++)
            {
                string ID = "";
                string NAME = "";
                int kolon = 0;
                foreach (var veri in faturakat[i])
                {

                    if (kolon == 0)
                    {
                        ID = veri.ToString();
                    }
                    else if (kolon == 1)
                    {
                        NAME = veri.ToString();
                    }

                    kolon++;
                }
                kolon = 0;

                using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select * from INVOICE_OZEL where ID='" + ID + "'", con))
                    {
                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                        {
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1, "INVOICE_OZEL");
                            DataRow[] adf = ds1.Tables["INVOICE_OZEL"].Select("ID='" + ID + "'");
                            if (adf.Length != 0)
                            {
                                DataRow dr = adf[0];
                                dr["Name"] = NAME;
                                da.Update(ds1, "INVOICE_OZEL");

                            }
                        }
                    }
                }
            }

            db.SaveChanges();
            Message = "Kayıt Güncellendi";
            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
          
        }

        [HttpPost]
        public ActionResult RenkEkle( string json)
        {
            json = "[" + json + "]";

            List<RENKLER> items = JsonConvert.DeserializeObject<List<RENKLER>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                RENKLER er = items[i];
                try
                {
                    if (er.ID.ToString() == "-1" || er.ID.ToString() == "0")
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from RENKLER", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "RENKLER");
                                    DataRow df = ds.Tables["RENKLER"].NewRow();


                                    df["Name"] = er.Name;
                                    df["FirmaID"] = Session["FirmaID"].ToString();
                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                    df["Company_Code"] = company_code;

                                    ds.Tables["RENKLER"].Rows.Add(df);
                                    da.Update(ds, "RENKLER");
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
                                    "select * from RENKLER where ID='" + er.ID + "'", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "RENKLER");
                                    DataRow[] adf = ds.Tables["RENKLER"].Select("ID='" + er.ID + "'");
                                    if (adf.Length != 0)
                                    {
                                        DataRow df = adf[0];
                                        df["Name"] = er.Name;
                                        df["FirmaID"] = Session["FirmaID"].ToString();
                                        string firmaid = Session["FirmaID"].ToString();
                                        string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                        df["Company_Code"] = company_code;

                                        da.Update(ds, "RENKLER");

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
            var result = new { sonuc = 1, Message = "" };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        
        [HttpPost]

        public ActionResult BedenEkle(Bedenler bd)
        {
            Bedenler yenibd = new Bedenler();

            var result = new { sonuc = 1, Message = "Başarılı Bir Şekilde Kayıt Eklenmiştir." };
            try
            {
                if(bd.ID == -1) { 
                
                yenibd = bd;
                yenibd.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                yenibd.Company_Code = company_code;
                db.Bedenler.Add(yenibd);
                db.SaveChanges();
                }
                else
                {
                    yenibd = db.Bedenler.Where(x => x.ID == bd.ID).FirstOrDefault();
                    yenibd.Beden1 = bd.Beden1;
                    yenibd.Beden2 = bd.Beden2;
                    yenibd.Beden3 = bd.Beden3;
                    yenibd.Beden4 = bd.Beden4;
                    yenibd.Beden5 = bd.Beden5;
                    yenibd.Beden6 = bd.Beden6;
                    yenibd.Beden7 = bd.Beden7;
                    yenibd.Beden8 = bd.Beden8;
                    yenibd.Beden9 = bd.Beden9;
                    yenibd.Beden10 = bd.Beden10;
                    yenibd.Aciklama = bd.Aciklama;
                    
                    db.SaveChanges();

                }

            }
            catch (Exception e)
            {
                result = new {sonuc = 0, Message = "Kayıt Eklenirken Bir Hata Oluştu"};
            }

            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BedenBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                Bedenler bdn = db.Bedenler.Where(x => x.ID == id).FirstOrDefault<Bedenler>();

               




                return Json(new { success = true, data = bdn }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteRenk(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE RENKLER  where ID='" + id + "'", conp))
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