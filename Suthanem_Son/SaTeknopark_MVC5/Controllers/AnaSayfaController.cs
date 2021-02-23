using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaTeknopark_MVC5.Models;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class AnaSayfaController : Controller
    {
        // GET: AnaSayfa
        public ActionResult AnaSayfa2()
        {
            return View();
        }

        public ActionResult DemoAnasayfa()
        {
            AyarMetot.Siradaki("", "Cari", "FirmaKodu", Session["FirmaID"].ToString());
            ViewBag.CariKoduSiradaki = AyarMetot.GetNumara;

            AyarMetot.Siradaki("", "Stok", "StokKodu", Session["FirmaID"].ToString());
            ViewBag.StokKoduSiradaki = AyarMetot.GetNumara;

            return View();
        }

        private DateTime HataninIlkgunu()
        {
            DateTime dt_Hafta = DateTime.Now;
            DateTime dt_Hafta_ilkGun = DateTime.Now;
            switch ((int)dt_Hafta.DayOfWeek)
            {
                case 0://Haftanın ilk günü Pazar kabul edildiğinden
                    dt_Hafta_ilkGun = dt_Hafta.AddDays(-6); // İçinde olduğumuz haftanın başı Pazartesi
                    break;

                default:// Gün Pazar değilse;
                    dt_Hafta_ilkGun = dt_Hafta.AddDays(1 - (int)dt_Hafta.DayOfWeek); // İçinde olduğumuz haftanın başı Pazartesi
                    break;
            }

            return dt_Hafta_ilkGun;

        }


        public ActionResult OnMuhasebeAnaSayfa()
        {
            #region bankakontrol

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


            #endregion

            #region kasakontrol

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


            #endregion
            GelirGider gt = new GelirGider()
            {
                GelirPzt = 0,
                GelirSali = 0,
                GelirCar = 0,
                GelirPer = 0,
                GelirCuma = 0,
                GelirCmr = 0,
                GelirPaz = 0,
                GiderPzt = 0,
                GiderSali = 0,
                GiderCar = 0,
                GiderPer = 0,
                GiderCuma = 0,
                GiderCmr = 0,
                GiderPaz = 0,
                AylikGelirKaydi = 0,
                AylikGiderKaydi = 0,
            };

            DateTime haftaninilkgunu = HataninIlkgunu();


            for (int j = 0; j < 6; j++)
            {
                decimal sonucGelir = 0;
                decimal sonucGider = 0;
                decimal AylikMasraf = 0;
                decimal AylikGelir = 0;
                DateTime bas = haftaninilkgunu;


                try
                {
                    using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                    {
                        conp.Open();
                        string srg =
                            "set dateformat dmy;select Sum(Tutar) from IslemlerListesi where BA='(A)' and  (CONVERT(datetime, IslemTarihi) BETWEEN convert(datetime, '" +
                            bas.ToString("dd.MM.yyyy") + " 00:00:00.000" + "') AND convert(datetime, '" +
                            bas.ToString("dd.MM.yyyy") + " 23:59:59.000" + "')) ";
                        SqlCommand command = new SqlCommand("set dateformat dmy;select Sum(Tutar) from IslemlerListesi where BA='(A)' and  (CONVERT(datetime, IslemTarihi) BETWEEN convert(datetime, '" + bas.ToString("dd.MM.yyyy") + " 00:00:00.000" + "') AND convert(datetime, '" + bas.ToString("dd.MM.yyyy") + " 23:59:59.000" + "')) ", conp);

                        sonucGider = Convert.ToDecimal(command.ExecuteScalar());

                    }
                }
                catch (Exception e1) { }

                try
                {
                    using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                    {
                        conp.Open();
                        SqlCommand command = new SqlCommand("set dateformat dmy;select Sum(Tutar) from IslemlerListesi where BA='(B)' and  (CONVERT(datetime, IslemTarihi) BETWEEN convert(datetime, '" + bas.ToString("dd.MM.yyyy") + " 00:00:00.000" + "') AND convert(datetime, '" + bas.ToString("dd.MM.yyyy") + " 23:59:59.000" + "')) ", conp);

                        sonucGelir = Convert.ToDecimal(command.ExecuteScalar());

                    }
                }
                catch { }

                try
                {
                    using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                    {
                        conp.Open();
                        SqlCommand command =
                            new SqlCommand(
                                " set dateformat dmy;Select SUM(Tutar) as Toplam from CASH_PAY where IslemTarih>dateadd(day,-30,getdate()) and (IslemTipi = 'A' or IslemTipi = 'E')",
                                conp);

                        AylikMasraf = Convert.ToDecimal(Convert.ToDecimal(command.ExecuteScalar()).ToString("N2"));
                        gt.AylikGiderKaydi = AylikMasraf;
                    }
                }
                catch (Exception e1)
                {

                }
                try
                {
                    using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                    {
                        conp.Open();
                        SqlCommand command =
                            new SqlCommand(
                                " set dateformat dmy;Select SUM(Tutar) as Toplam from CASH_PAY where IslemTarih>dateadd(day,-30,getdate()) and (IslemTipi = 'M' or IslemTipi = 'B')",
                                conp);

                        AylikGelir = Convert.ToDecimal(Convert.ToDecimal(command.ExecuteScalar()).ToString("N2"));
                        gt.AylikGelirKaydi = AylikGelir;
                    }
                }
                catch (Exception e1)
                {

                }
                if (j == 0)
                {
                    gt.GelirPzt = sonucGelir;
                    gt.GiderPzt = sonucGider;
                }
                else if (j == 1)
                {
                    gt.GelirSali = sonucGelir;
                    gt.GiderSali = sonucGider;
                }
                else if (j == 2)
                {
                    gt.GelirCar = sonucGelir;
                    gt.GiderCar = sonucGider;
                }
                else if (j == 3)
                {
                    gt.GelirPer = sonucGelir;
                    gt.GiderPer = sonucGider;
                }
                else if (j == 4)
                {
                    gt.GelirCuma = sonucGelir;
                    gt.GiderCuma = sonucGider;

                }
                else if (j == 5)
                {
                    gt.GelirCmr = sonucGelir;
                    gt.GiderCmr = sonucGider;

                }
                else if (j == 6)
                {
                    gt.GelirPaz = sonucGelir;
                    gt.GiderPaz = sonucGider;

                }

                haftaninilkgunu = haftaninilkgunu.AddDays(1);



            }

            GelirGider GT1 = gt;

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string SorguFirmaID = Session["FirmaID"].ToString();
            string TLSorgu = @"select top 5 c.ID,c.CariUnvan,c.paraBirimi, SUM(alacakB-borcB) as [Bakiye] 
from Cari C INNER JOIN BALANCE B ON cariID=C.ID and C.paraBirimi='TL' and C.FirmaID =" + SorguFirmaID +
@"group by c.CariUnvan,c.ID,c.paraBirimi having SUM(alacakB-borcB) <= 0
order by  [Bakiye]";
            string EuroSorgu = @"select top 5 c.ID,c.CariUnvan,c.paraBirimi, SUM(alacakB-borcB) as [Bakiye] 
from Cari C INNER JOIN BALANCE B ON cariID=C.ID and C.paraBirimi='EUR'  and C.FirmaID =" + SorguFirmaID +
@"group by c.CariUnvan,c.ID,c.paraBirimi having SUM(alacakB-borcB) <= 0
order by  [Bakiye]";
            string USDSorgu = @"select top 5 c.ID,c.CariUnvan,c.paraBirimi, SUM(alacakB-borcB) as [Bakiye] 
from Cari C INNER JOIN BALANCE B ON cariID=C.ID and C.paraBirimi='USD'  and C.FirmaID =" + SorguFirmaID +
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

            var Carilist = db.Cari.Where(x => x.FirmaID == FirmaID).ToList();

            ViewBag.CariSayisi = Carilist.Count;

            var AlımFaturası = db.INVOICE.Where(x => x.FirmaID == FirmaID & x.fType == "alimF").ToList();
            var SatisFaturasi = db.INVOICE.Where(x => x.FirmaID == FirmaID & x.fType == "satisF").ToList();
            decimal toplam = 0;
            string BakiyeRemove = "";
            string bakiyemiktarı;
            int uzunluk = 0;
            foreach (var item in AlımFaturası)
            {
                bakiyemiktarı = item.genelToplam.ToString();
                uzunluk = item.genelToplam.ToString().Length;
                bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);

                toplam = toplam + Convert.ToDecimal(bakiyemiktarı);
            }

            ViewBag.AlimFaturası = toplam + " " + "TL";

            toplam = 0;
            foreach (var item in SatisFaturasi)
            {
                bakiyemiktarı = item.genelToplam.ToString();
                uzunluk = item.genelToplam.ToString().Length;
                bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                toplam = toplam + Convert.ToDecimal(bakiyemiktarı);


            }
            ViewBag.SatisFaturasi = toplam + " " + "TL";
            toplam = 0;

            var TahsilatToplam = db.CASH_PAY.Where(x => x.FirmaID == FirmaID & x.IslemTipi == "T").ToList();

            foreach (var item in TahsilatToplam)
            {
                bakiyemiktarı = item.Tutar.ToString();
                uzunluk = item.Tutar.ToString().Length;
                bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                toplam = toplam + Convert.ToDecimal(bakiyemiktarı);
            }

            ViewBag.TahsilatToplam = toplam + " " + "TL";
            toplam = 0;

            #endregion


            #region BankaVeKasalar


            int vKasaID = Convert.ToInt32(Session["vKasaID"].ToString());
            int vBankaID = Convert.ToInt32(Session["vBankaID"].ToString());
            Kasa kisikasa = db.Kasa.Where(x => x.ID == vKasaID).FirstOrDefault();
            BALANCE_KASA blcks = db.BALANCE_KASA.Where(x => x.kasaID == kisikasa.ID && x.paraBirimi == kisikasa.ParaBirimi).FirstOrDefault();
            Banka kisibanka = db.Banka.Where(x => x.ID == vBankaID).FirstOrDefault();
            BALANCE_BANK blcbk = db.BALANCE_BANK.Where(x => x.bankaID == kisibanka.ID && x.paraBirimi == kisikasa.ParaBirimi).FirstOrDefault();

            ViewBag.kisiBankaAdi = kisibanka.BankaAdi;
            string tutar = Convert.ToDecimal(blcbk.bakiye).ToString("N2");
            ViewBag.kisiBankaTutar = tutar.Replace(".", "").Replace(",", ".");

            ViewBag.kisiKasaAdi = kisikasa.KasaAdi;
            tutar = Convert.ToDecimal(blcks.bakiye).ToString("N2");
            ViewBag.kisiKasaTutar = tutar.Replace(".","").Replace(",", ".");












            var BankaList = db.Banka.Where(x => x.FirmaID == FirmaID && x.Merkez == true).ToList();

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
                    bakiyemiktarı = bakiyemiktarı.Replace(",", ".");
                    ViewBag.TlBankaBakiye = bakiyemiktarı;
                }

            }

            var KasaList = db.Kasa.Where(x => x.FirmaID == FirmaID && x.Merkez == true).ToList();

            foreach (var item in KasaList)
            {
                if (item.ParaBirimi == "TL")
                {
                    ViewBag.TlKasaName = item.KasaAdi;
                    bakiyemiktarı = item.Bakiye.ToString();
                    uzunluk = item.Bakiye.ToString().Length;
                    bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                    bakiyemiktarı = bakiyemiktarı.Replace(",", ".");
                    ViewBag.TlKasaBakiye = bakiyemiktarı;
                }
                //else if (item.ParaBirimi == "EUR")
                //{
                //    ViewBag.EurKasaName = item.KasaAdi;
                //    bakiyemiktarı = item.Bakiye.ToString();
                //    uzunluk = item.Bakiye.ToString().Length;
                //    bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                //    ViewBag.EurKasaBakiye = bakiyemiktarı + " " + "EUR";
                //}
                //else if (item.ParaBirimi == "USD")
                //{
                //    ViewBag.UsdKasaName = item.KasaAdi;
                //    bakiyemiktarı = item.Bakiye.ToString();
                //    uzunluk = item.Bakiye.ToString().Length;
                //    bakiyemiktarı = bakiyemiktarı.Remove(uzunluk - 4, 4);
                //    ViewBag.UsdKasaBakiye = bakiyemiktarı + " " + "USD";
                //}

            }
            #endregion


           



            #region GelirGiderTablo
            List<KasaDurum> KasaDurumlist = new List<KasaDurum>();
            List<BankaDurum> BankaDurumList = new List<BankaDurum>();
            var ks = db.Kasa.Where(x => x.FirmaID == FirmaID && x.ParaBirimi == "TL").ToList();

            foreach (var item in ks)
            {
                KasaDurum newkasa = new KasaDurum();
                newkasa.KasaAdi = item.KasaAdi;
                newkasa.KasaBakiye = Convert.ToDecimal(item.Bakiye).ToString("N2").Replace(",",".");
                KasaDurumlist.Add(newkasa);
            }
            var banka = db.Banka.Where(x => x.FirmaID == FirmaID && x.hnParaBirimi == "TL").ToList();
            
            foreach (var item in banka)
            {
                BankaDurum bankayeni = new BankaDurum();
                bankayeni.BankaAdi = item.BankaAdi;
                bankayeni.BankaBakiye = Convert.ToDecimal(item.Bakiye).ToString("N2").Replace(",", ".");
                BankaDurumList.Add(bankayeni);
            }



            gt.BankalarList = BankaDurumList;
            gt.KasalarList = KasaDurumlist;
            #endregion




            return View(gt);
        }


        public ActionResult AnaSayfa()
        {

            ViewBag.pzt = 250;
            ViewBag.sali = 100;
            ViewBag.cars = 60;
            ViewBag.pers = 30;
            ViewBag.cuma = 90;
            ViewBag.cmts = 40;
            ViewBag.pzr = 50;


            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaSorgID = Session["FirmaID"].ToString();
            List<Servis> yonetim = new List<Servis>();
            string sorg = @"SET DATEFORMAT DMY;Select top 6 ParaBirimi,ServisIslemNo,Serino,CariID,(Select CariUnvan from Cari where ID=TECHNICAL.CariID) 
as CariUnvan,Tarih,Marka,Model,GenelToplam,Durum,TECHNICAL.ID from TECHNICAL where FirmaID =" + FirmaSorgID + " order by Tarih,ID  desc";

            string sorg2 = @"set dateformat dmy;Select count(ID) as Sayi,'Hepsi' as Durum from TECHNICAL Union all SELECT COUNT(Durum) AS Sayi,'HepsiSonHafta' as Durum from TECHNICAL  where DATEDIFF(WEEK,Tarih, getdate()) BETWEEN 0 AND 2 and FirmaID=" + FirmaSorgID + " Union all SELECT COUNT(Durum) AS Sayi,'SonHafta' as Durum from TECHNICAL  where (Durum='ONARIM TAMAMLANDI' OR Durum='TESLİMAT BEKLİYOR') And FirmaID=" + FirmaSorgID + " and  DATEDIFF(WEEK,Tarih, getdate()) BETWEEN 0 AND 2 Union all SELECT COUNT(Durum) AS Sayi,'Bekleyen' as Durum from TECHNICAL  where (Durum='TESLİMAT BEKLİYOR') and FirmaID=" + FirmaSorgID;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Servis yt = new Servis();

                            yt.GenelToplam = Convert.ToDecimal(dr["GenelToplam"]).ToString("N2");
                            yt.ServisIslemNo = dr["ServisIslemNo"].ToString();
                            yt.Serino = dr["Serino"].ToString();
                            yt.CariID = dr["CariID"].ToString();
                            yt.ParaBirimi = dr["ParaBirimi"].ToString();
                            try
                            {
                                yt.Tarih = Convert.ToDateTime(dr["Tarih"]);
                            }
                            catch (Exception)
                            {

                                yt.Tarih = DateTime.Now;
                            }
                            yt.Marka = dr["Marka"].ToString();
                            yt.Model = dr["Model"].ToString();
                            yt.Durum = dr["Durum"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.CariUnvan = dr["CariUnvan"].ToString();

                            yonetim.Add(yt);


                        }
                    }
                }
            }

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg2, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Servis yt2 = new Servis();

                            if (dr["Durum"].ToString() == "Hepsi")
                            {
                                ViewBag.ServisTumu = dr["Sayi"].ToString();
                            }

                            if (dr["Durum"].ToString() == "HepsiSonHafta")
                            {
                                ViewBag.HepsiSonHafta = dr["Sayi"].ToString();
                            }

                            if (dr["Durum"].ToString() == "SonHafta")
                            {
                                ViewBag.SonHafta = dr["Sayi"].ToString();
                            }

                            if (dr["Durum"].ToString() == "Bekleyen")
                            {
                                ViewBag.Bekleyen = dr["Sayi"].ToString();
                            }

                        }
                    }
                }
            }

            ViewBag.Islemler = yonetim;

            return View();

        }
    }
}