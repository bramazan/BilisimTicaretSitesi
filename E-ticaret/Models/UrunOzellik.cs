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
    
    public partial class UrunOzellik
    {
        public UrunOzellik()
        {
            this.Urun = new HashSet<Urun>();
            this.UrunOzellikDetay = new HashSet<UrunOzellikDetay>();
        }
    
        public int urunozellikId { get; set; }
        public string ozellikadi { get; set; }
    
        public virtual ICollection<Urun> Urun { get; set; }
        public virtual ICollection<UrunOzellikDetay> UrunOzellikDetay { get; set; }
    }
}
