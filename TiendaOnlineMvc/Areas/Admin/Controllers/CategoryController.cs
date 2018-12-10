using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TiendaOnlineMvc.DAL;
using TiendaOnlineMvc.Models;

namespace TiendaOnlineMvc.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        private TiendaOnlineMvcContext dbContexto = new TiendaOnlineMvcContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(dbContexto.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category categoria = dbContexto.Categories.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categories/Create
        public ActionResult CrearNuevo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearNuevo([Bind(Include = "Id,Name")] Category categoria)
        {
            if (ModelState.IsValid)
            {
                dbContexto.Categories.Add(categoria);
                dbContexto.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categories/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category categoria = dbContexto.Categories.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Name")] Category categoria)
        {
            if (ModelState.IsValid)
            {
                dbContexto.Entry(categoria).State = EntityState.Modified;
                dbContexto.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        /// <summary>
        /// Metodo para borrar un producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category categoria = dbContexto.Categories.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        /// <summary>
        /// Metodo para confirmar el borrado de un producto.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmacionBorrar(int id)
        {
            Category categoria = dbContexto.Categories.Find(id);
            dbContexto.Categories.Remove(categoria);
            dbContexto.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Realiza tareas definidas por la aplicación asociadas a
        /// la liberación o al restablecimiento de recursos no
        /// administrados.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContexto.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
