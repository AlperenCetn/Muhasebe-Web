using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaTeknopark_MVC5.Models;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class ExcelController : Controller
    {
        // GET: Excel
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Resim file)
        {

            if (file.Dosya.ContentLength > 0)
            {


                var filenamecmr = Path.GetFileName(file.Dosya.FileName);
                var kayityeri = Path.Combine(@"C:/FRANCBELGELER", filenamecmr);
                if (System.IO.File.Exists(kayityeri))
                {
                    kayityeri = Path.Combine(@"C:/FRANCBELGELER", DateTime.Now.ToString("HHmmss") + filenamecmr);
                }
                else
                {

                }

                file.Dosya.SaveAs(kayityeri);


                DataTable dt = ConvertExcelToDataTable(kayityeri);

                int EmpID = -1;
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

                try
                {
                    using (SqlConnection conp = new SqlConnection(strcon))
                    {
                        if (conp.State == ConnectionState.Closed) conp.Open();
                        using (SqlCommand command = new SqlCommand("SELECT MAX(ID)+1 From Cari", conp))
                        {
                            EmpID = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }
                }
                catch { }
                if (EmpID == -1 || EmpID == 0) EmpID = 1;

                try
                {
                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\alper\AppData\Local\Sayazilim", "sonuç.xml"), dt.Rows.Count.ToString());

                }
                catch
                { }

                int aktarimsay = 0;

                using (sayazilimEntities db = new sayazilimEntities())
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Yetkili = "";

                        bool kayitvar = false;


                        using (SqlConnection con = new SqlConnection(strcon))
                        {
                            con.Open();
                            using (SqlCommand carigetir = new SqlCommand(@"SELECT ID From Cari where ID='" + Convert.ToInt32(file.ID) + "'", con))
                            {
                                using (SqlDataReader dr = carigetir.ExecuteReader())
                                {
                                    while (dr.Read())
                                    {
                                        kayitvar = true;

                                    }
                                }
                            }
                        }

                        string FirmaKodu = "";
                        if (kayitvar == false)
                        {
                            try { FirmaKodu = dt.Rows[i]["Kod"].ToString(); } catch { }

                            int FirmaID = Convert.ToInt32(Session["FirmaID"].ToString());
                            Cari cr = db.Cari.Where(x => x.FirmaKodu == FirmaKodu && x.FirmaID == FirmaID).FirstOrDefault<Cari>();

                            if (cr == null)
                            {

                                if (FirmaKodu != "")
                                {
                                    string SonGorusme = "";




                                    DateTime Duzenleme = DateTime.Now;
                                    try { Duzenleme = Convert.ToDateTime(dt.Rows[i]["Duzenleme"]); } catch { }



                                    string BasvuruSira = "BN" + EmpID.ToString();
                                    string FirmaSira = "F" + EmpID.ToString();
                                    Cari kayit = new Cari();

                                    kayit.CariUnvan = dt.Rows[i]["Müşteri Adı"].ToString();
                                    try
                                    {
                                        kayit.KTipi = dt.Rows[i]["C.Tipi"].ToString();
                                    }
                                    catch { }
                                    kayit.Adres = dt.Rows[i]["Grubu"].ToString();
                                    kayit.GSM = dt.Rows[i]["İletişim Bilgileri"].ToString();

                                    kayit.FirmaKodu = FirmaKodu;
                                    kayit.paraBirimi = "TL";
                                    string PersonelAdi = dt.Rows[i]["Temsilci"].ToString();

                                    Personel pr = db.Personel.Where(x => x.Adi == PersonelAdi || x.FirmaID == FirmaID).FirstOrDefault<Personel>();

                                    if (pr != null)
                                    {
                                        kayit.MusteriTemsilcisi = pr.ID;
                                    }

                                    try { kayit.Il = dt.Rows[i]["Şehir"].ToString(); } catch { }
                                    try { kayit.Ilce = dt.Rows[i]["İlçe"].ToString(); } catch { }

                                    db.Cari.Add(kayit);
                                    db.SaveChanges();
                                    aktarimsay++;
                                    EmpID++;
                                    decimal alacak = Convert.ToDecimal(dt.Rows[i]["Alacak"].ToString());
                                    decimal borc = Convert.ToDecimal(dt.Rows[i]["Borç"].ToString());

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
                            }
                        }
                    }
                }

                ViewBag.Aktarilan = "Başarılı Aktarılan Cari Sayısı: "+aktarimsay.ToString();
                ViewBag.Sayi = "Toplam Cari Sayısı :" +aktarimsay.ToString();
                ViewBag.Success = "CARİLER BAŞARIYLA AKTARILDI";

            }
            //if the code reach here means everthing goes fine and excel data is imported into database


            return View();
        }


        public ActionResult Data(int MarkaID = 0)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Data(Resim file)
        {

            if (file.Dosya.ContentLength > 0)
            {


                var filenamecmr = Path.GetFileName(file.Dosya.FileName);
                var kayityeri = Path.Combine(@"C:/FRANCBELGELER", filenamecmr);
                if (System.IO.File.Exists(kayityeri))
                {
                    kayityeri = Path.Combine(@"C:/FRANCBELGELER", filenamecmr);
                }
                else
                {

                }

                file.Dosya.SaveAs(kayityeri);


                DataTable dt = ConvertExcelToDataTable(kayityeri);

                int EmpID = -1;
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

                try
                {
                    using (SqlConnection conp = new SqlConnection(strcon))
                    {
                        if (conp.State == ConnectionState.Closed) conp.Open();
                        using (SqlCommand command = new SqlCommand("SELECT MAX(ID)+1 From Stok", conp))
                        {
                            EmpID = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }
                }
                catch { }
                if (EmpID == -1 || EmpID == 0) EmpID = 1;

                try
                {
                    System.IO.File.WriteAllText(Path.Combine(@"C:\Users\alper\AppData\Local\Sayazilim", "sonuç.xml"), dt.Rows.Count.ToString());

                }
                catch
                { }

                int aktarimsay = 0;

                using (sayazilimEntities db = new sayazilimEntities())
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Yetkili = "";

                        bool kayitvar = false;


                        if (kayitvar == false)
                        {

                            using (SqlConnection conp = new SqlConnection(strcon))
                            {
                                if (conp.State == ConnectionState.Closed) conp.Open();
                                using (SqlDataAdapter daBASVURULAR = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from Stok", strcon))
                                {
                                    using (SqlCommandBuilder cb = new SqlCommandBuilder(daBASVURULAR))
                                    {
                                        try
                                        {
                                            DateTime Duzenleme = DateTime.Now;

                                            string stokkodu = dt.Rows[i]["Kod"].ToString();
                                            short firmaID = Convert.ToInt16(Session["FirmaID"].ToString());

                                            Stok stk = db.Stok.Where(x => x.FirmaID == firmaID && x.StokKodu == stokkodu).FirstOrDefault<Stok>();
                                            Stok st = new Stok();
                                            if (stk != null)
                                            {
                                                AyarMetot.Siradaki("", "Stok", "StokKodu", Session["FirmaID"].ToString());
                                                st.StokKodu = AyarMetot.GetNumara;

                                            }
                                            else
                                            {
                                                st.StokKodu = stokkodu;
                                            }

                                            st.UrunAdi = dt.Rows[i]["Ad"].ToString();
                                            try { st.Marka = dt.Rows[i]["Marka"].ToString(); } catch { };
                                            try { st.Grubu = dt.Rows[i]["Grubu"].ToString(); } catch { };
                                            try { st.StoktaKalan = Convert.ToInt32(dt.Rows[i]["Ürün Miktarı"].ToString()); } catch { };
                                            try { st.Kategori1 = dt.Rows[i]["Kategori1"].ToString(); } catch { };
                                            try { st.Kategori2 = dt.Rows[i]["Kategori2"].ToString(); } catch { };
                                            try { st.Kategori3 = dt.Rows[i]["Kategori3"].ToString(); } catch { };
                                            try { st.Kategori4 = dt.Rows[i]["Kategori4"].ToString(); } catch { };
                                            try { st.UrunTuru = dt.Rows[i]["Ürün Türü"].ToString(); } catch { };
                                            try { st.AlishFiyat = Convert.ToInt32(dt.Rows[i]["Alış Fiyatı"].ToString()); } catch { };
                                            try { st.SatishFiyat = Convert.ToInt32(dt.Rows[i]["Satış Fiyat"].ToString()); } catch { };
                                            try { st.KDV = Convert.ToInt32(dt.Rows[i]["Kdv"].ToString()); } catch { };
                                            try { st.ParaBirimi = dt.Rows[i]["Satış PB"].ToString(); } catch { };
                                            try { st.Barkod = dt.Rows[i]["Barkod"].ToString(); } catch { };
                                            try { st.F2 = Convert.ToInt32(dt.Rows[i]["F2"].ToString()); } catch { };
                                            try { st.F4 = Convert.ToInt32(dt.Rows[i]["F4"].ToString()); } catch { };
                                            try { st.F5 = Convert.ToInt32(dt.Rows[i]["F5"].ToString()); } catch { };
                                            try { st.F3 = Convert.ToInt32(dt.Rows[i]["F3"].ToString()); } catch { };
                                            try
                                            {
                                                string depoadi = dt.Rows[i]["Depo Adı"].ToString();
                                                STORE store = db.STORE.Where(x =>
                                                        x.DepoAdi == depoadi &&
                                                        x.FirmaID == firmaID)
                                                    .FirstOrDefault<STORE>();

                                                st.SdepoID = store.ID;

                                            }
                                            catch { }

                                            st.FirmaID = firmaID;
                                            string firmaid = Session["FirmaID"].ToString();
                                            string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                            st.Company_Code = company_code;


                                            aktarimsay++;

                                            db.Stok.Add(st);
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


                                            EmpID++;
                                        }
                                        catch (Exception E1)
                                        {
                                            try
                                            {
                                                System.IO.File.WriteAllText(Path.Combine(@"C:\Users\alper\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());

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
                ViewBag.Aktarilan = "Başarılı Aktarılan Stok Sayısı: " +aktarimsay.ToString();
                ViewBag.Sayi = "Toplam Stok Sayısı: "+aktarimsay.ToString();
                ViewBag.Success = "STOKLAR BAŞARIYLA AKTARILDI";



            }
            //if the code reach here means everthing goes fine and excel data is imported into database


            return View();
        }


        public class Resim
        {
            public HttpPostedFileBase Dosya { get; set; }
            public int ID { get; set; }
        }

        public static DataTable ConvertExcelToDataTable(string FileName)
        {

            OleDbConnection theConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + FileName + "; Extended Properties='Excel 12.0; TypeGuessRows=0; HDR=YES; IMEX=1'");
            theConnection.Open();

            System.Data.DataTable dataSet = theConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string[] workSheetNames = new String[dataSet.Rows.Count];

            int y = 0;
            string sayfaadi = "Cari$";
            foreach (DataRow row in dataSet.Rows)
            {
                workSheetNames[y] = row["TABLE_NAME"].ToString().Replace("'", "");
                if (y == 0)
                    sayfaadi = workSheetNames[y];
                y++;
            }


            OleDbDataAdapter theDataAdapter = new OleDbDataAdapter("SELECT * FROM [ExcelListeAktarim$]", theConnection);
            DataSet theDS = new DataSet();
            DataTable dt = new DataTable();
            theDataAdapter.Fill(dt);

            return dt;

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


    }
}