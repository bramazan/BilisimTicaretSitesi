using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace E_ticaret.Models
{
    public class Giris
    {
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "*Yanlis mail adresi girdiniz !")]
        [Display(Name = "E-Mail Gir")]
        public string EPosta { get; set; }

        [Required(ErrorMessage = "*Parola giriniz !")]
        [Display(Name = "Parola Gir")]
        [DataType(DataType.Password)]

        public string Parola { get; set; }

        public bool RememberMe { get; set; }
    }
}