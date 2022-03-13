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
    public class DevolucionsController : Controller
    {
        private AppFerreteriaContext db = new AppFerreteriaContext();

        // GET: Devolucions
        public ActionResult Index()
        {
            var devolucions = db.Devolucions.Include(d => d.Clientes).Include(d => d.Motosierras);
            return View(devolucions.ToList());
        }

        //// GET: Devolucions/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Devolucion devolucion = db.Devolucions.Find(id);
        //    if (devolucion == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(devolucion);
        //}

        // GET: Devolucions/Create
        public ActionResult Create()
        {
            ViewBag.ClientesID = new SelectList(db.Clientes, "ClientesID", "ClienteFN");
            var motosierrasCombo = (from a in db.Motosierras where a.EstadoMotosierra == EstadoMotosierra.Alquilada select a).ToList();
            ViewBag.MotosierrasID = new SelectList(motosierrasCombo, "MotosierrasID", "CodigoMotosierra");
            return View();
        }

        // POST: Devolucions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DevolucionID,FechaDevolucion,ClientesID,MotosierrasID")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                db.Devolucions.Add(devolucion);
                db.SaveChanges();
                Motosierras motosierras = db.Motosierras.Find(devolucion.MotosierrasID);
                motosierras.EstadoMotosierra = EstadoMotosierra.Disponible;
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }

            ViewBag.ClientesID = new SelectList(db.Clientes, "ClientesID", "ClienteFN", devolucion.ClientesID);
            ViewBag.MotosierrasID = new SelectList(db.Motosierras, "MotosierrasID", "CodigoMotosierra", devolucion.MotosierrasID);
            return View(devolucion);
        }

        //// GET: Devolucions/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Devolucion devolucion = db.Devolucions.Find(id);
        //    if (devolucion == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ClientesID = new SelectList(db.Clientes, "ClientesID", "NombreCliente", devolucion.ClientesID);
        //    ViewBag.MotosierrasID = new SelectList(db.Motosierras, "MotosierrasID", "CodigoMotosierra", devolucion.MotosierrasID);
        //    return View(devolucion);
        //}

        //// POST: Devolucions/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        //// más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "DevolucionID,FechaDevolucion,ClientesID,MotosierrasID")] Devolucion devolucion)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(devolucion).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ClientesID = new SelectList(db.Clientes, "ClientesID", "NombreCliente", devolucion.ClientesID);
        //    ViewBag.MotosierrasID = new SelectList(db.Motosierras, "MotosierrasID", "CodigoMotosierra", devolucion.MotosierrasID);
        //    return View(devolucion);
        //}

        //// GET: Devolucions/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Devolucion devolucion = db.Devolucions.Find(id);
        //    if (devolucion == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(devolucion);
        //}

        // POST: Devolucions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Devolucion devolucion = db.Devolucions.Find(id);
            db.Devolucions.Remove(devolucion);
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
