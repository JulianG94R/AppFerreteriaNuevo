using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppFerreteria.Data;
using AppFerreteria.Models;

namespace AppFerreteria.Controllers
{
    [Authorize]
    public class MotosierrasController : Controller
    {
        private AppFerreteriaContext db = new AppFerreteriaContext();

        // GET: Motosierras
        public ActionResult Index()
        {
            return View(db.Motosierras.ToList());
        }

     
        // GET: Motosierras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Motosierras/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MotosierrasID,CodigoMotosierra,PrecioAlquiler,CodDeFabrica,EstadoMotosierra")] Motosierras motosierras, HttpPostedFileBase Imagen)
        {
            if (Imagen != null && Imagen.ContentLength > 0)
            {
                byte[] imagenData = null;
                using (var binaryCampo = new BinaryReader(Imagen.InputStream))
                {
                    imagenData = binaryCampo.ReadBytes(Imagen.ContentLength);
                }
                motosierras.ImagenMoto = imagenData;



                if (ModelState.IsValid)
                {
                    db.Motosierras.Add(motosierras);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            else
            {
                ModelState.AddModelError("ImagenMoto", "Imagen requerida");
            }

            return View(motosierras);
        }

        // GET: Motosierras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motosierras motosierras = db.Motosierras.Find(id);
            if (motosierras == null)
            {
                return HttpNotFound();
            }
            return View(motosierras);
        }

        // POST: Motosierras/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MotosierrasID,CodigoMotosierra,PrecioAlquiler,CodDeFabrica,EstadoMotosierra")] Motosierras motosierras, HttpPostedFileBase Imagen)
        {
            if (ModelState.IsValid)
            {
                var imgActual = (from a in db.Motosierras where a.MotosierrasID == motosierras.MotosierrasID select a.ImagenMoto).Single();

                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    byte[] imagenData = null;
                    using (var binaryCampo = new BinaryReader(Imagen.InputStream))
                    {
                        imagenData = binaryCampo.ReadBytes(Imagen.ContentLength);
                    }
                    motosierras.ImagenMoto = imagenData;

                }

                else
                {
                    motosierras.ImagenMoto = imgActual;
                }

                db.Entry(motosierras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(motosierras);
        }

       
        // POST: Motosierras/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Motosierras motosierras = db.Motosierras.Find(id);
            db.Motosierras.Remove(motosierras);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult convertirImagen(int MotosierrasID)
        {
            var motosierras = db.Motosierras.Where(x => x.MotosierrasID == MotosierrasID).Single();
            if (motosierras.ImagenMoto !=null)
            {
                var imagenCampo = db.Motosierras.Where(x => x.MotosierrasID == MotosierrasID).FirstOrDefault();
                return File(imagenCampo.ImagenMoto, "image/jpeg");
            }
            return View();
        }


        [AllowAnonymous]

        public JsonResult BuscarMoto()
        {
            List<Motosierras> listadoMotosierrasDisponibles = new List<Motosierras>();
            var motosierras = (from a in db.Motosierras where a.EstadoMotosierra == EstadoMotosierra.Disponible select a).ToList();

            foreach (var item in motosierras)
            {
                var motoBuscar = new Motosierras
                {
                    CodigoMotosierra = item.CodigoMotosierra,
                    CodDeFabrica = item.CodDeFabrica,
                    PrecioAlquiler = item.PrecioAlquiler,
                    MotoImagenPrincipalString = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(item.ImagenMoto))

                };
                listadoMotosierrasDisponibles.Add(motoBuscar);

            }
            return Json(listadoMotosierrasDisponibles, JsonRequestBehavior.AllowGet);

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
