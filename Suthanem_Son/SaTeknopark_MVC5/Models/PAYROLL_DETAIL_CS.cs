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
    
    public partial class PAYROLL_DETAIL_CS
    {
        public int ID { get; set; }
        public Nullable<int> BordroID { get; set; }
        public Nullable<int> CekSenetID { get; set; }
        public string Durum { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<int> CariID { get; set; }
        public Nullable<int> BankaID { get; set; }
        public Nullable<int> KasaID { get; set; }
        public Nullable<int> BorcluAlacakli { get; set; }
        public string Donem { get; set; }
        public Nullable<bool> ManuelGuncelleme { get; set; }
        public Nullable<int> FirmaID { get; set; }
        public string Company_Code { get; set; }
    }
}
