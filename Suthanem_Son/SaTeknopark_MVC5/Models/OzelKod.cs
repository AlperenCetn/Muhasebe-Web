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
    
    public partial class OzelKod
    {
        public int ID { get; set; }
        public string Kod { get; set; }
        public string KodAdi { get; set; }
        public Nullable<int> ustKodID { get; set; }
        public string Aciklama { get; set; }
        public Nullable<decimal> Bakiye { get; set; }
        public Nullable<int> kodLevel { get; set; }
        public string paraBirimi { get; set; }
        public string KartTipi { get; set; }
        public Nullable<int> Kdv { get; set; }
        public string KdvRapordaGor { get; set; }
        public string GrupAdi { get; set; }
        public Nullable<bool> OzelKodList { get; set; }
        public Nullable<short> FirmaID { get; set; }
        public Nullable<decimal> Komisyon { get; set; }
        public Nullable<bool> DevirOK { get; set; }
        public string Company_Code { get; set; }
    }
}
