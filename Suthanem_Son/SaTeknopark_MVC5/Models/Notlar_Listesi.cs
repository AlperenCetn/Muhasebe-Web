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
    
    public partial class Notlar_Listesi
    {
        public int ID { get; set; }
        public Nullable<int> KategoriID { get; set; }
        public string NotBasligi { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string NotIcerik { get; set; }
        public Nullable<int> FirmaID { get; set; }
        public string Company_Code { get; set; }
    }
}
