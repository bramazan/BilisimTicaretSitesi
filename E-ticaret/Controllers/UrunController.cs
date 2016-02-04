using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_ticaret.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace E_ticaret.Controllers
{
    public class UrunController : Controller
    {
        private eticaretEntities db = new eticaretEntities();

        // GET: Urun
        public ActionResult Index(string siralama,string arama,string currentFilter,int? page)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(siralama) ? "urunadi_azalan" :"";
            ViewBag.PriceSortParm = siralama == "fiyat_artan" ? "fiyat_azalan" : "fiyat_artan";
            ViewBag.MarkaSortParm = siralama == "markaadi_artan" ? "markaadi_azalan" : "markaadi_artan";
            ViewBag.KategoriSortParm = siralama == "kategoriadi_artan" ? "kategoriadi_azalan" : "kateforiadi_artan";



            //var urun = db.Urun.Include(u => u.Kampanya1).Include(u => u.Kategori).Include(u => u.Marka).Include(u => u.Siparis1).Include(u => u.UrunOzellik).Include(u=>u.urunresim);
            var urun = from s in db.Urun select s;

            //arama
            if(!String.IsNullOrEmpty(arama))
            {
                urun = urun.Where(s => s.urunadi.ToUpper().Contains(arama.ToUpper()));
            }


            switch (siralama)
            {
                case "urunadi_azalan":
                    urun = urun.OrderByDescending(s => s.urunadi);
                    break;

                case "fiyat_artan":
                    urun = urun.OrderBy(s => s.urunfiyat);
                    break;
                case "fiyat_azalan":
                    urun = urun.OrderByDescending(s => s.urunfiyat);
                    break;

                case "markaadi_artan":
                    urun = urun.OrderBy(s => s.Marka.markaadi);
                    break;
                case "markaadi_azalan":
                    urun = urun.OrderByDescending(s => s.Marka.markaadi);
                    break;

                case "kategoriadi_artan":
                    urun = urun.OrderBy(s => s.Kategori.kategoriadi);
                    break;

                case "kategoriadi_azalan":
                    urun = urun.OrderByDescending(s => s.Kategori.kategoriadi);
                    break;

                default:
                    urun = urun.OrderBy(s => s.Kategori.kategoriadi);
                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            return View(urun.ToPagedList(pageNumber,pageSize));
        }

        // GET: Urun/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // GET: Urun/Create
        public ActionResult Create()
        {
            ViewBag.kampanyaId = new SelectList(db.Kampanya, "kampanyaId", "kampanyabasligi");
            ViewBag.kategoriId = new SelectList(db.Kategori.Where(i=>i.anakategoriId!=0), "kategoriId", "kategoriadi");
            ViewBag.markaId = new SelectList(db.Marka, "markaId", "markaadi");
            ViewBag.siparisId = new SelectList(db.Siparis, "siparisId", "siparisId");
            ViewBag.ozellikId = new SelectList(db.UrunOzellik, "urunozellikId", "ozellikadi");
            return View();
        }

        // POST: Urun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.




        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "urunId,urunadi,kategoriId,markaId,siparisId,ozellikId,kampanyaId,urunfiyat,urunresim")] Urun urun, HttpPostedFileBase file)
        //{
            
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        MemoryStream memoryStream = file.InputStream as MemoryStream;
        //        if (memoryStream == null)
        //        {
        //            memoryStream = new MemoryStream();
        //            file.InputStream.CopyTo(memoryStream);
        //        }
        //        urun.urunresim = memoryStream.ToArray();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        db.Urun.Add(urun);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.kampanyaId = new SelectList(db.Kampanya, "kampanyaId", "kampanyabasligi", urun.kampanyaId);
        //    ViewBag.kategoriId = new SelectList(db.Kategori, "kategoriId", "kategoriadi", urun.kategoriId);
        //    ViewBag.markaId = new SelectList(db.Marka, "markaId", "markaadi", urun.markaId);
        //    ViewBag.siparisId = new SelectList(db.Siparis, "siparisId", "siparisId", urun.siparisId);
        //    ViewBag.ozellikId = new SelectList(db.UrunOzellik, "urunozellikId", "ozellikadi", urun.ozellikId);
        //    return View(urun);
        //}




        [HttpPost]
        public ActionResult Create(Urun urun ,HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                
            }
            //doğrulama yapılmadığı takdirde ekrana aynı view getirilecek
            return View();
            try
            {
                Urun _urun= new Urun();
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    urun.urunresim = memoryStream.ToArray();
                }

                
                //_slide.urunadi = urun.urunadi;
                //_slide.urunfiyat = urun.urunfiyat;
                //_slide.Marka.markaadi = urun.Marka.markaadi;
               db.Urun.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Create", "Urun");
            }
            catch (Exception ex)
            {
                throw new Exception("Eklerken hata oluştu");
            }
        }








        // GET: Urun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            ViewBag.kampanyaId = new SelectList(db.Kampanya, "kampanyaId", "kampanyabasligi", urun.kampanyaId);
            ViewBag.kategoriId = new SelectList(db.Kategori, "kategoriId", "kategoriadi", urun.kategoriId);
            ViewBag.markaId = new SelectList(db.Marka, "markaId", "markaadi", urun.markaId);
            ViewBag.siparisId = new SelectList(db.Siparis, "siparisId", "siparisId", urun.siparisId);
            ViewBag.ozellikId = new SelectList(db.UrunOzellik, "urunozellikId", "ozellikadi", urun.ozellikId);
            ViewBag.urunresim = new SelectList(db.UrunResim, "urunresim", "urunresim", urun.urunresim);
            return View(urun);
        }

        // POST: Urun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "urunId,urunadi,kategoriId,markaId,siparisId,ozellikId,kampanyaId,urunfiyat,urunresim")] Urun urun)
        {
           
            ViewBag.kampanyaId = new SelectList(db.Kampanya, "kampanyaId", "kampanyabasligi", urun.kampanyaId);
            ViewBag.kategoriId = new SelectList(db.Kategori, "kategoriId", "kategoriadi", urun.kategoriId);
            ViewBag.markaId = new SelectList(db.Marka, "markaId", "markaadi", urun.markaId);
            ViewBag.siparisId = new SelectList(db.Siparis, "siparisId", "siparisId", urun.siparisId);
            ViewBag.ozellikId = new SelectList(db.UrunOzellik, "urunozellikId", "ozellikadi", urun.ozellikId);
            ViewBag.urunresim = new SelectList(db.UrunResim, "urunresim", "urunresim", urun.urunresim);
            if (ModelState.IsValid)
            {
                db.Entry(urun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(urun);
        }

        // GET: Urun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Urun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = db.Urun.Find(id);
            db.Urun.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
