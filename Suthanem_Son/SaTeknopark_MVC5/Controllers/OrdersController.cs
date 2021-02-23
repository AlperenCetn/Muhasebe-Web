


using SaTeknopark_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class OrdersController : Controller
    {


        // GET: Orders
        public ActionResult Orders()
        {

        
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<OrdersModel> bekleyen = new List<OrdersModel>();
            List<OrdersModel> teslim = new List<OrdersModel>();
            List<OrdersModel> iptal = new List<OrdersModel>();
        
            string sorg = @"Select durumu,SiparisNo ,ID,GenelToplam,SiparisNotu,KuryeID,IptalAciklama,IslemTarihi,PersonelID,Sikayet,Aciklama,Teslimat, (Select CariUnvan from Cari  where CariID=Cari.ID )as CariUnvan,

(Select CASE When GSM =null then Telefon1 else GSM end from Cari  where CariID=Cari.ID )as CariTel,Tarif,
(Select Adres from Cari  where CariID=Cari.ID)as Adres,(Select Telefon1 from Cari  where CariID=Cari.ID) as Telefon1,
(Select CariGrubu from Cari  where CariID=Cari.ID) as CariGrubu from ORDERS  where  KuryeID= "+ Session["PersonelID"].ToString()+" Order BY ID Desc";



            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand ordersgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = ordersgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            OrdersModel yt = new OrdersModel();
                            yt.SiparisNo = dr["SiparisNo"].ToString();
                            yt.GenelToplam = Convert.ToDecimal(dr["GenelToplam"]);
                            yt.CariUnvan = dr["CariUnvan"].ToString();
                            yt.Adres = dr["Adres"].ToString();
                            yt.CariGrubu = dr["CariGrubu"].ToString();
                            yt.IslemTarihi = dr["IslemTarihi"].ToString();
                            yt.SiparisNotu = dr["SiparisNotu"].ToString();
                            yt.Telefon1 = dr["CariTel"].ToString();
                            yt.Sikayet = dr["Sikayet"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();
                            yt.Tarif = dr["Tarif"].ToString();
                            yt.Teslimat = dr["Teslimat"].ToString();
                            yt.IptalAciklama = dr["IptalAciklama"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.KuryeID = Convert.ToInt32(dr["KuryeID"]);
                            yt.PersonelID = Convert.ToInt32(Session["PersonelID"]);


                            if (dr["durumu"].ToString() == "Teslim Edildi")
                            {
                                teslim.Add(yt);

                            }
                            else if (dr["durumu"].ToString() == "İptal Edildi")
                            {
                                iptal.Add(yt);

                            }
                            else if (dr["durumu"].ToString() == "Yolda" )
                            {
                                bekleyen.Add(yt);
                            
                                }
                          

                        }

                    }

                }

            }
            }
            catch (Exception e1)
            { System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Admin\AppData\Local\Sayazilim", "sonuç.xml"), e1.ToString()); }

            ViewBag.teslim = teslim.ToList();
            ViewBag.iptal = iptal.ToList();
            ViewBag.bekleyen = bekleyen.ToList();
            ViewBag.yolda = bekleyen.ToList();
            return View();

        }
public class OrdersModel
        {
            public string SiparisNo { get; set; }
            public decimal GenelToplam { get; set; }
            public string CariUnvan { get; set; }
            public string Adres { get; set; }
            public string CariGrubu { get; set; }

            public string SiparisNotu { get; set; }
            public string IslemTarihi { get; set; }
            public string Sikayet { get; set; }
            public string Aciklama { get; set; }
            public string Tarif { get; set; }
            public string Telefon1 { get; set; }
            public string Teslimat { get; set; }
            public string IptalAciklama { get; set; }
            public int KuryeID { get; set; }
            public int PersonelID { get; set; }
            public int ID { get; set; }








        }

        public class StokModel
        {
            public string UrunAdi { get; set; }
            public decimal Fiyat { get; set; }
            public decimal Miktar { get; set; }
            public decimal Tutar { get; set; }
            public string ID { get; set; }

            public string Birim { get; set; }












        }


        //public ActionResult DeleteBasvuru(int id, string aciklama)
        //{
        //    using (sayazilimEntities db = new sayazilimEntities())
        //    {
        //        ORDERS emp = db.ORDERS.Where(x => x.ID == id).FirstOrDefault<ORDERS>();
        
        //        emp.IptalAciklama = aciklama;
          
        //        db.SaveChanges();
        //        return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
        //    }
        //}



        [HttpGet]
        public ActionResult GetOrders()
        {
            

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }



      

        public ActionResult OrdersBilgi(int id)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            OrdersModel yonetim = new OrdersModel();
            string sorg = @"Select SiparisNo ,ID,GenelToplam,SiparisNotu,IslemTarihi,Sikayet,Aciklama,(Select CASE When GSM =null then Telefon1 else GSM end from Cari  where CariID=Cari.ID )as CariTel,Teslimat, (Select CariUnvan from Cari  where CariID=Cari.ID )as CariUnvan,Tarif,(Select Adres from Cari  where CariID=Cari.ID)as Adres,(Select CariGrubu from Cari  where CariID=Cari.ID) as CariGrubu from ORDERS where ID='" + id+"'";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand ordersgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = ordersgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            OrdersModel yt = new OrdersModel();
                            yt.SiparisNo = dr["SiparisNo"].ToString();
                            yt.GenelToplam = Convert.ToDecimal(dr["GenelToplam"]);
                            yt.CariUnvan = dr["CariUnvan"].ToString();
                            yt.Adres = dr["Adres"].ToString();
                            yt.CariGrubu = dr["CariGrubu"].ToString();
                            yt.IslemTarihi = dr["IslemTarihi"].ToString();
                            yt.SiparisNotu = dr["SiparisNotu"].ToString();
                            yt.Sikayet = dr["Sikayet"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString(); 
                            yt.Tarif = dr["Tarif"].ToString();
                            yt.Telefon1 = dr["CariTel"].ToString();
                             yt.Teslimat = dr["Teslimat"].ToString();

                            yt.ID = Convert.ToInt32(dr["ID"]);



                            yonetim = yt;
                        }
                    }
                }
            }


        
            return Json(new { success = true,data =yonetim }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult OrdersDetay(int id)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            List<StokModel> yonetim = new List<StokModel>();
            string sorg = @"Select UrunAdi,Fiyat,Miktar,Tutar,ORDERS_DETAIL.ID,Stok.Birimi from ORDERS_DETAIL,Stok  WHERE UrunID=STOK.ID and SiparisID='" + id + "'";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand stokgetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = stokgetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            StokModel yt = new StokModel();
                            yt.UrunAdi = dr["UrunAdi"].ToString();
                            yt.Fiyat = Convert.ToDecimal(dr["Fiyat"]);
                            yt.Miktar = Convert.ToDecimal(dr["Miktar"]);
                            yt.Tutar = Convert.ToDecimal(dr["Tutar"]);
                            yt.ID = dr["ID"].ToString();
                            yt.Birim = dr["Birimi"].ToString();
                            yonetim.Add(yt);
                        }
                    }
                }
            }



            return Json(new { success = true, data = yonetim.ToList() }, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult GuncelleOrders(int id,string durum, string aciklama)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                ORDERS emp = db.ORDERS.Where(x => x.ID == id).FirstOrDefault<ORDERS>();
                emp.Durumu = durum;
              //  emp.IptalAciklama = aciklama;
                db.SaveChanges();
                return Json(new { success = true, message = "Durum Güncellendi" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}


    