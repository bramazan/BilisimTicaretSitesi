using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_ticaret.Models;
using System.IO;
using PagedList;
using System.Web.Security;

namespace E_ticaret.Controllers
{
    public class AdminController : Controller
    {
        public eticaretEntities ent = new eticaretEntities();

        public ActionResult Kapat()
        {
            
            return View();


        }
        public ActionResult Index()
        {
            ViewBag.Mesaj = "gunaydın";
            return View();
        }

        
        #region // Slider

        public ActionResult Slider(string siralama,string arama, string currentFilter, int? page)
        {
            ViewBag.NameSortParm =  String.IsNullOrEmpty(siralama) ? "slidertext_azalan" : "";
            ViewBag.DateSortParm = siralama == "tarih_artan" ? "tarih_azalan" : "tarih_artan";



            //var slider = ent.Slider.ToList();
            var slider = from s in ent.Slider select s;
            //arama
            if (!String.IsNullOrEmpty(arama))
            {
                slider = slider.Where(s => s.slidertext.ToUpper().Contains(arama.ToUpper()));
            }

            //sıralama
            switch(siralama)
            {
                case "slidertext_azalan":
                    slider = slider.OrderByDescending(s => s.slidertext);
                    break;

                case"tarih_artan":
                     slider = slider.OrderBy(s => s.baslangictarihi);
                    break;
                case "tarih_azalan":
                    slider = slider.OrderByDescending(s => s.baslangictarihi);
                    break;

                default :
                    slider = slider.OrderBy(s => s.baslangictarihi);
                    break;
                        
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(slider.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult SlideEkle()
        {
            return View();
        }
        public ActionResult SlideDuzenle(int? SlideID)
        {
            var _slideDuzenle = ent.Slider.Where(x => x.sliderId == SlideID).FirstOrDefault();
            return View(_slideDuzenle);
        }
        public ActionResult SlideSil(int SlideID)
        {
            try
            {
                ent.Slider.Remove(ent.Slider.First(d => d.sliderId == SlideID));
                ent.SaveChanges();
                return RedirectToAction("Slider", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }
        [HttpPost]
        public ActionResult SlideEkle(Slider s, HttpPostedFileBase file)
        {
            try
            {
                Slider _slide = new Slider();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _slide.sliderfoto = memoryStream.ToArray();
                }
                _slide.slidertext = s.slidertext;
                _slide.baslangictarihi = s.baslangictarihi;
                _slide.bitistarihi = s.bitistarihi;
                ent.Slider.Add(_slide);
                ent.SaveChanges();
                return RedirectToAction("Slider", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }
        [HttpPost]
        public ActionResult SlideDuzenle(Slider slide, HttpPostedFileBase file)
        {
            try
            {
                var _slideDuzenle = ent.Slider.Where(x => x.sliderId == slide.sliderId).FirstOrDefault();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _slideDuzenle.sliderfoto = memoryStream.ToArray();
                }
                _slideDuzenle.slidertext = slide.slidertext;
                _slideDuzenle.baslangictarihi = slide.baslangictarihi;
                _slideDuzenle.bitistarihi = slide.bitistarihi;
                ent.SaveChanges();
                return RedirectToAction("Slider", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }
        
        #endregion


         #region//marka yonetimi
        public ActionResult Marka(string siralama, string arama,string currentFilter, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(siralama) ? "markaadi_azalan" : "";
           
            //var marka = ent.Marka.ToList();
            var marka = from s in ent.Marka select s;
            //arama
            if (!String.IsNullOrEmpty(arama))
            {
                marka = marka.Where(s => s.markaadi.ToUpper().Contains(arama.ToUpper()));
            }

            //sıralama
            switch(siralama)
            {
                case "markaadi_azalan":
                    marka = marka.OrderByDescending(s => s.markaadi);
                    break;

                

                default :
                    marka = marka.OrderBy(s => s.markaadi);
                    break;
                        
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(marka.ToPagedList(pageNumber, pageSize));
            
        }


        public ActionResult MarkaEkle()
        {
            return View();
        }


        public ActionResult MarkaDuzenle(int? MarkaID)
        {
            var _markaDuzenle = ent.Marka.Where(x => x.markaId == MarkaID).FirstOrDefault();
            return View(_markaDuzenle);
        }

        [HttpPost]
        public ActionResult MarkaEkle(Marka m, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Marka _marka = new Marka();
                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream memoryStream = file.InputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            file.InputStream.CopyTo(memoryStream);
                        }
                        _marka.firmaresmi = memoryStream.ToArray();
                    }
                    _marka.markaadi = m.markaadi;
                    _marka.aciklama = m.aciklama;
                    //_marka.bitistarihi = s.bitistarihi;
                    ent.Marka.Add(_marka);
                    ent.SaveChanges();
                    return RedirectToAction("Marka", "Admin");
                }
                catch (Exception ex)
                {
                    throw new Exception("Eklerken hata oluştu");
                }
               
            }
            //doğrulama yapılmadığı takdirde ekrana aynı view getirilecek
            return View();
            
        }


        public ActionResult MarkaSil(int MarkaID)
        {
            try
            {
                ent.Marka.Remove(ent.Marka.First(d => d.markaId== MarkaID));
                ent.SaveChanges();
                return RedirectToAction("Marka", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }



        [HttpPost]
        public ActionResult MarkaDuzenle(Marka marka, HttpPostedFileBase file)
        {
            try
            {
                var _markaDuzenle = ent.Marka.Where(x => x.markaId == marka.markaId).FirstOrDefault();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _markaDuzenle.firmaresmi = memoryStream.ToArray();
                }
                _markaDuzenle.markaadi = marka.markaadi;
                _markaDuzenle.aciklama = marka.aciklama;
                //_markaDuzenle. = marka;
                ent.SaveChanges();
                return RedirectToAction("Marka", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }



        #endregion



        #region //kampanya yonetimi

        public ActionResult Kampanya(string siralama, string arama,string currentFilter, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(siralama) ? "kampanyaadi_azalan" : "";
            ViewBag.IndirimSortParm = siralama == "indirimorani_artan" ? "indirimorani_azalan" : "indirimorani_artan";
            ViewBag.UrunadiSortParm = siralama == "ürünadi_artan" ? "ürünadi_azalan" : "ürünadi_artan";
            ViewBag.DateSortParm = siralama == "baslangictarih_artan" ? "baslangictarih_azalan" : "baslangictarih_artan";
            ViewBag.FinishDateSortParm = siralama == "bitistarih_artan" ? "bitistarih_azalan" : "bitistarih_artan";

            var kampanya = from s in ent.Kampanya select s;

            if (!String.IsNullOrEmpty(arama))
            {
                kampanya = kampanya.Where(s => s.kampanyabasligi.ToUpper().Contains(arama.ToUpper()));
            }


            //sıralama

            switch (siralama)
            {
                case "kampanyaadi_azalan":
                    kampanya = kampanya.OrderByDescending(s => s.kampanyabasligi);
                    break;

                case "indirimorani_artan":
                    kampanya = kampanya.OrderBy(s => s.indirimorani);
                    break;
                case "indirimorani_azalan":
                    kampanya = kampanya.OrderByDescending(s => s.indirimorani);
                    break;

                case "ürünadi_artan":
                    kampanya = kampanya.OrderBy(s => s.Urun.urunadi);
                    break;

                case "ürünadi_azalan":
                    kampanya = kampanya.OrderByDescending(s => s.Urun.urunadi);
                    break;

                case "baslangictarih_artan":
                    kampanya = kampanya.OrderBy(s => s.baslangictarihi);
                    break;

                case "baslangictarih_azalan":
                    kampanya = kampanya.OrderByDescending(s => s.baslangictarihi);
                    break;
                case "bitistarih_artan":
                    kampanya = kampanya.OrderBy(s => s.bitistarihi);
                    break;

                case "bitistarih_azalan":
                    kampanya = kampanya.OrderByDescending(s => s.bitistarihi);
                    break;

                default:
                    kampanya = kampanya.OrderBy(s => s.indirimorani);
                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(kampanya.ToPagedList(pageNumber, pageSize));
            
        }

        public ActionResult KampanyaEkle()
        {
            ViewBag.urunId = new SelectList(ent.Urun, "urunId", "urunadi");
            return View();
        }


        public ActionResult KampanyaDuzenle(int? KampanyaID)
        {
            var _kampanyaDuzenle = ent.Kampanya.Where(x => x.kampanyaId == KampanyaID).FirstOrDefault();
            ViewBag.urunId = new SelectList(ent.Urun, "urunId", "urunadi");
            return View(_kampanyaDuzenle);
        }


        public ActionResult KampanyaSil(int KampanyaID)
        {
            try
            {
                ent.Kampanya.Remove(ent.Kampanya.First(d => d.kampanyaId == KampanyaID));
                ent.SaveChanges();
                return RedirectToAction("Kampanya", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }


        [HttpPost]
        public ActionResult KampanyaEkle(Kampanya s, HttpPostedFileBase file)
        {
            try
            {
                Kampanya _kampanya = new Kampanya();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _kampanya.kampanyagorseli = memoryStream.ToArray();
                }
                _kampanya.kampanyabasligi = s.kampanyabasligi;
                _kampanya.indirimorani = s.indirimorani;
                _kampanya.kampanyaaciklamasi = s.kampanyaaciklamasi;
                _kampanya.baslangictarihi = s.baslangictarihi;
                _kampanya.bitistarihi = s.bitistarihi;
                _kampanya.urunId = s.urunId;
                ent.Kampanya.Add(_kampanya);
                ent.SaveChanges();
                return RedirectToAction("Kampanya", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }


        [HttpPost]
        public ActionResult KampanyaDuzenle(Kampanya kampanya, HttpPostedFileBase file)
        {
            try
            {
                var _kampanyaDuzenle = ent.Kampanya.Where(x => x.kampanyaId == kampanya.kampanyaId).FirstOrDefault();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    _kampanyaDuzenle.kampanyagorseli = memoryStream.ToArray();
                }
                _kampanyaDuzenle.kampanyaaciklamasi = kampanya.kampanyaaciklamasi;
                _kampanyaDuzenle.indirimorani = kampanya.indirimorani;
                _kampanyaDuzenle.kampanyabasligi = kampanya.kampanyabasligi;
                _kampanyaDuzenle.baslangictarihi = kampanya.baslangictarihi;
                _kampanyaDuzenle.bitistarihi = kampanya.bitistarihi;
                _kampanyaDuzenle.urunId = kampanya.urunId;
                //ent.SaveChanges();
                if(ent.SaveChanges()>0)
                    return RedirectToAction("Kampanya", "Admin");
                else
                    return RedirectToAction("Kampanya", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }

        #endregion


        #region //Kategori yönetimi

          public ActionResult KategoriEkle()
        {
            return View();
        }

          public ActionResult Kategori(string siralama,string arama, string currentFilter, int? page)
        {
            
            ViewBag.NameSortParm = String.IsNullOrEmpty(siralama) ? "kategoriadi_azalan" : "";
            
            var kategori = from s in ent.Kategori select s;
            //var kategori = ent.Kategori.ToList();

            //arama
            if (!String.IsNullOrEmpty(arama))
            {
               kategori = kategori.Where(s => s.kategoriadi.ToUpper().Contains(arama.ToUpper()));
            }

            //sıralama
            switch (siralama)
            {
                case "kategoriadi_azalan":
                    kategori = kategori.OrderByDescending(s => s.kategoriadi);
                    break;



                default:
                    kategori = kategori.OrderBy(s => s.kategoriadi);
                    break;

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(kategori.ToPagedList(pageNumber, pageSize));

            
        }


        public ActionResult KategoriDuzenle(int? KategoriID)
        {
            var _kategoriDuzenle = ent.Kategori.Where(x => x.kategoriId == KategoriID).FirstOrDefault();
            return View(_kategoriDuzenle);
        }


        [HttpPost]
        public ActionResult KategoriEkle(Kategori m, HttpPostedFileBase file)
        {
            try
            {
                Kategori _kategori = new Kategori();

                _kategori.kategoriId = m.kategoriId;
                _kategori.anakategoriId = m.anakategoriId;
                _kategori.kategoriadi = m.kategoriadi;
                //_marka.bitistarihi = s.bitistarihi;
                ent.Kategori.Add(_kategori);
                ent.SaveChanges();
                return RedirectToAction("Kategori", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }


        public ActionResult KategoriSil(int KategoriID)
        {
            try
            {
                ent.Kategori.Remove(ent.Kategori.First(d => d.kategoriId == KategoriID));
                ent.SaveChanges();
                return RedirectToAction("Kategori", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }


        [HttpPost]
        public ActionResult KategoriDuzenle(Kategori kategori, HttpPostedFileBase file)
        {
            try
            {
                var _kategoriDuzenle = ent.Kategori.Where(x => x.kategoriId == kategori.kategoriId).FirstOrDefault();

                _kategoriDuzenle.kategoriId = kategori.kategoriId;
                _kategoriDuzenle.anakategoriId = kategori.anakategoriId;
                _kategoriDuzenle.kategoriadi= kategori.kategoriadi;
                //_markaDuzenle. = marka;
                ent.SaveChanges();
                return RedirectToAction("Kategori", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        
        
        }
    


       #endregion


        #region //admin ekleme

        public ActionResult Admin(string siralama ,string arama,string currentFilter, int? page)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(siralama) ? "adminadi_azalan" : "";
            ViewBag.SurNameSortParm =siralama == "adminsoyadi_artan" ? "adminsoyadi_azalan":"adminsoyadi_artan";
            ViewBag.EmailSortParm = siralama == "email_artan" ? "email_azalan" : "email_artan";
            ViewBag.DataSortParm = siralama == "kayittarihi_artan" ? "kayittarihi_azalan" : "kayittarihi_artan";

            //var admin = ent.Uyeler.Where(i => i.rolId == 1).ToList();
            var admin = from s in ent.Uyeler.Where(i => i.rolId == 1)  select s;

            //arama
            if (!String.IsNullOrEmpty(arama))
            {
                admin = admin.Where(s => s.ad.ToUpper().Contains(arama.ToUpper()));
            }


            //sıralama
            switch (siralama)
            {
                case "adminadi_azalan":
                    admin = admin.OrderByDescending(s => s.ad);
                    break;

                case "adminsoyadi_artan":
                    admin = admin.OrderBy(s => s.soyad);
                    break;
                case "adminsoyadi_azalan":
                    admin = admin.OrderByDescending(s => s.soyad);
                    break;

                case "email_artan":
                    admin = admin.OrderBy(s => s.email);
                    break;
                case "email_azalan":
                    admin = admin.OrderByDescending(s => s.email);
                    break;

                case "kayittarihi_artan":
                    admin = admin.OrderBy(s => s.kayittarihi);
                    break;
                case "kayittarihi_azalan":
                    admin = admin.OrderByDescending(s => s.kayittarihi);
                    break;



                default:
                    admin = admin.OrderBy(s => s.ad);
                    break;

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(admin.ToPagedList(pageNumber, pageSize));
          
        }

        public ActionResult AdminEkle()
        {
            ViewBag.rolId = new SelectList(ent.Rol, "rolId", "Roladi");
            return View();
        }


        public ActionResult AdminDuzenle(int? AdminID)
        {
            var _adminDuzenle = ent.Uyeler.Where(x => x.uyeId == AdminID).FirstOrDefault();
            ViewBag.rolId = new SelectList(ent.Rol, "rolId", "Roladi");
            return View(_adminDuzenle);
        }
        
        [HttpPost]
        public ActionResult AdminEkle(Uyeler m, HttpPostedFileBase file)
        {

            try
            {
                Uyeler _admin = new Uyeler();

                string sifrelisifre = FormsAuthentication.HashPasswordForStoringInConfigFile(_admin.parola, "md5");
                _admin.parola = sifrelisifre;
                _admin.ad = m.ad;
                _admin.soyad = m.soyad;
                _admin.cinsiyet = m.cinsiyet;
                _admin.email = m.email;
                _admin.guvenliksorusu = m.guvenliksorusu;
                _admin.guvenliksorusucevap = m.guvenliksorusucevap;
                _admin.parola = m.parola;
                _admin.rolId = m.rolId;
                _admin.tc = m.tc;
                _admin.telefon = m.telefon;
                _admin.adres = m.adres;
                _admin.kayittarihi = m.kayittarihi;
               
                //_marka.bitistarihi = s.bitistarihi;
                ent.Uyeler.Add(_admin);
                ent.SaveChanges();
                return RedirectToAction("Admin", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }


        public ActionResult AdminSil(int AdminID)
        {
            try
            {
                ent.Uyeler.Remove(ent.Uyeler.First(d => d.uyeId == AdminID));
                ent.SaveChanges();
                return RedirectToAction("Admin", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }


        [HttpPost]
        public ActionResult AdminDuzenle(Uyeler admin, HttpPostedFileBase file)
        {
            try
            {
                var _adminDuzenle = ent.Uyeler.Where(x => x.uyeId == admin.uyeId).FirstOrDefault();

                _adminDuzenle.ad = admin.ad;
                _adminDuzenle.soyad = admin.soyad;
                _adminDuzenle.cinsiyet = admin.cinsiyet;
                _adminDuzenle.email = admin.email;
                _adminDuzenle.guvenliksorusu = admin.guvenliksorusu;
                _adminDuzenle.guvenliksorusucevap = admin.guvenliksorusucevap;
                _adminDuzenle.parola = admin.parola;
                _adminDuzenle.rolId = admin.rolId;
                _adminDuzenle.tc = admin.tc;
                _adminDuzenle.telefon = admin.telefon;
                _adminDuzenle.adres = admin.adres;
                _adminDuzenle.kayittarihi = admin.kayittarihi;
                //_markaDuzenle. = marka;
                ent.SaveChanges();
                return RedirectToAction("Admin", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }
        #endregion

        #region //uye yonetimi


        public ActionResult Uyeler(string siralama, string arama, string currentFilter, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(siralama) ? "uyeadi_azalan" : "";

            //var marka = ent.Marka.ToList();
            var uye = from s in ent.Uyeler select s;
            //arama
            if (!String.IsNullOrEmpty(arama))
            {
                uye = uye.Where(s => s.ad.ToUpper().Contains(arama.ToUpper()));
            }

            //sıralama
            switch (siralama)
            {
                case "markaadi_azalan":
                    uye = uye.OrderByDescending(s => s.ad);
                    break;



                default:
                    uye = uye.OrderBy(s => s.ad);
                    break;

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(uye.ToPagedList(pageNumber, pageSize));

        }


        public ActionResult UyeEkle()
        {
            return View();
        }


        public ActionResult UyeDuzenle(int? UyeID)
        {
            var _uyeDuzenle = ent.Uyeler.Where(x => x.uyeId == UyeID).FirstOrDefault();
            return View(_uyeDuzenle);
        }

       


        public ActionResult UyeSil(int UyeID)
        {
            try
            {
                ent.Uyeler.Remove(ent.Uyeler.First(d => d.uyeId == UyeID));
                ent.SaveChanges();
                return RedirectToAction("Uye", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }



        [HttpPost]
        public ActionResult UyeDuzenle(Uyeler uyeler, HttpPostedFileBase file)
        {
            try
            {
                var _uyeDuzenle = ent.Uyeler.Where(x => x.uyeId == uyeler.uyeId).FirstOrDefault();
                
                _uyeDuzenle.ad = uyeler.ad;
                _uyeDuzenle.soyad = uyeler.soyad;
                //_markaDuzenle. = marka;
                ent.SaveChanges();
                return RedirectToAction("Uyeler", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Güncellerken hata oluştu " + ex.Message);
            }

        }


        


        #endregion



        #region //kargoyonetimi

        public ActionResult Kargo(string siralama, string arama, string currentFilter, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(siralama) ? "kargoadi_azalan" : "";

            //var marka = ent.Marka.ToList();
            var kargo = from s in ent.Kargo select s;
            //arama
            if (!String.IsNullOrEmpty(arama))
            {
                kargo = kargo.Where(s => s.firmaadi.ToUpper().Contains(arama.ToUpper()));
            }

            //sıralama
            switch (siralama)
            {
                case "kargoadi_azalan":
                    kargo = kargo.OrderByDescending(s => s.firmaadi);
                    break;



                default:
                    kargo = kargo.OrderBy(s => s.firmaadi);
                    break;

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(kargo.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult KargoEkle()
        {
            return View();
        }

        public ActionResult KargoDuzenle(int?KargoID)
        {
            var _kargoDuzenle = ent.Kargo.Where(x => x.kargoId == KargoID).FirstOrDefault();
            return View(_kargoDuzenle);
        }

        public ActionResult KargoSil(int KargoID)
        {
            try
            {
                ent.Kargo.Remove(ent.Kargo.First(d => d.kargoId == KargoID));
                ent.SaveChanges();
                return RedirectToAction("Kargo", "Admin");
            }
            catch (Exception ex)
            {
                throw new Exception("Silerken hata oluştu", ex.InnerException);
            }
        }

        [HttpPost]
        public ActionResult KargoEkle(Kargo k, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Kargo _kargo = new Kargo();
                   
                    _kargo.firmaadi = k.firmaadi;
                    _kargo.firmaaciklamasi = k.firmaaciklamasi;
                    //_marka.bitistarihi = s.bitistarihi;
                    ent.Kargo.Add(_kargo);
                    ent.SaveChanges();
                    return RedirectToAction("Kargo", "Admin");
                }
                catch (Exception ex)
                {
                    throw new Exception("Eklerken hata oluştu");
                }

            }
            //doğrulama yapılmadığı takdirde ekrana aynı view getirilecek
            return View();
            
        }

        [HttpPost]
        public ActionResult KargoDuzenle(Kargo k ,HttpPostedFileBase file)
    {
        try
        {
            var _kargoduzenle=ent.Kargo.Where(x => x.kargoId == k.kargoId).FirstOrDefault();


            _kargoduzenle.firmaadi = k.firmaadi;
            _kargoduzenle.firmaaciklamasi = k.firmaaciklamasi;
            //_marka.bitistarihi = s.bitistarihi;
            ent.Kargo.Add(_kargoduzenle);
            ent.SaveChanges();
            return RedirectToAction("Kargo", "Admin");
        }
        catch (Exception ex)
        {
            throw new Exception("Eklerken hata oluştu");
        }
    }

        #endregion
    }

}
