using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SystemaVidanta.DAL;
using SystemaVidanta.Models;

namespace SystemaVidanta.Controllers
{
    public class ArticleController : Controller
    {
        private SystemVidantaContext db = new SystemVidantaContext();
        [HttpPost]
        public JsonResult BuscarArticulos(int? id, bool json = false)
        {
            if (id == null)
            {
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return Json(article, JsonRequestBehavior.AllowGet);
        }
        // GET: Article
        public ActionResult Index()
        {
            return View(db.Article.ToList());
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View(db.Article.ToList());
            }
            else
            {
                ViewBag.Colab = name;
                return View(db.Article.Where(c => c.NombreArtículo.ToLower().Contains(name)));
            }
        }

        // GET: Article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NombreArtículo,Descripción,Marca,Modelo,NumSerie,NumInterno,FechaEntrada,FechaSalida")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Article.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Article/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NombreArtículo,Descripción,Marca,Modelo,NumSerie,NumInterno,FechaEntrada,FechaSalida")] Article article)
        {

            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Article/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Article.Find(id);
            db.Article.Remove(article);
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
        public FileResult Export()
        {
            DataTable dt = new DataTable("Articulos");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Id Artículo"),
                                           new DataColumn("Nombre del Articulo"),
                                           new DataColumn("Descripcion"),
                                           new DataColumn("Marca"),
                                           new DataColumn("Modelo"),
                                           new DataColumn("Numero de serie"),
                                           new DataColumn("Numero Interno"),
                                           new DataColumn("Fecha de entrada")
           });
            var articulos = db.Article.ToList();
            foreach (var articulo in articulos)
            {
                    dt.Rows.Add(articulo.ID,articulo.NombreArtículo,articulo.Descripción,articulo.Marca, articulo.Modelo,articulo.NumSerie,
                        articulo.NumInterno,articulo.FechaEntrada
                    );
            }
                
            

            using (XLWorkbook wb = new XLWorkbook())
            {

                var worksheet = wb.Worksheets.Add(dt);
                var rango = worksheet.Range("A1:H1"); //Seleccionamos un rango
                rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
                rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
                rango.Style.Font.FontSize = 15; //Indicamos el tamaño de la fuente
                rango.Style.Fill.BackgroundColor = XLColor.BlueGray; //Indicamos el color de background
                worksheet.Columns(1, 8).AdjustToContents();
            using (MemoryStream stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LISTA ARTICULOS  " + DateTime.Now.ToString() + ".xlsx");
            }
        }
        }
    }
}
