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
    
    public partial class Banka
    {
        public int ID { get; set; }
        public string BankaKodu { get; set; }
        public string BankaAdi { get; set; }
        public string Varsayilan { get; set; }
        public string SubeAdi { get; set; }
        public string HesapNo { get; set; }
        public string hnParaBirimi { get; set; }
        public string Iban { get; set; }
        public Nullable<decimal> Bakiye { get; set; }
        public string IlgiliKisi { get; set; }
        public string IletisimNo { get; set; }
        public string FaxNo { get; set; }
        public Nullable<int> MKodu { get; set; }
        public string EkBilgiler { get; set; }
        public string KayitT { get; set; }
        public string DegT { get; set; }
        public Nullable<short> FirmaID { get; set; }
        public Nullable<decimal> ABorc2018 { get; set; }
        public Nullable<bool> GizliBanka { get; set; }
        public string Company_Code { get; set; }
        public string Aciklama { get; set; }
        public string Telefon { get; set; }
        public Nullable<bool> Merkez { get; set; }
    }
}