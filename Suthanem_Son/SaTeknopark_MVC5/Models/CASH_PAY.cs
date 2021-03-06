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
    
    public partial class CASH_PAY
    {
        public int ID { get; set; }
        public string IslemTipi { get; set; }
        public string IslemNo { get; set; }
        public string IslemTarih { get; set; }
        public Nullable<int> CariID { get; set; }
        public Nullable<int> PersonelID { get; set; }
        public Nullable<int> KasaID { get; set; }
        public Nullable<int> BankaID { get; set; }
        public Nullable<int> KrediKartiID { get; set; }
        public Nullable<int> TaksitSayisi { get; set; }
        public Nullable<int> OzelKodID { get; set; }
        public string Aciklama { get; set; }
        public string ParaBirimi { get; set; }
        public Nullable<decimal> Tutar { get; set; }
        public Nullable<decimal> gTutar { get; set; }
        public string gParaBirimi { get; set; }
        public Nullable<decimal> aTutar { get; set; }
        public string aParaBirimi { get; set; }
        public Nullable<decimal> exRate { get; set; }
        public Nullable<int> gonderenID { get; set; }
        public string gonderenType { get; set; }
        public Nullable<int> alanID { get; set; }
        public string alanType { get; set; }
        public string KayT { get; set; }
        public string DegT { get; set; }
        public string Donem { get; set; }
        public Nullable<int> AlanCariID { get; set; }
        public Nullable<int> AdisYonTahsilatID { get; set; }
        public Nullable<int> OzelKodKdv { get; set; }
        public Nullable<int> KayitPersonelID { get; set; }
        public Nullable<System.DateTime> KayitTarih { get; set; }
        public Nullable<int> HavaleMasrafID { get; set; }
        public Nullable<int> CekSenetID { get; set; }
        public Nullable<int> wsID { get; set; }
        public Nullable<int> CariBankaKID { get; set; }
        public Nullable<int> ProjeID { get; set; }
        public Nullable<decimal> Kesinti { get; set; }
        public Nullable<int> VirmanFaturaID { get; set; }
        public Nullable<short> FirmaID { get; set; }
        public Nullable<int> DugunReservID { get; set; }
        public Nullable<decimal> PrimOr { get; set; }
        public Nullable<int> SantiyeID { get; set; }
        public string FaturaIDler { get; set; }
        public Nullable<decimal> TLEURKUR { get; set; }
        public string Plaka2 { get; set; }
        public Nullable<int> AlanVOKID { get; set; }
        public string KontratNo { get; set; }
        public string ITLPozisyonNo { get; set; }
        public string IHLPozisyonNo { get; set; }
        public Nullable<decimal> TLjpyKur { get; set; }
        public Nullable<int> FaturaID { get; set; }
        public Nullable<bool> AracGiderIslemesin { get; set; }
        public string AracPlaka { get; set; }
        public Nullable<decimal> TLUSDKUR { get; set; }
        public Nullable<decimal> TLGBPKUR { get; set; }
        public string Company_Code { get; set; }
        public Nullable<int> DoktorID { get; set; }
    }
}
