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
    
    public partial class VARIANT
    {
        public int ID { get; set; }
        public Nullable<int> UrunID { get; set; }
        public string Renk { get; set; }
        public Nullable<int> RenkKod { get; set; }
        public string Beden { get; set; }
        public string Sezon { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
        public Nullable<decimal> Miktar { get; set; }
        public string Aciklama { get; set; }
        public string Barkod { get; set; }
        public Nullable<short> FirmaID { get; set; }
        public string RenkAdi { get; set; }
        public string BedenTuru { get; set; }
        public string Company_Code { get; set; }
    }
}