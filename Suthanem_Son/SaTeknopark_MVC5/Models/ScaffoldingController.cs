using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SaTeknopark_MVC5.Models
{


    //
    public class UserModel
    {
        public string UserName { get; set; }
        public int PersonelID { get; set; }
        public int PersonelType { get; set; }
        public string RaporGor { get; set; }
        public string FirmaKodu { get; set; }
        public string vDepoID { get; set; }
        public string vKasaID { get; set; }
        public string vBankaID { get; set; }
        public string vKKTID { get; set; }
        public string PersonelAdi { get; set; }
        public string PersonelGrubu { get; set; }
        public bool TeknikServis { get; set; }
        public string TerminalTuru { get; set; }
        public string FirmaID { get; set; }

        public string Company_Code { get; set; }
        public string PB { get; set; }
        public string PaketTipi { get; set; }

        public int sCariID { get; set; }
    }
    internal class GirisKontrol
    {
        
        public static UserModel EntranceControl(string KA, string SF, string ConnectionString)
        {
            UserModel userModel = new UserModel();


            AyarMetot.alanEkle();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("Select Top 1 * From  Personel where WebKA='" + KA + "' AND WebSifre ='" + GirisKontrol.hash(SF.Trim(), true) + "'", connection))
                {
                    using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int personelID = Convert.ToInt32(dr["ID"].ToString());
                           
                            sayazilimEntities db = new sayazilimEntities();
                            Personel pr = db.Personel.Where(x => x.ID == personelID).FirstOrDefault<Personel>();
                            userModel.UserName = KA;
                            userModel.vDepoID = dr["vDepoID"].ToString();
                            userModel.vKasaID = dr["vKasaID"].ToString();
                            userModel.vBankaID = dr["vBankaID"].ToString();
                            userModel.vKKTID = dr["vKKTID"].ToString();
                            userModel.FirmaID = dr["FirmaID"].ToString();
                            userModel.sCariID = Convert.ToInt32(dr["sCariID"].ToString());
                            
                            if (userModel.vDepoID == "")
                            {
                                STORE str = new STORE();
                                str.DepoAdi = "Merkez";
                                str.Merkez = true;
                                str.Durumu = "Aktif";
                                str.Kodu = "DP001";
                                str.FirmaID = Convert.ToInt16(userModel.FirmaID);
                                db.STORE.Add(str);
                                db.SaveChanges();
                                STORE yenistr = db.STORE.OrderByDescending(x => x.ID).FirstOrDefault();
                                pr.vDepoID = yenistr.ID;
                                userModel.vDepoID = yenistr.ID.ToString();
                                db.SaveChanges();
                            }

                            if (userModel.vKasaID == "")
                            {
                                Kasa ks = new Kasa();
                                ks.KasaAdi = "Merkez";
                                ks.Bakiye = 0;
                                ks.KasaKodu = "KS001";
                                ks.ParaBirimi = "TL";
                                ks.FirmaID = Convert.ToInt16(userModel.FirmaID);
                                ks.Merkez = true;
                                DateTime now = DateTime.Now;
                                ks.KayitT = now.Day.ToString() + "." + now.Month.ToString() + "." + now.Year.ToString();
                                db.Kasa.Add(ks);
                                db.SaveChanges();
                                var list = db.Kasa.OrderByDescending(x => x.ID).Take(1).ToList();
                                foreach (var item in list)
                                {
                                    decimal tutar = Convert.ToDecimal(ks.Bakiye);
                                    GirisKontrol.Bakiyeekle("KAF", Convert.ToInt32(item.ID), 0, ks.ParaBirimi.ToString());
                                    pr.vKasaID = item.ID;
                                    userModel.vKasaID = pr.vKasaID.ToString();
                                    db.SaveChanges();
                                }
                            }

                            if (userModel.vBankaID == "")
                            {
                                Banka bk = new Banka();
                                bk.BankaAdi = "Merkez";
                                bk.FirmaID = Convert.ToInt16(userModel.FirmaID);
                                bk.Merkez = true;
                                bk.BankaKodu = "BK001";
                                bk.Bakiye = 0;
                                DateTime now = DateTime.Now;
                                bk.hnParaBirimi = "TL";
                                bk.KayitT = now.Day.ToString() + "." + now.Month.ToString() + "." + now.Year.ToString();
                                db.Banka.Add(bk);
                                db.SaveChanges();
                                var list = db.Banka.OrderByDescending(x => x.ID).Take(1).ToList();
                                foreach (var item in list)
                                {
                                    decimal tutar = Convert.ToDecimal(bk.Bakiye);
                                    string islemtipi = "TL";
                                    Bakiyeekle(islemtipi, item.ID,tutar,item.hnParaBirimi);
                                    pr.vBankaID = item.ID;
                                    userModel.vBankaID = item.ID.ToString();
                                    db.SaveChanges();
                                }
                            }
                            
                            userModel.Company_Code = dr["Company_Code"].ToString();
                            userModel.PB = dr["PB"].ToString();
                            userModel.sCariID = -1;
                            if (userModel.PB == "Varsayılan")
                            {
                                userModel.PB = "TL";
                            }
                            //userModel.PB = dr["vKKTID"].ToString();
                            userModel.PersonelAdi = dr["Adi"].ToString() + " " + dr["Soyadi"].ToString();
                            userModel.FirmaKodu = dr["Adi"].ToString() + " " + dr["Soyadi"].ToString();
                            userModel.PersonelID = Convert.ToInt32(dr["ID"]);
                            if (dr["PersonelGrubu"].ToString() == "Kullanıcı")
                            {
                                userModel.PersonelType = 2;

                            }
                            else if (dr["PersonelGrubu"].ToString() == "Admin")
                            {
                                userModel.PersonelType = 1;

                            }
                            else if (dr["PersonelGrubu"].ToString() == "Teknik Servis Kullanıcısı")
                            {
                                userModel.PersonelType = 4;

                            }
                            else if (dr["PersonelGrubu"].ToString() == "Ön Muhasebe 1")
                            {
                                userModel.PersonelType = 5;
                            }
                            else
                            {
                                userModel.PersonelType = 3;
                                

                            }
                            userModel.PersonelGrubu = dr["PersonelGrubu"].ToString();

                        }
                    }

                }


                using (SqlCommand sqlCommand = new SqlCommand("Select RaporGorme From  Yetkiler where PersonelID='" + userModel.PersonelID + "'", connection))
                {
                    using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            userModel.RaporGor = dr["RaporGorme"].ToString();

                        }
                    }

                }

                using (SqlCommand sqlCommand = new SqlCommand("Select Top 1 * From  Cari where WebKA='" + KA + "' AND WebSifre ='" + GirisKontrol.hash(SF.Trim(), true) + "'", connection))
                {
                    using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            userModel.UserName = KA;
                            userModel.vDepoID = "1";
                            userModel.vKasaID = "1";
                            userModel.vBankaID = "1";
                            userModel.vKKTID = "1";
                            userModel.PersonelAdi = dr["CariUnvan"].ToString();
                            userModel.FirmaKodu = dr["CariUnvan"].ToString();
                            userModel.PersonelID = Convert.ToInt32(dr["ID"]);
                            userModel.PersonelType = 3;
                            userModel.FirmaID = dr["FirmaID"].ToString();
                            if(dr["KTipi"].ToString() == "BAYİİ") { 
                            userModel.sCariID = Convert.ToInt32(dr["ID"].ToString());
                            }
                            else
                            {
                                userModel.sCariID = -1;
                            }
                            userModel.PersonelGrubu = "";

                        }
                    }

                }
            }
            userModel.TeknikServis = false;
            return userModel;
        }

        public static string hash(string toEncrypt, bool useHashing)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
                string s = (string) new AppSettingsReader().GetValue("SecurityKey", typeof(string));
                byte[] numArray;
                if (useHashing)
                {
                    MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
                    numArray = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(s));
                    cryptoServiceProvider.Clear();
                }
                else
                    numArray = Encoding.UTF8.GetBytes(s);

                TripleDESCryptoServiceProvider cryptoServiceProvider1 = new TripleDESCryptoServiceProvider();
                cryptoServiceProvider1.Key = numArray;
                cryptoServiceProvider1.Mode = CipherMode.ECB;
                cryptoServiceProvider1.Padding = PaddingMode.PKCS7;
                byte[] inArray = cryptoServiceProvider1.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
                cryptoServiceProvider1.Clear();
                return Convert.ToBase64String(inArray, 0, inArray.Length);
            }
            catch
            {
                return "";
            }
        }

        public static string unhash(string cipherString, bool useHashing)
        {
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(cipherString);
                string s = (string) new AppSettingsReader().GetValue("SecurityKey", typeof(string));
                byte[] numArray;
                if (useHashing)
                {
                    MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
                    numArray = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(s));
                    cryptoServiceProvider.Clear();
                }
                else
                    numArray = Encoding.UTF8.GetBytes(s);

                TripleDESCryptoServiceProvider cryptoServiceProvider1 = new TripleDESCryptoServiceProvider();
                cryptoServiceProvider1.Key = numArray;
                cryptoServiceProvider1.Mode = CipherMode.ECB;
                cryptoServiceProvider1.Padding = PaddingMode.PKCS7;
                byte[] bytes = cryptoServiceProvider1.CreateDecryptor()
                    .TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                cryptoServiceProvider1.Clear();
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return "";
            }
        }
        public static void Bakiyeekle(string Tipi = "", int cID  = -1, decimal tutar = 0, string parabirimi = "TL")
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
                        dt["CariID"] = -1;
                        dt["PersonelID"] = AyarMetot.PersonelID;
                        if (Tipi == "KAF")
                        {
                            dt["KasaID"] = cID;
                            dt["BankaID"] = -1;
                        }
                        else
                        {
                            dt["BankaID"] = cID;
                            dt["KasaID"] = -1;

                        }

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



                        ds1.Tables["CASH_paY"].Rows.Add(dt);
                        da.Update(ds1, "CASH_paY");

                    }
                }
            }
        }

    }
    //
    public class GorusmeModel
    {
        public DateTime? DuzenlemeTarihi { get; set; }
        public int ID { get; set; }
        public int BasvuruID { get; set; }
        public int MarkaID { get; set; }
        public string GDurumu { get; set; }
        public string GorusmeTipi { get; set; }
        public string GorusmeNotu { get; set; }
        public string YorumNotu { get; set; }
        public string MarkaAdi { get; set; }
        public string Yetkili { get; set; }
        public string Butce { get; set; }
        public string Kaynak { get; set; }
        public string Puan { get; set; }
        public string Sehir { get; set; }
        public DateTime? BasvuruTarihi { get; set; }
        public string Telefon { get; set; }
        public string FirmaNo { get; set; }
        public string Ulke { get; set; }
        public int? PersonelID { get; set; }
    }
    public class Worker
    {
        // By default, the Entity Framework interprets a property that's named ID or classnameID as the primary key.
        public int ID { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Office { get; set; }

        public string Extn { get; set; }

        public string Salary { get; set; }

    }
}