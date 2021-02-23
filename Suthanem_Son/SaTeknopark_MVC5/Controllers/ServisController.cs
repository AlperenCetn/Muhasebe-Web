using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaTeknopark_MVC5.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Data.Entity.Validation;

//D

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class ServisController : Controller
    {
        sayazilimEntities db = new sayazilimEntities();

        public ActionResult BakimListesi()
        {
            return View();
        }

        public class Resim
        {
            public HttpPostedFileBase Dosya { get; set; }
            public int ID { get; set; }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(Resim resim, int id = 0)
        {
            if (Request.Files.Count > 0)
            {
                var filenamecmr = Path.GetFileName(resim.Dosya.FileName);
                var kayityeri = Path.Combine(Server.MapPath("~/Content/File/"), resim.ID + "_SRV_" + filenamecmr);
                id = resim.ID;

                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                bool kayitvar = false;

                try
                {
                    using (SqlConnection conp = new SqlConnection(strcon))
                    {
                        if (conp.State == ConnectionState.Closed) conp.Open();
                        using (SqlCommand command = new SqlCommand("SELECT count(ID) From ServisBelgeler where ServisID='" + id + "' ", conp))
                        {
                            if (Convert.ToInt32(command.ExecuteScalar()) >= 1)
                            {
                                kayitvar = true;
                            }
                        }
                    }
                }
                catch
                { }


                if (kayitvar == false)
                {


                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter daBordro = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from ServisBelgeler", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(daBordro))
                            {
                                DataSet dsServisBelgeler = new DataSet();
                                daBordro.Fill(dsServisBelgeler, "ServisBelgeler");
                                DataRow dr = dsServisBelgeler.Tables["ServisBelgeler"].NewRow();

                                dr["BelgeAdi"] = resim.ID + "_SRV_" + filenamecmr;
                                dr["ServisID"] = resim.ID;
                                dsServisBelgeler.Tables["ServisBelgeler"].Rows.Add(dr);
                                daBordro.Update(dsServisBelgeler, "ServisBelgeler");

                            }
                        }
                    }
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();

                        using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select * from ServisBelgeler where ServisID='" + resim.ID + "'", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds1 = new DataSet();
                                da.Fill(ds1, "ServisBelgeler");
                                DataRow[] adf = ds1.Tables["ServisBelgeler"].Select("ServisID='" + resim.ID + "'");
                                if (adf.Length != 0)
                                {
                                    DataRow dr = adf[0];

                                    dr["BelgeAdi"] = resim.ID + "_SRV_" + filenamecmr;
                                    dr["ServisID"] = resim.ID;

                                    da.Update(ds1, "ServisBelgeler");

                                }
                            }
                        }
                    }
                }

                resim.Dosya.SaveAs(kayityeri);



                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    // burada dilerseniz dosya tipine gore filtreleme yaparak sadece istediginiz dosya formatindaki dosyalari yukleyebilirsiniz
                    //if (file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/png" || file.ContentType == "image/gif")
                    //{
                    var fi = new FileInfo(file.FileName);
                    var fileName = Path.GetFileName(file.FileName);
                    fileName = Guid.NewGuid().ToString() + fi.Extension;
                    var path = Path.Combine(Server.MapPath("~/Content/File/"), fileName);
                    file.SaveAs(path);
                    //}
                }
            }

            var result = new { sonuc = 1, Message = "BELGENİZ BAŞARIYLA YÜKLENDİ!", emp = id };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public FileResult DosyaGoster(string DosyaAd, int FaturaID = -1)
        {

            string[] dizi = DosyaAd.Split('.');
            string Uzantı = dizi[dizi.Length - 1];
            byte[] dosya = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/File/") + DosyaAd);
            if (Uzantı == "pdf")
            {
                return File(dosya, "application/pdf");
            }
            else if (Uzantı == "jpeg")
            {
                return File(dosya, "image/jpeg");
            }
            else if (Uzantı == "txt")
            {
                return File(dosya, "text/plain");
            }
            else if (Uzantı == "jpg")
            {
                return File(dosya, "image/jpg");
            }
            else if (Uzantı == "png")
            {
                return File(dosya, "image/png");

            }
            else if (Uzantı == "xlsx")
            {
                return File(dosya, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            }
            else if (Uzantı == "xls")
            {
                return File(dosya, "application/vnd.ms-excel");

            }
            else if (Uzantı == "docx" || Uzantı == "doc")
            {
                return File(dosya, "application/msword");

            }
            else
            {
                return File(dosya, System.Net.Mime.MediaTypeNames.Application.Octet);

            }

        }

        public partial class TECHNICAL_PERIOD2
        {
            public int ID { get; set; }
            public Nullable<int> ServisID { get; set; }
            public Nullable<System.DateTime> Tarih { get; set; }
            public Nullable<System.DateTime> BakimTarihi { get; set; }
            public string Durumu { get; set; }
            public string Aciklama { get; set; }
            public string CariUnvan { get; set; }
            public string Telefon { get; set; }
            public string ServisNo { get; set; }
            public string Marka { get; set; }
            public string Model { get; set; }
            public string Cinsi { get; set; }

        }

        public ActionResult GetBakimListesi()
        {

            List<TECHNICAL_PERIOD2> yonetim = new List<TECHNICAL_PERIOD2>();

            string FirmaID = Session["FirmaID"].ToString();
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;


            string sorg = @"set dateformat dmy; Select TECHNICAL_PERIOD.ID,TECHNICAL_PERIOD.ServisID, BakimTarihi as [Tarih],TECHNICAL_PERIOD.Durumu,(Select CariUnvan from cari where ID=CariID) as Cari,(Select case when Telefon1<>''then Telefon1 else GSM end  from cari where ID=CariID) as Telefon,
              ServisIslemNo as ServisNo,TECHNICAL_PERIOD.Aciklama as Aciklama, Marka, Model, Cinsi , TECHNICAL_PERIOD.ID from TECHNICAL_PERIOD, TECHNICAL
              where TECHNICAL.ID = TECHNICAL_PERIOD.ServisID and TECHNICAL.FirmaID=" + FirmaID + "   Order by Tarih ";



            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            TECHNICAL_PERIOD2 yt = new TECHNICAL_PERIOD2();

                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.ServisID = Convert.ToInt32(dr["ServisID"]);

                            yt.Tarih = Convert.ToDateTime(dr["Tarih"]);
                            //yt.BakimTarihi = Convert.ToDateTime(dr["BakimTarihi"]);
                            yt.Durumu = dr["Durumu"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();
                            yt.CariUnvan = dr["Cari"].ToString();
                            yt.Telefon = dr["Telefon"].ToString();
                            yt.ServisNo = Convert.ToString(dr["ServisNo"]);
                            yt.Marka = dr["Marka"].ToString();
                            yt.Model = dr["Model"].ToString();
                            yt.Cinsi = dr["Cinsi"].ToString();


                            yonetim.Add(yt);


                        }

                    }
                }
            }



            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult BakimDurumu(TECHNICAL_PERIOD tc)
        {
            TECHNICAL_PERIOD emp = db.TECHNICAL_PERIOD.Where(x => x.ServisID == tc.ServisID).FirstOrDefault<TECHNICAL_PERIOD>();
            emp.Durumu = tc.Durumu;

            db.SaveChanges();
            string Message = "Durum Güncellendi";

            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public FileResult ServisYazdir(string ID = "")
        {
            FastReport.Utils.Config.WebMode = true;
            FastReport.Report report1 = new FastReport.Report();

            report1.Prepare(true);

            try
            {
                report1.Load(Path.Combine(Server.MapPath("~/SERVISBELGELER"), "Servis.frx"));
            }
            catch { }


            Datasetler ds = new Datasetler();
            ServisBilgileriYukle(ds, ID);

            report1.RegisterData((DataTable)ds.Cihaz, "Cihaz");
            report1.GetDataSource("Cihaz").Enabled = true;


            report1.RegisterData((DataTable)ds.Cari_Bilgileri, "Cari Bilgileri");
            report1.GetDataSource("Cari Bilgileri").Enabled = true;

            report1.RegisterData((DataTable)ds.FirmaBilgileri, "FirmaBilgileri");
            report1.GetDataSource("FirmaBilgileri").Enabled = true;

            report1.RegisterData((DataTable)ds.Cihaz_Durum_Geçmişi, "Cihaz Durum Geçmişi");
            report1.GetDataSource("Cihaz Durum Geçmişi").Enabled = true;


            report1.RegisterData((DataTable)ds.CihazDetay, "CihazDetay");
            report1.GetDataSource("CihazDetay").Enabled = true;

            using (FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport())
            {


                report1.Prepare(true);


                report1.Export(pdf, Path.Combine(Server.MapPath("~/SERVISBELGELER"), "Servis.pdf"));


                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                report1.Save(Path.Combine(Server.MapPath("~/SERVISBELGELER"), "Servis.frx"));
                report1.Dispose();
                byte[] dosya = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/SERVISBELGELER"), "Servis.pdf"));
                return File(dosya, "application/pdf");
            }

        }
        private void INVOICE_DETAILBilgileri(Datasetler SADataset1, string pID, decimal fkuru, string KDVdh)
        {
            using (SqlConnection bag = new SqlConnection(AyarMetot.conString))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();


                DataRow ds;
                using (SqlConnection con7 = new SqlConnection(AyarMetot.conString))
                {
                    con7.Open();
                    using (SqlCommand servisgetir = new SqlCommand("Select * From TECHNICAL_DETAIL where CihazID='" + pID + "'", con7))
                    {
                        using (SqlDataReader da = servisgetir.ExecuteReader())
                        {
                            int a = 1;
                            while (da.Read())
                            {


                                int stkID = Convert.ToInt32(da["UrunID"]);

                                ds = SADataset1.CihazDetay.NewRow();

                                ds["ID"] = da["CihazID"].ToString();

                                ds["Sıra NO"] = a.ToString();

                                try
                                {
                                    using (SqlCommand d6 = new SqlCommand("Select StokKodu From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Ürün Kodu"] = d6.ExecuteScalar();
                                    }
                                }
                                catch { }

                                try
                                {
                                    using (SqlCommand d6 = new SqlCommand("Select UrunAdi From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Ürün Adı"] = d6.ExecuteScalar().ToString().Replace("İ", "I").Replace("Ğ", "G").Replace("Ş", "S").Replace("ş", "s");
                                    }
                                }
                                catch { }

                                try
                                {
                                    using (SqlCommand d6 = new SqlCommand("Select Marka From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Marka"] = d6.ExecuteScalar();
                                    }
                                }
                                catch { }

                                try
                                {
                                    using (SqlCommand command = new SqlCommand("SELECT KodAdi From OzelKod where ID='" + da["SatirOzelKodID"] + "'", bag))
                                    {
                                        ds["Satır Özel Kod"] = command.ExecuteScalar();
                                    }
                                }
                                catch
                                { }





                                int sfID = -1;

                                ds["sfID"] = Convert.ToInt32(da["ID"]);
                                sfID = Convert.ToInt32(da["ID"]);

                                ds["Seri Numara"] = da["SeriNo"].ToString();


                                try
                                {
                                    using (SqlCommand d6 = new SqlCommand("Select DepoAdi From Depo where ID='" + da["depoID"] + "'", bag))
                                    {
                                        ds["Depo"] = d6.ExecuteScalar();
                                    }
                                }
                                catch { }

                                ds["Birim"] = da["Birim"].ToString();

                                if (da["Birim"].ToString().ToLower() == "adet")
                                {

                                    ds["Miktar"] = Convert.ToInt32(da["Miktar"]).ToString();
                                    ds["Miktar_Num"] = Convert.ToInt32(da["Miktar"]).ToString();
                                    ds["Mal Fazlası"] = Convert.ToInt32(0).ToString();
                                    ds["Miktar (MF Dahil)"] = (Convert.ToInt32(da["Miktar"])).ToString();

                                }
                                else
                                {

                                    ds["Miktar"] = Convert.ToDecimal(da["Miktar"]).ToString("N" + "" + AyarMetot.Amount_Yuvarlama + "");
                                    ds["Miktar_Num"] = Convert.ToDecimal(da["Miktar"]).ToString();
                                    ds["Mal Fazlası"] = Convert.ToDecimal(0).ToString("N" + "" + AyarMetot.Amount_Yuvarlama + "");
                                    ds["Miktar (MF Dahil)"] = (Convert.ToDecimal(da["Miktar"])).ToString("N" + "" + AyarMetot.Amount_Yuvarlama + "");

                                }

                                ds["Fiyat"] = Math.Round(Convert.ToDecimal(da["Fiyat"]), AyarMetot.Price_Yuvarlama).ToString();
                                ds["Fiyat_Num"] = Convert.ToDecimal(da["Fiyat"]).ToString();
                                ds["Birim Adet"] = Math.Round(Convert.ToDecimal(da["BirimAdet"]), AyarMetot.Stok_Yuvarlama).ToString();
                                ds["Toplam Miktar (Ana Birim)"] = Math.Round(Convert.ToDecimal(da["gBirimMiktar"]), AyarMetot.Stok_Yuvarlama).ToString();
                                ds["Miktar (Ana Birim)"] = Math.Round(Convert.ToDecimal(da["BirimAdet"]), AyarMetot.Stok_Yuvarlama).ToString();
                                ds["Adet Fiyatı"] = Math.Round(Convert.ToDecimal(da["Fiyat"]), AyarMetot.Price_Yuvarlama).ToString();
                                ds["KDV"] = Convert.ToInt32(da["KDV"]).ToString();

                                ds["Tutar"] = Math.Round(Convert.ToDecimal(da["Tutar"]), AyarMetot.Price_Yuvarlama).ToString("N2");
                                ds["Tutar_Num"] = Convert.ToDecimal(da["Tutar"]).ToString();

                                ds["Kdv Dahil Fiyat"] = Math.Round(Convert.ToDecimal(da["Fiyat"]), AyarMetot.Price_Yuvarlama).ToString("N2");
                                ds["Kdv Hariç Fiyat"] = Math.Round(Convert.ToDecimal(da["Fiyat"]), AyarMetot.Price_Yuvarlama).ToString("N2");
                                ds["Kdv Dahil Tutar"] = Math.Round(Convert.ToDecimal(da["actTutar"]), AyarMetot.Price_Yuvarlama).ToString("N2");
                                ds["Kdv Hariç Tutar"] = Math.Round(Convert.ToDecimal(da["Tutar"]), AyarMetot.Price_Yuvarlama).ToString("N2");

                                ds["Kdv Dahil Fiyat_Num"] = Convert.ToDecimal(da["Fiyat"]).ToString();
                                ds["Kdv Hariç Fiyat_Num"] = Convert.ToDecimal(da["Fiyat"]).ToString();
                                ds["Kdv Dahil Tutar_Num"] = Convert.ToDecimal(da["actTutar"]).ToString();
                                ds["Kdv Hariç Tutar_Num"] = Convert.ToDecimal(da["Tutar"]).ToString();

                                ds["Kdv Dahil Fiyat (İskontolu)_Num"] = ((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) - (((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                ds["Kdv Hariç Fiyat (İskontolu)_Num"] = (Convert.ToDecimal(da["Fiyat"]) - ((Convert.ToDecimal(da["Fiyat"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                ds["Kdv Dahil Tutar (İskontolu)_Num"] = (Convert.ToDecimal(da["actTutar"]) - ((Convert.ToDecimal(da["actTutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                ds["Kdv Hariç Tutar (İskontolu)_Num"] = (Convert.ToDecimal(da["Tutar"]) - ((Convert.ToDecimal(da["Tutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();

                                if (KDVdh == "Dahil")
                                {
                                    decimal haric = Convert.ToDecimal(da["Fiyat"]) / (1 + (Convert.ToDecimal(da["KDV"]) / 100));

                                    ds["Kdv Dahil Fiyat_Num"] = Convert.ToDecimal(da["Fiyat"]).ToString();
                                    ds["Kdv Hariç Fiyat_Num"] = haric.ToString();
                                    ds["Kdv Dahil Tutar_Num"] = ((Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]))).ToString();
                                    ds["Kdv Hariç Tutar_Num"] = Convert.ToDecimal(da["Tutar"]).ToString();

                                    ds["Kdv Dahil Fiyat (İskontolu)_Num"] = ((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) - (((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                    ds["Kdv Hariç Fiyat (İskontolu)_Num"] = (Convert.ToDecimal(da["Fiyat"]) - ((Convert.ToDecimal(da["Fiyat"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                    ds["Kdv Dahil Tutar (İskontolu)_Num"] = (Convert.ToDecimal(da["actTutar"]) - ((Convert.ToDecimal(da["actTutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                    ds["Kdv Hariç Tutar (İskontolu)_Num"] = (Convert.ToDecimal(da["Tutar"]) - ((Convert.ToDecimal(da["Tutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                }
                                else if (KDVdh == "Hariç")
                                {
                                    ds["Kdv Dahil Fiyat_Num"] = ((Convert.ToDecimal(da["Fiyat"])) * (1 + (Convert.ToDecimal(da["KDV"]) / 100))).ToString();
                                    ds["Kdv Hariç Fiyat_Num"] = Convert.ToDecimal(da["Fiyat"]).ToString();
                                    ds["Kdv Dahil Tutar_Num"] = ((Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]) * (1 + (Convert.ToDecimal(da["KDV"]) / 100)))).ToString();
                                    ds["Kdv Hariç Tutar_Num"] = Convert.ToDecimal(da["Tutar"]).ToString();

                                    ds["Kdv Dahil Fiyat (İskontolu)_Num"] = ((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) - (((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                    ds["Kdv Hariç Fiyat (İskontolu)_Num"] = (Convert.ToDecimal(da["Fiyat"]) - ((Convert.ToDecimal(da["Fiyat"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                    ds["Kdv Dahil Tutar (İskontolu)_Num"] = (Convert.ToDecimal(da["actTutar"]) - ((Convert.ToDecimal(da["actTutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                    ds["Kdv Hariç Tutar (İskontolu)_Num"] = (Convert.ToDecimal(da["Tutar"]) - ((Convert.ToDecimal(da["Tutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                }
                                ds["Kdv Dahil Fiyat (İskontolu)"] = Math.Round((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) - (((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) / 100) * Convert.ToDecimal(da["Iskonta"])), AyarMetot.Price_Yuvarlama).ToString("N2");
                                ds["Kdv Hariç Fiyat (İskontolu)"] = Math.Round(Convert.ToDecimal(da["Fiyat"]) - ((Convert.ToDecimal(da["Fiyat"]) / 100) * Convert.ToDecimal(da["Iskonta"])), AyarMetot.Price_Yuvarlama).ToString("N2");
                                ds["Kdv Dahil Tutar (İskontolu)"] = Math.Round(Convert.ToDecimal(da["actTutar"]) - ((Convert.ToDecimal(da["actTutar"]) / 100) * Convert.ToDecimal(da["Iskonta"])), AyarMetot.Price_Yuvarlama).ToString("N2");
                                ds["Kdv Hariç Tutar (İskontolu)"] = Math.Round(Convert.ToDecimal(da["Tutar"]) - ((Convert.ToDecimal(da["Tutar"]) / 100) * Convert.ToDecimal(da["Iskonta"])), AyarMetot.Price_Yuvarlama).ToString("N2");

                                decimal yereltutar = (Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]) / Convert.ToDecimal(da["Kur"]));
                                decimal yerelfiyat = (Convert.ToDecimal(da["Fiyat"]) / Convert.ToDecimal(da["Kur"]));

                                ds["Yerel Tutar Satır (İskontolu)"] = (yereltutar - ((yereltutar / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString("N2");
                                ds["Yerel Fiyat Satır (İskontolu)"] = (yerelfiyat - ((yerelfiyat / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString("N2");


                                ds["Para Birimi"] = da["pBirimi"].ToString();

                                if (da["pBirimi"].ToString() == "TL")
                                    ds["Para Birimi"] = "TL";
                                else
                                    ds["Para Birimi"] = da["pBirimi"].ToString();

                                ds["Vade Tarihi"] = da["VadeTarih"].ToString();

                                ds["Tevfikat"] = Convert.ToDecimal(da["Tevfikat"]).ToString("#,##0.00");
                                ds["İade Süresi"] = da["IadeSuresi"].ToString();

                                ds["Variyant"] = da["Variyant"].ToString();
                                //ds["Kur"] = da["Kur"].ToString();
                                ds["İskonto"] = Convert.ToDecimal(da["Iskonta"]).ToString("#,##0.00");
                                ds["İskonto 2"] = Convert.ToDecimal(da["Iskonta2"]).ToString("#,##0.00");
                                ds["İskonto 3"] = Convert.ToDecimal(da["Iskonta3"]).ToString("#,##0.00");
                                ds["İskonto 4"] = Convert.ToDecimal(da["Iskonta4"]).ToString("#,##0.00");
                                ds["İskonto 5"] = Convert.ToDecimal(da["Iskonta5"]).ToString("#,##0.00");

                                try
                                {
                                    using (SqlCommand d6 = new SqlCommand("Select Barkod From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["BARKODE"] = d6.ExecuteScalar();
                                    }
                                }
                                catch { }

                                ds["Yerel Tutar Satır"] = (Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]) / Convert.ToDecimal(da["Kur"])).ToString("#,##0.00");
                                ds["Yerel Fiyat Satır"] = (Convert.ToDecimal(da["Fiyat"]) / Convert.ToDecimal(da["Kur"])).ToString("#,##0.00");
                                ds["Yerel Tutar Satır_Num"] = (Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]) / Convert.ToDecimal(da["Kur"])).ToString();
                                ds["Yerel Fiyat Satır_Num"] = (Convert.ToDecimal(da["Fiyat"]) / Convert.ToDecimal(da["Kur"])).ToString();
                                ds["Satır Açıklaması"] = da["EkAciklama"].ToString();

                                try
                                {
                                    using (SqlCommand d6 = new SqlCommand("Select Adi From EkOzellik where ID='" + da["EkOzellikID"] + "'", bag))
                                    {
                                        ds["Ek Özellik Adı"] = d6.ExecuteScalar();
                                    }
                                }
                                catch { }

                                try
                                {
                                    using (SqlCommand d6 = new SqlCommand("Select Aciklama From EkOzellik where ID='" + da["EkOzellikID"] + "'", bag))
                                    {
                                        ds["Ek Özellik Açıklaması"] = d6.ExecuteScalar();
                                    }
                                }
                                catch { }

                                try
                                {
                                    ds["ÖTV"] = Convert.ToDecimal(da["otv"]).ToString("#,##0.00");
                                }
                                catch { }


                                decimal tlToplam = (Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]));
                                decimal lToplam = (Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]));
                                try
                                {
                                    lToplam = lToplam * (1 + (Convert.ToDecimal(da["otv"]) / 100));
                                    ds["Satır ÖTV Tutarı"] = (lToplam - tlToplam).ToString("#,##0.00");
                                }
                                catch { ds["Satır ÖTV Tutarı"] = (0).ToString("#,##0.00"); }




                                try
                                {
                                    ds["Metraj Miktarı"] = Convert.ToInt32(da["MetrajMiktari"]).ToString();

                                }
                                catch { }
                                try
                                {
                                    ds["Hizmet Sonlanma Tarihi"] = Convert.ToDateTime(da["HizmetSonlanma"]).ToString("dd.MM.yyyy");
                                }
                                catch { }

                                try
                                {
                                    ds["Teslim Tarihi"] = Convert.ToDateTime(da["Teslim Tarihi"]).ToString("dd.MM.yyyy");
                                }
                                catch { }




                                SADataset1.CihazDetay.Rows.Add(ds);
                                a++;
                            }
                        }
                    }
                }
            }
        }
        private void CariBilgileri(Datasetler SADataset1, string CariID)

        {
            using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
            {
                conp.Open();
                using (SqlCommand carigetir = new SqlCommand("Select * From Cari where ID='" + CariID + "'", conp))
                {
                    using (SqlDataReader cr = carigetir.ExecuteReader())
                    {

                        while (cr.Read())
                        {



                            DataRow ds = SADataset1.Cari_Bilgileri.NewRow();


                            ds["ID"] = cr["ID"].ToString();
                            ds["Firma Kodu"] = cr["FirmaKodu"].ToString();
                            ds["Cari Tipi"] = cr["KTipi"].ToString();
                            ds["Ünvanı"] = cr["CariUnvan"].ToString().Replace("İ", "I").Replace("Ğ", "G").Replace("Ş", "S").Replace("ş", "s");
                            ds["Vergi Dairesi"] = cr["VergiDr"].ToString();
                            ds["Vergi Daire Numarası"] = cr["VergiNo"].ToString();
                            ds["İlgili Kişi"] = cr["yetkili"].ToString();
                            ds["TC Numarası"] = cr["TCNo"].ToString();
                            ds["Adres"] = cr["Adres"].ToString();
                            ds["İl"] = cr["IL"].ToString().ToUpper();
                            ds["İlçe"] = cr["Ilce"].ToString();
                            ds["Ülke"] = cr["Ulke"].ToString();
                            ds["Telefon 1"] = cr["Telefon1"].ToString();
                            ds["GSM"] = cr["GSM"].ToString();
                            ds["Faks"] = cr["Faks"].ToString();
                            ds["E-Posta"] = cr["EPosta"].ToString();
                            ds["WebSite"] = cr["WebSite"].ToString();
                            ds["Posta Kodu"] = cr["PostaKodu"].ToString();
                            ds["Fatura Para Birimi"] = cr["paraBirimi"].ToString();
                            ds["Özel İskonto"] = cr["Iskonto"].ToString();



                            ds["Cari Banka IBAN"] = "";
                            ds["Cari Banka Hesap No"] = "";
                            ds["Cari Banka"] = "";
                            ds["Cari Banka IBAN"] = "";
                            ds["Cari Banka IBAN"] = "";


                            using (SqlConnection bag1 = new SqlConnection(AyarMetot.conString))
                            {
                                bag1.Open();
                                using (SqlCommand getir = new SqlCommand("Select alacakB,borcB,paraBirimi From BALANCE where cariID='" + CariID + "' and ParaBirimi='" + cr["paraBirimi"].ToString() + "'", bag1))
                                {
                                    using (SqlDataReader cb = getir.ExecuteReader())
                                    {

                                        while (cb.Read())
                                        {

                                            ds["Alacak Bakiyesi"] = Convert.ToDecimal(cb["alacakB"]).ToString();
                                            ds["Borç Bakiyesi"] = Convert.ToDecimal(cb["borcB"]).ToString();
                                            ds["Net Bakiye"] = (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])).ToString();

                                            if (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]) < 0)
                                                ds["Net Bakiye Borç Alacak"] = (-(Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]))).ToString("N2").ToString() + " (B)";
                                            else if (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]) > 0)
                                                ds["Net Bakiye Borç Alacak"] = (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])).ToString("N2").ToString() + " (A)";
                                            else
                                                ds["Net Bakiye Borç Alacak"] = (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])).ToString("N2").ToString() + " ( )";



                                        }
                                    }
                                }
                            }


                            SADataset1.Cari_Bilgileri.Rows.Add(ds);






                        }
                    }
                }
            }
        }

        private void ServisBilgileriYukle(Datasetler SADataset1, string pID = "")
        {


            SADataset1.Clear();


            AyarMetot.COMPANY_DETAIL(SADataset1);

            int cariID = -1;


            using (SqlConnection bag = new SqlConnection(AyarMetot.conString))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand getir = new SqlCommand("Select * From TECHNICAL where ID='" + pID + "'", bag))
                {
                    using (SqlDataReader dt = getir.ExecuteReader())
                    {

                        while (dt.Read())
                        {
                            DataRow ds = SADataset1.Cihaz.NewRow();

                            cariID = Convert.ToInt32(dt["CariID"]);

                            CariBilgileri(SADataset1, cariID.ToString());

                            int PersonelID = 1;

                            try
                            {
                                PersonelID = Convert.ToInt32(dt["Personel"]);
                            }
                            catch { }
                            cariID = Convert.ToInt32(dt["CariID"]);

                            ds["Servis İşlem No"] = dt["ServisIslemNo"].ToString();
                            ds["Seri Numarası"] = dt["Serino"].ToString();
                            try
                            {
                                ds["Onarım Tarihi"] = Convert.ToDateTime(dt["OnarimTarih"]).ToString("dd.MM.yyy");
                            }
                            catch { }

                            try
                            {
                                ds["Tahmini Onarım Tarihi"] = Convert.ToDateTime(dt["TahminiOnarimTarih"]).ToString("dd.MM.yyy");
                            }
                            catch { }

                            try
                            {
                                ds["İşlem Tarihi"] = Convert.ToDateTime(dt["Tarih"]).ToString();
                            }
                            catch { }

                            try
                            {
                                ds["Servise Giriş Saati"] = Convert.ToDateTime(dt["Tarih"]).ToString("HH:mm");
                            }
                            catch
                            {
                                ds["Servise Giriş Saati"] = "";
                            }
                            ds["İş Emri No"] = dt["isemriNo"].ToString();
                            ds["Para Birimi"] = dt["PAraBirimi"].ToString();

                            using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                            {
                                conp.Open();
                                using (SqlCommand carigetir = new SqlCommand("Select CariUnvan,FirmaKodu From Cari where ID='" + dt["CariID"] + "'", conp))
                                {
                                    using (SqlDataReader dr = carigetir.ExecuteReader())
                                    {
                                        while (dr.Read())
                                        {
                                            ds["Cari Ünvan"] = dr["CariUnvan"].ToString();
                                            ds["Firma Kodu"] = dr["FirmaKodu"].ToString();
                                        }
                                    }
                                }
                            }

                            using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                            {
                                conp.Open();
                                using (SqlCommand carigetir = new SqlCommand("Select CariUnvan,Adres , Case when Telefon1<>'' then Telefon1 else GSm end as Tel  From Cari where ID='" + dt["CariID"] + "'", conp))
                                {
                                    using (SqlDataReader dr = carigetir.ExecuteReader())
                                    {
                                        while (dr.Read())
                                        {
                                            ds["Dış Servis Carisi"] = dr["CariUnvan"].ToString();
                                            ds["Dış Servis Adres"] = dr["Adres"].ToString();
                                            ds["Dış Servis C_Telefon"] = dr["Tel"].ToString();
                                        }
                                    }
                                }
                            }


                            ds["Durum 2"] = dt["Durum2"].ToString();
                            ds["Durum"] = dt["Durum"].ToString();
                            ds["Marka"] = dt["Marka"].ToString();
                            ds["Model"] = dt["Model"].ToString();
                            ds["Cinsi"] = dt["Cinsi"].ToString();
                            ds["Servis Yeri"] = dt["ServisYeri"].ToString();
                            ds["Servis Tipi"] = dt["ServisTipi"].ToString();
                            ds["Onay"] = dt["Onay"].ToString();
                            ds["Garanti"] = dt["Garanti"].ToString();
                            ds["Emanet"] = dt["Emanet"].ToString();
                            ds["Şikayet"] = dt["Sikayet"].ToString();
                            ds["Ön Rapor"] = dt["OnRapor"].ToString();
                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Adi+' '+Soyadi From Personel where ID='" + dt["Personel"] + "'", conp))
                                    {
                                        ds["Cihazı Alan Personel"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }


                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Adi+' '+Soyadi From Personel where ID='" + dt["TekniKsyen"] + "'", conp))
                                    {
                                        ds["Teknisyen"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }



                            ds["Brüt Toplam"] = Convert.ToDecimal(dt["BrutToplam"]).ToString("#,##0.00");
                            ds["Net Toplam"] = Convert.ToDecimal(dt["NetToplam"]).ToString("#,##0.00");
                            ds["Genel Toplam"] = Convert.ToDecimal(dt["GenelToplam"]).ToString("#,##0.00");
                            int balancecari = Convert.ToInt32(cariID);
                            string Balancebirim = dt["ParaBirimi"].ToString();



                            ds["Toplam İskonto"] = Convert.ToDecimal(dt["ToplamIskonto"]).ToString("#,##0.00");
                            ds["Kdv Tutarı"] = Convert.ToDecimal(dt["ToplamKdv"]).ToString("#,##0.00");
                            ds["Arıza Tespit"] = dt["ArizaTespit"].ToString();
                            ds["Onarım Raporu"] = dt["OnarimRapor"].ToString().Replace("İ", "I").Replace("Ğ", "G").Replace("Ş", "S").Replace("ş", "s");
                            ds["Müşteri Yorumu"] = dt["MYorumu"].ToString();
                            ds["Müşteri Puanı"] = dt["MPuani"].ToString();
                            ds["Özel Alan 1"] = dt["oAlan1"].ToString();
                            ds["Özel Alan 2"] = dt["oAlan2"].ToString();
                            ds["Özel Alan 3"] = dt["oAlan3"].ToString();
                            ds["Özel Alan 4"] = dt["oAlan4"].ToString();
                            ds["Özel Alan 5"] = dt["oAlan5"].ToString();
                            ds["Özel Alan 6"] = dt["oAlan6"].ToString();
                            ds["Özel Alan 7"] = dt["oAlan7"].ToString();
                            ds["Özel Alan 8"] = dt["oAlan8"].ToString();


                            if (dt["oAlan3"].ToString() == "")
                            {
                                ds["Özel Alan 3"] = "0";

                            }

                            if (dt["tMaliyet"] != DBNull.Value)
                            {
                                ds["Toplam Maliyet"] = Convert.ToDecimal(dt["tMaliyet"]).ToString("#,##0.00");
                                ds["Toplam Karlılık"] = (Convert.ToDecimal(dt["GenelToplam"]) - Convert.ToDecimal(dt["tMaliyet"])).ToString("#,##0.00");
                            }
                            else
                            {
                                ds["Toplam Maliyet"] = Convert.ToDecimal(0).ToString("#,##0.00");
                                ds["Toplam Karlılık"] = Convert.ToDecimal(0).ToString("#,##0.00");

                            }

                            ds["İşçilik Tutarı"] = 0;
                            ds["İşçilik Süresi"] = 0;

                            using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                            {
                                if (conp.State == ConnectionState.Closed) conp.Open();
                                using (SqlCommand d6 = new SqlCommand("Select coalesce( SUM(Miktar),0) from TECHNICAL_DETAIL where UrunID IN (sELECT ID from Stok where UrunAdi=N'İşçilik'or  UrunAdi=N'İŞÇİLİK') AND CihazID='" + pID + "'", conp))
                                {
                                    ds["İşçilik Süresi"] = Convert.ToDecimal(d6.ExecuteScalar());
                                }
                            }
                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select coalesce( SUM(Tutar),0) from TECHNICAL_DETAIL where UrunID IN (sELECT ID from Stok where UrunAdi=N'İşçilik'or  UrunAdi=N'İŞÇİLİK') AND CihazID='" + pID + "'", conp))
                                    {
                                        ds["İşçilik Tutarı"] = Convert.ToDecimal(d6.ExecuteScalar());
                                    }
                                }
                            }
                            catch { }

                            ds["Km"] = dt["Kilometre"].ToString();
                            ds["ModelYili"] = dt["Yil"].ToString();
                            ds["MotorHacmi"] = dt["MotorHacmi"].ToString();
                            ds["YakitMiktar"] = dt["YakitMiktar"].ToString();
                            ds["Yakıt Tipi"] = dt["YakitTipi"].ToString();
                            ds["Vites"] = dt["Vites"].ToString();
                            ds["SanzimanNo"] = dt["SanzimanNo"].ToString();
                            using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                            {
                                if (conp.State == ConnectionState.Closed) conp.Open();
                                using (SqlCommand carigetir = new SqlCommand("Select Durum,Tarih From TECH_STATE where CihazID='" + pID + "'", conp))
                                {
                                    using (SqlDataReader dr = carigetir.ExecuteReader())
                                    {
                                        while (dr.Read())
                                        {

                                            DataRow ds1 = SADataset1.Cihaz_Durum_Geçmişi.NewRow();

                                            ds1["Durum"] = dr["Durum"].ToString();
                                            ds1["Değişiklik Tarihi"] = dr["Tarih"].ToString();

                                            SADataset1.Cihaz_Durum_Geçmişi.Rows.Add(ds1);

                                        }
                                    }
                                }
                            }

                            try
                            {
                                ds["Garanti Başlangıç Tarihi"] = Convert.ToDateTime(dt["GarantiBaslangicTarihi"]).ToString("dd.MM.yyy");
                            }
                            catch { }

                            try
                            {
                                ds["Garanti Bitiş Tarihi"] = Convert.ToDateTime(dt["GarantiBitisTarihi"]).ToString("dd.MM.yyy");
                            }
                            catch { }
                            ds["Cihaz Adı"] = dt["CihazAdi"].ToString();
                            ds["Cihaz Adresi"] = dt["CihazAdresi"].ToString();

                            try
                            {
                                ds["Cihaz Satış Tarihi"] = Convert.ToDateTime(dt["CihazSatisTarihi"]).ToString("dd.MM.yyy");
                            }
                            catch { }

                            try
                            {
                                ds["Cihaz Montaj Tarihi"] = Convert.ToDateTime(dt["CihazMontajTarihi"]).ToString("dd.MM.yyy");
                            }
                            catch { }

                            SADataset1.Cihaz.Rows.Add(ds);

                            string KDVdh = "Dahil";

                            if (dt["KdvDH"].ToString() != "D") KDVdh = "Hariç";
                            INVOICE_DETAILBilgileri(SADataset1, pID, 1, KDVdh);

                        }
                    }
                }
            }


        }
        [HttpPost]
        public ActionResult DeleteServis(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                TECHNICAL emp = db.TECHNICAL.Where(x => x.ID == id).FirstOrDefault<TECHNICAL>();
                if (emp.Durum == "GİRİŞ YAPILDI")
                {

                    db.TECHNICAL.Remove(emp);
                    db.SaveChanges();
                    using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                    {
                        conp1.Open();
                        using (SqlCommand cu = new SqlCommand("Delete From TECHNICAL_DETAIL where CihazID=" + id + "", conp1))
                        {
                            cu.ExecuteNonQuery().ToString();
                        }
                    }

                    return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Üzerinde İşlem Yapılan Servis Kaydı Silinemedi." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteBakim(int id)
        {

            //using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
            //{
            //    conp1.Open();


            //    using (SqlCommand d6 = new SqlCommand("update TECHNICAL SET  Durum='SERVİSE ALINDI' where ID=" + id, conp1))
            //    {
            //        d6.ExecuteNonQuery();
            //    }
            //}


            using (sayazilimEntities db = new sayazilimEntities())
            {
                TECHNICAL_PERIOD emp = db.TECHNICAL_PERIOD.Where(x => x.ID == id).FirstOrDefault<TECHNICAL_PERIOD>();
                db.TECHNICAL_PERIOD.Remove(emp);

                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult yeniDurum(TECH_STATE tc)
        {
            try
            {

                TECHNICAL eskitc = db.TECHNICAL.Where(x => x.ID == tc.CihazID).FirstOrDefault<TECHNICAL>();
                int personelid = Convert.ToInt32(Session["PersonelID"].ToString());
                Personel pc = db.Personel.Where(x => x.ID == personelid).FirstOrDefault<Personel>();
                if (eskitc.Durum == "TESLİM EDİLDİ")
                {
                    if (pc.PersonelGrubu == "Admin")
                    {
                        TECH_STATE stk = null;
                        string Message = "Kayıt Eklendi";

                        stk = new TECH_STATE();
                        stk = tc;
                        stk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                        string firmaid = Session["FirmaID"].ToString();
                        string company_code = "SA01" + firmaid.PadLeft(3, '0');
                        stk.Company_Code = company_code;
                        db.TECH_STATE.Add(stk);
                        db.SaveChanges();

                        TECHNICAL emp = db.TECHNICAL.Where(x => x.ID == tc.CihazID).FirstOrDefault<TECHNICAL>();
                        emp.Durum = tc.Durum;
                        db.SaveChanges();

                        var result = new { sonuc = 1, Message = Message };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var result = new { sonuc = 0, Message = "İşlemi Gerçekleştirmek için yetkiniz yoktur." };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    TECH_STATE stk = null;
                    string Message = "Kayıt Eklendi";

                    stk = new TECH_STATE();
                    stk = tc;

                    db.TECH_STATE.Add(stk);
                    db.SaveChanges();

                    TECHNICAL emp = db.TECHNICAL.Where(x => x.ID == tc.CihazID).FirstOrDefault<TECHNICAL>();
                    emp.Durum = tc.Durum;
                    db.SaveChanges();

                    var result = new { sonuc = 1, Message = Message };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                var result = new { sonuc = 3, Message = "Bir hata meydana geldi." };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetServis()
        {
            string FirmaID = Session["FirmaID"].ToString();
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<Servis> yonetim = new List<Servis>();
            string sorg = @"Select ServisIslemNo,Serino,BayiID,CihazAdresi,CariID,(Select CariUnvan from Cari where ID=TECHNICAL.CariID) 
  as CariUnvan,(Select Adi+' '+Soyadi from Personel where ID =TECHNICAL.Tekniksyen)as Teknisyen,
  Tarih,Tekniksyen as PersonelID,
  (select Top 1 Aciklama from TECH_STATE ts where ts.CihazID = TECHNICAL.ID ORder by ID DEsc) as Aciklama,
  Marka,Model,GenelToplam,ParaBirimi, Durum,TECHNICAL.ID from TECHNICAL Where FirmaID =" + FirmaID;



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


                            yt.ParaBirimi = dr["ParaBirimi"].ToString();
                            yt.GenelToplam = Convert.ToDecimal(dr["GenelToplam"]).ToString("N2");
                            yt.ServisIslemNo = dr["ServisIslemNo"].ToString();
                            yt.Serino = dr["Serino"].ToString();
                            yt.CariID = dr["CariID"].ToString();
                            yt.CihazAdresi = dr["CihazAdresi"].ToString();
                            try
                            {
                                yt.Tarih = Convert.ToDateTime((dr["Tarih"]));
                                yt.IslemTarihiF = Convert.ToDateTime(dr["Tarih"]).ToString("dd.MM.yyyy");
                            }
                            catch (Exception)
                            {
                                yt.Tarih = DateTime.Now;
                                yt.IslemTarihiF = DateTime.Now.ToString("dd.MM.yyyy");
                            }
                            yt.Marka = dr["Marka"].ToString();
                            yt.Model = dr["Model"].ToString();
                            yt.Durum = dr["Durum"].ToString();
                            yt.Teknisyen = dr["Teknisyen"].ToString();
                            yt.PersonelID = dr["PersonelID"].ToString();
                            if (yt.Teknisyen == "")
                            {
                                try
                                {
                                    int bayiID = Convert.ToInt32(dr["BayiID"].ToString());
                                    Cari bayi = db.Cari.Where(x => x.ID == bayiID).FirstOrDefault<Cari>();
                                    yt.Teknisyen = bayi.CariUnvan;
                                }
                                catch { }
                            }
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.CariUnvan = dr["CariUnvan"].ToString();
                            yt.BayiID= dr["BayiID"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();
                            yonetim.Add(yt);
                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServisBilgi()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<TECH_STATE> yonetim = new List<TECH_STATE>();
            string sorg = @"select * from TECH_STATE Where FirmaID =" + FirmaID;



            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TECH_STATE yt = new TECH_STATE();


                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.Durum = Convert.ToString(dr["Tarih"]);
                            yt.Aciklama = dr["Aciklama"].ToString();

                            //try
                            //{


                            //    yt.FirmaID =1;
                            //}catch { yt.FirmaID = 1; }

                            yonetim.Add(yt);


                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ServisSorgu(string serv)
        {

            using (sayazilimEntities db = new sayazilimEntities())
            {
                TECHNICAL emp = db.TECHNICAL.Where(x => x.Serino == serv).FirstOrDefault<TECHNICAL>();
                if (emp != null)
                {
                    return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, data = emp }, JsonRequestBehavior.AllowGet);
                }

            }
        }

        public ActionResult EkServisBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                TECHNICAL emp = db.TECHNICAL.Where(x => x.ID == id).FirstOrDefault<TECHNICAL>();
                return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ServisKayitlari()
        {
            //ADDbakim(false, "MONTH", DateTime.Now, DateTime.Now.AddMonths(6), DateTime.Now, 2);
            return View();
        }

        public ActionResult yeniServis(int id = 0)
        {
            ViewBag.OzelAlan1 = "1. Özel Alan ";
            ViewBag.OzelAlan2 = "2. Özel Alan ";
            ViewBag.OzelAlan3 = "3. Özel Alan ";
            ViewBag.OzelAlan4 = "4. Özel Alan ";
            ViewBag.OzelAlan5 = "5. Özel Alan ";
            ViewBag.OzelAlan6 = "6. Özel Alan ";
            ViewBag.OzelAlan7 = "7. Özel Alan ";
            ViewBag.OzelAlan8 = "8. Özel Alan ";
            ViewBag.OzelAlan9 = "9. Özel Alan ";


            ViewBag.Kontrol1 = "1.Kontrol";
            ViewBag.Kontrol2 = "2.Kontrol";
            ViewBag.Kontrol3 = "3. Kontrol";
            ViewBag.Kontrol4 = "4.Kontrol";
            ViewBag.Kontrol5 = "5.Kontrol";
            ViewBag.Kontrol6 = "6.Kontrol";
            ViewBag.Kontrol7 = "7.Kontrol";
            try
            {
                TECHNICAL tch = db.TECHNICAL.Where(x => x.ID == id).FirstOrDefault<TECHNICAL>();

                ViewBag.TechnicalDetail = tch.Durum;
            }
            catch
            {
                ViewBag.TechnicalDetail = "YENİ SERVİS KAYDI";
            }

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string sorg = @"select Name from SPECIAL_TECH";
            string sorg2 = @"SELECT Name FROM SERVICE_CONTROLGRUP";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand ozelALanGetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = ozelALanGetir.ExecuteReader())
                    {
                        int i = 1;
                        while (dr.Read())
                        {
                            if (dr["Name"].ToString() != "")
                            {

                                if (i == 1) { ViewBag.OzelAlan1 = dr["Name"].ToString(); }
                                if (i == 2) { ViewBag.OzelAlan2 = dr["Name"].ToString(); }
                                if (i == 3) { ViewBag.OzelAlan3 = dr["Name"].ToString(); }
                                if (i == 4) { ViewBag.OzelAlan4 = dr["Name"].ToString(); }
                                if (i == 5) { ViewBag.OzelAlan5 = dr["Name"].ToString(); }
                                if (i == 6) { ViewBag.OzelAlan6 = dr["Name"].ToString(); }
                                if (i == 7) { ViewBag.OzelAlan7 = dr["Name"].ToString(); }
                                if (i == 8) { ViewBag.OzelAlan8 = dr["Name"].ToString(); }
                                if (i == 9) { ViewBag.OzelAlan9 = dr["Name"].ToString(); }
                            }
                            i++;
                        }
                    }
                }

                using (SqlCommand KontrolALanGetir = new SqlCommand(sorg2, con))
                {
                    using (SqlDataReader dr = KontrolALanGetir.ExecuteReader())
                    {
                        int i = 1;
                        while (dr.Read())
                        {
                            if (dr["Name"].ToString() != "")
                            {

                                if (i == 1) { ViewBag.Kontrol1 = dr["Name"].ToString(); }
                                if (i == 2) { ViewBag.Kontrol2 = dr["Name"].ToString(); }
                                if (i == 3) { ViewBag.Kontrol3 = dr["Name"].ToString(); }
                                if (i == 4) { ViewBag.Kontrol4 = dr["Name"].ToString(); }
                                if (i == 5) { ViewBag.Kontrol5 = dr["Name"].ToString(); }
                                if (i == 6) { ViewBag.Kontrol6 = dr["Name"].ToString(); }
                                if (i == 7) { ViewBag.Kontrol7 = dr["Name"].ToString(); }
                            }
                            i++;
                        }
                    }
                }
            }


            if (id == 0)
            {


                AyarMetot.Siradaki("", "TECHNICAL", "ServisIslemNo", Session["FirmaID"].ToString());
                ViewBag.Siradaki = AyarMetot.GetNumara;

                AyarMetot.Siradaki("", "Cari", "FirmaKodu", Session["FirmaID"].ToString());
                ViewBag.SiradakiFirmaKodu = AyarMetot.GetNumara;

                return View(new TECHNICAL());
            }
            else
            {
                using (sayazilimEntities db = new sayazilimEntities())
                {
                    var servisdetaylari = db.TECHNICAL_DETAIL.Where(x => x.CihazID == id).ToList<TECHNICAL_DETAIL>();
                    ViewBag.Detay = servisdetaylari.ToList();


                    var ser = db.TECHNICAL.Where(x => x.ID == id).FirstOrDefault<TECHNICAL>();
                    try
                    {
                        if (ser.Durum == "Teslim")
                            ViewBag.Siradaki = ser.ServisIslemNo;
                    }
                    catch
                    {

                    }

                    string[] parcalar;



                    List<ServisDetayEk> sr = new List<ServisDetayEk>();
                    for (int i = 0; i < servisdetaylari.Count; i++)
                    {
                        TECHNICAL_DETAIL td = servisdetaylari[i];
                        ServisDetayEk st = new ServisDetayEk();
                        st.UrunAdi = AyarMetot.StokBilgiCek(Convert.ToInt32(td.UrunID), "UrunAdi");

                        sr.Add(st);

                    }

                    ViewBag.servisEk = sr;
                    ViewBag.GarantiBaslangicTarihi = Convert.ToDateTime(ser.GarantiBaslangicTarihi).ToString("yyyy-MM-dd");
                    ViewBag.GarantiBitisTarihi = Convert.ToDateTime(ser.GarantiBitisTarihi).ToString("yyyy-MM-dd");
                    //var srv = db.ServisBelgeler.Where(x => x.ID == id).FirstOrDefault<ServisBelgeler>();
                    //ViewBag.BelgeAdi = srv.BelgeAdi;


                    //var cari = db.Cari.Where(x => x.ID == id).FirstOrDefault<Cari>();
                    //ViewBag.Yetkili = cari.Yetkili;
                    //ViewBag.Telefon = cari.GSM;
                    //ViewBag.EPosta = cari.EPosta;
                    //ViewBag.Adres = cari.Adres;

                    ViewBag.Yok = "";
                    ViewBag.Haftalik = "";
                    ViewBag.Aylik = "";
                    ViewBag.Yillik = "";
                    ViewBag.Sonraki = "";
                    ViewBag.BelirliAralik = "";


                    if (ser.BakimPeriyodu == "Yok")
                    {
                        ViewBag.Yok = "checked";
                    }
                    if (ser.BakimPeriyodu == "Haftalik")
                    {
                        ViewBag.Haftalik = "checked";
                    }
                    if (ser.BakimPeriyodu == "Aylik")
                    {
                        ViewBag.Aylik = "checked";
                    }
                    if (ser.BakimPeriyodu == "Yillik")
                    {
                        ViewBag.Yillik = "checked";
                    }
                    if (ser.BakimPeriyodu == "Sonraki")
                    {
                        ViewBag.Sonraki = "checked";
                    }
                    if (ser.BakimPeriyodu == "Belirli")
                    {
                        ViewBag.BelirliAralik = "checked";
                    }
                    if (ser.Durum != "TESLİM EDİLDİ")
                    {
                        return View(ser);
                    }
                    else
                    {
                        return RedirectToAction("ServisKayitlari", "Servis");
                    }
                }



                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand carigetir = new SqlCommand(@"select * FROM ServisBelgeler where ServisID='" + id + "'", con))
                    {
                        using (SqlDataReader dr = carigetir.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ViewBag.cmr = dr["Cmr"].ToString();
                                ViewBag.t1 = dr["T1"].ToString();
                                ViewBag.t2 = dr["T2"].ToString();
                                ViewBag.ps = dr["Pasaport"].ToString();
                                ViewBag.ft = dr["Fatura"].ToString();
                                ViewBag.ts = dr["TasimaSozlesmesi"].ToString();
                                ViewBag.dg1 = dr["Diger1"].ToString();
                                ViewBag.dg2 = dr["Diger2"].ToString();


                            }
                        }

                    }
                }

            }

        }

        [HttpPost]
        public JsonResult DeleteYB(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                TECHNICAL_DETAIL emp = db.TECHNICAL_DETAIL.Where(x => x.ID == id).FirstOrDefault<TECHNICAL_DETAIL>();
                db.TECHNICAL_DETAIL.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult yeniServis(TECHNICAL srv, Array[] data)
        {

            TECHNICAL ser = new TECHNICAL();
            //TECH_STATE ser2 = new TECH_STATE();
            string Message = "Kayıt Eklendi";


            var result = new { sonuc = 0, Message = "" };
            bool kayitAdi = false;
            ViewBag.kayitAdi = kayitAdi;

            if (srv.CariID == "-1" || srv.CariID == "0" || srv.CariID == null)
            {
                result = new { sonuc = 0, Message = "Cari Seçiniz" };
                return Json(result, JsonRequestBehavior.AllowGet);


            }
            else
            {
                int ServisID = -1;
                string Mik = "";
                string Fiyat = "";
                decimal Kdv = 18;
                int UrunID = -1;
                string Birim = "Adet";
                string Iskonta = "";
                string SeriNo = "";
                string GarantiSuresi = "";
                int firmaidsrg = Convert.ToInt32(Session["FirmaID"].ToString());
                int ID = -1;
                var list = db.TECHNICAL.Where(x => x.ServisIslemNo == srv.ServisIslemNo && x.FirmaID == firmaidsrg).ToList();

                var crler = db.Cari.Where(x => x.SeriNo == srv.Serino).ToList();
                Cari crler2 = db.Cari.Where(x => x.SeriNo == srv.Serino).FirstOrDefault();

                int srvcarid = Convert.ToInt32(srv.CariID);
                bool kayit = false;


                if (crler.Count == 0)
                {
                    kayit = true;
                }
                else if (crler2.ID == srvcarid)
                {
                    kayit = true;
                }

                if (srv.ID == -1 || srv.ID == 0)
                {
                    if (kayit)
                    {
                        if (list.Count == 0)
                        {

                            ser = srv;

                            if (srv.knt1.ToLower() == "true" || srv.knt1.ToLower() == "1")
                            {
                                ser.knt1 = "Evet";
                            }
                            else
                            {
                                ser.knt1 = "Hayır";
                            }

                            if (srv.knt2.ToLower() == "true" || srv.knt2.ToLower() == "1")
                            {
                                ser.knt2 = "Evet";
                            }
                            else
                            {
                                ser.knt2 = "Hayır";
                            }

                            if (srv.knt3.ToLower() == "true" || srv.knt3.ToLower() == "1")
                            {
                                ser.knt3 = "Evet";
                            }
                            else
                            {
                                ser.knt3 = "Hayır";
                            }

                            if (srv.knt4.ToLower() == "true" || srv.knt4.ToLower() == "1")
                            {
                                ser.knt4 = "Evet";
                            }
                            else
                            {
                                ser.knt4 = "Hayır";
                            }

                            if (srv.knt5.ToLower() == "true" || srv.knt5.ToLower() == "1")
                            {
                                ser.knt5 = "Evet";
                            }
                            else
                            {
                                ser.knt5 = "Hayır";
                            }

                            if (srv.knt6.ToLower() == "true" || srv.knt6.ToLower() == "1")
                            {
                                ser.knt6 = "Evet";
                            }
                            else
                            {
                                ser.knt6 = "Hayır";
                            }




                            ser.BrutToplam = 0;
                            //burada varsayılan değerleri atayacağız
                            ser.ServisID = -1;

                            int gelencariID = Convert.ToInt32(srv.CariID);

                            var paralist2 = db.Cari.Where(x => x.ID == gelencariID).ToList();
                            foreach (var item in paralist2)
                            {
                                ser.ParaBirimi = item.paraBirimi;
                            }


                            ser.TopHamMaliyet = 0;
                            ser.OZelKodID = -1;
                            ser.MPuani = 0;
                            ser.Faturatipi = "SRV";
                            ser.SonDurumTarih = Convert.ToString(DateTime.Now);
                            ser.q18Kdv = 0;
                            ser.kdvDH = "D";
                            ser.q8Kdv = 0;
                            ser.q1Kdv = 0;
                            ser.gIskonta = 0;
                            ser.TahminiOnarimTarih = DateTime.Now;
                            ser.ServisOzelKod = -1;
                            ser.KayitPersonelID = 1;
                            ser.Kur = 1;
                            ser.DolarKur = 1;
                            ser.EuroKur = 1;
                            ser.OnarimTarih = DateTime.Now;
                            ser.ServisOdemesi = 0;

                            ser.BakimPeriyodu = "Yok";
                            ser.GbpKur = 1;
                            ser.ChfKur = 1;
                            ser.YetkiliID = -1;
                            ser.DServisCID = -1;
                            ser.SonrakiBakimTarihi = DateTime.Now;

                            ser.DenemeServis = false;
                            ser.MKDkur = 1;
                            ser.jpykur = 1;
                            ser.MotorHacmi = 0;
                            ser.Kilometre = 0;
                            ser.Durum = "GİRİŞ YAPILDI";
                            DateTime dt = Convert.ToDateTime(srv.Tarih);

                            ser.Tarih = dt.ToString("dd") + "." + dt.ToString("MM") + "." + dt.ToString("yyyy") + " " +
                                        dt.ToString("HH") + ":" + dt.ToString("mm") + ":" + dt.ToString("ss");
                            ser.NetToplam = 0;
                            ser.GenelToplam = 0;
                            ser.tMaliyet = 0;
                            ser.ToplamIskonto = 0;
                            ser.ToplamKdv = 0;
                            ser.CihazAdi = srv.CihazAdi;
                            ser.CihazAdresi = srv.CihazAdresi;
                            ser.BakimBaslangicTarihi = Convert.ToDateTime(srv.Tarih);
                            ser.BakimBitisTarihi = Convert.ToDateTime(srv.Tarih);
                            ser.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                            string firmaid = Session["FirmaID"].ToString();
                            string company_code = "SA01" + firmaid.PadLeft(3, '0');
                            ser.Company_Code = company_code;
                            try
                            {
                                ser.FirmaID = (short)Convert.ToInt32(Session["FirmaID"].ToString());
                            }
                            catch (Exception e)
                            {
                                ser.FirmaID = 1;
                            }

                            int CariID = Convert.ToInt32(srv.CariID);

                            Cari cr = db.Cari.Where(x => x.ID == CariID).FirstOrDefault<Cari>();

                            cr.SeriNo = srv.Serino;
                            db.TECHNICAL.Add(ser);

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception e)
                            {

                            }

                            using (SqlConnection conp1 = new SqlConnection(AyarMetot.conString))
                            {
                                if (conp1.State == ConnectionState.Closed) conp1.Open();
                                using (SqlCommand sID =
                                    new SqlCommand(@"select top (1) ID FROM TECHNICAL  Order BY ID Desc", conp1))
                                {
                                    ServisID = Convert.ToInt32(sID.ExecuteScalar());
                                }
                            }


                            using (SqlConnection conp1 = new SqlConnection(AyarMetot.conString))
                            {
                                if (conp1.State == ConnectionState.Closed) conp1.Open();


                                using (SqlCommand sID = new SqlCommand(
                                    @" set dateformat dmy ; update TECHNICAL_PERIOD set Durumu='Servise Alındı' where ServisID='" +
                                    srv.ID + "' AND " +
                                    "CONVERT(nvarchar,Tarih,104)=CONVERT(nvarchar,'" + ser.Tarih +
                                    "',104) and Durumu='Beklemede'", conp1))
                                {
                                    sID.ExecuteNonQuery();

                                }
                            }
                        }

                        else
                        {
                            result = new { sonuc = 0, Message = "Aynı Servis İşlem Numarasına Ait Kayıt Zaten Mevcut." };
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else 
                    {
                        result = new { sonuc = 0, Message = "Bu Seri Numarasına Ait Cari Kaydı Bulunmaktadır." };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {


                    var list2 = db.TECHNICAL.Where(x => x.ServisIslemNo == srv.ServisIslemNo & x.ID != srv.ID).ToList();

                    if (list2.Count == 0)
                    {

                        ser = db.TECHNICAL.Where(x => x.ID == srv.ID).FirstOrDefault<TECHNICAL>();

                        ser.CihazAdi = srv.CihazAdi;
                        ser.CihazAdresi = srv.CihazAdresi;
                        ser.ServisIslemNo = srv.ServisIslemNo;
                        ser.Serino = srv.Serino;
                        DateTime dt = Convert.ToDateTime(srv.Tarih);

                        ser.Tarih = dt.ToString("dd") + "." + dt.ToString("MM") + "." + dt.ToString("yyyy") + " " +
                            dt.ToString("HH") + ":" + dt.ToString("mm") + ":" + dt.ToString("ss");

                        ser.Marka = srv.Marka;
                        ser.Model = srv.Model;
                        ser.Cinsi = srv.Cinsi;
                        ser.Sikayet = srv.Sikayet;
                        ser.OnRapor = srv.OnRapor;
                        ser.OnarimRapor = srv.OnarimRapor;
                        ser.ArizaTespit = srv.ArizaTespit;
                        int gelencariID3 = Convert.ToInt32(srv.CariID);
                        var paralist = db.Cari.Where(x => x.ID == gelencariID3).ToList();
                        foreach (var item in paralist)
                        {
                            ser.ParaBirimi = item.paraBirimi;
                        }





                        //System.IO.File.WriteAllText(Path.Combine(@"C:\Yedekler\", "sonuç.xml"), ser.OnarimRapor.ToString());

                        ser.Personel = srv.Personel;
                        ser.Tekniksyen = srv.Tekniksyen;
                        ser.ServisSekli = srv.ServisSekli;

                        ser.GarantiTipi = srv.GarantiTipi;
                        ser.BakimMetni = srv.BakimMetni;
                        ser.CihazGarantisi = srv.CihazGarantisi;

                        ser.oAlan1 = srv.oAlan1;
                        ser.oAlan2 = srv.oAlan2;
                        ser.oAlan3 = srv.oAlan3;
                        ser.oAlan4 = srv.oAlan4;
                        ser.oAlan5 = srv.oAlan5;
                        ser.oAlan6 = srv.oAlan6;
                        ser.oAlan7 = srv.oAlan7;
                        ser.oAlan8 = srv.oAlan8;
                        ser.oAlan9 = srv.oAlan9;
                        try
                        {
                            ser.BayiID = Convert.ToInt32(srv.BayiID);
                        }
                        catch
                        {
                            ser.BayiID = -1;
                        }
                        ser.BakimBitisTarihi = srv.BakimBitisTarihi;
                        ser.CariID = srv.CariID;

                        ser.BakimPeriyodu = srv.BakimPeriyodu;


                        string cek = "WEEK";
                        if (ser.BakimPeriyodu == "Aylik")
                        {
                            cek = "MONTH";
                        }
                        else if (ser.BakimPeriyodu == "Yillik")
                        {
                            cek = "YEAR";
                        }
                        else if (ser.BakimPeriyodu == "Belirli")
                        {
                            cek = "Belirli";
                        }


                        ADDbakim(false, cek, Convert.ToDateTime(ser.BakimBaslangicTarihi),
                            Convert.ToDateTime(ser.BakimBitisTarihi), ser.ID);

                        Message = "Kayıt Güncellendi";
                        kayitAdi = true;
                        ViewBag.kayitAdi = kayitAdi;

                        ServisID = srv.ID;

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {

                                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"),
                                        ve.PropertyName + ";" + ve.ErrorMessage.ToString());



                                }
                            }
                            throw;
                        }



                    }
                    else
                    {
                        result = new { sonuc = 0, Message = "Aynı Servis İşlem Numarasına Ait Kayıt Zaten Mevcut." };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }






                int kolon = 0;
                if (data != null)
                {
                    for (int i = 0; i < data.Length; i++)
                    {

                        foreach (var veri in data[i])
                        {
                            if (kolon == 0)
                            {
                                ID = Convert.ToInt32(veri);
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
                                Fiyat = veri.ToString();
                            }
                            else if (kolon == 4)
                            {
                                Kdv = Convert.ToDecimal(veri);
                            }
                            else if (kolon == 5)
                            {
                                UrunID = Convert.ToInt32(veri);
                            }
                            else if (kolon == 6)
                            {
                                Iskonta = veri.ToString();
                            }
                            else if (kolon == 7)
                            {
                                SeriNo = veri.ToString();
                            }
                            else if (kolon == 8)
                            {
                                GarantiSuresi = veri.ToString();
                            }

                            kolon++;
                        }

                        kolon = 0;

                        try
                        {
                            System.IO.File.WriteAllText(
                                Path.Combine(@"C:\Users\ilhan\AppData\Local\Sayazilim", "ilhan.xml"),
                                Fiyat.ToString());
                        }
                        catch (Exception e)
                        {
                        }


                        decimal Miktar = Convert.ToDecimal(Mik.Replace(".", ","));
                        decimal Isk = 0;
                        try
                        {
                            Isk = Convert.ToDecimal(Iskonta.Replace(".", ","));
                        }
                        catch { }

                        decimal Fiy = Convert.ToDecimal(Fiyat.Replace(".", ","));


                        string KdvDh = "H";



                        try
                        {

                            decimal FMiktar = Fiy * Miktar;

                            FMiktar = FMiktar - (FMiktar * Isk / 100);


                            if (ID == -1)
                            {

                                using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                                {
                                    if (con.State == ConnectionState.Closed) con.Open();
                                    using (SqlDataAdapter da =
                                        new System.Data.SqlClient.SqlDataAdapter(
                                            "select Top 1 * from TECHNICAL_DETAIL", con))
                                    {
                                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                        {
                                            DataSet ds = new DataSet();
                                            da.Fill(ds, "TECHNICAL_DETAIL");
                                            DataRow df = ds.Tables["TECHNICAL_DETAIL"].NewRow();

                                            df["Durumu"] = "Aktif";
                                            df["CihazID"] = ServisID;
                                            df["IslemTarihi"] = DateTime.Now;
                                            df["IslemTuru"] = "Servis";
                                            df["UrunID"] = UrunID;
                                            df["SeriNo"] = SeriNo;
                                            df["depoID"] = 1;
                                            df["PersonelID"] = 1;
                                            df["Birim"] = "Adet";
                                            df["Miktar"] = Miktar;
                                            df["gBirimMiktar"] = Miktar;
                                            df["Fiyat"] = Fiy;
                                            df["AdetFiyati"] = Fiy;
                                            df["FirmaID"] = Convert.ToInt32(Session["FirmaID"].ToString());
                                            df["Company_Code"] = Session["Company_Code"].ToString();



                                            if (KdvDh == "D")
                                            {

                                                df["actTutar"] = FMiktar;
                                                df["Tutar"] = (FMiktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                            }
                                            else
                                            {

                                                df["Tutar"] = FMiktar;
                                                df["actTutar"] = (FMiktar) + ((FMiktar) * Kdv / 100);

                                            }


                                            df["pBirimi"] = "TL";

                                            df["VadeTarih"] = DateTime.Now;

                                            df["IadeSuresi"] = DateTime.Now;


                                            df["KDV"] = Kdv;
                                            df["Tevfikat"] = 0;
                                            df["Variyant"] = "";
                                            df["VariyantSel"] = -1;
                                            df["Iskonta"] = Isk;
                                            df["Iskonta2"] = 0;
                                            df["Iskonta3"] = 0;
                                            df["Iskonta4"] = 0;
                                            df["Iskonta5"] = 0;

                                            df["Kur"] = 1;


                                            df["Maliyet"] = 0;
                                            df["GarantiSuresi"] = GarantiSuresi;
                                            df["Donem"] = DateTime.Now.Year;

                                            df["EkAciklama"] = "";
                                            df["Fparabirimi"] = "TL";
                                            df["SatirOzelKodID"] = -1;
                                            df["BirimAdet"] = 1;

                                            df["OIV"] = 0;
                                            df["OTV"] = 0;
                                            df["Tevfikat"] = 0;

                                            string firmaid = Session["FirmaID"].ToString();
                                            string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                            df["Company_Code"] = company_code;

                                            ds.Tables["TECHNICAL_DETAIL"].Rows.Add(df);
                                            da.Update(ds, "TECHNICAL_DETAIL");
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
                                            "select * from TECHNICAL_DETAIL where ID='" + ID + "'", con))
                                    {
                                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                        {
                                            DataSet ds = new DataSet();
                                            da.Fill(ds, "TECHNICAL_DETAIL");
                                            DataRow[] adf = ds.Tables["TECHNICAL_DETAIL"].Select("ID='" + ID + "'");
                                            if (adf.Length != 0)
                                            {


                                                DataRow df = adf[0];
                                                df["Durumu"] = "Aktif";
                                                df["IslemTarihi"] = DateTime.Now;
                                                df["IslemTuru"] = "Servis";

                                                df["SeriNo"] = SeriNo;
                                                df["depoID"] = 1;
                                                df["PersonelID"] = 1;
                                                df["Birim"] = "Adet";
                                                df["Miktar"] = Miktar;
                                                df["gBirimMiktar"] = Miktar;
                                                df["Fiyat"] = Fiy;
                                                df["AdetFiyati"] = Fiy;



                                                if (KdvDh == "D")
                                                {

                                                    df["actTutar"] = FMiktar;
                                                    df["Tutar"] =
                                                        (FMiktar) / (Convert.ToDecimal((100 + Kdv) / 100));
                                                }
                                                else
                                                {

                                                    df["Tutar"] = FMiktar;
                                                    df["actTutar"] = (FMiktar) + ((FMiktar) * Kdv / 100);

                                                }

                                                df["pBirimi"] = "TL";

                                                df["VadeTarih"] = DateTime.Now;

                                                df["IadeSuresi"] = DateTime.Now;


                                                df["KDV"] = Kdv;
                                                df["Tevfikat"] = 0;
                                                df["Variyant"] = "";
                                                df["VariyantSel"] = -1;
                                                df["Iskonta"] = Isk;
                                                df["Iskonta2"] = 0;
                                                df["Iskonta3"] = 0;
                                                df["Iskonta4"] = 0;
                                                df["Iskonta5"] = 0;

                                                df["Kur"] = 1;


                                                df["Maliyet"] = 0;
                                                try
                                                {
                                                    df["GarantiSuresi"] = Convert.ToInt32(GarantiSuresi);

                                                }
                                                catch (Exception e)
                                                {
                                                    df["GarantiSuresi"] = 0;
                                                }

                                                df["Donem"] = DateTime.Now.Year;

                                                df["EkAciklama"] = "";
                                                df["Fparabirimi"] = "TL";
                                                df["SatirOzelKodID"] = -1;
                                                df["BirimAdet"] = 1;

                                                df["OIV"] = 0;
                                                df["OTV"] = 0;
                                                df["Tevfikat"] = 0;


                                                da.Update(ds, "TECHNICAL_DETAIL");
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
                                System.IO.File.WriteAllText(
                                    Path.Combine(@"C:\Users\ilhan\AppData\Local\Sayazilim", "sonuç.xml"),
                                    E1.ToString());
                            }
                            catch
                            {
                            }
                        }



                    }
                }



                try
                {
                    using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                    {
                        conp1.Open();



                        using (SqlCommand d6 = new SqlCommand(@"  update TECHNICAL SET 
 netToplam=(Select coalesce( SUM((Miktar*Fiyat)-(Miktar*Fiyat*Iskonta/100)),0) from TECHNICAL_DETAIL WHERE CihazID=TECHNICAL.ID)
,GenelToplam=(Select coalesce( SUM(((Miktar*Fiyat)-(Miktar*Fiyat*Iskonta/100))+((Miktar*Fiyat)-(Miktar*Fiyat*Iskonta/100))*(Convert(decimal(18,2),kdv)/100)),0)from TECHNICAL_DETAIL WHERE CihazID=TECHNICAL.ID) ,
  ToplamKdv=(Select coalesce( SUM(((Miktar*Fiyat)-(Miktar*Fiyat*Iskonta/100))*(Convert(decimal(18,2),kdv)/100)),0) 
  from TECHNICAL_DETAIL WHERE CihazID=TECHNICAL.ID)


 where TECHNICAL.ID=" + ServisID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }



                        using (SqlCommand d6 =
                            new SqlCommand("update TECHNICAL SET brutToplam=netToplam where ID=" + ServisID, conp1))
                        {
                            d6.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {
                }


                result = new { sonuc = 1, Message = Message };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult ServisBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                TECHNICAL emp = db.TECHNICAL.Where(x => x.ID == id).FirstOrDefault<TECHNICAL>();
                if (id == -1)
                {
                    return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, data = emp }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult CariBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {

                //if (id == -1)
                //{
                Cari emp = db.Cari.Where(x => x.ID == id).FirstOrDefault<Cari>();
                try
                {
                    ViewBag.Yetkili = emp.Yetkili;
                    ViewBag.Telefon = emp.GSM;
                    if (emp.GSM == null)
                    {
                        ViewBag.Telefon = emp.Telefon1;
                    }

                    ViewBag.EPosta = emp.EPosta;
                    ViewBag.Adres = emp.Adres;
                }
                catch
                { }
                return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);

                //}
                //else
                //{
                //    var cari = db.Cari.Where(x => x.ID == id).FirstOrDefault<Cari>();
                //    ViewBag.Yetkili = cari.Yetkili;
                //    ViewBag.Telefon = cari.GSM;
                //    ViewBag.EPosta = cari.EPosta;
                //    ViewBag.Adres = cari.Adres;
                //    return Json(new { success = true, data = cari }, JsonRequestBehavior.AllowGet);
                //}


            }
        }
        [HttpGet]
        public ActionResult GetServisDurum(int id)
        {
            List<TECH_STATE> yonetim = new List<TECH_STATE>();


            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;


            string sorg = @"select TECH_STATE.Durum,Aciklama,TECH_STATE.Tarih from TECH_STATE Inner Join TECHNICAL On TECH_STATE.CihazID= TECHNICAL.ID where TECH_STATE.CihazID='" + id + "'";



            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            TECH_STATE yt = new TECH_STATE();

                            yt.Durum = dr["Durum"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();
                            yt.Tarih = Convert.ToDateTime(dr["Tarih"]);

                            yonetim.Add(yt);


                        }

                    }
                }
            }



            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public ActionResult GetBakimPeriyodu(int id)
        //{
        //    List<TECHNICAL> yonetim = new List<TECHNICAL>();


        //    string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;


        //    string sorg = @"select * from TECHNICAL_PERIOD";



        //    using (SqlConnection con = new SqlConnection(strcon))
        //    {
        //        con.Open();
        //        using (SqlCommand servisgetir = new SqlCommand(sorg, con))
        //        {
        //            using (SqlDataReader dr = servisgetir.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {


        //                    TECHNICAL yt = new TECHNICAL();

        //                    yt.Durum = dr["Durum"].ToString();
        //                    yt.Aciklama = dr["Aciklama"].ToString();
        //                    yt.Tarih = Convert.ToDateTime(dr["Tarih"]);

        //                    yonetim.Add(yt);


        //                }

        //            }
        //        }
        //    }



        //    return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        //}



        //ADDbakim(false, "MONTH", DateTime.Now, DateTime.Now.AddMonths(6), DateTime.Now, 2);

        private void ADDbakim(bool yedek, string cek, DateTime bs, DateTime bt, int ServisID)
        {
            bool sadecesonraki = false;
            DateTime sonraki = DateTime.Now;
            bool bakimvar = true;
            string OncekiPeriyod = cek;
            if (yedek)

            {

                try
                {
                    using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                    {
                        if (conp.State == ConnectionState.Closed) conp.Open();
                        using (SqlCommand cu = new SqlCommand("TRUNCATE TABLE TECHNICAL_YEDEK_PERIOD", conp))
                        {
                            cu.ExecuteNonQuery();
                        }
                    }
                }
                catch { }

            }

            int yeni = 0;
            int belirli = 0;

            try
            {
                System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Hatice\AppData\Local\Sayazilim", "cek.xml"), cek.ToString());
            }
            catch
            { }

            // cek = "WEEK";
            //if (checkBox14.Checked)
            //{
            //    cek = "MONTH";
            //}
            //else if (checkBox15.Checked)
            //{
            //    cek = "YEAR";
            //}
            //else if (checkBox29.Checked)
            //{
            //    cek = "Belirli";
            //}

            if (cek != "Belirli")
            {
                try
                {
                    using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                    {
                        if (conp.State == ConnectionState.Closed) conp.Open();
                        using (SqlCommand d6 = new SqlCommand("set dateformat dmy;select DATEDIFF(" + cek + ",convert(datetime, '" + DateTime.Now.ToString("dd.MM.yyyy") + " 00:00:00.000" + "'),convert(datetime, '" + bt.ToString("dd.MM.yyyy") + " 00:00:00.000" + "'))", conp))
                        {
                            yeni = Convert.ToInt32(d6.ExecuteScalar());
                        }
                    }
                }
                catch { }




            }
            else
            {
                TimeSpan GunFarki = bt.AddDays(1).Subtract(Convert.ToDateTime(bs.ToString("dd.MM.yyyy") + " 00:00:00.000"));
                if (belirli != 0)
                    yeni = Convert.ToInt32(Convert.ToInt32(GunFarki.TotalDays) / belirli);
                else
                {
                    yeni = Convert.ToInt32(Convert.ToInt32(GunFarki.TotalDays));

                }
            }
            DateTime ilktarih = Convert.ToDateTime(bs.ToString("dd.MM.yyyy") + " 00:00:00.000");

            if (sadecesonraki)
            {
                ilktarih = Convert.ToDateTime(sonraki.ToString("dd.MM.yyyy") + " 00:00:00.000");
            }

            if (bakimvar)
            {
                if (sadecesonraki || sonraki.ToString("ddMMyyyy") != sonraki.ToString("ddMMyyyy"))
                {

                    int say = 0;

                    using (SqlConnection con1 = new SqlConnection(AyarMetot.strcon))
                    {
                        if (con1.State == ConnectionState.Closed) con1.Open();
                        using (SqlCommand cusername = new SqlCommand("set dateformat dmy; SELECT Count(ID) FROM TECHNICAL_PERIOD where ServisID='" + ServisID + "' and  (CONVERT(datetime, BakimTarihi) BETWEEN convert(datetime, '" + ilktarih.ToString("dd.MM.yyyy") + " 00:00:00.000" + "') AND convert(datetime, '" + ilktarih.ToString("dd.MM.yyyy") + " 23:59:59.000" + "')) ", con1))
                        {
                            if (cusername.ExecuteScalar() != DBNull.Value)
                            {

                                say = Convert.ToInt32(cusername.ExecuteScalar());

                            }
                        }
                    }

                    if (say < 1)
                    {
                        if (ilktarih.ToString("ddMMyyyy") != DateTime.Now.ToString("ddMMyyyy"))
                        {
                            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                            {
                                if (con.State == ConnectionState.Closed) con.Open();

                                int srv = ServisID;

                                if (ServisID == -1)
                                {
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM TECHNICAL", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) srv = Convert.ToInt32(cu.ExecuteScalar());
                                    }
                                }
                                if (yedek == false)
                                {
                                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select top 1 * from TECHNICAL_PERIOD", con))
                                    {
                                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                        {
                                            DataSet dssFatura = new DataSet();
                                            da.Fill(dssFatura, "TECHNICAL_PERIOD");

                                            DataRow df = dssFatura.Tables["TECHNICAL_PERIOD"].NewRow();

                                            df["Durumu"] = "Beklemede";
                                            df["Tarih"] = ilktarih;
                                            df["BakimTarihi"] = ilktarih;
                                            df["Aciklama"] = "";
                                            df["ServisID"] = srv;

                                            dssFatura.Tables["TECHNICAL_PERIOD"].Rows.Add(df);
                                            da.Update(dssFatura, "TECHNICAL_PERIOD");
                                        }
                                    }
                                }
                                else
                                {

                                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select top 1 * from TECHNICAL_YEDEK_PERIOD", con))
                                    {
                                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                        {
                                            DataSet dssFatura = new DataSet();
                                            da.Fill(dssFatura, "TECHNICAL_YEDEK_PERIOD");

                                            DataRow df = dssFatura.Tables["TECHNICAL_YEDEK_PERIOD"].NewRow();

                                            df["Durumu"] = "Beklemede";
                                            df["Tarih"] = ilktarih;
                                            df["BakimTarihi"] = ilktarih;
                                            df["Aciklama"] = "";
                                            df["ServisID"] = srv;

                                            dssFatura.Tables["TECHNICAL_YEDEK_PERIOD"].Rows.Add(df);
                                            da.Update(dssFatura, "TECHNICAL_YEDEK_PERIOD");
                                        }
                                    }

                                }
                            }
                        }

                    }

                }
                else
                {
                    try
                    {
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Hatice\AppData\Local\Sayazilim", "ilhan.xml"), yeni.ToString());
                    }
                    catch
                    { }

                    string per = "";
                    if (cek == "WEEK")
                    {
                        per = "Haftalik";
                    }
                    else if (cek == "MONTH")
                    {
                        per = "Aylik";
                    }
                    else if (cek == "YEAR")
                    {
                        per = "Yillik";
                    }
                    else if (cek == "WEEK")
                    {
                        per = "Belirli";
                    }
                    else if (sadecesonraki || sonraki.ToString("ddMMyyyy") != sonraki.ToString("ddMMyyyy"))
                    {
                        per = "SadeceSonBakim";
                    }
                    else
                    {
                        per = "Yok";
                    }
                    int srv = ServisID;

                    if (ServisID == -1)
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();

                            using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM TECHNICAL", con))
                            {
                                if (cu.ExecuteScalar() != DBNull.Value) srv = Convert.ToInt32(cu.ExecuteScalar());
                            }
                        }
                    }
                    if (OncekiPeriyod != per)
                    {
                        try
                        {
                            using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                            {
                                if (conp.State == ConnectionState.Closed) conp.Open();
                                using (SqlCommand cu = new SqlCommand("DELETE TECHNICAL_PERIOD WHERE ServisID='" + srv + "'", conp))
                                {
                                    cu.ExecuteNonQuery();
                                }
                            }
                        }
                        catch { }

                    }



                    for (int i = 0; i < yeni; i++)
                    {
                        if (ilktarih.ToString("ddMMyyyy") != DateTime.Now.ToString("ddMMyyyy"))
                        {
                            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                            {
                                if (con.State == ConnectionState.Closed) con.Open();


                                if (yedek == false)
                                {

                                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select top 1 * from TECHNICAL_PERIOD", con))
                                    {
                                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                        {
                                            DataSet dssFatura = new DataSet();
                                            da.Fill(dssFatura, "TECHNICAL_PERIOD");

                                            DataRow df = dssFatura.Tables["TECHNICAL_PERIOD"].NewRow();

                                            df["Durumu"] = "Beklemede";
                                            df["Tarih"] = ilktarih;
                                            df["BakimTarihi"] = ilktarih;
                                            df["Aciklama"] = "";
                                            df["ServisID"] = srv;

                                            dssFatura.Tables["TECHNICAL_PERIOD"].Rows.Add(df);
                                            da.Update(dssFatura, "TECHNICAL_PERIOD");
                                        }
                                    }
                                }
                                else
                                {

                                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select top 1 * from TECHNICAL_YEDEK_PERIOD", con))
                                    {
                                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                        {
                                            DataSet dssFatura = new DataSet();
                                            da.Fill(dssFatura, "TECHNICAL_YEDEK_PERIOD");

                                            DataRow df = dssFatura.Tables["TECHNICAL_YEDEK_PERIOD"].NewRow();

                                            df["Durumu"] = "Beklemede";
                                            df["Tarih"] = ilktarih;
                                            df["BakimTarihi"] = ilktarih;
                                            df["Aciklama"] = "";
                                            df["ServisID"] = srv;

                                            dssFatura.Tables["TECHNICAL_YEDEK_PERIOD"].Rows.Add(df);
                                            da.Update(dssFatura, "TECHNICAL_YEDEK_PERIOD");
                                        }
                                    }

                                }
                            }
                        }
                        if (cek == "WEEK") ilktarih = ilktarih.AddDays(7);
                        else if (cek == "MONTH") ilktarih = ilktarih.AddMonths(1);
                        else if (cek == "YEAR") ilktarih = ilktarih.AddYears(1);
                        else if (cek == "Belirli") ilktarih = ilktarih.AddDays(Convert.ToInt32(belirli));
                    }
                }
            }
            else
            {
                //    if (yedek)
                //        dataGridView4.DataSource = null;
            }
            //if (yedek) BakimYukle(true);
        }
        public ActionResult StokBilgiYeni(int? id = -1, string Brk = "")
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                bool kodbulundu = true;
                Stok emp = db.Stok.Where(x => x.ID == id).FirstOrDefault<Stok>();
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
                }
                return Json(new { success = true, data = emp, kodbulundu = kodbulundu }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDurum()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<TECHNICAL> yonetim = new List<TECHNICAL>();
            string sorg = @"select COUNT(ID) as oALan10,Durum,Tarih from TECHNICAL where FirmaID= " + FirmaID + " group by Durum,Tarih";

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
                            yt.Tarih = dr["Tarih"].ToString();

                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetServisTuru()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<TECHNICAL> yonetim = new List<TECHNICAL>();
            string sorg = @"select COUNT(ID) as oALan9,ServisSekli from TECHNICAL where FirmaID=" + FirmaID + " group by ServisSekli";

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

                            yt.ServisSekli = dr["ServisSekli"].ToString();
                            yt.oAlan9 = dr["oAlan9"].ToString();

                            yonetim.Add(yt);

                        }
                    }
                }
            }
            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GarantiTakibi()
        {
            return View();
        }
        public ActionResult GetGarantiTakibi()
        {

            List<TECHNICALGaranti> yonetim = new List<TECHNICALGaranti>();

            //string FirmaID = Session["FirmaID"].ToString();
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string sorg = @"set dateformat dmy;Select ServisIslemNo as [ServisIslemNo],case when GETDATE() between GarantiBaslangicTarihi and GarantiBitisTarihi  then 'Devam Ediyor' else 'Sona Erdi' end as [GarantiDurumu],(Select CariUnvan from cari where ID=CariID)as CariUnvan,Marka,CihazAdi,GarantiBaslangicTarihi,GarantiBitisTarihi,Serino,TECHNICAL.ID from TECHNICAL where FirmaID=" + Session["FirmaID"].ToString();
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TECHNICALGaranti yt = new TECHNICALGaranti();
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.GarantiBaslangicTarihi = Convert.ToDateTime(dr["GarantiBaslangicTarihi"]);
                            yt.GarantiBitisTarihi = Convert.ToDateTime(dr["GarantiBitisTarihi"]);
                            yt.CariUnvan = dr["CariUnvan"].ToString();
                            yt.ServisIslemNo = dr["ServisIslemNo"].ToString();
                            yt.GarantiDurumu = dr["GarantiDurumu"].ToString();
                            yt.Marka = dr["Marka"].ToString();
                            yt.CihazAdi = dr["CihazAdi"].ToString();
                            yt.Serino = Convert.ToString(dr["Serino"]);
                            yonetim.Add(yt);
                        }

                    }
                }
            }



            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }

    }
}