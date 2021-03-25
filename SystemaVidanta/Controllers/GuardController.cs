using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SystemaVidanta.DAL;
using SystemaVidanta.Models;

namespace SystemaVidanta.Controllers
{
    public class GuardController : Controller
    {
        private SystemVidantaContext db = new SystemVidantaContext();

        // GET: Guard
        public ActionResult Index()
        {
            return View(db.Guards.ToList());
        }

        // GET: Guard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guard guard = db.Guards.Find(id);
            if (guard == null)
            {
                return HttpNotFound();
            }
            return View(guard);
        }

        // GET: Guard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guard/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumColaborador,Empresa,FolioResguardo,FechaResguardo,FechaDevolución,TipoMovimiento,TipoPrestamo,Ubicación,ObservacionesResguardo,VoBo")] Guard guard)
        {
            if (ModelState.IsValid)
            {
                db.Guards.Add(guard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guard);
        }

        // GET: Guard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guard guard = db.Guards.Find(id);
            if (guard == null)
            {
                return HttpNotFound();
            }
            return View(guard);
        }

        // POST: Guard/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NumColaborador,Empresa,FolioResguardo,FechaResguardo,FechaDevolución,TipoMovimiento,TipoPrestamo,Ubicación,ObservacionesResguardo,VoBo")] Guard guard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guard);
        }

        // GET: Guard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guard guard = db.Guards.Find(id);
            if (guard == null)
            {
                return HttpNotFound();
            }
            return View(guard);
        }

        // POST: Guard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guard guard = db.Guards.Find(id);
            db.Guards.Remove(guard);
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
