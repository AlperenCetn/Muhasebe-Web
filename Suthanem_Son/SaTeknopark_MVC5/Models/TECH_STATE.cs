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
    
    public partial class TECH_STATE
    {
        public int ID { get; set; }
        public Nullable<int> CihazID { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string Durum { get; set; }
        public string Aciklama { get; set; }
        public Nullable<int> PersonelID { get; set; }
        public string Durum2 { get; set; }
        public Nullable<int> DServisCID { get; set; }
        public Nullable<bool> OtomatikTeslimSms { get; set; }
        public Nullable<int> FirmaID { get; set; }
        public string Company_Code { get; set; }
    }
}