//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SaTeknopark_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class INVOICE_E
    {
        public int ID { get; set; }
        public Nullable<int> FaturaID { get; set; }
        public Nullable<int> EFaturaNo { get; set; }
        public string Senaryo { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public Nullable<System.DateTime> GetDateYeni { get; set; }
        public Nullable<System.DateTime> LastDate { get; set; }
        public string ReadStates { get; set; }
        public string VKN { get; set; }
        public string Yonu { get; set; }
        public string GonderimTipi { get; set; }
        public Nullable<System.DateTime> SaveDate { get; set; }
        public string SendMessage { get; set; }
        public string UUID { get; set; }
        public string UUIDZarf { get; set; }
        public string TypeCode { get; set; }
        public string Status { get; set; }
        public string Durum { get; set; }
        public string PostaKutusu { get; set; }
        public string ZarfBilgisi { get; set; }
        public string CevapNedeni { get; set; }
        public string CevapUUID { get; set; }
        public string BelgeDurumu { get; set; }
        public string RefNumber { get; set; }
        public string MuhasebeFaturaNo { get; set; }
        public Nullable<int> FirmaID { get; set; }
        public string Company_Code { get; set; }
    }
}
