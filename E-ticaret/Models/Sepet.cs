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
    
    public partial class Sepet
    {
        public Sepet()
        {
            this.Siparis = new HashSet<Siparis>();
        }
    
        public int sepetId { get; set; }
        public Nullable<int> urunId { get; set; }
        public Nullable<int> uyeId { get; set; }
        public Nullable<int> miktar { get; set; }
    
        public virtual Urun Urun { get; set; }
        public virtual Uyeler Uyeler { get; set; }
        public virtual ICollection<Siparis> Siparis { get; set; }
    }
}