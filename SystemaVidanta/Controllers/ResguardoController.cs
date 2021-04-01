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
    public class ResguardoController : Controller
    {
        private SystemVidantaContext db = new SystemVidantaContext();

        // GET: Guard
        public ActionResult Index()
        {
            return View(db.Resguardos.ToList());
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(string articulo)
        {
            if (string.IsNullOrEmpty(articulo))
            {
                return View(db.Resguardos.ToList());
            }
            else
            {
                ViewBag.Colab = articulo;
                return View(db.Resguardos.Where(c => c.NumColaborador.ToLower().Contains(articulo)));
            }
        }
        // GET: Guard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resguardo resguardo = db.Resguardos.Find(id);
            if (resguardo == null)
            {
                return HttpNotFound();
            }
            return View(resguardo);
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
        //[ValidateAntiForgeryToken]
        public JsonResult Create(Resguardo resguardo)
        {
            bool estado = false;
            try
            {

                Resguardo obj = new Resguardo
                {
                    NumColaborador = resguardo.NumColaborador,
                    Empresa = resguardo.Empresa,
                    FolioResguardo = resguardo.FolioResguardo,
                    FechaResguardo = resguardo.FechaResguardo,
                    FechaDevolucion = resguardo.FechaDevolucion,
                    TipoMovimiento = resguardo.TipoMovimiento,
                    TipoPrestamo = resguardo.TipoPrestamo,
                    Ubicacion = resguardo.Ubicacion,
                    ObservacionesResguardo = resguardo.ObservacionesResguardo,
                    VoBo = resguardo.VoBo,
                 
                   
                    };


                    db.Resguardos.Add(obj);
                    db.SaveChanges();
                    var LastID = (from c in db.Resguardos orderby c.ID descending select c.ID).First();

                    foreach (ResguardoDetalle detalles in resguardo.DetallesResguardo)
                    {
                    //ResguardoDetalle test = new ResguardoDetalle { ID = 1, IdArticulo = detalles.IdArticulo };
                    //detalles.Id = 1;
                    detalles.ResguardoID = LastID;
                    db.DetallesResguardo.Add(detalles);
                    
                    }
                    db.SaveChanges();
                    estado = true;
                
            }
            catch (Exception e) 
            {
                ModelState.AddModelError("GUARD_ERROR", e.Message); return new JsonResult { Data = new { estado } };
            }
            

            return new JsonResult { Data = new { estado } };
        }

        // GET: Guard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resguardo guard = db.Resguardos.Find(id);
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
        public ActionResult Edit([Bind(Include = "ID,NumColaborador,Empresa,FolioResguardo,FechaResguardo,FechaDevolución,TipoMovimiento,TipoPrestamo,Ubicación,ObservacionesResguardo,VoBo")] Resguardo resguardo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resguardo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resguardo);
        }

        // GET: Guard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resguardo resguardo = db.Resguardos.Find(id);
            if (resguardo == null)
            {
                return HttpNotFound();
            }
            return View(resguardo);
        }

        // POST: Guard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resguardo resguardo = db.Resguardos.Find(id);
            db.Resguardos.Remove(resguardo);
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
