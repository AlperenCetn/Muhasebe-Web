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
using System.Security.Permissions;
using System.Web.ModelBinding;
using DevComponents.Editors;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class StokController : Controller
    {
        sayazilimEntities db = new sayazilimEntities();

        public ActionResult Urunler(int YeniStok = 0)
        {
            ViewBag.YeniStok = YeniStok;
            int FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
            var kategorilist = db.STK_KATEGORI.Where(x => x.FirmaID == FirmaID).ToList();
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

            #region Kategoriler

            if (ViewBag.Kategori2 == null)
            {
                ViewBag.Kategori2 = "Kategori 2";
            }
            if (ViewBag.Kategori3 == null)
            {
                ViewBag.Kategori3 = "Kategori 3";
            }
            if (ViewBag.Kategori4 == null)
            {
                ViewBag.Kategori4 = "Kategori 4";
            }
            if (ViewBag.Kategori5 == null)
            {
                ViewBag.Kategori5 = "Kategori 5";
            }
            if (ViewBag.Kategori6 == null)
            {
                ViewBag.Kategori6 = "Kategori 6";
            }

            #endregion

            AyarMetot.Siradaki("", "Stok", "StokKodu", Session["FirmaID"].ToString());
            ViewBag.StokKoduSiradaki = AyarMetot.GetNumara;

            using (SqlConnection con2 = new System.Data.SqlClient.SqlConnection(AyarMetot.strcon))
            {

                if (con2.State == ConnectionState.Closed) con2.Open();
                try
                {
                    using (SqlCommand calis = new SqlCommand("update stok set StoktaKalan=(SELECT Coalesce(SUM(S.gBirimMiktar+s.MF),0) FROM PRODUCT_HAREKET S " +
                                                             "where Stok.ID = S.UrunID and S.GC = '(G)' ) - (SELECT Coalesce(SUM(S.gBirimMiktar + S.MF), 0)  " +
                                                             "FROM PRODUCT_HAREKET S  " +
                                                             "where Stok.ID = S.UrunID  and S.GC = '(Ç)' ) where (Stok.UrunTuru <> N'Hizmet' AND Stok.UrunTuru <> N'Masraf') ", con2))
                    {
                        try
                        {
                            calis.CommandTimeout = 0;
                            calis.ExecuteNonQuery();
                        }
                        catch
                        { }
                    }
                }
                catch
                { }

                try
                {
                    using (SqlCommand calis = new SqlCommand("update stok set StoktaKalan=0 where (Stok.UrunTuru = N'Hizmet' OR Stok.UrunTuru = N'Masraf') ", con2))
                    {
                        try
                        {
                            calis.CommandTimeout = 0;
                            calis.ExecuteNonQuery();
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }


            return View();

        }

        public partial class StokString
        {
            public string af { get; set; }
            public string sf { get; set; }
            public string F2 { get; set; }
            public string F3 { get; set; }
            public string F4 { get; set; }
            public string F5 { get; set; }
            public string F6 { get; set; }
            public string F7 { get; set; }
            public string F8 { get; set; }
            public string F9 { get; set; }
            public string F10 { get; set; }


        }

        public partial class Kategori
        {
            public string Ktg1 { get; set; }
            public string Ktg2 { get; set; }
            public string Ktg3 { get; set; }
            public string Ktg4 { get; set; }
            public string Ktg5 { get; set; }
            public string Ktg6 { get; set; }
        }

        [HttpPost]
        public JsonResult yeniStok(Stok st, StokString dg, Kategori ktg)
        {
            var result = new { sonuc = 0, Message = "" };
            Stok stk = null;
            string Message = "Kayıt Eklendi";


            decimal satis = 0;
            decimal alis = 0;

            decimal f2 = 0;


            decimal f3 = 0;
            decimal f4 = 0;
            decimal f5 = 0;
            decimal f6 = 0;
            decimal f7 = 0;
            decimal f8 = 0;
            decimal f9 = 0;
            decimal f10 = 0;
            try
            {
                satis = decimal.Parse(dg.sf, CultureInfo.InvariantCulture);
                alis = decimal.Parse(dg.af, CultureInfo.InvariantCulture);

                f2 = decimal.Parse(dg.F2, CultureInfo.InvariantCulture);


                f3 = decimal.Parse(dg.F3, CultureInfo.InvariantCulture);
                f4 = decimal.Parse(dg.F4, CultureInfo.InvariantCulture);
                f5 = decimal.Parse(dg.F5, CultureInfo.InvariantCulture);
                f6 = decimal.Parse(dg.F6, CultureInfo.InvariantCulture);
                try
                {
                    f7 = decimal.Parse(dg.F7, CultureInfo.InvariantCulture);
                }
                catch { }
                try
                {
                    f8 = decimal.Parse(dg.F8, CultureInfo.InvariantCulture);
                }
                catch { }
                try
                {
                    f9 = decimal.Parse(dg.F9, CultureInfo.InvariantCulture); ;
                }
                catch { }
                try
                {
                    f10 = decimal.Parse(dg.F10, CultureInfo.InvariantCulture);
                }
                catch { }
            }
            catch (Exception)
            { }

            string ktg1 = "";
            string ktg2 = "";
            string ktg3 = "";
            string ktg4 = "";
            string ktg5 = "";
            string ktg6 = "";

            try
            {
                ktg1 = ktg.Ktg1;
                ktg2 = ktg.Ktg2;
                ktg3 = ktg.Ktg3;
                ktg4 = ktg.Ktg4;
                ktg5 = ktg.Ktg5;
                ktg6 = ktg.Ktg6;
            }
            catch (Exception)
            { }

            int firmaidsrg = Convert.ToInt32(Session["FirmaID"].ToString());
            var list = db.Stok.Where(x => x.StokKodu == st.StokKodu && x.FirmaID == firmaidsrg).ToList();

            if (st.ID == -1)
            {
                if (list.Count == 0)
                {

                    stk = new Stok();
                    if (st.StoktaKalan == null) st.StoktaKalan = 0;
                    stk = st;
                    stk.SatishFiyat = satis;
                    stk.AlishFiyat = alis;
                    if (st.AlishFiyat == null) st.AlishFiyat = 0;
                    if (st.SatishFiyat == null) st.SatishFiyat = 0;

                    stk.StokList = true;
                    stk.Kategori1 = ktg1;
                    stk.Kategori2 = ktg2;
                    stk.Kategori3 = ktg3;
                    stk.Kategori4 = ktg4;
                    stk.Kategori5 = ktg5;
                    stk.Kategori6 = ktg6;
                    //STRİNG GÜNCELLEMELER KÜSÜRATLARIN
                    //DOĞRU GİRİLMESİ İÇİN YAPILDI

                    stk.F2 = f2;
                    stk.F3 = f3;
                    stk.F4 = f4;
                    stk.F5 = f5;
                    stk.F6 = f6;
                    stk.F7 = f7;
                    stk.F8 = f8;
                    stk.F9 = f9;
                    stk.F10 = f10;
                    stk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                    stk.WebPGoster = true;
                    if (stk.UrunTuru == "Hizmet")
                    {
                        stk.StoktaKalan = 0;
                    }
                    string firmaid = Session["FirmaID"].ToString();
                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                    stk.Company_Code = company_code;
                    db.Stok.Add(stk);
                    db.SaveChanges();

                    if (stk.StoktaKalan != 0)
                    {

                        int depoIslId = 0;



                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter daDepoIslm = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from STORE_PROCESS", con))
                            {
                                using (SqlCommandBuilder cb1 = new SqlCommandBuilder(daDepoIslm))
                                {
                                    DataSet dsDepoIslm = new DataSet();
                                    daDepoIslm.Fill(dsDepoIslm, "STORE_PROCESS");


                                    DataRow dd = dsDepoIslm.Tables["STORE_PROCESS"].NewRow();
                                    dd["IslemKodu"] = stk.ID + "STK";
                                    dd["IslemTarih"] = DateTime.Now.ToString("dd.MM.yyyy");
                                    dd["GonderenDepoID"] = -1;
                                    dd["AlanDepoID"] = Session["vDepoID"].ToString();
                                    dd["Aciklama"] = "Stok Açılış Depo Girişi " + DateTime.Now.ToString("dd.MM.yyyy");
                                    dd["Tutar"] = stk.AlishFiyat * stk.StoktaKalan;
                                    dd["paraBirimi"] = stk.ParaBirimi;
                                    dd["personelID"] = AyarMetot.PersonelID;
                                    dd["Donem"] = AyarMetot.Donem;
                                    dd["KayitPersonelID"] = AyarMetot.PersonelID;

                                    dsDepoIslm.Tables["STORE_PROCESS"].Rows.Add(dd);
                                    daDepoIslm.Update(dsDepoIslm, "STORE_PROCESS");

                                }
                            }
                        }


                        using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                        {
                            if (conp1.State == ConnectionState.Closed) conp1.Open();
                            using (SqlCommand sID = new SqlCommand(@"select top (1) ID FROM 
               STORE_PROCESS  Order BY ID Desc", conp1))
                            {
                                depoIslId = Convert.ToInt32(sID.ExecuteScalar());
                            }
                        }


                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter daSDepoIslm = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from STORE_PROCESS_DETAIL", con))
                            {
                                using (SqlCommandBuilder cb1 = new SqlCommandBuilder(daSDepoIslm))
                                {
                                    DataSet dsSDepoIslm = new DataSet();
                                    daSDepoIslm.Fill(dsSDepoIslm, "STORE_PROCESS_DETAIL");

                                    DataRow ds = dsSDepoIslm.Tables["STORE_PROCESS_DETAIL"].NewRow();
                                    ds["DepoIslemID"] = depoIslId;
                                    ds["IslemTarihi"] = DateTime.Now.ToString("dd.MM.yyyy");
                                    ds["gDepoID"] = -1;
                                    ds["aDepoID"] = Session["vDepoID"].ToString();
                                    ds["urunID"] = stk.ID;
                                    ds["urunFiyat"] = stk.AlishFiyat;
                                    ds["urunMiktar"] = stk.StoktaKalan;
                                    ds["paraBirimi"] = stk.ParaBirimi;
                                    ds["variyantID"] = -1;
                                    ds["Kur"] = 1;
                                    ds["Donem"] = AyarMetot.Donem;
                                    ds["personelID"] = AyarMetot.PersonelID;
                                    ds["OzelKodID"] = -1;
                                    dsSDepoIslm.Tables["STORE_PROCESS_DETAIL"].Rows.Add(ds);
                                    daSDepoIslm.Update(dsSDepoIslm, "STORE_PROCESS_DETAIL");
                                }
                            }
                        }

                    }
                }
                else
                {
                    result = new { sonuc = 0, Message = "Aynı Stok Koduna Ait Kayıt Zaten Mevcut." };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }


            }

            else
            {

                stk = db.Stok.Where(x => x.ID == st.ID).FirstOrDefault<Stok>();
                stk.StokKodu = st.StokKodu;
                stk.UrunAdi = st.UrunAdi;
                stk.Barkod = st.Barkod;
                stk.Marka = st.Marka;
                stk.Grubu = st.Grubu;
                stk.KDV = st.KDV;
                stk.Birimi = st.Birimi;
                stk.ParaBirimi = st.ParaBirimi;
                stk.UrunTuru = st.UrunTuru;
                stk.KisaKod = st.KisaKod;
                stk.PartiNo = st.PartiNo;
                stk.Detay2 = st.Detay2;
                stk.OEM = st.OEM;
                stk.StoktaKalan = st.StoktaKalan;

                stk.P2 = st.P2;
                stk.P3 = st.P3;
                stk.P4 = st.P4;
                stk.P5 = st.P5;
                stk.P6 = st.P6;

                stk.BRM2 = st.BRM2;
                stk.BRM3 = st.BRM3;
                stk.BRM4 = st.BRM4;
                stk.BRM5 = st.BRM5;
                stk.BRM6 = st.BRM6;
                stk.BRM7 = st.BRM7;
                stk.BRM8 = st.BRM8;
                stk.BRM9 = st.BRM9;
                stk.BRM10 = st.BRM10;

                stk.nbadet2 = st.nbadet2;
                stk.nbadet3 = st.nbadet3;
                stk.nbadet4 = st.nbadet4;
                stk.nbadet5 = st.nbadet5;
                stk.nbadet6 = st.nbadet6;
                stk.nbadet7 = st.nbadet7;
                stk.nbadet8 = st.nbadet8;
                stk.nbadet9 = st.nbadet9;
                stk.nbadet10 = st.nbadet10;

                stk.Brkd2 = st.Brkd2;
                stk.Brkd3 = st.Brkd3;
                stk.Brkd4 = st.Brkd4;
                stk.Brkd5 = st.Brkd5;
                stk.Brkd6 = st.Brkd6;

                stk.Kategori1 = ktg1;
                stk.Kategori2 = ktg2;
                stk.Kategori3 = ktg3;
                stk.Kategori4 = ktg4;
                stk.Kategori5 = ktg5;
                stk.Kategori6 = ktg6;


         
                // STRİNG GÜNCELLEMELER KÜSÜRATLARIN
                //DOĞRU GİRİLMESİ İÇİN YAPILDI
                stk.SatishFiyat = satis;
                stk.AlishFiyat = alis;
                stk.F2 = f2;
                stk.F3 = f3;
                stk.F4 = f4;
                stk.F5 = f5;
                stk.F6 = f6;
                stk.F7 = f7;
                stk.F8 = f8;
                stk.F9 = f9;
                stk.F10 = f10;
                stk.KalanStkU = st.KalanStkU;
                db.SaveChanges();
                Message = "Kayıt Güncellendi";

            }

            result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult StokBilgi(int id)
        {

            using (sayazilimEntities db = new sayazilimEntities())
            {
                Stok emp = db.Stok.Where(x => x.ID == id).FirstOrDefault<Stok>();
                return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteStok(int id)
        {
            Stok emp = db.Stok.Where(x => x.ID == id).FirstOrDefault<Stok>();
            using (sayazilimEntities db = new sayazilimEntities())
            {
                using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                {
                    conp.Open();
                    SqlCommand cmd = new SqlCommand("IslemKontrolStok", conp);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StokID", emp.ID); // input parameter

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
                        if (average.Value.ToString() != "")
                        {
                            return Json(
                                new
                                {
                                    success = false,
                                    message = "Bu ürüne ait " + average.Value.ToString() +
                                              " işlemi bulunmaktadır.Silinemez"
                                }, JsonRequestBehavior.AllowGet);
                        }
                        else if (emp.StoktaKalan != 0)
                        {
                            return Json(
                                new
                                {
                                    success = false,
                                    message = "Seçili Stok Bakiyesi 0'dan Farklıdır.İşlemlerinizi Kontrol Ediniz"
                                }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            db.Stok.Attach(emp);
                            db.Stok.Remove(emp);
                            db.SaveChanges();
                            return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        db.Stok.Attach(emp);
                        db.Stok.Remove(emp);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                    }


                }



            }
        }

        public partial class StokListesi
        {
            public string ID { get; set; }
            public string StokKodu { get; set; }
            public string Miktar2 { get; set; }
            public string UrunAdi { get; set; }
            public string Grubu { get; set; }
            public string Miktar { get; set; }
            public string Marka { get; set; }
            public string Barkod { get; set; }

        }

        public ActionResult SatisListesi()
        {
            return View();
        }

        public partial class SatislarListesi
        {
            public string faturaID { get; set; }
            public string IslemTarihi { get; set; }

            public string IslemTarihiF { get; set; }
            public string VadeTarih { get; set; }
            public Nullable<int> UrunID { get; set; }
            public string UrunAdi { get; set; }
            public string Birimi { get; set; }
            public Nullable<decimal> Miktar { get; set; }
            public Nullable<decimal> StoktaKalan { get; set; }
            public Nullable<decimal> KalanStkU { get; set; }
            public string Tutar { get; set; }
            public string Tutar2 { get; set; }
            public string pBirimi { get; set; }
            public int personelID { get; set; }

            public string CariUnvan { get; set; }
        }

        public ActionResult GetSatislarListesi()
        {

            string sr = @"  ";


            string FirmaID = Session["FirmaID"].ToString();

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<SatislarListesi> yonetim = new List<SatislarListesi>();
            string sorg =
                @"select FaturaNo,IslemTarihi,CariUnvan, UrunID,INVOICE.personelID,Stok.UrunAdi,Stok.Birimi, sum(Miktar) as Miktar,StoktaKalan,KalanStkU,
                           sum(Miktar*Fiyat) as Tutar,(sum(Miktar*F2)) as Tutar2,pBirimi
                           from INVOICE_DETAIL,Stok ,INVOICE 
						   where UrunID=Stok.ID 
						   and (IslemTuru=N'Sevk İrsaliyesi' or 
						   IslemTuru=N'Satış Faturası') AND faturaID=INVOICE.ID" + sr + " and INVOICE.FirmaID =" + FirmaID +

                @"group by UrunID,UrunAdi,Birimi,pBirimi,CariUnvan,
						   IslemTarihi,StoktaKalan,KalanStkU,INVOICE.personelID,FaturaNo";

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
                                SatislarListesi yt = new SatislarListesi();
                                yt.CariUnvan = dr["CariUnvan"].ToString();
                                yt.personelID = Convert.ToInt32(dr["personelID"]);
                                yt.faturaID = dr["FaturaNo"].ToString();
                                yt.IslemTarihi = dr["IslemTarihi"].ToString();
                                yt.IslemTarihiF = Convert.ToDateTime(dr["IslemTarihi"]).ToString("dd.MM.yyyy");
                                yt.VadeTarih = dr["IslemTarihi"].ToString();
                                yt.UrunID = Convert.ToInt32(dr["UrunID"]);
                                yt.UrunAdi = dr["UrunAdi"].ToString();
                                yt.Birimi = dr["Birimi"].ToString();
                                try
                                {
                                    yt.Miktar = Convert.ToDecimal(dr["Miktar"]);
                                }
                                catch
                                {
                                    yt.Miktar = 0;
                                }

                                try
                                {
                                    yt.StoktaKalan = Convert.ToDecimal(dr["StoktaKalan"]);
                                }
                                catch
                                {
                                    yt.StoktaKalan = 0;
                                }

                                try
                                {
                                    yt.KalanStkU = Convert.ToDecimal(dr["KalanStkU"]);
                                }
                                catch
                                {
                                    yt.KalanStkU = 0;
                                }


                                yt.Tutar = Convert.ToDecimal(dr["Tutar"]).ToString("N2");
                                yt.Tutar2 = Convert.ToDecimal(dr["Tutar2"]).ToString("N2");
                                yt.pBirimi = dr["pBirimi"].ToString();
                                yonetim.Add(yt);

                            }
                        }
                    }
                }




            }
            catch (Exception e1)
            {
                System.IO.File.WriteAllText(Path.Combine(@"C:\Users\ilhan\AppData\Local\Sayazilim", "sonuç.xml"),
                    e1.ToString());
            }


            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStok()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<StokListesi> yonetim = new List<StokListesi>();
            string sorg = @"Select ID,StokKodu,UrunAdi,Grubu,(StoktaKalan) as Miktar,Marka,Barkod from Stok where WebPGoster=1 and FirmaID= " + FirmaID;

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

                                yt.ID = dr["ID"].ToString();
                                yt.StokKodu = dr["StokKodu"].ToString();
                                yt.UrunAdi = dr["UrunAdi"].ToString();
                                yt.Grubu = dr["Grubu"].ToString();
                                try
                                {
                                    string miktar = Convert.ToDecimal(dr["Miktar"]).ToString("N2");
                                    yt.Miktar = miktar;
                                }
                                catch (Exception e)
                                {
                                    yt.Miktar = "0";
                                }

                                yt.Marka = dr["Marka"].ToString();
                                yt.Barkod = dr["Barkod"].ToString();
                                int agacid = Convert.ToInt32(yt.ID);
                                var agaclist = db.TREE_PRODUCT.Where(x => x.AgacID == agacid).ToList();
                                if (agaclist.Count != 0)
                                {
                                    foreach (var item in agaclist)
                                    {
                                        try
                                        {
                                            int i1 = Convert.ToInt32(item.Miktar);

                                            int i2 = Convert.ToInt32(yt.Miktar);
                                            int sonuc = i1 * i2;
                                            int urunid = Convert.ToInt32(item.UrunID);
                                            Stok st = db.Stok.Where(x => x.ID == urunid).FirstOrDefault();
                                            yt.Miktar2 = sonuc.ToString() + " " + st.UrunAdi;
                                        }

                                        catch { }
                                    }
                                }
                                else
                                {
                                    yt.Miktar2 = "0";

                                }


                                yonetim.Add(yt);

                            }
                        }
                    }
                }




            }
            catch (Exception e1)
            {
                System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"),
                    e1.ToString());
            }


            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StokHareketler(int id)
        {
            ViewBag.StokHareketlerId = id;

            return View();
        }

        public ActionResult GetStokHareketler(int id)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<ProductHareket> yonetim = new List<ProductHareket>();
            string sorg = @"SET DATEFORMAT DMY;select IslemTuru,UrunID,IslemTarihi,Miktar,gBirimMiktar,SatirParaBirimi,YerelBrut,FaturaNo from PRODUCT_HAREKET where UrunID=" + id;

            decimal alacak = 0;
            decimal borc = 0;

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
                                ProductHareket yt = new ProductHareket();
                                yt.IslemTuru = dr["IslemTuru"].ToString();
                                yt.FaturaNo = dr["FaturaNo"].ToString();
                                yt.SatirParaBirimi = dr["SatirParaBirimi"].ToString();
                                yt.IslemTarihi = Convert.ToDateTime(dr["IslemTarihi"].ToString());
                                yt.Miktar = Convert.ToDecimal(dr["Miktar"].ToString()).ToString("N2");
                                yt.gBirimMiktar = Convert.ToDecimal(dr["YerelBrut"].ToString()).ToString("N2");

                                yt.YerelBrut = Convert.ToDecimal(Convert.ToDecimal(yt.Miktar) * Convert.ToDecimal(yt.gBirimMiktar)).ToString("N2");

                                yt.UrunID = Convert.ToInt32(dr["UrunID"].ToString());
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

        public ActionResult UrunAgacı()
        {
            TREE_PRODUCT tp = new TREE_PRODUCT();

            return View(tp);
        }

        public ActionResult GetUrunAgacları()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<TreeProduct> yonetim = new List<TreeProduct>();
            string sorg =
                @"set dateformat dmy; sELECT distinct 'UG' AS [-],  (Select Stok.Stokkodu from Stok where ID=AgacID) as [KOD],
                     (Select Stok.UrunAdi from Stok where ID=AgacID) as [AD],(Select Stok.kategori1 from Stok where ID=AgacID) 
					 as [K1],(Select Stok.kategori2 from Stok where ID=AgacID) as [K2],(Select Stok.kategori3 from Stok where ID=AgacID) 
					 as [K3],(Select Stok.kategori4 from Stok where ID=AgacID) as [K4],(Select Stok.kategori5 from Stok where ID=AgacID) as [K5],
                     TREE_PRODUCT.Aciklama,
 CONVERT(VARCHAR(10), KayitTarih, 104) AS [Kayıt Tarihi],AgacID  
FROM TREE_PRODUCT   group by AgacID,Aciklama, CONVERT(VARCHAR(10), KayitTarih, 104) ";

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
                                TreeProduct yt = new TreeProduct();

                                yt.Kod = dr["KOD"].ToString();
                                yt.AgacID = dr["AgacID"].ToString();

                                yt.Acıklama = dr["Aciklama"].ToString();
                                yt.KayitTarihi = dr["Kayıt Tarihi"].ToString();
                                yt.UrunAdı = dr["AD"].ToString();
                                yonetim.Add(yt);

                            }
                        }
                    }
                }




            }
            catch (Exception e1)
            {
                System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Hatice\AppData\Local\Sayazilim", "sonuç.xml"),
                    e1.ToString());
            }


            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }
        public partial class TreeProduct
        {
            public string ID { get; set; }
            public string AgacID { get; set; }
            public string Acıklama { get; set; }
            public string UrunAdı { get; set; }
            public string Kod { get; set; }
            public string KayitTarihi { get; set; }

        }

        [HttpPost]
        public ActionResult DeleteUrunAgac(int id)
        {
            TREE_PRODUCT emp = db.TREE_PRODUCT.Where(x => x.AgacID == id).FirstOrDefault<TREE_PRODUCT>();
            var treelist = db.TREE_PRODUCT.Where(x => x.AgacID == id).ToList();
            using (sayazilimEntities db = new sayazilimEntities())
            {
                using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                {
                    conp.Open();
                    SqlCommand cmd = new SqlCommand("IslemKontrolStok", conp);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StokID", emp.ID); // input parameter

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
                        if (average.Value.ToString() != "")
                        {
                            return Json(
                                new
                                {
                                    success = false,
                                    message = "Bu ürüne ait " + average.Value.ToString() +
                                              " işlemi bulunmaktadır.Silinemez"
                                }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {


                            foreach (var item in treelist)
                            {
                                db.TREE_PRODUCT.Attach(item);
                                db.TREE_PRODUCT.Remove(item);

                            }
                            db.SaveChanges();
                            return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        foreach (var item in treelist)
                        {
                            db.TREE_PRODUCT.Attach(item);
                            db.TREE_PRODUCT.Remove(item);

                        }
                        db.SaveChanges();

                        return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }

        public ActionResult YeniUrunAgaci(int id = 0)
        {
            TREE_PRODUCT tp = new TREE_PRODUCT();
            if (id != 0)
            {

                var servisdetaylari = db.TREE_PRODUCT.Where(x => x.AgacID == id).ToList<TREE_PRODUCT>();
                tp = servisdetaylari[0];
                ViewBag.Detay2 = servisdetaylari.ToList();


                List<FaturaDetaylariEk> ftr = new List<FaturaDetaylariEk>();
                for (int i = 0; i < servisdetaylari.Count; i++)
                {
                    TREE_PRODUCT td = servisdetaylari[i];
                    FaturaDetaylariEk stkek = new FaturaDetaylariEk();
                    stkek.UrunAdi = AyarMetot.StokBilgiCek(Convert.ToInt32(td.UrunID), "UrunAdi");

                    ftr.Add(stkek);

                }

                ViewBag.FaturaEk2 = ftr;

            }


            ;
            return View(tp);
        }

        [HttpPost]
        public JsonResult YeniUrunAgaci(Array[] data, string indirim, string AgacID, string Aciklama)
        {

            for (int i = 0; i < data.Length; i++)
            {
                string Mik = "";
                decimal Fiyat = 0;
                int UrunID = -1;
                string Birim = "Adet";
                string ID = "-1";
                int kolon = 0;

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
                        ID = veri.ToString();
                    }


                    kolon++;
                }
                kolon = 0;

                decimal Miktar = Convert.ToDecimal(Mik.Replace(".", ","));

                try
                {
                    if (ID == "-1")
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from TREE_PRODUCT", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "TREE_PRODUCT");
                                    DataRow df = ds.Tables["TREE_PRODUCT"].NewRow();

                                    df["AgacID"] = AgacID;
                                    df["KayitTarih"] = DateTime.Now;
                                    df["UrunID"] = UrunID;
                                    df["Birim"] = Birim;
                                    df["Miktar"] = Miktar;
                                    df["AlishFiyat"] = 0;
                                    df["SatishFiyat"] = Fiyat;
                                    df["Aciklama"] = Aciklama;
                                    df["BirimAdet"] = 1;


                                    ds.Tables["TREE_PRODUCT"].Rows.Add(df);
                                    da.Update(ds, "TREE_PRODUCT");
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
                                    "select * from TREE_PRODUCT where ID='" + ID + "'", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "TREE_PRODUCT");
                                    DataRow[] adf = ds.Tables["TREE_PRODUCT"].Select("ID='" + ID + "'");
                                    if (adf.Length != 0)
                                    {
                                        DataRow df = adf[0];
                                        df["UrunID"] = UrunID;
                                        df["Birim"] = Birim;
                                        df["Miktar"] = Miktar;
                                        df["AlishFiyat"] = 0;
                                        df["SatishFiyat"] = Fiyat;
                                        da.Update(ds, "TREE_PRODUCT");

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



            return Json(new { sonuc = "1", Mesaj = "Başarılı" });

        }

        [HttpPost]
        public JsonResult DeleteTreeProc(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                TREE_PRODUCT emp = db.TREE_PRODUCT.Where(x => x.ID == id).FirstOrDefault<TREE_PRODUCT>();
                db.TREE_PRODUCT.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult DeleteStokHareket(int id)
        {

            try
            {
                STORE_PROCESS_DETAIL spd = db.STORE_PROCESS_DETAIL.Where(x => x.urunID == id).FirstOrDefault<STORE_PROCESS_DETAIL>();

                STORE_PROCESS sp = db.STORE_PROCESS.Where(x => x.ID == spd.DepoIslemID).FirstOrDefault<STORE_PROCESS>();


                db.STORE_PROCESS.Remove(sp);
                db.STORE_PROCESS_DETAIL.Remove(spd);
                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            {
                return Json(new { success = false, message = "Kayıt Silinemedi!" }, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult GetVaryant(int id)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            string FirmaID = Session["FirmaID"].ToString();
            List<VARIANT> yonetim = new List<VARIANT>();

            string sorg = @"select * from VARIANT where UrunID=" + id;

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
                                VARIANT yt = new VARIANT();

                                yt.ID = Convert.ToInt32(dr["ID"]);
                                yt.RenkAdi = dr["RenkAdi"].ToString();
                                yt.Beden = dr["Beden"].ToString();
                                yt.Fiyat = Convert.ToDecimal(Convert.ToDecimal(dr["Fiyat"]).ToString("N2"));

                                try
                                {
                                    string miktar = Convert.ToDecimal(dr["Miktar"]).ToString("N2");
                                    yt.Miktar = Convert.ToDecimal(miktar);
                                }
                                catch (Exception e)
                                {
                                    yt.Miktar = 0;
                                }
                                yt.Aciklama = dr["Aciklama"].ToString();
                                yt.Barkod = dr["Barkod"].ToString();


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
                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Hatice\AppData\Local\Sayazilim", "sonuç.xml"),
                        e1.ToString());
                }
                catch { }
            }


            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult YeniVaryant(VARIANT vr)
        {
            try
            {
                VARIANT varyant = new VARIANT();
                varyant = vr;
                varyant.Aciklama = vr.Aciklama;
                varyant.Barkod = vr.Barkod;
                varyant.RenkAdi = vr.RenkAdi;
                varyant.Beden = vr.Beden;
                varyant.Fiyat = vr.Fiyat;
                varyant.Renk = vr.RenkAdi;
                varyant.Sezon = DateTime.Now.Year.ToString();
                varyant.Renk = "255;255;255";

                varyant.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());


                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                varyant.Company_Code = company_code;

                db.VARIANT.Add(varyant);
                db.SaveChanges();
                return Json(new { sonuc = "1", Mesaj = "Başarılı!" });
            }
            catch (Exception e)
            {
                return Json(new { sonuc = "0", Mesaj = "Kayıt Eklenirken Hata Oluştu!" });
            }

        }


        public ActionResult VariantList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetVariantList()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<VariantList> yonetim = new List<VariantList>();
            string FirmaID = Session["FirmaID"].ToString();
            string sorg = @"select (select UrunAdi from Stok s where s.ID=v.UrunID) as UrunAdi,RenkAdi,Beden,Sezon,Fiyat,Miktar,Aciklama,Barkod from VARIANT v Where v.FirmaID=" + Session["FirmaID"].ToString();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand dekontgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = dekontgetir.ExecuteReader())
                    {
                        int i = 1;
                        while (dr.Read())
                        {


                            VariantList yt = new VariantList();
                            yt.Counter = i.ToString();
                            yt.UrunAdi = dr["UrunAdi"].ToString();
                            yt.RenkAdi = dr["RenkAdi"].ToString();
                            yt.Beden = dr["RenkAdi"].ToString();
                            yt.Sezon = dr["Sezon"].ToString();
                            yt.Fiyat = Convert.ToDecimal(dr["Fiyat"].ToString()).ToString("N2");
                            yt.Miktar = Convert.ToDecimal(dr["Miktar"].ToString()).ToString("N2");
                            yt.Aciklama = dr["Aciklama"].ToString();
                            yt.Barkod = dr["Barkod"].ToString();
                            yonetim.Add(yt);

                        }
                    }
                }
            }


            return Json(new { data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}