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
    
    public partial class TREE_PRODUCT
    {
        public int ID { get; set; }
        public Nullable<int> AgacID { get; set; }
        public Nullable<int> UrunID { get; set; }
        public Nullable<decimal> AlishFiyat { get; set; }
        public Nullable<decimal> SatishFiyat { get; set; }
        public Nullable<decimal> Miktar { get; set; }
        public string Aciklama { get; set; }
        public string SeriNo { get; set; }
        public Nullable<System.DateTime> KayitTarih { get; set; }
        public Nullable<short> FirmaID { get; set; }
        public Nullable<decimal> BirimAdet { get; set; }
        public string Birim { get; set; }
        public string EkAlan { get; set; }
        public string Company_Code { get; set; }
    }
}
