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
    
    public partial class STORE_PROCESS_DETAIL
    {
        public int ID { get; set; }
        public Nullable<int> DepoIslemID { get; set; }
        public Nullable<System.DateTime> IslemTarihi { get; set; }
        public Nullable<int> gDepoID { get; set; }
        public Nullable<int> aDepoID { get; set; }
        public Nullable<int> personelID { get; set; }
        public Nullable<int> urunID { get; set; }
        public Nullable<decimal> urunFiyat { get; set; }
        public Nullable<decimal> urunMiktar { get; set; }
        public string urunBirim { get; set; }
        public string paraBirimi { get; set; }
        public string variyantID { get; set; }
        public Nullable<decimal> Kur { get; set; }
        public string Donem { get; set; }
        public string IslemTipi { get; set; }
        public Nullable<int> OzelKodID { get; set; }
        public Nullable<decimal> KDV { get; set; }
        public Nullable<int> DepoSayimID { get; set; }
        public Nullable<int> EmirID { get; set; }
        public string SeriNo { get; set; }
        public string Ekalan { get; set; }
        public string Aciklama { get; set; }
        public string GirisTuru { get; set; }
        public Nullable<int> TCariID { get; set; }
        public Nullable<int> TakimID { get; set; }
        public string UrunSeriNoMob { get; set; }
        public Nullable<int> KodID { get; set; }
        public Nullable<decimal> SayimMiktari { get; set; }
        public Nullable<int> FaturaSatirID { get; set; }
        public Nullable<int> SiparisIDHFT { get; set; }
        public Nullable<int> FirmaID { get; set; }
        public string Company_Code { get; set; }
        public string AracPlaka { get; set; }
        public string AracPlaka2 { get; set; }
    }
}