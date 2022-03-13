using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppFerreteria.Data;
using AppFerreteria.Models;

namespace AppFerreteria.Controllers
{
    [Authorize]
    public class AlquilersController : Controller
    {
        private AppFerreteriaContext db = new AppFerreteriaContext();

        // GET: Alquilers
        public ActionResult Index()
        {
            var alquilers = db.Alquilers.Include(a => a.Clientes).Include(a => a.Motosierras);
            return View(alquilers.ToList());
        }

        //// GET: Alquilers/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Alquiler alquiler = db.Alquilers.Find(id);
        //    if (alquiler == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(alquiler);
        //}

        // GET: Alquilers/Create
        public ActionResult Create()
        {
            ViewBag.ClientesID = new SelectList(db.Clientes, "ClientesID", "ClienteFN");
            var motosierraCombo = (from a in db.Motosierras where a.EstadoMotosierra == EstadoMotosierra.Disponible select a).ToList();
            ViewBag.MotosierrasID = new SelectList(motosierraCombo, "MotosierrasID", "CodigoMotosierra");
            return View();
        }

        // POST: Alquilers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlquilerID,FechaAlquiler,ClientesID,MotosierrasID")] Alquiler alquiler)
        {
            if (ModelState.IsValid)
            {
                db.Alquilers.Add(alquiler);
                db.SaveChanges();
                Motosierras motosierras = db.Motosierras.Find(alquiler.MotosierrasID);
                motosierras.EstadoMotosierra = EstadoMotosierra.Alquilada;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientesID = new SelectList(db.Clientes, "ClientesID", "ClienteFN", alquiler.ClientesID);
            ViewBag.MotosierrasID = new SelectList(db.Motosierras, "MotosierrasID", "CodigoMotosierra", alquiler.MotosierrasID);
            return View(alquiler);
        }

        //// GET: Alquilers/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Alquiler alquiler = db.Alquilers.Find(id);
        //    if (alquiler == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ClientesID = new SelectList(db.Clientes, "ClientesID", "NombreCliente", alquiler.ClientesID);
        //    ViewBag.MotosierrasID = new SelectList(db.Motosierras, "MotosierrasID", "CodigoMotosierra", alquiler.MotosierrasID);
        //    return View(alquiler);
        //}

        //// POST: Alquilers/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        //// más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "AlquilerID,FechaAlquiler,ClientesID,MotosierrasID")] Alquiler alquiler)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(alquiler).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ClientesID = new SelectList(db.Clientes, "ClientesID", "NombreCliente", alquiler.ClientesID);
        //    ViewBag.MotosierrasID = new SelectList(db.Motosierras, "MotosierrasID", "CodigoMotosierra", alquiler.MotosierrasID);
        //    return View(alquiler);
        //}

        //// GET: Alquilers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Alquiler alquiler = db.Alquilers.Find(id);
        //    if (alquiler == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(alquiler);
        //}

        // POST: Alquilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alquiler alquiler = db.Alquilers.Find(id);
            db.Alquilers.Remove(alquiler);
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
