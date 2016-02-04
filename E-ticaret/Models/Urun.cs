//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_ticaret.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Urun
    {
        public Urun()
        {
            this.Fiyat = new HashSet<Fiyat>();
            this.Kampanya = new HashSet<Kampanya>();
            this.Sepet = new HashSet<Sepet>();
            this.Siparis = new HashSet<Siparis>();
            this.Taksitler = new HashSet<Taksitler>();
            this.UrunOzellikDetay = new HashSet<UrunOzellikDetay>();
            this.UrunResim1 = new HashSet<UrunResim>();
            this.UrunYorum = new HashSet<UrunYorum>();
        }
    
        public int urunId { get; set; }
       
        public string urunadi { get; set; }
         
        public Nullable<int> kategoriId { get; set; }
        public Nullable<int> markaId { get; set; }
        public Nullable<int> siparisId { get; set; }
        public Nullable<int> ozellikId { get; set; }
        public Nullable<int> kampanyaId { get; set; }
        public Nullable<decimal> urunfiyat { get; set; }
        public byte[] urunresim { get; set; }
    
        public virtual ICollection<Fiyat> Fiyat { get; set; }
        public virtual ICollection<Kampanya> Kampanya { get; set; }
        public virtual Kampanya Kampanya1 { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual Marka Marka { get; set; }
        public virtual ICollection<Sepet> Sepet { get; set; }
        public virtual ICollection<Siparis> Siparis { get; set; }
        public virtual Siparis Siparis1 { get; set; }
        public virtual ICollection<Taksitler> Taksitler { get; set; }
        public virtual UrunOzellik UrunOzellik { get; set; }
        public virtual ICollection<UrunOzellikDetay> UrunOzellikDetay { get; set; }
        public virtual ICollection<UrunResim> UrunResim1 { get; set; }
        public virtual ICollection<UrunYorum> UrunYorum { get; set; }
    }
}