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
    
    public partial class UrunResim
    {
        public int urunresimId { get; set; }
        public Nullable<int> urunId { get; set; }
        public string resimadi { get; set; }
    
        public virtual Urun Urun { get; set; }
    }
}
