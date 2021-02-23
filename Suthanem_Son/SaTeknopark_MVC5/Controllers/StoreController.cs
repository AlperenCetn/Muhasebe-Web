using SaTeknopark_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            AyarMetot.Siradaki("", "DEPO", "IslemNumarası", Session["FirmaID"].ToString());
            ViewBag.DepoKoduSiradaki = AyarMetot.GetNumara;
            return View();
        }
        sayazilimEntities db = new sayazilimEntities();
        [HttpPost]
        public JsonResult YeniDepo(STORE dk)
        {

            STORE car = null;
            string Message = "Kayıt Eklendi";
            int sonuc = 1;
            var result = new { sonuc = sonuc, Message = Message };
            int firmaid = Convert.ToInt32(Session["FirmaID"].ToString());
            if (dk.ID == -1)
            {

                car = new STORE();
                car = dk;

                if (dk.Varsayilan == null || dk.Varsayilan == "")
                {
                    car.Varsayilan = "Evet";
                }
                int firmaidsrg = Convert.ToInt32(Session["FirmaID"].ToString());
                var depolist = db.STORE.Where(x => x.FirmaID == firmaidsrg && x.Kodu == car.Kodu).ToList();
                if (depolist.Count == 0)
                {
                    car.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());

                    car.KayitT = DateTime.Now.ToString("dd.MM.yyyy");
                    car.DegT = DateTime.Now.ToString("dd.MM.yyyy");
                    car.Merkez = false;
                    string firmaid2 = Session["FirmaID"].ToString();
                    string company_code = "SA01" + firmaid2.PadLeft(3, '0');
                    car.Company_Code = company_code;
                    db.STORE.Add(car);
                    db.SaveChanges();
                }

                else
                {
                    Message = "Aynı Depo Kodu İle Kayıt Eklenemez.";
                    sonuc = 0;
                }
            }
            else
            {
                car = db.STORE.Where(x => x.ID == dk.ID).FirstOrDefault<STORE>();
                car.Adres = dk.Adres;
                car.DepoAdi = dk.DepoAdi;
                car.Kodu = dk.Kodu;
                car.DegT = DateTime.Now.ToString("dd.MM.yyyy");
                car.EkBilgiler = dk.EkBilgiler;
                car.Durumu = dk.Durumu;
                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }

            result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetDepo()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<STORE> yonetim = new List<STORE>();
            string sorg = @" Select Kodu,ID,DepoAdi,Varsayilan,Durumu,EkBilgiler from STORE where FirmaID =" + FirmaID;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand dekontgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = dekontgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            STORE yt = new STORE();
                            yt.Kodu = dr["Kodu"].ToString();
                            yt.DepoAdi = dr["DepoAdi"].ToString();
                            yt.Varsayilan = dr["Varsayilan"].ToString();
                            yt.Durumu = dr["Durumu"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"].ToString());
                            yt.EkBilgiler = dr["EkBilgiler"].ToString();
                            yonetim.Add(yt);
                        }
                    }
                }
            }

            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteDepo(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                STORE str = db.STORE.Where(x => x.ID == id).FirstOrDefault();
                if (str.Merkez == true)
                {

                    return Json(new { success = false, message = "Merkez Depo Silinemez." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    STORE emp = db.STORE.Where(x => x.ID == id).FirstOrDefault<STORE>();
                    db.STORE.Remove(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        public ActionResult DepoBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                STORE emp = db.STORE.Where(x => x.ID == id).FirstOrDefault<STORE>();
                return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult DepoTransfer(int id=-1)
        {
            AyarMetot.Siradaki("", "Transfer", "IslemKodu", Session["FirmaID"].ToString());
            ViewBag.depoKoduSiradaki = AyarMetot.GetNumara;


            STORE_PROCESS str = new STORE_PROCESS();




            return View(str);
        }

        [HttpPost]
        public ActionResult DepoTransferleri(STORE_PROCESS data, string json, string islemtipi)
        {
            STORE_PROCESS tk = new STORE_PROCESS();
            string KdvDh = "H";
            int SiparisID = -1;

            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {

                tk = data;


                if (islemtipi == "depogiris")
                {
                    tk.GonderenDepoID = -1;
                }
                else if (islemtipi == "depocikis")
                {
                    tk.AlanDepoID = -1;
                }

                tk.Donem = DateTime.Now.Year.ToString();

                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.DolarKur = 1;
                tk.IslemTarih = Convert.ToDateTime(tk.IslemTarih).ToString("dd.MM.yyyy");
                tk.EuroKur = 1;
                tk.GirisTuru = "Müşteriden Gelen";
                tk.TCariID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid2 = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid2.PadLeft(3, '0');
                tk.Company_Code = company_code;
                if (tk.Tutar == null)
                {
                    tk.Tutar = 0;
                }
                db.STORE_PROCESS.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM STORE_PROCESS Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        SiparisID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                tk = db.STORE_PROCESS.Where(x => x.ID == data.ID).FirstOrDefault<STORE_PROCESS>();


                if (islemtipi == "depogiris")
                {
                    tk.GonderenDepoID = -1;
                }
                else if (islemtipi == "depocikis")
                {
                    tk.AlanDepoID = -1;
                }

                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.IslemTarih = Convert.ToDateTime(tk.IslemTarih).ToString("dd.MM.yyyy");
                tk.Kur = 1;
                tk.DolarKur = 1;
                tk.EuroKur = 1;
                tk.GirisTuru = "Müşteriden Gelen";
                tk.TCariID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid2 = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid2.PadLeft(3, '0');
                tk.Company_Code = company_code;

                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            json = "[" + json + "]";
           
            List<STORE_PROCESS_DETAIL> items = JsonConvert.DeserializeObject<List<STORE_PROCESS_DETAIL>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                STORE_PROCESS_DETAIL er = items[i];

                decimal Kdv = Convert.ToDecimal(er.KDV);
                try
                {
                    if (er.ID.ToString() == "-1" || er.ID.ToString() == "0")
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from STORE_PROCESS_DETAIL", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "STORE_PROCESS_DETAIL");
                                    DataRow df = ds.Tables["STORE_PROCESS_DETAIL"].NewRow();
                                    df["DepoIslemID"] = SiparisID;
                                    df["IslemTarihi"] = DateTime.Now;
                                    df["gDepoID"] = tk.GonderenDepoID;
                                    df["aDepoID"] = tk.AlanDepoID;
                                    df["personelID"] = tk.personelID;
                                    df["urunID"] = er.urunID;
                                    df["urunFiyat"] = er.urunFiyat;
                                    df["urunMiktar"] = er.urunMiktar;
                                    df["urunBirim"] = er.urunBirim;
                                    df["paraBirimi"] = tk.paraBirimi;
                                    df["Kur"] = tk.Kur;
                                    df["Donem"] = DateTime.Now.Year;
                                    df["IslemTipi"] = "Depo İşlem";
                                    df["KDV"] = er.KDV;
                                    df["Aciklama"] = er.Aciklama;
                                    df["GirisTuru"] = tk.GirisTuru;
                                    df["TCariID"] = -1;
                                    df["TakimID"] = -1;
                                    df["KodID"] = -1;
                                    df["SiparisIDHFT"] = -1;
                                    df["FirmaID"] = tk.FirmaID;
                                    df["Company_Code"] = tk.Company_Code;
                                    ds.Tables["STORE_PROCESS_DETAIL"].Rows.Add(df);
                                    da.Update(ds, "STORE_PROCESS_DETAIL");
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
                                    "select * from STORE_PROCESS_DETAIL where ID='" + er.ID + "'", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "STORE_PROCESS_DETAIL");
                                    DataRow[] adf = ds.Tables["STORE_PROCESS_DETAIL"].Select("ID='" + er.ID + "'");
                                    if (adf.Length != 0)
                                    {
                                        DataRow df = adf[0];
                                        df["DepoIslemID"] = SiparisID;
                                        df["IslemTarihi"] = Convert.ToDateTime(data.IslemTarih);
                                        df["gDepoID"] = tk.GonderenDepoID;
                                        df["aDepoID"] = tk.AlanDepoID;
                                        df["personelID"] = tk.personelID;
                                        df["urunID"] = er.urunID;
                                        df["urunFiyat"] = er.urunFiyat;
                                        df["urunMiktar"] = er.urunMiktar;
                                        df["urunBirim"] = er.urunBirim;
                                        df["paraBirimi"] = tk.paraBirimi;
                                        df["Kur"] = tk.Kur;
                                        df["Donem"] = DateTime.Now.Year;
                                        df["IslemTipi"] = "Depo İşlem";
                                        df["KDV"] = er.KDV;
                                        df["Aciklama"] = er.Aciklama;
                                        df["GirisTuru"] = tk.GirisTuru;
                                        df["TCariID"] = -1;
                                        df["TakimID"] = -1;
                                        df["KodID"] = -1;
                                        df["SiparisIDHFT"] = -1;
                                        df["FirmaID"] = tk.FirmaID;
                                        df["Company_Code"] = tk.Company_Code;
                                        ds.Tables["STORE_PROCESS_DETAIL"].Rows.Add(df);
                                        da.Update(ds, "STORE_PROCESS_DETAIL");

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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }
            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DepoHareketler()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetDepoHareketler()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<DepoHareket> yonetim = new List<DepoHareket>();
            string FirmaID = Session["FirmaID"].ToString();
            string sorg = @"set dateformat dmy;select s.IslemTarih as IslemTarihiF2,* from STORE_PROCESS s,STORE_PROCESS_DETAIL d where d.DepoIslemID = s.ID and s.FirmaID="+Session["FirmaID"].ToString();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand dekontgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = dekontgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            DepoHareket yt = new DepoHareket();
                            yt.IslemNo = dr["IslemKodu"].ToString();
                            yt.IslemTuru = dr["IslemTipi"].ToString();
                            yt.IslemTarih = Convert.ToDateTime(dr["IslemTarihiF2"].ToString()).ToString("dd.MM.yyyy");
                            yt.TarihF2 = Convert.ToDateTime(dr["IslemTarihiF2"].ToString()).ToString("dd.MM.yyyy");


                            int urunid = Convert.ToInt32(dr["urunID"].ToString());
                            Stok st = db.Stok.Where(x => x.ID == urunid).FirstOrDefault();
                            yt.UrunAdi = st.UrunAdi;
                            yt.UrunKodu = st.StokKodu;
                            yt.Miktar = Convert.ToDecimal(dr["urunMiktar"].ToString()).ToString("N2");
                            yt.Fiyat = Convert.ToDecimal(dr["urunFiyat"].ToString()).ToString("N2");
                            yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                            yt.Aciklama = dr["Aciklama"].ToString();
                            int personelid = Convert.ToInt32(dr["personelID"].ToString());
                            Personel pr = db.Personel.Where(x => x.ID == personelid).FirstOrDefault();

                            yt.Personel = (pr.Adi + " " + pr.Soyadi).TrimEnd();
                            int gdepoid = Convert.ToInt32(dr["GonderenDepoID"].ToString());

                            yt.AlanDepoID = dr["AlanDepoID"].ToString();
                            yt.GonderenDepoID = dr["GonderenDepoID"].ToString();

                            if (gdepoid != -1)
                            {

                                STORE depo = db.STORE.Where(x => x.ID == gdepoid).FirstOrDefault();
                                yt.GonderenDepo = depo.DepoAdi;
                                yt.IslemTuru = "Depo Çıkışı";
                            }
                            else
                            {
                                yt.GonderenDepo = "";
                            }


                            int adepoid = Convert.ToInt32(dr["AlanDepoID"].ToString());


                            if (adepoid != -1)
                            {
                                STORE depo = db.STORE.Where(x => x.ID == adepoid).FirstOrDefault();
                                yt.AlanDepo = depo.DepoAdi;
                                yt.IslemTuru = "Depo Girişi";
                            
                            }
                            else
                            {
                                yt.AlanDepo = "";
                            }
                            yt.ID = dr["ID"].ToString();

                            if (adepoid != -1 && gdepoid != -1)
                            {
                                yt.IslemTuru = "Depo Transfer";
                            }
                            yonetim.Add(yt);

                        }
                    }
                }
            }


            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteDepoDetay(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE STORE_PROCESS_DETAIL  where ID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                }


                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }
        public class DepoHareket
        {
            public string IslemNo { get; set; }
            public string IslemTuru { get; set; }
            public string IslemTarih { get; set; }
            public string TarihF2 { get; set; }
            public string UrunKodu { get; set; }
            public string UrunAdi { get; set; }
            public string Miktar { get; set; }
            public string Fiyat { get; set; }

            public string Tutar { get; set; }
            public string Aciklama { get; set; }
            public string Personel { get; set; }
            public string GonderenDepo { get; set; }
            public string GonderenDepoID { get; set; }
            public string AlanDepoID { get; set; }

            public string AlanDepo { get; set; }

            public string ID { get; set; }

        }


        public ActionResult DeleteStoreProcess(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE STORE_PROCESS  where ID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE STORE_PROCESS_DETAIL  where DepoIslemID='" + id + "'", conp))
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