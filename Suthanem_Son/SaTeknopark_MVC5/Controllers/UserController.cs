using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SaTeknopark_MVC5.Models;
using System.Net.Mail;
using System.Net.Mime;
using System.Data;

namespace SaTeknopark_MVC5.Controllers
{



    [AllowAnonymous]

    public class UserController : BaseController
    {
        public object ScaffoldingController { get; private set; }

        // GET: User
        //public ActionResult Login(string returnUrl)
        //{
        //    try
        //    {

        //        //Okuma işlem yapacağımız dosyanın yolunu belirtiyoruz.
        //        FileStream fs = new FileStream(Path.Combine(Server.MapPath("~/SERVISBELGELER"), "Hesap.txt"), FileMode.Open, FileAccess.Read);
        //        //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
        //        //2.parametre dosyanın açılacağını,
        //        //3.parametre dosyaya erişimin veri okumak için olacağını gösterir.
        //        StreamReader sw = new StreamReader(fs);
        //        //Okuma işlemi için bir StreamReader nesnesi oluşturduk.
        //        string yazi = sw.ReadLine();
        //        while (yazi != null)
        //        {
        //            ViewBag.Demo = yazi;
        //            yazi = sw.ReadLine();
        //        }
        //        sw.Close();
        //        fs.Close();
        //    }
        //    catch
        //    {

        //    }


        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}


        public ActionResult Remember()
        {
            ViewBag.Mesaj = "";
            return View();
        }


        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }



        //    var data = GirisKontrol.EntranceControl(model.UserName, model.Password, con);

        //    Session["Bayi"] = "";

        //    if (data.PersonelID == 0)
        //    {
        //        ModelState.AddModelError("", "Kullanıcı Bulunamadı.Kullanıcı Adı ve Şifrenizi Kontrol Ediniz!");
        //        return RedirectToAction("Login", "Login");
        //    }
        //    else
        //    {



        //        Session["UserName"] = data.UserName;
        //        Session["PersonelID"] = data.PersonelID;
        //        Session["PersonelType"] = data.PersonelType;
        //        if (data.PersonelType == 3)
        //        {
        //            Session["Bayi"] = "Bayi";
        //        }
        //        Session["User"] = data;
        //        Session["RaporGor"] = data.RaporGor;
        //        Session["PersonelAdi"] = data.PersonelAdi;
        //        Session["vDepoID"] = data.vDepoID;
        //        Session["vKasaID"] = data.vKasaID;
        //        Session["FirmaID"] = data.FirmaID;
        //        ViewBag.AyarmetotFirmaID = data.FirmaID;
        //        Session["Company_Code"] = data.Company_Code;
        //        Session["vBankaID"] = data.vBankaID;
        //        Session["vKKTID"] = data.vKKTID;
        //        Session["Grubu"] = data.PersonelGrubu;
        //        AyarMetot.pKasaID = Convert.ToInt32(data.vKasaID);
        //        try
        //        {
        //            AyarMetot.pKKTID = Convert.ToInt32(data.vKKTID);
        //        }
        //        catch { }
        //        AyarMetot.pBankaID = Convert.ToInt32(data.vBankaID);

        //        Session["PaketTipi"] = data.PaketTipi;


        //        if (data.UserName == "tani")
        //        {
        //            Session["PaketTipi"] = "Servis";
        //        }

        //        Session["sCariID"] = data.sCariID;
        //        Session["FirmaKodu"] = data.FirmaKodu;
        //        Session["sCariID"] = data.sCariID;


        //        ViewBag.ParaBirimi = data.PB;
        //        sayazilimEntities db = new sayazilimEntities();
        //        try
        //        {
        //            Personel list = db.Personel.Where(x => x.ID == data.PersonelID).FirstOrDefault<Personel>();
        //            ViewBag.TemsilciID = list.Adi;
        //        }
        //        catch (Exception e)
        //        {

        //        }



        //        if (Session["vDepoID"] != null)
        //        {
        //            if (Session["vDepoID"].ToString() == "")
        //            {
        //                Session["vDepoID"] = 1;

        //            }
        //        }
        //        else
        //        {
        //            Session["vDepoID"] = 1;
        //        }

        //        using (SqlConnection bag = new SqlConnection(AyarMetot.strcon))
        //        {
        //            if (bag.State == ConnectionState.Closed) bag.Open();
        //            using (SqlCommand sayarlar = new SqlCommand("Select * From Ayarlar where FirmaID=" + data.FirmaID, bag))
        //            {
        //                using (SqlDataReader dr = sayarlar.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        if (dr["vrsPBName"] != DBNull.Value) AyarMetot.PB_Long = dr["vrsPBName"].ToString();
        //                        if (dr["vrsPBCode"] != DBNull.Value) AyarMetot.PB_Short = dr["vrsPBCode"].ToString();
        //                        if (dr["NegatifUrunKontrolu"] != DBNull.Value) AyarMetot.NegatifUrunKontrolu = dr["NegatifUrunKontrolu"].ToString();
        //                        try
        //                        {
        //                            if (dr["HizliBitir"] != DBNull.Value) AyarMetot.HizliBitir = dr["HizliBitir"].ToString();
        //                        }
        //                        catch { }

        //                        if (dr["SslDurumu"] != DBNull.Value) AyarMetot.MailSsl = Convert.ToBoolean(dr["SslDurumu"]);
        //                        if (dr["EmanetTakibi"] != DBNull.Value) AyarMetot.EmanetTakibi = Convert.ToBoolean(dr["EmanetTakibi"]);
        //                        if (dr["Fabrika"] != DBNull.Value) AyarMetot.EmanetTakibi = Convert.ToBoolean(dr["Fabrika"]);
        //                        if (dr["KodileArama"] != DBNull.Value) AyarMetot.KodileArama = Convert.ToBoolean(dr["KodileArama"]);
        //                        if (dr["fMail"] != DBNull.Value) AyarMetot.FMail = dr["fMail"].ToString();

        //                        if (dr["fMailSifre"] != DBNull.Value)
        //                        {
        //                            if (dr["fMailSifre"].ToString() != "")
        //                            {
        //                                AyarMetot.fMailSifre = KODLA.Ac(dr["fMailSifre"].ToString(), AyarMetot.ilhan_Control);
        //                            }
        //                        }
        //                        if (dr["fMailSender"] != DBNull.Value) AyarMetot.fMailSender = dr["fMailSender"].ToString();
        //                        if (dr["fPort"] != DBNull.Value) AyarMetot.FPort = Convert.ToInt32(dr["fPort"]);
        //                        if (dr["SMSUser"] != DBNull.Value) AyarMetot.SMSUser = dr["SMSUser"].ToString();
        //                        if (dr["SMSPass"] != DBNull.Value)
        //                        {
        //                            if (dr["SMSPass"].ToString() != "")
        //                                try
        //                                {
        //                                    AyarMetot.SMSPass = KODLA.Ac(dr["SMSPass"].ToString(),
        //                                        AyarMetot.ilhan_Control);
        //                                }
        //                                catch
        //                                {
        //                                    AyarMetot.SMSPass = "";
        //                                }

        //                        }
        //                        if (dr["SMSSender"] != DBNull.Value) AyarMetot.SMSSender = dr["SMSSender"].ToString();
        //                        if (dr["PostaSunucu"] != DBNull.Value) AyarMetot.PostaSunucu = dr["PostaSunucu"].ToString();

        //                        if (dr["EFatPK"] != DBNull.Value) AyarMetot.CompanyaliciKutusu = dr["EFatPK"].ToString();
        //                        if (dr["EFatGB"] != DBNull.Value) AyarMetot.CompanyGIBKutusu = dr["EFatGB"].ToString();
        //                        if (dr["EFatKA"] != DBNull.Value) AyarMetot.CompanyKullanici = dr["EFatKA"].ToString();
        //                        if (dr["EFatSifre"] != DBNull.Value) AyarMetot.CompanySifre = dr["EFatSifre"].ToString();

        //                        if (dr["StokYuvarlamaSayisi"] != DBNull.Value) AyarMetot.Amount_Yuvarlama = Convert.ToInt32(dr["StokYuvarlamaSayisi"]);
        //                        if (dr["StokYuvarlamaSayisi"] != DBNull.Value) AyarMetot.Stok_Yuvarlama = Convert.ToInt32(dr["StokYuvarlamaSayisi"]);
        //                        if (dr["FiyatYuvarlamaSayisi"] != DBNull.Value) AyarMetot.Price_Yuvarlama = Convert.ToInt32(dr["FiyatYuvarlamaSayisi"]);
        //                        if (dr["GenelYuvarlamaSayisi"] != DBNull.Value)
        //                        {
        //                            AyarMetot.YuvarlamaSayisi = Convert.ToInt32(dr["GenelYuvarlamaSayisi"]);
        //                            AyarMetot.General_YuvarlamaString = "N" + dr["GenelYuvarlamaSayisi"].ToString();
        //                        }
        //                        if (dr["SmsFirma"] != DBNull.Value) AyarMetot.SmsFirma = dr["SmsFirma"].ToString();
        //                        if (dr["OrtakBelge"] != DBNull.Value) AyarMetot.OrtakBelge = dr["OrtakBelge"].ToString();
        //                        if (dr["TerminalTuru"] != DBNull.Value) AyarMetot.ProTerminal = dr["TerminalTuru"].ToString();
        //                        if (dr["FaturaEkranindaCari"] != DBNull.Value) AyarMetot.FaturaEkranindaCari = Convert.ToBoolean(dr["FaturaEkranindaCari"]);
        //                        if (dr["OzelKodOrderNo"] != DBNull.Value) AyarMetot.OzelKodOrderNo = Convert.ToBoolean(dr["OzelKodOrderNo"]);
        //                        if (dr["FaturadaFireGirisi"] != DBNull.Value) AyarMetot.FaturadaFireGirisi = Convert.ToBoolean(dr["FaturadaFireGirisi"]);
        //                        if (dr["BoyutSecimi"] != DBNull.Value) AyarMetot.BoyutSecimi = dr["BoyutSecimi"].ToString();
        //                        try
        //                        {
        //                            data.PaketTipi = KODLA.Ac(dr["Version"].ToString(), AyarMetot.ilhan_Control);
        //                            Session["PaketTipi"] = KODLA.Ac(dr["Version"].ToString(), AyarMetot.ilhan_Control);
        //                        }
        //                        catch
        //                        {
        //                        }

        //                        var pakettipi = Session["PaketTipi"].ToString();
        //                        break;
        //                    }
        //                }
        //            }
        //        }

        //        Session["TerminalTuru"] = AyarMetot.ProTerminal;
        //        data.TerminalTuru = AyarMetot.ProTerminal;

        //        if (data.PersonelGrubu == "Kurye")
        //        {
        //            return RedirectToAction("Orders", "Orders");
        //        }
        //        else if (data.PersonelType == 4)
        //        {
        //            return RedirectToAction("Index", "ServisList");
        //        }
        //        else if (data.PersonelType == 5)
        //        {
        //            return RedirectToAction("OnMuhasebeAnaSayfa", "AnaSayfa");
        //        }
        //        else if (data.PaketTipi == "Ön Muhasebe 1")
        //        {
        //            return RedirectToAction("OnMuhasebeAnaSayfa", "AnaSayfa");
        //        }
        //        else if (data.PaketTipi == "Ön Muhasebe 2")
        //        {
        //            return RedirectToAction("OnMuhasebeAnaSayfa", "AnaSayfa");
        //        }
        //        else if (data.PaketTipi == "Servis1")
        //        {
        //            return RedirectToAction("AnaSayfa", "AnaSayfa");
        //        }
        //        else if (data.PaketTipi == "Servis3")
        //        {
        //            return RedirectToAction("AnaSayfa", "AnaSayfa");
        //        }
        //        else if (data.PaketTipi == "Ön Muhasebe 2")
        //        {
        //            return RedirectToAction("OnMuhasebeAnaSayfa", "AnaSayfa");
        //        }
        //        else if (data.PaketTipi == "Perakende 1")
        //        {
        //            return RedirectToAction("OnMuhasebeAnaSayfa", "AnaSayfa");
        //        }
        //        else
        //        {
        //            return RedirectToAction("OnMuhasebeAnaSayfa", "AnaSayfa");
        //        }


        //    }



        //}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remember(string Mail)
        {
            string KA = "";
            string SF = "";

            string con = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("Select * From  CARi where ePOSTA='" + Mail + "' ", connection))
                {
                    using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            KA = dr["WEBKA"].ToString();
                            SF = GirisKontrol.unhash(dr["WEBSifre"].ToString().Trim(), true);
                        }
                    }

                }
            }



            SendEmail(Mail, KA, SF);

            ViewBag.Mesaj = "Hatırlatma mailiniz başarıyla gönderilmiştir";

            Session["PersonelID"] = null;
            Session["UserName"] = null;
            Session["User"] = null;
            Session["PersonelType"] = null;
            Session["RaporGor"] = null;
            Session["FirmaKodu"] = null;
            Session["PersonelAdi"] = null;
            Session["Grubu"] = null;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["PersonelID"] = null;
            Session["UserName"] = null;
            Session["User"] = null;
            Session["PersonelType"] = null;
            Session["RaporGor"] = null;
            Session["FirmaKodu"] = null;
            Session["FirmaAdi"] = "Flash";
            Session["PersonelAdi"] = null;
            Session["Grubu"] = null;
            return RedirectToAction("Index", "ServisList");
        }

        private bool SendEmail(string tomail, string KA, string SF)
        {
            SmtpClient smtpclient = new SmtpClient();
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            smtpclient.Port = 587;

            //const string username = "info@dstspeed.com.tr";
            //const string password = "Fl@sh18";
            //MailAddress fromaddress = new MailAddress("info@franchisecompany.com.tr");
            //smtpclient.Host = "smtp.yandex.com";
            //mail.To.Add("info@franchisecompany.com.tr");


            const string username = "sahinhurcan@sayazilim.com";
            const string password = "e88*Toprak";
            MailAddress fromaddress = new MailAddress("sahinhurcan@sayazilim.com");
            smtpclient.Host = "mail.yandex.net";
            mail.To.Add(tomail);


            mail.From = fromaddress;

            mail.Subject = "ŞİFRE HATIRLATMA";

            mail.IsBodyHtml = true;


            string mailicerik = "Sisteme giriş yaparken kullandığınız kullanıcı adı ve şifre aşağıdadır.<br>" +
                Environment.NewLine +
                Environment.NewLine + "<br>Kullanıcı Adı:  " + KA +
                Environment.NewLine + "<br>Şifre:  " + SF;




            ContentType mimeType = new System.Net.Mime.ContentType("text/html");


            AlternateView alternate = AlternateView.CreateAlternateViewFromString(mailicerik, mimeType);
            mail.AlternateViews.Add(alternate);
            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpclient.Credentials = new System.Net.NetworkCredential(username, password);
            smtpclient.EnableSsl = true;
            try
            {

                smtpclient.Send(mail);
                return true;

            }
            catch (Exception)
            {
                return false;

            }

        }
        public class LoginViewModel
        {

            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Kullanıcı Adı Gereklidir!")]
            [Display(Name = "Kullanıcı Adı")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Şifre Gereklidir!")]
            [DataType(DataType.Password)]
            [Display(Name = "Şİfre")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class RegisterViewModal
        {
            [Required(ErrorMessage = "E-Mail Gereklidir!")]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Kullanıcı Adı Gereklidir!")]
            [Display(Name = "Kullanıcı Adı")]
            public string FirmaIsmi { get; set; }

            [Required(ErrorMessage = "Kullanıcı Adı Gereklidir!")]
            [Display(Name = "Kullanıcı Adı")]
            public string KullaniciAdi { get; set; }

            [Required(ErrorMessage = "Şifre Gereklidir!")]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Telefon Gereklidir!")]

            [Display(Name = "Telefon")]
            public string FirmaTel { get; set; }

            public string Aciklama { get; set; }

            public string Pakettipi { get; set; }

        }



        private string paketCoz(string paket)
        {


            if (paket == "Servis")
            {
                return "Teknik Servis 1";

            }
            else if (paket == "Servis2")
            {
                return "Teknik Servis 2";

            }
            else if (paket == "hizlisatis")
            {
                return "Perakende 1";

            }
            else if (paket == "Perakende2")
            {
                return "Perakende 2";

            }
            else if (paket == "Muhasebe1")
            {
                return "Ön Muhasebe 1";
            }
            else if (paket == "Muhasebe2")
            {
                return "Ön Muhasebe 2";

            }
            else if (paket == "Muhasebe3")
            {
                return "Ön Muhasebe 3";
            }
            else if (paket == "Adisyon")
            {
                return "Adisyon 1";

            }
            else if (paket == "Adisyon2")
            {
                return "Adisyon 2";

            }
            else if (paket == "Full")
            {
                return "Full Paket";

            }
            else
            {
                return "Ön Muhasebe 1";

            }

        }

        private string paketKapa(string paket)
        {


            if (paket == "Teknik Servis 1")
            {
                return "Servis";

            }
            else if (paket == "Teknik Servis 2")
            {
                return "Servis2";

            }
            else if (paket == "Perakende 1")
            {
                return "hizlisatis";

            }
            else if (paket == "Perakende 2")
            {
                return "Perakende2";

            }
            else if (paket == "Ön Muhasebe 1")
            {
                return "Muhasebe1";
            }
            else if (paket == "Ön Muhasebe 2")
            {
                return "Muhasebe2";

            }
            else if (paket == "Ön Muhasebe 3")
            {
                return "Muhasebe3";
            }
            else if (paket == "Adisyon 1")
            {
                return "Adisyon";

            }
            else if (paket == "Adisyon 2")
            {
                return "Adisyon2";

            }
            else if (paket == "Full Paket")
            {
                return "Full";

            }
            else
            {
                return "Muhasebe1";

            }

        }



        //public ActionResult Register()
        //{
        //    ViewBag.Sonuc = "İlkGiris";
        //    return View();
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult Register(RegisterViewModal rg)
        //{
        //    sayazilimEntities db = new sayazilimEntities();

        //    try
        //    {
        //        var firmalarlist = db.Firmalar.Where(x => x.FirmaEMail == rg.Email).ToList();

        //        if (firmalarlist.Count == 0)
        //        {
        //            Firmalar fr = new Firmalar();

        //            fr.FirmaAdi = rg.FirmaIsmi;
        //            string sifre = rg.Password;
        //            fr.FirmaEMail = rg.Email;
        //            fr.FirmaTel = rg.FirmaTel;
        //            var firmalist = db.Firmalar.OrderByDescending(x => x.ID).FirstOrDefault();
        //            if (firmalist != null)
        //            {
        //                int i = Convert.ToInt32(firmalist.FirmaID + 1);
        //                fr.FirmaID = i;
        //            }
        //            else
        //            {
        //                fr.FirmaID = 2;
        //            }
        //            Personel pr = new Personel();
        //            pr.Adi = "Admin";
        //            pr.Soyadi = "";
        //            pr.Pozisyonu = "Admin";
        //            pr.PersonelGrubu = "Admin";
        //            pr.FirmaID = Convert.ToInt16(fr.FirmaID);
        //            pr.sCariID = -1;
        //            pr.WebKA = rg.KullaniciAdi;
        //            pr.WebSifre = GirisKontrol.hash(sifre, true);
        //            db.Personel.Add(pr);
        //            db.Firmalar.Add(fr);
        //            db.SaveChanges();






        //            #region YeniFirmaKontrol
        //            int firmaid = Convert.ToInt16(pr.FirmaID);
        //            var setting = db.Ayarlar.Where(x => x.FirmaID == firmaid).ToList();
        //            if (setting.Count == 0)
        //            {
        //                DataRow dt = null;
        //                using (SqlConnection con = new SqlConnection(AyarMetot.conString))
        //                {
        //                    if (con.State == ConnectionState.Closed) con.Open();
        //                    using (SqlDataAdapter da =
        //                        new System.Data.SqlClient.SqlDataAdapter(
        //                            "select * from AYARLAR where ID='1'", con))
        //                    {
        //                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
        //                        {
        //                            DataSet ds = new DataSet();
        //                            da.Fill(ds, "AYARLAR");
        //                            DataRow[] adf = ds.Tables["AYARLAR"].Select("ID='1'");
        //                            if (adf.Length != 0)
        //                            {
        //                                dt = adf[0];
        //                            }
        //                        }
        //                    }
        //                }
        //                using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
        //                {
        //                    if (con.State == ConnectionState.Closed) con.Open();
        //                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from AYARLAR where FirmaID=" + firmaid.ToString(), con))
        //                    {
        //                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
        //                        {
        //                            DataSet ds = new DataSet();
        //                            da.Fill(ds, "AYARLAR");
        //                            DataRow df = ds.Tables["AYARLAR"].NewRow();
        //                            df.ItemArray = dt.ItemArray.Clone() as object[];
        //                            df["FirmaID"] = fr.FirmaID;

        //                            df["Version"] = KODLA.Kapa(rg.Pakettipi, AyarMetot.ilhan_Control);
        //                            df["fMail"] = "";
        //                            df["fMailSifre"] = "";
        //                            df["fMailSender"] = "";
        //                            df["SMSUser"] = "";
        //                            df["SMSPass"] = "";
        //                            df["MesajSonu"] = "";
        //                            df["BorcMesajSonu"] = "";
        //                            df["SmsSender"] = "";
        //                            df["EFatGB"] = "";
        //                            df["EFatKA"] = "";
        //                            df["EFatSifre"] = "";
        //                            df["EIrsKA"] = "";
        //                            df["EIrsSF"] = "";
        //                            df["KurumKodu"] = "";
        //                            df["EIrsGB"] = "";
        //                            df["Entegrator"] = "";
        //                            df["PostaSunucu"] = "";
        //                            string company_code = "SA01" + fr.FirmaID.ToString().PadLeft(3, '0');
        //                            df["Company_Code"] = company_code;
        //                            ds.Tables["AYARLAR"].Rows.Add(df);
        //                            da.Update(ds, "AYARLAR");


        //                        }
        //                    }
        //                }
        //            }
        //            var compony = db.COMPANY_DETAIL.Where(x => x.FirmaID == firmaid).ToList();
        //            if (compony.Count == 0)
        //            {
        //                DataRow dt = null;
        //                using (SqlConnection con = new SqlConnection(AyarMetot.conString))
        //                {
        //                    if (con.State == ConnectionState.Closed) con.Open();
        //                    using (SqlDataAdapter da =
        //                        new System.Data.SqlClient.SqlDataAdapter(
        //                            "select * from COMPANY_DETAIL where ID='1'", con))
        //                    {
        //                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
        //                        {
        //                            DataSet ds = new DataSet();
        //                            da.Fill(ds, "COMPANY_DETAIL");
        //                            DataRow[] adf = ds.Tables["COMPANY_DETAIL"].Select("ID='1'");
        //                            if (adf.Length != 0)
        //                            {
        //                                dt = adf[0];


        //                            }
        //                        }
        //                    }
        //                }
        //                using (SqlConnection con = new SqlConnection(AyarMetot.strcon))
        //                {
        //                    if (con.State == ConnectionState.Closed) con.Open();
        //                    using (SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select Top 1 * from COMPANY_DETAIL", con))
        //                    {
        //                        using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
        //                        {
        //                            DataSet ds = new DataSet();
        //                            da.Fill(ds, "COMPANY_DETAIL");
        //                            DataRow df = ds.Tables["COMPANY_DETAIL"].NewRow();
        //                            df.ItemArray = dt.ItemArray.Clone() as object[];
        //                            Firmalar frm = db.Firmalar.Where(x => x.FirmaID == firmaid).FirstOrDefault();
        //                            df["FirmaAdi"] = frm.FirmaAdi;
        //                            string company_code = "SA01" + fr.FirmaID.ToString().PadLeft(3, '0');
        //                            df["Company_Code"] = company_code;
        //                            Personel pb = db.Personel.OrderByDescending(x => x.ID).FirstOrDefault();
        //                            df["IlgiliKisi"] = (pb.Adi + pb.Soyadi).TrimEnd();
        //                            Firmalar dr = db.Firmalar.Where(x => x.FirmaID == firmaid).FirstOrDefault();
        //                            df["Telefon"] = dr.FirmaTel;
        //                            df["Faks"] = "";
        //                            df["ePosta"] = dr.FirmaEMail;
        //                            df["WebSite"] = "";
        //                            df["VergiDairesi"] = "";
        //                            df["VergiNo"] = "";
        //                            df["Ulke"] = "";
        //                            df["Sehir"] = "";
        //                            df["PostaKodu"] = "";
        //                            df["Adres"] = "";
        //                            df["TicaretSicilNo"] = "";
        //                            df["MersisNo"] = "";
        //                            df["Ilce"] = "";
        //                            df["FirmaID"] = fr.FirmaID;

        //                            ds.Tables["COMPANY_DETAIL"].Rows.Add(df);
        //                            da.Update(ds, "COMPANY_DETAIL");


        //                        }
        //                    }
        //                }
        //            }

        //            var stk_ktg = db.STK_KATEGORI.Where(x => x.FirmaID == firmaid).ToList();
        //            if (stk_ktg.Count == 0)
        //            {
        //                int i = 7;
        //                for (int j = 1; j < i; j++)
        //                {
        //                    STK_KATEGORI stk = new STK_KATEGORI();
        //                    stk.FirmaID = fr.FirmaID;
        //                    stk.Name = "Stok Kategori " + j;
        //                    db.STK_KATEGORI.Add(stk);
        //                    db.SaveChanges();
        //                }
        //            }

        //            var special = db.SPECIAL_TECH.Where(x => x.FirmaID == firmaid).ToList();
        //            if (special.Count == 0)
        //            {
        //                int i = 7;
        //                for (int j = 1; j < i; j++)
        //                {
        //                    SPECIAL_TECH stk = new SPECIAL_TECH();
        //                    stk.FirmaID = Convert.ToInt16(fr.FirmaID);
        //                    stk.Name = "Stok Kategori " + j;
        //                    db.SPECIAL_TECH.Add(stk);
        //                    db.SaveChanges();
        //                }
        //            }


        //            var carikategori = db.CARI_KATEGORI.Where(x => x.FirmaID == firmaid).ToList();
        //            if (carikategori.Count == 0)
        //            {
        //                int i = 7;
        //                for (int j = 1; j < i; j++)
        //                {
        //                    CARI_KATEGORI stk = new CARI_KATEGORI();
        //                    stk.FirmaID = firmaid;
        //                    stk.Name = "Cari Kategori " + j;
        //                    db.CARI_KATEGORI.Add(stk);
        //                    db.SaveChanges();
        //                }
        //            }


        //            #endregion


        //            Session["FirmaAdi"] = fr.FirmaAdi;
        //            MailOkey(rg);
        //            return RedirectToAction("Login", "Login");
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return View();
        //    }

        //}


        
    }
}