using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaTeknopark_MVC5.Models
{

    public partial class ProductHareket
    {
        public string IslemTuru { get; set; }
        public string FaturaNo { get; set; }
        public string SatirParaBirimi { get; set; }
        public DateTime IslemTarihi { get; set; }
        public string Miktar { get; set; }
        public string Tutar { get; set; }
        public string gBirimMiktar { get; set; }
        public string YerelBrut { get; set; }
        public int UrunID { get; set; }
    }

    public partial class MalzemelerList
    {
        public string UrunAdi { get; set; }
        public string ServisNo { get; set; }
        public string CariUnvan { get; set; }
        public string Fiyat { get; set; }
        public string Miktar { get; set; }
        public string Tutar { get; set; }
        public string Tarih { get; set; }
        public string ParaBirimi { get; set; }
    }

    public partial class VariantList
    {
        public string Counter { get; set; }
        public string UrunAdi { get; set; }
        public string RenkAdi { get; set; }
        public string Beden { get; set; }
        public string Sezon { get; set; }
        public string Fiyat { get; set; }
        public string Miktar { get; set; }
        public string Aciklama { get; set; }
        public string Barkod { get; set; }
    }

    public partial class OzelKodHareketler
    {
        public string Counter { get; set; }
        public string IslemBilgisi { get; set; }
        public string IslemNumarasi { get; set; }
        public string Tarih { get; set; }
        public string VadeTarihi { get; set; }
        public string Firma { get; set; }
        public string Tutar { get; set; }
        public string PB { get; set; }
        public string Personel { get; set; }
    }

    public partial class BaBsFiltre
    {
        public string Yil { get; set; }
        public string Ay { get; set; }
        public string PB { get; set; }
    }

    public partial class BaBsRapor
    {
        public string FirmaKodu { get; set; }
        public string Unvan { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string BA_Tutar { get; set; }
        public string BA_BelgeAdedi { get; set; }
        public string BS_Tutar { get; set; }
        public string BS_BelgeAdedi { get; set; }
    }

    public partial class CekSenetRapor
    {
        public string BaslangicTarih { get; set; }
        public string BitisTarih { get; set; }
        public string EvrakTipi { get; set; }
        public string Durumu { get; set; }
        public string PersonelID { get; set; }
        public string CariID { get; set; }
        public string PB { get; set; }
        public string OzelKodID { get; set; }
    }

    public partial class GunSonuRpr
    {
        public string Toplam { get; set; }
        public string KKTutar { get; set; }
        public string Indirim { get; set; }
        public string NakitTutar { get; set; }
        public string HvlTutar { get; set; }
        public string ParaKartTutar { get; set; }
        public string CariSatis { get; set; }
        public string TaksitTutar { get; set; }
        public string SgkTutar { get; set; }
        public string Miktar { get; set; }
        public string UrunAdi { get; set; }

    }

    public partial class AylikOdemeTahsilatRpr
    {
        public string Tipi { get; set; }
        public string IslemNo { get; set; }
        public string IslemTipi { get; set; }
        public string HesapTur { get; set; }
        public string Alacak { get; set; }
        public string Odeme { get; set; }
        public string PB { get; set; }
        public string Aciklama { get; set; }
        public string VadeTarihi { get; set; }

    }
    
    public partial class AlacakOdemeRapor
    {
        public string AlacakOdeme { get; set; }
        public string Personel { get; set; }
        public string Tarih { get; set; }
        public string IslemNo { get; set; }
        public string Tutar { get; set; }
        public string Birim { get; set; }
        public string Aciklama { get; set; }
        public string PozisyonNo { get; set; }
    }

    public partial class BorcAlacakRapor
    {
        public int ID { get; set; }
        public string AdiSoyadi { get; set; }
        public string Borc { get; set; }
        public string Alacak { get; set; }
        public string Bakiye { get; set; }
    }

    public partial class SatisRapor
    {
        public string MusteriKodu { get; set; }
        public string MusteriAdi { get; set; }
        public string FaturaAdedi { get; set; }
        public string FaturaToplami { get; set; }
        public string NakitToplami { get; set; }
        public string KKToplami { get; set; }
        public string TahsilatAdedi { get; set; }
        public string TahsilatToplami { get; set; }
        public string CariID { get; set; }
    }

    public partial class KasiyerSatis
    {
        public string durum { get; set; }
        public string Personel { get; set; }
        public string SatisAdedi { get; set; }
        public string SatisTutari { get; set; }
        public string Prim { get; set; }
        public string Maliyet { get; set; }
        public string Karlilik { get; set; }
    }
    
    public partial class Perakendesatis
    {
        public string Durum { get; set; }
        public string IslemTipi { get; set; }
        public string NakitTutar { get; set; }
        public string KKTutar { get; set; }
        public string BankaID { get; set; }
        public string Indirim { get; set; }
        public int CariID { get; set; }
        public int ntoID { get; set; }
        public int kktID { get; set; }
        public bool iade { get; set; }
        public bool veresiye { get; set; }
        public string Aciklama { get; set; }
    }

    public partial class ServisDetayEk
    {
        public string UrunAdi { get; set; }
    }

    public partial class Siparis
    {
        public int ID { get; set; }
        public string SiparisNo { get; set; }
        public string CariUnvan { get; set; }
        public string Personel { get; set; }
        public Nullable<int> TeklifID { get; set; }
        public Nullable<System.DateTime> IslemTarihi { get; set; }
        public Nullable<int> CariID { get; set; }
        public Nullable<decimal> BrutToplam { get; set; }
        public Nullable<decimal> NetToplam { get; set; }
        public Nullable<decimal> ToplamKdv { get; set; }
        public Nullable<decimal> ToplamIskonto { get; set; }
        public Nullable<decimal> GenelToplam { get; set; }
        public Nullable<decimal> q18Kdv { get; set; }
        public Nullable<decimal> q8Kdv { get; set; }
        public Nullable<decimal> q1Kdv { get; set; }
        public Nullable<decimal> Iskonto { get; set; }
        public Nullable<int> PersonelID { get; set; }
        public string kdvDH { get; set; }
        public string Aciklama { get; set; }
        public string Donem { get; set; }
        public string IslemTipi { get; set; }
        public string Durumu { get; set; }
        public string ParaBirimi { get; set; }
        public Nullable<decimal> SatirIskonto { get; set; }
        public Nullable<decimal> OzelIskonto { get; set; }
        public Nullable<int> OzelKodID { get; set; }
        public Nullable<decimal> YerelTutar { get; set; }
        public Nullable<decimal> DolarKur { get; set; }
        public Nullable<decimal> EuroKur { get; set; }
        public Nullable<decimal> KalanTutar { get; set; }
        public Nullable<decimal> TeslimEdilenTutar { get; set; }
        public Nullable<int> KayitPersonelID { get; set; }
        public Nullable<System.DateTime> KayitTarih { get; set; }
        public Nullable<decimal> Kur { get; set; }
        public Nullable<int> ProjeID { get; set; }
        public string OzelAlan1 { get; set; }
        public string OzelAlan2 { get; set; }
        public string OzelAlan3 { get; set; }
        public string OzelAlan4 { get; set; }
        public string OzelAlan5 { get; set; }
        public Nullable<bool> IptalEdildi { get; set; }
        public Nullable<System.DateTime> VadeTarihi { get; set; }
        public Nullable<decimal> tOIV { get; set; }
        public Nullable<decimal> tOTV { get; set; }
        public Nullable<short> FirmaID { get; set; }
        public Nullable<decimal> GbpKur { get; set; }
        public Nullable<decimal> ChfKur { get; set; }
        public string Ack1 { get; set; }
        public Nullable<bool> Aktarildi { get; set; }
        public string Hafta { get; set; }
        public string AracPlakasi { get; set; }
        public string Teslimat { get; set; }
        public string Tarif { get; set; }
        public string Sikayet { get; set; }
        public string SiparisNotu { get; set; }
        public string Company_Code { get; set; }
        public string SubeDepo { get; set; }
        public string Kurye { get; set; }
        public Nullable<int> KuryeID { get; set; }
    }

    public partial class Teklif
    {
        public int ID { get; set; }
        public string TeklifNo { get; set; }
        public string CariUnvan { get; set; }
        public string Personel { get; set; }
        public Nullable<System.DateTime> IslemTarihi { get; set; }
        public Nullable<System.DateTime> TeklifSonTarihi { get; set; }
        public Nullable<int> CariID { get; set; }
        public Nullable<decimal> BrutToplam { get; set; }
        public Nullable<decimal> NetToplam { get; set; }
        public Nullable<decimal> ToplamKdv { get; set; }
        public Nullable<decimal> ToplamIskonto { get; set; }
        public Nullable<decimal> GenelToplam { get; set; }
        public Nullable<int> PersonelID { get; set; }
        public string kdvDH { get; set; }
        public string ParaBirimi { get; set; }
        public string Aciklama { get; set; }
        public string Donem { get; set; }
        public Nullable<decimal> q18Kdv { get; set; }
        public Nullable<decimal> q8Kdv { get; set; }
        public Nullable<decimal> q1Kdv { get; set; }
        public Nullable<decimal> Iskonto { get; set; }
        public string Durumu { get; set; }
        public string IslemTipi { get; set; }
        public Nullable<decimal> OzelIskonto { get; set; }
        public Nullable<decimal> SatirIskonto { get; set; }
        public string SozlesmeMetni { get; set; }
        public Nullable<int> OzelKodID { get; set; }
        public Nullable<decimal> YerelTutar { get; set; }
        public Nullable<decimal> DolarKur { get; set; }
        public Nullable<decimal> EuroKur { get; set; }
        public Nullable<int> KayitPersonelID { get; set; }
        public Nullable<System.DateTime> KayitTarih { get; set; }
        public Nullable<decimal> Kur { get; set; }
        public Nullable<int> ProjeID { get; set; }
        public string OzelAlan1 { get; set; }
        public string OzelAlan2 { get; set; }
        public string OzelAlan3 { get; set; }
        public string OzelAlan4 { get; set; }
        public string OzelAlan5 { get; set; }
        public Nullable<bool> IptalEdildi { get; set; }
        public Nullable<System.DateTime> VadeTarihi { get; set; }
        public Nullable<decimal> tOIV { get; set; }
        public Nullable<decimal> tOTV { get; set; }
        public Nullable<short> FirmaID { get; set; }
        public Nullable<decimal> ChfKur { get; set; }
        public Nullable<decimal> GbpKur { get; set; }
        public string TekTip { get; set; }
        public string Ack1 { get; set; }
        public Nullable<bool> Aktarildi { get; set; }
        public Nullable<bool> UrunAciklamasiGoster { get; set; }
        public string TOdemeSekli { get; set; }
        public string TeklifSuresi { get; set; }
        public string Teslimat { get; set; }
        public string Company_Code { get; set; }
    }

    public partial class GelirGider
    {
        public decimal GelirPzt { get; set; }
        public decimal GelirSali { get; set; }
        public decimal GelirCar { get; set; }
        public decimal GelirPer { get; set; }
        public decimal GelirCuma { get; set; }
        public decimal GelirCmr { get; set; }
        public decimal GelirPaz { get; set; }
        public decimal GiderPzt { get; set; }
        public decimal GiderSali { get; set; }
        public decimal GiderCar { get; set; }
        public decimal GiderPer { get; set; }
        public decimal GiderCuma { get; set; }
        public decimal GiderCmr { get; set; }
        public decimal GiderPaz { get; set; }
        public decimal AylikGiderKaydi { get; set; }
        public decimal AylikGelirKaydi { get; set; }
        public List<KasaDurum> KasalarList { get; set; }
        public List<BankaDurum> BankalarList { get; set; }
        
    }

    public partial class KasaDurum
    {
        public string KasaAdi { get; set; }
        public string KasaBakiye { get; set; }
    }

    public partial class BankaDurum
    {
        public string BankaAdi { get; set; }
        public string BankaBakiye { get; set; }
    }

    public partial class TEKLIF_SATIR
    {
        public int ID { get; set; }
        public string Durumu { get; set; }
        public string UrunAdi { get; set; }
        public Nullable<int> teklifID { get; set; }
        public Nullable<System.DateTime> IslemTarihi { get; set; }
        public string IslemTuru { get; set; }
        public Nullable<int> UrunID { get; set; }
        public Nullable<int> depoID { get; set; }
        public Nullable<int> personelID { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
        public Nullable<decimal> Miktar { get; set; }
        public Nullable<decimal> actTutar { get; set; }
        public Nullable<decimal> Tutar { get; set; }
        public string Birim { get; set; }
        public string VadeTarih { get; set; }
        public Nullable<int> KDV { get; set; }
        public Nullable<decimal> Iskonta { get; set; }
        public Nullable<decimal> Iskonta2 { get; set; }
        public Nullable<decimal> Iskonta3 { get; set; }
        public Nullable<decimal> Iskonta4 { get; set; }
        public Nullable<decimal> Iskonta5 { get; set; }
        public Nullable<int> Tevfikat { get; set; }
        public string IadeSuresi { get; set; }
        public string SeriNo { get; set; }
        public string pBirimi { get; set; }
        public string Variyant { get; set; }
        public string VariyantSel { get; set; }
        public Nullable<decimal> Kur { get; set; }
        public Nullable<decimal> Maliyet { get; set; }
        public string Donem { get; set; }
        public Nullable<int> GarantiSuresi { get; set; }
        public string FaturaMaliyeti { get; set; }
        public Nullable<int> CihazID { get; set; }
        public string EkAciklama { get; set; }
        public Nullable<int> SatirOzelKodID { get; set; }
        public string Fparabirimi { get; set; }
        public Nullable<decimal> BirimAdet { get; set; }
        public Nullable<decimal> AdetFiyati { get; set; }
        public Nullable<bool> IrsaliyedenGecis { get; set; }
        public Nullable<decimal> gBirimMiktar { get; set; }
        public Nullable<decimal> YerelNet { get; set; }
        public Nullable<decimal> mf { get; set; }
        public Nullable<bool> FiyatGuncelle { get; set; }
        public Nullable<System.DateTime> TeslimTarihi { get; set; }
        public Nullable<System.DateTime> HizmetSonlanma { get; set; }
        public Nullable<int> OIV { get; set; }
        public Nullable<System.DateTime> KontrolTarih { get; set; }
        public Nullable<decimal> frtFiyat { get; set; }
        public Nullable<decimal> ftrSonDesc { get; set; }
        public string SeriNoList { get; set; }
        public Nullable<decimal> OTV { get; set; }
        public Nullable<decimal> KalanMiktar { get; set; }
        public Nullable<decimal> MetreMiktarAdet { get; set; }
        public Nullable<decimal> Uzunluk { get; set; }
        public Nullable<decimal> Genislik { get; set; }
        public Nullable<decimal> Derinlik { get; set; }
        public Nullable<decimal> MiktarBasinaOTV { get; set; }
        public Nullable<decimal> kpGenislik { get; set; }
        public Nullable<decimal> KPYukseklik { get; set; }
        public Nullable<decimal> KPIskonto { get; set; }
        public Nullable<decimal> KpKar { get; set; }
        public Nullable<int> FirmaID { get; set; }
        public string Company_Code { get; set; }
    }

    public partial class TECHNICALGaranti
    {
        public int ID { get; set; }
        public string ServisIslemNo { get; set; }
        public string GarantiDurumu { get; set; }
        public string CariUnvan { get; set; }
        public string Marka { get; set; }
        public string Serino { get; set; }
        public string CihazAdi { get; set; }
        public DateTime GarantiBitisTarihi { get; set; }
        public DateTime GarantiBaslangicTarihi { get; set; }

    }

    public partial class FaturaDetaylariEk
    {
        public string UrunAdi { get; set; }
    }

    public partial class INVOICE2
    {
        public int ID { get; set; }
        public string Durumu { get; set; }
        public string TarihF2 { get; set; }
        public string kdvDH { get; set; }
        public string KapaliAcik { get; set; }
        public string VadeKontrol { get; set; }
        public string IrsaliyeID { get; set; }
        public Nullable<int> AktarilanFID { get; set; }
        public string FaturaType { get; set; }
        public string fType { get; set; }
        public string FaturaNo { get; set; }
        public string FaturaTarihi { get; set; }
        public string paraBirimi { get; set; }
        public Nullable<int> CariID { get; set; }
        public string CariKodu { get; set; }
        public string CariUnvan { get; set; }
        public string CariVergD { get; set; }
        public string CariVergiNo { get; set; }
        public Nullable<int> personelID { get; set; }
        public string Aciklama { get; set; }
        public string gVadeTarih { get; set; }
        public Nullable<decimal> gKDV { get; set; }
        public Nullable<decimal> g18KDV { get; set; }
        public Nullable<decimal> g8KDV { get; set; }
        public Nullable<decimal> g1KDV { get; set; }
        public Nullable<decimal> gTevfikat { get; set; }
        public string gIadeSuresi { get; set; }
        public Nullable<decimal> brutToplam { get; set; }
        public Nullable<decimal> satirIskonta { get; set; }
        public Nullable<decimal> ozelIskonta { get; set; }
        public Nullable<decimal> toplamIskonta { get; set; }
        public Nullable<decimal> netToplam { get; set; }
        public Nullable<decimal> tKDV { get; set; }
        public Nullable<decimal> tTevfikat { get; set; }
        public string genelToplam { get; set; }
        public string KayitT { get; set; }
        public string DegT { get; set; }
        public Nullable<decimal> FKuru { get; set; }
        public Nullable<decimal> YerelTutar { get; set; }
        public string Saat { get; set; }
        public string Donem { get; set; }
        public string Aciklama2 { get; set; }
        public string YazdirmaDurumu { get; set; }
        public Nullable<int> ServisCihazID { get; set; }
        public string VadeGun { get; set; }
        public string PrimYuzde { get; set; }
        public Nullable<decimal> DolarKur { get; set; }
        public Nullable<decimal> EuroKur { get; set; }
        public Nullable<int> FaturaOzelKod { get; set; }
        public Nullable<int> SiparisID { get; set; }
        public string IrsaliyeTuru { get; set; }
        public Nullable<int> KayitPersonelID { get; set; }
        public Nullable<System.DateTime> KayitTarih { get; set; }
        public Nullable<int> ProjeID { get; set; }
        public string FaturaSeri { get; set; }
        public string OzelAlan1 { get; set; }
        public string OzelAlan2 { get; set; }
        public string OzelAlan3 { get; set; }
        public string OzelAlan4 { get; set; }
        public string OzelAlan5 { get; set; }
        public string FaturaAdresi { get; set; }
        public string Yetkili { get; set; }
        public string SevkAdresi { get; set; }
        public Nullable<System.DateTime> SevkTarihi { get; set; }
        public string FatTip { get; set; }
        public Nullable<decimal> tOIV { get; set; }
        public Nullable<System.DateTime> ipTalTarihi { get; set; }
        public Nullable<System.DateTime> KontrolTarih { get; set; }
        public Nullable<int> AdresID { get; set; }
        public Nullable<decimal> gIskonta { get; set; }
        public string TevkifatTur { get; set; }
        public string SeriNoList { get; set; }
        public Nullable<decimal> tmaliyet { get; set; }
        public string OzelAlan6 { get; set; }
        public string OzelAlan7 { get; set; }
        public string OzelAlan8 { get; set; }
        public Nullable<short> FirmaID { get; set; }
        public Nullable<bool> CariyeIslesin { get; set; }
        public Nullable<bool> EBelgeGonderimi { get; set; }
        public Nullable<decimal> FaturaOdemesi { get; set; }
        public Nullable<decimal> GbpKur { get; set; }
        public Nullable<decimal> ChfKur { get; set; }
        public Nullable<decimal> OTVToplami { get; set; }
        public Nullable<bool> MiktarBasinaOTVDurumu { get; set; }
        public Nullable<int> AracID { get; set; }
        public Nullable<int> SantiyeID { get; set; }
        public Nullable<bool> DolarVar { get; set; }
        public Nullable<bool> EuroVar { get; set; }
        public Nullable<bool> GBPVar { get; set; }
        public string CMR { get; set; }
        public Nullable<bool> T1 { get; set; }
        public Nullable<bool> T2 { get; set; }
        public Nullable<bool> FaturaYuklendi { get; set; }
        public Nullable<bool> Pasaport { get; set; }
        public Nullable<bool> EmanetIrsaliyesi { get; set; }
        public Nullable<decimal> g10KDV { get; set; }
        public Nullable<decimal> MKDkur { get; set; }
        public string Plaka1 { get; set; }
        public string Plaka2 { get; set; }
        public Nullable<bool> eFatDuz { get; set; }
        public Nullable<decimal> YerelTL { get; set; }
        public string OB_FaturaDurumu { get; set; }
        public string OB_UUID { get; set; }
        public Nullable<decimal> jpykur { get; set; }
        public Nullable<decimal> TLjpyKur { get; set; }
        public Nullable<decimal> HAFKomisyon { get; set; }
        public Nullable<int> USTFATID { get; set; }
        public string HAFID { get; set; }
        public Nullable<decimal> ManuelFarkKur { get; set; }
        public Nullable<decimal> g19KDV { get; set; }
        public Nullable<bool> KdvliKomisyon { get; set; }
        public Nullable<decimal> TLUSDKUR { get; set; }
        public Nullable<decimal> TLEURKUR { get; set; }
        public Nullable<decimal> TLGBPKUR { get; set; }
        public string Company_Code { get; set; }
    }
    public partial class PersonelBilgiEk
    {
        public Personel pr { get; set; }
        public Yetkiler yt { get; set; }
    }

    public partial class Servis
    {
        public int ID { get; set; }
        public int Sira { get; set; }
        public string ServisIslemNo { get; set; }
        public string Serino { get; set; }
        public string Cinsi { get; set; }
        public string PersonelID { get; set; }
        public string BayiID { get; set; }
        public string Durum { get; set; }
        public string Marka { get; set; }
        public string CariID { get; set; }
        public string Model { get; set; }
        public string SonDurumTarih { get; set; }
        public int DurumSayisi { get; set; }
        public string CariUnvan { get; set; }
        public string ParaBirimi { get; set; }
        public string GenelToplam { get; set; }
        public string Teknisyen { get; set; }
        public string CihazAdresi { get; set; }
        public DateTime Tarih { get; set; }
        public string IslemTarihiF { get; set; }
        public string Aciklama { get; set; }
    }

    public partial class Bordrolar
    {
        public int ID { get; set; }
        public string KayitTipi { get; set; }
        public string IslemNumarasi { get; set; }
        public string KayitTarihi { get; set; }
        public string Cari { get; set; }
        public string Personel { get; set; }
        public string Tutar { get; set; }
        public string PB { get; set; }
        public string OrtalamaVade { get; set; }
        public string Aciklama { get; set; }
        public int PersonelAdi { get; set; }
        public string IslemTarihiF { get; set; }
    }

    public partial class Cekler
    {
        public int ID { get; set; }
        public string Durumu { get; set; }
        public string BordroTipi { get; set; }
        public string BordroNumarasi { get; set; }
        public string IslemNumarasi { get; set; }
        public string SeriNumarasi { get; set; }
        public string VadeTarihi { get; set; }
        public string Cari { get; set; }
        public string BorcluAlacakli { get; set; }
        public string Tutar { get; set; }
        public string PB { get; set; }
        public string CekBankasi { get; set; }
        public string Aciklama { get; set; }
        public int PersonelAdi { get; set; }
        public string IslemTarihiF { get; set; }
    }

    public partial class IslemlerListesi
    {
        public int No { get; set; }
        public string IslemNo { get; set; }
        public string IslemTipi { get; set; }
        public string Aciklama { get; set; }
        public string Tarih { get; set; }
        public string Tutar { get; set; }
        public string Doviz { get; set; }
    }

    public partial class TAHO
    {
        public string IslemTipi { get; set; }
        public string C_ { get; set; }
        public string IslemTuru { get; set; }
        public string IslemNo { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<System.DateTime> Vade { get; set; }
        public string Cari { get; set; }
        public string Kasa { get; set; }
        public string Banka { get; set; }
        public string KrediKart { get; set; }
        public Nullable<decimal> Tutar { get; set; }
        public string PB { get; set; }
        public string AB { get; set; }
        public Nullable<decimal> YerelTutar { get; set; }
        public string OzelKodAdi { get; set; }
        public string Personel { get; set; }
        public string Aciklama { get; set; }
        public Nullable<decimal> Kur { get; set; }
        public int ID { get; set; }
        public Nullable<int> PersonelID { get; set; }
        public Nullable<int> OzelKodID { get; set; }
        public Nullable<int> KasaID { get; set; }
        public Nullable<int> BankaID { get; set; }
        public Nullable<int> KrediKartiID { get; set; }
        public Nullable<int> gonderenID { get; set; }
        public Nullable<int> alanID { get; set; }
        public string GPB { get; set; }
        public string APB { get; set; }
        public string Tablo { get; set; }
    }

    public partial class FirmaDetail
    {
        public COMPANY_DETAIL detail { get; set; }
        public Ayarlar settings { get; set; }
        public List<Kategoriler> kat { get; set; }
        public List<Kategoriler> carikat { get; set; }
        public List<Kategoriler> serviskat { get; set; }
        public List<Kategoriler> FaturaList { get; set; }
        public List<CURRENCY_LIST> ParaBirimleri { get; set; }
    }

    public partial class Kategoriler
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public partial class CariListesi
    {
        public int ID { get; set; }
        public string FirmaKodu { get; set; }
        public string KTipi { get; set; }
        public string CariUnvan { get; set; }
        public string CariGrubu { get; set; }
        public string Telefon1 { get; set; }
        public string Yetkili { get; set; }
        public string TlBakiye { get; set; }
        public string EURBakiye { get; set; }
        public string USDBakiye { get; set; }
    }

    public partial class HareketlerListesi
    {
        public int ID { get; set; }
        public string FaturaNo { get; set; }
        public string ParaBirimi { get; set; }
        public Nullable<System.DateTime> IslemTarihi { get; set; }
        public string IslemTarihiF { get; set; }
        public Nullable<System.DateTime> Vade { get; set; }
        public Nullable<decimal> Tutar { get; set; }
        public string Aciklama { get; set; }
        public string Doviz { get; set; }
        public Nullable<decimal> Alacak { get; set; }
        public Nullable<decimal> Borc { get; set; }
        public Nullable<decimal> Bakiye { get; set; }
        public string AlacakS { get; set; }
        public string BorcS { get; set; }
        public string BakiyeS { get; set; }
        public string IslemTipi { get; set; }
        public string FaturaType { get; set; }


    }

    public partial class Finans
    {

        public string ID { get; set; }
        public string IslemTuru { get; set; }
        public string IslemNo { get; set; }
        public string OzelKodAdi { get; set; }
        public string Cari { get; set; }
        public string Aciklama { get; set; }
        public string Kasa { get; set; }
        public string Banka { get; set; }
        public string KrediKart { get; set; }
        public string YerelTutar { get; set; }
        public string Personel { get; set; }
        public string Tablo { get; set; }
        public string PB { get; set; }
        public string TarihF2 { get; set; }
        public string PersonelID { get; set; }
        public string IslemTipi { get; set; }
        public string Giris { get; set; }
        public string Cikis { get; set; }
        public string Bakiye { get; set; }
        public string AB { get; set; }
        public Nullable<decimal> EuroBakiye { get; set; }
        public Nullable<decimal> UsdNakiye { get; set; }
        public DateTime Tarih { get; set; }

    }

    public partial class Kasa1
    {
        public int ID { get; set; }
        public string KasaKodu { get; set; }
        public string Aciklama { get; set; }
        public string KasaAdi { get; set; }
        public string Varsayilan { get; set; }
        public Nullable<decimal> Bakiye { get; set; }
        public Nullable<decimal> Bakiye_EUR { get; set; }
        public Nullable<decimal> Bakiye_USD { get; set; }
        public Nullable<decimal> Bakiye_GBP { get; set; }
        public string ParaBirimi { get; set; }
        public DateTime KayitT { get; set; }
    }

    public partial class Banka1
    {

        public Nullable<decimal> Bakiye { get; set; }
        public Nullable<decimal> Bakiye_EUR { get; set; }
        public Nullable<decimal> Bakiye_USD { get; set; }
        public Nullable<decimal> Bakiye_GBP { get; set; }
        public int ID { get; set; }
        public string BankaKodu { get; set; }
        public string BankaAdi { get; set; }
        public string Varsayilan { get; set; }
        public string SubeAdi { get; set; }
        public string HesapNo { get; set; }
        public string hnParaBirimi { get; set; }
        public string Iban { get; set; }
        public string IlgiliKisi { get; set; }
        public string IletisimNo { get; set; }
        public string FaxNo { get; set; }
        public Nullable<int> MKodu { get; set; }
        public string EkBilgiler { get; set; }
        public DateTime KayitT { get; set; }
        public string DegT { get; set; }
        public Nullable<short> FirmaID { get; set; }
        public Nullable<decimal> ABorc2018 { get; set; }
        public Nullable<bool> GizliBanka { get; set; }
        public string Aciklama { get; set; }
        public string Telefon { get; set; }

    }

    public partial class SiparisListesi
    {
        public string IslemNo { get; set; }
        public string IslemTipi { get; set; }
        public Nullable<System.DateTime> IslemTarih { get; set; }
        public Nullable<decimal> ntoTutar { get; set; }
        public Nullable<decimal> kktTutar { get; set; }
        public string ParaBirimi { get; set; }
        public Nullable<decimal> Indirim { get; set; }
        public Nullable<decimal> GenelTutar { get; set; }
        public string IslemTipiHP { get; set; }
        public string gVadeTarihi { get; set; }
        public string OdemeTipi { get; set; }
        public string MusteriAdi { get; set; }
        public Nullable<int> PersonelID { get; set; }
        public string SatisNo { get; set; }
    }

    public partial class CariModel
    {
        public int ID { get; set; }
        public string FirmaKodu { get; set; }
        public string KTipi { get; set; }
        public string CariUnvan { get; set; }
        public string VergiDr { get; set; }
        public string VergiNo { get; set; }
        public string Yetkili { get; set; }
        public string TCNo { get; set; }
        public string paraBirimi { get; set; }
        public Nullable<decimal> Iskonto { get; set; }
        public Nullable<decimal> Bakiye { get; set; }
        public Nullable<decimal> SatisF2 { get; set; }
        public string CariGrubu { get; set; }
        public string Fotograf { get; set; }
        public string Adres { get; set; }
        public string Telefon1 { get; set; }
        public string GSM { get; set; }
        public string Faks { get; set; }
        public string EPosta { get; set; }
        public string WebSite { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string Ulke { get; set; }
        public string PostaKodu { get; set; }
        public string Cinsiyet { get; set; }
        public string DogumT { get; set; }
        public string Milleti { get; set; }
        public string MedeniH { get; set; }
        public string EkBilgi { get; set; }
        public string ozelAlan1 { get; set; }
        public string ozelAlan2 { get; set; }
        public string ozelAlan3 { get; set; }
        public string ozelAlan4 { get; set; }
        public string ozelAlan5 { get; set; }
        public string KayitT { get; set; }
        public string DegT { get; set; }
        public Nullable<bool> CariList { get; set; }
        public Nullable<int> MusteriTemsilcisi { get; set; }
        public Nullable<int> OzelKodID { get; set; }
        public Nullable<short> FirmaID { get; set; }
        public string MersisNo { get; set; }
        public string TicaretSicilNo { get; set; }
        public string BagkurNo { get; set; }
        public Nullable<int> DateVade { get; set; }
        public string StokFG { get; set; }
        public string WebKA { get; set; }
        public string WebSifre { get; set; }
        public Nullable<int> TedCariID { get; set; }
        public Nullable<bool> Aktarildi { get; set; }
        public string EkAlan { get; set; }
        public Nullable<bool> ToplamBazindaRisk { get; set; }
        public Nullable<decimal> BakiyeLimit { get; set; }
        public Nullable<int> SantiyeID { get; set; }
        public Nullable<bool> EfaturaKullanicisi { get; set; }
        public string city2 { get; set; }
        public string town2 { get; set; }
        public string address2 { get; set; }
        public Nullable<bool> KomisyonOtomatik { get; set; }
        public Nullable<decimal> FaturaKomisyonGideri { get; set; }
        public Nullable<int> UstCariID { get; set; }
        public string PayType { get; set; }
        public string Plaka { get; set; }
        public string NotDefteri { get; set; }
        public string AltUst { get; set; }
        public decimal Alacak { get; set; }
        public decimal Borc { get; set; }
        public string SonDurum { get; set; }
        public Nullable<bool> eFatDuzenlenmesin { get; set; }
        public string Iletisim { get; set; }
    }

    public partial class ListCariBorc
    {
        public Cari cari;
        public decimal ToplamBakiye;
        public string ParaDurumu;

    }

    public partial class KarZarar
    {
        public string Ay { get; set; }
        public decimal SF { get; set; }
        public decimal AF { get; set; }
        public decimal MF { get; set; }
        public decimal TP { get; set; }
        public decimal Durum { get; set; }
    }

    public partial class PerakendeList
    {
        public int ID { get; set; }
        public string IslemNo { get; set; }
        public string IslemTipi { get; set; }
        public string Tur { get; set; }
        public string OdemeTipi { get; set; }
        public string MusteriAdi { get; set; }
        public string IslemTarihi { get; set; }
        public string Indirim { get; set; }
        public string Tutar { get; set; }
        public string NakitTutar { get; set; }
        public string KKTutar { get; set; }
        public string HavaleTutar { get; set; }
        public string ParaKTutar { get; set; }
        public string KazanilanPuan { get; set; }
        public string PB { get; set; }
        public string Personel { get; set; }
        public string SatisNotu { get; set; }
        public string IslemTarihiF { get; set; }
    }

}