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
    public class PersonelController : Controller
    {
        // GET: Personel

        // GET: Personel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Personeller()
        {
            return View();
        }




        public ActionResult GetPersonel()
        {
            string FirmaID = Session["FirmaID"].ToString();
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<Personel> yonetim = new List<Personel>();
            string sorg = @"select ID,Adi,Soyadi,PersonelGrubu,Adres,EkBilgiler,Milleti,TCNo,Cinsiyet,Telefon1,Telefon2 from Personel where FirmaID ="+ FirmaID;
            if (Convert.ToInt32(Session["sCariID"].ToString()) != -1)
            {
               sorg = @"select ID,Adi,Soyadi,PersonelGrubu,Adres,EkBilgiler,Milleti,TCNo,Cinsiyet,Telefon1,Telefon2 from Personel where sCariID ="+ Session["sCariID"].ToString() + "And FirmaID ="+ FirmaID;
            }


            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand servisgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = servisgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Personel yt = new Personel();

                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.Adi = dr["Adi"].ToString();
                            yt.Soyadi = dr["Soyadi"].ToString();
                            yt.PersonelGrubu = dr["PersonelGrubu"].ToString();
                            yt.Milleti = dr["Milleti"].ToString();
                            yt.TCNo = dr["TCNo"].ToString();
                            yt.Cinsiyet = dr["Cinsiyet"].ToString();
                            yt.Telefon1 = Convert.ToString(dr["Telefon1"]);
                            if (yt.Telefon1 == "")
                            {
                                yt.Telefon1 = dr["Telefon2"].ToString();
                            }
                            yt.Adres = dr["Adres"].ToString();
                            yt.EkBilgiler = dr["EkBilgiler"].ToString();

                            yonetim.Add(yt);


                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);
        }



        sayazilimEntities db = new sayazilimEntities();
        [HttpPost]
        public JsonResult yeniPersonel(Personel pr, Yetkiler yt)
        {
            string Message = "Kayıt Eklendi";
            var result = new {sonuc = 0, Message = Message};
            Personel per = null;
            
            if (pr.ID == -1)
            {
                per = new Personel();
                per = pr;
                per.PB = "Varsayılan";
                per.vKasaID = pr.vKasaID;
                per.vBankaID = pr.vBankaID;

                per.sCariID = Convert.ToInt32(Session["sCariID"].ToString());
                per.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                per.Company_Code = company_code;
                try
                {
                    per.WebSifre = GirisKontrol.hash(pr.WebSifre, true);
                }
                catch (Exception e)
                {

                }


                try
                {
                    db.Personel.Add(per);
                    db.SaveChanges();

                    Personel person = db.Personel.OrderByDescending(x => x.ID).FirstOrDefault<Personel>();

                    Yetkiler yetki = new Yetkiler();
                    yetki = yt;

                    yetki.PersonelID = person.ID;
                    yetki.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                    firmaid = Session["FirmaID"].ToString();
                    company_code = "SA01" + firmaid.PadLeft(3, '0');
                    yetki.Company_Code = company_code;
                    db.Yetkiler.Add(yetki);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Message = "Kayıt eklenirken bir hata oluştu.";
                    result = new { sonuc = 0, Message = Message };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            
                



            }
            else
            {
                per = db.Personel.Where(x => x.ID == pr.ID).FirstOrDefault<Personel>();


                per.ID = pr.ID;
                per.Adi = pr.Adi;
                per.Soyadi = pr.Soyadi;
                per.PersonelGrubu = pr.PersonelGrubu;
                per.Milleti = pr.Milleti;
                per.TCNo = pr.TCNo;
                per.Cinsiyet = pr.Cinsiyet;
                per.Telefon1 = pr.Telefon1;
                per.Adres = pr.Adres;
                per.EkBilgiler = pr.EkBilgiler;
                per.BabaAdi = pr.BabaAdi;
                per.Maasi = pr.Maasi;
                per.IL = pr.IL;
                per.Ilce = pr.Ilce;
                per.EPosta = pr.EPosta;
                per.SicilNo = pr.SicilNo;
                per.WebKA = pr.WebKA;

                try
                {
                    per.WebSifre = GirisKontrol.hash(pr.WebSifre, true);
                }
                catch (Exception e)
                {

                }


                per.Telefon2 = pr.Telefon2;
                per.vKasaID = pr.vKasaID;
                per.vBankaID = pr.vBankaID;


                Yetkiler yetki = db.Yetkiler.Where(x=>x.PersonelID == per.ID).FirstOrDefault<Yetkiler>();


                yetki.StokEG = yt.StokEG;
                yetki.StokSil = yt.StokSil;
                yetki.CariEG = yt.CariEG;
                yetki.CariSil = yt.CariSil;
                yetki.FaturaEG = yt.FaturaEG;
                yetki.FaturaSil = yt.FaturaSil;
                yetki.KasaEG = yt.KasaEG;
                yetki.KasaSil = yt.KasaSil;
                yetki.BankaEG = yt.BankaEG;
                yetki.BankaSil = yt.BankaSil;
                yetki.ServisEG = yt.ServisEG;
                yetki.ServisSil = yt.ServisSil;
                yetki.PersonelEG = yt.PersonelEG;
                yetki.PersonelSil = yt.PersonelSil;
                yetki.KrediKEG = yt.KrediKEG;
                yetki.KrediKSil = yt.KrediKSil;
                yetki.MaasEG = yt.MaasEG;
                yetki.MaasSil = yt.MaasSil;

                yetki.YazdirSec = yt.YazdirSec;
                yetki.KasaIslem = yt.KasaIslem;
                yetki.BankaIslem = yt.BankaIslem;
                yetki.ServisIslem = yt.ServisIslem;
                yetki.PersonelGorme = yt.PersonelGorme;
                yetki.IslemSilme = yt.IslemSilme;
                yetki.RaporGorme = yt.RaporGorme;
                yetki.Hatirlatma = yt.Hatirlatma;
                yetki.GelirGiderIslem = yt.GelirGiderIslem;
                yetki.MesajGonderme = yt.MesajGonderme;
                yetki.IslemGorme = yt.IslemGorme;
                yetki.DepoGorme = yt.DepoGorme;
                yetki.CariKartGor = yt.CariKartGor;
                yetki.CariBakiyeGor = yt.CariBakiyeGor;
                yetki.HizliSatisMenusuGor = yt.HizliSatisMenusuGor;
                yetki.SadeceKendiCarisi = yt.SadeceKendiCarisi;
                yetki.StokKartGor = yt.StokKartGor;
                yetki.StokBakiyeGor = yt.StokBakiyeGor;
                yetki.KendiDepoMiktari = yt.KendiDepoMiktari;
                yetki.FiyatDegistirme = yt.FiyatDegistirme;
                yetki.DusukFiyatSatis = yt.DusukFiyatSatis;
                yetki.EIrsaliyeKullan = yt.EIrsaliyeKullan;
                yetki.KasaDevri = yt.KasaDevri;
                yetki.KasaBankaBakGor = yt.KasaBankaBakGor;
                yetki.UretimGorunumuKullan = yt.UretimGorunumuKullan;
                yetki.HizliSatisSime = yt.HizliSatisSime;

                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }

            result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        
        public ActionResult PersonelBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
            Personel emp = db.Personel.Where(x => x.ID == id).FirstOrDefault<Personel>();

                string websifre = GirisKontrol.unhash(emp.WebSifre, true);
                emp.WebSifre = websifre;


                Yetkiler yt = db.Yetkiler.Where(x => x.PersonelID == id).FirstOrDefault<Yetkiler>();
                PersonelBilgiEk personel = new PersonelBilgiEk();

                personel.pr = emp;
                personel.yt = yt;
                


                return Json(new { success = true, data = personel }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeletePersonel(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                Personel emp = db.Personel.Where(x => x.ID == id).FirstOrDefault<Personel>();
                if (id == 1 || emp.PersonelGrubu == "Admin")
                {
                    return Json(new { success = false, message = "Bu kullanıcı silinemez." },
                        JsonRequestBehavior.AllowGet);
                    //if (Session["Grubu"].ToString() != "Admin")
                    //{
                    //    return Json(new {success = false, message = "Bu kullanıcı silinemez."},
                    //        JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{

                    //    using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                    //    {
                    //        conp.Open();
                    //        SqlCommand cmd = new SqlCommand("IslemKontrolPersonel", conp);
                    //        cmd.CommandType = CommandType.StoredProcedure;
                    //        cmd.Parameters.AddWithValue("@PersonelID", emp.ID);  // input parameter

                    //        // output parameter
                    //        SqlParameter average = new SqlParameter("@deger", SqlDbType.NVarChar, 50);
                    //        average.Direction = System.Data.ParameterDirection.Output;
                    //        cmd.Parameters.Add(average);

                    //        // return value
                    //        SqlParameter count = cmd.CreateParameter();
                    //        count.Direction = System.Data.ParameterDirection.ReturnValue;
                    //        cmd.Parameters.Add(count);


                    //        cmd.ExecuteNonQuery();

                    //        if (Convert.ToInt32(count.Value) != 0)
                    //        {
                    //            if (average.Value.ToString() == "İşlem")
                    //            {
                    //                return Json(new { success = false, message = "Bu Personele ait " + average.Value.ToString() + " işlem bulunmaktadır.Silinemez" }, JsonRequestBehavior.AllowGet);
                    //            }
                    //            else
                    //            {
                    //                return Json(new { success = false, message = "Kayıt silinirken bir hata oluştu." }, JsonRequestBehavior.AllowGet);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            db.Personel.Remove(emp);
                    //            Yetkiler yet = db.Yetkiler.Where(x => x.PersonelID == id).FirstOrDefault<Yetkiler>();


                    //            db.Yetkiler.Remove(yet);
                    //            db.SaveChanges();

                    //            return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                    //        }
                    //    }
                    //}
                }
               else { 
                
                using (SqlConnection conp = new System.Data.SqlClient.SqlConnection(AyarMetot.conString))
                {
                    conp.Open();
                    SqlCommand cmd = new SqlCommand("IslemKontrolPersonel", conp);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PersonelID", emp.ID);  // input parameter

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
                        if (average.Value.ToString() == "İşlem")
                        {
                            return Json(new { success = false, message = "Bu Personele ait " + average.Value.ToString() + " işlem bulunmaktadır.Silinemez" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, message = "Kayıt silinirken bir hata oluştu." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        db.Personel.Remove(emp);
                        Yetkiler yet = db.Yetkiler.Where(x => x.PersonelID == id).FirstOrDefault<Yetkiler>();


                        db.Yetkiler.Remove(yet);
                        db.SaveChanges();

                        return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
                    }
                }
                }
            }
        }


        public ActionResult PersonelHareketler(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult GetPersonelHareketler(int id)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<HareketlerListesi> yonetim = new List<HareketlerListesi>();
            string sorg = @"SET DATEFORMAT DMY;Select IslemTipi,ID,Aciklama,ParaBirimi,Tutar,IslemNo,IslemTarih from PERSONEL_HAREKET WHERE ID='" + id + "'";

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
                                HareketlerListesi yt = new HareketlerListesi();

                                yt.FaturaNo = dr["IslemNo"].ToString();
                                yt.IslemTipi = dr["Islemtipi"].ToString();
                                yt.Aciklama = dr["Aciklama"].ToString();
                                yt.FaturaType = dr["FaturaType"].ToString();
                                yt.ParaBirimi = dr["ParaBirimi"].ToString();
                                yt.ID = Convert.ToInt32(dr["ID"].ToString());
                                try
                                {
                                    yt.IslemTarihi = Convert.ToDateTime(dr["IslemTarihi"]);
                                    yt.IslemTarihiF = Convert.ToDateTime(dr["IslemTarihi"]).ToString("dd.MM.yyyy");
                                }
                                catch (Exception e)
                                {
                                    yt.IslemTarihi = DateTime.Now;
                                    yt.IslemTarihiF = DateTime.Now.ToString("dd.MM.yyyy");
                                }



                                yt.Tutar = alacak - borc;

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




    }
}