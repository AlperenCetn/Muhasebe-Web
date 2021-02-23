using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SaTeknopark_MVC5.Models;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class PerakendeController : Controller
    {

        sayazilimEntities db = new sayazilimEntities();

        // GET: Perakende
        public ActionResult Index()
        {

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string SorguFirmaID = Session["FirmaID"].ToString();
            string TLSorgu = @"select top 5 c.ID,c.CariUnvan,c.paraBirimi, SUM(alacakB-borcB) as [Bakiye] 
                            from Cari C INNER JOIN BALANCE B ON cariID=C.ID and C.paraBirimi='TL' and C.FirmaID =" +
                             SorguFirmaID +
                             @"group by c.CariUnvan,c.ID,c.paraBirimi having SUM(alacakB-borcB) <= 0
                            order by  [Bakiye]";
            string EuroSorgu = @"select top 5 c.ID,c.CariUnvan,c.paraBirimi, SUM(alacakB-borcB) as [Bakiye] 
                            from Cari C INNER JOIN BALANCE B ON cariID=C.ID and C.paraBirimi='EUR'  and C.FirmaID =" +
                               SorguFirmaID +
                               @"group by c.CariUnvan,c.ID,c.paraBirimi having SUM(alacakB-borcB) <= 0
                             order by  [Bakiye]";
            string USDSorgu = @"select top 5 c.ID,c.CariUnvan,c.paraBirimi, SUM(alacakB-borcB) as [Bakiye] 
                            from Cari C INNER JOIN BALANCE B ON cariID=C.ID and C.paraBirimi='USD'  and C.FirmaID =" +
                              SorguFirmaID +
                              @"group by c.CariUnvan,c.ID,c.paraBirimi having SUM(alacakB-borcB) <= 0
                            order by  [Bakiye]";

            #region ViewBagler

            int i = 0;
            string bakiye1 = "";
            int bakiye = 0;
            ViewBag.TLName1 = "";
            ViewBag.TlBakiye1 = 0;
            ViewBag.TLName2 = "";
            ViewBag.TlBakiye2 = 0;
            ViewBag.TLName3 = "";
            ViewBag.TlBakiye3 = 0;
            ViewBag.TLName4 = "";
            ViewBag.TlBakiye4 = 0;
            ViewBag.TLName5 = "";
            ViewBag.TlBakiye5 = 0;
            ViewBag.USDName1 = "";
            ViewBag.USDBakiye1 = 0;
            ViewBag.USDName2 = "";
            ViewBag.USDBakiye2 = 0;
            ViewBag.USDName3 = "";
            ViewBag.USDBakiye3 = 0;
            ViewBag.USDName4 = "";
            ViewBag.USDBakiye4 = 0;
            ViewBag.USDName5 = "";
            ViewBag.USDBakiye5 = 0;
            ViewBag.EURName1 = "";
            ViewBag.EURBakiye1 = 0;
            ViewBag.EURName2 = "";
            ViewBag.EURBakiye2 = 0;
            ViewBag.EURName3 = "";
            ViewBag.EURBakiye3 = 0;
            ViewBag.EURName4 = "";
            ViewBag.EURBakiye4 = 0;
            ViewBag.EURName5 = "";
            ViewBag.EURBakiye5 = 0;
            ViewBag.TlBankaName = "";
            ViewBag.TlBankaBakiye = 0;
            ViewBag.EurBankaName = "";
            ViewBag.EurBankaBakiye = "";
            ViewBag.UsdBankaName = "";
            ViewBag.UsdBankaBakiye = 0;
            ViewBag.TlKasaName = "";
            ViewBag.TlKasaBakiye = 0;
            ViewBag.EurKasaName = "";
            ViewBag.EurKasaBakiye = "";
            ViewBag.UsdKasaName = "";
            ViewBag.UsdKasaBakiye = 0;



            #endregion

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    i = 0;
                    con.Open();
                    using (SqlCommand carigetir = new SqlCommand(TLSorgu, con))
                    {
                        using (SqlDataReader dr = carigetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (i == 0)
                                {

                                    ViewBag.TLName1 = dr["CariUnvan"].ToString();

                                    ViewBag.TlBakiye1 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.TlBakiye1 = dr["Bakiye"].ToString();
                                    }


                                }

                                if (i == 1)
                                {

                                    ViewBag.TLName2 = dr["CariUnvan"].ToString();


                                    ViewBag.TlBakiye2 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.TlBakiye2 = dr["Bakiye"].ToString();
                                    }


                                }

                                if (i == 2)
                                {
                                    ViewBag.TLName3 = dr["CariUnvan"].ToString();

                                    ViewBag.TlBakiye3 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.TlBakiye3 = dr["Bakiye"].ToString();
                                    }


                                }

                                if (i == 3)
                                {
                                    ViewBag.TLName4 = dr["CariUnvan"].ToString();

                                    ViewBag.TlBakiye4 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.TlBakiye4 = dr["Bakiye"].ToString();
                                    }


                                }

                                if (i == 4)
                                {
                                    ViewBag.TLName5 = dr["CariUnvan"].ToString();

                                    ViewBag.TlBakiye5 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.TlBakiye5 = dr["Bakiye"].ToString();
                                    }
                                }

                                i++;




                            }
                        }
                    }

                }

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    i = 0;
                    con.Open();
                    using (SqlCommand carigetir = new SqlCommand(USDSorgu, con))
                    {
                        using (SqlDataReader dr = carigetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (i == 0)
                                {
                                    ViewBag.USDName1 = dr["CariUnvan"].ToString();
                                    ViewBag.USDBakiye1 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.USDBakiye1 = dr["Bakiye"].ToString();
                                    }

                                }

                                if (i == 1)
                                {
                                    ViewBag.USDName2 = dr["CariUnvan"].ToString();
                                    ViewBag.USDBakiye2 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.USDBakiye2 = dr["Bakiye"].ToString();
                                    }

                                }

                                if (i == 2)
                                {
                                    ViewBag.USDName3 = dr["CariUnvan"].ToString();
                                    ViewBag.USDBakiye3 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.USDBakiye3 = dr["Bakiye"].ToString();
                                    }

                                }

                                if (i == 3)
                                {
                                    ViewBag.USDName4 = dr["CariUnvan"].ToString();
                                    ViewBag.USDBakiye4 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.USDBakiye4 = dr["Bakiye"].ToString();
                                    }

                                }

                                if (i == 4)
                                {
                                    ViewBag.USDName5 = dr["CariUnvan"].ToString();
                                    ViewBag.USDBakiye5 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.USDBakiye5 = dr["Bakiye"].ToString();
                                    }

                                }

                                i++;




                            }
                        }
                    }

                }

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    i = 0;
                    con.Open();
                    using (SqlCommand carigetir = new SqlCommand(EuroSorgu, con))
                    {
                        using (SqlDataReader dr = carigetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (i == 0)
                                {
                                    ViewBag.EURName1 = dr["CariUnvan"].ToString();
                                    ViewBag.EURBakiye1 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.EURBakiye1 = dr["Bakiye"].ToString();
                                    }

                                }

                                if (i == 1)
                                {
                                    ViewBag.EURName2 = dr["CariUnvan"].ToString();
                                    ViewBag.EURBakiye2 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.EURBakiye2 = dr["Bakiye"].ToString();
                                    }

                                }

                                if (i == 2)
                                {
                                    ViewBag.EURName3 = dr["CariUnvan"].ToString();
                                    ViewBag.EURBakiye3 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.EURBakiye3 = dr["Bakiye"].ToString();
                                    }

                                }

                                if (i == 3)
                                {
                                    ViewBag.EURName4 = dr["CariUnvan"].ToString();
                                    ViewBag.EURBakiye4 = dr["Bakiye"].ToString().Remove(0, 1);
                                    if (dr["Bakiye"].ToString() == "0,000000")
                                    {
                                        ViewBag.EURBakiye4 = dr["Bakiye"].ToString();
                                    }

                                }

                                if (i == 4)
                                {
                                    ViewBag.EURName5 = dr["CariUnvan"].ToString();
                                    ViewBag.EURBakiye5 = dr["Bakiye"].ToString().Remove(0, 1);

                                    if (dr["Bakiye"].ToString() != "0,000000")
                                    {
                                        ViewBag.EURBakiye5 = dr["Bakiye"].ToString();
                                    }

                                }


                                i++;




                            }
                        }
                    }

                    con.Close();
                }

            }
            catch (Exception e1)
            {
            }

            int FirmaID = 1;

            try
            {
                FirmaID = Convert.ToInt32(Session["FirmaID"].ToString());
            }
            catch (Exception e)
            {
                FirmaID = 1;


            }


            #region Faturalar




            sayazilimEntities db = new sayazilimEntities();


            var retaillist = db.RETAIL.ToList();
            decimal bugunluktoplam = 0;
            decimal haftalıktoplam = 0;
            foreach (var item in retaillist)
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                string datettime = Convert.ToDateTime(item.IslemTarih).ToString("yyyy-MM-dd");
                if (datettime == dt)
                {
                    bugunluktoplam += Convert.ToDecimal(item.ntoTutar);
                }

            }

            ViewBag.bugunluktoplam = bugunluktoplam.ToString("N2");
            string SonHaftaSrg = @"SELECT * FROM RETAIL 
WHERE  DATEDIFF(WEEK, IslemTarih, getdate()) BETWEEN 0 AND 2 ";
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand carigetir = new SqlCommand(SonHaftaSrg, con))
                {
                    using (SqlDataReader dr = carigetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string toplanacak = dr["ntoTutar"].ToString();
                            haftalıktoplam += Convert.ToDecimal(toplanacak);

                        }
                    }
                }

                con.Close();
            }

            ViewBag.haftalıktoplam = haftalıktoplam.ToString("N2");


            string nakithavale = @"SET DATEFORMAT DMY; select coalesce(sum(GenelTutar),0) as [Toplam Satış],
                                    coalesce(sum(ntoTutar),0) as [Toplam Nakit],
                                    coalesce(sum(hvlTutar),0) as [Toplam Havale],
                                    coalesce(sum(kktTutar),0) as [Toplam KK] ,
                                    coalesce(sum(ParaKartTahsilatTutar),0) as [Toplam Para Kart],
                                    coalesce(sum(TaksitTutar),0) as [Toplam Taksitli] ,
                                    coalesce(sum(S.SgkTutar),0) as [Toplam SGK],
                                    (Select sum(R.GenelTutar) from RETAIL R  where R.IslemTipi=N'Perakende Satış'  and CariID<>-1) as [Cari Satış] 
                                    from RETAIL S  where S.IslemTipi=N'Perakende Satış' 
   ";
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand carigetir = new SqlCommand(nakithavale, con))
                {
                    using (SqlDataReader dr = carigetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            decimal ToplamNakit = Convert.ToDecimal(dr["Toplam Nakit"].ToString());
                            ViewBag.ToplamNakit = ToplamNakit.ToString("N2") + " TL";
                            decimal ToplamHavale = Convert.ToDecimal(dr["Toplam Havale"].ToString());
                            ViewBag.ToplamHavale = ToplamHavale.ToString("N2") + " TL";
                            decimal ToplamKK = Convert.ToDecimal(dr["Toplam KK"].ToString());
                            ViewBag.ToplamKK = ToplamKK.ToString("N2") + " TL";
                            decimal ToplamParaKart = Convert.ToDecimal(dr["Toplam Para Kart"].ToString());
                            ViewBag.ToplamParaKart = ToplamParaKart.ToString("N2") + " TL";
                            try
                            {
                                decimal CariSatış = Convert.ToDecimal(dr["Cari Satış"].ToString());
                                ViewBag.CariSatış = CariSatış.ToString("N2") + " TL";
                            }
                            catch
                            {
                                ViewBag.CariSatış = "0,00" + " TL";
                            }
                        }
                    }
                }

                con.Close();
            }


            string query =
                @"set dateformat dmy ;Select (select  Coalesce(sum(T.Tutar*T.[İşlem Kuru]),0)  from HareketlerListesi T where  
            T.DugunYapildi = 1 and T.CariID <> -1  and T.BA = '(B)' and T.CariID IN(sELECT ID FROM Cari C where
            ((select Coalesce(sum(H.Tutar* H.[İşlem Kuru]),0) from HareketlerListesi H where H.DugunYapildi = 1 and H.BA = '(B)'and C.ID = H.CariID )-
                (select Coalesce(sum(H.Tutar * H.[İşlem Kuru]), 0) from HareketlerListesi H where H.DugunYapildi = 1 and H.BA = '(A)' and C.ID = H.CariID))> 0) ) -
                (select  Coalesce(sum(T.Tutar * T.[İşlem Kuru]), 0)  from HareketlerListesi T where T.DugunYapildi = 1 and T.CariID <> -1  and T.BA = '(A)'
            and T.CariID IN(sELECT ID FROM Cari C where  ((select Coalesce(sum(H.Tutar* H.[İşlem Kuru]),0) from HareketlerListesi H
                where H.DugunYapildi = 1 and H.BA = '(B)'and C.ID = H.CariID )-
                (select Coalesce(sum(H.Tutar * H.[İşlem Kuru]), 0) from HareketlerListesi H where H.DugunYapildi = 1 and H.BA = '(A)' and C.ID = H.CariID))> 0))  as Alacak";

            string query2 = "set dateformat dmy ;Select (select  Coalesce(sum(T.Tutar*T.[İşlem Kuru]),0) " +
                            " from HareketlerListesi T where T.DugunYapildi=1 and T.CariID<>-1 and T.BA='(B)' and T.CariID " +
                            " IN (sELECT ID FROM Cari C where  ((select Coalesce(sum(H.Tutar*H.[İşlem Kuru]),0) from HareketlerListesi H where H.DugunYapildi=1 and H.BA='(B)'and C.ID=H.CariID )- " +
                            " (select Coalesce(sum(H.Tutar*H.[İşlem Kuru]),0) from HareketlerListesi H where  H.DugunYapildi=1 and H.BA='(A)' and C.ID=H.CariID))<0) ) - " +


                            " (select  Coalesce(sum(T.Tutar*T.[İşlem Kuru]),0) " +
                            " from HareketlerListesi T where  T.DugunYapildi=1 and T.CariID<>-1 and T.BA='(A)' and T.CariID " +
                            " IN (sELECT ID FROM Cari C where  ((select Coalesce(sum(H.Tutar*H.[İşlem Kuru]),0) from HareketlerListesi H where H.DugunYapildi=1 and H.BA='(B)'and C.ID=H.CariID )- " +
                            " (select Coalesce(sum(H.Tutar*H.[İşlem Kuru]),0) from HareketlerListesi H where H.DugunYapildi=1 and H.BA='(A)' and C.ID=H.CariID))<0)) as borc";


            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand carigetir = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = carigetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            decimal CariAlacak = Convert.ToDecimal(dr["Alacak"].ToString());
                            ViewBag.CariAlacak = CariAlacak.ToString("N2") + " TL";

                        }
                    }
                }

                con.Close();
            }


            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand carigetir = new SqlCommand(query2, con))
                {
                    using (SqlDataReader dr = carigetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            decimal CariBorc = Convert.ToDecimal(dr["Borc"].ToString());
                            ViewBag.CariBorc = CariBorc.ToString("N2") + " TL";
                        }
                    }
                }

                con.Close();
            }


            var SatisFaturasi = db.INVOICE.Where(x => x.FirmaID == FirmaID & x.fType == "satisF").ToList();
            decimal toplam = 0;
            string BakiyeRemove = "";
            string bakiyemiktarı;
            int uzunluk = 0;

            #endregion

            #region BankaVeKasalar

            var BankaList = db.Banka.Where(x => x.FirmaID == FirmaID).ToList();

            foreach (var item in BankaList)
            {
                uzunluk = 0;
                BakiyeRemove = "";
                bakiyemiktarı = "";
                if (item.hnParaBirimi == "TL")
                {

                    ViewBag.TlBankaName = item.BankaAdi;
                    bakiyemiktarı = item.Bakiye.ToString();
                    uzunluk = item.Bakiye.ToString().Length;
                    bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                    ViewBag.TlBankaBakiye = bakiyemiktarı + " " + "TL";
                }
                else if (item.hnParaBirimi == "EUR")
                {
                    ViewBag.EurBankaName = item.BankaAdi;
                    bakiyemiktarı = item.Bakiye.ToString();
                    uzunluk = item.Bakiye.ToString().Length;
                    bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                    ViewBag.EurBankaBakiye = bakiyemiktarı + " " + "EUR";
                }
                else if (item.hnParaBirimi == "USD")
                {
                    ViewBag.UsdBankaName = item.BankaAdi;
                    bakiyemiktarı = item.Bakiye.ToString();
                    uzunluk = item.Bakiye.ToString().Length;
                    bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                    ViewBag.UsdBankaBakiye = bakiyemiktarı + " " + "USD";

                }

            }

            var KasaList = db.Kasa.Where(x => x.FirmaID == FirmaID).ToList();

            foreach (var item in KasaList)
            {
                if (item.ParaBirimi == "TL")
                {
                    ViewBag.TlKasaName = item.KasaAdi;
                    bakiyemiktarı = item.Bakiye.ToString();
                    uzunluk = item.Bakiye.ToString().Length;
                    bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                    ViewBag.TlKasaBakiye = bakiyemiktarı + " " + "TL";
                }
                else if (item.ParaBirimi == "EUR")
                {
                    ViewBag.EurKasaName = item.KasaAdi;
                    bakiyemiktarı = item.Bakiye.ToString();
                    uzunluk = item.Bakiye.ToString().Length;
                    bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                    ViewBag.EurKasaBakiye = bakiyemiktarı + " " + "EUR";
                }
                else if (item.ParaBirimi == "USD")
                {
                    ViewBag.UsdKasaName = item.KasaAdi;
                    bakiyemiktarı = item.Bakiye.ToString();
                    uzunluk = item.Bakiye.ToString().Length;
                    bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                    ViewBag.UsdKasaBakiye = bakiyemiktarı + " " + "USD";
                }

            }




            #endregion

            return View();
        }


        public ActionResult PerakendeSatis()
        {
            int firmaid = Convert.ToInt32(Session["FirmaID"].ToString());
            var urunler = db.Stok.Where(x => x.FirmaID == firmaid).ToList();
            ViewBag.Urunler = urunler.ToList();
            string FirmaID = Session["FirmaID"].ToString();
            AyarMetot.Siradaki("", "Stok", "StokKodu", Session["FirmaID"].ToString());
            ViewBag.StokKoduSiradaki2 = AyarMetot.GetNumara;
  
            var kategorilist = db.STK_KATEGORI.Where(x => x.FirmaID == firmaid).ToList();
            int i = 0;
            foreach (var item in kategorilist)
            {
                if (i == 0)
                {
                    ViewBag.Kategori1 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori1 = "Kategori 1";
                    }
                }
                else if (i == 1)
                {
                    ViewBag.Kategori2 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori2 = "Kategori 2";
                    }
                }
                else if (i == 2)
                {
                    ViewBag.Kategori3 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori3 = "Kategori 3";
                    }
                }
                else if (i == 3)
                {
                    ViewBag.Kategori4 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori4 = "Kategori 4";
                    }
                }
                else if (i == 4)
                {
                    ViewBag.Kategori5 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori5 = "Kategori 5";
                    }
                }
                else if (i == 5)
                {
                    ViewBag.Kategori6 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori6 = "Kategori 6";
                    }
                }

                i++;
            }
            return View();
        }

        [HttpPost]
        public ActionResult PerakendeSatis(Perakendesatis data, string json)
        {
            int perakendeID;
            try { 
            data.NakitTutar = data.NakitTutar.Replace(",", "").Replace(".", ",");
            }
            catch { }
            try { 
            data.KKTutar = data.KKTutar.Replace(",", "").Replace(".", ",");
            }
            catch { }
            string UniCode = Guid.NewGuid().ToString().Substring(24, 12);
            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from RETAIL", con))
                {
                    using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "RETAIL");
                        DataRow dr = ds.Tables["RETAIL"].NewRow();
                        AyarMetot.Siradaki("", "Perakende", "FirmaKodu", Session["FirmaID"].ToString());
                        dr["Durum"] = "Aktif";
                        dr["IslemNo"] = AyarMetot.GetNumara;
                        dr["IslemTipi"] = data.Durum;
                        dr["IslemTarih"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        dr["PersonelID"] = Session["PersonelID"].ToString();
                        dr["ParaBirimi"] = "TL";
                        dr["Indirim"] = 0;
                        dr["Aciklama"] = data.Aciklama;
                        dr["AcikKapali"] = "Kapali";
                        dr["KayT"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        dr["DegT"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        dr["IslemTipiHP"] = "Hızlı Satış";
                        dr["Donem"] = DateTime.Now.Year;
                        dr["PersonelYuzde"] = 0;
                        dr["ParaUstu"] = 0;
                        dr["CariID"] = data.CariID;
                        dr["IndirimID"] = -1;
                        dr["BonusKartID"] = -1;
                        dr["BonusKartTutar"] = 0;
                        dr["gIadeSuresi"] = "";
                        dr["gVadeTarihi"] = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                        dr["PerakendeOzelKod"] = -1;
                        dr["SatisIadeID"] = -1;
                        dr["IadeTarih"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        dr["KayitPersonelID"] = Session["PersonelID"].ToString();
                        dr["KayitTarih"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        dr["Kur"] = 1;
                        dr["ParaKartTahsilatID"] = -1;
                        dr["ParaKartTahsilatTutar"] = 0;
                        dr["VadeT"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        dr["Iskonto"] = 0;
                        //dr["tmaliyet"]
                        dr["FirmaID"] = Session["FirmaID"].ToString();
                        dr["SgkTutar"] = 0;
                        string firmaid2 = Session["FirmaID"].ToString();
                        string company_code2 = "SA01" + firmaid2.PadLeft(3, '0');
                        dr["Company_Code"] = company_code2;

                        dr["KalanTutar"] = 0;
                        dr["SatisNo"] = UniCode;
                        dr["toplamIsk"] = 0;
                        dr["OzelIsk"] = 0;
                        dr["SatirIsk"] = 0;
                        dr["MONEY_ID"] = -1;
                        dr["MONEY_ORAN"] = 0;
                        dr["SatilanPErsonelID"] = -1;
                        dr["ntoID"] = -1;
                        dr["BrutTutar"] = 0;
                        dr["KDVTutar"] = 0;
                        dr["NetTutar"] = 0;
                        dr["HvlTutar"] = 0;
                        dr["Indirim"] = 0;

                        if (data.veresiye==false)
                        {
                            if (data.IslemTipi == "nakit")
                            {
                                dr["ntoTutar"] = Convert.ToDecimal(data.NakitTutar.Replace(".",",")).ToString("N2");
                                dr["Indirim"] = Convert.ToDecimal(data.Indirim.Replace(".",",")).ToString("N2");
                                dr["KasaID"] = Session["vKasaID"].ToString();
                                dr["kktID"] = -1;
                                dr["kktTutar"] = 0;
                                dr["BankaID"] = -1;
                                dr["GenelTutar"] = Convert.ToDecimal(data.NakitTutar.Replace(".", ",")).ToString("N2"); 

                            }
                            else if (data.IslemTipi == "kk")
                            {
                                dr["ntoTutar"] = 0;
                                dr["KasaID"] = -1;
                                dr["kktID"] = -1;
                                dr["kktTutar"] = Convert.ToDecimal(data.KKTutar.Replace(".", ",")).ToString("N2"); 
                                dr["BankaID"] = data.BankaID;
                                dr["GenelTutar"] = Convert.ToDecimal(data.KKTutar.Replace(".", ",")).ToString("N2");

                            }
                            else if (data.IslemTipi == "parcali")
                            {
                                dr["ntoTutar"] = data.NakitTutar;
                                dr["KasaID"] = Session["vKasaID"].ToString();
                                dr["kktID"] = -1;
                                dr["kktTutar"] = data.KKTutar;
                                dr["BankaID"] = data.BankaID;

                                decimal toplam = Convert.ToDecimal(data.KKTutar.Replace(".", ",")) + Convert.ToDecimal(data.NakitTutar.Replace(".", ","));
                                dr["GenelTutar"] = toplam;

                            }
                        }
                        else
                        {

                            dr["ntoTutar"] = 0;
                            dr["CariID"] = data.CariID;
                            dr["KasaID"] = -1;
                            dr["kktID"] = -1;
                            dr["kktTutar"] = 0;
                            dr["BankaID"] = -1;
                            decimal toplam = Convert.ToDecimal(data.KKTutar.Replace(".", ",")) + Convert.ToDecimal(data.NakitTutar.Replace(".", ","));
                            dr["GenelTutar"] = toplam;
                        }

                        ds.Tables["RETAIL"].Rows.Add(dr);
                        da.Update(ds, "RETAIL");

                    }

                    using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                    {
                        if (conp1.State == ConnectionState.Closed) conp1.Open();
                        using (SqlCommand sID = new SqlCommand(@"select top (1) ID FROM 
                            RETAIL where PersonelID=" + Session["PersonelID"].ToString() + " Order BY ID Desc", conp1))
                        {
                            perakendeID = Convert.ToInt32(sID.ExecuteScalar());
                        }
                    }
                }
            }

            #region CASH_PAY


            if ((data.CariID != -1))
            {
                if (data.veresiye == false) //Veresiye Kayıt Şartı
                {

                    using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                    {
                        bag.Open();

                        using (SqlDataAdapter daCASH_PAY =
                            new System.Data.SqlClient.SqlDataAdapter("select top 1 * from CASH_PAY", bag))
                        {
                            using (SqlCommandBuilder cbS = new SqlCommandBuilder(daCASH_PAY))
                            {
                                DataSet dsCASH_PAY = new DataSet();
                                daCASH_PAY.Fill(dsCASH_PAY, "CASH_PAY");

                                DataRow dt;
                                if (Convert.ToDecimal(data.NakitTutar) != 0 &&
                                    Math.Round(Convert.ToDecimal(data.NakitTutar), 2) != 0)
                                {
                                    dt = dsCASH_PAY.Tables["CASH_PAY"].NewRow();

                                    dt["IslemTarih"] = DateTime.Now.Date.Day.ToString() + "." +
                                                       DateTime.Now.Date.Month.ToString() + "." +
                                                       DateTime.Now.Date.Year.ToString();



                                    dt["CariID"] = data.CariID;
                                    dt["PersonelID"] = Session["PersonelID"].ToString();
                                    dt["KayitpersonelID"] = AyarMetot.PersonelID;
                                    dt["HavaleMasrafID"] = -1;
                                    dt["CariBankaKID"] = -1;
                                    dt["HavaleMasrafID"] = -1;
                                    dt["KrediKartiID"] = -1;
                                    dt["OzelKodID"] = -1;
                                    data.iade = false;
                                    if (data.iade == false)
                                    {
                                        dt["Aciklama"] =
                                            AyarMetot.GetNumara + " İşlem Numaralı Perakende Satış Tahsilatı.";
                                        dt["IslemTipi"] = "T";
                                        dt["IslemNo"] = "T" + DateTime.Now.Year.ToString() +
                                                        DateTime.Now.Month.ToString() +
                                                        DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() +
                                                        DateTime.Now.Minute.ToString() +
                                                        DateTime.Now.Second.ToString();
                                        ;
                                    }
                                    else
                                    {
                                        dt["IslemTipi"] = "O";
                                        dt["IslemNo"] = "O" + DateTime.Now.Year.ToString() +
                                                        DateTime.Now.Month.ToString() +
                                                        DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() +
                                                        DateTime.Now.Minute.ToString() +
                                                        DateTime.Now.Second.ToString();
                                        dt["Aciklama"] =
                                            AyarMetot.GetNumara + " İşlem Numaralı Perakende İade Ödemesi.";
                                    }

                                    dt["ParaBirimi"] = "TL";

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
                                    dt["AdisyonTahsilatID"] = -1;

                                    dt["KasaID"] = AyarMetot.pKasaID;
                                    dt["BankaID"] = -1;

                                    dt["Tutar"] = Math.Round(Convert.ToDecimal(data.NakitTutar),
                                        AyarMetot.YuvarlamaSayisi);
                                    data.ntoID = -1;
                                    dt["FirmaID"] = Session["FirmaID"].ToString();

                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                    dt["Company_Code"] = company_code;
                                    if (data.ntoID == -1)
                                    {
                                        dsCASH_PAY.Tables["CASH_PAY"].Rows.Add(dt);

                                    }

                                    daCASH_PAY.Update(dsCASH_PAY, "CASH_PAY");


                                    if (data.ntoID == -1)
                                    {
                                        try
                                        {
                                            using (SqlConnection baglan = new SqlConnection(AyarMetot.strcon))
                                            {
                                                baglan.Open();
                                                using (SqlCommand command =
                                                    new SqlCommand(
                                                        "SELECT ID From CASH_PAY Where KayitpersonelID='" +
                                                        AyarMetot.PersonelID + "' order by ID desc", baglan))
                                                {
                                                    data.ntoID = Convert.ToInt32(command.ExecuteScalar());
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }

                                if (Convert.ToDecimal(data.KKTutar) != 0 &&
                                        Math.Round(Convert.ToDecimal(data.KKTutar), 2) != 0)
                                    {
                                        data.kktID = -1;
                                        if (data.kktID == -1) dt = dsCASH_PAY.Tables["CASH_PAY"].NewRow();
                                        else
                                        {
                                            DataRow[] tDT = dsCASH_PAY.Tables["CASH_PAY"]
                                                .Select("ID='" + data.kktID + "'");
                                            if (tDT.Length != 0) dt = tDT[0];
                                            else dt = dsCASH_PAY.Tables["CASH_PAY"].NewRow();
                                        }


                                        dt["IslemTarih"] = DateTime.Now.Date.Day.ToString() + "." +
                                                           DateTime.Now.Date.Month.ToString() + "." +
                                                           DateTime.Now.Date.Year.ToString();


                                        dt["CariID"] = data.CariID;
                                        dt["PersonelID"] = Session["PersonelID"].ToString();
                                        dt["KayitpersonelID"] = AyarMetot.PersonelID;
                                        dt["HavaleMasrafID"] = -1;
                                        dt["CariBankaKID"] = -1;
                                        dt["KrediKartiID"] = -1;
                                        dt["OzelKodID"] = -1;
                                        dt["Aciklama"] =
                                            AyarMetot.GetNumara + " İşlem Numaralı Perakende Satış Tahsilatı.";
                                        dt["ParaBirimi"] = "TL";

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
                                        dt["AdisyonTahsilatID"] = -1;
                                        dt["IslemTipi"] = "KKT";
                                        dt["IslemNo"] = "KKT" + DateTime.Now.Year.ToString() +
                                                        DateTime.Now.Month.ToString() +
                                                        DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() +
                                                        DateTime.Now.Minute.ToString() +
                                                        DateTime.Now.Second.ToString();
                                        dt["KasaID"] = -1;
                                        dt["BankaID"] = data.BankaID;
                                        dt["Tutar"] = Math.Round(Convert.ToDecimal(data.KKTutar),
                                            AyarMetot.YuvarlamaSayisi);
                                        dt["FirmaID"] = Session["FirmaID"].ToString();

                                        string firmaid = Session["FirmaID"].ToString();
                                        string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                        dt["Company_Code"] = company_code;
                                        dsCASH_PAY.Tables["CASH_PAY"].Rows.Add(dt);
                                        daCASH_PAY.Update(dsCASH_PAY, "CASH_PAY");




                                        if (data.kktID == -1)
                                        {
                                            try
                                            {

                                                using (SqlConnection baglan = new SqlConnection(AyarMetot.strcon))
                                                {
                                                    baglan.Open();
                                                    using (SqlCommand command =
                                                        new SqlCommand(
                                                            "SELECT ID From CASH_PAY Where KayitpersonelID='" +
                                                            AyarMetot.PersonelID + "' order by ID desc", baglan))
                                                    {
                                                        data.kktID = Convert.ToInt32(command.ExecuteScalar());
                                                    }
                                                }
                                            }

                                            catch
                                            {
                                            }
                                        }

                                    }

                                if (data.ntoID==0)
                                {
                                    data.ntoID = -1;
                                }
                                if (data.kktID == 0)
                                {
                                    data.kktID = -1;
                                }
                                try
                                {
                                    using (SqlConnection baglan = new SqlConnection(AyarMetot.strcon))
                                    {
                                        baglan.Open();
                                        using (SqlCommand command = new SqlCommand(
                                            "Update RETAIL set ntoID='" + data.ntoID + "', kktID='" + data.kktID +
                                            "' where ID='" + perakendeID + "'", baglan))
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }

                    #endregion


                }

            }


            json = "[" + json + "]";
            List<RETAIL_DETAIL> items = JsonConvert.DeserializeObject<List<RETAIL_DETAIL>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                decimal Miktar = 0;
                RETAIL_DETAIL er = items[i];
                if (er.Miktar != null)
                {
                    Miktar = decimal.Parse(er.Miktar.ToString(), CultureInfo.InvariantCulture);
                }
                else
                {
                    er.Miktar = 0;
                }

                decimal Kdv = Convert.ToDecimal(er.KDV);
                try
                {

                    using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da =
                            new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from RETAIL_DETAIL", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "RETAIL_DETAIL");
                                DataRow df = ds.Tables["RETAIL_DETAIL"].NewRow();

                                df["SatisID"] = perakendeID;
                                df["StokID"] = er.StokID;
                                df["DepoID"] = 1;
                                df["Fiyat"] = er.Fiyat;
                                df["Miktar"] = er.Miktar;
                                df["Tutar"] = er.Tutar;
                                df["KDV"] = 18;
                                df["Birim"] = "Adet";
                                df["SeriNo"] = "-";
                                df["ParaBirimi"] = "TL";
                                df["VariyantID"] = -1;
                                df["Kur"] = 1;
                                df["Donem"] = DateTime.Now.Year;
                                df["GarantiSuresi"] = 0;
                                df["Maliyet"] = 0;
                                df["FaturaMaliyeti"] = "Otomatik Maliyet";
                                df["IadeSuresi"] = "";
                                df["VadeTarihi"] = "";
                                df["SatirOzelKodID"] = -1;
                                df["IslemTipi"] = "Perakende Satış";
                                df["BirimAdet"] = 1;
                                df["gBirimMiktar"] = er.Miktar;
                                df["SatisPB"] = "TL";
                                df["EkOzellikID"] = -1;
                                df["Iskonto2"] = 0;
                                df["Iskonto3"] = 0;
                                df["Iskonto4"] = 0;
                                df["Iskonto5"] = 0;
                                df["Uzunluk"] = 1;
                                df["Genislik"] = 1;
                                df["MetreMiktarAdet"] = 1;
                                df["Derinlik"] = 1;
                                df["FirmaID"] = Session["FirmaID"].ToString();
                                Stok st = db.Stok.Where(x => x.ID == er.StokID).FirstOrDefault();
                                df["AlisF"] = st.AlishFiyat;
                                df["SatisTarihi"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                df["SatisCariID"] = -1;
                                df["SatisNo"] = UniCode;
                                df["RIskonto"] = 0;
                                string firmaid = Session["FirmaID"].ToString();
                                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                df["Company_Code"] = company_code;

                                ds.Tables["RETAIL_DETAIL"].Rows.Add(df);
                                da.Update(ds, "RETAIL_DETAIL");
                            }
                        }
                    }


                }
                catch (Exception E1)
                {


                    try
                    {
                        System.IO.File.WriteAllText(
                            Path.Combine(@"C:\Users\Alper\AppData\Local\Sayazilim", "sonuç.xml"),
                            E1.ToString());
                    }
                    catch
                    {
                    }
                }
            }

            try
            {
                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    conp1.Open();


                    using (SqlCommand d6 = new SqlCommand(@"update RETAIL SET 
 GenelTutar=(Select coalesce( SUM(Miktar*Fiyat),0) from RETAIL_DETAIL WHERE SatisID=RETAIL.ID)
,NetTutar=(Select coalesce( SUM((Miktar*Fiyat) / ((1+ (Convert(decimal(18,2),kdv)/100)))),0) from RETAIL_DETAIL WHERE SatisID=RETAIL.ID) ,
BrutTutar=(Select coalesce( SUM((Miktar*Fiyat) / ((1+ (Convert(decimal(18,2),kdv)/100)))),0) from RETAIL_DETAIL WHERE SatisID=RETAIL.ID) ,
  KDVTutar=(Select (coalesce( SUM((Miktar*Fiyat*Convert(decimal(18,2),kdv)) /
  (100+ (Convert(decimal(18,2),kdv)))),0)) from RETAIL_DETAIL WHERE SatisID=RETAIL.ID) where ID=" + perakendeID,
                        conp1))
                    {
                        d6.ExecuteNonQuery();
                    }



                    //using (SqlCommand d6 = new SqlCommand("update INVOICE SET brutToplam=netToplam,YerelTutar=GenelToplam where ID=" + perakendeID, conp1))
                    //{
                    //    d6.ExecuteNonQuery();
                    //}
                }
            }
            catch
            {
            }

            string Message = "Başarılı";
            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PerakendeListesi()
        {
            return View();
        }

        public ActionResult GetPerakendeList()
        {

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<PerakendeList> yonetim = new List<PerakendeList>();
            string sorg = @"set dateformat dmy; select RETAIL.ID, RETAIL.PersonelID, RETAIL.Durum AS [Durumu], RETAIL.IslemTipiHP AS [İşlem Tipi],case when RETAIL.IslemTipi=N'Perakende Satış' then N'SATIŞ' else N'İADE' end  AS[Tür], RETAIL.IslemNo AS [İşlem No],
                        case when ntoID=-1 and kktID=-1 AND ntoTutar= 0 and kktTutar= 0 and hvlTutar= 0 THEN N'VERESİYE' WHEN ntoTutar<> 0 and kktTutar= 0 AND KalanTutar<>0 then N'NAKİT-VERESİYE' WHEN ntoTutar<> 0 and kktTutar<> 0 then N'PARÇALI' WHEN  hvlTutar <>0 THEN N'HAVALE' WHEN ntoTutar <>0 THEN N'NAKİT' ELSE N'KREDİ KARTI' end AS [Ödeme Tipi], (Select CariUnvan from Cari where Cari.ID = CariID) AS [Müşteri Adı], 
 RETAIL.IslemTarih AS[İşlem Tarihi], RETAIL.Indirim AS[İndirim], 
 RETAIL.GenelTutar AS[Tutar],RETAIL.ntoTutar AS[Nakit Tutar],RETAIL.kktTutar AS[Kredi K. Tutar],RETAIL.hvlTutar AS[Havale Tutar],RETAIL.ParaKartTahsilatTutar AS[Para Kart Tutar],((ntoTutar+kktTutar+hvlTutar)*MONEY_ORAN /100) as [Kazanılan Para Puan],case when RETAIL.ParaBirimi='TRY' then 'TL' else RETAIL.ParaBirimi end  AS[PB],(Select Personel.Adi + ' ' + Personel.Soyadi from Personel where Personel.ID = PersonelID) AS[Personel], RETAIL.Aciklama AS[Satış Notu]
 from RETAIL Where FirmaID =" + FirmaID;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand faturaGetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = faturaGetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PerakendeList yt = new PerakendeList();
                            try
                            {
                                yt.ID = Convert.ToInt32(dr["ID"].ToString());
                                yt.IslemTipi = dr["İşlem Tipi"].ToString();
                                yt.Tur = dr["Tür"].ToString();
                                yt.IslemNo = dr["İşlem No"].ToString();
                                yt.OdemeTipi = dr["Ödeme Tipi"].ToString();
                                yt.MusteriAdi = dr["Müşteri Adı"].ToString();
                                yt.IslemTarihi = dr["İşlem Tarihi"].ToString();
                                yt.IslemTarihiF = Convert.ToDateTime(dr["İşlem Tarihi"].ToString())
                                    .ToString("dd-MM-yyyy");
                                yt.Indirim = Convert.ToDecimal(dr["İndirim"].ToString()).ToString("N2");
                                yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                                yt.ParaKTutar = Convert.ToDecimal(dr["Para Kart Tutar"].ToString()).ToString("N2");

                                try
                                {
                                    yt.NakitTutar = Convert.ToDecimal(dr["Nakit Tutar"].ToString()).ToString("N2");
                                }
                                catch(Exception e)
                                {
                                    yt.NakitTutar = "0,00";
                                }

                                try
                                {

                                    yt.KKTutar = Convert.ToDecimal(dr["Kredi K. Tutar"].ToString()).ToString("N2");
                                }
                                catch (Exception e)
                                {
                                    yt.KKTutar = "0,00";

                                }

                                try
                                {
                                    yt.HavaleTutar = Convert.ToDecimal(dr["Havale Tutar"].ToString()).ToString("N2");

                                }
                                catch (Exception e)
                                {
                                    yt.HavaleTutar = "0,00";
                                }
                                try
                                {

                                    yt.KazanilanPuan = Convert.ToDecimal(dr["Kazanılan Para Tutar"].ToString()).ToString("N2");
                                }
                                catch (Exception e)
                                {
                                    yt.KazanilanPuan = "0,00";
                                }
                                yt.PB = dr["PB"].ToString();
                                yt.Personel = dr["Personel"].ToString();
                                yt.SatisNotu = dr["Satış Notu"].ToString();


                                yonetim.Add(yt);
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DeletePerakende(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE RETAIL_DETAIL  where SatisID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                }


                RETAIL emp = db.RETAIL.Where(x => x.ID == id).FirstOrDefault<RETAIL>();
                db.RETAIL.Remove(emp);





                db.SaveChanges();
                return Json(new {success = true, message = "Kayıt Silindi"}, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GunSonuRaporu()
        {
            return View();
        }


        
        public ActionResult GetGunSonuRaporu()
        {
            string sr = @"  ";
            string FirmaID = Session["FirmaID"].ToString();

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;


            GunSonuRpr yt = new GunSonuRpr();
            string sorg =
                @"Set dateformat dmy;select 
coalesce(sum(GenelTutar),0) as Toplam, 
coalesce(sum(kktTutar),0) as KKTutar,
coalesce(sum(Indirim),0) as Indirim,
coalesce(sum(ntoTutar),0) as NakitTutar,
coalesce(sum(hvlTutar),0) as HvlTutar,
coalesce(sum(ParaKartTahsilatTutar),0) as ParaKartTutar,
(select coalesce(sum(GenelTutar),0) from RETAIL where CariID <> -1 ) as CariSatis,
coalesce(sum(TaksitTutar),0) as TaksitTutar,
coalesce(sum(SgkTutar),0) as SgkTutar
from RETAIL where  convert(date,IslemTarih,103) = convert(date, getdate(), 103) and FirmaID=" +
                Session["FirmaID"].ToString();


            string sorg2 = @"select TOP 1 Sum(Miktar) as Miktar,(Select UrunAdi From Stok s where s.ID=RETAIL_DETAIL.StokID) as UrunAdi from RETAIL_DETAIL where  
convert(date,RETAIL_DETAIL.SatisTarihi,103) = convert(date, getdate(), 103) and FirmaID=" + Session["FirmaID"].ToString() + " group by StokID order By Miktar Desc";

            try
            {

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand satisgetir = new SqlCommand(sorg, con))
                    {
                        using (SqlDataReader dr = satisgetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                yt.Toplam = Convert.ToDecimal(dr["Toplam"].ToString()).ToString("N2") + " TL";
                                yt.KKTutar = Convert.ToDecimal(dr["KKTutar"].ToString()).ToString("N2") + " TL";
                                yt.Indirim = Convert.ToDecimal(dr["Indirim"].ToString()).ToString("N2") + " TL";
                                yt.NakitTutar = Convert.ToDecimal(dr["NakitTutar"].ToString()).ToString("N2") + " TL";
                                yt.ParaKartTutar = Convert.ToDecimal(dr["ParaKartTutar"].ToString()).ToString("N2") + " TL";
                                yt.CariSatis = Convert.ToDecimal(dr["CariSatis"].ToString()).ToString("N2") + " TL";
                                yt.TaksitTutar = Convert.ToDecimal(dr["TaksitTutar"].ToString()).ToString("N2") + " TL";
                                yt.SgkTutar = Convert.ToDecimal(dr["SgkTutar"].ToString()).ToString("N2") + " TL";
                                yt.HvlTutar = Convert.ToDecimal(dr["HvlTutar"].ToString()).ToString("N2") + " TL";


                            }
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                try
                {
                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\alperen\AppData\Local\Sayazilim", "sonuç.xml"),
                        e1.ToString());
                }
                catch { }
            }
            try
            {

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand satisgetir = new SqlCommand(sorg2, con))
                    {
                        using (SqlDataReader dr = satisgetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                yt.Miktar = Convert.ToDecimal(dr["Miktar"].ToString()).ToString("N2") + " Adet";
                                yt.UrunAdi = dr["UrunAdi"].ToString();


                            }
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                try
                {
                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\alperen\AppData\Local\Sayazilim", "sonuç.xml"),
                        e1.ToString());
                }
                catch { }
            }


            return Json(new { data = yt,success = true }, JsonRequestBehavior.AllowGet);
        }


    }
}