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
    public class RaporlarController : Controller
    {
        
        // GET: Raporlar
        public ActionResult Raporlar()
        {
            return View();
        }

        #region CariRapor
        public ActionResult CariRapor()
        {
            return View();
        }

        public class servismodel
        {
            public DateTime IslemTarihi { get; set; }
            public string FaturaNo { get; set; }
            public string IslemTuru { get; set; }
            public decimal Miktar { get; set; }
            public decimal DövizliFiyatBrut { get; set; }
            public string UrunAdi { get; set; }
            public string Grubu { get; set; }
            public string Barkod { get; set; }
            public string CariUnvan { get; set; }
            public string FirmaKodu { get; set; }
            public string SatirParaBirimi { get; set; }

        }


        [HttpGet]
        public ActionResult GetCari()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<RaporlarController.servismodel> yonetim = new List<RaporlarController.servismodel>();
            //List<servismodel> servis = new List<servismodel>();
            //List<servismodel> yeniservis = new List<servismodel>();
            //List<servismodel> bitenservis = new List<servismodel>();

            try
            {
                string sorg = @"SELECT UrunAdi ,Grubu,Barkod ,IslemTarihi ,IslemTuru ,FaturaNo,Miktar,
 DövizliFiyatBrut,SatirParaBirimi ,(Select CariUnvan from Cari where ID = CariID) as CariUnvan,(Select FirmaKodu from Cari where ID = CariID) as FirmaKodu
 FROM PRODUCT_HAREKET  WHERE(IslemTuru = N'Alım Faturası') and FirmaID="+Session["FirmaID"].ToString()+"  Order by IslemTarihi Desc  ";

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                    {
                        using (SqlDataReader dr = maasgetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {


                                RaporlarController.servismodel yt = new RaporlarController.servismodel();
                                try
                                {
                                    yt.IslemTarihi = Convert.ToDateTime(dr["IslemTarihi"]);
                                }
                                catch (Exception)
                                {


                                }

                                yt.Barkod = dr["Barkod"].ToString();
                                yt.Grubu = dr["Grubu"].ToString();
                                yt.UrunAdi = dr["UrunAdi"].ToString();
                                yt.IslemTuru = dr["IslemTuru"].ToString();
                                yt.FaturaNo = dr["FaturaNo"].ToString();
                                try
                                {
                                    yt.Miktar = Convert.ToDecimal(dr["Miktar"]);
                                }
                                catch (Exception)
                                {


                                }
                                try
                                {
                                    yt.DövizliFiyatBrut = Convert.ToDecimal(dr["DövizliFiyatBrut"]);
                                }
                                catch (Exception)
                                {


                                }


                                yt.SatirParaBirimi = dr["SatirParaBirimi"].ToString();
                                yt.CariUnvan = dr["CariUnvan"].ToString();
                                yt.FirmaKodu = dr["FirmaKodu"].ToString();




                                yonetim.Add(yt);

                            }
                        }

                    }



                }
            }
            catch (Exception E1)
            {


                System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Admin\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());

            }

            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult SatisRaporu()
        {


            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<RaporlarController.servismodel> yonetim = new List<RaporlarController.servismodel>();
            //List<servismodel> servis = new List<servismodel>();
            //List<servismodel> yeniservis = new List<servismodel>();
            //List<servismodel> bitenservis = new List<servismodel>();

            try
            {
                string sorg = @"SELECT UrunAdi ,Grubu,Barkod ,IslemTarihi ,IslemTuru ,FaturaNo,Miktar,
 DövizliFiyatBrut,SatirParaBirimi ,(Select CariUnvan from Cari where ID = CariID) as CariUnvan,(Select FirmaKodu from Cari where ID = CariID) as FirmaKodu
 FROM PRODUCT_HAREKET  WHERE(IslemTuru = N'Satış Faturası' or IslemTuru = N'Perakende Satış') and FirmaID="+Session["FirmaID"].ToString()+"  Order by IslemTarihi Desc  ";

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                    {
                        using (SqlDataReader dr = maasgetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {


                                RaporlarController.servismodel yt = new RaporlarController.servismodel();
                                try
                                {
                                    yt.IslemTarihi = Convert.ToDateTime(dr["IslemTarihi"]);
                                }
                                catch (Exception)
                                {


                                }

                                yt.Barkod = dr["Barkod"].ToString();
                                yt.Grubu = dr["Grubu"].ToString();
                                yt.UrunAdi = dr["UrunAdi"].ToString();
                                yt.IslemTuru = dr["IslemTuru"].ToString();
                                yt.FaturaNo = dr["FaturaNo"].ToString();
                                try
                                {
                                    yt.Miktar = Convert.ToDecimal(dr["Miktar"]);
                                }
                                catch (Exception)
                                {


                                }
                                try
                                {
                                    yt.DövizliFiyatBrut = Convert.ToDecimal(dr["DövizliFiyatBrut"]);
                                }
                                catch (Exception)
                                {


                                }


                                yt.SatirParaBirimi = dr["SatirParaBirimi"].ToString();
                                yt.CariUnvan = dr["CariUnvan"].ToString();
                                yt.FirmaKodu = dr["FirmaKodu"].ToString();




                                yonetim.Add(yt);

                            }
                        }

                    }



                }
            }
            catch (Exception E1)
            {


                System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Admin\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());

            }

            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);


        }


        public class CariModel
        {
            public string CariUnvan { get; set; }
            public string FirmaKodu { get; set; }

            public int ID { get; set; }
            public string TL { get; set; }
            public string GBP { get; set; }
            public string USD { get; set; }
            public string JPY { get; set; }
            public string EUR { get; set; }
            public string KTipi { get; set; }
            public string Temsilci { get; set; }
            public string Il { get; set; }
            public string Ilce { get; set; }
            public string İletişimBilgileri { get; set; }
            public string ozelAlan1 { get; set; }
            public string CariGrubu { get; set; }

            public string city2 { get; set; }
            public string Yetkili { get; set; }
            public string VergiDr { get; set; }
            public string VergiNo { get; set; }
            public string GSM { get; set; }
            public string Faks { get; set; }
            public string EPosta { get; set; }
            public string Ulke { get; set; }
            public string PostaKodu { get; set; }
            public string Adres { get; set; }
            public string town2 { get; set; }
            public string address2 { get; set; }
            public string EkBilgi { get; set; }
            public string MersisNo { get; set; }
            public string TicaretSicilNo { get; set; }
            public string BagkurNo { get; set; }
            public string NotDefteri { get; set; }
            public string TCNo { get; set; }
            public string Telefon1 { get; set; }






        }
        public ActionResult GetCariBaDöviz()
        {


            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<RaporlarController.CariModel> yonetim2 = new List<RaporlarController.CariModel>();


            try
            {
                string sorg = @"set dateformat dmy ; SELECT   ID, FirmaKodu ,CariUnvan 
 , Cari.Yetkili AS[Yetkili Kişi]
, (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'TL'    and C.BA = '(A)' AND C.CariID = Cari.ID ) -  
 (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'TL'     and C.BA = '(B)' AND C.CariID = Cari.ID  ) as [TL],  
(select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'USD'     and C.BA = '(A)' AND C.CariID = Cari.ID ) -  
  (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'USD'   and C.BA = '(B)' AND C.CariID = Cari.ID  ) as [USD],  
(select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'EUR'     and C.BA = '(A)' AND C.CariID = Cari.ID ) -  
  (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'EUR'   and C.BA = '(B)' AND C.CariID = Cari.ID  ) as [EUR],  
(select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'GBP'     and C.BA = '(A)' AND C.CariID = Cari.ID ) -  
  (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'GBP'   and C.BA = '(B)' AND C.CariID = Cari.ID  ) as [GBP],  
 (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'JPY'     and C.BA = '(A)' AND C.CariID = Cari.ID ) -  
  (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'JPY'   and C.BA = '(B)' AND C.CariID = Cari.ID  ) as [JPY],  

KTipi 
             ,(Select Adi + ' ' + Soyadi from Personel where ID = MusteriTemsilcisi) as Temsilci ,  

              Il ,Ilce, Cari.EPosta + ' ' + Cari.Telefon1 + ' ' + Cari.GSM AS İletişimBilgileri,  
            ozelAlan1,CariGrubu ,City2 
          
               FROM Cari where FirmaID = " + Session["FirmaID"].ToString();

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                    {
                        using (SqlDataReader dr = maasgetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {


                                RaporlarController.CariModel yt = new RaporlarController.CariModel();

                                yt.CariUnvan = dr["CariUnvan"].ToString();
                                yt.ID = Convert.ToInt32(dr["ID"]);
                                yt.FirmaKodu = dr["FirmaKodu"].ToString();
                                yt.TL = dr["TL"].ToString();
                                yt.GBP = dr["GBP"].ToString();
                                yt.USD = dr["USD"].ToString();
                                yt.JPY = dr["JPY"].ToString();
                                yt.EUR = dr["EUR"].ToString();
                                yt.KTipi = dr["KTipi"].ToString();
                                yt.Temsilci = dr["Temsilci"].ToString();
                                yt.Il = dr["Il"].ToString();
                                yt.Ilce = dr["Ilce"].ToString();
                                yt.İletişimBilgileri = dr["İletişimBilgileri"].ToString();
                                yt.ozelAlan1 = dr["ozelAlan1"].ToString();
                                yt.CariGrubu = dr["CariGrubu"].ToString();
                                yt.city2 = dr["city2"].ToString();





                                yonetim2.Add(yt);

                            }
                        }

                    }



                }
            }
            catch (Exception E1)
            {


                System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Admin\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());

            }

            return Json(new { data = yonetim2.ToList() }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult GetAdresTelefonDefteri()
        {


            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<RaporlarController.CariModel> yonetim1 = new List<RaporlarController.CariModel>();
            //List<servismodel> servis = new List<servismodel>();
            //List<servismodel> yeniservis = new List<servismodel>();
            //List<servismodel> bitenservis = new List<servismodel>();


            string sorg = @"set dateformat dmy ;set dateformat dmy ; SELECT  ID, FirmaKodu,CariUnvan


 , (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'TL'        and C.BA = '(A)' AND C.CariID = Cari.ID ) -  
  (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'TL'         and C.BA = '(B)' AND C.CariID = Cari.ID  ) as [TL],  
 (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'USD'         and C.BA = '(A)' AND C.CariID = Cari.ID ) -  
   (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'USD'       and C.BA = '(B)' AND C.CariID = Cari.ID  ) as [USD],  
 (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'EUR'         and C.BA = '(A)' AND C.CariID = Cari.ID ) -  
   (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'EUR'       and C.BA = '(B)' AND C.CariID = Cari.ID  ) as [EUR],  
 (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'GBP'         and C.BA = '(A)' AND C.CariID = Cari.ID ) -  
   (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'GBP'       and C.BA = '(B)' AND C.CariID = Cari.ID  ) as [GBP],  
  (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'JPY'         and C.BA = '(A)' AND C.CariID = Cari.ID ) -  
   (select coalesce(sum(Tutar), 0) from HareketlerListesi C where C.Doviz = 'JPY'       and C.BA = '(B)' AND C.CariID = Cari.ID  ) as [JPY],  

 Yetkili
  , KTipi
  , CariGrubu 
  , VergiDr 
  , VergiNo 

  , Telefon1 
  , GSM 
  , Faks 
  , EPosta 
  , Il
  , Ilce 
  , Ulke 
  , PostaKodu
  , Adres 
  , city2 
  , town2 
    ,TCNo
  , address2 
  , EkBilgi 
  , MersisNo 
  , TicaretSicilNo 
  , BagkurNo 
  , NotDefteri 
  ,(Select Adi + ' ' + Soyadi from Personel where ID = MusteriTemsilcisi) as Temsilci    

                FROM Cari where FirmaID=" + Session["FirmaID"].ToString();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            RaporlarController.CariModel yt = new RaporlarController.CariModel();


                            yt.CariUnvan = dr["CariUnvan"].ToString();

                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.FirmaKodu = dr["FirmaKodu"].ToString();


                            yt.Yetkili = dr["Yetkili"].ToString();
                            yt.KTipi = dr["KTipi"].ToString();
                            yt.CariGrubu = dr["CariGrubu"].ToString();
                            yt.VergiDr = dr["VergiDr"].ToString();
                            yt.VergiNo = dr["VergiNo"].ToString();
                            yt.Telefon1 = dr["Telefon1"].ToString();
                            yt.GSM = dr["GSM"].ToString();
                            yt.Faks = dr["Faks"].ToString();
                            yt.EPosta = dr["EPosta"].ToString();
                            yt.Il = dr["Il"].ToString();
                            yt.Ilce = dr["Ilce"].ToString();
                            yt.Ulke = dr["Ulke"].ToString();
                            yt.PostaKodu = dr["PostaKodu"].ToString();
                            yt.Adres = dr["Adres"].ToString();
                            yt.city2 = dr["city2"].ToString();
                            yt.town2 = dr["town2"].ToString();
                            yt.address2 = dr["address2"].ToString();
                            yt.EkBilgi = dr["EkBilgi"].ToString();
                            yt.MersisNo = dr["MersisNo"].ToString();
                            yt.TicaretSicilNo = dr["TicaretSicilNo"].ToString();
                            yt.BagkurNo = dr["BagkurNo"].ToString();
                            yt.NotDefteri = dr["NotDefteri"].ToString();
                            yt.Temsilci = dr["Temsilci"].ToString();
                            yt.TCNo = dr["TCNo"].ToString();


                            yonetim1.Add(yt);

                        }
                    }

                }



            }


            return Json(new { data = yonetim1.ToList() }, JsonRequestBehavior.AllowGet);


        }


        #endregion

        #region ServisRapor

        public ActionResult DurumAnalizi()
        {
            using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
            {
                if (conp1.State == ConnectionState.Closed) conp1.Open();
                using (SqlCommand sID = new SqlCommand(@"SET DATEFORMAT dmy;
           SELECT COUNT(ID) 
FROM TECHNICAL where Durum=N'TESLİM EDİLDİ' and FirmaID =" + Session["FirmaID"].ToString(), conp1))
                {
                    ViewBag.bitenservis = Convert.ToInt32(sID.ExecuteScalar());
                }
            }

            using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
            {
                if (conp1.State == ConnectionState.Closed) conp1.Open();
                using (SqlCommand sID = new SqlCommand(@"SET DATEFORMAT dmy;
           SELECT COUNT(ID) 
FROM TECHNICAL where Durum=N'GİRİŞ YAPILDI' and FirmaID =" + Session["FirmaID"].ToString(), conp1))
                {
                    ViewBag.yeniservis = Convert.ToInt32(sID.ExecuteScalar());
                }
            }
            using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
            {
                if (conp1.State == ConnectionState.Closed) conp1.Open();
                using (SqlCommand sID = new SqlCommand(@"SET DATEFORMAT dmy;
           SELECT COUNT(ID) 
FROM TECHNICAL where FirmaID =" + Session["FirmaID"].ToString(), conp1))
                {
                    ViewBag.servis = Convert.ToInt32(sID.ExecuteScalar());
                }
            }
            return View();
        }

        public class servisrapormodel
        {
            public string Personel { get; set; }
            public string ServisTipi { get; set; }
            public string Durum { get; set; }
            public string ServisKaydı { get; set; }
        }

        [HttpGet]
        public ActionResult GetServis()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<servisrapormodel> yonetim = new List<servisrapormodel>();



            string sorg = @"SELECT TECHNICAL.Durum, COUNT(*) AS [ServisKaydı], TECHNICAL.ServisTipi AS [ServisTipi] 
            FROM TECHNICAL INNER JOIN Personel ON TECHNICAL.Tekniksyen = Personel.ID  where TECHNICAL.FirmaID=" + Session["FirmaID"].ToString() + @"
                                GROUP BY TECHNICAL.Durum, TECHNICAL.ServisTipi 
                                   ORDER BY [ServisKaydı] DESC ";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            servisrapormodel yt = new servisrapormodel();
                            yt.Durum = dr["Durum"].ToString();
                            yt.ServisTipi = dr["ServisTipi"].ToString();
                            yt.ServisKaydı = dr["ServisKaydı"].ToString();
                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region StokRapor
        public ActionResult StokRapor()
        {
            return View();
        }

        public class stokmodelrapor
        {
            public string Stokkodu { get; set; }
            public string UrunAdi { get; set; }
            public string Marka { get; set; }
            public Nullable<System.DateTime> KayitT { get; set; }

            public string Grubu { get; set; }
            public string UrunTuru { get; set; }
            public string KDV { get; set; }
            public string Barkod { get; set; }
            public decimal GirişMiktarı { get; set; }
            public decimal SonAlis { get; set; }
            public decimal DepoMevcudu { get; set; }
            public decimal ÇıkışMiktarı { get; set; }
            public decimal KalanMiktarKritik { get; set; }
            public decimal StokLimiti { get; set; }
            public decimal AlışFiyatı { get; set; }
            public decimal SatışFiyatı { get; set; }
            public string Envanter { get; set; }
            public string PB { get; set; }
            public int ID { get; set; }
            public decimal KalanSTKU { get; set; }
            public decimal TLKarşılığı { get; set; }
            public string KayitTF { get; set; }



        }

        public class stokmodel
        {
            public string StokKodu { get; set; }
            public string UrunAdi { get; set; }
            public string Birimi { get; set; }


            public int ID { get; set; }
            public decimal StoktaKalan { get; set; }





        }

        public class analizmodel
        {
            public string StokKodu { get; set; }
            public string UrunAdi { get; set; }
            public string Birimi { get; set; }
            public int ID { get; set; }
            public decimal StoktaKalan { get; set; }
            public decimal AlishFiyat { get; set; }
            public decimal SatishFiyat { get; set; }
            public decimal sonAlis { get; set; }
            public decimal SonAlisB { get; set; }
            public decimal SonSatis { get; set; }
            public decimal SonSatisB { get; set; }


        }

        [HttpGet]
        public ActionResult GetStokRapor(DateTime bas, DateTime bit, int DepoID = -1)
        {
            //DateTime bas, DateTime bit,int DepoID,int PersonelID
            List<stokmodelrapor> yonetim = new List<stokmodelrapor>();
            try
            {

                //if (bas == null) bas = Convert.ToDateTime("01.01.2020");
                //if (bit == null) bit = DateTime.Now;

                string tutartip = "( case when coalesce(S.DövizliFiyatBrut,0)=0 then S.Alishfiyat else S.DövizliFiyatBrut end )";

                //if (radioButton2.Checked)
                //{

                tutartip = " ( case when coalesce(S.Alishfiyat,0)=0 then S.DövizliFiyatBrut else S.Alishfiyat end )";

                //}



                //DateTime bas = Convert.ToDateTime("01.01.2020");
                //DateTime bit = Convert.ToDateTime("31.12.2020");
                int depo = 0;
                string DepoAd = "";
                string select = "";
                string AdepoCek = "";
                string GdepoCek = "";

                string tarih2 = "and (S.IslemTarihi BETWEEN CONVERT(datetime, '" + bas.ToString("dd.MM.yyyy") + " 00:00:00.000" + "') AND CONVERT(datetime, '" + bit.ToString("dd.MM.yyyy") + " 23:59:00.000" + "')) ";

                try
                {
                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Admin\AppData\Local\Sayazilim", "sonuç.xml"),
                                    DepoID.ToString());
                }
                catch { }


                if (DepoID != -1)
                {
                    AdepoCek = " and S.DepoID='" + DepoID + "'";
                    GdepoCek = " and S.GDepoID='" + DepoID + "'";
                }

                else if (DepoID == -1 || DepoID == 0)
                {
                    using (SqlConnection con7 = new SqlConnection(AyarMetot.conString))
                    {
                        con7.Open();
                        string srg = "Select distinct DepoAdi as KS,ID from STORE where DepoAdi <> ' ' and FirmaID = " + Session["FirmaID"].ToString();
                        using (SqlCommand sayarlar = new SqlCommand(srg, con7))
                        {
                            using (SqlDataReader dr = sayarlar.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    depo++;
                                    DepoAd = dr["ID"].ToString();
                                    select += " ,(SELECT Coalesce(SUM(S.gBirimMiktar + s.MF), 0)  FROM PRODUCT_HAREKET S where T.ID = S.UrunID  " + tarih2 + "  and ((S.GC = '(G)' and (S.DepoID='" + dr["ID"].ToString() + "')) or (S.GC = '(GÇ)' and (S.DepoID='" + dr["ID"].ToString() + "') )) ) -(SELECT  Coalesce(SUM(S.gBirimMiktar + S.MF), 0)  FROM PRODUCT_HAREKET S where T.ID = S.UrunID  " + tarih2 + "  and ((S.GC = '(Ç)' AND (S.GDepoID = '" + dr["ID"].ToString() + "')) or (S.GC = '(GÇ)' and (S.GDepoID = '" + dr["ID"].ToString() + "') ) ) )  AS [DepoMevcudu] ";
                                }
                            }
                        }
                    }
                }

                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                //string FirmaID = Session["FirmaID"].ToString();


                string Envantertutari = "(((SELECT Coalesce(SUM(" + tutartip + " * S.gBirimMiktar) / sum(S.Miktar), 0)  FROM PRODUCT_HAREKET S where T.ID = S.UrunID " + tarih2 + " " + AdepoCek + "  and S.GC = '(G)' AND(SELECT SUM(S.gBirimMiktar ) FROM PRODUCT_HAREKET S where T.ID = S.UrunID " + tarih2 + " " + GdepoCek + "  and S.GC = '(G)') > 0))*(((SELECT Coalesce(SUM(S.gBirimMiktar ), 0)  FROM PRODUCT_HAREKET S where T.ID = S.UrunID " + tarih2 + " " + AdepoCek + "  and S.GC = '(G)') -(SELECT  Coalesce(SUM(S.gBirimMiktar), 0)  FROM PRODUCT_HAREKET S where T.ID = S.UrunID " + tarih2 + "  " + GdepoCek + "  and S.GC = '(Ç)'))))";
                string sorg = @"set dateformat dmy; Select T.Stokkodu   
                ,T.UrunAdi
                ,T.Marka 
               
                ,T.Grubu 
                ,T.UrunTuru
                ,T.KDV 
                ,T.Barkod 
                ,T.SonAlis " + select +

                @", (SELECT Coalesce(SUM(S.gBirimMiktar), 0)  FROM PRODUCT_HAREKET S where T.ID = S.UrunID " + tarih2 + "  and (S.GC = '(G)' or (S.GC = '(GÇ)' )) ) AS[GirişMiktarı]  " +
                     ", (SELECT Coalesce(SUM(S.gBirimMiktar), 0)  FROM PRODUCT_HAREKET S where T.ID = S.UrunID  " + tarih2 + " and (S.GC = '(Ç)' or (S.GC = '(GÇ)' )) ) AS[ÇıkışMiktarı]  " +
                     @",Coalesce(T.KalanStkU,0) as [StokLimiti]
				     ,Coalesce(0,0) as [KalanMiktarKritik]
                  ,T.AlishFiyat as [AlışFiyatı] 
                  ,T.SatishFiyat as [SatışFiyatı] " +
                     @", " + Envantertutari + " AS [Envanter] " +
                    @",T. ParaBirimi AS [PB] 
                 ,T.ID,KalanSTKU  
                , Convert(decimal(18,6),0) as [TLKarşılığı]  
                 FROM STOK T where T.UrunTuru<>N'Hizmet' AND T.UrunTuru<>N'Masraf' and T.UrunTuru<>N'Üretim Maliyeti' and FirmaID= " + Session["FirmaID"].ToString();

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                    {
                        using (SqlDataReader dr = maasgetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {


                                stokmodelrapor yt = new stokmodelrapor();

                                yt.Stokkodu = dr["Stokkodu"].ToString();
                                yt.UrunAdi = dr["UrunAdi"].ToString();
                                yt.Marka = dr["Marka"].ToString();
                                yt.Grubu = dr["Grubu"].ToString();
                                yt.UrunTuru = dr["UrunTuru"].ToString();
                                yt.KDV = dr["KDV"].ToString();
                                yt.Barkod = dr["Barkod"].ToString();



                                yt.ID = Convert.ToInt32(dr["ID"]);
                                try
                                {
                                    yt.GirişMiktarı = Convert.ToDecimal(dr["GirişMiktarı"]);
                                }
                                catch (Exception)
                                {


                                }
                                try
                                {
                                    yt.ÇıkışMiktarı = Convert.ToDecimal(dr["ÇıkışMiktarı"]);
                                }
                                catch (Exception)
                                {


                                }
                                try
                                {
                                    yt.SonAlis = Convert.ToDecimal(dr["SonAlis"]);
                                }
                                catch (Exception)
                                {


                                }
                                try
                                {
                                    yt.KalanMiktarKritik = Convert.ToDecimal(dr["KalanMiktarKritik"]);
                                }
                                catch (Exception)
                                {


                                }
                                try
                                {
                                    yt.StokLimiti = Convert.ToDecimal(dr["StokLimiti"]);
                                }
                                catch (Exception)
                                {


                                }

                                yt.AlışFiyatı = Convert.ToDecimal(dr["AlışFiyatı"]);


                                yt.SatışFiyatı = Convert.ToDecimal(dr["SatışFiyatı"]);

                                try
                                {
                                    yt.KalanSTKU = Convert.ToDecimal(dr["KalanSTKU"]);
                                }
                                catch (Exception)
                                {


                                }


                                yt.TLKarşılığı = Convert.ToDecimal(dr["TLKarşılığı"]);

                                try
                                {

                                    yt.DepoMevcudu = Convert.ToDecimal(dr["DepoMevcudu"]);
                                }
                                catch (Exception)
                                {


                                }



                                yt.Envanter = dr["Envanter"].ToString();
                                yt.PB = dr["PB"].ToString();





                                yonetim.Add(yt);

                            }
                        }
                    }
                }


            }
            catch (Exception e1)
            {
                try
                {
                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Admin\AppData\Local\Sayazilim", "sonuç.xml"),
                                      e1.ToString());
                }
                catch { }
            }



            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetStokAnaliz()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<analizmodel> yonetim = new List<analizmodel>();



            string sorg = @"set dateformat dmy;set language Turkish; SELECT Stok.StokKodu , Stok.UrunAdi , Stok.Birimi ,
         Stok.StoktaKalan, AlishFiyat , SatishFiyat, sonAlis , SonAlisB , SonSatis  , SonSatisB , ID 
       FROM  STOK where ID is not NULL and FirmaID=" + Session["FirmaID"].ToString();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            analizmodel yt = new analizmodel();

                            yt.StokKodu = dr["StokKodu"].ToString();
                            yt.UrunAdi = dr["UrunAdi"].ToString();
                            yt.Birimi = dr["Birimi"].ToString();

                            yt.ID = Convert.ToInt32(dr["ID"]);
                            try
                            {
                                yt.StoktaKalan = Convert.ToDecimal(dr["StoktaKalan"]);
                            }
                            catch (Exception)
                            {


                            }
                            try
                            {
                                yt.AlishFiyat = Convert.ToDecimal(dr["AlishFiyat"]);
                            }
                            catch (Exception)
                            {


                            }
                            try
                            {
                                yt.SatishFiyat = Convert.ToDecimal(dr["SatishFiyat"]);
                            }
                            catch (Exception)
                            {


                            }
                            try
                            {
                                yt.sonAlis = Convert.ToDecimal(dr["sonAlis"]);
                            }
                            catch (Exception)
                            {


                            }
                            try
                            {
                                yt.SonAlisB = Convert.ToDecimal(dr["SonAlisB"]);
                            }
                            catch (Exception)
                            {


                            }
                            try
                            {
                                yt.SonSatis = Convert.ToDecimal(dr["SonSatis"]);
                            }
                            catch (Exception)
                            {


                            }
                            try
                            {
                                yt.SonSatisB = Convert.ToDecimal(dr["SonSatisB"]);
                            }
                            catch (Exception)
                            {


                            }



                            yonetim.Add(yt);

                        }
                    }
                }
            }





            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetStokEnvarter()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<stokmodel> yonetim = new List<stokmodel>();



            string sorg = @"set dateformat dmy;set language Turkish; SELECT Stok.StokKodu, Stok.UrunAdi , Stok.Birimi, 
         Stok.StoktaKalan ,ID
       FROM  STOK  where ID is not NULL and FirmaID=" + Session["FirmaID"].ToString();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            stokmodel yt = new stokmodel();

                            yt.StokKodu = dr["StokKodu"].ToString();
                            yt.UrunAdi = dr["UrunAdi"].ToString();
                            yt.Birimi = dr["Birimi"].ToString();

                            yt.ID = Convert.ToInt32(dr["ID"]);
                            try
                            {
                                yt.StoktaKalan = Convert.ToDecimal(dr["StoktaKalan"]);
                            }
                            catch (Exception)
                            {


                            }



                            yonetim.Add(yt);

                        }
                    }
                }
            }





            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region KdvRapor

        public ActionResult KdvRapor()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetKdvRapor()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<KDVRaporuTL> yonetim = new List<KDVRaporuTL>();



            string sorg = @"set dateformat dmy; select * from kdvraporuTL ";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            KDVRaporuTL yt = new KDVRaporuTL();

                            try
                            {

                                yt.Sat__Tutar = Convert.ToDecimal(dr["Sat__Tutar"]);
                            }
                            catch (Exception)
                            {
                                yt.Sat__Tutar = 0;

                            }

                            try
                            {

                                yt.Sat__KDV_Tutar = Convert.ToDecimal(dr["Sat__KDV_Tutar"]);
                            }
                            catch (Exception)
                            {
                                yt.Sat__KDV_Tutar = 0;

                            }
                            try
                            {

                                yt.Al__Tutar = Convert.ToDecimal(dr["Al__Tutar"]);
                            }
                            catch (Exception)
                            {
                                yt.Al__Tutar = 0;

                            }

                            try
                            {

                                yt.Al__KDV_Tutar = Convert.ToDecimal(dr["Al__KDV_Tutar"]);
                            }
                            catch (Exception)
                            {
                                yt.Al__KDV_Tutar = 0;

                            }

                            try
                            {

                                yt.KDV_Fark = Convert.ToDecimal(dr["KDV_Fark"]);
                            }
                            catch (Exception)
                            {

                                yt.KDV_Fark = 0;
                            }
                            try
                            {

                                yt.Sat___18_KDV_Tutar = Convert.ToDecimal(dr["Sat___18_KDV_Tutar"]);
                            }
                            catch (Exception)
                            {
                                yt.Sat___18_KDV_Tutar = 0;

                            }
                            try
                            {

                                yt.Sat___8_KDV_Tutar = Convert.ToDecimal(dr["Sat___8_KDV_Tutar"]);
                            }
                            catch (Exception)
                            {

                                yt.Sat___8_KDV_Tutar = 0;
                            }
                            try
                            {

                                yt.Sat___1_KDV_Tutar = Convert.ToDecimal(dr["Sat___1_KDV_Tutar"]);
                            }
                            catch (Exception)
                            {

                                yt.Sat___1_KDV_Tutar = 0;
                            }

                            try
                            {

                                yt.Al___18_KDV_Tutar = Convert.ToDecimal(dr["Al___18_KDV_Tutar"]);
                            }
                            catch (Exception)
                            {

                                yt.Al___18_KDV_Tutar = 0;
                            }
                            try
                            {

                                yt.Al___8_KDV_Tutar = Convert.ToDecimal(dr["Al___8_KDV_Tutar"]);
                            }
                            catch (Exception)
                            {
                                yt.Al___8_KDV_Tutar = 0;

                            }
                            try
                            {

                                yt.Al___1_KDV_Tutar = Convert.ToDecimal(dr["Al___1_KDV_Tutar"]);
                            }
                            catch (Exception)
                            {
                                yt.Al___1_KDV_Tutar = 0;

                            }

                            if(dr["Donem"].ToString() == "?UBAT") { 
                            yt.Donem = "ŞUBAT";
                            }
                            else if (dr["Donem"].ToString() == "A?USTOS")
                            {
                                yt.Donem = "AĞUSTOS";
                            }
                            else if (dr["Donem"].ToString() == "EK?M")
                            {
                                yt.Donem = "EKİM";
                            }
                            else if (dr["Donem"].ToString() == "EYL?L")
                            {
                                yt.Donem = "EYLÜL";
                            }
                            else if (dr["Donem"].ToString() == "HAZ?RAN")
                            {
                                yt.Donem = "HAZİRAN";
                            }
                            else if (dr["Donem"].ToString() == "N?SAN")
                            {
                                yt.Donem = "NİSAN";
                            }
                            else
                            {
                                yt.Donem = dr["Donem"].ToString();
                            }
                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region FaturaRaporları

        public ActionResult FaturaRapor()
        {
            return View();
        }

        public class faturakarlililk
        {
           
            public string FaturaNo { get; set; }
            public string FaturaTipi { get; set; }
            public string CariKodu { get; set; }
            public string CariUnvan { get; set; }
            public string NetTutar { get; set; }
            public string Tutar { get; set; }
            public string Maliyet { get; set; }
            public string FaturaKari { get; set; }
            public string KodDetayi { get; set; }
            public Nullable<System.DateTime> Tarih { get; set; }
            public int ID { get; set; }
        }
        public class FaturaAylikSatis
        {
            public int ID { get; set; }
            public string Donem { get; set; }
            public string Brut { get; set; }
            public string GenelToplam { get; set; }
            public string Indirim { get; set; }
            public string KDV { get; set; }
            public string Maliyet { get; set; }
            public string Karlilik { get; set; }
            public string FaturaTahsilati { get; set; }
            public string Kalan { get; set; }
            public string donemyil { get; set; }
        }

        [HttpGet]
        public ActionResult GetFaturaKarlilik()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<faturakarlililk> yonetim = new List<faturakarlililk>();



            string sorg = @"set dateformat dmy; SELECT Convert(datetime, INVOICE.FaturaTarihi) AS Tarih, INVOICE.FaturaNo AS [Fatura No], INVOICE.FaturaType AS [Fatura Tipi], INVOICE.CariKodu AS [Cari Kodu], INVOICE.CariUnvan AS [Cari Ünvanı],
          coalesce(INVOICE.NetToplam * INVOICE.FKuru, 0) AS NetTutar, coalesce(INVOICE.GenelToplam * INVOICE.FKuru, 0) AS Tutarı, Coalesce(tmaliyet, 0)  AS Maliyet, Coalesce(coalesce(INVOICE.NetToplam * INVOICE.FKuru, 0) - (Coalesce(tmaliyet, 0)), 0) AS[Fatura Kârı], (0)
           AS[Ürün Sayısı], (Select KodAdi FROM OzelKod WHERE ID = FaturaOzelKod) AS[Kod Detayı] FROM INVOICE
        WHERE(INVOICE.FaturaType = N'Satış Faturası') AND
      (INVOICE.Durumu = N'Aktif') and FirmaID ="+ Session["FirmaID"].ToString();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            faturakarlililk yt = new faturakarlililk();

                            yt.Tarih = Convert.ToDateTime(Convert.ToDateTime(dr["Tarih"].ToString()).ToString("dd.MM.yyyy"));
                            yt.FaturaNo = dr["Fatura No"].ToString();
                            yt.FaturaTipi = dr["Fatura Tipi"].ToString();
                            yt.CariKodu = dr["Cari Kodu"].ToString();
                            yt.CariUnvan = dr["Cari Ünvanı"].ToString();
                            yt.NetTutar = Convert.ToDecimal(dr["NetTutar"].ToString()).ToString("N2");
                            yt.Tutar = Convert.ToDecimal(dr["Tutarı"].ToString()).ToString("N2");
                            yt.Maliyet = Convert.ToDecimal(dr["Maliyet"].ToString()).ToString("N2");
                            yt.FaturaKari = Convert.ToDecimal(dr["Fatura Kârı"].ToString()).ToString("N2");
                            if(dr["Kod Detayı"].ToString() != null) { 
                            yt.KodDetayi = dr["Kod Detayı"].ToString();
                            }
                            else
                            {
                                yt.KodDetayi = "";
                            }
                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetFaturaAylik()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<FaturaAylikSatis> yonetim = new List<FaturaAylikSatis>();



            string sorg = @"set dateformat dmy; set language Turkish select DATENAME(MONTH, CONVERT(datetime, RAPORTOPLAM.Tarih)) AS [Dönem],  
                     Sum(RAPORTOPLAM.Brut) AS Brüt, Sum(RAPORTOPLAM.GenelToplam) AS[Genel Toplam], Sum(RAPORTOPLAM.toplamIskonta) AS[İndirim],   
 Sum(RAPORTOPLAM.TKdv) AS KDV, sum(RAPORTOPLAM.tOIV) AS ÖİV, Sum(RAPORTOPLAM.tOTV) ÖTV, sum(RAPORTOPLAM.Maliyet) AS Maliyet,
 sum(RAPORTOPLAM.Kârlılık) AS Kârlılık, SUM(RAPORTOPLAM.Ödenen) AS [Fatura Tahsilatı],  
 SUM(RAPORTOPLAM.GenelToplam - RAPORTOPLAM.Ödenen) AS Kalan, DATEPART(M, CONVERT(datetime, RAPORTOPLAM.Tarih)) AS M,
 SUM(RAPORTOPLAM.tOTV + RAPORTOPLAM.TKdv + RAPORTOPLAM.tOIV) AS[Vergilerin Toplamı], COUNT(*) AS[Belge Adedi], 
 RAPORTOPLAM.Donem AS [Dönem / Yıl]  
 FROM RAPORTOPLAM  where FaturaType =N'Satış Faturası' and FirmaID="+Session["FirmaID"].ToString() +@" GROUP BY DATEPART(M, CONVERT(datetime, RAPORTOPLAM.Tarih)), 
 DATENAME(MONTH, CONVERT(datetime, RAPORTOPLAM.Tarih)), RAPORTOPLAM.Donem  
 ORDER BY DATEPART(M, CONVERT(datetime, RAPORTOPLAM.Tarih))";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            FaturaAylikSatis yt = new FaturaAylikSatis();

                            //yt.ID=Convert.ToInt32(dr["ID"].ToString());
                            yt.Donem = dr["Dönem"].ToString();
                            yt.Brut = Convert.ToDecimal(dr["Brüt"].ToString()).ToString("N2");
                            yt.GenelToplam = Convert.ToDecimal(dr["Genel Toplam"].ToString()).ToString("N2");
                            yt.Indirim = Convert.ToDecimal(dr["İndirim"].ToString()).ToString("N2");
                            yt.KDV = Convert.ToDecimal(dr["KDV"].ToString()).ToString("N2");
                            yt.Maliyet = Convert.ToDecimal(dr["Maliyet"].ToString()).ToString("N2");
                            yt.Karlilik = Convert.ToDecimal(dr["Kârlılık"].ToString()).ToString("N2");
                            yt.FaturaTahsilati = Convert.ToDecimal(dr["Fatura Tahsilatı"].ToString()).ToString("N2");
                            yt.Kalan = Convert.ToDecimal(dr["Kalan"].ToString()).ToString("N2");
                            yt.donemyil =dr["Dönem / Yıl"].ToString();


                            yonetim.Add(yt);

                        }
                    }
                }
            }





            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetFaturaSatis()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<FaturaAylikSatis> yonetim = new List<FaturaAylikSatis>();



            string sorg = @"set dateformat dmy; set language Turkish select DATENAME(MONTH, CONVERT(datetime, RAPORTOPLAM.Tarih)) AS [Dönem],  
                     Sum(RAPORTOPLAM.Brut) AS Brüt, Sum(RAPORTOPLAM.GenelToplam) AS[Genel Toplam], Sum(RAPORTOPLAM.toplamIskonta) AS[İndirim],   
 Sum(RAPORTOPLAM.TKdv) AS KDV, sum(RAPORTOPLAM.tOIV) AS ÖİV, Sum(RAPORTOPLAM.tOTV) ÖTV, sum(RAPORTOPLAM.Maliyet) AS Maliyet,
 sum(RAPORTOPLAM.Kârlılık) AS Kârlılık, SUM(RAPORTOPLAM.Ödenen) AS [Fatura Tahsilatı],  
 SUM(RAPORTOPLAM.GenelToplam - RAPORTOPLAM.Ödenen) AS Kalan, DATEPART(M, CONVERT(datetime, RAPORTOPLAM.Tarih)) AS M,
 SUM(RAPORTOPLAM.tOTV + RAPORTOPLAM.TKdv + RAPORTOPLAM.tOIV) AS[Vergilerin Toplamı], COUNT(*) AS[Belge Adedi], 
 RAPORTOPLAM.Donem AS [Dönem / Yıl]  
 FROM RAPORTOPLAM  where FaturaType =N'Alış Faturası'  and FirmaID="+Session["FirmaID"].ToString()+@"GROUP BY DATEPART(M, CONVERT(datetime, RAPORTOPLAM.Tarih)), 
 DATENAME(MONTH, CONVERT(datetime, RAPORTOPLAM.Tarih)), RAPORTOPLAM.Donem  
 ORDER BY DATEPART(M, CONVERT(datetime, RAPORTOPLAM.Tarih))";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            FaturaAylikSatis yt = new FaturaAylikSatis();

                            yt.Donem = dr["Dönem"].ToString();
                            yt.Brut = Convert.ToDecimal(dr["Brüt"].ToString()).ToString("N2");
                            yt.GenelToplam = Convert.ToDecimal(dr["Genel Toplam"].ToString()).ToString("N2");
                            yt.Indirim = Convert.ToDecimal(dr["İndirim"].ToString()).ToString("N2");
                            yt.KDV = Convert.ToDecimal(dr["KDV"].ToString()).ToString("N2");
                            
                            yt.FaturaTahsilati = Convert.ToDecimal(dr["Fatura Tahsilatı"].ToString()).ToString("N2");
                            yt.donemyil = dr["Dönem / Yıl"].ToString();


                            yonetim.Add(yt);

                        }
                    }
                }
            }




            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region FinansalRaporlar

        public ActionResult FinansalRapolar()
        {
            return View();
        }
        #endregion

        #region ÇekSenetRaporları

        public ActionResult CekSenetRaporlar()
        {
            return View();
        }
        #endregion

        #region PersonelRaporlar

        public ActionResult PersonelRaporlar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAlacakOdeme()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<AlacakOdemeRapor> yonetim = new List<AlacakOdemeRapor>();
            string sorg = @"set dateformat dmy;  set language turkish; 
              SELECT 'P' as [-],PERSONEL_HAREKET.Islem AS [Alacak/Ödeme Tipi],(Select Adi+' '+Soyadi from Personel where ID=AOPersonel) 
			  as [Personel], PERSONEL_HAREKET.IslemTarih AS Tarih,IslemNo AS [İşlem No],  Tutar as Tutar, case when  ParaBirimi = 'Varsayılan' then 'TL' else ParaBirimi end AS Birim,
Aciklama AS Açıklama,Tutar as TutarDecimal,PERSONEL_HAREKET.AOPersonel,IslemTipi,Donem,ID,Tablo,PozisyonNo FROM PERSONEL_HAREKET  
                    where AOPersonel<>-1  order by[Tarih]";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            AlacakOdemeRapor yt = new AlacakOdemeRapor();

                            yt.AlacakOdeme = dr["Alacak/Ödeme Tipi"].ToString();
                            yt.Personel = dr["Personel"].ToString();
                            yt.Tarih = dr["Tarih"].ToString();
                            yt.IslemNo = dr["İşlem No"].ToString();
                            yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                            yt.Birim = dr["Birim"].ToString();
                            yt.Aciklama = dr["Açıklama"].ToString();
                            yt.PozisyonNo = dr["PozisyonNo"].ToString();
                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBorcAlacak()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<BorcAlacakRapor> yonetim = new List<BorcAlacakRapor>();
            string sorg = @"set dateformat dmy;  set language turkish; 
                select Adi+' '+Soyadi as [Adı Soyadı],Borc as Borç,Alacak ,convert(varchar(50), Alacak-Borc) as Bakiye,ID, 
			  Alacak-Borc as BakiyeDecimal from Personel where ID is not null";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            BorcAlacakRapor yt = new BorcAlacakRapor();

                            yt.ID = Convert.ToInt32(dr["ID"].ToString());
                            yt.AdiSoyadi = dr["Adı Soyadı"].ToString();
                            yt.Borc = Convert.ToDecimal(dr["Borç"].ToString()).ToString("N2");
                            yt.Alacak = Convert.ToDecimal(dr["Alacak"].ToString()).ToString("N2");
                            yt.Bakiye = Convert.ToDecimal(dr["Bakiye"].ToString()).ToString("N2");
                            
                   
                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSatisRaporu()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<SatisRapor> yonetim = new List<SatisRapor>();
            string sorg = @"set dateformat dmy;  set language turkish;  
                  select CASE WHEN(Select FirmaKodu from Cari where ID = INVOICE.CariID) <> '' THEN(Select FirmaKodu from Cari where ID = INVOICE.CariID) 
                  WHEN(Select FirmaKodu from Cari where ID = RETAIL.CariID) <> '' THEN(Select FirmaKodu from Cari where ID = RETAIL.CariID) END as [Müşteri Kodu], 

                 CASE WHEN (Select CariUnvan from Cari where ID = INVOICE.CariID) <> '' THEN(Select CariUnvan from Cari where ID = INVOICE.CariID) 
                 WHEN(Select CariUnvan from Cari where ID = RETAIL.CariID) <> '' THEN(Select CariUnvan from Cari where ID = RETAIL.CariID) END as [Müşteri Adı], 

                 coalesce(Count(RETAIL.ID), 0) + coalesce(Count(INVOICE.ID), 0) as [Fatura Adedi], 

                 coalesce(Sum(GenelTutar * kUR), 0) + coalesce(Sum(GenelToplam * FKuru), 0) as [Fatura Toplamı], (Select coalesce(Sum(Tutar * exRate), 0) from CASH_PAY where CASH_PAY.CariID = RETAIL.CariID 
                 and(IslemTipi = 'T') ) as [Nakit toplamı],(Select coalesce(Sum(Tutar * exRate), 0) from CASH_PAY where CASH_PAY.CariID = RETAIL.CariID 

                 and(IslemTipi = 'KKT')  )as [Kredi Kartı Toplamı] ,(Select coalesce(Count(CASH_PAY.ID), 0) from CASH_PAY where CASH_PAY.CariID = RETAIL.CariID and(IslemTipi = 'KKT' or IslemTipi = 'T') ) 

                  as [Tahsilat Adedi],(Select coalesce(Sum(Tutar * exRate), 0) from CASH_PAY where CASH_PAY.CariID = RETAIL.CariID 
                 and(IslemTipi = 'KKT' or IslemTipi = 'T'  ) )as [Tahsilat Toplamı], CASE WHEN RETAIL.CariID is not null THEN RETAIL.CariID else INVOICE.CariID end as CariID  , 

                (Select coalesce(BALANCE.BorcB - BALANCE.AlacakB, 0) from BALANCE where (BALANCE.CariID = RETAIL.CariID or BALANCE.CariID = INVOICE.CariID ) 
            	and BALANCE.paraBirimi = 'TL') as Bakiye 
                from RETAIL FULL OUTER JOIN INVOICE ON INVOICE.CariID = RETAIL.CariID where ((INVOICE.FaturaType = N'Satış Faturası' ) or (RETAIL.IslemTipi = N'Perakende Satış')) 

                  group by RETAIL.CariID,INVOICE.CariID";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            SatisRapor yt = new SatisRapor();

                            yt.MusteriKodu = dr["Müşteri Kodu"].ToString();
                            yt.MusteriAdi = dr["Müşteri Adı"].ToString();
                            yt.FaturaAdedi = dr["Fatura Adedi"].ToString();
                            yt.FaturaToplami = Convert.ToDecimal(dr["Fatura Toplamı"].ToString()).ToString("N2");
                            yt.NakitToplami = Convert.ToDecimal(dr["Nakit toplamı"].ToString()).ToString("N2");
                            yt.KKToplami = Convert.ToDecimal(dr["Kredi Kartı Toplamı"].ToString()).ToString("N2");
                            yt.TahsilatToplami = Convert.ToDecimal(dr["Tahsilat Toplamı"].ToString()).ToString("N2");
                            yt.TahsilatAdedi = dr["Tahsilat Adedi"].ToString();


                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetKasiyerAnalizi()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<KasiyerSatis> yonetim = new List<KasiyerSatis>();
            string sorg = @"set dateformat dmy; select  'KAS' as [-], (select Adi+' '+Soyadi from Personel where ID=T.PersonelID) as Personel,COUNT(T.PersonelID) as [Satış Adedi],  
                 convert(varchar(50), SUM(T.GenelTutar * T.Kur)) as [Satış Tutarı], 
                 convert(varchar(50), (select Coalesce(Sum((Tutar*RETAIL_DETAIL.Kur*PrimYuzdesi)/100),0) from RETAIL_DETAIL, RETAIL where SatisID = RETAIL.ID and RETAIL.PersonelID = T.PersonelID and RETAIL.IslemTipi=N'Perakende Satış')) as [Prim Tutarı], 
                 PersonelID, SUM(T.GenelTutar * T.Kur) as ToplamDecimal, 
                 (select Coalesce(Sum((Tutar*RETAIL_DETAIL.Kur*PrimYuzdesi)/100),0) from RETAIL_DETAIL, RETAIL where SatisID = RETAIL.ID and RETAIL.PersonelID = T.PersonelID and RETAIL.IslemTipi=N'Perakende Satış') as PrimDecimal, 
                 convert(varchar(50), (select Coalesce(Sum(Maliyet),0) from RETAIL_DETAIL, RETAIL where SatisID = RETAIL.ID and RETAIL.PersonelID = T.PersonelID and RETAIL.IslemTipi=N'Perakende Satış')) as [Maliyet], 
                   (select Coalesce(Sum(Maliyet),0) from RETAIL_DETAIL, RETAIL where SatisID = RETAIL.ID and RETAIL.PersonelID = T.PersonelID and RETAIL.IslemTipi=N'Perakende Satış') as [MaliyetDecimal], 
                  convert(varchar(50), (select SUM(T.GenelTutar * T.Kur) - Coalesce(Sum(Maliyet),0) from RETAIL_DETAIL, RETAIL where SatisID = RETAIL.ID and RETAIL.PersonelID = T.PersonelID and RETAIL.IslemTipi=N'Perakende Satış'))  as [Kârlılık] ,  
                   (select SUM(T.GenelTutar * T.Kur) - Coalesce(Sum(Maliyet),0) from RETAIL_DETAIL, RETAIL where SatisID = RETAIL.ID and RETAIL.PersonelID = T.PersonelID and RETAIL.IslemTipi=N'Perakende Satış')  
                  as [KârlılıkDecimal] from RETAIL T,Personel where T.PersonelID = Personel.ID and T.IslemTipi=N'Perakende Satış'  
           

                 group by T.PersonelID";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            KasiyerSatis yt = new KasiyerSatis();

                            yt.durum = dr["-"].ToString();
                            yt.Personel = dr["Personel"].ToString();
                            yt.SatisAdedi = dr["Satış Adedi"].ToString();
                            yt.SatisTutari = Convert.ToDecimal(dr["Satış Tutarı"]).ToString("N2");
                            yt.Prim = Convert.ToDecimal(dr["Prim Tutarı"]).ToString("N2");
                            yt.Maliyet = Convert.ToDecimal(dr["Maliyet"]).ToString("N2");
                            yt.Karlilik = Convert.ToDecimal(dr["Kârlılık"]).ToString("N2");


                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region CekSenetRapor

        public ActionResult CekSenetRapor(CekSenetRapor ck)
        {
            return RedirectToAction("Raporlar", "Raporlar");
        }
        #endregion

        #region BABS
        public ActionResult BaBs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetBaBs(string Yil,string Ay,string PB)
        {   string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<BaBsRapor> yonetim = new List<BaBsRapor>();
            int ay1 = Convert.ToInt32(Ay);
            string selectTutar = " Sum(NetToplam * Fkuru) ";
            string Count = " Count(INVOICE.ID) ";

            if (PB != "") selectTutar = " Sum(NetToplam) ";
            string pb = " AND year(CONVERT(datetime, faturaTarihi))='" + Yil + "' ";

            if (PB != "")
            {
                pb = " AND year(CONVERT(datetime, faturaTarihi))='" + Yil + "' AND INVOICE.ParaBirimi='" + PB + "' ";
            }

            string sorg = "set dateformat dmy ; SELECT FirmaKodu  as [Firma Kodu], CariUnvan as [Ünvanı] , " +
" IL as [Şehir] , ILce as [İLÇE] ,T.ID ," +
" (Select " + selectTutar + " From INVOICE where CariID = t.ID and (FaturaType=N'Alım Faturası' or FaturaType=N'Satış İade Faturası' or FaturaType=N'Alınan Fiyat Farkı Faturası') and MONTH(CONVERT(datetime, faturaTarihi)) = " + Convert.ToInt32(ay1) + " " + pb + ") as [BA_Tutar]," +
" (Select " + Count + " From INVOICE where CariID = t.ID and (FaturaType=N'Alım Faturası' or FaturaType=N'Satış İade Faturası' or FaturaType=N'Alınan Fiyat Farkı Faturası') and MONTH(CONVERT(datetime, faturaTarihi)) = " + Convert.ToInt32(ay1) + "  " + pb + ") as [BA_Belge Adedi]," +
"(Select " + selectTutar + " From INVOICE where CariID = t.ID and (FaturaType=N'Satış Faturası' or FaturaType=N'Alım İade Faturası' or FaturaType=N'Verilen Fiyat Farkı Faturası') and MONTH(CONVERT(datetime, faturaTarihi)) = " + Convert.ToInt32(ay1) + "  " + pb + ") as [BS_Tutar]," +
"(Select " + Count + " From INVOICE where CariID = t.ID and (FaturaType=N'Satış Faturası' or FaturaType=N'Alım İade Faturası' or FaturaType=N'Verilen Fiyat Farkı Faturası')and MONTH(CONVERT(datetime, faturaTarihi)) = " + Convert.ToInt32(ay1) + "  " + pb + ") as [BS_Belge Adedi]" +
"  FROM Cari T where ((Select Coalesce(" + selectTutar + ", 0) From INVOICE where CariID = t.ID and(FaturaType=N'Satış Faturası' or FaturaType=N'Alım İade Faturası' or FaturaType=N'Verilen Fiyat Farkı Faturası' )) > 0) or ((Select Coalesce(" + selectTutar + ", 0) From INVOICE where CariID = t.ID and(FaturaType=N'Alım Faturası' or FaturaType=N'Satış İade Faturası' or FaturaType=N'Alınan Fiyat Farkı Faturası' )) > 0)";

           
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            BaBsRapor yt = new BaBsRapor();

                            yt.BA_BelgeAdedi = dr["BA_Belge Adedi"].ToString();
                            try
                            {
                                yt.BA_Tutar = Convert.ToDecimal(dr["BA_Tutar"].ToString()).ToString("N2");

                            }
                            catch
                            {
                                yt.BA_Tutar = "0.00";
                            }
                            yt.FirmaKodu = dr["Firma Kodu"].ToString();
                            yt.Unvan = dr["Ünvanı"].ToString();

                            yt.Ilce = dr["İLÇE"].ToString();
                            yt.Sehir = dr["Şehir"].ToString();
                            try
                            {
                                yt.BS_Tutar = Convert.ToDecimal(dr["BS_Tutar"].ToString()).ToString("N2");
                            }
                            catch
                            {
                                yt.BS_Tutar = "0.00";
                            }
                            yt.BS_BelgeAdedi = dr["BS_Belge Adedi"].ToString();

                            yonetim.Add(yt);

                        }
                    }
                }
            }





            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region AylıkOdemeTahsilat
        public ActionResult AylikOdemeRapor()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAylikOdeme()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //string FirmaID = Session["FirmaID"].ToString();
            List<AylikOdemeTahsilatRpr> yonetim = new List<AylikOdemeTahsilatRpr>();
            string sorg = @"SET dateformat dmy; Select Tipi,T.IslemNo as [İşlem No],T.IslemTipi as [İşlem Tipi],T.AlacakBorctipi as [Hesap/Tür],T.VadeTarihi as [Vade Tarihi],T.AlacakTutar as [Alacak Tutarı],T.OdemeTutar as [Ödeme Tutarı],T.PB,T.AlacakTutarTL as [Alacak Tutarı TL],
T.OdemeTutarTL as [Ödeme Tutarı TL],T.Aciklama as Açıklama from AylikOdemeTahsilat T ORDER BY [Vade Tarihi] ";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            AylikOdemeTahsilatRpr yt = new AylikOdemeTahsilatRpr();

                            yt.Tipi = dr["Tipi"].ToString();
                            yt.IslemTipi = dr["İşlem Tipi"].ToString();
                            yt.IslemNo = dr["İşlem No"].ToString();
                            yt.HesapTur = dr["Hesap/Tür"].ToString();
                            yt.VadeTarihi = Convert.ToDateTime(dr["Vade Tarihi"].ToString()).ToString("dd.MM.yyyy");

                            yt.Alacak = Convert.ToDecimal(dr["Alacak Tutarı"]).ToString("N2");
                            yt.Odeme = Convert.ToDecimal(dr["Ödeme Tutarı"]).ToString("N2");
                            yt.PB = dr["PB"].ToString();
                            yt.Aciklama = dr["Açıklama"].ToString();


                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region TeknikServisMalzeme

        public ActionResult MalzemeList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetMalzemeList()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
          


            List<MalzemelerList> MalzemelerList = new List<MalzemelerList>();

            string sorg = @" set dateformat dmy ; select IslemTarihi,(select (select CariUnvan from Cari c where c.ID = t.CariID) as CariUnvan from TECHNICAL t where t.ID = td.CihazID) as CariUnvan ,


(select UrunAdi from Stok s where s.ID = td.UrunID) as UrunAdi,Miktar,
(select ServisIslemNo from TECHNICAL t where t.ID = td.CihazID ) as ServisNo,Fiyat,
(Fiyat * Miktar) as Tutar,pBirimi

from TECHNICAL_DETAIL td where FirmaID=" + Session["FirmaID"].ToString();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand maasgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = maasgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            MalzemelerList malzeme = new MalzemelerList();


                            malzeme.CariUnvan = dr["CariUnvan"].ToString();
                            malzeme.UrunAdi = dr["UrunAdi"].ToString();
                            malzeme.ServisNo = dr["ServisNo"].ToString();
                            malzeme.ParaBirimi = dr["pBirimi"].ToString();
                            malzeme.Tarih = Convert.ToDateTime(dr["IslemTarihi"].ToString()).ToString("dd.MM.yyyy");
                            malzeme.Fiyat = Convert.ToDecimal(dr["Fiyat"].ToString()).ToString("N2");
                            malzeme.Miktar = Convert.ToDecimal(dr["Miktar"].ToString()).ToString("N2");
                            malzeme.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");


                            MalzemelerList.Add(malzeme);

                        }
                    }
                }
            }
            return Json(new { data = MalzemelerList.ToList() }, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}