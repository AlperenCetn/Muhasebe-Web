using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaTeknopark_MVC5.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Runtime;


//Demo Projesi

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class TablesController : Controller
    {


        public ActionResult StaticTables()
        {
            return View();
        }

        public ActionResult DataTables(int YeniStok = 0)
        {
            ViewBag.YeniCari = YeniStok;
            using (SqlConnection con2 = new System.Data.SqlClient.SqlConnection(AyarMetot.strcon))
            {

                if (con2.State == ConnectionState.Closed) con2.Open();
                string FirmaID = Session["FirmaID"].ToString();
                string srg = "select ID,ParaBirimi From Cari where FirmaID = " + FirmaID;
                using (SqlCommand csay = new SqlCommand(srg, con2))
                {
                    using (SqlDataReader rdr = csay.ExecuteReader())
                    {

                        while (rdr.Read())
                        {
                            using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.strcon))
                            {

                                if (conp.State == ConnectionState.Closed) conp.Open();
                                using (SqlCommand calis = new SqlCommand("BakiyeKontrolCari", conp))
                                {
                                    try
                                    {
                                        calis.CommandType = CommandType.StoredProcedure;
                                        calis.Parameters.AddWithValue("@CariID", Convert.ToInt32(rdr["ID"]));
                                        calis.Parameters.AddWithValue("@ParaBirimi", rdr["ParaBirimi"]);
                                        calis.ExecuteNonQuery();
                                    }
                                    catch
                                    { }
                                }
                            }
                        }
                    }
                }
            }
            AyarMetot.Siradaki("", "Cari", "FirmaKodu", Session["FirmaID"].ToString());
            ViewBag.CariKoduSiradaki = AyarMetot.GetNumara;

            return View();
        }

        public ActionResult GetDurum()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<TECHNICAL> yonetim = new List<TECHNICAL>();
            string sorg = @"select COUNT(ID) as oALan10,Durum from TECHNICAL Where FirmaID =" + FirmaID + " group by Durum";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TECHNICAL yt = new TECHNICAL();

                            yt.Durum = dr["Durum"].ToString();
                            yt.oAlan10 = dr["oAlan10"].ToString();
                            //yt.Tarih = dr["Tarih"].ToString();

                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult YeniTahsilat(int id = 0)
        {
            ViewBag.vKasaID = Session["vKasaID"].ToString();
            ViewBag.vBankaID = Session["vBankaID"].ToString();


            AyarMetot.Siradaki("", "TAHSILAT", "IslemNo", Session["FirmaID"].ToString());
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
                    ViewBag.ServisPersonelID = Session["PersonelID"].ToString();
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

        public ActionResult DepoTransfer()
        {

            return View();
        }

        public ActionResult Islemler()
        {
            return View();
        }

        sayazilimEntities db = new sayazilimEntities();
        [HttpPost]
        public JsonResult yeniCari(Cari cr, string alacakINT, string borcINT)
        {
            ViewBag.Sıradaki = AyarMetot.GetNumara;
            Cari car = null;

            int islemsonuc = 1;
            string Message = "Kayıt Eklendi";

            if (cr.ID == -1 || cr.ID == 0)
            {
                int firmaidsrg = Convert.ToInt32(Session["FirmaID"].ToString());
                var list = db.Cari.Where(x => x.FirmaKodu == cr.FirmaKodu && x.FirmaID == firmaidsrg).ToList();
                if (list.Count == 0)
                {
                    car = new Cari();
                    car = cr;
                    car.WebKA = cr.WebKA;
                    try
                    {
                        car.WebSifre = GirisKontrol.hash(cr.WebSifre, true);
                    }
                    catch (Exception e)
                    {

                    }


                    if (cr.paraBirimi == null)
                    {
                        car.paraBirimi = "TL";
                    }
                    else
                    {
                        car.paraBirimi = cr.paraBirimi;
                    }

                    car.CariList = true;
                    if (cr.MusteriTemsilcisi == null)
                    {
                        cr.MusteriTemsilcisi = 1;
                    }

                    try
                    {
                        car.FirmaID = (short)Convert.ToInt32(Session["FirmaID"]);
                    }
                    catch (Exception e)
                    {
                        car.FirmaID = 1;
                    }

                    if (car.KTipi != "BAYİİ")
                    {
                        car.DepoID = -1;
                    }
                    else
                    {
                        car.DepoID = cr.DepoID;
                    }

                    car.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                    string firmaid = Session["FirmaID"].ToString();
                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                    car.Company_Code = company_code;
                    db.Cari.Add(car);
                    db.SaveChanges();
                    decimal alacak = 0;
                    decimal borc = 0;
                    if (alacakINT != "")
                    {
                        alacak = Convert.ToDecimal(alacakINT);
                    }
                    if (borcINT != "")
                    {
                        borc = Convert.ToDecimal(borcINT);
                    }
                    var carilist = db.Cari.OrderByDescending(x => x.ID).Take(1).ToList();

                    foreach (var item in carilist)
                    {
                        if (alacak > 0)
                        {
                            Bakiyeekle("CAF", item.ID, alacak, item.paraBirimi);
                        }

                        if (borc > 0)
                        {
                            Bakiyeekle("CBF", item.ID, borc, item.paraBirimi);
                        }

                    }
                }
                else
                {
                    islemsonuc = 0;
                    Message = "Aynı Firma Koduna Ait Ver Mevcuttur.";
                }

            }
            else
            {


                car = db.Cari.Where(x => x.ID == cr.ID).FirstOrDefault<Cari>();

                car.CariUnvan = cr.CariUnvan;
                car.FirmaKodu = cr.FirmaKodu;
                car.Il = cr.Il;
                car.Yetkili = cr.Yetkili;
                car.GSM = cr.GSM;
                car.Telefon1 = cr.Telefon1;
                car.PostaKodu = cr.PostaKodu;
                car.Ilce = cr.Ilce;
                car.Faks = cr.Faks;
                car.Iskonto = cr.Iskonto;
                car.MersisNo = cr.MersisNo;
                car.TicaretSicilNo = cr.TicaretSicilNo;
                car.VergiDr = cr.VergiDr;
                car.VergiNo = cr.VergiNo;
                car.Adres = cr.Adres;
                car.EkBilgi = cr.EkBilgi;
                car.EPosta = cr.EPosta;
                car.KTipi = cr.KTipi;
                car.StokFG = cr.StokFG;
                car.CariGrubu = cr.CariGrubu;
                car.Milleti = cr.Milleti;
                car.WebKA = cr.WebKA;
                car.paraBirimi = cr.paraBirimi;
                try
                {

                    car.WebSifre = GirisKontrol.hash(cr.WebSifre, true);
                }
                catch { }
                db.SaveChanges();
                islemsonuc = 1;
                Message = "Kayıt Güncellendi";
            }

            var result = new { sonuc = islemsonuc, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult yeniTahsilat(
            string IslemTipi,
            int id,
            int CariID,
            string IslemNo,
            string Aciklama,
            string Tutar,
            string ThisKasaID,
            string ThisBankaID,
            string parabirimi,
            string Personel,
            int OzelKodID,
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
                cs.CariID = CariID;


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
                cs.IslemNo = IslemNo;
                cs.OzelKodID = OzelKodID;


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


                if (IslemTipi == "T" || IslemTipi == "O")
                {

                    kasaID = Convert.ToInt32(ThisKasaID);
                    bankaID = -1;
                    kkID = -1;

                }
                else if (IslemTipi == "G" || IslemTipi == "KKT" || IslemTipi == "H")
                {

                    kasaID = -1;
                    bankaID = Convert.ToInt32(ThisBankaID);
                    kkID = -1;

                }





                cs.KasaID = kasaID;
                cs.BankaID = bankaID;
                cs.KrediKartiID = kkID;
                cs.OzelKodID = OzelKodID;
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
                cs.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                cs.Company_Code = company_code;

                try
                {
                    cs.Plaka2 = "";
                }
                catch { }
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
                    item.CariID = CariID;
                    item.Tutar = TutarDecimal;
                    item.OzelKodID = OzelKodID;
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



        [HttpPost]
        public ActionResult DeleteCari(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                Cari emp = db.Cari.Where(x => x.ID == id).FirstOrDefault<Cari>();


                using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                {
                    conp.Open();
                    SqlCommand cmd = new SqlCommand("IslemKontrolCari", conp);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CariID", emp.ID);  // input parameter

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
                            return Json(new { success = false, message = "Seçili Cariye Ait " + average.Value.ToString() + " bulunmaktadır" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, message = "Seçili Cari Bakiyesi 0'dan Büyüktür.İşlemlerinizi Kontrol Ediniz" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        db.Cari.Remove(emp);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }


        public ActionResult CariBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                Cari emp = db.Cari.Where(x => x.ID == id).FirstOrDefault<Cari>();
                string websifre = GirisKontrol.unhash(emp.WebSifre, true);
                emp.WebSifre = websifre;

                BALANCE BAL = db.BALANCE.Where(x => x.cariID == id && x.paraBirimi == emp.paraBirimi).FirstOrDefault<BALANCE>();

                return Json(new { success = true, data = emp, bl = BAL }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult StokBilgi(int? id = -1, string Brk = "", string FiyatTipi = "1.Fiyat")
        {

            using (sayazilimEntities db = new sayazilimEntities())
            {
                Stok emp = new Stok();
                emp.ID = -1;
                bool kodbulundu = true;
                emp = db.Stok.Where(x => x.ID == id).FirstOrDefault<Stok>();
                if (id == -1 && Brk != "")
                {

                    emp = db.Stok.Where(x => x.Barkod == Brk).FirstOrDefault<Stok>();
                    
                    if (emp == null)
                    {

                        emp = db.Stok.Where(x =>  x.Brkd2 == Brk).FirstOrDefault<Stok>();
                        try { 
                        emp.Barkod = emp.Brkd2;
                        }
                        catch { }
                        if (emp == null)
                        {
                            emp = db.Stok.Where(x => x.Brkd3 == Brk).FirstOrDefault<Stok>();
                            try
                            {
                                emp.Barkod = emp.Brkd3;
                            }
                            catch { }
                            if (emp == null)
                            {
                                emp = db.Stok.Where(x => x.Brkd4 == Brk).FirstOrDefault<Stok>();
                                try
                                {
                                    emp.Barkod = emp.Brkd4;
                                }
                                catch { }
                                if (emp == null)
                                {
                                    emp = db.Stok.Where(x => x.Brkd5 == Brk).FirstOrDefault<Stok>();
                                    try
                                    {
                                        emp.Barkod = emp.Brkd5;
                                    }
                                    catch { }
                                }
                            }
                        }

                    }




                    if (emp != null)
                    {


                        kodbulundu = false;

                    }
                    else
                    {
                        using (SqlConnection conv = new SqlConnection(AyarMetot.strcon))
                        {
                            conv.Open();

                            using (SqlCommand carigetir = new SqlCommand("SELECT top 1 UrunID From OTHER_BARCODES where YeniBarkod='" + Brk + "'", conv))
                            {
                                using (SqlDataReader dr = carigetir.ExecuteReader())
                                {
                                    while (dr.Read())
                                    {
                                        int ID = Convert.ToInt32(dr["UrunID"]);
                                        emp = db.Stok.Where(x => x.ID == ID).FirstOrDefault<Stok>();
                                        if (emp != null)
                                        {
                                            kodbulundu = false;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {


                }


                return Json(new { success = true, data = emp, kodbulundu = kodbulundu }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetGorusme(int id)
        {

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;




            List<CariModel> yonetim = new List<CariModel>();

            string sr = @" Select Coalesce(-SUM(D.Miktar*(case when coalesce(S.F2,0)<>0 then S.F2 else S.SatishFiyat end)) ,0)
from INVOICE I,STOK S,INVOICE_DETAIL D WHERE I.CariID=Cari.ID 
AND I.ID=D.faturaID AND S.ID=D.UrunID ";

            if (AyarMetot.ProTerminal != "Simge")
            {
                sr = "0";
            }

            string SorguFirmaID = Session["FirmaID"].ToString();
            string sorg = "";
            if (id == 0)
            {
                sorg =
                    @"Select Telefon1,GSM,EPosta,alacakB,borcB,Milleti,FirmaKodu,CariUnvan,Cari.ID,CariGrubu,Coalesce( alacakB-borcB,0)  as [Bakiye],
(" + sr + @") as [SatisF2], KTipi,Cari.paraBirimi as PB from Cari INNER JOIN BALANCE ON cariID=Cari.ID and Balance.paraBirimi=Cari.paraBirimi Where Cari.KTipi <> N'BAYİİ' and Cari.FirmaID=" + SorguFirmaID + " order by CariUnvan";
            }
            else if (id == 1)
            {
                sorg =
                   @"Select Telefon1,GSM,EPosta,alacakB,borcB,Milleti,FirmaKodu,CariUnvan,Cari.ID,CariGrubu,Coalesce( alacakB-borcB,0)  as [Bakiye],
(0) as [SatisF2], KTipi,Cari.paraBirimi as PB from Cari INNER JOIN BALANCE ON cariID=Cari.ID and Balance.paraBirimi=Cari.paraBirimi where Cari.KTipi=N'BAYİİ' and Cari.FirmaID=" + SorguFirmaID + " order by CariUnvan ";
            }

            string sorg2 = @"SELECT CariUnvan,alacakB,borcB FROM Cari K, BALANCE Y WHERE K.ID = Y.cariID";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand carigetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = carigetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            CariModel yt = new CariModel();

                            yt.Alacak = Convert.ToDecimal(dr["alacakB"]);
                            yt.Borc = Convert.ToDecimal(dr["borcB"]);

                            var alacaklar = yt.Alacak - yt.Borc;

                            if (alacaklar < 0)
                            {
                                yt.SonDurum = "(B)";
                                int uzunluk = dr["Bakiye"].ToString().Length;
                                string sub = dr["Bakiye"].ToString().Substring(1, uzunluk - 1);
                                yt.Bakiye = Convert.ToDecimal(sub);
                            }
                            else if (alacaklar > 0)
                            {
                                yt.SonDurum = "(A)";
                                yt.Bakiye = Convert.ToDecimal(dr["Bakiye"]);
                            }
                            else
                            {
                                yt.SonDurum = "(-)";
                                yt.Bakiye = Convert.ToDecimal(dr["Bakiye"]);
                            }

                            yt.Iletisim = dr["Telefon1"].ToString();
                            if (dr["GSM"].ToString() != "")
                            {
                                yt.Iletisim = yt.Iletisim + "  " + dr["GSM"].ToString();
                                yt.Iletisim = yt.Iletisim.TrimStart().TrimEnd();
                            }
                            yt.EPosta = dr["EPosta"].ToString();
                            yt.FirmaKodu = dr["FirmaKodu"].ToString();
                            yt.CariUnvan = dr["CariUnvan"].ToString();
                            yt.KTipi = dr["KTipi"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"]);

                            yt.SatisF2 = Convert.ToDecimal(dr["SatisF2"]);
                            yt.CariGrubu = dr["CariGrubu"].ToString();
                            yt.paraBirimi = dr["PB"].ToString();

                            yonetim.Add(yt);

                        }
                    }
                }
            }



            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }
        public partial class StokListesi
        {
            public int ID { get; set; }
            public string StokKodu { get; set; }
            public string UrunAdi { get; set; }

            public string Grubu { get; set; }
            public string Fiyat { get; set; }
            public string Barkod { get; set; }
            public string Barkod2 { get; set; }
            public string Birimi { get; set; }


        }
        public ActionResult GetStok()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<StokListesi> yonetim = new List<StokListesi>();
            string sorg = @"Select Birimi,Barkod,Brkd2,StokKodu,SatishFiyat,UrunAdi,Grubu,ID from Stok Where FirmaID= " + FirmaID;

            try
            {

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand carigetir = new SqlCommand(sorg, con))
                    {
                        using (SqlDataReader dr = carigetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                StokListesi yt = new StokListesi();

                                yt.StokKodu = dr["StokKodu"].ToString();
                                yt.UrunAdi = dr["UrunAdi"].ToString();
                                yt.Birimi = dr["Birimi"].ToString();
                                yt.Barkod = dr["Barkod"].ToString();
                                yt.Barkod2 = dr["Brkd2"].ToString();
                                yt.Fiyat = dr["SatishFiyat"].ToString();
                                yt.ID = Convert.ToInt32(dr["ID"]);

                                yonetim.Add(yt);

                            }
                        }
                    }
                }




            }
            catch (Exception e1)
            { System.IO.File.WriteAllText(Path.Combine(@"C:\Users\ilhan\AppData\Local\Sayazilim", "sonuç.xml"), e1.ToString()); }


            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }


        public partial class IslemListesi
        {
            public string FaturaType { get; set; }
            public string IslemTarihi { get; set; }
            public string IslemTarihiF { get; set; }
            public string FaturaNo { get; set; }
            public string CariUnvan { get; set; }
            public string Tutar { get; set; }
            public string Personel { get; set; }
            public string Aciklama { get; set; }
            public string IslemYapan { get; set; }

            public string ID { get; set; }
            public string Tablo { get; set; }
        }

        [HttpGet]
        public ActionResult GetIslemler(int Anasayfa = 0)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;


            string FirmaID = Session["FirmaID"].ToString();

            List<IslemListesi> yonetim = new List<IslemListesi>();
            string sorg = @"set language turkish;set dateformat dmy; Select I.[*],FaturaType AS [İşlem Tipi],IslemTarihi as [Tarih],
FaturaNo as [İşlem No],(Select CariUnvan from Cari where ID = CariID) as [Firma],
Tutar,Personel,[İşlem Yapan],Aciklama,ID,Tablo from IslemlerListesi I Where PersonelID=" + Convert.ToInt32(Session["PersonelID"]) + " and (FaturaType=N'Gelen Havale' or FaturaType=N'Perakende İade' or FaturaType=N'Sevk İrsaliyesi' or FaturaType=N'Satış Faturası' or FaturaType=N'Kredi Kartı Tahsilat' or FaturaType=N'Perakende Satış'or FaturaType=N'Depo İşlemi') order by Tarih desc";

            if (Anasayfa == 1)
            {

                sorg = @"set language turkish;set dateformat dmy; Select top 6 I.[*],FaturaType AS [İşlem Tipi],IslemTarihi as [Tarih],
FaturaNo as [İşlem No],(Select CariUnvan from Cari where ID = CariID) as [Firma],
Tutar,Personel,[İşlem Yapan],Aciklama,ID,Tablo from IslemlerListesi I   order by Tarih desc";
            }

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand carigetir = new SqlCommand(sorg, con))
                    {
                        using (SqlDataReader dr = carigetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (Anasayfa == 1)
                                {
                                    if (dr["*"].ToString() != "CAF" && dr["*"].ToString() != "CBF")
                                    {


                                        IslemListesi isl = new IslemListesi();

                                        isl.FaturaType = dr["İşlem Tipi"].ToString();
                                        isl.IslemTarihi = dr["Tarih"].ToString();
                                        isl.IslemTarihiF = Convert.ToDateTime(dr["Tarih"]).ToString("yyyy.MM.dd");
                                        isl.FaturaNo = dr["İşlem No"].ToString();
                                        isl.CariUnvan = dr["Firma"].ToString();
                                        isl.Tutar = dr["Tutar"].ToString();
                                        isl.Personel = dr["Personel"].ToString();
                                        isl.Aciklama = dr["Aciklama"].ToString();
                                        isl.IslemYapan = dr["İşlem Yapan"].ToString();
                                        isl.ID = dr["ID"].ToString();
                                        isl.Tablo = dr["*"].ToString();
                                        yonetim.Add(isl);
                                    }
                                }
                                else
                                {
                                    IslemListesi isl = new IslemListesi();

                                    isl.FaturaType = dr["İşlem Tipi"].ToString();
                                    isl.IslemTarihi = dr["Tarih"].ToString();
                                    isl.IslemTarihiF = Convert.ToDateTime(dr["Tarih"]).ToString("yyyy.MM.dd");
                                    isl.FaturaNo = dr["İşlem No"].ToString();
                                    isl.CariUnvan = dr["Firma"].ToString();
                                    isl.Tutar = dr["Tutar"].ToString();
                                    isl.Personel = dr["Personel"].ToString();
                                    isl.Aciklama = dr["Aciklama"].ToString();
                                    isl.IslemYapan = dr["İşlem Yapan"].ToString();
                                    isl.ID = dr["ID"].ToString();
                                    isl.Tablo = dr["*"].ToString();
                                    yonetim.Add(isl);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                System.IO.File.WriteAllText(Path.Combine(@"C:\Users\alper\AppData\Local\Sayazilim", "sonuç.xml"), e1.ToString());

            }

            var sonuc = Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
            sonuc.MaxJsonLength = int.MaxValue;

            return sonuc;
        }


        public ActionResult GetCariHareketler(int id)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<HareketlerListesi> yonetim = new List<HareketlerListesi>();
            string sorg = @"SET DATEFORMAT DMY;Select [*] as Islemtipi,ID,BA,FaturaType,FaturaNo,Tutar,IslemTarihi,Vade,Aciklama,(0) as Alacak,(0) as Borc,(0) as Bakiye,Doviz from HareketlerListesi WHERE CariID='" + id + "' order by IslemTipi ";

            decimal alacak = 0;
            decimal borc = 0;
            decimal sonuc = 0;
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand carigetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = carigetir.ExecuteReader())
                    {
                        try
                        {


                            while (dr.Read())
                            {
                                HareketlerListesi yt = new HareketlerListesi();

                                yt.FaturaNo = dr["FaturaNo"].ToString();
                                try
                                {
                                    yt.Vade = Convert.ToDateTime(dr["Vade"]);
                                }
                                catch { yt.Vade = DateTime.Now; }


                                yt.IslemTipi = dr["Islemtipi"].ToString();
                                if (dr["BA"].ToString() == "(B)")
                                {
                                    decimal Borc = Convert.ToDecimal(dr["Tutar"]);
                                    yt.Borc = Convert.ToDecimal(Borc.ToString("N2"));
                                    yt.BorcS = Borc.ToString("N2");
                                    yt.Alacak = 0;
                                    yt.AlacakS = "0,00";
                                    borc += Convert.ToDecimal(dr["Tutar"]);
                                }
                                else if (dr["BA"].ToString() == "(A)")
                                {
                                    decimal alacak1 = Convert.ToDecimal(dr["Tutar"]);
                                    yt.Alacak = Convert.ToDecimal(alacak1.ToString("N2"));
                                    yt.AlacakS = alacak1.ToString("N2");
                                    yt.Borc = 0;
                                    yt.BorcS = "0,00";
                                    alacak += Convert.ToDecimal(dr["Tutar"]);
                                }
                                yt.Aciklama = dr["Aciklama"].ToString();
                                sonuc += (Convert.ToDecimal(yt.Alacak) - Convert.ToDecimal(yt.Borc));
                                yt.Bakiye = sonuc;
                                yt.BakiyeS = Convert.ToDecimal(sonuc).ToString("N2");
                                yt.Doviz = dr["Doviz"].ToString();
                                ViewBag.Parabirimi = dr["Doviz"].ToString();
                                yt.FaturaType = dr["FaturaType"].ToString();
                                yt.ID = Convert.ToInt32(dr["ID"].ToString());
                                try
                                {
                                    yt.IslemTarihi = Convert.ToDateTime(dr["IslemTarihi"]);
                                    yt.IslemTarihiF = Convert.ToDateTime(dr["IslemTarihi"]).ToString("dd.MM.yyyy");
                                }
                                catch (Exception e)
                                {
                                    yt.IslemTarihi = DateTime.Now;
                                    yt.IslemTarihiF = DateTime.Now.ToString("dd.MM.yyyy");
                                }
                                yonetim.Add(yt);
                            }
                        }
                        catch (Exception a)
                        {

                        }




                    }
                }
            }

            return Json(new
            {
                data = yonetim.Distinct()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CariHareketler(int id)
        {
            ViewBag.id = id;
            return View();

        }

        public ActionResult FooTables()
        {
            return View();
        }

        public ActionResult jqGrid()
        {
            return View();
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
                        dt["CariID"] = cID;
                        dt["PersonelID"] = AyarMetot.PersonelID;
                        dt["KasaID"] = -1;
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
                        dt["FirmaID"] = Convert.ToInt16(Session["FirmaID"].ToString());
                        string firmaid = Session["FirmaID"].ToString();
                        string company_code = "SA01" + firmaid.PadLeft(3, '0');
                        dt["Company_Code"] = company_code;

                        ds1.Tables["CASH_paY"].Rows.Add(dt);
                        da.Update(ds1, "CASH_paY");

                    }
                }
            }
        }


        public ActionResult GetKarZarar()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<KarZarar> yonetim = new List<KarZarar>();
            string sorg = @"SET LANGUAGE Turkish;set dateformat dmy; select  DATENAME(MONTH,IslemTarihi) AS Ay,(select coalesce( sum(Tutar),0) from IslemlerListesi P where P.[*]='SF' and DATENAME(MONTH,G.IslemTarihi)=DATENAME(MONTH,P.IslemTarihi)) as [SF],(select coalesce(sum(Tutar),0) from IslemlerListesi P where P.[*]='AF' and DATENAME(MONTH,G.IslemTarihi)=DATENAME(MONTH,P.IslemTarihi)) as [AF],(select coalesce(sum(Tutar),0) from IslemlerListesi P where P.[*]='A' or P.[*]='R' or P.[*]='E' and DATENAME(MONTH,G.IslemTarihi)=DATENAME(MONTH,P.IslemTarihi)) as [MF],(select coalesce(sum(Tutar),0) from IslemlerListesi P where P.[*]='TP' and DATENAME(MONTH,G.IslemTarihi)=DATENAME(MONTH,P.IslemTarihi)) as [TP] from IslemlerListesi G group by DATENAME(MONTH,IslemTarihi)";

            try
            {

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand carigetir = new SqlCommand(sorg, con))
                    {
                        using (SqlDataReader dr = carigetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                KarZarar yt = new KarZarar();

                                yt.Ay = dr["Ay"].ToString();
                                yt.SF = Convert.ToDecimal(dr["SF"]);
                                yt.AF = Convert.ToDecimal(dr["AF"]);
                                yt.MF = Convert.ToDecimal(dr["MF"]);
                                yt.TP = Convert.ToDecimal(dr["TP"]);
                                yt.Durum = yt.SF - (yt.AF + yt.MF + yt.TP);
                                yonetim.Add(yt);

                            }
                        }
                    }
                }




            }
            catch (Exception e1)
            { }


            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PerakendeSatis()
        {
            var urunler = db.Stok.ToList();
            ViewBag.Urunler = urunler.ToList();
            return View();
        }

    }
}