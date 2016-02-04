using E_ticaret.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Security;
using System.Globalization;


namespace E_ticaret.Controllers
{
    public class HomeController : Controller
    {
        eticaretEntities ent = new eticaretEntities();

        public ActionResult Index()
        {
            AnaSayfaDTO obj = new AnaSayfaDTO();
            obj.slider = ent.Slider.Where(x => (x.baslangictarihi <= DateTime.Now && x.bitistarihi > DateTime.Now)).ToList();
            //obj.uruntablo = ent.Urun.Where(x => (x.urunId == 71)).ToList();
            obj.kampanyaSlider = ent.Kampanya.Where(i => i.urunId != null && i.Urun != null && (i.baslangictarihi <= DateTime.Now && i.bitistarihi > DateTime.Now)).ToList();
            obj.vestelkampanyaSlider = ent.Kampanya.Where(i => i.urunId != null && i.Urun != null && (i.baslangictarihi <= DateTime.Now && i.bitistarihi > DateTime.Now)).ToList();

            obj.anaKategoriListesi = ent.Kategori.Where(i => i.anakategoriId == 0).ToList();
            obj.altkategorilistesi = ent.Kategori.Where(i => i.anakategoriId != 0).ToList();
            obj.sonurunler = ent.Urun.OrderBy(r => Guid.NewGuid()).Take(6).ToList();
            obj.gununurunleri = ent.Urun.OrderBy(r => Guid.NewGuid()).Take(6).ToList();
            //şimdilik çok satan ürünler rastegele çekiliyor veritabanından normalde satış miktarına göre çekilmelidir.
            obj.encoksatanurunler = ent.Urun.OrderBy(r => Guid.NewGuid()).Take(6).ToList();

            obj.yeniurunler = ent.Urun.OrderByDescending(x=>x.urunId).Take(6).ToList();


            

         
            return View("Index",obj);
                       
        }

        

        public ActionResult products(int id)
        {
            pr_items obj = new pr_items();

            obj.uruntablo = ent.Urun.Where(i => i.Kategori.kategoriId == id).ToList();
            obj.anaKategoriListesi = ent.Kategori.Where(i => i.anakategoriId == 0).ToList();
            obj.altkategorilistesi = ent.Kategori.Where(i => i.anakategoriId != 0).ToList();
            obj.gununurunleri = ent.Urun.OrderBy(r => Guid.NewGuid()).Take(6).ToList();



            return View ("products",obj);
        }


        public ActionResult productssummary(int id)
        {
            pr_summary obj = new pr_summary();

            obj.anaKategoriListesi = ent.Kategori.Where(i => i.anakategoriId == 0).ToList();
            obj.altkategorilistesi = ent.Kategori.Where(i => i.anakategoriId != 0).ToList();
            obj.gununurunleri = ent.Urun.OrderBy(r => Guid.NewGuid()).Take(6).ToList();



            return View("productssummary", obj);
        }




        public ActionResult productsdetay(int id)
        {

            urundetaysa denes = new urundetaysa();
            denes.uruntablo = ent.Urun.Where(i => i.Kategori.kategoriId == id).ToList();
            denes.anaKategoriListesi = ent.Kategori.Where(i => i.anakategoriId == 0).ToList();
            denes.altkategorilistesi = ent.Kategori.Where(i => i.anakategoriId != 0).ToList();
            denes.gununurunleri = ent.Urun.OrderBy(r => Guid.NewGuid()).Take(6).ToList();

            var urunDetay = ent.Urun.Where(i => i.urunId == id).FirstOrDefault();

            //Urun deneme = urunDetay;

            
            //denes.urunAdi = urunDetay.urunadi;
            ViewBag.urunAdi = urunDetay.urunadi;
            ViewBag.resim = urunDetay.urunresim;
            ViewBag.urunfiyat = urunDetay.urunfiyat;
            return View("productsdetay", denes);
        }



        //Üyelik İşlemleri
        public ActionResult Signup()
        {
            if (Session["kullanici"] == null)
            {
                List<SelectListItem> listes = new List<SelectListItem>();
                
                listes.Add(new SelectListItem { Text = "Bay", Value = "Bay" });
                listes.Add(new SelectListItem { Text = "Bayan", Value = "Bayan" });

                ViewBag.cinsiyetListesi = listes;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "guvenliksorusu,guvenliksorusucevap,cinsiyet,uyeId,email,parola,ad,soyad,telefon,adres,rolId,kayittarihi")] Uyeler kisiler)
        {
            if (kisiler.parola == kisiler.parola)
            {
                kisiler.rolId = 2;
                kisiler.kayittarihi = System.DateTime.Now;
                if (ModelState.IsValid)
                {
                    string sifrelisifre = FormsAuthentication.HashPasswordForStoringInConfigFile(kisiler.parola, "md5");
                    kisiler.parola = sifrelisifre;
                    ent.Uyeler.Add(kisiler);
                    ent.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Şifreleriniz Uyuşmuyor");
            }

            return View(kisiler);
        }
        [AllowAnonymous]
        public ActionResult Signin()
        {
            if (Session["kullanici"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(Uyeler model)
        {
            string sifrelisilfre = FormsAuthentication.HashPasswordForStoringInConfigFile(model.parola, "md5");
            var kayitlar = ent.Uyeler.Where(m => m.email == model.email && m.parola == sifrelisilfre).FirstOrDefault();
            if (kayitlar != null)
            {
                Session["kullaniciAdi"] = kayitlar.ad + " " + kayitlar.soyad;
                Session["kullaniciId"] = kayitlar.uyeId;
                if (kayitlar.rolId == 1)
                {
                    Session["kullanici"] = "1";
                }
                else if (kayitlar.rolId == 2)
                {
                    Session["kullanici"] = "2";
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı Adı veya Şifreniz Yanlış!");
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["kullaniciAdi"] = null;
            Session["kullanici"] = null;
            Session["kullaniciId"] = null;
            return RedirectToAction("Index", "Home");

        }
        public ActionResult NewPass()
        {
            if (Session["kullanici"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        
        

        public List<Kategori> subCategory(int anaId)
        {
            List<Kategori> listes = ent.Kategori.Where(i => i.anakategoriId == anaId).ToList();
            return listes;
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       

        public ActionResult tumkampanyalar()
        {
            TumKampanyalar tk = new TumKampanyalar();
            tk.anaKategoriListesi = ent.Kategori.Where(i => i.anakategoriId == 0).ToList();
            tk.altkategorilistesi = ent.Kategori.Where(i => i.anakategoriId != 0).ToList();
            tk.gununurunleri = ent.Urun.OrderBy(r => Guid.NewGuid()).Take(4).ToList();
            tk.tumkampanyalar = ent.Kampanya.Where(i => i.urunId != null && i.Urun != null).ToList();


            return View("tumkampanyalar",tk);
        }

        public ActionResult tumurunler(string currentFilter, int? page)
        {
            TumUrunler tu = new TumUrunler();
            tu.anaKategoriListesi = ent.Kategori.Where(i => i.anakategoriId == 0).ToList();
            tu.altkategorilistesi = ent.Kategori.Where(i => i.anakategoriId != 0).ToList();
            tu.gununurunleri = ent.Urun.OrderBy(r => Guid.NewGuid()).Take(4).ToList();
            tu.tumurunler = ent.Urun.ToList();
            //var de = from s in ent.Urun select s;
            //int pageSize = 10;
            //int pageNumber = (page ?? 1);
            return View("tumurunler" ,tu);

            
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Response.Redirect("~/Home/Login");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Giris x)
        {
            string sifrelisilfre = FormsAuthentication.HashPasswordForStoringInConfigFile(x.Parola, "md5");
            if (ModelState.IsValid)
            {
                var giris = ent.Uyeler.Where(i => i.email == x.EPosta && i.parola == sifrelisilfre && i.rolId == 1).FirstOrDefault();
                if (giris != null)
                {
                    Session["uye"] = "girdi";
                    Response.Redirect("~/Admin/Index");

                }
            }
            return View();
        }


        //dil değiştirmek içi kullanıyorruz tekrardan oldugumuz sayafaya dil değişmiş olarak geliyor.
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }

               
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       



        [HttpPost]
        public ActionResult Contact(iletisimmodel model)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("Rumuz: " + model.NickName);
                body.AppendLine("İsim: " + model.FullName);
                body.AppendLine("Tel: " + model.Phone);
                body.AppendLine("Eposta: " + model.Email);
                body.AppendLine("Konu: " + model.Message);
                Gmail.SendMail(body.ToString());
                ViewBag.Success = true;
            }
            return View();
        }



        
    
    }
    public class urundetaysa
    {
        public string urunAdi { get; set; }
        public List<Kategori> anaKategoriListesi { get; set; }
        public List<Kategori> altkategorilistesi { get; set; }
        public List<Urun> uruntablo { get; set; }
        public List<Urun> gununurunleri { get; set; }
    }
    public class pr_items
    {
        public List<Kategori> anaKategoriListesi { get; set; }
        public List<Kategori> altkategorilistesi { get; set; }
        public List<Urun> uruntablo { get; set; }
        public List<Urun> gununurunleri { get; set; }
    }


    public class pr_summary
    {
        public List<Kategori> anaKategoriListesi { get; set; }
        public List<Kategori> altkategorilistesi { get; set; }
   
        public List<Urun> gununurunleri { get; set; }
    }

    public class AnaSayfaDTO
    {
        public List<Slider> slider { get; set; }
        public List<Kampanya> kampanyaSlider { get; set; }
        public List<Urun> uruntablo { get; set; }
        public List<Kampanya> vestelkampanyaSlider { get; set; }
        public List<Kategori> anaKategoriListesi { get; set; }
        public List<Kategori> altkategorilistesi { get; set; }
        public List<Urun> sonurunler { get; set; }
        public List<Urun> gununurunleri { get; set; }
        public List<Urun> encoksatanurunler { get; set; }
        public List<Urun> yeniurunler { get; set; }
        
    }

    public class TumKampanyalar
    {
        public List<Kategori> anaKategoriListesi { get; set; }
        public List<Kategori> altkategorilistesi { get; set; }
        public List<Kampanya> tumkampanyalar { get; set; }
        public List<Urun> gununurunleri { get; set; }
        public List<Urun> uruntablo { get; set; }
    }

    public class TumUrunler
    {
        public List<Kategori> anaKategoriListesi { get; set; }
        public List<Kategori> altkategorilistesi { get; set; }
        public List<Urun> tumurunler { get; set; }
        public List<Urun> gununurunleri { get; set; }
        public List<Urun> uruntablo { get; set; }
        
    }

}