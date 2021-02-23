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


namespace SaTeknopark_MVC5
{
    public class AyarMetot
    {
        public static string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public static string PB_Short = "TL";
        public static string Hafta, OrtakBelge = "", PersonelTelefon = "", SpecialKontrol = "";
        public const string ilhan_Control = "#*514ilhansayaz2018";
        public static string FirmaL = "", Donem = DateTime.Now.Year.ToString(), PB_Long, FMail, PersonelAdi,
            fMailSifre, fMailSender, SMSUser, SMSPass, SMSSender, PostaSunucu,
            Read_Process = "Tüm İşlemler", KurGuncellemeMB = "",
            NegatifUrunKontrolu, DovizKullanimi = "Merkez Bankası", DepoListYetki = "",
            vsMiktarBasamak = "N2";
        public static decimal ManuelDolarKur = 1, ManuelEuroKur = 1, DolarinEuroKur = 1,
            EuronunDolarKur = 1;
        public static int FPort, PrimOrani = 0, PersonelID = 1,
            PersonelDepoID = 1;
        public static bool UzakKullanıcı = false, Fabrika = false, ElTerminali = false, EmanetTakibi = false, KodileArama = false, sgkGirisi = false, TabletorPC = false, MailSsl = false,
            Admin = true, Guncelleme = false, SecondPC = false;

        public static int vBolum = -1, pBankaID = -1, pKasaID = -1, pKKTID = -1, BankaSayisi = 1;

        public static DateTime TermBaslangici, TermBitisi;

        public static string BoyutSecimi = "Kapalı", ProTerminal = "Seçilmedi", SmsFirma = "Varsayılan", UnvanAdi = "Cari Ünvanı", CompanyName = "", StokEkleme = "Açık", PersonelEkleme = "Açık", CariEkleme = "Açık",
            Delete_Process = "Evet", GRUP_PERSONEL = "Kullanıcı", IadeYetki = "Evet", Package_Type = "Servis";

        public static bool FaturaEkranindaCari = false, AracOtoModulu = false, OzelKodOrderNo = false, FaturadaFireGirisi = false,
            salonyonetimi = false, SantiyeKullan = false, Efatura = false, PersonelRaporYetkisi = true, AlisFiyatGor = true;

        public static string kAdi = "sa", sifre = "123456", CompanyGIBKutusu = "";
        public static string CompanyaliciKutusu = "";
        public static string CompanyKullanici = "";
        public static string CompanySifre = "";
        public static string CompanyVkn = "";
        public static string CompanySehir = "";
        public static string CompanyAdres = "";
        public static string CompanyTel = "";
        public static string CompanyMesajNumara = "";
        public static string CompanySube = "MERKEZ";
        public static string GetNumara;
        public static string HizliBitir = "Kapalı";

        public static string SeciliPersonelBilgiCek(string Alan, string PersonelID)
        {
            try
            {
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (SqlCommand d6 = new SqlCommand("Select " + Alan + " From Personel where ID='" + PersonelID + "'", conp))
                    {
                        return d6.ExecuteScalar().ToString();
                    }
                }
            }
            catch { return ""; }
        }

        public static void SorguCalistir(string sorgu)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sorgu, con))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch
                    { }
                }
            }
        }
        public static void alanEkle()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                SorguCalistir("ALTER TABLE TECHNICAL ADD CihazSatisTarihi datetime NULL;");
                SorguCalistir("UPDATE TECHNICAL SET CihazSatisTarihi=Tarih WHERE (CihazSatisTarihi IS NULL)");


                SorguCalistir("ALTER TABLE TECHNICAL ADD GarantiBitisTarihi datetime NULL;");
                SorguCalistir("UPDATE TECHNICAL SET GarantiBitisTarihi=Tarih WHERE (GarantiBitisTarihi IS NULL)");


                SorguCalistir("ALTER TABLE TECHNICAL ADD GarantiBaslangicTarihi datetime NULL;");
                SorguCalistir("UPDATE TECHNICAL SET GarantiBaslangicTarihi=Tarih WHERE (GarantiBaslangicTarihi IS NULL)");


                SorguCalistir("ALTER TABLE TECHNICAL ADD CihazMontajTarihi datetime NULL;");
                SorguCalistir("UPDATE TECHNICAL SET CihazMontajTarihi=Tarih WHERE (CihazMontajTarihi IS NULL)");


                SorguCalistir("ALTER TABLE TECHNICAL ADD CihazAdi nvarchar(50) NULL;");
                SorguCalistir("UPDATE TECHNICAL SET CihazAdi=N'' WHERE (CihazAdi IS NULL)");

                SorguCalistir("ALTER TABLE TECHNICAL ADD CihazAdresi nvarchar(MAX) NULL;");
                SorguCalistir("UPDATE TECHNICAL SET CihazAdresi=N'' WHERE (CihazAdresi IS NULL)");


                SorguCalistir("ALTER TABLE Cari ADD DepoID int NULL;");
                SorguCalistir("UPDATE Cari SET DepoID=N'1' WHERE (DepoID IS NULL)");

                SorguCalistir("ALTER TABLE TECHNICAL ADD BayiID nvarchar(50) NULL;");
                SorguCalistir("UPDATE TECHNICAL SET BayiID=N'-1' WHERE (BayiID IS NULL)");

                SorguCalistir("ALTER TABLE Personel ADD sCariID int NULL;");
                SorguCalistir("UPDATE Personel SET sCariID=N'-1' WHERE (sCariID IS NULL)");

                SorguCalistir("ALTER TABLE Yetkiler ADD SiparisPopup bit NULL;");
                SorguCalistir("UPDATE Yetkiler SET SiparisPopup=0 WHERE (SiparisPopup IS NULL)");

                SorguCalistir("ALTER TABLE Kasa ADD Merkez bit NULL;");
                SorguCalistir("UPDATE Kasa SET Merkez=0 WHERE (Merkez IS NULL)");

                SorguCalistir("ALTER TABLE Banka ADD Merkez bit NULL;");
                SorguCalistir("UPDATE Banka SET Merkez=0 WHERE (Merkez IS NULL)");

                SorguCalistir("ALTER TABLE STORE ADD Merkez bit NULL;");
                SorguCalistir("UPDATE STORE SET Merkez=0 WHERE (Merkez IS NULL)");

                SorguCalistir("ALTER TABLE OZEL ADD Merkez bit NULL;");
                SorguCalistir("UPDATE OZEL SET Merkez=0 WHERE (Merkez IS NULL)");

                SorguCalistir("ALTER TABLE Personel ADD ToplamAdet int NULL;");
                SorguCalistir("UPDATE Personel SET ToplamAdet=0 WHERE (ToplamAdet IS NULL)");
                SorguCalistir("ALTER TABLE Personel ALTER COLUMN ToplamAdet int");
                SorguCalistir("ALTER TABLE TECHNICAL ALTER COLUMN BayiID int");

                SorguCalistir("ALTER TABLE STORE ADD BayiID nvarchar(50) NULL;");
                SorguCalistir("UPDATE STORE SET BayiID=N'-1' WHERE (BayiID IS NULL)");


                SorguCalistir("ALTER TABLE Firmalar ADD FirmaTel nvarchar(50) NULL;");
                SorguCalistir("UPDATE Firmalar SET FirmaTel='' WHERE (FirmaTel IS NULL)");

                SorguCalistir("ALTER TABLE Personel ADD PersonelBase64 image NULL;");
                SorguCalistir("UPDATE Personel SET PersonelBase64='' WHERE (PersonelBase64 IS NULL)");

                SorguCalistir("ALTER TABLE Firmalar ADD KayitTarih date NULL;");
                SorguCalistir("update Firmalar set KayitTarih=GETDATE() WHERE (KayitTarih IS NULL)");


                SorguCalistir("ALTER TABLE Cari ADD SeriNo nvarchar(MAX) NULL;");
                SorguCalistir("update Cari set SeriNo='' WHERE (SeriNo IS NULL)");

                SorguCalistir("ALTER TABLE TECHNICAL ADD ArizaKodu nvarchar(MAX) NULL;");
                SorguCalistir("update TECHNICAL set ArizaKodu='' WHERE (ArizaKodu IS NULL)");

                SorguCalistir("ALTER TABLE TECHNICAL ADD ArizaKodu nvarchar(MAX) NULL;");
                SorguCalistir("update TECHNICAL set ArizaKodu='' WHERE (ArizaKodu IS NULL)");

                SorguCalistir("ALTER TABLE Firmalar ADD Demo bit NULL;");
                SorguCalistir("update Firmalar set Demo=1 WHERE (Demo IS NULL)");


            }
        }

        public static int YuvarlamaSayisi = 2;
        public static int Price_Yuvarlama = 2;
        public static int Amount_Yuvarlama = 2;
        public static string General_YuvarlamaString = "N2";
        public static int Stok_Yuvarlama = 2;

        public static decimal eurokur = 1;
        public static decimal dolarkur = 1;



        public static DataTable FaturaDetayBil(string FatID, string KDVdh, decimal fkuru)

        {
            DataTable tbl = new DataTable("UrunBilgileri");
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("Sıra No", typeof(string));
            tbl.Columns.Add("Ürün Adı", typeof(string));
            tbl.Columns.Add("Ürün Kodu", typeof(string));
            tbl.Columns.Add("Seri Numara", typeof(string));
            tbl.Columns.Add("Depo", typeof(string));

            tbl.Columns.Add("Birim", typeof(string));
            tbl.Columns.Add("Miktar", typeof(string));
            tbl.Columns.Add("Fiyat", typeof(string));
            tbl.Columns.Add("Tutar", typeof(string));
            tbl.Columns.Add("Para Birimi", typeof(string));
            tbl.Columns.Add("Vade Tarihi", typeof(string));
            tbl.Columns.Add("KDV", typeof(string));
            tbl.Columns.Add("Tevfikat", typeof(string));
            tbl.Columns.Add("Variyant", typeof(string));
            tbl.Columns.Add("İskonto", typeof(string));
            tbl.Columns.Add("İade Süresi", typeof(string));
            tbl.Columns.Add("İskonto 2", typeof(string));
            tbl.Columns.Add("İskonto 3", typeof(string));
            tbl.Columns.Add("İskonto 4", typeof(string));
            tbl.Columns.Add("İskonto 5", typeof(string));
            tbl.Columns.Add("BARKODE", typeof(string));
            tbl.Columns.Add("Yerel Fiyat Satır", typeof(string));
            tbl.Columns.Add("Yerel Tutar Satır", typeof(string));
            tbl.Columns.Add("Satır Açıklaması", typeof(string));
            tbl.Columns.Add("Marka", typeof(string));
            tbl.Columns.Add("Adet Fiyatı", typeof(string));
            tbl.Columns.Add("Birim Adet", typeof(string));

            tbl.Columns.Add("Ek Özellik Adı", typeof(string));
            tbl.Columns.Add("Ek Özellik Açıklaması", typeof(string));
            tbl.Columns.Add("sfID", typeof(string));
            tbl.Columns.Add("Seri Numarası Listesi", typeof(string));
            tbl.Columns.Add("Kdv Dahil Fiyat", typeof(string));
            tbl.Columns.Add("Kdv Dahil Tutar", typeof(string));
            tbl.Columns.Add("Kdv Hariç Fiyat", typeof(string));

            tbl.Columns.Add("Kdv Hariç Tutar", typeof(string));
            tbl.Columns.Add("Satır Net Tutar", typeof(string));
            tbl.Columns.Add("Mal Fazlası", typeof(string));
            tbl.Columns.Add("Miktar (MF Dahil)", typeof(string));
            tbl.Columns.Add("Kdv Dahil Fiyat (İskontolu)", typeof(string));
            tbl.Columns.Add("Kdv Dahil Tutar (İskontolu)", typeof(string));
            tbl.Columns.Add("Kdv Hariç Fiyat (İskontolu)", typeof(string));

            tbl.Columns.Add("Kdv Hariç Tutar (İskontolu)", typeof(string));
            tbl.Columns.Add("Toplam Miktar Ana Birim ", typeof(string));
            tbl.Columns.Add("Miktar (Ana Birim)", typeof(string));
            tbl.Columns.Add("Hizmet Sonlanma Tarihi", typeof(string));
            tbl.Columns.Add("Miktar Num", typeof(string));
            tbl.Columns.Add("Fiyat Num", typeof(string));
            tbl.Columns.Add("Tutar Num", typeof(string));

            tbl.Columns.Add("Yerel Fiyat Satır Num", typeof(string));
            tbl.Columns.Add("Yerel Tutar Satır Num", typeof(string));
            tbl.Columns.Add("Kdv Dahil Tutar Num", typeof(string));
            tbl.Columns.Add("Kdv Dahil Fiyat Num", typeof(string));
            tbl.Columns.Add("Kdv Hariç Fiyat Num", typeof(string));
            tbl.Columns.Add("Kdv Hariç Tutar Num", typeof(string));


            tbl.Columns.Add("Kdv Dahil Fiyat (İskontolu) Num", typeof(string));
            tbl.Columns.Add("Kdv Dahil Tutar (İskontolu) Num", typeof(string));
            tbl.Columns.Add("Kdv Hariç Fiyat (İskontolu) Num", typeof(string));
            tbl.Columns.Add("Kdv Hariç Tutar (İskontolu) Num", typeof(string));
            tbl.Columns.Add("ÖİV", typeof(string));
            tbl.Columns.Add("Satır Özel Kod", typeof(string));
            tbl.Columns.Add("ÖTV", typeof(string));
            tbl.Columns.Add("Satır ÖTV Tutarı", typeof(string));

            tbl.Columns.Add("Ürün Özel A1", typeof(string));
            tbl.Columns.Add("Ürün Özel A2", typeof(string));
            tbl.Columns.Add("Ürün Özel A3", typeof(string));
            tbl.Columns.Add("Ürün Özel A4", typeof(string));
            tbl.Columns.Add("Ürün Özel A5", typeof(string));
            tbl.Columns.Add("Miktar Başına Satır ÖTV", typeof(string));
            tbl.Columns.Add("Ürün Grubu", typeof(string));

            tbl.Columns.Add("Variyant Renk", typeof(string));
            tbl.Columns.Add("Variyant RenkKod", typeof(string));
            tbl.Columns.Add("Variyant Beden", typeof(string));
            tbl.Columns.Add("Variyant Sezon", typeof(string));
            tbl.Columns.Add("Variyant Fiyat", typeof(string));
            tbl.Columns.Add("Variyant Miktar", typeof(string));
            tbl.Columns.Add("Variyant Açıklama", typeof(string));
            tbl.Columns.Add("Variyant Barkod", typeof(string));
            tbl.Columns.Add("Variyant RenkAdi", typeof(string));
            tbl.Columns.Add("Variyant BedenTuru", typeof(string));
            tbl.Columns.Add("Açıklama 2", typeof(string));
            tbl.Columns.Add("SevkMiktar", typeof(string));
            tbl.Columns.Add("IadeMiktar", typeof(string));
            tbl.Columns.Add("MalzemeSayiKodu", typeof(string));
            using (SqlConnection con1 = new SqlConnection(AyarMetot.strcon))
            {
                if (con1.State == ConnectionState.Closed) con1.Open();
                using (SqlCommand servisgetir = new SqlCommand("Select * From INVOICE_DETAIL where FaturaID='" + FatID + "'", con1))
                {
                    using (SqlDataReader da = servisgetir.ExecuteReader())
                    {
                        int a = 1;
                        while (da.Read())
                        {
                            DataRow ds = tbl.NewRow();

                            ds["ID"] = FatID;
                            int stkID = Convert.ToInt32(da["UrunID"]);
                            ds["Sıra NO"] = a.ToString();
                            a++;
                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select StokKodu From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Ürün Kodu"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Grubu From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Ürün Grubu"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select UrunAdi From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Ürün Adı"] = d6.ExecuteScalar().ToString().Replace("İ", "I").Replace("Ğ", "G").Replace("Ş", "S");
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Kategori1 From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Ürün Özel A1"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Kategori2 From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Ürün Özel A2"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Kategori3 From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Ürün Özel A3"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Kategori4 From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Ürün Özel A4"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }


                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Kategori5 From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Ürün Özel A5"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }



                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Marka From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["Marka"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand command = new SqlCommand("SELECT KodAdi From OzelKod where ID='" + da["SatirOzelKodID"] + "'", bag))
                                    {
                                        ds["Satır Özel Kod"] = command.ExecuteScalar();
                                    }
                                }
                            }
                            catch
                            { }

                            int sfID = -1;

                            ds["sfID"] = Convert.ToInt32(da["ID"]);
                            sfID = Convert.ToInt32(da["ID"]);

                            ds["Seri Numara"] = da["SeriNo"].ToString();
                            ds["Seri Numarası Listesi"] = da["SeriNoList"].ToString();

                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select DepoAdi From Depo where ID='" + da["depoID"] + "'", bag))
                                    {
                                        ds["Depo"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            ds["Birim"] = da["Birim"].ToString();



                            try
                            {


                                if (da["MalzemeSayi"].ToString().Substring(0, 1) == "T")
                                {
                                    ds["MalzemeSayiKodu"] = "";
                                }
                                else
                                {
                                    ds["MalzemeSayiKodu"] = da["MalzemeSayi"].ToString();

                                }

                            }
                            catch { ds["MalzemeSayiKodu"] = ""; }


                            if (da["Birim"].ToString().ToLower() == "adet")
                            {
                                try
                                {
                                    ds["IadeMiktar"] = Convert.ToInt32(da["IadeMiktar"]).ToString();
                                    ds["SevkMiktar"] = Convert.ToInt32(da["SevkMiktar"]).ToString();
                                }
                                catch { }
                                ds["Miktar"] = Convert.ToInt32(da["Miktar"]).ToString();
                                ds["Miktar Num"] = Convert.ToInt32(da["Miktar"]).ToString();
                                ds["Mal Fazlası"] = Convert.ToInt32(da["mf"]).ToString();
                                ds["Miktar (MF Dahil)"] = (Convert.ToInt32(da["Miktar"]) + Convert.ToInt32(da["mf"])).ToString();

                            }
                            else
                            {
                                try
                                {
                                    ds["IadeMiktar"] = Convert.ToInt32(da["IadeMiktar"]).ToString();
                                    ds["SevkMiktar"] = Convert.ToInt32(da["SevkMiktar"]).ToString();
                                }
                                catch { }


                                ds["Miktar"] = Convert.ToDecimal(da["Miktar"]).ToString("N" + "" + AyarMetot.Amount_Yuvarlama + "");
                                ds["Miktar Num"] = Convert.ToDecimal(da["Miktar"]).ToString();
                                ds["Mal Fazlası"] = Convert.ToDecimal(da["mf"]).ToString("N" + "" + AyarMetot.Amount_Yuvarlama + "");
                                ds["Miktar (MF Dahil)"] = (Convert.ToDecimal(da["Miktar"]) + Convert.ToInt32(da["mf"])).ToString("N" + "" + AyarMetot.Amount_Yuvarlama + "");

                            }

                            decimal toplammf = 0;
                            toplammf += Convert.ToDecimal(da["mf"]) * Convert.ToDecimal(da["Fiyat"]) * fkuru;
                            ds["Fiyat"] = (Convert.ToDecimal(da["frtFiyat"])).ToString(AyarMetot.General_YuvarlamaString);
                            ds["Fiyat Num"] = Convert.ToDecimal(da["frtFiyat"]).ToString(AyarMetot.General_YuvarlamaString);
                            ds["Birim Adet"] = Math.Round(Convert.ToDecimal(da["BirimAdet"]), AyarMetot.Stok_Yuvarlama).ToString(AyarMetot.General_YuvarlamaString);
                            try
                            {
                                ds["Toplam Miktar (Ana Birim)"] = Math.Round(Convert.ToDecimal(da["gBirimMiktar"]), AyarMetot.Stok_Yuvarlama).ToString();
                            }
                            catch { }
                            ds["Miktar (Ana Birim)"] = Math.Round(Convert.ToDecimal(da["BirimAdet"]), AyarMetot.Stok_Yuvarlama).ToString();
                            ds["Adet Fiyatı"] = Math.Round(Convert.ToDecimal(da["AdetFiyati"]), AyarMetot.Price_Yuvarlama).ToString();
                            ds["KDV"] = Convert.ToInt32(da["KDV"]).ToString();

                            ds["Tutar"] = Convert.ToDecimal(da["Tutar"]).ToString("N" + AyarMetot.YuvarlamaSayisi + "");
                            ds["Tutar Num"] = Convert.ToDecimal(da["Tutar"]).ToString();

                            ds["Kdv Dahil Fiyat"] = Math.Round(Convert.ToDecimal(da["ftrSondesc"]), AyarMetot.Price_Yuvarlama).ToString("N3");
                            ds["Kdv Hariç Fiyat"] = Math.Round(Convert.ToDecimal(da["frtFiyat"]), AyarMetot.Price_Yuvarlama).ToString("N3");
                            ds["Kdv Dahil Tutar"] = Math.Round(Convert.ToDecimal(da["actTutar"]), AyarMetot.Price_Yuvarlama).ToString("N3");
                            ds["Kdv Hariç Tutar"] = Math.Round(Convert.ToDecimal(da["Tutar"]), AyarMetot.Price_Yuvarlama).ToString("N3");

                            ds["Kdv Dahil Fiyat Num"] = Convert.ToDecimal(da["ftrSondesc"]).ToString();
                            ds["Kdv Hariç Fiyat Num"] = Convert.ToDecimal(da["frtFiyat"]).ToString();
                            ds["Kdv Dahil Tutar Num"] = Convert.ToDecimal(da["actTutar"]).ToString();
                            ds["Kdv Hariç Tutar Num"] = Convert.ToDecimal(da["Tutar"]).ToString();

                            try
                            {
                                ds["Kdv Dahil Fiyat (İskontolu) Num"] = ((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) - (((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                            }
                            catch { }

                            ds["Kdv Hariç Fiyat (İskontolu) Num"] = (Convert.ToDecimal(da["frtFiyat"]) - ((Convert.ToDecimal(da["frtFiyat"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                            ds["Kdv Dahil Tutar (İskontolu) Num"] = (Convert.ToDecimal(da["actTutar"]) - ((Convert.ToDecimal(da["actTutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                            ds["Kdv Hariç Tutar (İskontolu) Num"] = (Convert.ToDecimal(da["Tutar"]) - ((Convert.ToDecimal(da["Tutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();

                            if (KDVdh == "Dahil")

                            {
                                decimal haric = Convert.ToDecimal(da["Fiyat"]) / (1 + (Convert.ToDecimal(da["KDV"]) / 100));

                                ds["Kdv Dahil Fiyat Num"] = Convert.ToDecimal(da["Fiyat"]).ToString();
                                ds["Kdv Hariç Fiyat Num"] = haric.ToString();
                                ds["Kdv Dahil Tutar Num"] = ((Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]))).ToString();
                                ds["Kdv Hariç Tutar Num"] = Convert.ToDecimal(da["Tutar"]).ToString();

                                ds["Kdv Dahil Fiyat (İskontolu) Num"] = ((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) - (((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                ds["Kdv Hariç Fiyat (İskontolu) Num"] = (Convert.ToDecimal(da["frtFiyat"]) - ((Convert.ToDecimal(da["frtFiyat"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                ds["Kdv Dahil Tutar (İskontolu) Num"] = (Convert.ToDecimal(da["actTutar"]) - ((Convert.ToDecimal(da["actTutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                ds["Kdv Hariç Tutar (İskontolu) Num"] = (Convert.ToDecimal(da["Tutar"]) - ((Convert.ToDecimal(da["Tutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                            }
                            else if (KDVdh == "Hariç")
                            {
                                ds["Kdv Dahil Fiyat Num"] = ((Convert.ToDecimal(da["Fiyat"])) * (1 + (Convert.ToDecimal(da["KDV"]) / 100))).ToString();
                                ds["Kdv Hariç Fiyat Num"] = Convert.ToDecimal(da["Fiyat"]).ToString();
                                ds["Kdv Dahil Tutar Num"] = ((Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]) * (1 + (Convert.ToDecimal(da["KDV"]) / 100)))).ToString();
                                ds["Kdv Hariç Tutar Num"] = Convert.ToDecimal(da["Tutar"]).ToString();

                                try
                                {
                                    ds["Kdv Dahil Fiyat (İskontolu) Num"] = ((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) - (((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                }
                                catch
                                {
                                }

                                ds["Kdv Hariç Fiyat (İskontolu) Num"] = (Convert.ToDecimal(da["frtFiyat"]) - ((Convert.ToDecimal(da["frtFiyat"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                ds["Kdv Dahil Tutar (İskontolu) Num"] = (Convert.ToDecimal(da["actTutar"]) - ((Convert.ToDecimal(da["actTutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                                ds["Kdv Hariç Tutar (İskontolu) Num"] = (Convert.ToDecimal(da["Tutar"]) - ((Convert.ToDecimal(da["Tutar"]) / 100) * Convert.ToDecimal(da["Iskonta"]))).ToString();
                            }
                            try
                            {
                                ds["Kdv Dahil Fiyat (İskontolu)"] = Math.Round((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) - (((Convert.ToDecimal(da["actTutar"]) / Convert.ToDecimal(da["Miktar"])) / 100) * Convert.ToDecimal(da["Iskonta"])), AyarMetot.Price_Yuvarlama).ToString("N" + Price_Yuvarlama);
                            }
                            catch { }
                            ds["Kdv Hariç Fiyat (İskontolu)"] = Math.Round(Convert.ToDecimal(da["frtFiyat"]) - ((Convert.ToDecimal(da["frtFiyat"]) / 100) * Convert.ToDecimal(da["Iskonta"])), AyarMetot.Price_Yuvarlama).ToString("N" + Price_Yuvarlama);
                            ds["Kdv Dahil Tutar (İskontolu)"] = Math.Round(Convert.ToDecimal(da["actTutar"]) - ((Convert.ToDecimal(da["actTutar"]) / 100) * Convert.ToDecimal(da["Iskonta"])), AyarMetot.Price_Yuvarlama).ToString("N" + Price_Yuvarlama);
                            ds["Kdv Hariç Tutar (İskontolu)"] = Math.Round(Convert.ToDecimal(da["Tutar"]) - ((Convert.ToDecimal(da["Tutar"]) / 100) * Convert.ToDecimal(da["Iskonta"])), AyarMetot.Price_Yuvarlama).ToString("N" + Price_Yuvarlama);

                            ds["Para Birimi"] = da["pBirimi"].ToString();

                            if (da["pBirimi"].ToString() == "TL")
                                ds["Para Birimi"] = "TL";
                            else
                                ds["Para Birimi"] = da["pBirimi"].ToString();

                            ds["Vade Tarihi"] = da["VadeTarih"].ToString();

                            ds["Tevfikat"] = Convert.ToDecimal(da["Tevfikat"]).ToString("#,##0.00");
                            ds["İade Süresi"] = da["IadeSuresi"].ToString();
                            ds["Variyant"] = da["Variyant"].ToString();
                            //ds["defConR"] = da["defConR"].ToString();
                            ds["İskonto"] = Convert.ToDecimal(da["Iskonta"]).ToString("#,##0.00");
                            ds["İskonto 2"] = Convert.ToDecimal(da["Iskonta2"]).ToString("#,##0.00");
                            ds["İskonto 3"] = Convert.ToDecimal(da["Iskonta3"]).ToString("#,##0.00");
                            ds["İskonto 4"] = Convert.ToDecimal(da["Iskonta4"]).ToString("#,##0.00");
                            ds["İskonto 5"] = Convert.ToDecimal(da["Iskonta5"]).ToString("#,##0.00");

                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Barkod From Stok where ID='" + stkID + "'", bag))
                                    {
                                        ds["BARKODE"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            ds["Yerel Tutar Satır"] = (Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]) / Convert.ToDecimal(da["KUR"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Fiyat Satır"] = (Convert.ToDecimal(da["Fiyat"]) / Convert.ToDecimal(da["KUR"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Tutar Satır Num"] = (Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]) / Convert.ToDecimal(da["KUR"])).ToString();
                            ds["Yerel Fiyat Satır Num"] = (Convert.ToDecimal(da["Fiyat"]) / Convert.ToDecimal(da["KUR"])).ToString();
                            ds["Satır Açıklaması"] = da["EkAciklama"].ToString();
                            ds["Açıklama 2"] = da["STAciklama2"].ToString();
                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Adi From EkOzellik where ID='" + da["EkOzellikID"] + "'", bag))
                                    {
                                        ds["Ek Özellik Adı"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (bag.State == ConnectionState.Closed) bag.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Aciklama From EkOzellik where ID='" + da["EkOzellikID"] + "'", bag))
                                    {
                                        ds["Ek Özellik Açıklaması"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                ds["ÖTV"] = Convert.ToDecimal(da["otv"]).ToString("N" + Price_Yuvarlama);
                            }
                            catch { }

                            try
                            {
                                decimal tlToplam = (Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]));
                                decimal lToplam = (Convert.ToDecimal(da["Fiyat"]) * Convert.ToDecimal(da["Miktar"]));
                                lToplam = lToplam * (1 + (Convert.ToDecimal(da["otv"]) / 100));
                                ds["Satır ÖTV Tutarı"] = (lToplam - tlToplam).ToString("N" + Price_Yuvarlama);
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


                            try
                            {
                                ds["Miktar Başına Satır ÖTV"] = Convert.ToDecimal(da["MiktarBasinaOTV"]).ToString();
                            }
                            catch { }


                            using (SqlConnection con2 = new SqlConnection(AyarMetot.strcon))
                            {
                                if (con2.State == ConnectionState.Closed) con2.Open();
                                using (SqlCommand gt2 = new SqlCommand("Select * From VARIANT WHERE ID='" + da["VariyantSel"] + "'", con2))
                                {
                                    using (SqlDataReader dav = gt2.ExecuteReader())
                                    {

                                        while (dav.Read())
                                        {
                                            ds["Variyant Renk"] = dav["Renk"].ToString();
                                            ds["Variyant RenkKod"] = dav["RenkKod"].ToString();
                                            ds["Variyant Beden"] = dav["Beden"].ToString();
                                            ds["Variyant Sezon"] = dav["Sezon"].ToString();
                                            ds["Variyant Fiyat"] = dav["Fiyat"].ToString();
                                            ds["Variyant Miktar"] = dav["Miktar"].ToString();
                                            ds["Variyant Açıklama"] = dav["Aciklama"].ToString();
                                            ds["Variyant Barkod"] = dav["Barkod"].ToString();
                                            ds["Variyant RenkAdi"] = dav["RenkAdi"].ToString();
                                            ds["Variyant BedenTuru"] = dav["BedenTuru"].ToString();
                                        }
                                    }
                                }
                            }


                            tbl.Rows.Add(ds);


                        }
                    }
                }
            }
            return tbl;
        }

        public static DataTable FaturaBil(string pID)

        {
            DataTable tbl = new DataTable("FaturaBilgileri");



            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("İşlem Numarası", typeof(string));
            tbl.Columns.Add("İşlem Tarihi", typeof(string));
            tbl.Columns.Add("Personel", typeof(string));
            tbl.Columns.Add("Vade Tarihi", typeof(string));
            tbl.Columns.Add("İşlem Para Birimi", typeof(string));
            tbl.Columns.Add("KDV", typeof(string));
            tbl.Columns.Add("Tevfikat", typeof(string));
            tbl.Columns.Add("İade Süresi", typeof(string));
            tbl.Columns.Add("Kayıt Saati", typeof(string));
            tbl.Columns.Add("Sevk Tarihi", typeof(string));

            tbl.Columns.Add("Aktarılan İrsaliyeler", typeof(string));
            tbl.Columns.Add("YerelKarşılığı", typeof(string));
            tbl.Columns.Add("YerelKarşılığıYazıyla", typeof(string));
            tbl.Columns.Add("Doviz Karşılığı (USD)", typeof(string));
            tbl.Columns.Add("Doviz Karşılığı (EUR)", typeof(string));
            tbl.Columns.Add("Satır Kuru (USD)", typeof(string));
            tbl.Columns.Add("Satır Kuru (EUR)", typeof(string));
            tbl.Columns.Add("FKuru", typeof(string));
            tbl.Columns.Add("Açıklama", typeof(string));
            tbl.Columns.Add("İskonto", typeof(string));

            tbl.Columns.Add("Brüt Toplam", typeof(string));
            tbl.Columns.Add("Özel İskonto", typeof(string));
            tbl.Columns.Add("Satır İskonto", typeof(string));
            tbl.Columns.Add("Toplam İskonto", typeof(string));
            tbl.Columns.Add("Net Toplam", typeof(string));
            tbl.Columns.Add("Toplam %18 KDV", typeof(string));
            tbl.Columns.Add("Toplam %8 KDV", typeof(string));
            tbl.Columns.Add("Toplam %1 KDV", typeof(string));
            tbl.Columns.Add("Toplam KDV", typeof(string));
            tbl.Columns.Add("Genel Toplam", typeof(string));

            tbl.Columns.Add("Yerel Brüt Toplam", typeof(string));
            tbl.Columns.Add("Yerel Satır İskonto", typeof(string));
            tbl.Columns.Add("Yerel Özel İskonto", typeof(string));
            tbl.Columns.Add("Yerel Toplam İskonto", typeof(string));
            tbl.Columns.Add("Yerel Net Toplam", typeof(string));
            tbl.Columns.Add("Yerel Toplam %18 KDV", typeof(string));
            tbl.Columns.Add("Yerel Toplam %8 KDV", typeof(string));
            tbl.Columns.Add("Yerel Toplam %1 KDV", typeof(string));
            tbl.Columns.Add("Yerel Toplam KDV", typeof(string));
            tbl.Columns.Add("Yerel Tevfikat", typeof(string));

            tbl.Columns.Add("Tarih", typeof(string));
            tbl.Columns.Add("Uzun Tarih", typeof(string));
            tbl.Columns.Add("Uzun Tarih ve Haftanın Günü", typeof(string));
            tbl.Columns.Add("Saat", typeof(string));
            tbl.Columns.Add("Uzun Saat", typeof(string));
            tbl.Columns.Add("Yalnız Yazısı", typeof(string));
            tbl.Columns.Add("Güncel Dolar Kuru", typeof(string));
            tbl.Columns.Add("Güncel Euro Kuru", typeof(string));
            tbl.Columns.Add("Firma Kodu", typeof(string));
            tbl.Columns.Add("Cari Ünvanı", typeof(string));

            tbl.Columns.Add("Satır Toplamı Kdvsiz ", typeof(string));
            tbl.Columns.Add("Açıklama 2", typeof(string));
            tbl.Columns.Add("Özel Alan 1", typeof(string));
            tbl.Columns.Add("Özel Alan 2", typeof(string));
            tbl.Columns.Add("Özel Alan 3", typeof(string));
            tbl.Columns.Add("Özel Alan 4", typeof(string));
            tbl.Columns.Add("Özel Alan 5", typeof(string));

            tbl.Columns.Add("Sevk Adresi", typeof(string));
            tbl.Columns.Add("Fatura Adresi", typeof(string));
            tbl.Columns.Add("İlgili Kişi", typeof(string));

            tbl.Columns.Add("Cari Vergi Dairesi", typeof(string));
            tbl.Columns.Add("Cari Vergi Numarası", typeof(string));
            tbl.Columns.Add("Proje Adı", typeof(string));
            tbl.Columns.Add("Cari E Posta", typeof(string));
            tbl.Columns.Add("Cari Şehir", typeof(string));
            tbl.Columns.Add("Cari Telefon", typeof(string));
            tbl.Columns.Add("Cari GSM", typeof(string));
            tbl.Columns.Add("Cari Ulke", typeof(string));
            tbl.Columns.Add("Cari İlçe", typeof(string));
            tbl.Columns.Add("Cari Fatura Adresi", typeof(string));
            tbl.Columns.Add("Cari İrsaliye Adresi", typeof(string));

            tbl.Columns.Add("Cari Grubu", typeof(string));
            tbl.Columns.Add("Cari TCNo", typeof(string));
            tbl.Columns.Add("Tevfikatsız Toplam KDV", typeof(string));
            tbl.Columns.Add("Toplam Net Miktar", typeof(string));
            tbl.Columns.Add("Ödenen", typeof(string));
            tbl.Columns.Add("Kalan", typeof(string));
            tbl.Columns.Add("Toplam ÖİV", typeof(string));
            tbl.Columns.Add("Yerel Toplam ÖİV", typeof(string));
            tbl.Columns.Add("Toplam MF", typeof(string));
            tbl.Columns.Add("Toplam ÖTV", typeof(string));
            tbl.Columns.Add("Yerel Toplam ÖTV", typeof(string));
            tbl.Columns.Add("Sipariş No", typeof(string));
            tbl.Columns.Add("Servis No", typeof(string));
            tbl.Columns.Add("Özel Alan 6", typeof(string));
            tbl.Columns.Add("Özel Alan 7", typeof(string));
            tbl.Columns.Add("Özel Alan 8", typeof(string));
            tbl.Columns.Add("Fatura Özel Kod (Order No)", typeof(string));
            tbl.Columns.Add("Ödenen_Alınan Tutar", typeof(string));
            tbl.Columns.Add("Fatura Öncesi Bakiye", typeof(string));
            tbl.Columns.Add("Fatura Öncesi Bakiye (AB)", typeof(string));

            tbl.Columns.Add("DolarKuruVar", typeof(bool));
            tbl.Columns.Add("EuroKuruVar", typeof(bool));
            tbl.Columns.Add("GBPKuruVar", typeof(bool));

            tbl.Columns.Add("Toplam %18 KDV Matrahı", typeof(string));
            tbl.Columns.Add("Toplam %8 KDV Matrahı", typeof(string));
            tbl.Columns.Add("Toplam %1 KDV Matrahı", typeof(string));

            tbl.Columns.Add("TLUSDKUR", typeof(decimal));
            tbl.Columns.Add("TLEURKUR", typeof(decimal));
            tbl.Columns.Add("TLGBPKUR", typeof(decimal));
            tbl.Columns.Add("TLKURPB", typeof(decimal));

            using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand servisgetir = new SqlCommand("Select * From INVOICE where ID='" + pID + "'", bag))
                {
                    using (SqlDataReader dt = servisgetir.ExecuteReader())
                    {
                        while (dt.Read())
                        {

                            DataRow ds = tbl.NewRow();

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select KodAdi From OzelKod where ID='" + dt["FaturaOzelKod"] + "'", con7))
                                    {
                                        ds["Fatura Özel Kod (Order No)"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Tutar From CASH_PAY where Aciklama LIKE '%" + dt["Faturano"].ToString() + "%'", con7))
                                    {
                                        ds["Ödenen_Alınan Tutar"] = d6.ExecuteScalar().ToString();
                                    }
                                }
                            }
                            catch { ds["Ödenen_Alınan Tutar"] = 0; }


                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Telefon1 From Cari where ID='" + dt["CariID"] + "'", con7))
                                    {
                                        ds["Cari Telefon"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }




                            string BorcAB = "";
                            decimal bk = 0;


                            try
                            {
                                using (SqlConnection bag1 = new SqlConnection(AyarMetot.strcon))
                                {
                                    bag1.Open();
                                    using (SqlCommand getir = new SqlCommand("Select alacakB,borcB From BALANCE where cariID='" + dt["CariID"] + "' and ParaBirimi='" + dt["paraBirimi"].ToString() + "'", bag1))
                                    {
                                        using (SqlDataReader cb = getir.ExecuteReader())
                                        {
                                            while (cb.Read())
                                            {
                                                decimal ABK = 0;
                                                decimal BBK = 0;

                                                try { ABK = Convert.ToDecimal(cb["alacakB"]); } catch { }
                                                try
                                                { BBK = Convert.ToDecimal(cb["borcB"]); }
                                                catch { }

                                                if (ABK - BBK < 0)
                                                {
                                                    bk = (Convert.ToDecimal(dt["Geneltoplam"]) + (ABK - BBK)); ;
                                                }
                                                else if (ABK - BBK > 0)
                                                {
                                                    bk = (ABK - BBK + Convert.ToDecimal(dt["Geneltoplam"]));
                                                }
                                                else
                                                {
                                                    bk = (Convert.ToDecimal(dt["Geneltoplam"]) + ABK - BBK);
                                                }

                                                if (bk < 0)
                                                {
                                                    BorcAB = (-(bk)).ToString("N2").ToString() + " (B)";
                                                }
                                                else if (bk > 0)
                                                {
                                                    BorcAB = (bk).ToString("N2").ToString() + " (A)";
                                                }
                                                else
                                                {
                                                    BorcAB = (bk).ToString("N2").ToString() + " ( )";
                                                }

                                                ds["Fatura Öncesi Bakiye"] = bk.ToString();
                                                ds["Fatura Öncesi Bakiye (AB)"] = BorcAB;
                                            }
                                        }
                                    }
                                }

                            }
                            catch { }



                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select GSM From Cari where ID='" + dt["CariID"] + "'", con7))
                                    {
                                        ds["Cari GSM"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Eposta From Cari where ID='" + dt["CariID"] + "'", con7))
                                    {
                                        ds["Cari E-Posta"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Il From Cari where ID='" + dt["CariID"] + "'", con7))
                                    {
                                        ds["Cari Şehir"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Ilce From Cari where ID='" + dt["CariID"] + "'", con7))
                                    {
                                        ds["Cari İlçe"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Adres From Cari where ID='" + dt["CariID"] + "'", con7))
                                    {
                                        ds["Cari Fatura Adresi"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Adres2 From Cari where ID='" + dt["CariID"] + "'", con7))
                                    {
                                        ds["Cari İrsaliye Adresi"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select CariGrubu From Cari where ID='" + dt["CariID"] + "'", con7))
                                    {
                                        ds["Cari Grubu"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select TcNo From Cari where ID='" + dt["CariID"] + "'", con7))
                                    {
                                        ds["Cari TcNo"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }


                            decimal USDKURU = 1;
                            decimal EURKURU = 1;




                            ds["İşlem Numarası"] = dt["FaturaNo"].ToString();
                            ds["ID"] = dt["ID"].ToString();
                            try
                            {
                                ds["İşlem Tarihi"] = Convert.ToDateTime(dt["FaturaTarihi"]).ToString("dd.MM.yyyy");
                            }
                            catch
                            {
                                ds["İşlem Tarihi"] = dt["FaturaTarihi"].ToString();
                            }
                            ds["Fkuru"] = Convert.ToDecimal(dt["FKuru"]).ToString("#,##0.0000");
                            try
                            {
                                ds["TLUSDKUR"] = Convert.ToDecimal(dt["TLUSDKUR"]);
                            }
                            catch { ds["TLUSDKUR"] = 1; }
                            try
                            {
                                ds["TLEURKUR"] = Convert.ToDecimal(dt["TLEURKUR"]);
                            }
                            catch { ds["TLEURKUR"] = 1; }
                            try
                            {
                                ds["TLGBPKUR"] = Convert.ToDecimal(dt["TLGBPKUR"]);
                            }
                            catch { ds["TLGBPKUR"] = 1; }
                            if (Convert.ToDecimal(dt["FKuru"]) == 1)
                            {
                                try
                                {
                                    if (dt["paraBirimi"].ToString() == "EUR")
                                        ds["TLKURPB"] = Convert.ToDecimal(dt["TLEURKUR"]);
                                    else if (dt["paraBirimi"].ToString() == "USD")
                                        ds["TLKURPB"] = Convert.ToDecimal(dt["TLUSDKUR"]);
                                    else if (dt["paraBirimi"].ToString() == "GBP")
                                        ds["TLKURPB"] = Convert.ToDecimal(dt["TLGBPKUR"]);
                                }
                                catch { ds["TLKURPB"] = Convert.ToDecimal(dt["Fkuru"]); }
                            }
                            else
                            {
                                ds["TLKURPB"] = Convert.ToDecimal(dt["Fkuru"]);

                            }

                            ds["Kayıt Saati"] = dt["Saat"].ToString();
                            ds["Satır Kuru (USD)"] = Math.Round(Convert.ToDecimal(dt["DolarKur"]), 4).ToString();
                            ds["Satır Kuru (EUR)"] = Math.Round(Convert.ToDecimal(dt["EuroKur"]), 4).ToString();

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select ProjeAdi From PROJECTS where ID='" + dt["ProjeID"] + "'", con7))
                                    {
                                        ds["Proje Adı"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            if (dt["YerelTutar"] != DBNull.Value)
                            {
                                ds["YerelKarşılığı"] = Convert.ToDecimal(dt["YerelTutar"]).ToString("#,##0.00");

                                if (USDKURU != 0)
                                    ds["Doviz Karşılığı (USD)"] = (Convert.ToDecimal(dt["YerelTutar"]) * (1 / USDKURU)).ToString("#,##0.00");
                                else
                                    ds["Doviz Karşılığı (USD)"] = (Convert.ToDecimal(dt["YerelTutar"])).ToString("#,##0.00");

                                if (EURKURU != 0)
                                    ds["Doviz Karşılığı (EUR)"] = (Convert.ToDecimal(dt["YerelTutar"]) * (1 / EURKURU)).ToString("#,##0.00");
                                else
                                    ds["Doviz Karşılığı (EUR)"] = (Convert.ToDecimal(dt["YerelTutar"])).ToString("#,##0.00");

                                ds["YerelKarşılığıYazıyla"] = textify(Convert.ToDecimal(dt["YerelTutar"]));
                            }

                            if (dt["FaturaType"].ToString() == "Alım Faturası" || dt["FaturaType"].ToString() == "Satış Faturası")
                            {
                                if (dt["IrsaliyeID"].ToString() != "-1")
                                {
                                    string[] tID = dt["IrsaliyeID"].ToString().Split('.');
                                    string fIslemNos = "";
                                    for (int i = 0; i < tID.Length; i++)
                                    {
                                        using (SqlConnection conni = new SqlConnection(AyarMetot.strcon))
                                        {
                                            conni.Open();

                                            using (SqlCommand igetir = new SqlCommand("Select FaturaNo From INVOICE where ID='" + tID[i] + "'", conni))
                                            {
                                                using (SqlDataReader cb = igetir.ExecuteReader())
                                                {
                                                    while (cb.Read())
                                                    {
                                                        if (fIslemNos == "") fIslemNos = cb["FaturaNo"].ToString();
                                                        else fIslemNos += ", " + cb["FaturaNo"].ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    ds["Aktarılan İrsaliyeler"] = fIslemNos;


                                    try
                                    {

                                        ds["Sevk Tarihi"] = Convert.ToDateTime(dt["SevkTarihi"]).ToString("dd.MM.yyyy");
                                    }
                                    catch
                                    {
                                        ds["Sevk Tarihi"] = dt["SevkTarihi"].ToString();

                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        ds["Sevk Tarihi"] = Convert.ToDateTime(dt["SevkTarihi"]).ToString("dd.MM.yyyy");
                                    }
                                    catch
                                    {
                                        ds["Sevk Tarihi"] = dt["SevkTarihi"].ToString();
                                    }
                                }
                            }

                            if (dt["paraBirimi"].ToString() == "TL")
                                ds["İşlem Para Birimi"] = "TL";
                            else
                                ds["İşlem Para Birimi"] = dt["paraBirimi"].ToString();
                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Adi+' '+Soyadi From Personel where ID='" + dt["PersonelId"] + "'", conp))
                                    {
                                        ds["Personel"] = d6.ExecuteScalar().ToString().Replace("İ", "I").Replace("Ğ", "G").Replace("Ş", "S");
                                    }
                                }
                            }
                            catch { }


                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select SiparisNo From ORDERS where ID='" + dt["SiparisID"] + "'", conp))
                                    {
                                        ds["Sipariş No"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select ServisIslemNo From TECHNICAL where ID='" + dt["ServisCihazID"] + "'", conp))
                                    {
                                        ds["Servis No"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }

                            ds["Vade Tarihi"] = dt["gVadeTarih"].ToString();
                            ds["KDV"] = Convert.ToDecimal(dt["gKDV"]).ToString("#,##0.00");
                            ds["Tevfikat"] = Convert.ToDecimal(dt["gTevfikat"]).ToString("#,##0.00");
                            ds["İskonto"] = Convert.ToDecimal(dt["gIskonta"]).ToString("#,##0.00");
                            ds["Açıklama"] = dt["Aciklama"].ToString();
                            ds["Açıklama 2"] = dt["Aciklama2"].ToString();
                            ds["KDV"] = Convert.ToDecimal(dt["tKDV"]).ToString("#,##0.00");
                            //ds["Dolar Net Bakiye"] = getBALANCEEuroUSD(cariID, "USD");
                            //ds["Euro Net Bakiye"] = getBALANCEEuroUSD(cariID, "EUR");

                            ds["Tarih"] = DateTime.Now.ToString("d");
                            ds["Uzun Tarih"] = DateTime.Now.ToString("dd MMMM yyyy");
                            ds["Uzun Tarih ve Haftanın Günü"] = DateTime.Now.ToString("dd MMMM yyyy dddd");
                            ds["Saat"] = DateTime.Now.ToString("HH:mm");
                            ds["Uzun Saat"] = DateTime.Now.ToString("HH:mm:ss");
                            ds["Güncel Dolar Kuru"] = Math.Round(AyarMetot.dolarkur, 4).ToString();
                            ds["Güncel Euro Kuru"] = Math.Round(AyarMetot.eurokur, 4).ToString();
                            if (dt["paraBirimi"].ToString() == "TL")
                            {
                                ds["Yalnız Yazısı"] = textify(Convert.ToDecimal(dt["GenelToplam"]));
                            }
                            else if (dt["paraBirimi"].ToString() == "USD")
                            {
                                ds["Yalnız Yazısı"] = textifyDolar(Convert.ToDecimal(dt["GenelToplam"]));
                            }
                            else if (dt["paraBirimi"].ToString() == "EUR")
                            {
                                ds["Yalnız Yazısı"] = textifyEuro(Convert.ToDecimal(dt["GenelToplam"]));
                            }
                            else
                            {
                                ds["Yalnız Yazısı"] = textify(Convert.ToDecimal(dt["GenelToplam"]));
                            }


                            ds["Yerel Brüt Toplam"] = (Convert.ToDecimal(dt["brutToplam"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Satır İskonto"] = (Convert.ToDecimal(dt["satirIskonta"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Özel İskonto"] = (Convert.ToDecimal(dt["ozelIskonta"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Net Toplam"] = (Convert.ToDecimal(dt["netToplam"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Toplam %18 KDV"] = (Convert.ToDecimal(dt["g18KDV"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Toplam %8 KDV"] = (Convert.ToDecimal(dt["g8KDV"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Toplam %1 KDV"] = (Convert.ToDecimal(dt["g1KDV"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Toplam KDV"] = (Convert.ToDecimal(dt["tKDV"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            try
                            {
                                ds["Yerel Toplam ÖİV"] = (Convert.ToDecimal(dt["tOIV"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            }
                            catch { }

                            try
                            {
                                ds["Yerel Toplam ÖtV"] = (Convert.ToDecimal(dt["OTVToplami"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            }
                            catch { }


                            ds["Yerel Toplam İskonto"] = (Convert.ToDecimal(dt["toplamIskonta"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Tevfikat"] = (Convert.ToDecimal(dt["gTevfikat"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);

                            decimal netmiktar = 0;



                            ds["Toplam Net Miktar"] = netmiktar.ToString("N" + Amount_Yuvarlama);

                            ds["Özel Alan 1"] = dt["OzelAlan1"].ToString();
                            ds["Özel Alan 2"] = dt["OzelAlan2"].ToString();
                            ds["Özel Alan 3"] = dt["OzelAlan3"].ToString();
                            ds["Özel Alan 4"] = dt["OzelAlan4"].ToString();
                            ds["Özel Alan 5"] = dt["OzelAlan5"].ToString();
                            ds["Özel Alan 6"] = dt["OzelAlan6"].ToString();
                            ds["Özel Alan 7"] = dt["OzelAlan7"].ToString();
                            ds["Özel Alan 8"] = dt["OzelAlan8"].ToString();
                            ds["Sevk Adresi"] = dt["SevkAdresi"].ToString();


                            try
                            {
                                using (SqlConnection con1 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con1.Open();
                                    using (SqlCommand command = new SqlCommand("SELECT (CASE WHEN Adres != '' THEN Adres else '' END) +(CASE WHEN Ilce != '' THEN ' '+Ilce else '' END)+ (CASE WHEN IL != '' THEN '/'+IL else '' END)  From Cari" +
                                        " where ID='" + dt["CariID"] + "'", con1))
                                    {
                                        ds["Fatura Adresi"] = command.ExecuteScalar().ToString();
                                    }
                                }
                            }
                            catch
                            { }



                            ds["İlgili Kişi"] = dt["Yetkili"].ToString();
                            ds["Cari Vergi Dairesi"] = dt["CariVergD"].ToString();
                            ds["Cari Vergi Numarası"] = dt["CariVergiNo"].ToString();
                            try
                            {
                                ds["İşlem Türü"] = dt["FaturaType"].ToString();
                            }
                            catch { }


                            ds["Brüt Toplam"] = Convert.ToDecimal(dt["brutToplam"]).ToString("N" + Price_Yuvarlama);
                            ds["Satır İskonto"] = Convert.ToDecimal(dt["satirIskonta"]).ToString("N" + Price_Yuvarlama);
                            ds["Özel İskonto"] = Convert.ToDecimal(dt["ozelIskonta"]).ToString("N" + Price_Yuvarlama);
                            ds["Net Toplam"] = Convert.ToDecimal(dt["netToplam"]).ToString("N" + Price_Yuvarlama);
                            ds["Toplam %18 KDV"] = Convert.ToDecimal(dt["g18KDV"]).ToString("N" + Price_Yuvarlama);
                            ds["Toplam %8 KDV"] = Convert.ToDecimal(dt["g8KDV"]).ToString("N" + Price_Yuvarlama);
                            ds["Toplam %1 KDV"] = Convert.ToDecimal(dt["g1KDV"]).ToString("N" + Price_Yuvarlama);
                            ds["Toplam KDV"] = Convert.ToDecimal(dt["tKDV"]).ToString("N" + Price_Yuvarlama);
                            try
                            {
                                ds["Toplam ÖİV"] = Convert.ToDecimal(dt["tOIV"]).ToString("N" + Price_Yuvarlama);
                            }
                            catch { }

                            try
                            {
                                ds["Toplam ÖTV"] = Convert.ToDecimal(dt["OTVToplami"]).ToString("N" + Price_Yuvarlama);
                            }
                            catch { }


                            ds["Toplam İskonto"] = Convert.ToDecimal(dt["toplamIskonta"]).ToString("N" + Price_Yuvarlama);
                            ds["Genel Toplam"] = ((Convert.ToDecimal(Convert.ToDecimal(dt["NetToplam"]).ToString("N2"))) + (Convert.ToDecimal(Convert.ToDecimal(dt["tKDV"]).ToString("N2"))) - (Convert.ToDecimal(Convert.ToDecimal(dt["tTevfikat"]).ToString("N2")))).ToString("N" + YuvarlamaSayisi);
                            ds["Tevfikatsız Toplam KDV"] = (Convert.ToDecimal(dt["tKDV"]) - Convert.ToDecimal(dt["tTevfikat"])).ToString("N" + Price_Yuvarlama);
                            decimal fkuru = 0;

                            try
                            {
                                using (SqlConnection con7 = new SqlConnection(AyarMetot.strcon))
                                {
                                    con7.Open();
                                    using (SqlCommand appvers = new SqlCommand("Select Fkuru from INVOICE  where ID='" + dt["ID"] + "'", con7))
                                    {

                                        fkuru = Convert.ToDecimal(appvers.ExecuteScalar());

                                    }

                                }

                            }
                            catch { }




                            ds["Yerel Brüt Toplam"] = (Convert.ToDecimal(dt["brutToplam"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Satır İskonto"] = (Convert.ToDecimal(dt["satirIskonta"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Özel İskonto"] = (Convert.ToDecimal(dt["ozelIskonta"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Net Toplam"] = (Convert.ToDecimal(dt["netToplam"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Toplam %18 KDV"] = (Convert.ToDecimal(dt["g18KDV"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Toplam %8 KDV"] = (Convert.ToDecimal(dt["g8KDV"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Toplam %1 KDV"] = (Convert.ToDecimal(dt["g1KDV"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Toplam KDV"] = (Convert.ToDecimal(dt["tKDV"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Toplam İskonto"] = (Convert.ToDecimal(dt["toplamIskonta"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);
                            ds["Yerel Tevfikat"] = (Convert.ToDecimal(dt["tTevfikat"]) * Convert.ToDecimal(dt["FKuru"])).ToString("N" + Price_Yuvarlama);



                            ds["Tevfikat"] = Convert.ToDecimal(dt["tTevfikat"]).ToString("N" + Price_Yuvarlama);
                            ds["Brüt Toplam"] = Convert.ToDecimal(dt["brutToplam"]).ToString("N" + Price_Yuvarlama);
                            ds["Satır İskonto"] = Convert.ToDecimal(dt["satirIskonta"]).ToString("N" + Price_Yuvarlama);
                            ds["Özel İskonto"] = Convert.ToDecimal(dt["ozelIskonta"]).ToString("N" + Price_Yuvarlama);
                            ds["Net Toplam"] = Convert.ToDecimal(dt["netToplam"]).ToString("N" + Price_Yuvarlama);
                            ds["Toplam %18 KDV"] = Convert.ToDecimal(dt["g18KDV"]).ToString("N" + Price_Yuvarlama);
                            ds["Toplam %8 KDV"] = Convert.ToDecimal(dt["g8KDV"]).ToString("N" + Price_Yuvarlama);
                            ds["Toplam %1 KDV"] = Convert.ToDecimal(dt["g1KDV"]).ToString("N" + Price_Yuvarlama);
                            ds["Toplam KDV"] = Convert.ToDecimal(dt["tKDV"]).ToString("N" + Price_Yuvarlama);
                            ds["Toplam İskonto"] = Convert.ToDecimal(dt["toplamIskonta"]).ToString("N" + Price_Yuvarlama);
                            ds["Genel Toplam"] = ((Convert.ToDecimal(Convert.ToDecimal(dt["NetToplam"]).ToString("N2"))) + (Convert.ToDecimal(Convert.ToDecimal(dt["tKDV"]).ToString("N2"))) - (Convert.ToDecimal(Convert.ToDecimal(dt["tTevfikat"]).ToString("N2")))).ToString("N" + YuvarlamaSayisi);

                            try
                            {
                                ds["DolarKuruVar"] = Convert.ToBoolean(dt["DolarVar"]);
                            }
                            catch { }
                            try
                            {
                                ds["EuroKuruVar"] = Convert.ToBoolean(dt["EuroVar"]);
                            }
                            catch { }
                            try
                            {
                                ds["GBPKuruVar"] = Convert.ToBoolean(dt["GBPVar"]);
                            }
                            catch { }


                            decimal sekiz_Matrah = 0;
                            decimal onsekiz_matrah = 0;
                            decimal bir_matrah = 0;



                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand cu = new SqlCommand("select (Sum(Miktar*frtFiyat)) from INVOICE_DETAIL WHERE KDV=8 AND fATURAID=" + pID + "", conp))
                                    {
                                        sekiz_Matrah = Convert.ToDecimal(cu.ExecuteScalar());
                                    }
                                }
                            }
                            catch { }


                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand cu = new SqlCommand("select (Sum(Miktar*frtFiyat)) from INVOICE_DETAIL WHERE KDV=18 AND fATURAID=" + pID + "", conp))
                                    {
                                        onsekiz_matrah = Convert.ToDecimal(cu.ExecuteScalar());
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand cu = new SqlCommand("select (Sum(Miktar*frtFiyat)) from INVOICE_DETAIL WHERE KDV=1 AND fATURAID=" + pID + "", conp))
                                    {
                                        bir_matrah = Convert.ToDecimal(cu.ExecuteScalar());
                                    }
                                }
                            }
                            catch { }


                            ds["Toplam %18 KDV Matrahı"] = onsekiz_matrah;
                            ds["Toplam %8 KDV Matrahı"] = sekiz_Matrah;
                            ds["Toplam %1 KDV Matrahı"] = bir_matrah;

                            tbl.Rows.Add(ds);
                        }
                    }
                }
            }

            return tbl;
        }


        public static List<SelectListItem> Cariler(string FirmaID)
        {

            List<SelectListItem> liste = new List<SelectListItem>();


            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand carigetir = new SqlCommand(@"Select distinct Gsm,ID,CariUnvan From Cari where KTipi<>N'BAYİİ' and FirmaID=" + FirmaID, con))
                {
                    using (SqlDataReader dr = carigetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string cari = (dr["CariUnvan"].ToString()).TrimStart() + " " + dr["GSM"].ToString();

                            SelectListItem bt = new SelectListItem
                            {
                                Value = dr["ID"].ToString(),
                                Text = cari,
                            };
                            liste.Add(bt);
                        }
                    }
                }

            }

            return liste;
        }
        public static List<SelectListItem> Kasalar(string FirmaID)
        {
            List<SelectListItem> liste = new List<SelectListItem>();


            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand carigetir = new SqlCommand(@"Select distinct KasaKodu,KasaAdi,ID,ParaBirimi From Kasa where FirmaID=" + FirmaID, con))
                {
                    using (SqlDataReader dr = carigetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string cari = dr["KasaAdi"].ToString();

                            if (dr["KasaKodu"].ToString() != "")
                            {
                                cari = dr["KasaKodu"].ToString() + "-" + dr["KasaAdi"].ToString();

                            }




                            SelectListItem bt = new SelectListItem
                            {
                                Value = dr["ID"].ToString(),
                                Text = cari,
                            };
                            liste.Add(bt);
                        }
                    }
                }

            }

            return liste;
        }
        public static List<SelectListItem> Bankalar(string FirmaID)
        {
            List<SelectListItem> liste = new List<SelectListItem>();


            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand carigetir = new SqlCommand(@"Select distinct BankaKodu,BankaAdi,ID From Banka Where FirmaID=" + FirmaID, con))
                {
                    using (SqlDataReader dr = carigetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string cari = dr["BankaAdi"].ToString();

                            if (dr["BankaKodu"].ToString() != "")
                            {
                                cari = dr["BankaKodu"].ToString() + "-" + dr["BankaAdi"].ToString();

                            }




                            SelectListItem bt = new SelectListItem
                            {
                                Value = dr["ID"].ToString(),
                                Text = cari,
                            };
                            liste.Add(bt);
                        }
                    }
                }

            }

            return liste;
        }

        public static List<SelectListItem> KdvDh()
        {
            List<SelectListItem> KdvDahilh = new List<SelectListItem>();
            SelectListItem bt = new SelectListItem
            {
                Value = "D",
                Text = "Dahil",
            };
            KdvDahilh.Add(bt);
            bt = new SelectListItem
            {
                Value = "H",
                Text = "Hariç",
            };
            KdvDahilh.Add(bt);


            return KdvDahilh;
        }

        public static List<SelectListItem> Depolar(string FirmaID)
        {
            List<SelectListItem> liste = new List<SelectListItem>();


            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand carigetir = new SqlCommand(@"Select distinct ID,DepoAdi From STORE where FirmaID=" + FirmaID.ToString(), con))
                {
                    using (SqlDataReader dr = carigetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string depo = dr["DepoAdi"].ToString();


                            SelectListItem bt = new SelectListItem
                            {
                                Value = dr["ID"].ToString(),
                                Text = depo,
                            };
                            liste.Add(bt);
                        }
                    }
                }

            }

            return liste;
        }

        //

        public static List<SelectListItem> Birimler()
        {
            List<SelectListItem> Birim = new List<SelectListItem>();

            SelectListItem bt = new SelectListItem
            {
                Value = "Adet",
                Text = "Adet",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Kg",
                Text = "Kg",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "M",
                Text = "M",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Lt",
                Text = "Lt",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Gram",
                Text = "Gram",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Mt",
                Text = "Mt",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Yıl",
                Text = "Yıl",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Ay",
                Text = "Ay",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Gün",
                Text = "Gün",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Ay",
                Text = "Ay",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Dakika",
                Text = "Dakika",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Saniye",
                Text = "Saniye",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "Takım",
                Text = "Takım",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "TK",
                Text = "TK",
            };
            Birim.Add(bt);
            bt = new SelectListItem
            {
                Value = "MetreKare",
                Text = "M2",
            };
            Birim.Add(bt);
            return Birim;
        }
        public static List<SelectListItem> Urunler(string FirmaID)
        {
            List<SelectListItem> liste = new List<SelectListItem>();
            //SelectListItem bt1 = new SelectListItem
            //{
            //    Value = "0",
            //    Text = "Seçiniz",

            //};
            //liste.Add(bt1);
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand urungetir = new SqlCommand(@"Select distinct ID,StokKodu,UrunAdi From Stok where FirmaID = " + FirmaID, con))
                {
                    using (SqlDataReader dr = urungetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string urun = dr["UrunAdi"].ToString();

                            if (dr["StokKodu"].ToString() != "")
                            {
                                urun = dr["StokKodu"].ToString() + "-" + dr["UrunAdi"].ToString();
                            }
                            SelectListItem bt = new SelectListItem
                            {
                                Value = dr["ID"].ToString(),
                                Text = urun,

                            };
                            liste.Add(bt);



                        }
                    }
                }
            }


            return liste;
        }
        public static List<SelectListItem> Fiyatlar()
        {

            List<SelectListItem> fiyat = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand urungetir = new SqlCommand(@"Select distinct ID,SatishFiyat From Stok", con))
                {
                    using (SqlDataReader dr = urungetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            string SatisFiyati = dr["SatishFiyat"].ToString();

                            SelectListItem ft = new SelectListItem
                            {
                                Value = dr["ID"].ToString(),
                                Text = SatisFiyati,

                            };
                            fiyat.Add(ft);

                        }
                    }
                }
            }


            return fiyat;
        }
        public static DataTable FirmaBil()

        {
            DataTable FirmaBilgileri = new DataTable();
            FirmaBilgileri.Columns.Add("Firma Adı", typeof(string));
            FirmaBilgileri.Columns.Add("Telefon", typeof(string));
            FirmaBilgileri.Columns.Add("Şehir", typeof(string));
            FirmaBilgileri.Columns.Add("Vergi Dairesi", typeof(string));
            FirmaBilgileri.Columns.Add("Vergi No", typeof(string));
            FirmaBilgileri.Columns.Add("Ülke", typeof(string));
            FirmaBilgileri.Columns.Add("Posta Kodu", typeof(string));
            FirmaBilgileri.Columns.Add("Adres", typeof(string));
            FirmaBilgileri.Columns.Add("SicilNo", typeof(string));



            using (SqlConnection bag = new SqlConnection(strcon))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand servisgetir = new SqlCommand("Select * From COMPANY_DETAIL", bag))
                {
                    using (SqlDataReader dfb = servisgetir.ExecuteReader())
                    {
                        while (dfb.Read())
                        {

                            FirmaBilgileri.Rows.Add(dfb["FirmaAdi"].ToString(),
                                dfb["Telefon"].ToString(),
                                dfb["Sehir"].ToString(),
                                dfb["VergiDairesi"].ToString(),
                                dfb["VergiNo"].ToString(),
                                dfb["Ulke"].ToString(),
                                dfb["PostaKodu"].ToString(),
                                dfb["Adres"].ToString(),
                                dfb["SicilNo"].ToString());

                        }
                    }
                }
            }

            return FirmaBilgileri;
        }
        public static void Siradaki(string GetNumber, string table, string alan, string FirmaID)
        {
            using (SqlConnection con = new System.Data.SqlClient.SqlConnection(AyarMetot.strcon))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                if (table == "Teklif")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 TeklifNo from OFFER Where FirmaID=" + FirmaID + " Order by ID desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "TK001";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "TK001";
                        }
                    }
                }
                if (table == "Maas")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 IslemNO from SALARYPERSON Where FirmaID=" + FirmaID + " Order by ID desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "MS0001";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "TK001";
                        }
                    }
                }
                if (table == "Siparis")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 SiparisNo from ORDERS Where FirmaID=" + FirmaID + " Order by ID desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "SP001";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "SP001";
                        }
                    }
                }
                if (table == "Stok")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 STokKodu from STok Where FirmaID=" + FirmaID + " Order by ID desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "UR000";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "UR000";
                        }
                    }
                }
                if (table == "Cari")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 FirmaKodu from Cari where FirmaID=" + FirmaID + " and KTipi != N'BAYİİ' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "CR000";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "CR000";
                        }
                    }
                }
                if (table == "OFFER")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 TeklifNo from OFFER Order by CONVERT(datetime,IslemTarihi) desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "TEK001";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by CONVERT(datetime,IslemTarihi) desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "TEK001";
                        }
                    }
                }
                if (table == "ORDERS")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 SiparisNo from ORDERS Order by CONVERT(datetime,IslemTarihi) desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "SIP001";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by CONVERT(datetime,IslemTarihi) desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "SIP001";
                        }
                    }
                }
                if (table == "DIRECT")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy; select top 1 UretimEmriNo from PRODUCE_DIRECTION Order by CONVERT(datetime,UretimTarihi) desc,ID  desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "SER001";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy; select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by CONVERT(datetime,Tarih) desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "SER001";
                        }
                    }
                }
                if (table == "TECHNICAL")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy; select top 1 ServisIslemNo from TECHNICAL Where FirmaID=" + FirmaID + " Order by CONVERT(datetime,Tarih) DESC,ID desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "SER001";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy; select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by CONVERT(datetime,Tarih) desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "SER001";
                        }
                    }
                }
                if (table == "Fatura")
                {
                    string nb = "";


                    if (alan == "Satış")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy; Select Top 1 FaturaNo from SiradakiFatura Where FirmaID=" + FirmaID + "  order by ID desc ,FatID desc ", con))
                            {

                                nb = (command.ExecuteScalar()).ToString();

                            }
                        }
                        catch
                        {
                            GetNumber = "00000";

                        }

                        if (nb == "") nb = "00000";
                    }
                    else if (alan == "Alış")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy ;select top 1 FaturaNo from INVOICE where (FaturaType = N'Alım Faturası'  )   order by CONVERT(datetime,FaturaTarihi) desc,ID desc", con))
                            {

                                nb = (command.ExecuteScalar()).ToString();

                            }
                        }
                        catch
                        {
                            GetNumber = "00000";
                        }
                    }
                    else if (alan == "Alım")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy ;select top 1 FaturaNo from INVOICE where FaturaType = N'Alım İrsaliyesi'  order by CONVERT(datetime,FaturaTarihi) desc,ID desc", con))
                            {

                                nb = (command.ExecuteScalar()).ToString();

                            }
                        }
                        catch
                        {
                            GetNumber = "00000";
                        }
                    }
                    else if (alan == "Hizmet Alım")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy ;select top 1 FaturaNo from INVOICE where FaturaType = N'Hizmet Alım Faturası'   order by CONVERT(datetime,FaturaTarihi) desc,ID desc", con))
                            {

                                nb = (command.ExecuteScalar()).ToString();

                            }
                        }
                        catch
                        {
                            GetNumber = "00000";
                        }
                    }
                    else if (alan == "AFF")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy ;select top 1 FaturaNo from INVOICE where FaturaType = N'Alınan Fiyat Farkı Faturası'   order by CONVERT(datetime,FaturaTarihi) desc,ID desc", con))
                            {

                                nb = (command.ExecuteScalar()).ToString();

                            }
                        }
                        catch
                        {
                            GetNumber = "00000";
                        }
                    }
                    else if (alan == "Satış İade")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy ;select top 1 FaturaNo from INVOICE where FaturaType = N'Satış İade Faturası'   order by CONVERT(datetime,FaturaTarihi) desc,ID desc", con))
                            {

                                nb = (command.ExecuteScalar()).ToString();

                            }
                        }
                        catch
                        {
                            GetNumber = "00000";
                        }
                    }
                    else if (alan == "Proforma Satış")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy ;select top 1 FaturaNo from INVOICE where FaturaType = N'Proforma Satış Faturası'   order by CONVERT(datetime,FaturaTarihi) desc,ID desc", con))
                            {

                                nb = (command.ExecuteScalar()).ToString();

                            }
                        }
                        catch
                        {
                            GetNumber = "00000";
                        }
                    }
                    else if (alan == "Proforma Alım")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy ;select top 1 FaturaNo from INVOICE where FaturaType = N'Proforma Alım Faturası'   order by CONVERT(datetime,FaturaTarihi) desc,ID desc", con))
                            {

                                nb = (command.ExecuteScalar()).ToString();

                            }
                        }
                        catch
                        {
                            GetNumber = "00000";
                        }
                    }
                    else if (alan == "Sevk")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("set dateformat dmy ;select top 1 FaturaNo from INVOICE where (FaturaType = N'Sevk İrsaliyesi')   order by CONVERT(datetime,FaturaTarihi) desc,ID desc", con))
                            {

                                nb = (command.ExecuteScalar()).ToString();

                            }
                        }
                        catch
                        {
                            GetNumber = "SI0000";
                        }
                    }

                    GetNumber = nb;
                }
                if (table == "BANKA")
                {
                    string bnkkod = "";
                    using (SqlCommand cusername = new SqlCommand("SELECT MAx(ID)+1 FROM Banka Where FirmaID=" + FirmaID, con))
                    {

                        if (cusername.ExecuteScalar() != DBNull.Value) bnkkod = "BNK0" + cusername.ExecuteScalar().ToString();
                        else bnkkod = "BNK001";
                    }

                    GetNumber = bnkkod;



                }
                if (table == "KASA")
                {
                    string bnkkod = "";
                    using (SqlCommand cusername = new SqlCommand("SELECT MAx(ID)+1 FROM Kasa Where FirmaID=" + FirmaID, con))
                    {

                        if (cusername.ExecuteScalar() != DBNull.Value) bnkkod = "KS0" + cusername.ExecuteScalar().ToString();
                        else bnkkod = "KS001";
                    }

                    GetNumber = bnkkod;



                }
                if (table == "DEKONT")
                {
                    string bnkkod = "";
                    using (SqlCommand cusername = new SqlCommand("SELECT MAx(ID)+1 FROM Dekont Where FirmaID =" + FirmaID, con))
                    {

                        if (cusername.ExecuteScalar() != DBNull.Value) bnkkod = "DK0" + cusername.ExecuteScalar().ToString();
                        else bnkkod = "DK001";
                    }

                    GetNumber = bnkkod;
                }
                if (table == "DEPO")
                {
                    string bnkkod = "";
                    using (SqlCommand cusername = new SqlCommand("SELECT MAx(ID)+1 FROM STORE where FirmaID=" + FirmaID, con))
                    {

                        if (cusername.ExecuteScalar() != DBNull.Value) bnkkod = "DP0" + cusername.ExecuteScalar().ToString();
                        else bnkkod = "DP001";
                    }

                    GetNumber = bnkkod;

                }
                if (table == "MASRAF")
                {
                    string bnkkod = "";
                    using (SqlCommand cusername = new SqlCommand("SELECT MAx(ID)+1 FROM CASH_PAY Where (IslemTipi = 'E' or IslemTipi = 'A'  or IslemTipi = 'M' or IslemTipi = 'B') and FirmaID=" + FirmaID, con))
                    {

                        if (cusername.ExecuteScalar() != DBNull.Value) bnkkod = "MF0" + cusername.ExecuteScalar().ToString();
                        else bnkkod = "MF001";
                    }

                    GetNumber = bnkkod;



                }
                if (table == "TAHSILAT")
                {
                    string bnkkod = "";
                    using (SqlCommand cusername = new SqlCommand("SELECT MAx(ID)+1 FROM CASH_PAY Where (IslemTipi = 'T' or IslemTipi = 'O'  or IslemTipi = 'G' or IslemTipi = 'H') and FirmaID =" + FirmaID, con))
                    {

                        if (cusername.ExecuteScalar() != DBNull.Value) bnkkod = "TH0" + cusername.ExecuteScalar().ToString();
                        else bnkkod = "TH001";
                    }

                    GetNumber = bnkkod;



                }
                if (table == "Bayii")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 FirmaKodu from Cari where Ktipi=N'BAYİİ' and FirmaID =" + FirmaID + " Order by ID desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "BY000";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "BY000";
                        }
                    }
                }
                if (table == "CEKSENET")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 IslemNo from PAYROLL where FirmaID = " + FirmaID + " Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "BRK000";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "BRK000";
                        }
                    }
                }
                if (table == "CEK")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 PortfoyNo from PAYROLL_CS Where FirmaID = " + FirmaID + " Order by ID desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "PF000";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "PF000";
                        }
                    }
                }
                if (table == "Transfer")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 IslemKodu from STORE_PROCESS Where FirmaID = " + FirmaID + " Order by ID desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "DP001";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "DP001";
                        }
                    }
                }
                if (table == "Ozel")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 IslemNO from OzelKod Where FirmaID=" + FirmaID + " Order by ID desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "OK001";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "TK001";
                        }
                    }
                }
                if (table == "Perakende")
                {
                    if (GetNumber == "")
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select top 1 IslemNo from RETAIL Where FirmaID=" + FirmaID + " Order by ID desc", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "PRS0001";
                        }
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select Top 1 " + alan + " from " + table + " where " + alan + " like '" + GetNumber + "%' Order by ID desc ", con))
                            {
                                GetNumber = command.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            GetNumber = "PRS0001";
                        }
                    }
                }









                char last = '1', last1 = '1', last2 = '1', last3 = '1', last4 = '1', last5 = '1', last6 = '1', last7 = '1';
                string text = "", text1 = "", text2 = "", text3 = "", text4 = "", text5 = "", text7 = "";
                if (GetNumber.Length != 0)
                {
                    if (GetNumber.Length > 0)
                    {
                        last = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 1, 1));
                        text = GetNumber.Substring(GetNumber.Length - 1, 1);
                    }

                    if (GetNumber.Length > 1)
                    {
                        last1 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 2, 1));
                        text1 = GetNumber.Substring(GetNumber.Length - 2, 2);
                    }

                    if (GetNumber.Length > 2)

                    {
                        last2 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 3, 1));
                        text2 = GetNumber.Substring(GetNumber.Length - 3, 3);
                    }

                    if (GetNumber.Length > 3)
                    {
                        last3 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 4, 1));
                        text3 = GetNumber.Substring(GetNumber.Length - 4, 4);
                    }

                    if (GetNumber.Length > 4)
                    {
                        last4 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 5, 1));
                        text4 = GetNumber.Substring(GetNumber.Length - 5, 5);
                    }

                    if (GetNumber.Length > 5)
                    {
                        last5 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 6, 1));
                        text5 = GetNumber.Substring(GetNumber.Length - 6, 6);
                    }

                    if (GetNumber.Length > 6)
                    {
                        last7 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 7, 1));
                        text7 = GetNumber.Substring(GetNumber.Length - 7, 7);
                    }

                    if (GetNumber.Length > 7)
                    {
                        //  last4 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 8, 1));
                    }

                    if ((!Char.IsNumber(last1) && IsNumeric(text) && text == "9") || GetNumber == "9")

                    {
                        GetNumber = GetNumber.Substring(0, GetNumber.Length - 1) + "10";
                        GetNumara = GetNumber;
                        con.Close();
                        return;
                    }
                    else if (!Char.IsNumber(last2) && IsNumeric(text1) && text1 == "99" || GetNumber == "99")

                    {
                        GetNumber = GetNumber.Substring(0, GetNumber.Length - 2) + "100";
                        GetNumara = GetNumber;
                        con.Close();
                        return;
                    }
                    else if (!Char.IsNumber(last3) && IsNumeric(text2) && text2 == "999" || GetNumber == "999")

                    {
                        GetNumber = GetNumber.Substring(0, GetNumber.Length - 3) + "1000";
                        GetNumara = GetNumber;
                        con.Close();
                        return;
                    }
                    else if (!Char.IsNumber(last4) && IsNumeric(text3) && text3 == "9999" || GetNumber == "9999")

                    {
                        GetNumber = GetNumber.Substring(0, GetNumber.Length - 4) + "10000";
                        GetNumara = GetNumber;
                        con.Close();
                        return;
                    }
                    else if (!Char.IsNumber(last5) && IsNumeric(text4) && text4 == "99999" || GetNumber == "99999")

                    {
                        GetNumber = GetNumber.Substring(0, GetNumber.Length - 5) + "100000";
                        GetNumara = GetNumber;
                        con.Close();
                        return;
                    }
                    else if (!Char.IsNumber(last6) && IsNumeric(text5) && text5 == "999999" || GetNumber == "999999")

                    {
                        GetNumber = GetNumber.Substring(0, GetNumber.Length - 6) + "1000000";
                        GetNumara = GetNumber;
                        con.Close();
                        return;
                    }
                    else if (!Char.IsNumber(last) && IsNumeric(last1) && last1.ToString() == "9" && last.ToString().ToLower() == "z" && GetNumber.Length > 2)

                    {
                        char nextChar = (char)((int)last2 + 1);
                        if (Char.IsLower(last))
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 3) + nextChar.ToString() + "0a";
                            GetNumara = GetNumber;
                        }
                        else
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 3) + nextChar.ToString() + "0A";
                            GetNumara = GetNumber;
                        }
                        con.Close();
                        return;
                    }
                    else if (!Char.IsNumber(last) && !Char.IsNumber(last1) && last2.ToString() == "9" && text1.ToString().ToLower() == "zz" && GetNumber.Length > 3)

                    {
                        char nextChar = (char)((int)last3 + 1);
                        if (Char.IsLower(last))
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 4) + nextChar.ToString() + "0aa";
                            GetNumara = GetNumber;
                        }
                        else
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 4) + nextChar.ToString() + "0AA";
                            GetNumara = GetNumber;
                        }
                        con.Close();
                        return;
                    }
                    else if (IsNumeric(last1) && last.ToString().ToLower() == "z" && GetNumber.Length > 1)

                    {
                        char nextChar = (char)((int)last1 + 1);
                        if (Char.IsLower(last))
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 2) + nextChar.ToString() + "a";
                            GetNumara = GetNumber;
                        }
                        else
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 2) + nextChar.ToString() + "A";
                            GetNumara = GetNumber;
                        }
                        con.Close();
                        return;
                    }
                    else if (IsNumeric(last2) && text1.ToLower() == "zz" && GetNumber.Length > 2)

                    {
                        char nextChar = (char)((int)last2 + 1);
                        if (Char.IsLower(last))
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 3) + nextChar.ToString() + "aa";
                            GetNumara = GetNumber;
                        }
                        else
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 3) + nextChar.ToString() + "AA";
                            GetNumara = GetNumber;
                        }
                        con.Close();
                        return;
                    }

                    if (Char.IsNumber(last))
                    {
                        var yenikarakter = "";

                        var str = GetNumber;

                        bool son = false, son1 = false, son2 = false, son3 = false, son4 = false, son5 = false, son6 = false, son7 = false;

                        char eskikarakter = str[str.Length - 1];
                        if (eskikarakter.ToString() == "9")
                        {
                            eskikarakter = str[str.Length - 2];

                            if (eskikarakter.ToString() == "9")
                            {
                                eskikarakter = str[str.Length - 3];
                                if (eskikarakter.ToString() == "9")
                                {
                                    eskikarakter = str[str.Length - 4];
                                    if (eskikarakter.ToString() == "9")
                                    {
                                        eskikarakter = str[str.Length - 5];
                                        if (eskikarakter.ToString() == "9")
                                        {
                                            eskikarakter = str[str.Length - 6];
                                            if (eskikarakter.ToString() == "9")
                                            {
                                                if (eskikarakter.ToString() == "9")
                                                {
                                                    son6 = true;
                                                    if (GetNumber.Length > 7)
                                                        eskikarakter = str[str.Length - 8];
                                                }
                                                else
                                                {
                                                    son5 = true;
                                                    if (GetNumber.Length > 6)
                                                        eskikarakter = str[str.Length - 7];
                                                }
                                            }
                                            else
                                            {
                                                son4 = true;
                                                if (GetNumber.Length > 5)
                                                    eskikarakter = str[str.Length - 6];
                                            }
                                        }
                                        else
                                        {
                                            son3 = true;
                                            eskikarakter = str[str.Length - 5];
                                        }
                                    }
                                    else
                                    {
                                        son2 = true;
                                        eskikarakter = str[str.Length - 4];
                                    }
                                }
                                else
                                {
                                    son1 = true;
                                    eskikarakter = str[str.Length - 3];
                                }
                            }
                            else
                            {
                                son = true;
                            }
                        }
                        else
                        {
                            eskikarakter = str[str.Length - 1];
                        }

                        char nextChar = (char)((int)eskikarakter + 1);

                        yenikarakter = nextChar.ToString();

                        if (son == true)
                        {
                            if (char.IsDigit(last1))
                                GetNumber = GetNumber.Substring(0, GetNumber.Length - 2) + yenikarakter.ToString() + 0.ToString();
                            else
                            {
                                GetNumber = GetNumber.Substring(0, GetNumber.Length - 1) + Convert.ToInt32(last).ToString();
                            }
                        }
                        else if (son1 == true)
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 3) + yenikarakter.ToString() + "00";
                        }
                        else if (son2 == true)
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 4) + yenikarakter.ToString() + "000";
                        }
                        else if (son3 == true)
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 5) + yenikarakter.ToString() + "0000";
                        }
                        else if (son4 == true)
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 6) + yenikarakter.ToString() + "00000";
                        }
                        else if (son5 == true)
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 7) + yenikarakter.ToString() + "000000";
                        }
                        else if (son6 == true)
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 8) + yenikarakter.ToString() + "0000000";
                        }
                        else if (son7 == true)
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 9) + yenikarakter.ToString() + "00000000";
                        }
                        else
                        {
                            GetNumber = GetNumber.Substring(0, GetNumber.Length - 1) + yenikarakter.ToString();
                        }

                        GetNumara = GetNumber;
                        con.Close();
                    }
                    else
                    {
                        if (GetNumber.Length != 0)
                        {
                            if (GetNumber.Length > 0)
                                last = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 1, 1));

                            if (GetNumber.Length > 1)
                                last1 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 2, 1));

                            if (GetNumber.Length > 2)

                                last2 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 3, 1));

                            if (GetNumber.Length > 3)
                                last3 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 4, 1));

                            if (GetNumber.Length > 4)
                                last4 = Convert.ToChar(GetNumber.Substring(GetNumber.Length - 5, 1));

                            var yenikarakter = "";
                            var str = GetNumber;
                            char eskikarakter = str[str.Length - 1];
                            bool yeni = false;
                            if (eskikarakter.ToString().ToLower() == "z")
                            {
                                if (str.Length > 1)
                                    eskikarakter = str[str.Length - 2];
                                yeni = true;
                            }
                            else
                            {
                                eskikarakter = str[str.Length - 1];
                            }
                            char nextChar = (char)((int)eskikarakter + 1);

                            if (yeni == true)
                            {
                                yenikarakter = nextChar.ToString();
                            }
                            else
                            {
                                yenikarakter = nextChar.ToString();
                            }

                            if (yeni == true)
                            {
                                if (Char.IsLower(last1))
                                {
                                    if (GetNumber.Length > 2)
                                        GetNumber = GetNumber.Substring(0, GetNumber.Length - 2) + yenikarakter.ToString() + "a";
                                }
                                else
                                {

                                    if (GetNumber.Length > 1)
                                        GetNumber = GetNumber.Substring(0, GetNumber.Length - 2) + yenikarakter.ToString() + "A";
                                }
                            }
                            else
                            {
                                GetNumber = GetNumber.Substring(0, GetNumber.Length - 1) + yenikarakter.ToString();
                            }
                        }
                        GetNumara = GetNumber;
                        con.Close();
                    }
                }
            }
        }
        public static string YeniNumara(string Tablo)
        {

            using (SqlConnection con1 = new SqlConnection(AyarMetot.strcon))
            {
                con1.Open();


                try
                {
                    using (SqlCommand peryetki = new SqlCommand("SELECT max(ID) From " + Tablo + "", con1))
                    {
                        return peryetki.ExecuteScalar().ToString();
                    }
                }
                catch
                {
                    return "001";
                }


            }

        }
        private static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public static string StokBilgiCek(int ID, string AD)
        {
            try
            {
                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (SqlCommand d6 = new SqlCommand("Select " + AD + " From STOK where ID='" + ID + "'", conp))
                    {
                        return d6.ExecuteScalar().ToString();
                    }
                }
            }
            catch { return ""; }
        }
        public static DataTable DepoIslemDetayBil(string pID)
        {

            DataTable dep = new DataTable();

            dep.Columns.Add("Ürün Adı", typeof(string));
            dep.Columns.Add("Ürün Kodu", typeof(string));
            dep.Columns.Add("Ürün Türü", typeof(string));
            dep.Columns.Add("Barkod", typeof(string));
            dep.Columns.Add("Ürün Fiyatı", typeof(string));
            dep.Columns.Add("Ürün Tutarı", typeof(string));
            dep.Columns.Add("Ürün Birimi", typeof(string));
            dep.Columns.Add("Para Birimi", typeof(string));
            dep.Columns.Add("Ürün Miktarı", typeof(string));
            dep.Columns.Add("İşlem Tipi", typeof(string));
            dep.Columns.Add("Marka", typeof(string));
            dep.Columns.Add("Grubu", typeof(string));
            dep.Columns.Add("SeriNo", typeof(string));

            using (SqlConnection bag = new SqlConnection(strcon))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand servisgetir = new SqlCommand("Select * From STORE_PROCESS_DETAIL WHERE DEPoIslemID='" + pID + "'", bag))
                {
                    using (SqlDataReader da = servisgetir.ExecuteReader())
                    {
                        while (da.Read())
                        {
                            DataRow ds = dep.NewRow();

                            int urun = Convert.ToInt32(da["UrunID"]);
                            //ds["Sıra NO"] = (i + 1).ToString();
                            ds["Ürün Adı"] = AyarMetot.StokBilgiCek(urun, "UrunAdi").ToString().Replace("İ", "I").Replace("Ğ", "G");
                            ds["Marka"] = StokBilgiCek(urun, "Marka");
                            ds["Grubu"] = StokBilgiCek(urun, "Grubu");
                            ds["Ürün Kodu"] = StokBilgiCek(urun, "StokKodu").ToString().Replace("İ", "I").Replace("Ğ", "G");
                            ds["Ürün Türü"] = StokBilgiCek(urun, "UrunTuru");
                            ds["Ürün Birimi"] = StokBilgiCek(urun, "Birimi");
                            ds["Barkod"] = StokBilgiCek(urun, "BARKOD");
                            ds["Ürün Miktarı"] = Convert.ToDecimal(da["UrunMiktar"]).ToString("#,##0.00");
                            ds["Ürün Fiyatı"] = Convert.ToDecimal(da["UrunFiyat"]).ToString("#,##0.00");
                            ds["Ürün Tutarı"] = (Convert.ToDecimal(da["UrunFiyat"]) * Convert.ToDecimal(da["UrunMiktar"])).ToString("#,##0.00");
                            ds["Para Birimi"] = da["paraBirimi"].ToString();
                            ds["SeriNo"] = da["SeriNo"].ToString();
                            dep.Rows.Add(ds);

                        }
                    }
                }
            }

            return dep;
        }
        public static DataTable DepoIslemBil(string pID)
        {

            DataTable dep = new DataTable();
            dep.Columns.Add("İşlem Numarası", typeof(string));
            dep.Columns.Add("İşlem Tarihi", typeof(string));
            dep.Columns.Add("Personel", typeof(string));
            dep.Columns.Add("Alan Depo", typeof(string));
            dep.Columns.Add("Gönderen Depo", typeof(string));
            dep.Columns.Add("Tutar", typeof(string));
            dep.Columns.Add("Açıklama", typeof(string));
            dep.Columns.Add("İşlem Tipi", typeof(string));
            dep.Columns.Add("SicilNo", typeof(string));


            using (SqlConnection bag = new SqlConnection(strcon))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand servisgetir = new SqlCommand("Select * From STORE_PROCESS WHERE ID='" + pID + "'", bag))
                {
                    using (SqlDataReader da = servisgetir.ExecuteReader())
                    {
                        while (da.Read())
                        {
                            int PersonelID = Convert.ToInt32(da["PersonelID"]);
                            int gonderen = Convert.ToInt32(da["GonderenDepoID"]);
                            int alan = Convert.ToInt32(da["AlanDepoID"]);
                            string islemtip = "";

                            DataRow ds = dep.NewRow();
                            ds["İşlem Numarası"] = da["IslemKodu"].ToString();
                            ds["İşlem Tarihi"] = Convert.ToDateTime(da["IslemTarih"].ToString()).ToShortDateString();
                            ds["Personel"] = SeciliPersonelBilgiCek("Adi + Soyadi", PersonelID.ToString());

                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();

                                    using (SqlCommand d6 = new SqlCommand("Select DepoAdi From STORE where ID='" + alan + "'", conp))
                                    {
                                        ds["Alan Depo"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }


                            try
                            {
                                using (SqlConnection conp = new SqlConnection(AyarMetot.strcon))

                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();

                                    using (SqlCommand d6 = new SqlCommand("Select DepoAdi From STORE where ID='" + gonderen + "'", conp))
                                    {
                                        ds["Gönderen Depo"] = d6.ExecuteScalar();
                                    }
                                }
                            }
                            catch { }


                            ds["Tutar"] = Convert.ToDecimal(da["Tutar"]).ToString("#,##0.00");
                            ds["Açıklama"] = da["Aciklama"].ToString();
                            if (alan == -1 && gonderen != -1) islemtip = "DEPODAN ÜRÜN ÇIKIŞI";
                            if (alan != -1 && gonderen == -1) islemtip = "DEPOYA ÜRÜN GİRİŞİ";
                            if (alan != -1 && gonderen != -1) islemtip = "DEPOLAR ARASI ÜRÜN AKTARIMI";
                            ds["İşlem Tipi"] = islemtip;

                            dep.Rows.Add(ds);
                        }
                    }
                }
            }

            return dep;
        }
        public static DataTable CariBil(int cariID, string ay, string year, string ayadi, string PB)
        {


            try
            {
                using (SqlConnection con = new System.Data.SqlClient.SqlConnection(strcon))
                {

                    if (con.State == ConnectionState.Closed) con.Open();


                    using (SqlConnection con2 = new System.Data.SqlClient.SqlConnection(strcon))
                    {

                        if (con2.State == ConnectionState.Closed) con2.Open();

                        using (SqlCommand csay = new SqlCommand("select ID,ParaBirimi From Cari where ID='" + cariID + "'", con2))
                        {
                            using (SqlDataReader rdr = csay.ExecuteReader())
                            {

                                while (rdr.Read())
                                {

                                    using (SqlCommand calis = new SqlCommand("BakiyeKontrolCari", con))
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
            }
            catch { }

            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("Ünvanı", typeof(string));
            tbl.Columns.Add("Şehir", typeof(string));
            tbl.Columns.Add("Firma Kodu", typeof(string));
            tbl.Columns.Add("Vergi Dairesi", typeof(string));
            tbl.Columns.Add("Vergi Numarası", typeof(string));
            tbl.Columns.Add("Yetkili Kişi", typeof(string));
            /*8*/
            tbl.Columns.Add("TCKN", typeof(string));
            tbl.Columns.Add("Adres", typeof(string));
            tbl.Columns.Add("İlçe", typeof(string));
            tbl.Columns.Add("Ülke", typeof(string));
            tbl.Columns.Add("Sabit TEL", typeof(string));
            tbl.Columns.Add("GSM", typeof(string));
            tbl.Columns.Add("Faks", typeof(string));
            tbl.Columns.Add("E-Posta", typeof(string));
            /*16*/
            tbl.Columns.Add("WebSite", typeof(string));
            tbl.Columns.Add("Posta Kodu", typeof(string));
            tbl.Columns.Add("Alacak Bakiyesi", typeof(string));
            tbl.Columns.Add("Borç Bakiyesi", typeof(string));
            tbl.Columns.Add("Net Bakiye", typeof(string));
            tbl.Columns.Add("Net Bakiye Yazıyla", typeof(string));
            tbl.Columns.Add("Cari Para Birimi", typeof(string));
            tbl.Columns.Add("Cari Net Bakiye(A/B)", typeof(string));
            tbl.Columns.Add("Bağkur No", typeof(string));
            tbl.Columns.Add("BA Bakiye", typeof(string));
            tbl.Columns.Add("BS Bakiye", typeof(string));
            tbl.Columns.Add("BA BelgeSayisi", typeof(string));
            tbl.Columns.Add("BS BelgeSayisi", typeof(string));
            tbl.Columns.Add("Donem Ay", typeof(string));
            tbl.Columns.Add("Donem Yıl", typeof(string));
            tbl.Columns.Add("MTemsilcisi", typeof(string));
            tbl.Columns.Add("Cari Net Bakiye(A/B)(EUR)", typeof(string));
            tbl.Columns.Add("Cari Net Bakiye(A/B)(USD)", typeof(string));
            tbl.Columns.Add("Seçili PB", typeof(string));
            tbl.Columns.Add("Cari Net Bakiye(A/B)_Tarih", typeof(string));
            string donemay = ayadi;
            string donemyil = year;

            string para = PB;



            using (SqlConnection bag = new SqlConnection(strcon))
            {
                if (bag.State == ConnectionState.Closed) bag.Open();
                using (SqlCommand servisgetir = new SqlCommand("Select * From Cari where ID='" + cariID + "'", bag))
                {
                    using (SqlDataReader dfb = servisgetir.ExecuteReader())
                    {
                        while (dfb.Read())
                        {
                            string netyaziyla = "", BocAB = "", BorcABEUR = "", BorcABUSD = "";
                            string Temsilcisi = "";

                            string borc = "0", alacak = "0", bakiye = "0";
                            if (PB == "Tümü") para = dfb["paraBirimi"].ToString();

                            try
                            {
                                System.IO.File.WriteAllText(Path.Combine(@"C:\Users\ilhan\AppData\Local\Sayazilim", "sonuç.xml"), para.ToString());
                            }
                            catch
                            { }

                            using (SqlConnection bag1 = new SqlConnection(strcon))
                            {
                                bag1.Open();
                                using (SqlCommand getir = new SqlCommand("Select alacakB,borcB From BALANCE where cariID='" + cariID + "' and ParaBirimi='" + para + "'", bag1))
                                {
                                    using (SqlDataReader cb = getir.ExecuteReader())
                                    {
                                        while (cb.Read())
                                        {

                                            if (para == "TL")
                                            {
                                                netyaziyla = textify(Math.Abs(Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])));

                                            }
                                            else if (para == "USD" && AyarMetot.PB_Short == "TL")
                                            {
                                                netyaziyla = textifyDolar(Math.Abs(Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])));

                                            }
                                            else if (para == "EUR" && AyarMetot.PB_Short == "TL")
                                            {
                                                netyaziyla = textifyEuro(Math.Abs(Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])));

                                            }
                                            else
                                            {
                                                netyaziyla = textify(Math.Abs(Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])));

                                            }

                                            if (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]) < 0)
                                                BocAB = (-(Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]))).ToString("N2").ToString() + " (B)";
                                            else if (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]) > 0)
                                                BocAB = (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])).ToString("N2").ToString() + " (A)";
                                            else
                                                BocAB = (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])).ToString("N2").ToString() + " ( )";



                                            alacak = Convert.ToDecimal(cb["alacakB"]).ToString();
                                            borc = Convert.ToDecimal(cb["borcB"]).ToString();
                                            bakiye = (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])).ToString();



                                        }
                                    }
                                }


                            }


                            using (SqlConnection bag1 = new SqlConnection(strcon))
                            {
                                bag1.Open();
                                using (SqlCommand getir = new SqlCommand("Select alacakB,borcB From BALANCE where cariID='" + cariID + "' and ParaBirimi='EUR'", bag1))
                                {
                                    using (SqlDataReader cb = getir.ExecuteReader())
                                    {
                                        while (cb.Read())
                                        {

                                            if (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]) < 0)
                                                BorcABEUR = (-(Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]))).ToString("N2").ToString() + " (B)";
                                            else if (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]) > 0)
                                                BorcABEUR = (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])).ToString("N2").ToString() + " (A)";
                                            else
                                                BorcABEUR = (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])).ToString("N2").ToString() + " ( )";





                                        }
                                    }
                                }
                            }
                            using (SqlConnection bag1 = new SqlConnection(strcon))
                            {
                                bag1.Open();
                                using (SqlCommand getir = new SqlCommand("Select alacakB,borcB From BALANCE where cariID='" + cariID + "' and ParaBirimi='USD'", bag1))
                                {
                                    using (SqlDataReader cb = getir.ExecuteReader())
                                    {
                                        while (cb.Read())
                                        {



                                            if (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]) < 0)
                                                BorcABUSD = (-(Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]))).ToString("N2").ToString() + " (B)";
                                            else if (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"]) > 0)
                                                BorcABUSD = (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])).ToString("N2").ToString() + " (A)";
                                            else
                                                BorcABUSD = (Convert.ToDecimal(cb["alacakB"]) - Convert.ToDecimal(cb["borcB"])).ToString("N2").ToString() + " ( )";

                                        }
                                    }
                                }
                            }


                            decimal BA = 0;
                            decimal BS = 0;

                            int BABelge = 0;
                            int BSBelge = 0;
                            string tarihcek = "";
                            if (ay != "Tümü")
                            {
                                DateTime date1 = new DateTime(Convert.ToInt32(year), Convert.ToInt32(ay), 1, 0, 0, 0);
                                DateTime date2 = new DateTime(Convert.ToInt32(year), Convert.ToInt32(ay), date1.AddMonths(1).AddDays(-1).Day, 23, 59, 0);


                                ay = "  and  (CONVERT(datetime, FaturaTarihi) BETWEEN convert(datetime, '" + date1.ToString("dd.MM.yyyy") + " 00:00:00.000" + "') AND convert(datetime, '" + date2.ToString("dd.MM.yyyy") + " 23:59:59.000" + "'))";
                                tarihcek = "  and  (CONVERT(datetime, IslemTarihi) BETWEEN convert(datetime, '" + date2.AddYears(-10).ToString("dd.MM.yyyy") + " 00:00:00.000" + "') AND convert(datetime, '" + date2.ToString("dd.MM.yyyy") + " 23:59:59.000" + "'))";

                            }
                            else
                            {
                                ay = "";
                                tarihcek = "";
                            }



                            try
                            {
                                using (SqlConnection conp = new SqlConnection(strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("set dateformat dmy; Select Sum(NetToplam*Fkuru) From INVOICE where CariID='" + cariID + "'" + ay + " and (FaturaType=N'Alım Faturası' or FaturaType=N'Satış İade Faturası' or FaturaType=N'Alınan Fiyat Farkı Faturası')", conp))
                                    {
                                        BA = Convert.ToDecimal(d6.ExecuteScalar());
                                    }
                                }
                            }
                            catch { }



                            try
                            {
                                using (SqlConnection conp = new SqlConnection(strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("set dateformat dmy; Select Sum(NetToplam*Fkuru) From INVOICE where CariID='" + cariID + "'" + ay + " and (FaturaType=N'Satış Faturası' or FaturaType=N'Alım İade Faturası' or FaturaType=N'Verilen Fiyat Farkı Faturası')", conp))
                                    {
                                        BS = Convert.ToDecimal(d6.ExecuteScalar());
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection conp = new SqlConnection(strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("set dateformat dmy; Select Count(ID) From INVOICE where CariID='" + cariID + "'" + ay + " and (FaturaType=N'Alım Faturası' or FaturaType=N'Satış İade Faturası' or FaturaType=N'Alınan Fiyat Farkı Faturası')", conp))
                                    {
                                        BABelge = Convert.ToInt32(d6.ExecuteScalar());
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                using (SqlConnection conp = new SqlConnection(strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("set dateformat dmy; Select Count(ID) From INVOICE where CariID='" + cariID + "'" + ay + " and (FaturaType=N'Satış Faturası' or FaturaType=N'Alım İade Faturası' or FaturaType=N'Verilen Fiyat Farkı Faturası')", conp))
                                    {
                                        BSBelge = Convert.ToInt32(d6.ExecuteScalar());
                                    }
                                }
                            }
                            catch { }




                            try
                            {
                                using (SqlConnection conp = new SqlConnection(strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand("Select Adi+' '+Soyadi From Personel where ID='" + dfb["MusteriTemsilcisi"] + "'", conp))
                                    {
                                        Temsilcisi = d6.ExecuteScalar().ToString();
                                    }
                                }
                            }


                            catch { }


                            decimal TarihBakiye = 0;


                            string query = "set dateformat dmy ;select (select Coalesce(sum(H.Tutar*H.[İşlem Kuru]),0) from HareketlerListesi H where H.DugunYapildi=1 and H.BA='(B)'and '" + cariID + "'=H.CariID " + tarihcek + "  )- " +
                            " (select Coalesce(sum(H.Tutar * H.[İşlem Kuru]), 0) from HareketlerListesi H where H.DugunYapildi = 1 and H.BA = '(A)' and '" + cariID + "' = H.CariID " + tarihcek + " )";
                            try
                            {
                                using (SqlConnection conp = new SqlConnection(strcon))
                                {
                                    if (conp.State == ConnectionState.Closed) conp.Open();
                                    using (SqlCommand d6 = new SqlCommand(query, conp))
                                    {
                                        TarihBakiye = Convert.ToDecimal(d6.ExecuteScalar());
                                    }
                                }
                            }
                            catch { }

                            tbl.Rows.Add(
                            dfb["ID"].ToString(),
                            dfb["CariUnvan"].ToString().Replace("İ", "I").Replace("Ğ", "G"),
                            dfb["Il"].ToString().ToUpper(),
                            dfb["FirmaKodu"].ToString(),
                            dfb["VergiDr"].ToString(),
                            dfb["VergiNo"].ToString(),
                            dfb["Yetkili"].ToString(),
                            /*8*/ dfb["TCNo"].ToString(),
                                  dfb["Adres"].ToString(),
                                  dfb["Ilce"].ToString(),
                                  dfb["Ulke"].ToString(),
                                  dfb["Telefon1"].ToString(),
                                  dfb["GSM"].ToString(),
                                  dfb["Faks"].ToString(),
                                  dfb["EPosta"].ToString(),
                            /*16*/  dfb["WebSite"].ToString(), "",
                            alacak, borc, bakiye, netyaziyla,
                            dfb["paraBirimi"].ToString(),
                            BocAB,
                            dfb["BagkurNo"].ToString(),
                            BA.ToString(),
                            BS.ToString(),
                            BABelge.ToString(),
                            BSBelge.ToString(),
                            donemay, donemyil, Temsilcisi,
                            BorcABEUR,
                            BorcABUSD,
                            para, TarihBakiye
                            );
                        }
                    }
                }
            }
            return tbl;

        }
        public static string textify(decimal tutar)
        {
            string sTutar = tutar.ToString("F2").Replace('.', ',');
            string lira = sTutar.Substring(0, sTutar.IndexOf(','));
            string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
            string yazi = "";

            string[] birler = { "", " Bir", " İki", " Üç", " Dört", " Beş", " Altı", " Yedi", " Sekiz", " Dokuz" };
            string[] onlar = { "", " On", " Yirmi", " Otuz", " Kırk", " Elli", " Altmış", " Yetmiş", " Seksen", " Doksan" };
            string[] binler = { " Katrilyon", " Trilyon", " Milyar", " Milyon", " Bin", "" };

            int grupSayisi = 6;

            lira = lira.PadLeft(grupSayisi * 3, '0');

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3)
            {
                grupDegeri = "";

                if (lira.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + " Yüz";

                if (grupDegeri == " Biryüz")
                    grupDegeri = " Yüz";

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))];

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))];

                if (grupDegeri != "")
                    grupDegeri += binler[i / 3];

                if (grupDegeri == " Birbin")
                    grupDegeri = " Bin";

                yazi += grupDegeri;
            }

            if (yazi != "")
                yazi += " Türk Lirası";

            int yaziUzunlugu = yazi.Length;

            if (kurus.Substring(0, 1) != "0")
                yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

            if (kurus.Substring(1, 1) != "0")
                yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

            if (yazi.Length > yaziUzunlugu)
                yazi += " Kuruş";
            else
                yazi += " Sıfır Kuruş";

            if (tutar >= 100 && yazi.Substring(1, 4).Contains("Bir")) yazi = yazi.Remove(0, 4);
            if (tutar >= 1000 && tutar < 2000 && yazi.Substring(5, 3).Contains("Bir"))
            {
                yazi = yazi.Remove(4, 4);
            }
            else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(9, 3).Contains("Bir"))
            {
                yazi = yazi.Remove(8, 4);
            }
            else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(8, 3).Contains("Bir"))
            {
                yazi = yazi.Remove(7, 4);
            }
            else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(10, 3).Contains("Bir"))
            {


                yazi = yazi.Remove(9, 4);
            }
            else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(11, 3).Contains("Bir"))
            {

                try
                {
                    yazi = yazi.Remove(10, 4);
                }
                catch { }
            }

            return yazi;
        }
        public static string textifyDolar(decimal tutar)
        {
            string sTutar = tutar.ToString("F2").Replace('.', ',');
            string lira = sTutar.Substring(0, sTutar.IndexOf(','));
            string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
            string yazi = "";

            string[] birler = { "", " Bir", " İki", " Üç", " Dört", " Beş", " Altı", " Yedi", " Sekiz", " Dokuz" };
            string[] onlar = { "", " On", " Yirmi", " Otuz", " Kırk", " Elli", " Altmış", " Yetmiş", " Seksen", " Doksan" };
            string[] binler = { " Katrilyon", " Trilyon", " Milyar", " Milyon", " Bin", "" };

            int grupSayisi = 6;

            lira = lira.PadLeft(grupSayisi * 3, '0');

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3)
            {
                grupDegeri = "";

                if (lira.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + " Yüz";

                if (grupDegeri == " Biryüz")
                    grupDegeri = " Yüz";

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))];

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))];

                if (grupDegeri != "")
                    grupDegeri += binler[i / 3];

                if (grupDegeri == " Birbin")
                    grupDegeri = " Bin";

                yazi += grupDegeri;
            }

            if (yazi != "")
                yazi += " Dolar";

            int yaziUzunlugu = yazi.Length;

            if (kurus.Substring(0, 1) != "0")
                yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

            if (kurus.Substring(1, 1) != "0")
                yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

            if (yazi.Length > yaziUzunlugu)
                yazi += " Cent";
            else
                yazi += "";

            if (tutar >= 100 && yazi.Substring(1, 4).Contains("Bir")) yazi = yazi.Remove(0, 4);
            if (tutar >= 1000 && tutar < 2000 && yazi.Substring(5, 3).Contains("Bir"))
            {
                yazi = yazi.Remove(4, 4);
            }
            else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(9, 3).Contains("Bir"))
            {
                yazi = yazi.Remove(8, 4);
            }
            else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(8, 3).Contains("Bir"))
            {
                yazi = yazi.Remove(7, 4);
            }
            else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(10, 3).Contains("Bir"))
            {
                yazi = yazi.Remove(9, 4);
            }
            else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(11, 3).Contains("Bir"))
            {
                yazi = yazi.Remove(10, 4);
            }

            return yazi;
        }
        public static string textifyEuro(decimal tutar)
        {
            try
            {
                string sTutar = tutar.ToString("F2").Replace('.', ',');
                string lira = sTutar.Substring(0, sTutar.IndexOf(','));
                string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
                string yazi = "";

                string[] birler = { "", " Bir", " İki", " Üç", " Dört", " Beş", " Altı", " Yedi", " Sekiz", " Dokuz" };
                string[] onlar = { "", " On", " Yirmi", " Otuz", " Kırk", " Elli", " Altmış", " Yetmiş", " Seksen", " Doksan" };
                string[] binler = { " Katrilyon", " Trilyon", " Milyar", " Milyon", " Bin", "" };

                int grupSayisi = 6;

                lira = lira.PadLeft(grupSayisi * 3, '0');

                string grupDegeri;

                for (int i = 0; i < grupSayisi * 3; i += 3)
                {
                    grupDegeri = "";

                    if (lira.Substring(i, 1) != "0")
                        grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + " Yüz";

                    if (grupDegeri == " Biryüz")
                        grupDegeri = " Yüz";

                    grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))];

                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))];

                    if (grupDegeri != "")
                        grupDegeri += binler[i / 3];

                    if (grupDegeri == " Birbin")
                        grupDegeri = " Bin";

                    yazi += grupDegeri;
                }

                if (yazi != "")
                    yazi += " Euro";

                int yaziUzunlugu = yazi.Length;

                if (kurus.Substring(0, 1) != "0")
                    yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

                if (kurus.Substring(1, 1) != "0")
                    yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

                if (yazi.Length > yaziUzunlugu)
                    yazi += " Cent";

                else
                    yazi += "";

                if (tutar >= 100 && yazi.Substring(1, 4).Contains("Bir")) yazi = yazi.Remove(0, 4);

                if (tutar >= 1000 && tutar < 2000 && yazi.Substring(5, 3).Contains("Bir"))
                {
                    yazi = yazi.Remove(4, 4);
                }
                else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(9, 3).Contains("Bir"))
                {
                    yazi = yazi.Remove(8, 4);
                }
                else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(8, 3).Contains("Bir"))
                {
                    yazi = yazi.Remove(7, 4);
                }
                else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(10, 3).Contains("Bir"))
                {


                    yazi = yazi.Remove(9, 4);
                }
                else if (tutar >= 2000 && tutar < 10000 && yazi.Substring(11, 3).Contains("Bir"))
                {

                    try
                    {
                        yazi = yazi.Remove(10, 4);
                    }
                    catch { }
                }

                return yazi;
            }
            catch { return ""; }
        }
        public static string conString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        //Servis
        public static List<SelectListItem> ServisDurumlar()
        {
            List<SelectListItem> Butce = new List<SelectListItem>
             {
                     new SelectListItem { Text = "GİRİŞ YAPILDI", Value = "GİRİŞ YAPILDI" },
                     new SelectListItem { Text = "SERVİSE ALINDI", Value = "SERVİSE ALINDI" },
                     new SelectListItem { Text = "TEST AŞAMASINDA", Value = "TEST AŞAMASINDA" },
                     new SelectListItem { Text = "ONARIMA BAŞLANDI", Value = "ONARIMA BAŞLANDI" },
                     new SelectListItem { Text = "PARÇA BEKLİYOR", Value = "PARÇA BEKLİYOR" },
                     new SelectListItem { Text = "MÜŞTERİ ONAYI BEKLENİYOR", Value = "MÜŞTERİ ONAYI BEKLENİYOR" },
                     new SelectListItem { Text = "MÜŞTERİ ONAYI ALINDI", Value = "MÜŞTERİ ONAYI ALINDI" },
                     new SelectListItem { Text = "MÜŞTERİ ONAYLAMADI", Value = "MÜŞTERİ ONAYLAMADI" },
                     new SelectListItem { Text = "İŞLEM YAPILMADI", Value = "İŞLEM YAPILMADI" },
                     new SelectListItem { Text = "KALİTE KONTROLÜ YAPILIYOR", Value = "KALİTE KONTROLÜ YAPILIYOR" },
                     new SelectListItem { Text = "ONAY BEKLENİYOR", Value = "ONAY BEKLENİYOR" },
                     new SelectListItem { Text = "ONARIM TAMAMLANDI", Value = "ONARIM TAMAMLANDI" },
                     new SelectListItem { Text = "TESLİMAT BEKLİYOR", Value = "TESLİMAT BEKLİYOR" },
                     new SelectListItem { Text = "FATURALANDI", Value = "FATURALANDI" },
                     new SelectListItem { Text = "TESLİM EDİLDİ", Value = "TESLİM EDİLDİ" },
                     new SelectListItem { Text = "ONARIM İPTAL EDİLDİ", Value = "ONARIM İPTAL EDİLDİ" },
                     new SelectListItem { Text = "FABRİKA İADESİ BEKLENİYOR", Value = "FABRİKA İADESİ BEKLENİYOR" },
                     new SelectListItem { Text = "FABRİKA DEĞİŞİM BEKLENİYOR", Value = "FABRİKA DEĞİŞİM BEKLENİYOR" },
                     new SelectListItem { Text = "MONTAJ TAMAMLANDI", Value = "MONTAJ TAMAMLANDI" },
                     new SelectListItem { Text = "BAKIM YAPILDI", Value = "BAKIM YAPILDI" },
                     new SelectListItem { Text = "KONTROL EDİLDİ", Value = "KONTROL EDİLDİ" },
                     new SelectListItem { Text = "ARIZA GİDERİLDİ", Value = "ARIZA GİDERİLDİ" },
                     new SelectListItem { Text = "SERVİS ERTELENDİ", Value = "SERVİS ERTELENDİ" },
                     new SelectListItem { Text = "DIŞ SERVİSTE", Value = "DIŞ SERVİSTE" },
             };
            return Butce;
        }
        public static List<SelectListItem> PBLER(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> IcerikTurListe = (from k in db.CURRENCY_LIST.Where(x => x.Durumu == true)
                                                       select new SelectListItem
                                                       {
                                                           Value = k.ParaBirimi.ToString(),
                                                           Text = k.ParaBirimi.ToString(),
                                                           Selected = true,
                                                       }).ToList();

                return IcerikTurListe;
            }

        }
        public static List<SelectListItem> StokList(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> IcerikTurListe = (from k in db.Stok.Where(x => x.FirmaID == firmaid)
                                                       select new SelectListItem
                                                       {
                                                           Value = k.ID.ToString(),
                                                           Text = k.UrunAdi.ToString(),
                                                           Selected = true,
                                                       }).ToList();

                return IcerikTurListe;
            }

        }
        public static List<SelectListItem> DepolarListesi(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> IcerikTurListe = (from k in db.STORE.Where(x => x.FirmaID == firmaid)
                                                       select new SelectListItem
                                                       {

                                                           Value = k.ID.ToString(),
                                                           Text = k.DepoAdi.ToString(),
                                                           Selected = true,
                                                       }).ToList();

                return IcerikTurListe;
            }

        }
        public static List<SelectListItem> CariKategori(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            try
            {
                using (sayazilimEntities db = new sayazilimEntities())
                {
                    List<SelectListItem> IcerikTurListe = (from k in db.CARI_KATEGORI.Where(x => x.FirmaID == firmaid)
                                                           select new SelectListItem
                                                           {
                                                               Value = k.ID.ToString(),
                                                               Text = k.Name.ToString().TrimEnd().TrimStart(),
                                                               Selected = true,
                                                           }).ToList();

                    return IcerikTurListe;
                }

            }
            catch
            {
                List<SelectListItem> IcerikTurListe = new List<SelectListItem>();
                return IcerikTurListe;
            }

        }
        public static List<SelectListItem> UstCariler(string FirmaID)
        {

            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> IcerikTurListe = (from k in db.Cari.Where(x => x.FirmaID == firmaid && x.KTipi != "BAYİİ")
                                                       select new SelectListItem
                                                       {
                                                           Value = k.ID.ToString(),
                                                           Text = (k.GSM.ToString() + " " + k.CariUnvan.ToString() + " " + k.Adres.ToString()).ToString().TrimEnd().TrimStart(),
                                                           Selected = true,
                                                       }).ToList();

                return IcerikTurListe;
            }

        }
        public static List<SelectListItem> Bayiler(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> IcerikTurListe = (from k in db.Cari.Where(x => x.FirmaID == firmaid)
                                                       where k.KTipi == "BAYİİ"
                                                       select new SelectListItem
                                                       {
                                                           Value = k.ID.ToString(),
                                                           Text = (k.Telefon1.ToString() + " " + k.CariUnvan.ToString() + " " + k.Adres.ToString()).ToString().TrimEnd().TrimStart(),
                                                           Selected = true,
                                                       }).ToList();

                return IcerikTurListe;
            }

        }
        public static List<SelectListItem> BayiiList(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> IcerikTurListe = (from k in db.Cari.Where(x => x.KTipi == "BAYİİ" && x.FirmaID == firmaid)
                                                       select new SelectListItem
                                                       {
                                                           Value = k.ID.ToString(),
                                                           Text = k.CariUnvan.ToString(),
                                                           Selected = true,
                                                       }).ToList();

                return IcerikTurListe;
            }

        }
        public static List<SelectListItem> Personeller(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> IcerikTurListe = (from k in db.Personel.Where(x => x.FirmaID == firmaid)
                                                       select new SelectListItem
                                                       {
                                                           Value = k.ID.ToString(),
                                                           Text = (k.Adi.ToString() + " " + k.Soyadi.ToString()).TrimEnd(),
                                                           Selected = true,
                                                       }).ToList();

                return IcerikTurListe;
            }
        }
        public static List<SelectListItem> MusteriTemsilcileri(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> IcerikTurListe = (from k in db.Personel.Where(x => x.FirmaID == firmaid)
                                                       select new SelectListItem
                                                       {
                                                           Value = k.ID.ToString(),
                                                           Text = k.Adi.ToString() + " " + k.Soyadi,
                                                           Selected = true,
                                                       }).ToList();

                return IcerikTurListe;
            }

        }
        public static List<SelectListItem> Bakiye()
        {
            List<SelectListItem> Bakiye = new List<SelectListItem>
{
new SelectListItem { Text = "TL", Value = "TL" },
new SelectListItem { Text = "USD", Value = " USD" },
new SelectListItem { Text = "GBP", Value = " GBP" },

};
            return Bakiye;
        }
        public static void COMPANY_DETAIL(Datasetler SADataset1)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();

                using (SqlCommand carigetir = new SqlCommand("Select * From COMPANY_DETAIL", con))
                {
                    using (SqlDataReader dfb = carigetir.ExecuteReader())
                    {
                        while (dfb.Read())
                        {
                            DataRow ds = SADataset1.FirmaBilgileri.NewRow();

                            ds["Firma Adı"] = dfb["FirmaAdi"].ToString();
                            ds["Telefon"] = dfb["Telefon"].ToString();
                            ds["Şehir"] = dfb["Sehir"].ToString();
                            ds["Vergi Dairesi"] = dfb["VergiDairesi"].ToString();
                            ds["Vergi No"] = dfb["VergiNo"].ToString();
                            ds["Ülke"] = dfb["Ulke"].ToString();
                            ds["Posta Kodu"] = dfb["PostaKodu"].ToString();
                            ds["Adres"] = dfb["Adres"].ToString();

                            SADataset1.FirmaBilgileri.Rows.Add(ds);
                        }
                    }
                }
            }
        }
        public static List<SelectListItem> IslemTipi()
        {
            List<SelectListItem> IslemTipi = new List<SelectListItem>
{
new SelectListItem { Text = "ALACAK", Value = "ALACAK" },
new SelectListItem { Text = "ÖDEME", Value = " ÖDEME" },

};
            return IslemTipi;
        }
        public static List<SelectListItem> DekontTipi()
        {
            List<SelectListItem> IslemTipi = new List<SelectListItem>
{
new SelectListItem { Text = "Alacak Dekontu", Value = "Alacak Dekontu" },
new SelectListItem { Text = "Borç Dekontu", Value = "Borç Dekontu" },

};
            return IslemTipi;
        }
        public static List<SelectListItem> DepoDurumu()
        {
            List<SelectListItem> IslemTipi = new List<SelectListItem>
{
new SelectListItem { Text = "Aktif", Value = "Aktif" },
new SelectListItem { Text = "Pasif", Value = "Pasif" },

};
            return IslemTipi;
        }
        public static List<SelectListItem> FaturaType()
        {
            List<SelectListItem> IslemTipi = new List<SelectListItem>
{
new SelectListItem { Text = "Satış Faturası", Value = "SF" },
new SelectListItem { Text = "Alım Faturası", Value = "AF" },
new SelectListItem { Text = "Sevk İrsaliyesi", Value = "SI" },
new SelectListItem { Text = "Alım İrsaliyesi", Value = "AI" },

};
            return IslemTipi;
        }

        public static List<SelectListItem> CekSenet()
        {
            List<SelectListItem> IslemTipi = new List<SelectListItem>
            {
                new SelectListItem { Text = "Çek", Value = "Çek" },
                new SelectListItem { Text = "Senet", Value = "Senet" },

            };
            return IslemTipi;
        }
        public static List<SelectListItem> AlacakBorcTipi()
        {
            List<SelectListItem> AlacakBorcTipi = new List<SelectListItem>
{
new SelectListItem { Text = "MAAŞ", Value = "MAAŞ" },
new SelectListItem { Text = "YEMEK", Value = " YEMEK" },
new SelectListItem { Text = "YOL HARCIRAHI", Value = " YOL HARCIRAHI" },
new SelectListItem { Text = "PRİM", Value = " PRİM" },
new SelectListItem { Text = "AVANS", Value = " AVANS" },
                     new SelectListItem { Text = "AÇIK AVANS", Value = " AÇIKAVANS" },
new SelectListItem { Text = "İKRAMİYE", Value = " İKRAMİYE" },
new SelectListItem { Text = "MESAİ", Value = " MESAİ" },
new SelectListItem { Text = "DİĞER", Value = " DİĞER" },
};
            return AlacakBorcTipi;
        }
        public static List<SelectListItem> BakimList()
        {

            List<SelectListItem> IslemTipi = new List<SelectListItem>
{
                new SelectListItem { Text = "Tüm Durumlar", Value = "Tüm Durumlar" },
                new SelectListItem { Text = "GİRİŞ YAPILDI", Value = "GİRİŞ YAPILDI" },
                new SelectListItem { Text = "MONTAJ TAMAMLANDI", Value = "MONTAJ TAMAMLANDI" },
                new SelectListItem { Text = "BAKIM YAPILDI", Value = "BAKIM YAPILDI" },
                new SelectListItem { Text = "KONTROL EDİLDİ", Value = "KONTROL EDİLDİ" },
                new SelectListItem { Text = "ARIZA GİDERİLDİ", Value = "ARIZA GİDERİLDİ" },
                new SelectListItem { Text = "SERVİS ERTELENDİ", Value = "SERVİS ERTELENDİ" },
                new SelectListItem { Text = "SERVİSE ALINDI", Value = "SERVİSE ALINDI" },

};
            return IslemTipi;
        }
        public static List<SelectListItem> EvrakTipi()
        {

            List<SelectListItem> IslemTipi = new List<SelectListItem>
{
new SelectListItem { Text = "Tüm Evraklar", Value = "Tüm Evraklar" },
new SelectListItem { Text = "Müşteri Çeki", Value = "Müşteri Çeki" },
               new SelectListItem { Text = "Müşteri Senedi", Value = "Müşteri Senedi" },
new SelectListItem { Text = "Çekimiz", Value = "Çekimiz" },
new SelectListItem { Text = "Senedimiz", Value = "Senedimiz" }
};
            return IslemTipi;
        }
        public static List<SelectListItem> CekDurumu()
        {

            List<SelectListItem> IslemTipi = new List<SelectListItem>
{
new SelectListItem { Text = "Tüm Durumlar", Value = "Tüm Durumlar" },
new SelectListItem { Text = "Portföyde", Value = "Portföyde" },
   new SelectListItem { Text = "Teminat", Value = "Teminat" },
new SelectListItem { Text = "İade Edildi", Value = "İade Edildi" },
new SelectListItem { Text = "Faktoringde Bekliyor", Value = "Faktoringde Bekliyor" },
new SelectListItem { Text = "Tahsil Edildi", Value = "Tahsil Edildi" },
new SelectListItem { Text = "Ödendi", Value = "Ödendi" },
new SelectListItem { Text = "İade Alındı", Value = "İade Alındı" },
new SelectListItem { Text = "Ciro Edildi", Value = "Ciro Edildi" },
new SelectListItem { Text = "Banka_Portföyde", Value = "Banka_Portföyde" },
};
            return IslemTipi;
        }
        public static List<SelectListItem> PersonelList(string sCariID = "-1", string FirmaID = "-1")
        {
            int scariid = Convert.ToInt32(sCariID);
            int firmaid = Convert.ToInt32(FirmaID);

            if (scariid == -1)
            {
                using (sayazilimEntities db = new sayazilimEntities())
                {
                    List<SelectListItem> PersonelList = (from k in db.Personel.Where(x => x.FirmaID == firmaid)
                                                         select new SelectListItem
                                                         {
                                                             Value = k.ID.ToString(),
                                                             Text = k.Adi + " " + k.Soyadi,
                                                         }).ToList();



                    return PersonelList;
                }
            }
            else
            {
                using (sayazilimEntities db = new sayazilimEntities())
                {
                    List<SelectListItem> PersonelList = (from k in db.Personel.Where(x => x.sCariID == scariid && x.FirmaID == firmaid)
                                                         select new SelectListItem
                                                         {
                                                             Value = k.ID.ToString(),
                                                             Text = k.Adi + " " + k.Soyadi,
                                                         }).ToList();



                    return PersonelList;
                }
            }
        }
        public static List<SelectListItem> DepoListesi(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> PersonelList = (from k in db.STORE.Where(x => x.FirmaID == firmaid)
                                                     select new SelectListItem
                                                     {
                                                         Value = k.ID.ToString(),
                                                         Text = k.DepoAdi.ToString(),

                                                     }).ToList();




                return PersonelList;
            }
        }
        public static List<SelectListItem> Ulkeler()
        {

            try
            {
                using (sayazilimEntities db = new sayazilimEntities())
                {
                    List<SelectListItem> UlkeList = (from k in db.COUNTRIES
                                                     select new SelectListItem
                                                     {
                                                         Value = k.KOD,
                                                         Text = k.ULKE_ADI,
                                                     }).ToList();



                    return UlkeList;
                }

            }
            catch
            {
                using (sayazilimEntities db = new sayazilimEntities())
                {
                    List<SelectListItem> UlkeList = (from k in db.COUNTRIES
                                                     select new SelectListItem
                                                     {
                                                         Value = k.KOD,
                                                         Text = k.ULKE_ADI,
                                                     }).ToList();



                    return UlkeList;
                }

            }

        }
        public static List<SelectListItem> OzelKod(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> OzelKod = (from k in db.OzelKod.Where(x => x.FirmaID == firmaid)
                                                select new SelectListItem
                                                {
                                                    Value = k.ID.ToString(),
                                                    Text = k.KodAdi.ToString(),
                                                    Selected = true,
                                                }).ToList();



                return OzelKod;
            }

        }
        public static List<SelectListItem> GarantiDurumu()
        {
            List<SelectListItem> Butce = new List<SelectListItem>();

            SelectListItem bt = new SelectListItem
            {
                Value = "Yasal Garantili",
                Text = "Yasal Garantili",
            };
            Butce.Add(bt);
            bt = new SelectListItem
            {
                Value = "Servis Garantili",
                Text = "Servis Garantili",
            };
            Butce.Add(bt);
            bt = new SelectListItem
            {
                Value = "Garanti Dışı",
                Text = "Garanti Dışı",
            };
            Butce.Add(bt);

            return Butce;

        }
        public static List<SelectListItem> BakimDurumu()
        {
            List<SelectListItem> Butce = new List<SelectListItem>();

            SelectListItem bt = new SelectListItem { Value = "Bakım Yok", Text = "Aylık", };
            Butce.Add(bt);

            bt = new SelectListItem { Value = "Haftalık", Text = "Yıllık", };
            Butce.Add(bt);

            bt = new SelectListItem { Value = "Sonraki Bakım", Text = "Belirli Aralık", };
            Butce.Add(bt);

            return Butce;

        }
        public static List<SelectListItem> ServisTurleri()
        {
            List<SelectListItem> Butce = new List<SelectListItem>();

            SelectListItem bt = new SelectListItem
            {
                Value = "Arıza",
                Text = "Arıza",
            };
            Butce.Add(bt);
            bt = new SelectListItem
            {
                Value = "Bakım",
                Text = "Bakım",
            };
            Butce.Add(bt);
            bt = new SelectListItem
            {
                Value = "Onarım",
                Text = "Onarım",
            };
            Butce.Add(bt);
            bt = new SelectListItem
            {
                Value = "Montaj",
                Text = "Montaj",
            };
            Butce.Add(bt);
            bt = new SelectListItem
            {
                Value = "Diğer",
                Text = "Diğer",
            };
            Butce.Add(bt);
            return Butce;
        }
        public static List<SelectListItem> UstKod(string FirmaID)
        {

            List<SelectListItem> liste = new List<SelectListItem>();


            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand ozelgetir = new SqlCommand(@"Select KodAdi,ID From OzelKod Where FirmaID=" + FirmaID, con))
                {
                    using (SqlDataReader dr = ozelgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string cari = dr["KodAdi"].ToString();



                            SelectListItem bt = new SelectListItem
                            {
                                Value = dr["ID"].ToString(),
                                Text = cari,
                            };
                            liste.Add(bt);
                        }
                    }
                }

            }

            return liste;
        }
        public static List<SelectListItem> Kodlar(string FirmaID)
        {
            List<SelectListItem> liste = new List<SelectListItem>();


            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand ozelgetir = new SqlCommand(@"Select KodAdi,ID From OzelKod Where FirmaID=" + FirmaID, con))
                {
                    using (SqlDataReader dr = ozelgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string kodadi = dr["KodAdi"].ToString();



                            SelectListItem bt = new SelectListItem
                            {
                                Value = dr["ID"].ToString(),
                                Text = kodadi,
                            };
                            liste.Add(bt);
                        }
                    }
                }

            }

            return liste;
        }
        public static void UrunAgaciEkleFatura(string ID, string turu)
        {
            try
            {
                using (SqlConnection cong = new SqlConnection(AyarMetot.strcon))
                {
                    if (cong.State == ConnectionState.Closed) cong.Open();

                    using (SqlCommand get = new SqlCommand("Select urunID,ID From INVOICE_DETAIL where FaturaID='" + ID + "'", cong))
                    {
                        using (SqlDataReader db = get.ExecuteReader())
                        {
                            while (db.Read())
                            {
                                using (SqlConnection conu = new SqlConnection(AyarMetot.strcon))
                                {
                                    if (conu.State == ConnectionState.Closed) conu.Open();

                                    using (SqlCommand carigetir = new SqlCommand("Select * From TREE_PRODUCT where AgacID='" + db["urunID"] + "'", conu))
                                    {
                                        using (SqlDataReader du = carigetir.ExecuteReader())
                                        {
                                            int i = 0;
                                            while (du.Read())
                                            {
                                                using (SqlConnection conf = new SqlConnection(AyarMetot.strcon))
                                                {
                                                    if (conf.State == ConnectionState.Closed) conf.Open();
                                                    using (SqlDataAdapter dau = new System.Data.SqlClient.SqlDataAdapter("select top 1 * from TREE_SATIS", conf))
                                                    {
                                                        using (SqlCommandBuilder cb = new SqlCommandBuilder(dau))
                                                        {
                                                            DataSet sfatu = new DataSet();
                                                            dau.Fill(sfatu, "TREE_SATIS");

                                                            bool degervar = false;

                                                            try
                                                            {
                                                                using (SqlConnection conde = new SqlConnection(AyarMetot.strcon))
                                                                {
                                                                    if (conde.State == ConnectionState.Closed) conde.Open();
                                                                    using (SqlCommand d6 = new SqlCommand("Select Count(ID) From TREE_SATIS where Islem='" + db["ID"] + "' and IslemTuru='Fatura' and  AgacUrunID ='" + db["urunID"] + "' and UrunID='" + du["UrunID"] + "'", conde))
                                                                    {
                                                                        if (Convert.ToInt32(d6.ExecuteScalar()) >= 1)
                                                                        {
                                                                            degervar = true;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            catch { }

                                                            if (degervar == false)
                                                            {
                                                                DataRow df = sfatu.Tables["TREE_SATIS"].NewRow();
                                                                df["AgacUrunID"] = db["urunID"];
                                                                df["IslemID"] = db["ID"];
                                                                df["UrunID"] = du["UrunID"];
                                                                df["BirimAdet"] = du["BirimAdet"];
                                                                df["Birim"] = du["Birim"];
                                                                df["IslemTuru"] = turu;
                                                                df["SeriNo"] = du["Serino"];
                                                                df["Miktar"] = du["Miktar"];
                                                                df["SatishFiyat"] = du["SatishFiyat"];


                                                                try
                                                                {
                                                                    using (SqlConnection conde = new SqlConnection(AyarMetot.strcon))
                                                                    {
                                                                        if (conde.State == ConnectionState.Closed) conde.Open();
                                                                        using (SqlCommand d6 = new SqlCommand("Select KDV From STOK where ID='" + du["urunID"] + "' ", conde))
                                                                        {

                                                                            df["KDV"] = Convert.ToInt32(d6.ExecuteScalar());

                                                                        }
                                                                    }
                                                                }
                                                                catch { }


                                                                sfatu.Tables["TREE_SATIS"].Rows.Add(df);
                                                            }

                                                            i++;
                                                            dau.Update(sfatu, "TREE_SATIS");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
        public static List<SelectListItem> PaketList()
        {

            List<SelectListItem> IslemTipi = new List<SelectListItem>
            {
                new SelectListItem { Text = "Full Paket", Value = "Full" },
                new SelectListItem { Text = "Ön Muhasebe 1", Value = "Muhasebe1" },
                new SelectListItem { Text = "Ön Muhasebe 2", Value = "Muhasebe2" },
                new SelectListItem { Text = "Ön Muhasebe 3", Value = "Muhasebe3" },
                new SelectListItem { Text = "Teknik Servis 1", Value = "Servis1" },
                new SelectListItem { Text = "Teknik Servis 2", Value = "Servis2" },
                new SelectListItem { Text = "Perakende Satış", Value = "hizlisatis" },
            };
            return IslemTipi;
        }




        public static List<SelectListItem> GarantiSuresi()
        {
            List<SelectListItem> Butce = new List<SelectListItem>
             {
                     new SelectListItem { Text = "0", Value = "0" },
                     new SelectListItem { Text = "1", Value = "1" },
                     new SelectListItem { Text = "2", Value = "2" },
                     new SelectListItem { Text = "3", Value = "3" },
                     new SelectListItem { Text = "4", Value = "4" },
                     new SelectListItem { Text = "5", Value = "5" },
                     new SelectListItem { Text = "6", Value = "6" },
                     new SelectListItem { Text = "7", Value = "7" },
                     new SelectListItem { Text = "8", Value = "8" },
                     new SelectListItem { Text = "9", Value = "9" },
                     new SelectListItem { Text = "10", Value = "10" },
                     new SelectListItem { Text = "11", Value = "11" },
                     new SelectListItem { Text = "12", Value = "12" },
                     new SelectListItem { Text = "13", Value = "13" },
                     new SelectListItem { Text = "14", Value = "14" },
                     new SelectListItem { Text = "15", Value = "15" },
                     new SelectListItem { Text = "16", Value = "16" },
                     new SelectListItem { Text = "17", Value = "17" },
                     new SelectListItem { Text = "18", Value = "18" },
                     new SelectListItem { Text = "19", Value = "19" },
                     new SelectListItem { Text = "20", Value = "20" },
                     new SelectListItem { Text = "21", Value = "21" },
                     new SelectListItem { Text = "22", Value = "22" },
                     new SelectListItem { Text = "23", Value = "23" },
                     new SelectListItem { Text = "24", Value = "24" },
                     new SelectListItem { Text = "25", Value = "25" },
                     new SelectListItem { Text = "26", Value = "26" },
                     new SelectListItem { Text = "27", Value = "27" },
                     new SelectListItem { Text = "28", Value = "28" },
                     new SelectListItem { Text = "29", Value = "29" },
                     new SelectListItem { Text = "30", Value = "30" },
                     new SelectListItem { Text = "31", Value = "31" },
                     new SelectListItem { Text = "32", Value = "32" },
                     new SelectListItem { Text = "33", Value = "33" },
                     new SelectListItem { Text = "34", Value = "34" },
                     new SelectListItem { Text = "35", Value = "35" },
                     new SelectListItem { Text = "36", Value = "36" },

             };
            return Butce;
        }
        public static List<SelectListItem> StokDurumu()
        {
            List<SelectListItem> Butce = new List<SelectListItem>
             {
                     new SelectListItem { Text = "Sıfır", Value = "Sıfır" },
                     new SelectListItem { Text = "Kullanılmış", Value = "Kullanılmış" },


             };
            return Butce;
        }

        public static string AyarBilgiCek(string Alan)
        {
            try
            {
                using (SqlConnection conp = new SqlConnection(AyarMetot.conString))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (SqlCommand d6 = new SqlCommand("Select " + Alan + " From Ayarlar ", conp))
                    {
                        return d6.ExecuteScalar().ToString();
                    }
                }
            }
            catch { return ""; }
        }
        public static string UrunAD(string Alan = "")
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    conp.Open();
                    using (SqlCommand d6 = new SqlCommand("Select UrunAdi From Stok where ID='" + Alan + "'", conp))
                    {
                        return d6.ExecuteScalar().ToString();
                    }
                }
            }
            catch { return ""; }
        }
        public static List<SelectListItem> BedenListesi(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> PersonelList = (from k in db.Bedenler.Where(x => x.FirmaID == firmaid)
                                                     select new SelectListItem
                                                     {
                                                         Value = k.ID.ToString(),
                                                         Text = k.BedenAdi,

                                                     }).ToList();




                return PersonelList;
            }
        }
        public static System.Drawing.Image ByteArrayToImage(byte[] bArray)
        {
            if (bArray == null)
                return null;

            System.Drawing.Image newImage;

            try
            {
                using (MemoryStream ms = new MemoryStream(bArray, 0, bArray.Length))
                {
                    ms.Write(bArray, 0, bArray.Length);
                    newImage = System.Drawing.Image.FromStream(ms, true);
                }
            }
            catch
            {
                newImage = null;

            }
            return newImage;
        }


        public static List<SelectListItem> BAYillar()
        {

            List<SelectListItem> IslemTipi = new List<SelectListItem>
{
        new SelectListItem { Text = "2018", Value = "2018" },
        new SelectListItem { Text = "2019", Value = "2019" },
        new SelectListItem { Text = "2020", Value = "2020" },
        new SelectListItem { Text = "2021", Value = "2021" },
        new SelectListItem { Text = "2022", Value = "2022" },
        new SelectListItem { Text = "2023", Value = "2023" },
        new SelectListItem { Text = "2024", Value = "2024" },
        new SelectListItem { Text = "2025", Value = "2025" },
        new SelectListItem { Text = "2026", Value = "2026" },
        new SelectListItem { Text = "2027", Value = "2027" },
         new SelectListItem { Text = "2027", Value = "2027" },
          new SelectListItem { Text = "2028", Value = "2028" },
          new SelectListItem { Text = "2029", Value = "2029" },
           new SelectListItem { Text = "2030", Value = "2030" },

};
            return IslemTipi;

        }
        public static List<SelectListItem> Aylar()
        {

            List<SelectListItem> IslemTipi = new List<SelectListItem>
{
        new SelectListItem { Text = "Ocak" , Value ="1"},
        new SelectListItem { Text = "Şubat", Value ="2"},
        new SelectListItem { Text = "Mart" ,Value ="3"},
        new SelectListItem { Text = "Nisan", Value ="4"},
        new SelectListItem { Text = "Mayıs" ,Value ="5"},
        new SelectListItem { Text = "Haziran",Value ="6" },
        new SelectListItem { Text = "Temmuz",Value ="7" },
        new SelectListItem { Text = "Ağustos",Value ="8" },
        new SelectListItem { Text = "Eylül", Value ="9"},
        new SelectListItem { Text = "Ekim" ,Value ="10"},
        new SelectListItem { Text = "Kasım", Value ="11"},
        new SelectListItem { Text = "Aralık", Value ="12"},


};
            return IslemTipi;

        }


        public static List<SelectListItem> NotKategori(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);
            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> PersonelList = (from k in db.Not_Kategori.Where(x => x.FirmaID == firmaid)
                    select new SelectListItem
                    {
                        Value = k.ID.ToString(),
                        Text = k.KategoriAdi,

                    }).ToList();




                return PersonelList;
            }
        }


        public static List<SelectListItem> NotKategoriList(string FirmaID)
        {
            int firmaid = Convert.ToInt32(FirmaID);


            using (sayazilimEntities db = new sayazilimEntities())
            {
                List<SelectListItem> PersonelList = (from k in db.Not_Kategori.Where(x => x.FirmaID == firmaid)
                    select new SelectListItem
                    {
                        Value = k.ID.ToString(),
                        Text = k.KategoriAdi,
                    }).ToList();



                return PersonelList;
            }


        }

    }

}