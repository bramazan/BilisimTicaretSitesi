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
    
    public partial class Fatura
    {
        public int faturaId { get; set; }
        public Nullable<int> siparisId { get; set; }
        public Nullable<int> uyeId { get; set; }
    
        public virtual Siparis Siparis { get; set; }
        public virtual Uyeler Uyeler { get; set; }
    }
}