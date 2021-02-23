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
    public class FaturaController : Controller
    {

        public ActionResult FaturalarListesi()
        {

            return View();

        }

        public ActionResult YeniFatura()
        {

            return View();
        }

        sayazilimEntities db = new sayazilimEntities();
        public ActionResult YeniIade()
        {
            ViewBag.FTipi = "1.Fiyat";
            var st = db.Stok.ToList().OrderBy(x => x.ID).Take(20);



            return View(st.ToList());
        }

        public ActionResult YeniSatis(int id = 0, string FaturaTipi = "SF")
        {
            var st = db.Stok.ToList().OrderBy(x => x.ID).Take(20);
            ViewBag.FTipi = "1.Fiyat";
            ViewBag.CariID = "0";
            var urun = db.Stok.ToList();
            ViewBag.Urunler = urun.ToList();
            AyarMetot.Siradaki("", "Stok", "StokKodu", Session["FirmaID"].ToString());
            ViewBag.StokKoduSiradaki2 = AyarMetot.GetNumara;
            if (FaturaTipi == "AF")
            {
                FaturaTipi = "Alım Faturası";
            }
            else
            {
                FaturaTipi = "Satış Faturası";
            }
            using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
            {
                if (conp1.State == ConnectionState.Closed) conp1.Open();
                using (SqlCommand sID = new SqlCommand("select top (1) ID FROM INVOICE Order BY ID Desc", conp1))
                {
                    ViewBag.SiradakiFaturaNumarasi = "SI" + Convert.ToInt32(sID.ExecuteScalar());
                }
            }
            ViewBag.FaturaTipi = FaturaTipi;

            INVOICE invoice = null;
            if (id == 0)
            {
                invoice = new INVOICE();

            }
            else
            {
                sayazilimEntities db = new sayazilimEntities();
                invoice = db.INVOICE.Where(x => x.ID == id).FirstOrDefault<INVOICE>();
                var servisdetaylari = db.INVOICE_DETAIL.Where(x => x.faturaID == id).ToList<INVOICE_DETAIL>();
                ViewBag.FaturaDetay = servisdetaylari.ToList();
                List<FaturaDetaylariEk> ftr = new List<FaturaDetaylariEk>();
                for (int i = 0; i < servisdetaylari.Count; i++)
                {
                    INVOICE_DETAIL td = servisdetaylari[i];
                    FaturaDetaylariEk stkek = new FaturaDetaylariEk();
                    stkek.UrunAdi = AyarMetot.StokBilgiCek(Convert.ToInt32(td.UrunID), "UrunAdi");

                    ftr.Add(stkek);

                }

                ViewBag.FaturaEk = ftr;

            }
            return View(invoice);

        }


        [HttpPost]
        public JsonResult YeniSatis(Array[] data, string indirim, string CariID, string FaturaTarihi,
               string Aciklama, string KdvDh, string Tipi, string SevkTarihi, string VadeTarihi, int FaturaID, string FaturaNo)
        {

            int kolon = 0;

            if (FaturaNo == "")
            {
                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand("select top (1) ID FROM INVOICE Order BY ID Desc", conp1))
                    {
                        FaturaNo = "SI" + Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }

            int PersonelID = -1;


            if (Session["PersonelID"] != null)
            {
                PersonelID = Convert.ToInt32(Session["PersonelID"]);

            }
            else
            {
                PersonelID = 1;

            }


            if (FaturaID == -1)
            {
                using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from INVOICE", con))
                    {
                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds, "INVOICE");
                            DataRow dr = ds.Tables["INVOICE"].NewRow();

                            dr["Durumu"] = "Aktif";
                            if (Tipi == "Sevk İrsaliyesi")
                            {
                                dr["FaturaType"] = "Sevk İrsaliyesi";
                                dr["FaturaSeri"] = "SI";
                                dr["fType"] = "SatisF";
                            }
                            else if (Tipi == "Satış Faturası")
                            {
                                dr["FaturaType"] = "Satış Faturası";
                                dr["FaturaSeri"] = "SF";
                                dr["fType"] = "SatisF";

                            }
                            else if (Tipi == "Alım Faturası")
                            {
                                dr["FaturaType"] = "Alım Faturası";
                                dr["FaturaSeri"] = "AF";
                                dr["fType"] = "AlimF";

                            }
                            else if (Tipi == "Alım İrsaliyesi")
                            {
                                dr["FaturaType"] = "Alım İrsaliyesi";
                                dr["FaturaSeri"] = "AF";
                                dr["fType"] = "AlimF";
                            }

                            dr["kdvDH"] = KdvDh;
                            dr["KapaliAcik"] = "Acik";
                            dr["VadeKontrol"] = 'N';
                            dr["Yetkili"] = "";
                            dr["FaturaAdresi"] = "";
                            dr["SevkAdresi"] = "";
                            dr["CariVergD"] = "";
                            dr["CariVergiNo"] = "";
                            dr["IrsaliyeID"] = -1;
                            dr["EBelgeGonderimi"] = false;
                            dr["AktarilanFID"] = -1;
                            dr["FaturaNo"] = FaturaNo;
                            dr["FaturaTarihi"] = Convert.ToDateTime(FaturaTarihi).Date.Day.ToString() + "." +
        Convert.ToDateTime(FaturaTarihi).Date.Month.ToString() + "." + Convert.ToDateTime(FaturaTarihi).Date.Year.ToString();

                            dr["SevkTarihi"] = Convert.ToDateTime(SevkTarihi).Date.Day.ToString() + "." +
                                                Convert.ToDateTime(SevkTarihi).Date.Month.ToString() + "." + Convert.ToDateTime(SevkTarihi).Date.Year.ToString();
                            dr["gVadeTarih"] = Convert.ToDateTime(VadeTarihi).Date.Day.ToString() + "." + Convert.ToDateTime(VadeTarihi).Date.Month.ToString() + "." + Convert.ToDateTime(VadeTarihi).Date.Year.ToString();

                            dr["paraBirimi"] = "TL";
                            dr["CariID"] = CariID;
                            using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                            {
                                conp.Open();
                                using (SqlCommand carigetir = new SqlCommand("Select FirmaKodu,CariUnvan From Cari where ID='" + CariID + "'", conp))
                                {
                                    using (SqlDataReader dc = carigetir.ExecuteReader())
                                    {
                                        while (dc.Read())
                                        {
                                            dr["CariKodu"] = dc["FirmaKodu"];
                                            dr["CariUnvan"] = dc["CariUnvan"]; ;
                                        }
                                    }
                                }
                            }

                            dr["Aciklama"] = Aciklama;
                            dr["Aciklama2"] = "";
                            dr["MiktarBasinaOTVDurumu"] = false;
                            dr["FatTip"] = "A";
                            dr["gKDV"] = 0;
                            dr["gIskonta"] = 0;
                            dr["HAFKomisyon"] = 0;
                            dr["gTevfikat"] = 0;
                            dr["TevkifatTur"] = "Seçilmedi";

                            dr["gIadeSuresi"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();

                            dr["Donem"] = DateTime.Now.Year;
                            dr["FaturaOzelKod"] = -1;
                            dr["FirmaID"] = Session["FirmaID"].ToString();

                            dr["personelID"] = PersonelID;
                            dr["KayitpersonelID"] = PersonelID;

                            //till here

                            //Fatura Summary goes here
                            dr["brutToplam"] = 0;
                            dr["satirIskonta"] = 0;
                            dr["ozelIskonta"] = 0;
                            dr["netToplam"] = 0;
                            dr["tKDV"] = 0;
                            dr["tOIV"] = 0;
                            dr["OTVToplami"] = 0;
                            dr["tTevfikat"] = 0;
                            dr["genelToplam"] = 0;


                            dr["FKuru"] = "1";
                            dr["YerelTutar"] = dr["genelToplam"];

                            dr["toplamIskonta"] = 0;
                            dr["g18KDV"] = 0;
                            dr["g8KDV"] = 0;
                            dr["g1KDV"] = 0;
                            dr["g19KDV"] = 0;

                            dr["FaturaNo"] = FaturaNo;

                            dr["YazdirmaDurumu"] = "Bekliyor";



                            dr["PrimYuzde"] = 0;


                            dr["DolarKur"] = 1;
                            dr["EuroKur"] = 1;
                            dr["chfKur"] = 1;
                            dr["gbpKur"] = 1;
                            dr["jpyKur"] = 1;



                            dr["saat"] = DateTime.Now.ToString("HH:mm");

                            dr["ServisCihazID"] = -1;
                            dr["SantiyeID"] = -1;

                            dr["AracID"] = -1;
                            dr["SiparisID"] = -1;
                            dr["ProjeID"] = -1;

                            dr["OzelAlan1"] = AyarMetot.SeciliPersonelBilgiCek("PlakaArac", Session["PersonelID"].ToString());
                            dr["OzelAlan2"] = AyarMetot.SeciliPersonelBilgiCek("Adi + ' '+ Soyadi", Session["PersonelID"].ToString());
                            dr["OzelAlan3"] = "";
                            dr["OzelAlan4"] = "";
                            dr["OzelAlan5"] = "";
                            dr["OzelAlan6"] = "";
                            dr["OzelAlan7"] = "";
                            dr["OzelAlan8"] = "";
                            dr["eFatDuz"] = false;

                            dr["DolarVar"] = false;
                            dr["EuroVar"] = false;
                            dr["GBPVar"] = false;
                            dr["TLEURKUR"] = 1;
                            dr["TLUSDKUR"] = 1;
                            dr["TLGBPKUR"] = 1;

                            dr["HAFID"] = -1;
                            dr["KdvliKomisyon"] = false;
                            dr["CariyeIslesin"] = true;

                            dr["IrsaliyeTuru"] = "";
                            string firmaid = Session["FirmaID"].ToString();
                            string company_code = "SA01" + firmaid.PadLeft(3, '0');
                            dr["Company_Code"] = company_code;

                            ds.Tables["INVOICE"].Rows.Add(dr);
                            da.Update(ds, "INVOICE");

                        }
                    }
                }



                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(@"select top (1) ID FROM 
               INVOICE where PersonelID=" + PersonelID + " Order BY ID Desc", conp1))
                    {
                        FaturaID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

                try
                {
                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\ilhan\AppData\Local\Sayazilim", "alperen.xml"), data.Length.ToString());
                }
                catch
                { }

            }
            else
            {
                using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    using (SqlDataAdapter da =
                        new System.Data.SqlClient.SqlDataAdapter(
                            "select * from INVOICE where ID='" + FaturaID + "'", con))
                    {
                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds, "INVOICE");
                            DataRow[] adf = ds.Tables["INVOICE"].Select("ID='" + FaturaID + "'");
                            if (adf.Length != 0)
                            {
                                DataRow dr = adf[0];

                                dr["Durumu"] = "Aktif";
                                if (Tipi == "Sevk İrsaliyesi")
                                {
                                    dr["FaturaType"] = "Sevk İrsaliyesi";
                                    dr["FaturaSeri"] = "SI";
                                    dr["fType"] = "SatisF";
                                }
                                else if (Tipi == "Satış Faturası")
                                {
                                    dr["FaturaType"] = "Satış Faturası";
                                    dr["FaturaSeri"] = "SF";
                                    dr["fType"] = "SatisF";

                                }
                                else if (Tipi == "Alım Faturası")
                                {
                                    dr["FaturaType"] = "Alım Faturası";
                                    dr["FaturaSeri"] = "AF";
                                    dr["fType"] = "AlimF";

                                }
                                else if (Tipi == "Alım İrsaliyesi")
                                {
                                    dr["FaturaType"] = "Alım İrsaliyesi";
                                    dr["FaturaSeri"] = "AF";
                                    dr["fType"] = "AlimF";
                                }
                                dr["kdvDH"] = KdvDh;

                                dr["KapaliAcik"] = "Acik";
                                dr["VadeKontrol"] = 'N';
                                dr["Yetkili"] = "";
                                dr["FaturaAdresi"] = "";
                                dr["SevkAdresi"] = "";
                                dr["CariVergD"] = "";
                                dr["CariVergiNo"] = "";
                                dr["IrsaliyeID"] = -1;
                                dr["EBelgeGonderimi"] = false;
                                dr["AktarilanFID"] = -1;
                                dr["FaturaNo"] = FaturaNo;
                                dr["FaturaTarihi"] = Convert.ToDateTime(FaturaTarihi).Date.Day.ToString() + "." +
            Convert.ToDateTime(FaturaTarihi).Date.Month.ToString() + "." + Convert.ToDateTime(FaturaTarihi).Date.Year.ToString();

                                if (SevkTarihi != null)
                                {
                                    dr["SevkTarihi"] = Convert.ToDateTime(SevkTarihi).Date.Day.ToString() + "." +
                                                        Convert.ToDateTime(SevkTarihi).Date.Month.ToString() + "." + Convert.ToDateTime(SevkTarihi).Date.Year.ToString();
                                }
                                else
                                {
                                    dr["SevkTarihi"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." +
                                                       Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();

                                }
                                if (VadeTarihi != null)
                                {
                                    dr["gVadeTarih"] = Convert.ToDateTime(VadeTarihi).Date.Day.ToString() + "." + Convert.ToDateTime(VadeTarihi).Date.Month.ToString() + "." + Convert.ToDateTime(VadeTarihi).Date.Year.ToString();
                                }
                                else
                                {
                                    dr["gVadeTarih"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();


                                }
                                dr["paraBirimi"] = "TL";
                                dr["CariID"] = CariID;
                                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                                {
                                    conp.Open();
                                    using (SqlCommand carigetir = new SqlCommand("Select FirmaKodu,CariUnvan From Cari where ID='" + CariID + "'", conp))
                                    {
                                        using (SqlDataReader dc = carigetir.ExecuteReader())
                                        {
                                            while (dc.Read())
                                            {
                                                dr["CariKodu"] = dc["FirmaKodu"];
                                                dr["CariUnvan"] = dc["CariUnvan"]; ;
                                            }
                                        }
                                    }
                                }

                                dr["Aciklama"] = Aciklama;
                                dr["Aciklama2"] = "";
                                dr["MiktarBasinaOTVDurumu"] = false;
                                dr["FatTip"] = "A";
                                dr["gKDV"] = 0;
                                dr["gIskonta"] = 0;
                                dr["HAFKomisyon"] = 0;
                                dr["gTevfikat"] = 0;
                                dr["TevkifatTur"] = "Seçilmedi";
                                dr["gIadeSuresi"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();

                                dr["Donem"] = DateTime.Now.Year;
                                dr["FaturaOzelKod"] = -1;

                                dr["personelID"] = PersonelID;
                                dr["KayitpersonelID"] = PersonelID;

                                //till here

                                //Fatura Summary goes here
                                dr["brutToplam"] = 0;
                                dr["satirIskonta"] = 0;
                                dr["ozelIskonta"] = 0;
                                dr["netToplam"] = 0;
                                dr["tKDV"] = 0;
                                dr["tOIV"] = 0;
                                dr["OTVToplami"] = 0;
                                dr["tTevfikat"] = 0;
                                dr["genelToplam"] = 0;


                                dr["FKuru"] = "1";
                                dr["YerelTutar"] = dr["genelToplam"];

                                dr["toplamIskonta"] = 0;
                                dr["g18KDV"] = 0;
                                dr["g8KDV"] = 0;
                                dr["g1KDV"] = 0;
                                dr["g19KDV"] = 0;

                                dr["FaturaNo"] = FaturaNo;

                                dr["YazdirmaDurumu"] = "Bekliyor";



                                dr["PrimYuzde"] = 0;


                                dr["DolarKur"] = 1;
                                dr["EuroKur"] = 1;
                                dr["chfKur"] = 1;
                                dr["gbpKur"] = 1;
                                dr["jpyKur"] = 1;



                                dr["saat"] = DateTime.Now.ToString("HH:mm");

                                dr["ServisCihazID"] = -1;
                                dr["SantiyeID"] = -1;

                                dr["AracID"] = -1;
                                dr["SiparisID"] = -1;
                                dr["ProjeID"] = -1;

                                dr["OzelAlan1"] = AyarMetot.SeciliPersonelBilgiCek("PlakaArac", Session["PersonelID"].ToString());
                                dr["OzelAlan2"] = AyarMetot.SeciliPersonelBilgiCek("Adi + ' '+ Soyadi", Session["PersonelID"].ToString());
                                dr["OzelAlan3"] = "";
                                dr["OzelAlan4"] = "";
                                dr["OzelAlan5"] = "";
                                dr["OzelAlan6"] = "";
                                dr["OzelAlan7"] = "";
                                dr["OzelAlan8"] = "";
                                dr["eFatDuz"] = false;

                                dr["DolarVar"] = false;
                                dr["EuroVar"] = false;
                                dr["GBPVar"] = false;
                                dr["TLEURKUR"] = 1;
                                dr["TLUSDKUR"] = 1;
                                dr["TLGBPKUR"] = 1;

                                dr["HAFID"] = -1;
                                dr["KdvliKomisyon"] = false;
                                dr["CariyeIslesin"] = true;

                                dr["IrsaliyeTuru"] = "";


                                da.Update(ds, "INVOICE");

                            }
                        }
                    }

                }



                try
                {
                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\ilhan\AppData\Local\Sayazilim", "alperen.xml"), data.Length.ToString());
                }
                catch
                { }
            }
            for (int i = 0; i < data.Length; i++)
            {
                string Mik = "";
                decimal Fiyat = 0;
                decimal Kdv = 18;
                int UrunID = -1;
                string Birim = "Adet";
                string Mik2 = "";
                string Birim2 = "Adet";
                string ID = "-1";
                foreach (var veri in data[i])
                {
                    if (kolon == 0)
                    {
                        UrunID = Convert.ToInt32(veri);
                    }
                    else if (kolon == 1)
                    {
                        Mik = veri.ToString();
                    }
                    else if (kolon == 2)
                    {
                        Birim = (veri).ToString();
                    }
                    else if (kolon == 3)
                    {
                        Fiyat = Convert.ToDecimal(veri);
                    }
                    else if (kolon == 4)
                    {
                        Kdv = Convert.ToDecimal(veri);
                    }
                    else if (kolon == 5)
                    {
                        Mik2 = veri.ToString();
                    }
                    else if (kolon == 6)
                    {
                        Birim2 = veri.ToString();
                    }
                    else if (kolon == 7)
                    {
                        ID = veri.ToString();
                    }
                    kolon++;
                }
                kolon = 0;

                decimal Miktar = Convert.ToDecimal(Mik.Replace(".", ","));
                decimal Miktar2 = 0;
                try
                {
                    Miktar2 = Convert.ToDecimal(Mik2.Replace(".", ","));
                }
                catch (Exception e)
                {
                }

                try
                {
                    if (ID == "-1")
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from INVOICE_DETAIL", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "INVOICE_DETAIL");
                                    DataRow df = ds.Tables["INVOICE_DETAIL"].NewRow();

                                    df["Durumu"] = "Aktif";
                                    df["faturaID"] = FaturaID;
                                    df["IslemTarihi"] = DateTime.Now;
                                    if (Tipi == "Sevk İrsaliyesi")
                                    {
                                        df["IslemTuru"] = "Sevk İrsaliyesi";
                                    }
                                    else if (Tipi == "Satış Faturası")
                                    {
                                        df["IslemTuru"] = "Satış Faturası";

                                    }
                                    else if (Tipi == "Alım Faturası")
                                    {
                                        df["IslemTuru"] = "Alım Faturası";

                                    }
                                    else if (Tipi == "Alım İrsaliyesi")
                                    {
                                        df["IslemTuru"] = "Alım İrsaliyesi";
                                    }
                                    df["UrunID"] = UrunID;
                                    df["SeriNo"] = "-";
                                    df["depoID"] = Session["vDepoID"];
                                    df["PersonelID"] = PersonelID;
                                    df["Birim"] = Birim;
                                    df["Birim2"] = Birim2;
                                    df["Miktar"] = Miktar;
                                    df["Miktar2"] = Miktar2;
                                    df["gBirimMiktar"] = Miktar;
                                    df["BirimAdet"] = 1;
                                    df["SevkMiktar"] = 0;
                                    df["IadeMiktar"] = 0;
                                    df["MalzemeSayi"] = "";
                                    df["IadeMiktar"] = 0;
                                    df["Fiyat"] = Fiyat;
                                    df["AdetFiyati"] = Fiyat;

                                    df["mf"] = 0;
                                    df["OIV"] = 0;
                                    if (KdvDh == "D")
                                    {
                                        df["frtFiyat"] = (Fiyat * Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                        df["FtrSonDesc"] = Fiyat;
                                        df["actTutar"] = Fiyat * Miktar;
                                        df["Tutar"] = (Fiyat * Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                    }
                                    else
                                    {

                                        df["Tutar"] = Fiyat * Miktar;
                                        df["actTutar"] = (Fiyat * Miktar) + ((Fiyat * Miktar) * Kdv / 100);
                                        df["frtFiyat"] = Fiyat;
                                        df["FtrSonDesc"] = (Fiyat) + ((Fiyat) * Kdv / 100);
                                    }

                                    df["pBirimi"] = AyarMetot.PB_Short;

                                    df["VadeTarih"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();
                                    df["IadeSuresi"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();
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
                                    df["FireMiktar"] = 0;
                                    df["MetreMiktarAdet"] = 0;
                                    df["TedCariID"] = -1;
                                    df["TakimID"] = -1;
                                    df["UrunSeriNoMob"] = "";
                                    df["AracPlaka"] = "";
                                    df["AracPlaka2"] = "";
                                    df["AracGiderIslemesin"] = false;
                                    df["kpGenislik"] = 0;
                                    df["KPYukseklik"] = 0;
                                    df["KPIskonto"] = 0;
                                    df["KPKar"] = 0;
                                    df["MetreMiktarAdet"] = 0;
                                    df["Motor"] = 0;
                                    df["MotorSN"] = 0;
                                    df["Alici"] = 0;
                                    df["AliciSN"] = 0;
                                    df["Kumanda"] = 0;
                                    df["KumandaSN"] = 0;
                                    df["GUCK"] = 0;
                                    df["GUCKSN"] = 0;
                                    df["STAciklama2"] = "";
                                    df["KAMHAFID"] = -1;
                                    df["OTV"] = 0;
                                    df["UnicodeSF"] = "";

                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                    df["Company_Code"] = company_code;

                                    ds.Tables["INVOICE_DETAIL"].Rows.Add(df);
                                    da.Update(ds, "INVOICE_DETAIL");
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
                                    "select * from INVOICE_DETAIL where ID='" + ID + "'", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "INVOICE_DETAIL");
                                    DataRow[] adf = ds.Tables["INVOICE_DETAIL"].Select("ID='" + ID + "'");
                                    if (adf.Length != 0)
                                    {
                                        DataRow df = adf[0];
                                        df["Durumu"] = "Aktif";
                                        df["faturaID"] = FaturaID;
                                        df["IslemTarihi"] = DateTime.Now;
                                        df["IslemTuru"] = "Sevk İrsaliyesi";
                                        df["UrunID"] = UrunID;
                                        df["SeriNo"] = "-";
                                        df["depoID"] = Session["vDepoID"];
                                        df["PersonelID"] = PersonelID;
                                        df["Birim"] = Birim;
                                        df["Birim2"] = Birim2;
                                        df["Miktar"] = Miktar;
                                        df["Miktar2"] = Miktar2;
                                        df["gBirimMiktar"] = Miktar;
                                        df["SevkMiktar"] = 0;
                                        df["IadeMiktar"] = 0;
                                        df["MalzemeSayi"] = "";
                                        df["IadeMiktar"] = 0;
                                        df["Fiyat"] = Fiyat;
                                        df["AdetFiyati"] = Fiyat;

                                        df["mf"] = 0;
                                        df["OIV"] = 0;
                                        if (KdvDh == "D")
                                        {
                                            df["frtFiyat"] = (Fiyat * Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                            df["FtrSonDesc"] = Fiyat;
                                            df["actTutar"] = Fiyat * Miktar;
                                            df["Tutar"] = (Fiyat * Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                        }
                                        else
                                        {

                                            df["Tutar"] = Fiyat * Miktar;
                                            df["actTutar"] = (Fiyat * Miktar) + ((Fiyat * Miktar) * Kdv / 100);
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
                                        df["FireMiktar"] = 0;
                                        df["MetreMiktarAdet"] = 0;
                                        df["TedCariID"] = -1;
                                        df["TakimID"] = -1;
                                        df["UrunSeriNoMob"] = "";
                                        df["AracPlaka"] = "";
                                        df["AracPlaka2"] = "";
                                        df["AracGiderIslemesin"] = false;
                                        df["kpGenislik"] = 0;
                                        df["KPYukseklik"] = 0;
                                        df["KPIskonto"] = 0;
                                        df["KPKar"] = 0;
                                        df["MetreMiktarAdet"] = 0;
                                        df["Motor"] = 0;
                                        df["MotorSN"] = 0;
                                        df["Alici"] = 0;
                                        df["AliciSN"] = 0;
                                        df["Kumanda"] = 0;
                                        df["KumandaSN"] = 0;
                                        df["GUCK"] = 0;
                                        df["GUCKSN"] = 0;
                                        df["STAciklama2"] = "";
                                        df["KAMHAFID"] = -1;
                                        df["OTV"] = 0;
                                        df["UnicodeSF"] = "";
                                        df["BirimAdet"] = 1;

                                        da.Update(ds, "INVOICE_DETAIL");

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
                        using (SqlCommand d6 = new SqlCommand(@"  update INVOICE SET 
 GenelToplam=(Select coalesce( SUM(Miktar*Fiyat),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)
,NetToplam=(Select coalesce( SUM((Miktar*Fiyat) / ((1+ (Convert(decimal(18,2),kdv)/100)))),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID) ,
  tKDV=(Select (coalesce( SUM((Miktar*Fiyat*Convert(decimal(18,2),kdv)) / (100+ (Convert(decimal(18,2),kdv)))),0)) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)

 where ID=" + FaturaID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand d6 = new SqlCommand(@"  update INVOICE SET 
 netToplam=(Select coalesce( SUM(Miktar*Fiyat),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)
,GenelToplam=(Select coalesce( SUM((Miktar*Fiyat)+(Miktar*Fiyat)*(Convert(decimal(18,2),kdv)/100)),0)from INVOICE_DETAIL WHERE faturaID=INVOICE.ID) ,
  tKDV=(Select coalesce( SUM((Miktar*Fiyat)*(Convert(decimal(18,2),kdv)/100)),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)


 where ID=" + FaturaID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }

                    }

                    using (SqlCommand d6 = new SqlCommand("update INVOICE SET brutToplam=netToplam,YerelTutar=GenelToplam where ID=" + FaturaID, conp1))
                    {
                        d6.ExecuteNonQuery();
                    }
                }
            }
            catch { }
            AyarMetot.UrunAgaciEkleFatura(FaturaID.ToString(), "Fatura");


            return Json(new { sonuc = "1", Mesaj = "Başarılı", id = FaturaID });

        }


        [HttpPost]
        public JsonResult DeleteYB(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                INVOICE_DETAIL emp = db.INVOICE_DETAIL.Where(x => x.ID == id).FirstOrDefault<INVOICE_DETAIL>();
                db.INVOICE_DETAIL.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult YeniIade(Array[] data, string indirim, string CariID, string Aciklama, string KdvDh)
        {



            //08.09.2020 ŞAHİN
            //INB sts = new Satis();
            int kolon = 0;
            //sts.Tarih = DateTime.Now;
            //sts.SiparisNo = DateTime.Now.ToString("dd.MM.yyyy");
            //sts.Indirim = Convert.ToDecimal(indirim) / Convert.ToDecimal(data.Length);
            //sts.PersonelID = 1;
            //if (musID != "-1") sts.MusteriID = Convert.ToInt32(musID);

            string FaturaNo = "";
            using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
            {
                if (conp1.State == ConnectionState.Closed) conp1.Open();
                using (SqlCommand sID = new SqlCommand("select top (1) ID FROM INVOICE Order BY ID Desc", conp1))
                {
                    FaturaNo = "SI" + Convert.ToInt32(sID.ExecuteScalar());
                }
            }
            int FaturaID = -1;
            int PersonelID = -1;


            if (Session["PersonelID"] != null)
            {
                PersonelID = Convert.ToInt32(Session["PersonelID"]);

            }
            else
            {
                PersonelID = 1;

            }

            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from INVOICE", con))
                {
                    using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "INVOICE");
                        DataRow dr = ds.Tables["INVOICE"].NewRow();


                        dr["Durumu"] = "Aktif";
                        dr["FaturaType"] = "Satış İade Faturası";
                        dr["FaturaSeri"] = "SIF";
                        dr["fType"] = "sIadeF";
                        dr["kdvDH"] = KdvDh;
                        dr["KapaliAcik"] = "Acik";
                        dr["VadeKontrol"] = 'N';
                        dr["Yetkili"] = "";
                        dr["FaturaAdresi"] = "";
                        dr["SevkAdresi"] = "";
                        dr["CariVergD"] = "";
                        dr["CariVergiNo"] = "";
                        dr["IrsaliyeID"] = -1;
                        dr["EBelgeGonderimi"] = false;
                        dr["AktarilanFID"] = -1;
                        dr["FaturaNo"] = FaturaNo;

                        dr["FaturaTarihi"] = DateTime.Now.Date.Day.ToString() + "." +
                                                           DateTime.Now.Date.Month.ToString() + "." + DateTime.Now.Date.Year.ToString(); ;
                        dr["SevkTarihi"] = DateTime.Now;
                        dr["paraBirimi"] = "TL";
                        dr["CariID"] = CariID;
                        using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                        {
                            conp.Open();
                            using (SqlCommand carigetir = new SqlCommand("Select FirmaKodu,CariUnvan From Cari where ID='" + CariID + "'", conp))
                            {
                                using (SqlDataReader dc = carigetir.ExecuteReader())
                                {
                                    while (dc.Read())
                                    {
                                        dr["CariKodu"] = dc["FirmaKodu"];
                                        dr["CariUnvan"] = dc["CariUnvan"]; ;
                                    }
                                }
                            }
                        }


                        dr["Aciklama"] = Aciklama;
                        dr["Aciklama2"] = "";
                        dr["MiktarBasinaOTVDurumu"] = false;
                        dr["FatTip"] = "A";
                        dr["gVadeTarih"] = DateTime.Now;
                        dr["gKDV"] = 0;
                        dr["gIskonta"] = 0;
                        dr["HAFKomisyon"] = 0;
                        dr["gTevfikat"] = 0;
                        dr["TevkifatTur"] = "Seçilmedi";
                        dr["gIadeSuresi"] = DateTime.Now;
                        dr["Donem"] = DateTime.Now.Year;
                        dr["FaturaOzelKod"] = -1;

                        dr["personelID"] = PersonelID;
                        dr["KayitpersonelID"] = PersonelID;

                        //till here

                        //Fatura Summary goes here
                        dr["brutToplam"] = 0;
                        dr["satirIskonta"] = 0;
                        dr["ozelIskonta"] = 0;
                        dr["netToplam"] = 0;
                        dr["tKDV"] = 0;
                        dr["tOIV"] = 0;
                        dr["OTVToplami"] = 0;
                        dr["tTevfikat"] = 0;
                        dr["genelToplam"] = 0;


                        dr["FKuru"] = "1";


                        dr["YerelTutar"] = dr["genelToplam"];

                        dr["toplamIskonta"] = 0;
                        dr["g18KDV"] = 0;
                        dr["g8KDV"] = 0;
                        dr["g1KDV"] = 0;
                        dr["g19KDV"] = 0;



                        dr["YazdirmaDurumu"] = "Bekliyor";



                        dr["PrimYuzde"] = 0;


                        dr["DolarKur"] = 1;
                        dr["EuroKur"] = 1;
                        dr["chfKur"] = 1;
                        dr["gbpKur"] = 1;
                        dr["jpyKur"] = 1;



                        dr["saat"] = DateTime.Now.ToString("HH:mm");

                        dr["ServisCihazID"] = -1;
                        dr["SantiyeID"] = -1;

                        dr["AracID"] = -1;
                        dr["SiparisID"] = -1;
                        dr["ProjeID"] = -1;

                        dr["OzelAlan1"] = "";
                        dr["OzelAlan2"] = "";
                        dr["OzelAlan3"] = "";
                        dr["OzelAlan4"] = "";
                        dr["OzelAlan5"] = "";
                        dr["OzelAlan6"] = "";
                        dr["OzelAlan7"] = "";
                        dr["OzelAlan8"] = "";
                        dr["eFatDuz"] = false;

                        dr["DolarVar"] = false;
                        dr["EuroVar"] = false;
                        dr["GBPVar"] = false;
                        dr["TLEURKUR"] = 1;
                        dr["TLUSDKUR"] = 1;
                        dr["TLGBPKUR"] = 1;




                        dr["HAFID"] = -1;
                        dr["KdvliKomisyon"] = false;
                        dr["CariyeIslesin"] = true;

                        dr["IrsaliyeTuru"] = "";

                        dr["FirmaID"] = Session["FirmaID"].ToString();
                        string firmaid = Session["FirmaID"].ToString();
                        string company_code = "SA01" + firmaid.PadLeft(3, '0');
                        dr["Company_Code"] = company_code;

                        ds.Tables["INVOICE"].Rows.Add(dr);
                        da.Update(ds, "INVOICE");

                    }
                }
            }



            using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
            {
                if (conp1.State == ConnectionState.Closed) conp1.Open();
                using (SqlCommand sID = new SqlCommand(@"select top (1) ID FROM 
               INVOICE where PersonelID=" + PersonelID + " Order BY ID Desc", conp1))
                {
                    FaturaID = Convert.ToInt32(sID.ExecuteScalar());
                }
            }



            for (int i = 0; i < data.Length; i++)
            {
                string Mik = "";
                decimal Fiyat = 0;
                decimal Kdv = 18;
                int UrunID = -1;
                string Birim = "Adet";
                foreach (var veri in data[i])
                {
                    if (kolon == 0)
                    {
                        UrunID = Convert.ToInt32(veri);
                    }
                    else if (kolon == 1)
                    {
                        Mik = veri.ToString();
                    }
                    else if (kolon == 2)
                    {
                        Birim = (veri).ToString();
                    }
                    else if (kolon == 3)
                    {
                        Fiyat = Convert.ToDecimal(veri);
                    }
                    else if (kolon == 4)
                    {
                        Kdv = Convert.ToDecimal(veri);
                    }

                    kolon++;
                }
                kolon = 0;

                decimal Miktar = decimal.Parse(Mik, CultureInfo.InvariantCulture);


                try
                {

                    using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from INVOICE_DETAIL", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "INVOICE_DETAIL");
                                DataRow df = ds.Tables["INVOICE_DETAIL"].NewRow();

                                df["Durumu"] = "Aktif";
                                df["faturaID"] = FaturaID;
                                df["IslemTarihi"] = DateTime.Now;
                                df["IslemTuru"] = "Satış İade Faturası";
                                df["UrunID"] = UrunID;
                                df["SeriNo"] = "-";
                                df["depoID"] = Session["vDepoID"];
                                df["PersonelID"] = PersonelID;
                                df["Birim"] = "Adet";
                                df["Miktar"] = Miktar;
                                df["gBirimMiktar"] = Miktar;
                                df["SevkMiktar"] = 0;
                                df["IadeMiktar"] = 0;
                                df["MalzemeSayi"] = "";
                                df["IadeMiktar"] = 0;
                                df["Fiyat"] = Fiyat;
                                df["AdetFiyati"] = Fiyat;
                                if (KdvDh == "D")
                                {
                                    df["frtFiyat"] = (Fiyat * Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                    df["FtrSonDesc"] = Fiyat;
                                    df["actTutar"] = Fiyat * Miktar;
                                    df["Tutar"] = (Fiyat * Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                }
                                else
                                {

                                    df["Tutar"] = Fiyat * Miktar;
                                    df["actTutar"] = (Fiyat * Miktar) + ((Fiyat * Miktar) * Kdv / 100);
                                    df["frtFiyat"] = Fiyat;
                                    df["FtrSonDesc"] = (Fiyat) + ((Fiyat) * Kdv / 100);
                                }

                                df["mf"] = 0;
                                df["OIV"] = 0;


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
                                df["FtrSonDesc"] = df["frtFiyat"];
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
                                df["FireMiktar"] = 0;
                                df["MetreMiktarAdet"] = 0;
                                df["TedCariID"] = -1;
                                df["TakimID"] = -1;
                                df["UrunSeriNoMob"] = "";
                                df["AracPlaka"] = "";
                                df["AracPlaka2"] = "";
                                df["AracGiderIslemesin"] = false;
                                df["kpGenislik"] = 0;
                                df["KPYukseklik"] = 0;
                                df["KPIskonto"] = 0;
                                df["KPKar"] = 0;
                                df["MetreMiktarAdet"] = 0;
                                df["Motor"] = 0;
                                df["MotorSN"] = 0;
                                df["Alici"] = 0;
                                df["AliciSN"] = 0;
                                df["Kumanda"] = 0;
                                df["KumandaSN"] = 0;
                                df["GUCK"] = 0;
                                df["GUCKSN"] = 0;
                                df["STAciklama2"] = "";
                                df["KAMHAFID"] = -1;
                                df["OTV"] = 0;
                                df["UnicodeSF"] = "";
                                df["FirmaID"] = Session["FirmaID"].ToString();
                                string firmaid = Session["FirmaID"].ToString();
                                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                df["Company_Code"] = company_code;

                                ds.Tables["INVOICE_DETAIL"].Rows.Add(df);
                                da.Update(ds, "INVOICE_DETAIL");
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
                        using (SqlCommand d6 = new SqlCommand(@"  update INVOICE SET 
 GenelToplam=(Select coalesce( SUM(Miktar*Fiyat),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)
,NetToplam=(Select coalesce( SUM((Miktar*Fiyat) / ((1+ (Convert(decimal(18,2),kdv)/100)))),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID) ,
  tKDV=(Select (coalesce( SUM((Miktar*Fiyat*Convert(decimal(18,2),kdv)) / (100+ (Convert(decimal(18,2),kdv)))),0)) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)

 where ID=" + FaturaID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand d6 = new SqlCommand(@"  update INVOICE SET 
 netToplam=(Select coalesce( SUM(Miktar*Fiyat),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)
,GenelToplam=(Select coalesce( SUM((Miktar*Fiyat)+(Miktar*Fiyat)*(Convert(decimal(18,2),kdv)/100)),0)from INVOICE_DETAIL WHERE faturaID=INVOICE.ID) ,
  tKDV=(Select coalesce( SUM((Miktar*Fiyat)*(Convert(decimal(18,2),kdv)/100)),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)


 where ID=" + FaturaID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }

                    }

                    using (SqlCommand d6 = new SqlCommand("update INVOICE SET brutToplam=netToplam,YerelTutar=GenelToplam where ID=" + FaturaID, conp1))
                    {
                        d6.ExecuteNonQuery();
                    }
                }
            }
            catch { }


            return Json(new { sonuc = "1", Mesaj = "Başarılı", id = FaturaID });

        }

        public ActionResult StokBilgi(int? id = -1, string Brk = "")
        {

            using (sayazilimEntities db = new sayazilimEntities())
            {
                bool kodbulundu = true;
                int FirmaID = Convert.ToInt32(Session["FirmaID"].ToString());
                Stok emp = db.Stok.Where(x => x.ID == id && x.FirmaID == FirmaID).FirstOrDefault<Stok>();
                if (id == -1 && Brk != "")
                {

                    emp = db.Stok.Where(x => x.Barkod == Brk).FirstOrDefault<Stok>();

                    if (emp == null)
                    {

                        emp = db.Stok.Where(x => (x.Brkd2 == Brk) || (x.Brkd3 == Brk)).FirstOrDefault<Stok>();
                        if (emp == null)
                        {
                            emp = db.Stok.Where(x => (x.Brkd4 == Brk)).FirstOrDefault<Stok>();
                            if (emp == null)
                            {
                                emp = db.Stok.Where(x => (x.Brkd5 == Brk)).FirstOrDefault<Stok>();
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
                                        emp = db.Stok.Where(x => x.ID == ID && x.FirmaID == FirmaID).FirstOrDefault<Stok>();
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
        public JsonResult GetCari(string Prefix)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                int FirmaID = Convert.ToInt32(Session["FirmaID"].ToString());
                var distinctCountries1 = db.Cari.Where(x => x.FirmaID == FirmaID).ToList();

                return Json(distinctCountries1.ToList(), JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetFaturalar()
        {

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<INVOICE2> yonetim = new List<INVOICE2>();
            string sorg = @"Select  ID,CariUnvan,FaturaNo,FaturaType,FaturaTarihi,personelID,gVadeTarih,SevkTarihi,paraBirimi,Aciklama,genelToplam from INVOICE Where FirmaID =" + FirmaID;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand faturaGetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = faturaGetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            INVOICE2 yt = new INVOICE2();
                            try
                            {

                                yt.ID = Convert.ToInt32(dr["ID"]);
                                yt.CariUnvan = dr["CariUnvan"].ToString();
                                yt.FaturaNo = dr["FaturaNo"].ToString();
                                yt.FaturaType = dr["FaturaType"].ToString();
                                yt.FaturaTarihi = Convert.ToDateTime(dr["FaturaTarihi"].ToString()).ToString("dd-MM-yyyy");
                                yt.TarihF2 = Convert.ToDateTime(dr["FaturaTarihi"].ToString()).ToString("dd-MM-yyyy");
                                yt.personelID = Convert.ToInt32(dr["personelID"].ToString());
                                string vadetarih = Convert.ToDateTime(dr["gVadeTarih"]).ToString("dd.MM.yyyy");

                                yt.gVadeTarih = vadetarih;

                                yt.paraBirimi = dr["paraBirimi"].ToString();
                                yt.Aciklama = dr["Aciklama"].ToString();
                                yt.SevkTarihi = Convert.ToDateTime(dr["SevkTarihi"].ToString());
                                //yt.SevkTarihi = dr["SevkTarihi"].ToString();
                                yt.genelToplam = Convert.ToDecimal(dr["genelToplam"]).ToString("N2");

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
        public ActionResult DeleteFatura(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE INVOICE_DETAIL  where FaturaID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                }


                INVOICE emp = db.INVOICE.Where(x => x.ID == id).FirstOrDefault<INVOICE>();
                db.INVOICE.Remove(emp);
                db.SaveChanges();


                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SatisYeni(int id = 0, string FaturaTipi = "SF")
        {
            ViewBag.FTipi = "1.Fiyat";
            ViewBag.CariID = "0";
            var urun = db.Stok.ToList();
            ViewBag.Urunler = urun.ToList();
            AyarMetot.Siradaki("", "Stok", "StokKodu", Session["FirmaID"].ToString());
            ViewBag.StokKoduSiradaki2 = AyarMetot.GetNumara;
            AyarMetot.Siradaki("", "Cari", "FirmaKodu", Session["FirmaID"].ToString());
            ViewBag.CariKoduSiradaki2 = AyarMetot.GetNumara;
            ViewBag.FaturaTipi = "Satış Faturası";
            #region stokkategori

            int FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
            var kategorilist = db.STK_KATEGORI.Where(x => x.FirmaID == FirmaID).ToList();
            int i1 = 0;
            foreach (var item in kategorilist)
            {
                if (i1 == 0)
                {
                    ViewBag.Kategori1 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori1 = "Kategori 1";
                    }
                }
                else if (i1 == 1)
                {
                    ViewBag.Kategori2 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori2 = "Kategori 2";
                    }
                }
                else if (i1 == 2)
                {
                    ViewBag.Kategori3 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori3 = "Kategori 3";
                    }
                }
                else if (i1 == 3)
                {
                    ViewBag.Kategori4 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori4 = "Kategori 4";
                    }
                }
                else if (i1 == 4)
                {
                    ViewBag.Kategori5 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori5 = "Kategori 5";
                    }
                }
                else if (i1 == 5)
                {
                    ViewBag.Kategori6 = item.Name;
                    if (item.Name == "")
                    {
                        ViewBag.Kategori6 = "Kategori 6";
                    }
                }

                i1++;
            }

            #endregion
            INVOICE invoice = new INVOICE();
           
            using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
            {
                if (conp1.State == ConnectionState.Closed) conp1.Open();
                using (SqlCommand sID = new SqlCommand("select top (1) ID FROM INVOICE Order BY ID Desc", conp1))
                {
                    ViewBag.SiradakiFaturaNumarasi = "SI" + Convert.ToInt32(sID.ExecuteScalar());
                }
            }
            ViewBag.FaturaTipi = FaturaTipi;
            #region FaturaEkBilgiler

            var FaturaEkBilgi = db.INVOICE_OZEL.Where(x => x.FirmaID == FirmaID).ToList();
            int counter = 0;
            foreach (var item in FaturaEkBilgi)
            {
                if (counter == 0)
                {
                    ViewBag.EkBilgi1 = item.Name;
                }
                else if (counter == 1)
                {
                    ViewBag.EkBilgi2 = item.Name;
                }
                else if (counter == 2)
                {
                    ViewBag.EkBilgi3 = item.Name;
                }
                else if (counter == 3)
                {
                    ViewBag.EkBilgi4 = item.Name;
                }
                else if (counter == 4)
                {
                    ViewBag.EkBilgi5 = item.Name;
                }
                else if (counter == 5)
                {
                    ViewBag.EkBilgi6 = item.Name;
                }
                else if (counter == 6)
                {
                    ViewBag.EkBilgi7 = item.Name;
                }
                else if (counter == 7)
                {
                    ViewBag.EkBilgi8 = item.Name;
                }

                counter++;
            }



            #endregion


            if (id == 0)
            {
                invoice = new INVOICE();

                if (FaturaTipi == "AF")
                {
                    FaturaTipi = "AF";
                    invoice.FaturaType = "AF";
                }
                else
                {
                    FaturaTipi = "SF";
                    invoice.FaturaType = "SF";
                }
                ViewBag.FaturaTipi = FaturaTipi;

            }
            else
            {
                sayazilimEntities db = new sayazilimEntities();
                invoice = db.INVOICE.Where(x => x.ID == id).FirstOrDefault<INVOICE>();
                var servisdetaylari = db.INVOICE_DETAIL.Where(x => x.faturaID == id).ToList<INVOICE_DETAIL>();
                ViewBag.FaturaDetay = servisdetaylari.ToList();

                foreach (var item in servisdetaylari)
                {
                    item.Miktar = Convert.ToDecimal( Convert.ToDecimal(item.Miktar).ToString("N2"));
                    item.actTutar = Convert.ToDecimal(Convert.ToDecimal(item.actTutar).ToString("N2"));
                    item.Fiyat = Convert.ToDecimal(Convert.ToDecimal(item.Fiyat).ToString("N2"));

                }

                List<FaturaDetaylariEk> ftr = new List<FaturaDetaylariEk>();
                for (int i = 0; i < servisdetaylari.Count; i++)
                {
                    INVOICE_DETAIL td = servisdetaylari[i];
                    FaturaDetaylariEk stkek = new FaturaDetaylariEk();
                    stkek.UrunAdi = AyarMetot.StokBilgiCek(Convert.ToInt32(td.UrunID), "UrunAdi");


                    

                    ftr.Add(stkek);

                }

                if (invoice.FaturaType == "Satış Faturası")
                {
                    invoice.FaturaType = "SF";
                }
                if (invoice.FaturaType == "Alım Faturası")
                {
                    invoice.FaturaType = "AF";
                }
                if (invoice.FaturaType == "Sevk İrsaliyesi")
                {
                    invoice.FaturaType = "SI";
                }
                if (invoice.FaturaType == "Alım İrsaliyesi")
                {
                    invoice.FaturaType = "AI";
                }




                ViewBag.FaturaEk = servisdetaylari;

            }
            return View(invoice);

        }



        [HttpPost]
        public JsonResult SatisYeni(INVOICE data, string json, string KdvDh)
        {
            int FaturaID = data.ID;
            int TeklifID = -1;

            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from INVOICE", con))
                    {
                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds, "INVOICE");
                            DataRow dr = ds.Tables["INVOICE"].NewRow();

                            dr["Durumu"] = "Aktif";
                            if (data.FaturaType == "Sevk İrsaliyesi")
                            {
                                dr["FaturaType"] = "Sevk İrsaliyesi";
                                dr["FaturaSeri"] = "SI";
                                dr["fType"] = "SatisF";
                            }
                            else if (data.FaturaType == "Satış Faturası")
                            {
                                dr["FaturaType"] = "Satış Faturası";
                                dr["FaturaSeri"] = "SF";
                                dr["fType"] = "SatisF";

                            }
                            else if (data.FaturaType == "Alım Faturası")
                            {
                                dr["FaturaType"] = "Alım Faturası";
                                dr["FaturaSeri"] = "AF";
                                dr["fType"] = "AlimF";

                            }
                            else if (data.FaturaType == "Alım İrsaliyesi")
                            {
                                dr["FaturaType"] = "Alım İrsaliyesi";
                                dr["FaturaSeri"] = "AF";
                                dr["fType"] = "AlimF";
                            }

                            dr["kdvDH"] = KdvDh;

                            dr["KapaliAcik"] = "Acik";
                            dr["VadeKontrol"] = 'N';
                            dr["Yetkili"] = "";
                            dr["FaturaAdresi"] = "";
                            dr["SevkAdresi"] = "";
                            dr["CariVergD"] = "";
                            dr["CariVergiNo"] = "";
                            dr["IrsaliyeID"] = -1;
                            dr["EBelgeGonderimi"] = false;
                            dr["AktarilanFID"] = -1;
                            dr["FaturaNo"] = data.FaturaNo;
                            dr["FaturaTarihi"] = Convert.ToDateTime(data.FaturaTarihi).Date.Day.ToString() + "." +
        Convert.ToDateTime(data.FaturaTarihi).Date.Month.ToString() + "." + Convert.ToDateTime(data.FaturaTarihi).Date.Year.ToString();

                            dr["SevkTarihi"] = Convert.ToDateTime(data.SevkTarihi).Date.Day.ToString() + "." +
                                                Convert.ToDateTime(data.SevkTarihi).Date.Month.ToString() + "." + Convert.ToDateTime(data.SevkTarihi).Date.Year.ToString();
                            dr["gVadeTarih"] = Convert.ToDateTime(data.gVadeTarih).Date.Day.ToString() + "." + Convert.ToDateTime(data.gVadeTarih).Date.Month.ToString() + "." + Convert.ToDateTime(data.gVadeTarih).Date.Year.ToString();

                            dr["paraBirimi"] = "TL";
                            dr["CariID"] = data.CariID;
                            using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                            {
                                conp.Open();
                                using (SqlCommand carigetir = new SqlCommand("Select FirmaKodu,CariUnvan From Cari where ID='" + data.CariID + "'", conp))
                                {
                                    using (SqlDataReader dc = carigetir.ExecuteReader())
                                    {
                                        while (dc.Read())
                                        {
                                            dr["CariKodu"] = dc["FirmaKodu"];
                                            dr["CariUnvan"] = dc["CariUnvan"]; ;
                                        }
                                    }
                                }
                            }

                            dr["Aciklama"] = data.Aciklama;
                            dr["Aciklama2"] = "";
                            dr["MiktarBasinaOTVDurumu"] = false;
                            dr["FatTip"] = "A";
                            dr["gKDV"] = 0;
                            dr["gIskonta"] = 0;
                            dr["HAFKomisyon"] = 0;
                            dr["gTevfikat"] = 0;
                            dr["TevkifatTur"] = "Seçilmedi";

                            dr["gIadeSuresi"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();

                            dr["Donem"] = DateTime.Now.Year;
                            dr["FaturaOzelKod"] = -1;
                            dr["FirmaID"] = Session["FirmaID"].ToString();

                            dr["personelID"] = Session["PersonelID"].ToString();
                            dr["KayitpersonelID"] = Session["PersonelID"].ToString();

                            //till here

                            //Fatura Summary goes here
                            dr["brutToplam"] = 0;
                            dr["satirIskonta"] = 0;
                            dr["ozelIskonta"] = 0;
                            dr["netToplam"] = 0;
                            dr["tKDV"] = 0;
                            dr["tOIV"] = 0;
                            dr["OTVToplami"] = 0;
                            dr["tTevfikat"] = 0;
                            dr["genelToplam"] = 0;


                            dr["FKuru"] = "1";
                            dr["YerelTutar"] = dr["genelToplam"];

                            dr["toplamIskonta"] = 0;
                            dr["g18KDV"] = 0;
                            dr["g8KDV"] = 0;
                            dr["g1KDV"] = 0;
                            dr["g19KDV"] = 0;

                            dr["FaturaNo"] = data.FaturaNo;

                            dr["YazdirmaDurumu"] = "Bekliyor";



                            dr["PrimYuzde"] = 0;


                            dr["DolarKur"] = 1;
                            dr["EuroKur"] = 1;
                            dr["chfKur"] = 1;
                            dr["gbpKur"] = 1;
                            dr["jpyKur"] = 1;



                            dr["saat"] = DateTime.Now.ToString("HH:mm");

                            dr["ServisCihazID"] = -1;
                            dr["SantiyeID"] = -1;

                            dr["AracID"] = -1;
                            dr["SiparisID"] = -1;
                            dr["ProjeID"] = -1;

                            dr["OzelAlan1"] = data.OzelAlan1;
                            dr["OzelAlan2"] = data.OzelAlan2;
                            dr["OzelAlan3"] = data.OzelAlan3;
                            dr["OzelAlan4"] = data.OzelAlan4;
                            dr["OzelAlan5"] = data.OzelAlan5;
                            dr["OzelAlan6"] = data.OzelAlan6;
                            dr["OzelAlan7"] = data.OzelAlan7;
                            dr["OzelAlan8"] = data.OzelAlan8;
                  
                            dr["eFatDuz"] = false;

                            dr["DolarVar"] = false;
                            dr["EuroVar"] = false;
                            dr["GBPVar"] = false;
                            dr["TLEURKUR"] = 1;
                            dr["TLUSDKUR"] = 1;
                            dr["TLGBPKUR"] = 1;

                            dr["HAFID"] = -1;
                            dr["KdvliKomisyon"] = false;
                            dr["EmanetIrsaliyesi"] = false;
                            dr["CariyeIslesin"] = true;

                            dr["IrsaliyeTuru"] = "";
                            string firmaid = Session["FirmaID"].ToString();
                            string company_code = "SA01" + firmaid.PadLeft(3, '0');
                            dr["Company_Code"] = company_code;

                            ds.Tables["INVOICE"].Rows.Add(dr);
                            da.Update(ds, "INVOICE");

                        }
                    }
                }



                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(@"select top (1) ID FROM 
                            INVOICE where PersonelID=" + Session["PersonelID"].ToString() + " Order BY ID Desc", conp1))
                    {
                        FaturaID = Convert.ToInt32(sID.ExecuteScalar());
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
                            "select * from INVOICE where ID='" + FaturaID + "'", con))
                    {
                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds, "INVOICE");
                            DataRow[] adf = ds.Tables["INVOICE"].Select("ID='" + FaturaID + "'");
                            if (adf.Length != 0)
                            {
                                DataRow dr = adf[0];

                                dr["Durumu"] = "Aktif";
                                if (data.FaturaType == "Sevk İrsaliyesi")
                                {
                                    dr["FaturaType"] = "Sevk İrsaliyesi";
                                    dr["FaturaSeri"] = "SI";
                                    dr["fType"] = "SatisF";
                                }
                                else if (data.FaturaType == "Satış Faturası")
                                {
                                    dr["FaturaType"] = "Satış Faturası";
                                    dr["FaturaSeri"] = "SF";
                                    dr["fType"] = "SatisF";

                                }
                                else if (data.FaturaType == "Alım Faturası")
                                {
                                    dr["FaturaType"] = "Alım Faturası";
                                    dr["FaturaSeri"] = "AF";
                                    dr["fType"] = "AlimF";

                                }
                                else if (data.FaturaType == "Alım İrsaliyesi")
                                {
                                    dr["FaturaType"] = "Alım İrsaliyesi";
                                    dr["FaturaSeri"] = "AF";
                                    dr["fType"] = "AlimF";
                                }
                                dr["kdvDH"] = KdvDh;

                                dr["KapaliAcik"] = "Acik";
                                dr["VadeKontrol"] = 'N';
                                dr["Yetkili"] = "";
                                dr["FaturaAdresi"] = "";
                                dr["SevkAdresi"] = "";
                                dr["CariVergD"] = "";
                                dr["CariVergiNo"] = "";
                                dr["IrsaliyeID"] = -1;
                                dr["EBelgeGonderimi"] = false;
                                dr["AktarilanFID"] = -1;
                                dr["FaturaNo"] = data.FaturaNo;
                                dr["FaturaTarihi"] = Convert.ToDateTime(data.FaturaTarihi).Date.Day.ToString() + "." +
            Convert.ToDateTime(data.FaturaTarihi).Date.Month.ToString() + "." + Convert.ToDateTime(data.FaturaTarihi).Date.Year.ToString();

                                if (data.SevkTarihi != null)
                                {
                                    dr["SevkTarihi"] = Convert.ToDateTime(data.SevkTarihi).Date.Day.ToString() + "." +
                                                        Convert.ToDateTime(data.SevkTarihi).Date.Month.ToString() + "." + Convert.ToDateTime(data.SevkTarihi).Date.Year.ToString();
                                }
                                else
                                {
                                    dr["SevkTarihi"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." +
                                                       Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();

                                }
                                if (data.gVadeTarih != null)
                                {
                                    dr["gVadeTarih"] = Convert.ToDateTime(data.gVadeTarih).Date.Day.ToString() + "." + Convert.ToDateTime(data.gVadeTarih).Date.Month.ToString() + "." + Convert.ToDateTime(data.gVadeTarih).Date.Year.ToString();
                                }
                                else
                                {
                                    dr["gVadeTarih"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();


                                }
                                dr["paraBirimi"] = "TL";
                                dr["CariID"] = data.CariID;
                                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                                {
                                    conp.Open();
                                    using (SqlCommand carigetir = new SqlCommand("Select FirmaKodu,CariUnvan From Cari where ID='" + data.CariID + "'", conp))
                                    {
                                        using (SqlDataReader dc = carigetir.ExecuteReader())
                                        {
                                            while (dc.Read())
                                            {
                                                dr["CariKodu"] = dc["FirmaKodu"];
                                                dr["CariUnvan"] = dc["CariUnvan"]; ;
                                            }
                                        }
                                    }
                                }

                                dr["Aciklama"] = data.Aciklama;
                                dr["Aciklama2"] = "";
                                dr["MiktarBasinaOTVDurumu"] = false;
                                dr["FatTip"] = "A";
                                dr["gKDV"] = 0;
                                dr["gIskonta"] = 0;
                                dr["HAFKomisyon"] = 0;
                                dr["gTevfikat"] = 0;
                                dr["TevkifatTur"] = "Seçilmedi";
                                dr["gIadeSuresi"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();

                                dr["Donem"] = DateTime.Now.Year;
                                dr["FaturaOzelKod"] = -1;

                                dr["personelID"] = Session["PersonelID"].ToString();
                                dr["KayitpersonelID"] = Session["PersonelID"].ToString();

                                //till here

                                //Fatura Summary goes here
                                dr["brutToplam"] = 0;
                                dr["satirIskonta"] = 0;
                                dr["ozelIskonta"] = 0;
                                dr["netToplam"] = 0;
                                dr["tKDV"] = 0;
                                dr["tOIV"] = 0;
                                dr["OTVToplami"] = 0;
                                dr["tTevfikat"] = 0;
                                dr["genelToplam"] = 0;


                                dr["FKuru"] = "1";
                                dr["YerelTutar"] = dr["genelToplam"];

                                dr["toplamIskonta"] = 0;
                                dr["g18KDV"] = 0;
                                dr["g8KDV"] = 0;
                                dr["g1KDV"] = 0;
                                dr["g19KDV"] = 0;

                                dr["FaturaNo"] = data.FaturaNo;

                                dr["YazdirmaDurumu"] = "Bekliyor";



                                dr["PrimYuzde"] = 0;


                                dr["DolarKur"] = 1;
                                dr["EuroKur"] = 1;
                                dr["chfKur"] = 1;
                                dr["gbpKur"] = 1;
                                dr["jpyKur"] = 1;



                                dr["saat"] = DateTime.Now.ToString("HH:mm");

                                dr["ServisCihazID"] = -1;
                                dr["SantiyeID"] = -1;

                                dr["AracID"] = -1;
                                dr["SiparisID"] = -1;
                                dr["ProjeID"] = -1;
                                dr["OzelAlan1"] = data.OzelAlan1;
                                dr["OzelAlan2"] = data.OzelAlan2;
                                dr["OzelAlan3"] = data.OzelAlan3;
                                dr["OzelAlan4"] = data.OzelAlan4;
                                dr["OzelAlan5"] = data.OzelAlan5;
                                dr["OzelAlan6"] = data.OzelAlan6;
                                dr["OzelAlan7"] = data.OzelAlan7;
                                dr["OzelAlan8"] = data.OzelAlan8;
                                dr["eFatDuz"] = false;

                                dr["DolarVar"] = false;
                                dr["EuroVar"] = false;
                                dr["GBPVar"] = false;
                                dr["TLEURKUR"] = 1;
                                dr["TLUSDKUR"] = 1;
                                dr["TLGBPKUR"] = 1;

                                dr["HAFID"] = -1;
                                dr["KdvliKomisyon"] = false;
                                dr["CariyeIslesin"] = true;

                                dr["EmanetIrsaliyesi"] = false;
                                dr["IrsaliyeTuru"] = "";


                                da.Update(ds, "INVOICE");

                            }
                        }
                    }

                }
            }

            json = "[" + json + "]";


            List<INVOICE_DETAIL> items = JsonConvert.DeserializeObject<List<INVOICE_DETAIL>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                decimal Miktar=0;
                INVOICE_DETAIL er = items[i];
                if(er.Miktar != null) { 
                Miktar= decimal.Parse(er.Miktar.ToString(), CultureInfo.InvariantCulture);
                }
                else
                {
                    er.Miktar = 0;
                }
                decimal Kdv = Convert.ToDecimal(er.KDV);
                try
                {
                    if (er.ID.ToString() == "-1" || er.ID.ToString() == "0")
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from INVOICE_DETAIL", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "INVOICE_DETAIL");
                                    DataRow df = ds.Tables["INVOICE_DETAIL"].NewRow();

                                    df["Durumu"] = "Aktif";
                                    df["faturaID"] = FaturaID;
                                    df["IslemTarihi"] = DateTime.Now;
                                    if (data.FaturaType == "Sevk İrsaliyesi")
                                    {
                                        df["IslemTuru"] = "Sevk İrsaliyesi";
                                    }
                                    else if (data.FaturaType == "Satış Faturası")
                                    {
                                        df["IslemTuru"] = "Satış Faturası";

                                    }
                                    else if (data.FaturaType == "Alım Faturası")
                                    {
                                        df["IslemTuru"] = "Alım Faturası";

                                    }
                                    else if (data.FaturaType == "Alım İrsaliyesi")
                                    {
                                        df["IslemTuru"] = "Alım İrsaliyesi";
                                    }
                                    df["UrunID"] = er.UrunID;
                                    df["SeriNo"] = "-";
                                    df["depoID"] = Session["vDepoID"];
                                    df["PersonelID"] = Session["PersonelID"].ToString();
                                    df["Birim"] = er.Birim;
                                    df["Miktar"] = er.Miktar;
                                    df["gBirimMiktar"] = er.Miktar;
                                    df["BirimAdet"] = 1;
                                    df["SevkMiktar"] = 0;
                                    df["IadeMiktar"] = 0;
                                    df["MalzemeSayi"] = "";
                                    df["IadeMiktar"] = 0;
                                    df["Fiyat"] = er.Fiyat;
                                    df["AdetFiyati"] = er.Fiyat;

                                    df["mf"] = 0;
                                    df["OIV"] = 0;
                                    if (KdvDh == "D")
                                    {
                                        df["frtFiyat"] = (er.Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                        df["FtrSonDesc"] = er.Fiyat;
                                        df["actTutar"] = er.Fiyat * er.Miktar;
                                        df["Tutar"] = (er.Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                    }
                                    else
                                    {

                                        df["Tutar"] = er.Fiyat * er.Miktar;
                                        df["actTutar"] = (er.Fiyat * er.Miktar) + ((er.Fiyat * er.Miktar) * Kdv / 100);
                                        df["frtFiyat"] = er.Fiyat;
                                        df["FtrSonDesc"] = (er.Fiyat) + ((er.Fiyat) * Kdv / 100);
                                    }

                                    df["pBirimi"] = AyarMetot.PB_Short;

                                    df["VadeTarih"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();
                                    df["IadeSuresi"] = Convert.ToDateTime(DateTime.Now).Date.Day.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Month.ToString() + "." + Convert.ToDateTime(DateTime.Now).Date.Year.ToString();
                                    df["KDV"] = er.KDV;
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
                                    df["FireMiktar"] = 0;
                                    df["MetreMiktarAdet"] = 0;
                                    df["TedCariID"] = -1;
                                    df["TakimID"] = -1;
                                    df["UrunSeriNoMob"] = "";
                                    df["AracPlaka"] = "";
                                    df["AracPlaka2"] = "";
                                    df["AracGiderIslemesin"] = false;
                                    df["kpGenislik"] = 0;
                                    df["KPYukseklik"] = 0;
                                    df["KPIskonto"] = 0;
                                    df["KPKar"] = 0;
                                    df["MetreMiktarAdet"] = 0;
                                    df["Motor"] = 0;
                                    df["MotorSN"] = 0;
                                    df["Alici"] = 0;
                                    df["AliciSN"] = 0;
                                    df["Kumanda"] = 0;
                                    df["KumandaSN"] = 0;
                                    df["GUCK"] = 0;
                                    df["GUCKSN"] = 0;
                                    df["STAciklama2"] = "";
                                    df["KAMHAFID"] = -1;
                                    df["OTV"] = 0;
                                    df["UnicodeSF"] = "";

                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                    df["Company_Code"] = company_code;

                                    ds.Tables["INVOICE_DETAIL"].Rows.Add(df);
                                    da.Update(ds, "INVOICE_DETAIL");
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
                                    "select * from INVOICE_DETAIL where ID='" + er.ID + "'", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "INVOICE_DETAIL");
                                    DataRow[] adf = ds.Tables["INVOICE_DETAIL"].Select("ID='" + er.ID + "'");
                                    if (adf.Length != 0)
                                    {
                                        DataRow df = adf[0];
                                        df["Durumu"] = "Aktif";
                                        df["faturaID"] = FaturaID;
                                        df["IslemTarihi"] = DateTime.Now;
                                        df["IslemTuru"] = "Sevk İrsaliyesi";
                                        df["UrunID"] = er.UrunID;
                                        df["SeriNo"] = "-";
                                        df["depoID"] = Session["vDepoID"];
                                        df["PersonelID"] = Session["PersonelID"].ToString();
                                        df["Birim"] = er.Birim;
                                        df["Miktar"] = er.Miktar;
                                        df["gBirimMiktar"] = er.Miktar;
                                        df["SevkMiktar"] = 0;
                                        df["IadeMiktar"] = 0;
                                        df["MalzemeSayi"] = "";
                                        df["IadeMiktar"] = 0;
                                        df["Fiyat"] = er.Fiyat;
                                        df["AdetFiyati"] = er.Fiyat;

                                        df["mf"] = 0;
                                        df["OIV"] = 0;
                                        if (KdvDh == "D")
                                        {
                                            df["frtFiyat"] = (er.Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                            df["FtrSonDesc"] = er.Fiyat;
                                            df["actTutar"] = Convert.ToDecimal(er.Fiyat) * Convert.ToDecimal(er.Miktar);
                                            df["Tutar"] = (er.Fiyat * er.Miktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                        }
                                        else
                                        {

                                            df["Tutar"] = er.Fiyat * er.Miktar;
                                            df["actTutar"] = Convert.ToDecimal(er.Fiyat) * Convert.ToDecimal(er.Miktar) + ((Convert.ToDecimal(er.Fiyat) * Convert.ToDecimal(er.Miktar)) *Kdv / 100);
                                            df["frtFiyat"] = er.Fiyat;
                                            df["FtrSonDesc"] = (er.Fiyat) + ((er.Fiyat) * Kdv / 100);                                   


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
                                        df["FireMiktar"] = 0;
                                        df["MetreMiktarAdet"] = 0;
                                        df["TedCariID"] = -1;
                                        df["TakimID"] = -1;
                                        df["UrunSeriNoMob"] = "";
                                        df["AracPlaka"] = "";
                                        df["AracPlaka2"] = "";
                                        df["AracGiderIslemesin"] = false;
                                        df["kpGenislik"] = 0;
                                        df["KPYukseklik"] = 0;
                                        df["KPIskonto"] = 0;
                                        df["KPKar"] = 0;
                                        df["MetreMiktarAdet"] = 0;
                                        df["Motor"] = 0;
                                        df["MotorSN"] = 0;
                                        df["Alici"] = 0;
                                        df["AliciSN"] = 0;
                                        df["Kumanda"] = 0;
                                        df["KumandaSN"] = 0;
                                        df["GUCK"] = 0;
                                        df["GUCKSN"] = 0;
                                        df["STAciklama2"] = "";
                                        df["KAMHAFID"] = -1;
                                        df["OTV"] = 0;
                                        df["UnicodeSF"] = "";
                                        df["BirimAdet"] = 1;

                                        da.Update(ds, "INVOICE_DETAIL");

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
                        using (SqlCommand d6 = new SqlCommand(@"  update INVOICE SET 
 GenelToplam=(Select coalesce( SUM(Miktar*Fiyat),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)
,NetToplam=(Select coalesce( SUM((Miktar*Fiyat) / ((1+ (Convert(decimal(18,2),kdv)/100)))),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID) ,
  tKDV=(Select (coalesce( SUM((Miktar*Fiyat*Convert(decimal(18,2),kdv)) / (100+ (Convert(decimal(18,2),kdv)))),0)) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)

 where ID=" + FaturaID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand d6 = new SqlCommand(@"  update INVOICE SET 
 netToplam=(Select coalesce( SUM(Miktar*Fiyat),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)
,GenelToplam=(Select coalesce( SUM((Miktar*Fiyat)+(Miktar*Fiyat)*(Convert(decimal(18,2),kdv)/100)),0)from INVOICE_DETAIL WHERE faturaID=INVOICE.ID) ,
  tKDV=(Select coalesce( SUM((Miktar*Fiyat)*(Convert(decimal(18,2),kdv)/100)),0) from INVOICE_DETAIL WHERE faturaID=INVOICE.ID)


 where ID=" + FaturaID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }

                    }

                    using (SqlCommand d6 = new SqlCommand("update INVOICE SET brutToplam=netToplam,YerelTutar=GenelToplam where ID=" + FaturaID, conp1))
                    {
                        d6.ExecuteNonQuery();
                    }
                }
            }
            catch { }
            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}