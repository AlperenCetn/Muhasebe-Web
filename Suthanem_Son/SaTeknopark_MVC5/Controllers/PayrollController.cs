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
using SaTeknopark_MVC5.Models;

namespace SaTeknopark_MVC5.Controllers
{
    [AuthorizeFilter]
    public class PayrollController : Controller
    {
        // GET: Payroll
        public ActionResult Bordrolar()
        {
            return View();
        }


        public ActionResult CekIade(int id = 0, int cek = 0, int ceksenet = 0)
        {
            sayazilimEntities db = new sayazilimEntities();
            PAYROLL py = new PAYROLL();
            if (id == 0)
            {

                if (cek == 0)
                {
                    AyarMetot.Siradaki("", "CEKSENET", "İslemNo", Session["FirmaID"].ToString());
                    ViewBag.CekSiradaki = AyarMetot.GetNumara;
                    AyarMetot.Siradaki("", "CEK", "PortfoyNo", Session["FirmaID"].ToString());
                    ViewBag.PortfoySiradakiNo = AyarMetot.GetNumara;

                    if (ceksenet == 0)
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                else
                {
                    PAYROLL_CS ceklist = db.PAYROLL_CS.Where(x => x.ID == cek).FirstOrDefault();
                    py = db.PAYROLL.Where(x => x.ID == ceklist.BordroID).FirstOrDefault();
                    DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                    py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                    var pylist = db.PAYROLL_CS.Where(x => x.BordroID == py.ID).ToList();
                    foreach (var item in pylist)
                    {
                        ViewBag.CekDurum = item.IslemTipi;
                        if (item.IslemTipi == "Çek")
                        {
                            ViewBag.CekSenetDurum = "Çek";
                        }
                        else
                        {
                            ViewBag.CekSenetDurum = "Senet";
                        }
                    }

                    ViewBag.pyList = pylist.ToList();
                }
            }
            else if (id != 0)
            {

                py = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault();
                DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                var pylist = db.PAYROLL_CS.Where(x => x.BordroID == id).ToList();
                foreach (var item in pylist)
                {
                    ViewBag.CekDurum = item.IslemTipi;
                    if (item.IslemTipi == "Çek")
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                ViewBag.pyList = pylist.ToList();

            }

            return View(py);
        }

        public ActionResult CekIadeVer(int id = 0, int cek = 0, int ceksenet = 0)
        {
            sayazilimEntities db = new sayazilimEntities();
            PAYROLL py = new PAYROLL();
            if (id == 0)
            {

                if (cek == 0)
                {
                    AyarMetot.Siradaki("", "CEKSENET", "İslemNo", Session["FirmaID"].ToString());
                    ViewBag.CekSiradaki = AyarMetot.GetNumara;
                    AyarMetot.Siradaki("", "CEK", "PortfoyNo", Session["FirmaID"].ToString());
                    ViewBag.PortfoySiradakiNo = AyarMetot.GetNumara;

                    if (ceksenet == 0)
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                else
                {
                    PAYROLL_CS ceklist = db.PAYROLL_CS.Where(x => x.ID == cek).FirstOrDefault();
                    py = db.PAYROLL.Where(x => x.ID == ceklist.BordroID).FirstOrDefault();
                    DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                    py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                    var pylist = db.PAYROLL_CS.Where(x => x.BordroID == py.ID).ToList();
                    foreach (var item in pylist)
                    {
                        ViewBag.CekDurum = item.IslemTipi;
                        if (item.IslemTipi == "Çek")
                        {
                            ViewBag.CekSenetDurum = "Çek";
                        }
                        else
                        {
                            ViewBag.CekSenetDurum = "Senet";
                        }
                    }

                    ViewBag.pyList = pylist.ToList();
                }
            }
            else if (id != 0)
            {

                py = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault();
                DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                var pylist = db.PAYROLL_CS.Where(x => x.BordroID == id).ToList();
                foreach (var item in pylist)
                {
                    ViewBag.CekDurum = item.IslemTipi;
                    if (item.IslemTipi == "Çek")
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                ViewBag.pyList = pylist.ToList();

            }

            return View(py);
        }

        public ActionResult CekTahsilatKasa(int id = 0, int cek = 0, int ceksenet = 0)
        {
            sayazilimEntities db = new sayazilimEntities();
            PAYROLL py = new PAYROLL();
            if (id == 0)
            {

                if (cek == 0)
                {
                    AyarMetot.Siradaki("", "CEKSENET", "İslemNo", Session["FirmaID"].ToString());
                    ViewBag.CekSiradaki = AyarMetot.GetNumara;
                    AyarMetot.Siradaki("", "CEK", "PortfoyNo", Session["FirmaID"].ToString());
                    ViewBag.PortfoySiradakiNo = AyarMetot.GetNumara;

                    if (ceksenet == 0)
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                else
                {
                    PAYROLL_CS ceklist = db.PAYROLL_CS.Where(x => x.ID == cek).FirstOrDefault();
                    py = db.PAYROLL.Where(x => x.ID == ceklist.BordroID).FirstOrDefault();
                    DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                    py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                    var pylist = db.PAYROLL_CS.Where(x => x.BordroID == py.ID).ToList();
                    foreach (var item in pylist)
                    {
                        ViewBag.CekDurum = item.IslemTipi;
                        if (item.IslemTipi == "Çek")
                        {
                            ViewBag.CekSenetDurum = "Çek";
                        }
                        else
                        {
                            ViewBag.CekSenetDurum = "Senet";
                        }
                    }

                    ViewBag.pyList = pylist.ToList();
                }
            }
            else if (id != 0)
            {

                py = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault();
                DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                var pylist = db.PAYROLL_CS.Where(x => x.BordroID == id).ToList();
                foreach (var item in pylist)
                {
                    ViewBag.CekDurum = item.IslemTipi;
                    if (item.IslemTipi == "Çek")
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                ViewBag.pyList = pylist.ToList();

            }

            return View(py);
        }

        public ActionResult CekTahsilatBanka(int id = 0, int cek = 0, int ceksenet = 0)
        {
            sayazilimEntities db = new sayazilimEntities();
            PAYROLL py = new PAYROLL();
            if (id == 0)
            {

                if (cek == 0)
                {
                    AyarMetot.Siradaki("", "CEKSENET", "İslemNo", Session["FirmaID"].ToString());
                    ViewBag.CekSiradaki = AyarMetot.GetNumara;
                    AyarMetot.Siradaki("", "CEK", "PortfoyNo", Session["FirmaID"].ToString());
                    ViewBag.PortfoySiradakiNo = AyarMetot.GetNumara;

                    if (ceksenet == 0)
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                else
                {
                    PAYROLL_CS ceklist = db.PAYROLL_CS.Where(x => x.ID == cek).FirstOrDefault();
                    py = db.PAYROLL.Where(x => x.ID == ceklist.BordroID).FirstOrDefault();
                    DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                    py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                    var pylist = db.PAYROLL_CS.Where(x => x.BordroID == py.ID).ToList();
                    foreach (var item in pylist)
                    {
                        ViewBag.CekDurum = item.IslemTipi;
                        if (item.IslemTipi == "Çek")
                        {
                            ViewBag.CekSenetDurum = "Çek";
                        }
                        else
                        {
                            ViewBag.CekSenetDurum = "Senet";
                        }
                    }

                    ViewBag.pyList = pylist.ToList();
                }
            }
            else if (id != 0)
            {

                py = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault();
                DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                var pylist = db.PAYROLL_CS.Where(x => x.BordroID == id).ToList();
                foreach (var item in pylist)
                {
                    ViewBag.CekDurum = item.IslemTipi;
                    if (item.IslemTipi == "Çek")
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                ViewBag.pyList = pylist.ToList();

            }

            return View(py);
        }

        public ActionResult CekOdemeKasa(int id = 0, int cek = 0, int ceksenet = 0)
        {
            sayazilimEntities db = new sayazilimEntities();
            PAYROLL py = new PAYROLL();
            if (id == 0)
            {

                if (cek == 0)
                {
                    AyarMetot.Siradaki("", "CEKSENET", "İslemNo", Session["FirmaID"].ToString());
                    ViewBag.CekSiradaki = AyarMetot.GetNumara;
                    AyarMetot.Siradaki("", "CEK", "PortfoyNo", Session["FirmaID"].ToString());
                    ViewBag.PortfoySiradakiNo = AyarMetot.GetNumara;

                    if (ceksenet == 0)
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                else
                {
                    PAYROLL_CS ceklist = db.PAYROLL_CS.Where(x => x.ID == cek).FirstOrDefault();
                    py = db.PAYROLL.Where(x => x.ID == ceklist.BordroID).FirstOrDefault();
                    DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                    py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                    var pylist = db.PAYROLL_CS.Where(x => x.BordroID == py.ID).ToList();
                    foreach (var item in pylist)
                    {
                        ViewBag.CekDurum = item.IslemTipi;
                        if (item.IslemTipi == "Çek")
                        {
                            ViewBag.CekSenetDurum = "Çek";
                        }
                        else
                        {
                            ViewBag.CekSenetDurum = "Senet";
                        }
                    }

                    ViewBag.pyList = pylist.ToList();
                }
            }
            else if (id != 0)
            {

                py = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault();
                DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                var pylist = db.PAYROLL_CS.Where(x => x.BordroID == id).ToList();
                foreach (var item in pylist)
                {
                    ViewBag.CekDurum = item.IslemTipi;
                    if (item.IslemTipi == "Çek")
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                ViewBag.pyList = pylist.ToList();

            }

            return View(py);
        }

        public ActionResult CekOdemeBanka(int id = 0, int cek = 0, int ceksenet = 0)
        {
            sayazilimEntities db = new sayazilimEntities();
            PAYROLL py = new PAYROLL();
            if (id == 0)
            {

                if (cek == 0)
                {
                    AyarMetot.Siradaki("", "CEKSENET", "İslemNo", Session["FirmaID"].ToString());
                    ViewBag.CekSiradaki = AyarMetot.GetNumara;
                    AyarMetot.Siradaki("", "CEK", "PortfoyNo", Session["FirmaID"].ToString());
                    ViewBag.PortfoySiradakiNo = AyarMetot.GetNumara;

                    if (ceksenet == 0)
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                else
                {
                    PAYROLL_CS ceklist = db.PAYROLL_CS.Where(x => x.ID == cek).FirstOrDefault();
                    py = db.PAYROLL.Where(x => x.ID == ceklist.BordroID).FirstOrDefault();
                    DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                    py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                    var pylist = db.PAYROLL_CS.Where(x => x.BordroID == py.ID).ToList();
                    foreach (var item in pylist)
                    {
                        ViewBag.CekDurum = item.IslemTipi;
                        if (item.IslemTipi == "Çek")
                        {
                            ViewBag.CekSenetDurum = "Çek";
                        }
                        else
                        {
                            ViewBag.CekSenetDurum = "Senet";
                        }
                    }

                    ViewBag.pyList = pylist.ToList();
                }
            }
            else if (id != 0)
            {

                py = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault();
                DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                var pylist = db.PAYROLL_CS.Where(x => x.BordroID == id).ToList();
                foreach (var item in pylist)
                {
                    ViewBag.CekDurum = item.IslemTipi;
                    if (item.IslemTipi == "Çek")
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                ViewBag.pyList = pylist.ToList();

            }

            return View(py);
        }

        public ActionResult BankayaCekCikis(int id = 0, int cek = 0,int ceksenet=0)
        {
            sayazilimEntities db = new sayazilimEntities();
            PAYROLL py = new PAYROLL();
            if (id == 0)
            {

                if (cek == 0)
                {
                    AyarMetot.Siradaki("", "CEKSENET", "İslemNo", Session["FirmaID"].ToString());
                    ViewBag.CekSiradaki = AyarMetot.GetNumara;
                    AyarMetot.Siradaki("", "CEK", "PortfoyNo", Session["FirmaID"].ToString());
                    ViewBag.PortfoySiradakiNo = AyarMetot.GetNumara;

                    if (ceksenet == 0)
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                else
                {
                    PAYROLL_CS ceklist = db.PAYROLL_CS.Where(x => x.ID == cek).FirstOrDefault();
                    py = db.PAYROLL.Where(x => x.ID == ceklist.BordroID).FirstOrDefault();
                    DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                    py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                    var pylist = db.PAYROLL_CS.Where(x => x.BordroID == py.ID).ToList();
                    foreach (var item in pylist)
                    {
                        ViewBag.CekDurum = item.IslemTipi;
                        if (item.IslemTipi == "Çek")
                        {
                            ViewBag.CekSenetDurum = "Çek";
                        }
                        else
                        {
                            ViewBag.CekSenetDurum = "Senet";
                        }
                    }

                    ViewBag.pyList = pylist.ToList();
                }
            }
            else if (id != 0)
            {

                py = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault();
                DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                var pylist = db.PAYROLL_CS.Where(x => x.BordroID == id).ToList();
                foreach (var item in pylist)
                {
                    ViewBag.CekDurum = item.IslemTipi;
                    if (item.IslemTipi == "Çek")
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                ViewBag.pyList = pylist.ToList();

            }

            return View(py);
        }

        public ActionResult BankadanCekAl(int id = 0, int cek = 0, int ceksenet = 0)
        {
            sayazilimEntities db = new sayazilimEntities();
            PAYROLL py = new PAYROLL();
            if (id == 0)
            {

                if (cek == 0)
                {
                    AyarMetot.Siradaki("", "CEKSENET", "İslemNo", Session["FirmaID"].ToString());
                    ViewBag.CekSiradaki = AyarMetot.GetNumara;
                    AyarMetot.Siradaki("", "CEK", "PortfoyNo", Session["FirmaID"].ToString());
                    ViewBag.PortfoySiradakiNo = AyarMetot.GetNumara;

                    if (ceksenet == 0)
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                else
                {
                    PAYROLL_CS ceklist = db.PAYROLL_CS.Where(x => x.ID == cek).FirstOrDefault();
                    py = db.PAYROLL.Where(x => x.ID == ceklist.BordroID).FirstOrDefault();
                    DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                    py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                    var pylist = db.PAYROLL_CS.Where(x => x.BordroID == py.ID).ToList();
                    foreach (var item in pylist)
                    {
                        ViewBag.CekDurum = item.IslemTipi;
                        if (item.IslemTipi == "Çek")
                        {
                            ViewBag.CekSenetDurum = "Çek";
                        }
                        else
                        {
                            ViewBag.CekSenetDurum = "Senet";
                        }
                    }

                    ViewBag.pyList = pylist.ToList();
                }
            }
            else if (id != 0)
            {

                py = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault();
                DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                var pylist = db.PAYROLL_CS.Where(x => x.BordroID == id).ToList();
                foreach (var item in pylist)
                {
                    ViewBag.CekDurum = item.IslemTipi;
                    if (item.IslemTipi == "Çek")
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                ViewBag.pyList = pylist.ToList();

            }

            return View(py);
        }

        public ActionResult GetBordrolar()
        {

            string FirmaID = Session["FirmaID"].ToString();
            List<Bordrolar> yonetim = new List<Bordrolar>();
            string sorg = @"select * from PAYROLL Where FirmaID =" + FirmaID;
            sayazilimEntities db = new sayazilimEntities();

            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
            {
                con.Open();
                using (SqlCommand BordroGetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = BordroGetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Bordrolar yt = new Bordrolar();
                            DateTime dt = new DateTime();

                            yt.ID = Convert.ToInt32(dr["ID"]);
                            dt = Convert.ToDateTime(dr["IslemTarih"].ToString());
                            yt.KayitTipi = dr["IslemTipi"].ToString();
                            yt.IslemNumarasi = dr["Islemno"].ToString();
                            yt.KayitTarihi = dt.ToString("dd.MM.yyyy");
                   

                            try
                            {
                                int cariid = Convert.ToInt32(dr["CariID"].ToString());
                                Cari cr = db.Cari.Where(x => x.ID == cariid).FirstOrDefault<Cari>();
                                yt.Cari = cr.CariUnvan;
                            }
                            catch (Exception)
                            {
                                yt.Cari = "";
                            }
                            try
                            {
                                int personel = Convert.ToInt32(dr["PersonelID"].ToString());
                                yt.Personel = AyarMetot.SeciliPersonelBilgiCek("Adi+' '+Soyadi",personel.ToString());
                            }
                            catch (Exception)
                            {
                                yt.Personel ="";
                            }
                          
                           
                            try
                            {
                                yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                            }
                            catch (Exception)
                            {
                                yt.Tutar = "0";
                            }
                          
                            yt.PB = dr["ParaBirimi"].ToString();
                            try
                            {
                                dt = Convert.ToDateTime(dr["OrtalamaVade"].ToString());
                                yt.OrtalamaVade = dt.ToString("dd.MM.yyyy");
                            }
                            catch (Exception)
                            { }

                            dt = Convert.ToDateTime(dr["IslemTarih"].ToString());
                            yt.IslemTarihiF = dt.ToString("dd.MM.yyyy");
                            yt.Aciklama = dr["Aciklama"].ToString();

                            yt.PersonelAdi = Convert.ToInt32(dr["PersonelID"].ToString());
                            
                            yonetim.Add(yt);

                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Cekler()
        {
            return View();
        }


        #region İade İşlemleri
        public ActionResult GetICekler(string durum = "", string Btip = "", string CariID = "")
        {

            if (durum == "IadeAl")
            {
                if (CariID == "")
                {
                    CariID = "-1";
                }
                CariID = " and Durumu='Ciro Edildi' and  CariID=" + CariID;
            }
            else if (durum == "IadeVer")
            {
                if (CariID == "")
                {
                    CariID = "-1";
                }
                CariID = " and Durumu='Portföyde' and  CariID=" + CariID;
            }
            else
            {
                CariID = " and  CariID=" + -1;
            }

            if (Btip != "")
            {
                Btip = " and  BordroTipi=N'" + Btip + "'";
            }



            string FirmaID = Session["FirmaID"].ToString();
            List<Cekler> yonetim = new List<Cekler>();
            string sorg = @"select * from PAYROLL_CS Where  FirmaID =" + FirmaID + CariID + Btip;
            sayazilimEntities db = new sayazilimEntities();

            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
            {
                con.Open();
                using (SqlCommand BordroGetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = BordroGetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Cekler yt = new Cekler();
                            DateTime dt = new DateTime();
                            yt.Durumu = dr["Durumu"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.BordroTipi = dr["BordroTipi"].ToString();
                            int bordroid = Convert.ToInt32(dr["BordroID"].ToString());
                            PAYROLL brd = db.PAYROLL.Where(x => x.ID == bordroid).FirstOrDefault();
                            yt.BordroNumarasi = brd.IslemNo;
                            yt.IslemNumarasi = dr["PortfoyNo"].ToString();

                            yt.SeriNumarasi = dr["SeriNo"].ToString();
                            dt = Convert.ToDateTime(dr["VadeTarih"].ToString());
                            yt.VadeTarihi = dt.ToString("dd.MM.yyyy");
                            int cariid = Convert.ToInt32(dr["CariID"].ToString());
                            Cari cr = db.Cari.Where(x => x.ID == cariid).FirstOrDefault<Cari>();
                            yt.Cari = cr.CariUnvan;
                            int borclualacakli = Convert.ToInt32(dr["BorcluAlacakli"].ToString());
                            Cari borcalacak = db.Cari.Where(x => x.ID == borclualacakli).FirstOrDefault<Cari>();
                            try
                            {
                                yt.BorcluAlacakli = borcalacak.CariUnvan;
                            }
                            catch (Exception)
                            {
                                yt.BorcluAlacakli = "";
                            }
                          
                            yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                            yt.PB = dr["ParaBirimi"].ToString();
                            yt.CekBankasi = dr["BankaAdi"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();
                            dt = Convert.ToDateTime(dr["KayitTarih"].ToString());
                            yt.IslemTarihiF = dt.ToString("dd.MM.yyyy");

                            yt.PersonelAdi = Convert.ToInt32(dr["CariID"].ToString());





                            yonetim.Add(yt);

                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult YeniCekIadeAl(PAYROLL data, string json, string ceksenet)
        {
            sayazilimEntities db = new sayazilimEntities();

            int PayrollID = -1;
            json = "[" + json + "]";
            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                PAYROLL tk = new PAYROLL();
                tk = data;
                tk.IslemTipi = "Çek/Senet İade Al";
                tk.VadeFarki = 0;

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

                tk.Tutar = 0;
                tk.KasaBankaID = -1;
                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.OzelKodID = -1;
                tk.Durumu = "Aktif";
                tk.BankaIDGC = -1;
                tk.BankaIDOT = -1;
                tk.KasaIDOT = -1;
                tk.FaktoringTahsilatID = -1;
                tk.FaktoringTediyeID = -1;
                tk.ProjeID = -1;
                tk.AdresID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                tk.PrimOr = -1;
                tk.SantiyeID = -1;
                tk.IslemCODE = "-1";
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                tk.OrtalamaVade = DateTime.Now;
                tk.IslemCODE = "CSNIDA";

                db.PAYROLL.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM PAYROLL Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        PayrollID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }
            }
            else
            {
                PAYROLL tk = db.PAYROLL.Where(x => x.ID == data.ID).FirstOrDefault<PAYROLL>();

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

                tk.Tutar = 0;
                tk.CariID = data.CariID;
                tk.IslemTarih = data.IslemTarih;
                tk.PersonelID = data.PersonelID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.Aciklama = data.Aciklama;
                PayrollID = data.ID;

                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            List<PAYROLL_CS> items = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                PAYROLL_CS er = items[i];
                try
                {
                    using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da =
                            new System.Data.SqlClient.SqlDataAdapter(
                                "select * from PAYROLL_CS where ID='" + er.ID + "'", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "PAYROLL_CS");
                                DataRow[] adf = ds.Tables["PAYROLL_CS"].Select("ID='" + er.ID + "'");
                                if (adf.Length != 0)
                                {
                                    DataRow df = adf[0];
                                    df["Durumu"] = "Portföyde";
                                    da.Update(ds, "PAYROLL_CS");


                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');

                                    int CekSenetID = -1;
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                    }

                                    using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                    {
                                        using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                        {
                                            DataSet dsdurum = new DataSet();
                                            dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                            DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                            drd["BordroID"] = PayrollID;
                                            drd["CekSenetID"] = CekSenetID;
                                            drd["CariID"] = data.CariID;
                                            drd["BorcluAlacakli"] = data.CariID;
                                            drd["BankaID"] = -1;
                                            drd["KasaID"] = -1;
                                            drd["Durum"] = df["Portföyde"];
                                            drd["CariID"] = data.CariID;
                                            drd["Tarih"] = data.IslemTarih;
                                            drd["Donem"] = AyarMetot.Donem;
                                            drd["FirmaID"] = Session["FirmaID"].ToString();
                                            drd["Company_Code"] = company_code;


                                            dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                            dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");
                                        }
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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }


            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult YeniCekIadeVer(PAYROLL data, string json, string ceksenet)
        {
            sayazilimEntities db = new sayazilimEntities();

            int PayrollID = -1;
            json = "[" + json + "]";
            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                PAYROLL tk = new PAYROLL();
                tk = data;
                tk.IslemTipi = "Çek/Senet İade Ver";
                tk.VadeFarki = 0;

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

                tk.Tutar = 0;
                tk.KasaBankaID = -1;
                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.OzelKodID = -1;
                tk.Durumu = "Aktif";
                tk.BankaIDGC = -1;
                tk.BankaIDOT = -1;
                tk.KasaIDOT = -1;
                tk.FaktoringTahsilatID = -1;
                tk.FaktoringTediyeID = -1;
                tk.ProjeID = -1;
                tk.AdresID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                tk.PrimOr = -1;
                tk.SantiyeID = -1;
                tk.IslemCODE = "-1";
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                tk.OrtalamaVade = DateTime.Now;
                tk.IslemCODE = "CSNIDV";

                db.PAYROLL.Add(tk);
                db.SaveChanges();

                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM PAYROLL Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        PayrollID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                PAYROLL tk = db.PAYROLL.Where(x => x.ID == data.ID).FirstOrDefault<PAYROLL>();

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

                tk.Tutar = 0;
                tk.CariID = data.CariID;
                tk.IslemTarih = data.IslemTarih;
                tk.PersonelID = data.PersonelID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.Aciklama = data.Aciklama;
                PayrollID = data.ID;

                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            List<PAYROLL_CS> items = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                PAYROLL_CS er = items[i];
                try
                {

                    using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da =
                            new System.Data.SqlClient.SqlDataAdapter(
                                "select * from PAYROLL_CS where ID='" + er.ID + "'", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "PAYROLL_CS");
                                DataRow[] adf = ds.Tables["PAYROLL_CS"].Select("ID='" + er.ID + "'");
                                if (adf.Length != 0)
                                {
                                    DataRow df = adf[0];
                                    df["Durumu"] = "Iade Edildi";
                                    da.Update(ds, "PAYROLL_CS");


                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');

                                    int CekSenetID = -1;
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                    }

                                    using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                    {
                                        using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                        {
                                            DataSet dsdurum = new DataSet();
                                            dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                            DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                            drd["BordroID"] = PayrollID;
                                            drd["CekSenetID"] = CekSenetID;
                                            drd["CariID"] = data.CariID;
                                            drd["BorcluAlacakli"] = data.CariID;
                                            drd["BankaID"] = -1;
                                            drd["KasaID"] = -1;
                                            drd["Durum"] = df["Iade Edildi"];
                                            drd["CariID"] = data.CariID;
                                            drd["Tarih"] = data.IslemTarih;
                                            drd["Donem"] = AyarMetot.Donem;
                                            drd["FirmaID"] = Session["FirmaID"].ToString();
                                            drd["Company_Code"] = company_code;


                                            dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                            dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");
                                        }
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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }


            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Yeni Çek/Senet
        public ActionResult GetCekler(string tarih1 = "", string tarih2 = "", string EvrakTipi = "", string CekDurum = "", string Personel = "", string PB = "", string OzelKod = "", string Cariler = "", string durum = "")
        {

            //string tarih = "";
            //string tarih2 = "";
            //string EvrakTipi = "";
            //string CekDurum = "";
            //string Personel = "";
            //string Cariler = "";
            //string PB = "";
            //string OzelKod = "";
          

            if (tarih1 != "")
            {
                tarih1 = " and IslemTarihi BETWEEN '" + tarih1 +  "' AND '"  + tarih2 + "'";
            }
            if (EvrakTipi != "" && EvrakTipi != "Tüm Evraklar")
            {
                EvrakTipi = " and BordroTipi=N'" + EvrakTipi + "'";
            }
            if (CekDurum != "" && CekDurum != "Tüm Durumlar")
            {
                CekDurum = " and Durumu=N'" + CekDurum + "'";
            }


            if (Personel != "" && Personel != "Seçiniz")
            {
                Personel = " and PersonelID='" + Personel + "'";
            }
            if (Cariler != null && Cariler != "Seçiniz")
            {
                Cariler = " and CariID=" + Cariler + "";
            }

            if (PB != "")
            {
                PB = " and ParaBirimi='"+ PB + "'";
            }
            if (OzelKod != "" && OzelKod != "Seçiniz")
            {
                OzelKod = " and OzelKodID=" + OzelKod + "";
            }

            //if (durum != "")
            //{
            //    durum = "and Durumu='Ciro Edildi' and CariID=" + durum;
            //}



            string FirmaID = Session["FirmaID"].ToString();
            List<Cekler> yonetim = new List<Cekler>();

            string sorg = @"select * from PAYROLL_CS Where FirmaID =" + FirmaID +tarih1 + EvrakTipi + CekDurum + Personel+ Cariler+ PB + OzelKod;
            sayazilimEntities db = new sayazilimEntities();

            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
            {
                con.Open();
                using (SqlCommand BordroGetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = BordroGetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Cekler yt = new Cekler();
                            DateTime dt = new DateTime();
                            yt.Durumu = dr["Durumu"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.BordroTipi = dr["BordroTipi"].ToString();
                            int bordroid = Convert.ToInt32(dr["BordroID"].ToString());
                            PAYROLL brd = db.PAYROLL.Where(x => x.ID == bordroid).FirstOrDefault();
                            yt.BordroNumarasi = brd.IslemNo;
                            yt.IslemNumarasi = dr["PortfoyNo"].ToString();

                            yt.SeriNumarasi = dr["SeriNo"].ToString();
                            dt = Convert.ToDateTime(dr["VadeTarih"].ToString());
                            yt.VadeTarihi = dt.ToString("dd.MM.yyyy");
                            int cariid = Convert.ToInt32(dr["CariID"].ToString());
                            Cari cr = db.Cari.Where(x => x.ID == cariid).FirstOrDefault<Cari>();
                            yt.Cari = cr.CariUnvan;
                            int borclualacakli = Convert.ToInt32(dr["BorcluAlacakli"].ToString());
                            if (borclualacakli==-1)
                            {
                                int firmaID1 = Convert.ToInt32(FirmaID);
                                COMPANY_DETAIL borcalacak = db.COMPANY_DETAIL.Where(x => x.FirmaID == firmaID1).FirstOrDefault<COMPANY_DETAIL>();
                                yt.BorcluAlacakli = borcalacak.FirmaAdi;
                            }
                            else
                            {
                                try
                                {

                                    Cari borcalacak = db.Cari.Where(x => x.ID == borclualacakli).FirstOrDefault<Cari>();
                                    yt.BorcluAlacakli = borcalacak.CariUnvan;
                                }
                                catch (Exception)
                                {
                                    yt.BorcluAlacakli = "";
                                }
                            }
                            
                          
                            yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                            yt.PB = dr["ParaBirimi"].ToString();
                            yt.CekBankasi = dr["BankaAdi"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();
                            dt = Convert.ToDateTime(dr["KayitTarih"].ToString());
                            yt.IslemTarihiF = dt.ToString("dd.MM.yyyy");

                            yt.PersonelAdi = Convert.ToInt32(dr["CariID"].ToString());





                            yonetim.Add(yt);

                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult YeniCekSenet(int id = 0, int cek = 0, int ceksenet = 0)
        {

            sayazilimEntities db = new sayazilimEntities();
            PAYROLL py = new PAYROLL();
            if (id == 0)
            {

                if (cek == 0)
                {
                    AyarMetot.Siradaki("", "CEKSENET", "İslemNo", Session["FirmaID"].ToString());
                    ViewBag.CekSiradaki = AyarMetot.GetNumara;
                    AyarMetot.Siradaki("", "CEK", "PortfoyNo", Session["FirmaID"].ToString());
                    ViewBag.PortfoySiradakiNo = AyarMetot.GetNumara;

                    if (ceksenet == 0)
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                else
                {
                    PAYROLL_CS ceklist = db.PAYROLL_CS.Where(x => x.ID == cek).FirstOrDefault();
                    py = db.PAYROLL.Where(x => x.ID == ceklist.BordroID).FirstOrDefault();
                    DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                    py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                    var pylist = db.PAYROLL_CS.Where(x => x.BordroID == py.ID).ToList();
                    foreach (var item in pylist)
                    {
                        ViewBag.CekDurum = item.IslemTipi;
                        if (item.IslemTipi == "Çek")
                        {
                            ViewBag.CekSenetDurum = "Çek";
                        }
                        else
                        {
                            ViewBag.CekSenetDurum = "Senet";
                        }
                    }

                    ViewBag.pyList = pylist.ToList();
                }
            }
            else if (id != 0)
            {

                py = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault();
                DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                var pylist = db.PAYROLL_CS.Where(x => x.BordroID == id).ToList();
                foreach (var item in pylist)
                {
                    ViewBag.CekDurum = item.IslemTipi;
                    if (item.IslemTipi == "Çek")
                    {
                        ViewBag.CekSenetDurum = "Çek";
                    }
                    else
                    {
                        ViewBag.CekSenetDurum = "Senet";
                    }
                }
                ViewBag.pyList = pylist.ToList();

            }

            return View(py);
        }

        [HttpPost]
        public ActionResult YeniCekSenet(PAYROLL data, string json, string ceksenet)
        {
            sayazilimEntities db = new sayazilimEntities();

            int PayrollID = -1;
            json = "[" + json + "]";
            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                PAYROLL tk = new PAYROLL();
                tk = data;
                tk.IslemTipi = "Çek/Senet Girişi";
                tk.VadeFarki = 0;

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);



                tk.Tutar = 0;
                tk.KasaBankaID = -1;
                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.OzelKodID = -1;
                tk.Durumu = "Aktif";
                tk.BankaIDGC = -1;
                tk.BankaIDOT = -1;
                tk.KasaIDOT = -1;
                tk.FaktoringTahsilatID = -1;
                tk.FaktoringTediyeID = -1;
                tk.ProjeID = -1;
                tk.AdresID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                tk.PrimOr = -1;
                tk.SantiyeID = -1;
                tk.IslemCODE = "-1";
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                tk.IslemCODE = "CSNGR";


                db.PAYROLL.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM PAYROLL Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        PayrollID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                PAYROLL tk = db.PAYROLL.Where(x => x.ID == data.ID).FirstOrDefault<PAYROLL>();



                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);


                tk.Tutar = 0;
                tk.CariID = data.CariID;
                tk.IslemTarih = data.IslemTarih;
                tk.OrtalamaVade = data.OrtalamaVade;
                tk.PersonelID = data.PersonelID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.Aciklama = data.Aciklama;
                PayrollID = data.ID;


                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            List<PAYROLL_CS> items = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                PAYROLL_CS er = items[i];
                try
                {
                    if (er.ID.ToString() == "-1" || er.ID.ToString() == "0")
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_CS", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "PAYROLL_CS");
                                    DataRow df = ds.Tables["PAYROLL_CS"].NewRow();

                                    df["Durumu"] = "Aktif";
                                    df["BordroID"] = PayrollID;
                                    Cari cari = db.Cari.Where(x => x.ID == data.CariID).FirstOrDefault<Cari>();
                                    df["CariUnvan"] = cari.CariUnvan;
                                    df["CariID"] = data.CariID;
                                    df["BorcluAlacakli"] = data.CariID;
                                    df["PortfoyNo"] = er.PortfoyNo;

                                    df["IslemTipi"] = ceksenet;
                                    if (ceksenet == "Çek")
                                    {
                                        df["BordroTipi"] = "Müşteri Çeki";
                                    }
                                    else if (ceksenet == "Senet")
                                    {
                                        df["BordroTipi"] = "Müşteri Senedi";
                                    }

                                    df["SeriNo"] = er.SeriNo;

                                    df["IslemTarihi"] = data.IslemTarih;
                                    df["evrakGT"] = DateTime.Now;
                                    df["VadeTarih"] = er.VadeTarih;
                                    df["Durumu"] = "Portföyde";
                                    df["Aciklama"] = data.Aciklama;
                                    df["BankaAdi"] = er.BankaAdi;
                                    df["SubeAdi"] = er.SubeAdi;
                                    df["OdemeYeri"] = er.OdemeYeri;
                                    df["KesideciUnvan"] = er.KesideciUnvan;
                                    df["KesideciTel"] = er.KesideciTel;
                                    df["ParaBirimi"] = data.ParaBirimi;
                                    df["Tutar"] = er.Tutar;
                                    df["Donem"] = DateTime.Now.Year;
                                    df["RiskLimitKapsam"] = "Kapsamında";
                                    df["ZimmetPersonelID"] = "-1";
                                    df["ZimmetAciklama"] = "";
                                    df["PersonelID"] = data.PersonelID;
                                    df["KayitPersonelID"] = Session["PersonelID"].ToString();
                                    df["KayitTarih"] = DateTime.Now;
                                    df["ProjeID"] = "-1";
                                    df["OzelKodID"] = "-1";
                                    df["TahsilEdilen"] = 0;
                                    df["Ciro"] = er.Ciro;
                                    df["BankaID"] = -1;
                                    df["CekBankaHesapNo"] = er.CekBankaHesapNo;
                                    df["HizliSatistan"] = 0;
                                    df["FirmaID"] = Session["FirmaID"].ToString();


                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                    df["Company_Code"] = company_code;
                                    df["SantiyeID"] = -1;

                                    ds.Tables["PAYROLL_CS"].Rows.Add(df);
                                    da.Update(ds, "PAYROLL_CS");

                                    int CekSenetID = -1;
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                    }

                                    using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                    {
                                        using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                        {
                                            DataSet dsdurum = new DataSet();
                                            dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                            DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                            drd["BordroID"] = PayrollID;
                                            drd["CekSenetID"] = CekSenetID;
                                            drd["CariID"] = data.CariID;
                                            drd["BorcluAlacakli"] = data.CariID;
                                            drd["BankaID"] = -1;
                                            drd["KasaID"] = -1;
                                            drd["Durum"] = "Portföyde";
                                            drd["CariID"] = data.CariID;
                                            drd["Tarih"] = data.IslemTarih;
                                            drd["Donem"] = AyarMetot.Donem;
                                            drd["FirmaID"] = Session["FirmaID"].ToString();
                                            drd["Company_Code"] = company_code;


                                            dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                            dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");
                                        }
                                    }







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
                                    "select * from PAYROLL_CS where ID='" + er.ID + "'", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "PAYROLL_CS");
                                    DataRow[] adf = ds.Tables["PAYROLL_CS"].Select("ID='" + er.ID + "'");
                                    if (adf.Length != 0)
                                    {
                                        DataRow df = adf[0];
                                        df["Durumu"] = "Portföyde";
                                        df["BordroID"] = PayrollID;
                                        Cari cari = db.Cari.Where(x => x.ID == data.CariID).FirstOrDefault<Cari>();
                                        df["CariUnvan"] = cari.CariUnvan;
                                        df["BorcluAlacakli"] = data.CariID;
                                        df["PortfoyNo"] = er.PortfoyNo;

                                        df["IslemTipi"] = ceksenet;
                                        if (ceksenet == "Çek")
                                        {
                                            df["BordroTipi"] = "Müşteri Çeki";
                                        }
                                        else if (ceksenet == "Senet")
                                        {
                                            df["BordroTipi"] = "Müşteri Senedi";
                                        }

                                        df["SeriNo"] = er.SeriNo;

                                        df["IslemTarihi"] = data.IslemTarih;
                                        df["VadeTarih"] = er.VadeTarih;
                                        df["Durumu"] = "Portföyde";
                                        df["Aciklama"] = data.Aciklama;
                                        df["BankaAdi"] = er.BankaAdi;
                                        df["SubeAdi"] = er.SubeAdi;
                                        df["OdemeYeri"] = er.OdemeYeri;
                                        df["KesideciUnvan"] = er.KesideciUnvan;
                                        df["KesideciTel"] = er.KesideciTel;
                                        df["ParaBirimi"] = data.ParaBirimi;
                                        df["Tutar"] = er.Tutar;
                                        df["RiskLimitKapsam"] = "Kapsamında";
                                        df["ZimmetPersonelID"] = "-1";
                                        df["ZimmetAciklama"] = "";
                                        df["PersonelID"] = data.PersonelID;
                                        df["KayitPersonelID"] = Session["PersonelID"].ToString();
                                        df["ProjeID"] = "-1";
                                        df["OzelKodID"] = "-1";
                                        df["TahsilEdilen"] = 0;
                                        df["Ciro"] = er.Ciro;
                                        df["BankaID"] = -1;
                                        df["CekBankaHesapNo"] = er.CekBankaHesapNo;
                                        df["HizliSatistan"] = 0;
                                        df["FirmaID"] = Session["FirmaID"].ToString();


                                        string firmaid = Session["FirmaID"].ToString();
                                        string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                        df["Company_Code"] = company_code;
                                        df["SantiyeID"] = -1;
                                        da.Update(ds, "PAYROLL_CS");

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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }

            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    string sqlsorgu =
                        "UPDATE P SET Tutar =(select SUM(C.Tutar) from PAYROLL_CS C where BordroID=P.ID) from PAYROLL P where P.ID=" +
                        PayrollID;
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand(sqlsorgu, conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception e)
            {

            }
            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Çek Çıkış

        public ActionResult GetCikisCekler(string durum = "")
        {
            string CariID = durum;

            if (CariID != "")
            {
                CariID = " and CariID=" + CariID;
            }
            else
            {
                CariID = "";
            }


            string FirmaID = Session["FirmaID"].ToString();
            List<Cekler> yonetim = new List<Cekler>();
            string sorg = @"select * from PAYROLL_CS Where Durumu='Portföyde' and FirmaID =" + FirmaID + ' ' + CariID;
            sayazilimEntities db = new sayazilimEntities();

            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
            {
                con.Open();
                using (SqlCommand BordroGetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = BordroGetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Cekler yt = new Cekler();
                            DateTime dt = new DateTime();
                            yt.Durumu = dr["Durumu"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.BordroTipi = dr["BordroTipi"].ToString();
                            int bordroid = Convert.ToInt32(dr["BordroID"].ToString());
                            PAYROLL brd = db.PAYROLL.Where(x => x.ID == bordroid).FirstOrDefault();
                            yt.BordroNumarasi = brd.IslemNo;
                            yt.IslemNumarasi = dr["PortfoyNo"].ToString();

                            yt.SeriNumarasi = dr["SeriNo"].ToString();
                            dt = Convert.ToDateTime(dr["VadeTarih"].ToString());
                            yt.VadeTarihi = dt.ToString("dd.MM.yyyy");
                            int cariid = Convert.ToInt32(dr["CariID"].ToString());
                            Cari cr = db.Cari.Where(x => x.ID == cariid).FirstOrDefault<Cari>();
                            yt.Cari = cr.CariUnvan;
                            int borclualacakli = Convert.ToInt32(dr["BorcluAlacakli"].ToString());
                            Cari borcalacak = db.Cari.Where(x => x.ID == borclualacakli).FirstOrDefault<Cari>();
                            yt.BorcluAlacakli = borcalacak.CariUnvan;
                            yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                            yt.PB = dr["ParaBirimi"].ToString();
                            yt.CekBankasi = dr["BankaAdi"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();
                            dt = Convert.ToDateTime(dr["KayitTarih"].ToString());
                            yt.IslemTarihiF = dt.ToString("dd.MM.yyyy");

                            yt.PersonelAdi = Convert.ToInt32(dr["CariID"].ToString());





                            yonetim.Add(yt);

                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult YeniCekCikis(int id = 0, int cek = 0)
        {
            sayazilimEntities db = new sayazilimEntities();
            PAYROLL py = new PAYROLL();
            if (id == 0)
            {

                if (cek == 0)
                {
                    AyarMetot.Siradaki("", "CEKSENET", "İslemNo", Session["FirmaID"].ToString());
                    ViewBag.CekSiradaki = AyarMetot.GetNumara;
                    AyarMetot.Siradaki("", "CEK", "PortfoyNo", Session["FirmaID"].ToString());
                    ViewBag.PortfoySiradakiNo = AyarMetot.GetNumara;
                }
                else
                {
                    PAYROLL_CS ceklist = db.PAYROLL_CS.Where(x => x.ID == cek).FirstOrDefault();
                    py = db.PAYROLL.Where(x => x.ID == ceklist.BordroID).FirstOrDefault();
                    DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                    py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                    var pylist = db.PAYROLL_CS.Where(x => x.BordroID == py.ID).ToList();
                    foreach (var item in pylist)
                    {
                        ViewBag.CekDurum = item.IslemTipi;
                    }
                    ViewBag.pyList = pylist.ToList();
                }
            }
            else if (id != 0)
            {

                py = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault();
                DateTime tarih = Convert.ToDateTime(py.IslemTarih);
                py.IslemTarih = Convert.ToDateTime(tarih.ToString("yyyy-MM-dd"));
                var pylist = db.PAYROLL_CS.Where(x => x.BordroID == id).ToList();
                foreach (var item in pylist)
                {
                    ViewBag.CekDurum = item.IslemTipi;
                }
                ViewBag.pyList = pylist.ToList();

            }

            return View(py);
        }

        [HttpPost]
        public ActionResult YeniCekCikis(PAYROLL data, string json, string ceksenet)
        {
            sayazilimEntities db = new sayazilimEntities();

            int PayrollID = -1;
            json = "[" + json + "]";
            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                PAYROLL tk = new PAYROLL();
                tk = data;
                tk.IslemTipi = "Çek/Senet Çıkışı";
                tk.VadeFarki = 0;

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);
                decimal tutar = 0;
                foreach (var item in payroll)
                {
                    tutar = tutar + Convert.ToDecimal(item.Tutar);
                }

                tk.Tutar = tutar;

                //tk.Tutar = 0;
                tk.KasaBankaID = -1;
                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.OzelKodID = -1;
                tk.Durumu = "Aktif";
                tk.BankaIDGC = -1;
                tk.BankaIDOT = -1;
                tk.KasaIDOT = -1;
                tk.FaktoringTahsilatID = -1;
                tk.FaktoringTediyeID = -1;
                tk.ProjeID = -1;
                tk.AdresID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                tk.PrimOr = -1;
                tk.SantiyeID = -1;
                tk.IslemCODE = "-1";
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                tk.IslemCODE = "CSNCK";


                db.PAYROLL.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM PAYROLL Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        PayrollID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                PAYROLL tk = db.PAYROLL.Where(x => x.ID == data.ID).FirstOrDefault<PAYROLL>();



                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);


                //tk.Tutar = 0;
                tk.CariID = data.CariID;
                tk.IslemTarih = data.IslemTarih;
                tk.OrtalamaVade = data.OrtalamaVade;
                tk.PersonelID = data.PersonelID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.Aciklama = data.Aciklama;
                PayrollID = data.ID;


                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            List<PAYROLL_CS> items = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                PAYROLL_CS er = items[i];
                try
                {
                    if (er.ID.ToString() == "-1" || er.ID.ToString() == "0")
                    {
                        using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
                        {



                            if (con.State == ConnectionState.Closed) con.Open();
                            using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_CS", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "PAYROLL_CS");
                                    DataRow df = ds.Tables["PAYROLL_CS"].NewRow();

                                    df["BordroID"] = PayrollID;
                                    Cari cari = db.Cari.Where(x => x.ID == data.CariID).FirstOrDefault<Cari>();
                                    df["CariUnvan"] = cari.CariUnvan;
                                    df["CariID"] = data.CariID;
                                    df["BorcluAlacakli"] = -1;
                                    df["PortfoyNo"] = er.PortfoyNo;

                                    df["IslemTipi"] = ceksenet;
                                    if (ceksenet == "Çek")
                                    {
                                        df["BordroTipi"] = "Çekimiz";
                                    }
                                    else if (ceksenet == "Senet")
                                    {
                                        df["BordroTipi"] = "Senedimiz";
                                    }

                                    df["SeriNo"] = er.SeriNo;

                                    df["IslemTarihi"] = data.IslemTarih;
                                    df["evrakGT"] = DateTime.Now;
                                    df["VadeTarih"] = er.VadeTarih;
                                    df["Durumu"] = "Ciro Edildi";
                                    df["Aciklama"] = data.Aciklama;
                                    df["BankaAdi"] = er.BankaAdi;
                                    df["SubeAdi"] = er.SubeAdi;
                                    df["OdemeYeri"] = er.OdemeYeri;
                                    df["KesideciUnvan"] = er.KesideciUnvan;
                                    df["KesideciTel"] = er.KesideciTel;
                                    df["ParaBirimi"] = data.ParaBirimi;
                                    df["Tutar"] = er.Tutar;
                                    df["Donem"] = DateTime.Now.Year;
                                    df["RiskLimitKapsam"] = "Kapsamında";
                                    df["ZimmetPersonelID"] = "-1";
                                    df["ZimmetAciklama"] = "";
                                    df["PersonelID"] = data.PersonelID;
                                    df["KayitPersonelID"] = Session["PersonelID"].ToString();
                                    df["KayitTarih"] = DateTime.Now;
                                    df["ProjeID"] = "-1";
                                    df["OzelKodID"] = "-1";
                                    df["TahsilEdilen"] = 0;
                                    df["Ciro"] = er.Ciro;
                                    df["BankaID"] = -1;
                                    df["CekBankaHesapNo"] = er.CekBankaHesapNo;
                                    df["HizliSatistan"] = 0;
                                    df["FirmaID"] = Session["FirmaID"].ToString();


                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                    df["Company_Code"] = company_code;
                                    df["SantiyeID"] = -1;

                                    ds.Tables["PAYROLL_CS"].Rows.Add(df);
                                    da.Update(ds, "PAYROLL_CS");


                                    int CekSenetID = -1;
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                    }

                                    using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                    {
                                        using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                        {
                                            DataSet dsdurum = new DataSet();
                                            dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                            DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                            drd["BordroID"] = PayrollID;
                                            drd["CekSenetID"] = CekSenetID;
                                            drd["CariID"] = data.CariID;
                                            drd["BorcluAlacakli"] = -1;
                                            drd["BankaID"] = -1;
                                            drd["KasaID"] = -1;
                                            drd["Durum"] = "Ciro Edildi";
                                            drd["CariID"] = data.CariID;
                                            drd["Tarih"] = data.IslemTarih;
                                            drd["Donem"] = AyarMetot.Donem;
                                            drd["FirmaID"] = Session["FirmaID"].ToString();
                                            drd["Company_Code"] = company_code;


                                            dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                            dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");



                                        }
                                    }


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
                                    "select * from PAYROLL_CS where ID='" + er.ID + "'", con))
                            {
                                using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                                {
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "PAYROLL_CS");
                                    DataRow[] adf = ds.Tables["PAYROLL_CS"].Select("ID='" + er.ID + "'");
                                    if (adf.Length != 0)
                                    {
                                        DataRow df = adf[0];
                                        df["BordroID"] = PayrollID;
                                        Cari cari = db.Cari.Where(x => x.ID == data.CariID).FirstOrDefault<Cari>();
                                        df["CariUnvan"] = cari.CariUnvan;
                                        df["IslemTipi"] = ceksenet;
                                        if (ceksenet == "Çek")
                                        {
                                            df["BordroTipi"] = "Müşteri Çeki";
                                        }
                                        else if (ceksenet == "Senet")
                                        {
                                            df["BordroTipi"] = "Müşteri Senedi";
                                        }
                                        df["Durumu"] = "Ciro Edildi";
                                        df["CariID"] = data.CariID;
                                        da.Update(ds, "PAYROLL_CS");


                                        string firmaid = Session["FirmaID"].ToString();
                                        string company_code = "SA01" + firmaid.PadLeft(3, '0');

                                        int CekSenetID = -1;
                                        using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                        {
                                            if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                        }
                                        using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                        {
                                            using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                            {
                                                DataSet dsdurum = new DataSet();
                                                dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                                DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                                drd["BordroID"] = PayrollID;
                                                drd["CekSenetID"] = CekSenetID;
                                                drd["CariID"] = data.CariID;
                                                drd["BorcluAlacakli"] = data.CariID;
                                                drd["BankaID"] = -1;
                                                drd["KasaID"] = -1;
                                                drd["Durum"] = "Ciro Edildi";
                                                drd["CariID"] = data.CariID;
                                                drd["Tarih"] = data.IslemTarih;
                                                drd["Donem"] = AyarMetot.Donem;
                                                drd["FirmaID"] = Session["FirmaID"].ToString();
                                                drd["Company_Code"] = company_code;


                                                dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                                dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");



                                            }
                                        }


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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }


            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Delete
        [HttpPost]
        public ActionResult DeleteCek(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE PAYROLL_CS  where ID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE PAYROLL_DETAIL_CS  where CekSenetID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                }


                db.SaveChanges();
                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeletePayroll(int id)
        {
            using (sayazilimEntities db = new sayazilimEntities())
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection conp = new SqlConnection(strcon))
                {
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE PAYROLL_CS  where BordroID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                    if (conp.State == ConnectionState.Closed) conp.Open();
                    using (var sqlWrite = new SqlCommand("DELETE PAYROLL_DETAIL_CS  where BordroID='" + id + "'", conp))
                    {

                        sqlWrite.ExecuteNonQuery();
                    }
                }


                PAYROLL emp = db.PAYROLL.Where(x => x.ID == id).FirstOrDefault<PAYROLL>();
                db.PAYROLL.Remove(emp);
                db.SaveChanges();


                return Json(new { success = true, message = "Kayıt Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region Tahsilatİşlamleri
        public ActionResult GetTCekler(string durum = "", string Btip = "")
        {

            if (durum == "TahsilatKasa")
            {
                if (Btip != "")
                {
                    Btip = " and  BordroTipi=N'" + Btip + "'";
                }
            }
            else if (durum == "TahsilatBanka")
            {
                if (Btip != "")
                {
                    Btip = " and  BordroTipi=N'" + Btip + "'";
                }
            }

            string FirmaID = Session["FirmaID"].ToString();
            List<Cekler> yonetim = new List<Cekler>();
            string sorg = @"select * from PAYROLL_CS Where Durumu='Portföyde' and FirmaID =" + FirmaID + Btip;
            sayazilimEntities db = new sayazilimEntities();

            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
            {
                con.Open();
                using (SqlCommand BordroGetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = BordroGetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Cekler yt = new Cekler();
                            DateTime dt = new DateTime();
                            yt.Durumu = dr["Durumu"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.BordroTipi = dr["BordroTipi"].ToString();
                            int bordroid = Convert.ToInt32(dr["BordroID"].ToString());
                            PAYROLL brd = db.PAYROLL.Where(x => x.ID == bordroid).FirstOrDefault();
                            yt.BordroNumarasi = brd.IslemNo;
                            yt.IslemNumarasi = dr["PortfoyNo"].ToString();

                            yt.SeriNumarasi = dr["SeriNo"].ToString();
                            dt = Convert.ToDateTime(dr["VadeTarih"].ToString());
                            yt.VadeTarihi = dt.ToString("dd.MM.yyyy");
                            int cariid = Convert.ToInt32(dr["CariID"].ToString());
                            try
                            {
                                Cari cr = db.Cari.Where(x => x.ID == cariid).FirstOrDefault<Cari>();
                                yt.Cari = cr.CariUnvan;
                            }
                            catch (Exception)
                            {
                                yt.Cari = "";
                            }

                            int borclualacakli = Convert.ToInt32(dr["BorcluAlacakli"].ToString());
                            try
                            {
                                Cari borcalacak = db.Cari.Where(x => x.ID == borclualacakli).FirstOrDefault<Cari>();
                                yt.BorcluAlacakli = borcalacak.CariUnvan;
                            }
                            catch (Exception)
                            {
                                yt.BorcluAlacakli = "";
                            }

                            yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                            yt.PB = dr["ParaBirimi"].ToString();
                            yt.CekBankasi = dr["BankaAdi"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();
                            dt = Convert.ToDateTime(dr["KayitTarih"].ToString());
                            yt.IslemTarihiF = dt.ToString("dd.MM.yyyy");

                            yt.PersonelAdi = Convert.ToInt32(dr["CariID"].ToString());





                            yonetim.Add(yt);

                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult YeniTahsilatKasa(PAYROLL data, string json)
        {
            sayazilimEntities db = new sayazilimEntities();

            int PayrollID = -1;
            json = "[" + json + "]";
            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                PAYROLL tk = new PAYROLL();
                tk = data;
                tk.IslemTipi = "Kasaya Çek Senet Tahsil İşlemi";
                tk.VadeFarki = 0;

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);



               // tk.Tutar = 0;
                tk.KasaBankaID = -1;
                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.OzelKodID = -1;
                tk.Durumu = "Aktif";
                tk.BankaIDGC = -1;
                tk.BankaIDOT = -1;
                tk.KasaIDOT = data.KasaIDOT;
                tk.FaktoringTahsilatID = -1;
                tk.FaktoringTediyeID = -1;
                tk.ProjeID = -1;
                tk.AdresID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                tk.PrimOr = -1;
                tk.SantiyeID = -1;
                tk.IslemCODE = "-1";
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                tk.IslemCODE = "KSYATAH";

                tk.CariID = -1;

                db.PAYROLL.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM PAYROLL Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        PayrollID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                PAYROLL tk = db.PAYROLL.Where(x => x.ID == data.ID).FirstOrDefault<PAYROLL>();

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

                //tk.Tutar = 0;
                tk.CariID = -1;
                tk.IslemTarih = data.IslemTarih;
                tk.KasaIDOT = data.KasaIDOT;
                tk.PersonelID = data.PersonelID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.Aciklama = data.Aciklama;
                PayrollID = data.ID;

                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            List<PAYROLL_CS> items = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                PAYROLL_CS er = items[i];
                try
                {

                    using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da =
                            new System.Data.SqlClient.SqlDataAdapter(
                                "select * from PAYROLL_CS where ID='" + er.ID + "'", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "PAYROLL_CS");
                                DataRow[] adf = ds.Tables["PAYROLL_CS"].Select("ID='" + er.ID + "'");
                                if (adf.Length != 0)
                                {
                                    DataRow df = adf[0];

                                    df["Durumu"] = "Tahsil Edildi";

                                    da.Update(ds, "PAYROLL_CS");


                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');

                                    int CekSenetID = -1;
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                    }

                                    using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                    {
                                        using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                        {
                                            DataSet dsdurum = new DataSet();
                                            dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                            DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                            drd["BordroID"] = PayrollID;
                                            drd["CekSenetID"] = CekSenetID;
                                            drd["CariID"] = data.CariID;
                                            drd["BorcluAlacakli"] = data.CariID;
                                            drd["BankaID"] = -1;
                                            drd["KasaID"] = -1;
                                            drd["Durum"] = "Tahsil Edildi";
                                            drd["CariID"] = data.CariID;
                                            drd["Tarih"] = data.IslemTarih;
                                            drd["Donem"] = AyarMetot.Donem;
                                            drd["FirmaID"] = Session["FirmaID"].ToString();
                                            drd["Company_Code"] = company_code;


                                            dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                            dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");
                                        }
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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }


            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult YeniTahsilatBanka(PAYROLL data, string json)
        {
            sayazilimEntities db = new sayazilimEntities();

            int PayrollID = -1;
            json = "[" + json + "]";
            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                PAYROLL tk = new PAYROLL();
                tk = data;
                tk.IslemTipi = "Bankaya Çek Senet Tahsil İşlemi";
                tk.VadeFarki = 0;

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

                //tk.Tutar = 0;
                tk.KasaBankaID = -1;
                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.OzelKodID = -1;
                tk.Durumu = "Aktif";
                tk.BankaIDGC = -1;
                tk.BankaIDOT = data.BankaIDOT;
                tk.KasaIDOT = -1;
                tk.FaktoringTahsilatID = -1;
                tk.FaktoringTediyeID = -1;
                tk.ProjeID = -1;
                tk.AdresID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                tk.PrimOr = -1;
                tk.SantiyeID = -1;
                tk.IslemCODE = "-1";
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                tk.IslemCODE = "BNKYATAH";

                tk.CariID = -1;

                db.PAYROLL.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM PAYROLL Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        PayrollID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                PAYROLL tk = db.PAYROLL.Where(x => x.ID == data.ID).FirstOrDefault<PAYROLL>();



                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);


                //tk.Tutar = 0;
                tk.CariID = data.CariID;
                tk.IslemTarih = data.IslemTarih;
                tk.KasaIDOT = data.KasaIDOT;
                tk.PersonelID = data.PersonelID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.Aciklama = data.Aciklama;
                PayrollID = data.ID;



                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            List<PAYROLL_CS> items = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                PAYROLL_CS er = items[i];
                try
                {


                    using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da =
                            new System.Data.SqlClient.SqlDataAdapter(
                                "select * from PAYROLL_CS where ID='" + er.ID + "'", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "PAYROLL_CS");
                                DataRow[] adf = ds.Tables["PAYROLL_CS"].Select("ID='" + er.ID + "'");
                                if (adf.Length != 0)
                                {
                                    DataRow df = adf[0];
                                    df["Durumu"] = "Tahsil Edildi";
                                    da.Update(ds, "PAYROLL_CS");


                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');
                                   

                                    int CekSenetID = -1;
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                    }

                                    using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                    {
                                        using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                        {
                                            DataSet dsdurum = new DataSet();
                                            dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                            DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                            drd["BordroID"] = PayrollID;
                                            drd["CekSenetID"] = CekSenetID;
                                            drd["CariID"] = data.CariID;
                                            drd["BorcluAlacakli"] = data.CariID;
                                            drd["BankaID"] = -1;
                                            drd["KasaID"] = -1;
                                            drd["Durum"] = "Tahsil Edildi";
                                            drd["CariID"] = data.CariID;
                                            drd["Tarih"] = data.IslemTarih;
                                            drd["Donem"] = AyarMetot.Donem;
                                            drd["FirmaID"] = Session["FirmaID"].ToString();
                                            drd["Company_Code"] = company_code;


                                            dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                            dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");
                                        }
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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }


            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Ödeme İşlemleri

        public ActionResult GetOCekler(string durum = "", string Btip = "")
        {

            if (durum == "OdemeKasa")
            {
                if (Btip != "")
                {
                    Btip = " and  BordroTipi=N'" + Btip + "'";
                }
            }
            else if (durum == "OdemeBanka")
            {
                if (Btip != "")
                {
                    Btip = " and  BordroTipi=N'" + Btip + "'";
                }
            }

            string FirmaID = Session["FirmaID"].ToString();
            List<Cekler> yonetim = new List<Cekler>();
            string sorg = @"select * from PAYROLL_CS Where Durumu='Ciro Edildi' and FirmaID =" + FirmaID + Btip;
            sayazilimEntities db = new sayazilimEntities();

            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
            {
                con.Open();
                using (SqlCommand BordroGetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = BordroGetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Cekler yt = new Cekler();
                            DateTime dt = new DateTime();
                            yt.Durumu = dr["Durumu"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.BordroTipi = dr["BordroTipi"].ToString();
                            int bordroid = Convert.ToInt32(dr["BordroID"].ToString());
                            PAYROLL brd = db.PAYROLL.Where(x => x.ID == bordroid).FirstOrDefault();
                            yt.BordroNumarasi = brd.IslemNo;
                            yt.IslemNumarasi = dr["PortfoyNo"].ToString();

                            yt.SeriNumarasi = dr["SeriNo"].ToString();
                            dt = Convert.ToDateTime(dr["VadeTarih"].ToString());
                            yt.VadeTarihi = dt.ToString("dd.MM.yyyy");
                            int cariid = Convert.ToInt32(dr["CariID"].ToString());
                            try
                            {
                                Cari cr = db.Cari.Where(x => x.ID == cariid).FirstOrDefault<Cari>();
                                yt.Cari = cr.CariUnvan;
                            }
                            catch (Exception)
                            {
                                yt.Cari = "";
                            }

                            int borclualacakli = Convert.ToInt32(dr["BorcluAlacakli"].ToString());
                            try
                            {
                                Cari borcalacak = db.Cari.Where(x => x.ID == borclualacakli).FirstOrDefault<Cari>();
                                yt.BorcluAlacakli = borcalacak.CariUnvan;
                            }
                            catch (Exception)
                            {
                                yt.BorcluAlacakli = "";
                            }

                            yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                            yt.PB = dr["ParaBirimi"].ToString();
                            yt.CekBankasi = dr["BankaAdi"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();
                            dt = Convert.ToDateTime(dr["KayitTarih"].ToString());
                            yt.IslemTarihiF = dt.ToString("dd.MM.yyyy");

                            yt.PersonelAdi = Convert.ToInt32(dr["CariID"].ToString());





                            yonetim.Add(yt);

                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult YeniOdemeKasa(PAYROLL data, string json)
        {
            sayazilimEntities db = new sayazilimEntities();

            int PayrollID = -1;
            json = "[" + json + "]";
            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                PAYROLL tk = new PAYROLL();
                tk = data;
                tk.IslemTipi = "Bankadan Çek Senet Ödeme İşlemi";
                tk.VadeFarki = 0;

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);



                //tk.Tutar = 0;
                tk.KasaBankaID = -1;
                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.OzelKodID = -1;
                tk.Durumu = "Aktif";
                tk.BankaIDGC = -1;
                tk.BankaIDOT = -1;
                tk.KasaIDOT = data.KasaIDOT;
                tk.FaktoringTahsilatID = -1;
                tk.FaktoringTediyeID = -1;
                tk.ProjeID = -1;
                tk.AdresID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                tk.PrimOr = -1;
                tk.SantiyeID = -1;
                tk.IslemCODE = "-1";
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                tk.IslemCODE = "BNKDANODE";

                tk.CariID = -1;

                db.PAYROLL.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM PAYROLL Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        PayrollID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                PAYROLL tk = db.PAYROLL.Where(x => x.ID == data.ID).FirstOrDefault<PAYROLL>();

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

                tk.Tutar = 0;
                tk.CariID = -1;
                tk.IslemTarih = data.IslemTarih;
                tk.KasaIDOT = data.KasaIDOT;
                tk.PersonelID = data.PersonelID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.Aciklama = data.Aciklama;
                PayrollID = data.ID;

                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            List<PAYROLL_CS> items = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                PAYROLL_CS er = items[i];
                try
                {

                    using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da =
                            new System.Data.SqlClient.SqlDataAdapter(
                                "select * from PAYROLL_CS where ID='" + er.ID + "'", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "PAYROLL_CS");
                                DataRow[] adf = ds.Tables["PAYROLL_CS"].Select("ID='" + er.ID + "'");
                                if (adf.Length != 0)
                                {
                                    DataRow df = adf[0];

                                    df["Durumu"] = "Ödendi";

                                    da.Update(ds, "PAYROLL_CS");


                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');

                                    int CekSenetID = -1;
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                    }

                                    using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                    {
                                        using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                        {
                                            DataSet dsdurum = new DataSet();
                                            dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                            DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                            drd["BordroID"] = PayrollID;
                                            drd["CekSenetID"] = CekSenetID;
                                            drd["CariID"] = data.CariID;
                                            drd["BorcluAlacakli"] = data.CariID;
                                            drd["BankaID"] = -1;
                                            drd["KasaID"] = -1;
                                            drd["Durum"] = "Ödendi";
                                            drd["CariID"] = data.CariID;
                                            drd["Tarih"] = data.IslemTarih;
                                            drd["Donem"] = AyarMetot.Donem;
                                            drd["FirmaID"] = Session["FirmaID"].ToString();
                                            drd["Company_Code"] = company_code;


                                            dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                            dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");
                                        }
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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }


            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult YeniOdemeBanka(PAYROLL data, string json)
        {
            sayazilimEntities db = new sayazilimEntities();

            int PayrollID = -1;
            json = "[" + json + "]";
            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                PAYROLL tk = new PAYROLL();
                tk = data;
                tk.IslemTipi = "Bankaya Çek Senet Tahsil İşlemi";
                tk.VadeFarki = 0;

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

                //tk.Tutar = 0;
                tk.KasaBankaID = -1;
                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.OzelKodID = -1;
                tk.Durumu = "Aktif";
                tk.BankaIDGC = -1;
                tk.BankaIDOT = data.BankaIDOT;
                tk.KasaIDOT = -1;
                tk.FaktoringTahsilatID = -1;
                tk.FaktoringTediyeID = -1;
                tk.ProjeID = -1;
                tk.AdresID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                tk.PrimOr = -1;
                tk.SantiyeID = -1;
                tk.IslemCODE = "-1";
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                tk.IslemCODE = "BNKYATAH";

                tk.CariID = -1;

                db.PAYROLL.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM PAYROLL Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        PayrollID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                PAYROLL tk = db.PAYROLL.Where(x => x.ID == data.ID).FirstOrDefault<PAYROLL>();



                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);


                tk.Tutar = 0;
                tk.CariID = data.CariID;
                tk.IslemTarih = data.IslemTarih;
                tk.KasaIDOT = data.KasaIDOT;
                tk.PersonelID = data.PersonelID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.Aciklama = data.Aciklama;
                PayrollID = data.ID;



                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            List<PAYROLL_CS> items = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                PAYROLL_CS er = items[i];
                try
                {


                    using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da =
                            new System.Data.SqlClient.SqlDataAdapter(
                                "select * from PAYROLL_CS where ID='" + er.ID + "'", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "PAYROLL_CS");
                                DataRow[] adf = ds.Tables["PAYROLL_CS"].Select("ID='" + er.ID + "'");
                                if (adf.Length != 0)
                                {
                                    DataRow df = adf[0];
                                    df["Durumu"] = "Tahsil Edildi";
                                    da.Update(ds, "PAYROLL_CS");

                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');

                                    int CekSenetID = -1;
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                    }

                                    using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                    {
                                        using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                        {
                                            DataSet dsdurum = new DataSet();
                                            dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                            DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                            drd["BordroID"] = PayrollID;
                                            drd["CekSenetID"] = CekSenetID;
                                            drd["CariID"] = data.CariID;
                                            drd["BorcluAlacakli"] = data.CariID;
                                            drd["BankaID"] = -1;
                                            drd["KasaID"] = -1;
                                            drd["Durum"] = "Tahsil Edildi";
                                            drd["CariID"] = data.CariID;
                                            drd["Tarih"] = data.IslemTarih;
                                            drd["Donem"] = AyarMetot.Donem;
                                            drd["FirmaID"] = Session["FirmaID"].ToString();
                                            drd["Company_Code"] = company_code;


                                            dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                            dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");
                                        }
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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }


            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Bankaya Çek Çıkış

        public ActionResult GetBankaIslemCekler(string Btip = "", string durum = "")
        {

            if (durum == "BankayaCikis")
            {
                if (Btip != "")
                {
                    Btip = " and Durumu='Portföyde' and BordroTipi=N'" + Btip + "'";
                }
            }

            else if (durum == "BankadanAl")
            {
                if (Btip != "")
                {
                    Btip = " and Durumu='Teminat' and BordroTipi=N'" + Btip + "'";
                }
            }
            

            string FirmaID = Session["FirmaID"].ToString();
            List<Cekler> yonetim = new List<Cekler>();
            string sorg = @"select * from PAYROLL_CS Where  FirmaID =" + FirmaID + ' ' + Btip;
            sayazilimEntities db = new sayazilimEntities();

            using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
            {
                con.Open();
                using (SqlCommand BordroGetir = new SqlCommand(sorg, con))
                {
                    using (SqlDataReader dr = BordroGetir.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Cekler yt = new Cekler();
                            DateTime dt = new DateTime();
                            yt.Durumu = dr["Durumu"].ToString();
                            yt.ID = Convert.ToInt32(dr["ID"]);
                            yt.BordroTipi = dr["BordroTipi"].ToString();
                            int bordroid = Convert.ToInt32(dr["BordroID"].ToString());
                            PAYROLL brd = db.PAYROLL.Where(x => x.ID == bordroid).FirstOrDefault();
                            yt.BordroNumarasi = brd.IslemNo;
                            yt.IslemNumarasi = dr["PortfoyNo"].ToString();

                            yt.SeriNumarasi = dr["SeriNo"].ToString();
                            dt = Convert.ToDateTime(dr["VadeTarih"].ToString());
                            yt.VadeTarihi = dt.ToString("dd.MM.yyyy");
                            int cariid = Convert.ToInt32(dr["CariID"].ToString());
                            Cari cr = db.Cari.Where(x => x.ID == cariid).FirstOrDefault<Cari>();
                            yt.Cari = cr.CariUnvan;
                            int borclualacakli = Convert.ToInt32(dr["BorcluAlacakli"].ToString());
                            Cari borcalacak = db.Cari.Where(x => x.ID == borclualacakli).FirstOrDefault<Cari>();
                            yt.BorcluAlacakli = borcalacak.CariUnvan;
                            yt.Tutar = Convert.ToDecimal(dr["Tutar"].ToString()).ToString("N2");
                            yt.PB = dr["ParaBirimi"].ToString();
                            yt.CekBankasi = dr["BankaAdi"].ToString();
                            yt.Aciklama = dr["Aciklama"].ToString();
                            dt = Convert.ToDateTime(dr["KayitTarih"].ToString());
                            yt.IslemTarihiF = dt.ToString("dd.MM.yyyy");

                            yt.PersonelAdi = Convert.ToInt32(dr["CariID"].ToString());
                            
                            yonetim.Add(yt);

                        }
                    }
                }
            }

            return Json(new { data = yonetim.Distinct() }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult BankayaCekCikis(PAYROLL data, string json, string ceksenet)
        {
            sayazilimEntities db = new sayazilimEntities();

            int PayrollID = -1;
            json = "[" + json + "]";
            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                PAYROLL tk = new PAYROLL();
                tk = data;
                tk.IslemTipi = "Banka Hesabına Çek/Senet Çıkış";
                tk.VadeFarki = 0;

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);



                tk.Tutar = 0;
                tk.KasaBankaID = -1;
                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.OzelKodID = -1;
                tk.Durumu = "Aktif";
                tk.BankaIDGC = -1;
                tk.BankaIDOT = -1;
                tk.KasaIDOT = -1;
                tk.FaktoringTahsilatID = -1;
                tk.FaktoringTediyeID = -1;
                tk.ProjeID = -1;
                tk.AdresID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                tk.PrimOr = -1;
                tk.SantiyeID = -1;
                tk.IslemCODE = "-1";
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                tk.IslemCODE = "BNKHSCK";
                tk.CariID = -1;

                db.PAYROLL.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM PAYROLL Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        PayrollID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                PAYROLL tk = db.PAYROLL.Where(x => x.ID == data.ID).FirstOrDefault<PAYROLL>();



                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);


                //tk.Tutar = 0;
                tk.CariID = data.CariID;
                tk.IslemTarih = data.IslemTarih;
                tk.OrtalamaVade = data.OrtalamaVade;
                tk.PersonelID = data.PersonelID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.Aciklama = data.Aciklama;
                PayrollID = data.ID;


                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            List<PAYROLL_CS> items = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                PAYROLL_CS er = items[i];
                try
                {
                    using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da =
                            new System.Data.SqlClient.SqlDataAdapter(
                                "select * from PAYROLL_CS where ID='" + er.ID + "'", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "PAYROLL_CS");
                                DataRow[] adf = ds.Tables["PAYROLL_CS"].Select("ID='" + er.ID + "'");
                                if (adf.Length != 0)
                                {
                                    DataRow df = adf[0];
                                    df["BordroID"] = PayrollID;
                                   
                                    df["Durumu"] = "Teminat";
                                   
                                   
                                    da.Update(ds, "PAYROLL_CS");


                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');

                                    int CekSenetID = -1;
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                    }

                                    using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                    {
                                        using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                        {
                                            DataSet dsdurum = new DataSet();
                                            dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                            DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                            drd["BordroID"] = PayrollID;
                                            drd["CekSenetID"] = CekSenetID;
                                            drd["CariID"] = data.CariID;
                                            drd["BorcluAlacakli"] = data.CariID;
                                            drd["BankaID"] = -1;
                                            drd["KasaID"] = -1;
                                            drd["Durum"] = "Teminat";
                                            drd["CariID"] = data.CariID;
                                            drd["Tarih"] = data.IslemTarih;
                                            drd["Donem"] = AyarMetot.Donem;
                                            drd["FirmaID"] = Session["FirmaID"].ToString();
                                            drd["Company_Code"] = company_code;


                                            dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                            dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");
                                        }
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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }


            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BankadanCekAl(PAYROLL data, string json, string ceksenet)
        {
            sayazilimEntities db = new sayazilimEntities();

            int PayrollID = -1;
            json = "[" + json + "]";
            string Message = "Kayıt Eklendi";
            if (data.ID == -1)
            {
                PAYROLL tk = new PAYROLL();
                tk = data;
                tk.IslemTipi = "Banka Hesabından Çek/Senet Giriş";
                tk.VadeFarki = 0;

                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);



                tk.Tutar = 0;
                tk.KasaBankaID = -1;
                tk.Donem = DateTime.Now.Year.ToString();
                tk.KayitPersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
                tk.KayitTarih = DateTime.Now;
                tk.Kur = 1;
                tk.OzelKodID = -1;
                tk.Durumu = "Aktif";
                tk.BankaIDGC = -1;
                tk.BankaIDOT = -1;
                tk.KasaIDOT = -1;
                tk.FaktoringTahsilatID = -1;
                tk.FaktoringTediyeID = -1;
                tk.ProjeID = -1;
                tk.AdresID = -1;
                tk.FirmaID = Convert.ToInt16(Session["FirmaID"].ToString());
                tk.PrimOr = -1;
                tk.SantiyeID = -1;
                tk.IslemCODE = "-1";
                string firmaid = Session["FirmaID"].ToString();
                string company_code = "SA01" + firmaid.PadLeft(3, '0');
                tk.Company_Code = company_code;
                tk.IslemCODE = "BNKHSNDANGR";
                tk.CariID = -1;

                db.PAYROLL.Add(tk);
                db.SaveChanges();


                using (SqlConnection conp1 = new SqlConnection(AyarMetot.strcon))
                {
                    string srg = @"select top (1) ID FROM PAYROLL Order BY ID Desc";
                    if (conp1.State == ConnectionState.Closed) conp1.Open();
                    using (SqlCommand sID = new SqlCommand(srg, conp1))
                    {
                        PayrollID = Convert.ToInt32(sID.ExecuteScalar());
                    }
                }

            }
            else
            {
                PAYROLL tk = db.PAYROLL.Where(x => x.ID == data.ID).FirstOrDefault<PAYROLL>();



                List<PAYROLL_CS> payroll = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);


                //tk.Tutar = 0;
                tk.CariID = data.CariID;
                tk.IslemTarih = data.IslemTarih;
                tk.OrtalamaVade = data.OrtalamaVade;
                tk.PersonelID = data.PersonelID;
                tk.ParaBirimi = data.ParaBirimi;
                tk.Aciklama = data.Aciklama;
                PayrollID = data.ID;


                db.SaveChanges();
                Message = "Kayıt Güncellendi";
            }
            List<PAYROLL_CS> items = JsonConvert.DeserializeObject<List<PAYROLL_CS>>(json);

            for (int i = 0; i < items.Count; i++)
            {
                PAYROLL_CS er = items[i];
                try
                {
                    using (SqlConnection con = new SqlConnection(AyarMetot.conString))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        using (SqlDataAdapter da =
                            new System.Data.SqlClient.SqlDataAdapter(
                                "select * from PAYROLL_CS where ID='" + er.ID + "'", con))
                        {
                            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds, "PAYROLL_CS");
                                DataRow[] adf = ds.Tables["PAYROLL_CS"].Select("ID='" + er.ID + "'");
                                if (adf.Length != 0)
                                {
                                    DataRow df = adf[0];
                                  
                                    df["Durumu"] = "Portföyde";
                                    da.Update(ds, "PAYROLL_CS");


                                    string firmaid = Session["FirmaID"].ToString();
                                    string company_code = "SA01" + firmaid.PadLeft(3, '0');

                                    int CekSenetID = -1;
                                    using (SqlCommand cu = new SqlCommand("SELECT MAx(ID) FROM PAYROLL_CS", con))
                                    {
                                        if (cu.ExecuteScalar() != DBNull.Value) CekSenetID = Convert.ToInt32(cu.ExecuteScalar());
                                    }

                                    using (SqlDataAdapter dadurum = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from PAYROLL_DETAIL_CS", con))
                                    {
                                        using (SqlCommandBuilder cd = new SqlCommandBuilder(dadurum))
                                        {
                                            DataSet dsdurum = new DataSet();
                                            dadurum.Fill(dsdurum, "PAYROLL_DETAIL_CS");
                                            DataRow drd = dsdurum.Tables["PAYROLL_DETAIL_CS"].NewRow();
                                            drd["BordroID"] = PayrollID;
                                            drd["CekSenetID"] = CekSenetID;
                                            drd["CariID"] = data.CariID;
                                            drd["BorcluAlacakli"] = data.CariID;
                                            drd["BankaID"] = -1;
                                            drd["KasaID"] = -1;
                                            drd["Durum"] = "Portföyde";
                                            drd["CariID"] = data.CariID;
                                            drd["Tarih"] = data.IslemTarih;
                                            drd["Donem"] = AyarMetot.Donem;
                                            drd["FirmaID"] = Session["FirmaID"].ToString();
                                            drd["Company_Code"] = company_code;


                                            dsdurum.Tables["PAYROLL_DETAIL_CS"].Rows.Add(drd);
                                            dadurum.Update(dsdurum, "PAYROLL_DETAIL_CS");
                                        }
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
                        System.IO.File.WriteAllText(Path.Combine(@"C:\Users\Alperen\AppData\Local\Sayazilim", "sonuç.xml"), E1.ToString());
                    }
                    catch
                    { }
                }
            }


            var result = new { sonuc = 1, Message = Message };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion




        public ActionResult CekBilgi(int id)
        {

            using (sayazilimEntities db = new sayazilimEntities())
            {
                PAYROLL_CS emp = db.PAYROLL_CS.Where(x => x.ID == id).FirstOrDefault<PAYROLL_CS>();
                string vadetarihi = Convert.ToDateTime(emp.VadeTarih).ToString("yyyy-MM-dd");
                return Json(new { success = true, cs = emp, vade = vadetarihi }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}