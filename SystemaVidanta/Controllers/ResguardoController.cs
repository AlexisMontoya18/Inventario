using ClosedXML.Excel;
using Rotativa;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.IO;
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
            var LastID = (from c in db.Resguardos orderby c.ID descending select c.ID).First();
            ViewBag.Folio = LastID;
            return View();
        }

        // POST: Guard/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(Resguardo resguardo,string imagen,string imagenUS, string encodedImage)
        {
            var LastID = 0;
            bool estado = false;
            try
            {

                Resguardo obj = new Resguardo
                {
                    NumColaborador = resguardo.NumColaborador,
                    Nombre=resguardo.Nombre,
                    Puesto=resguardo.Puesto,
                    Empresa = resguardo.Empresa,
                    FolioResguardo = resguardo.FolioResguardo,
                    FechaResguardo = resguardo.FechaResguardo,
                    FechaDevolucion = resguardo.FechaDevolucion,
                    TipoMovimiento = resguardo.TipoMovimiento,
                    TipoPrestamo = resguardo.TipoPrestamo,
                    Ubicacion = resguardo.Ubicacion,
                    UsuarioRecibe= resguardo.UsuarioRecibe,
                    ObservacionesResguardo = resguardo.ObservacionesResguardo,
                    imagen= encodedImage,
                    VoBo = resguardo.VoBo,
                    firmaColaborador = imagen,
                    firmaUsuario= imagenUS
                    
                };

               
                db.Resguardos.Add(obj);
                db.SaveChanges();
                LastID = (from c in db.Resguardos orderby c.ID descending select c.ID).First();

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


            return new JsonResult { Data = new { estado, id = LastID } };
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
        public ActionResult Edit([Bind(Include = "ID,NumColaborador,Empresa,FolioResguardo,FechaResguardo,FechaDevolucion,TipoMovimiento,TipoPrestamo,Ubicacion,ObservacionesResguardo,VoBo")] Resguardo resguardo)
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
        //metodo para hacer el pdf
        public ActionResult PdfResguardo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resguardo resguardoDetalle = db.Resguardos.Find(id);
            var nombre = db.Users.Where(c => c.ID== resguardoDetalle.UsuarioRecibe).FirstOrDefault();
            ViewBag.Imagen2 = resguardoDetalle.firmaUsuario;
            ViewBag.Imagen = resguardoDetalle.firmaColaborador;
            ViewBag.Imagen3 = resguardoDetalle.imagen;
            ViewBag.Nombre = nombre;
            if (resguardoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(resguardoDetalle);
        }
        public ActionResult PrintPDF(int? id)
        {
            ViewBag.Print = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resguardo resguardoDetalle1 = db.Resguardos.Find(id);
            if (resguardoDetalle1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.Imagen2 = resguardoDetalle1.firmaUsuario;
            ViewBag.Imagen = resguardoDetalle1.firmaColaborador;
            ViewBag.Imagen3 = resguardoDetalle1.imagen;
            Resguardo obj = resguardoDetalle1;
            obj.firmaUsuario = null;
            obj.firmaColaborador = null;
            obj.imagen = null;
            return new Rotativa.ActionAsPdf("PdfResguardo", obj)
            {
                PageSize = Size.A4,
                PageOrientation= Orientation.Landscape,
            };
        }
        //aqui termina 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public FileResult Export()
        {
            DataTable dt = new DataTable("Resguardos");
            dt.Columns.AddRange(new DataColumn[11] { new DataColumn("Numero Colaborador"),
                                           new DataColumn("Nombre"),
                                           new DataColumn("Puesto"),
                                           new DataColumn("Empresa"),
                                           new DataColumn("Folio Resguardo"),
                                           new DataColumn("Fecha Resguardo"),
                                           new DataColumn("Fecha Devolucion"),
                                           new DataColumn("Tipo Movimiento"),
                                           new DataColumn("Tipo Prestamo"),
                                           new DataColumn("Ubicacion"),
                                           new DataColumn("Vo.Bo")
           });
            var Resguardos = db.Resguardos.ToList();
            foreach (var Resguardo in Resguardos)
            {
                dt.Rows.Add(Resguardo.NumColaborador,Resguardo.Nombre,Resguardo.Puesto,Resguardo.Empresa,Resguardo.FolioResguardo,Resguardo.FechaResguardo,
                    Resguardo.FechaResguardo,Resguardo.TipoMovimiento,Resguardo.TipoMovimiento,Resguardo.Ubicacion,
                    Resguardo.VoBo
                );
            }



            using (XLWorkbook wb = new XLWorkbook())
            {

                var worksheet = wb.Worksheets.Add(dt);
                var rango = worksheet.Range("A1:K1"); //Seleccionamos un rango
                rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
                rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
                rango.Style.Font.FontSize = 15; //Indicamos el tamaño de la fuente
                rango.Style.Fill.BackgroundColor = XLColor.BlueGray; //Indicamos el color de background
                worksheet.Columns(1, 11).AdjustToContents();
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LISTA RESGUARDOS  " + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }
    }
}
