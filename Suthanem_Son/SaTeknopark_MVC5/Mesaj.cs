// Decompiled with JetBrains decompiler
// Type: SoftwareSa.Mesaj
// Assembly: SoftwareSa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 959B7D9E-3686-4487-9B54-51F8474EA333
// Assembly location: C:\Users\ilhan\Desktop\YEDEK\Proper\bin\Release\SoftwareSa.dll

using Newtonsoft.Json;
using SaTeknopark_MVC5.NetGSM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml;
using SaTeknopark_MVC5;

namespace Sayazilim_Teknopark
{
    public static class Mesaj
    {
        public static string DurumMesaj;
        public static string Durumkodu;
        public static string KrediDurumu;
        public static string Durumcuk;
        public static int KalanKredi;

        public static void SMTECHNICAL(
          string mesaja,
          string tel,
          string SmsFirma,
          string SMSUser,
          string SMSPass,
          string SMSSender)
        {
            try
            {
                if (tel.Substring(0, 1) == "0")
                    tel = tel.Substring(1, tel.Length - 1);
            }
            catch
            {
            }
            try
            {
                if (Mesaj.SMSGonder(mesaja, tel, SmsFirma, SMSUser, SMSPass, SMSSender))
                    Mesaj.DurumMesaj = "SMS Gönderildi";
                else
                    Mesaj.DurumMesaj = "SMS Gönderilemedi!";
            }
            catch (Exception ex)
            {
                Mesaj.DurumMesaj = "SMS Gönderilemedi! " + ex.Message;
         
            }
        }
        public class Credential
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public int ResellerID { get; set; } = 0;
        }

        public class ToMsisdn
        {

            public long Msisdn { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string CustomField1 { get; set; }
            public string TelefonNo
            {
                set
                {
                    Msisdn = Convert.ToInt64($"90{value}");
                }
            }
        }



        public class Sms
        {


            public List<ToMsisdn> ToMsisdns { get; set; }
            public List<int> ToGroups { get; set; }
            public bool IsCreateFromTeplate { get; set; }
            public string SmsTitle { get; set; }
            public string SmsContent { get; set; }
            public string RequestGuid { get; set; }
            public bool CanSendSmsToDuplicateMsisdn { get; set; }
            public string SmsSendingType { get; set; }
            public string SmsCoding { get; set; }
            public string SenderName { get; set; }
            public int Route { get; set; }
            public int ValidityPeriod { get; set; }
            public string DataCoding { get; set; }


        }





        public class Anamodel
        {

            public Credential Credential { get; set; }
            public Sms Sms { get; set; }


        }


        public class Status
        {
            public int Code { get; set; }
            public string Description { get; set; }
            public string Message { get; set; }
        }

        public class SmsRootObject
        {
            public int MessageID { get; set; }
            public Status Status { get; set; }
        }
        public static string GetResponse(string sURL, string sXml)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(new Uri(sURL)) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = 5000;
                byte[] data = UTF8Encoding.UTF8.GetBytes(sXml);
                request.ContentLength = data.Length;
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(data, 0, data.Length);
                }
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    System.IO.File.WriteAllText(Path.Combine(@"C: \Users\Alperen\AppData\Local\Sayazilim", "alperen.xml"), sXml);
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //public static string createXml(string USERNAME,string SECRET,
        //    string BASLIK,string gsm,string Mesaj)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.Encoding = Encoding.Unicode;
        //    settings.Indent = true;
        //    settings.IndentChars = (" ");



        //    using (XmlWriter writer = XmlWriter.Create(sb, settings))
        //    {
        //        writer.WriteStartElement("sms");
        //        writer.WriteElementString("username", USERNAME);
        //        writer.WriteElementString("password", SECRET);
        //        writer.WriteElementString("header", BASLIK);
        //        writer.WriteElementString("validity", "2880");
        //        writer.WriteStartElement("message");
        //        writer.WriteStartElement("gsm");
        //        writer.WriteElementString("no", "90"+gsm);
        //        writer.WriteEndElement(); //gsm
        //        writer.WriteStartElement("msg");
        //        writer.WriteCData(Mesaj);
        //        writer.WriteEndElement(); //msg
        //        writer.WriteEndElement(); //message
        //        writer.WriteEndElement(); // sms
        //        writer.Flush();
        //    }
        //    return sb.ToString();
        //}


        public static string createXml(string Sender, string MSG, string tel)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            settings.Indent = true;
            settings.IndentChars = (" ");



            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                writer.WriteStartElement("sms");
                writer.WriteElementString("username", AyarMetot.SMSUser);
                writer.WriteElementString("password", AyarMetot.SMSPass);
                writer.WriteElementString("header", Sender);
                writer.WriteElementString("validity", "2880");
                writer.WriteStartElement("message");
                writer.WriteStartElement("gsm");
                writer.WriteElementString("no", "90" + tel);
                writer.WriteEndElement(); //gsm
                writer.WriteStartElement("msg");
                writer.WriteCData(MSG);
                writer.WriteEndElement(); //msg
                writer.WriteEndElement(); //message
                writer.WriteEndElement(); // sms
                writer.Flush();
            }
            return sb.ToString();
        }



        public static bool SMSGonder(string mesaj, string tel,
            string SmsFirma, string SMSUser,
          string SMSPass,
          string SMSSender)
        {
            string amesaj = AyarMetot.AyarBilgiCek("MesajSonu");
            if (amesaj != "")
            {
                amesaj = Environment.NewLine + amesaj;

            }


            if (SmsFirma == "Varsayılan")
            {
                try
                {
                    try
                    {
                        if (tel.Substring(0, 1) == "0")
                            tel = tel.Substring(1, tel.Length - 1);
                    }
                    catch
                    {
                    }

                    string sURL = "http://sms.sayazilim.com/xmlapi/sendsms";


                    string xmlString = "<?xml version=\"1.0\"?>";
                    xmlString += "<SMS>";
                    xmlString += "<authentication>";
                    xmlString += "<username>" + SMSUser + "</username>";
                    xmlString += "<password>" + SMSPass + "</password>";
                    xmlString += "</authentication>";
                    xmlString += "<message>";
                    xmlString += "<originator>" + SMSSender + "</originator>";
                    xmlString += "<text>" + mesaj + "</text>";
                    xmlString += "<unicode />";
                    xmlString += "<international />";
                    xmlString += "</message>";
                    xmlString += "<receivers>";
                    xmlString += "<receiver>" + tel.Replace(" ", String.Empty) + "</receiver>";
                    xmlString += "</receivers>";
                    xmlString += "</SMS>";
                    GetResponse(sURL, xmlString);

                    //new sendSMSPortTypeClient().sendSMS(SMSUser, SMSPass, SMSSender, mesaj+amesaj, tel);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else if (SmsFirma == "KayseriTopluSms")
            {
                try
                {
                    tel = tel.Replace(" ", String.Empty);
                    try
                    {
                        if (tel.Substring(0, 1) == "0")
                            tel = tel.Substring(1, tel.Length - 1);
                    }
                    catch
                    {
                    }


                    Anamodel urun = new Anamodel();
                    Credential crt = new Credential();
                    Sms sms = new Sms();

                    crt.Username = SMSUser;
                    crt.Password = SMSPass;
                    crt.ResellerID = 10035;
                    sms.SenderName = SMSSender;
                    sms.SmsTitle = SMSSender;
                    sms.RequestGuid = "";
                    sms.SmsSendingType = "ByNumber";
                    sms.SmsCoding = "String";
                    sms.SmsContent = mesaj + amesaj;
                    ToMsisdn ToMsisdn = new ToMsisdn();
                    ToMsisdn.TelefonNo = tel;
                    sms.DataCoding = "Default";
                    List<ToMsisdn> ts = new List<ToMsisdn>();
                    ts.Add(ToMsisdn);
                    sms.ToMsisdns = ts;
                    urun.Credential = crt;
                    urun.Sms = sms;
                    using (var client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(urun), Encoding.UTF8, "application/json");
                        var response = client.PostAsync("http://web.kayseritoplusms.com/api/json/syncreply/SendInstantSms", content).Result;
                        var Samochod = response.Content.ReadAsStringAsync();
                        SmsRootObject pro = JsonConvert.DeserializeObject<SmsRootObject>(Samochod.Result);
                        string messge = "";
                        if (pro.MessageID == 0)
                        {
                            messge = pro.Status.Message;
                            return false;
                        }
                        else
                        {
                            if (pro.Status.Code == 200)
                            {
                                messge = "Mesaj Başarıyla gönderim kuyruğuna alındı";
                                return true;
                            }

                        }



                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else if (SmsFirma == "TESCOM")
            {

                tel = tel.Replace(" ", String.Empty);
                try
                {
                    if (tel.Substring(0, 1) == "0")
                        tel = tel.Substring(1, tel.Length - 1);
                }
                catch
                {
                }


                try
                {

                    string sURL = "http://api.tescom.com.tr:8080/api/smspost/v1";

                    GetResponse(sURL, createXml(SMSSender, mesaj, tel));
                    return true;
                }
                catch (Exception ex)
                {
                   
                    return false;
                }
            }
            else
            {
                try
                {

                    string[] gsm = new string[1] { tel };
                    new smsnnClient().smsGonder1NV2(SMSUser, SMSPass, SMSSender,
                        mesaj + amesaj, gsm, "TR", "", "", "", 0);
                    return true;
                }
                catch (Exception ex)
                {
                    
                    return false;
                }
            }
        }
    }


}
