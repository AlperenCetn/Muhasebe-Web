using SaTeknopark_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class DepoController : Controller
    {
        // GET: Depo
        public ActionResult Index()
        {
            return View();
        }
        sayazilimEntities db = new sayazilimEntities();

        public ActionResult DepoTransfer()
        {
            var st = db.Stok.ToList().OrderBy(x => x.ID);
            return View(st.ToList());
        }


        [HttpPost]
        public JsonResult DepoTransfer(Array[] data, string indirim, string GDepo, string ADepo, string Aciklama)
        {

            //INB sts = new Satis();
            int kolon = 0;
            //sts.Tarih = DateTime.Now;
            //sts.SiparisNo = DateTime.Now.ToString("dd.MM.yyyy");
            //sts.Indirim = Convert.ToDecimal(indirim) / Convert.ToDecimal(data.Length);
            //sts.PersonelID = 1;
            //if (musID != "-1") sts.MusteriID = Convert.ToInt32(musID);

            string IslemKodu = "";
            using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
            {
                if (conp1.State == ConnectionState.Closed) conp1.Open();
                using (SqlCommand sID = new SqlCommand("select top (1) ID FROM STORE_PROCESS Order BY ID Desc", conp1))
                {
                    IslemKodu = "DP" + Convert.ToInt32(sID.ExecuteScalar());
                }
            }
            int DepoIslemID = -1;
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
                using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from STORE_PROCESS", con))
                {
                    using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "STORE_PROCESS");
                        DataRow dr = ds.Tables["STORE_PROCESS"].NewRow();

                        dr["IslemKodu"] = IslemKodu;
                        dr["IslemTarih"] = DateTime.Now;
                        dr["GonderenDepoID"] = GDepo;
                        dr["AlanDepoID"] = ADepo;
                        dr["Aciklama"] = Aciklama;
                        //tutar
                        dr["paraBirimi"] = "TL";
                        dr["personelID"] = PersonelID;
                        dr["Donem"] = DateTime.Now.Year;
                        dr["KayitpersonelID"] = PersonelID;
                        dr["KayitTarih"] = DateTime.Now;
                        dr["DepoSayimID"] = -1;
                        dr["EmirID"] = -1;
                        dr["GirisTuru"] = "";
                        dr["TCariID"] = -1;
                        dr["FaturaSatirID"] = -1;
                        dr["SiparisIDHFT"] = -1;
                        dr["Kur"] = 1;
                        dr["DolarKur"] = 1;
                        dr["EuroKur"] = 1;

                        ds.Tables["STORE_PROCESS"].Rows.Add(dr);
                        da.Update(ds, "STORE_PROCESS");

                    }
                }
            }



            using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
            {
                if (conp1.State == ConnectionState.Closed) conp1.Open();
                using (SqlCommand sID = new SqlCommand(@"select top (1) ID FROM 
               STORE_PROCESS where PersonelID=" + PersonelID + " Order BY ID Desc", conp1))
                {
                    DepoIslemID = Convert.ToInt32(sID.ExecuteScalar());
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


                    kolon++;
                }
                kolon = 0;
                decimal Miktar = decimal.Parse(Mik, CultureInfo.InvariantCulture);



                try
                {

                    using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from STORE_PROCESS_DETAIL", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "STORE_PROCESS_DETAIL");
                                DataRow df = ds.Tables["STORE_PROCESS_DETAIL"].NewRow();

                                df["DepoIslemID"] = DepoIslemID;
                                df["IslemTarihi"] = DateTime.Now;
                                df["gDepoID"] = GDepo;
                                df["aDepoID"] = ADepo;
                                df["PersonelID"] = PersonelID;
                                df["UrunID"] = UrunID;
                                df["urunFiyat"] = Fiyat;
                                df["urunMiktar"] = Miktar;
                                df["paraBirimi"] = AyarMetot.PB_Short;
                                df["urunBirim"] = Birim;
                                df["variyantID"] = -1;
                                df["Kur"] = 1;
                                df["Donem"] = DateTime.Now.Year;
                                df["IslemTipi"] = "Depo İşlem";

                                df["OzelKodID"] = -1;
                                df["KDV"] = Kdv;
                                df["DepoSayimID"] = -1;
                                df["EmirID"] = -1;
                                df["SeriNo"] = "";
                                df["Aciklama"] = "";
                                df["GirisTuru"] = "";
                                df["TCariID"] = -1;
                                df["TakimID"] = -1;
                                df["UrunSeriNoMob"] = -1;
                                df["SayimMiktari"] = 0;
                                df["FaturaSatirID"] = -1;
                                df["SiparisIDHFT"] = -1;
                                df["KodID"] = -1;


                                ds.Tables["STORE_PROCESS_DETAIL"].Rows.Add(df);
                                da.Update(ds, "STORE_PROCESS_DETAIL");
                            }
                        }
                    }

                }
                catch 
                {



                }
            }

            try
            {
                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    conp1.Open();

                    using (SqlCommand d6 = new SqlCommand(@"update STORE_PROCESS SET Tutar=
                            (Select coalesce( SUM((urunMiktar*urunFiyat)),0) 
                            from STORE_PROCESS_DETAIL WHERE DepoIslemID=STORE_PROCESS.ID) where ID=" + DepoIslemID, conp1))
                    {
                        d6.ExecuteNonQuery();
                    }



                }
            }
            catch 
            {

            }


            return Json(new { sonuc = "1", Mesaj = "Başarılı", id = DepoIslemID });

        }

        //

    }
}