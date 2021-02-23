using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaTeknopark_MVC5.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class ServisListController : Controller
    {
        sayazilimEntities db = new sayazilimEntities();
        // GET: ServisList
        public ActionResult Index()
        {
            List<BALANCE> list = db.BALANCE.Where(x => x.cariID == -1).ToList();
            int kasa1 = 0;
            int kasa2 = 0;
            int kasa3 = 0;
            string birim1 = "";
            string birim2 = "";
            string birim3 = "";
            if (Session["PersonelType"].ToString() == "3")
            {


                int CariID = Convert.ToInt32(Session["PersonelID"]);

                list = db.BALANCE.Where(x => x.cariID == CariID).ToList();

                foreach (var item in list)
                {
                    if (item.paraBirimi == "TL")
                    {
                        kasa1 = Convert.ToInt32(item.alacakB - item.borcB);
                        birim1 = item.paraBirimi;
                    }
                    else if (item.paraBirimi == "EUR")
                    {
                        kasa2 = Convert.ToInt32(item.alacakB - item.borcB);
                        birim2 = item.paraBirimi;
                    }
                    else
                    {
                        kasa3 = Convert.ToInt32(item.alacakB - item.borcB);
                        birim3 = item.paraBirimi;
                    }
                }
                ViewBag.Value1 = kasa1;
                ViewBag.Value2 = kasa2;
                ViewBag.Value3 = kasa3;
                ViewBag.Birim1 = birim1;
                ViewBag.Birim2 = birim2;
                ViewBag.Birim3 = birim3;


            }
            if (Session["PersonelType"].ToString() == "4")
            {

            }
            return View();
        }



        public JsonResult SBilgi(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                var emp = db.TECHNICAL.Where(x => x.ID == id).FirstOrDefault<TECHNICAL>();

                return Json(new { success = true, data = emp }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetServis()
        {
            string PersonelID = Session["PersonelID"].ToString();

            string FirmaID2 = Session["FirmaID"].ToString();
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;


            List<Servis> yonetim = new List<Servis>();
            string sorg = @"set dateformat dmy; Select ServisIslemNo,Serino,BayiID,CihazAdresi,Cinsi,CariID,(Select CariUnvan from Cari where ID=TECHNICAL.CariID) 
                           as CariUnvan,(Select Adi+' '+Soyadi from Personel where ID =TECHNICAL.Tekniksyen)as Teknisyen,Tekniksyen,Tarih,Marka,Model,GenelToplam,ParaBirimi, Durum,TECHNICAL.ID from TECHNICAL Where FirmaID =" + FirmaID2 + " And Tekniksyen =" + PersonelID + " ORDER BY Tarih Desc";

            if (Session["sCariID"].ToString() != "-1")
            {
                sorg = @"set dateformat dmy; Select ServisIslemNo,Serino,BayiID,CihazAdresi,Cinsi,CariID,(Select CariUnvan from Cari where ID=TECHNICAL.CariID) 
                           as CariUnvan,(Select Adi+' '+Soyadi from Personel where ID =TECHNICAL.Tekniksyen)as Teknisyen,Tekniksyen,Tarih,Marka,Model,GenelToplam,ParaBirimi, Durum,TECHNICAL.ID from TECHNICAL Where FirmaID =" + FirmaID2 + " and    BayiID =" + Session["sCariID"].ToString() + " ORDER BY Tarih Desc";

            }
            int i = 1;
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

                            yt.Sira = i;
                            i++;
                            yt.ParaBirimi = dr["ParaBirimi"].ToString();
                            yt.GenelToplam = Convert.ToDecimal(dr["GenelToplam"]).ToString("N2");
                            yt.ServisIslemNo = dr["ServisIslemNo"].ToString();
                            yt.Serino = dr["Serino"].ToString();
                            yt.CariID = dr["CariID"].ToString();
                            yt.Cinsi = dr["Cinsi"].ToString();
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
                            yonetim.Add(yt);
                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }

    }




}