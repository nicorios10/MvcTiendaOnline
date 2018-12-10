using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaOnlineMvc.Areas.Admin.Models;
using TiendaOnlineMvc.DAL;
using TiendaOnlineMvc.Models;
using TiendaOnlineMvc.Utilities;

namespace TiendaOnlineMvc.Areas.Admin.Controllers
{
    public class ProductsController : AdminBaseController
    {
        private TiendaOnlineMvcContext dbContexto = new TiendaOnlineMvcContext();

        public ActionResult Index()
        {
            var productos = dbContexto.Products.Include(p => p.Category);

            return View(productos.ToList());
        }

        /// <summary>
        /// Ver detalles de los productos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = dbContexto.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        /// <summary>
        /// Crear un producto
        /// </summary>
        /// <returns></returns>
        public ActionResult Crear()
        {
            ProductViewModel model = new ProductViewModel();
            model.Categorias = new SelectList(dbContexto.Categories, "ID", "Name");
            
            return View(model);
        }

        /// <summary>
        /// Metodo para crear un producto.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(ProductViewModel model)
        {
            //verifico, si subio imagen, que la imagen sea jpg
            if (model.Imagen != null && !model.Imagen.ContentType.Equals("image/jpeg"))
            {
                ModelState.AddModelError("Imagen", "La imagen debe ser jpg.");
            }

            if (ModelState.IsValid)
            {
                string imageUri = "";
                if (model.Imagen != null && model.Imagen.ContentLength > 0)
                {
                    var uploadDir = "~/Content/Images/Products";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.Imagen.FileName);
                    model.Imagen.SaveAs(imagePath);
                    imageUri = string.Format("{0}/{1}", uploadDir, model.Imagen.FileName);
                }

                //Creo mi entidad a partir del ViewModel.
                Product producto = new Product();

                producto.Name = model.Nombre;
                producto.Price = model.Precio;
                producto.Description = model.Descripcion;
                producto.LastUpdated = model.Actualizacion;
                producto.CategoryId = model.CategoriaId;
                producto.ImageUri = imageUri;

                if (ModelState.IsValid)
                {
                    dbContexto.Products.Add(producto);
                    dbContexto.SaveChanges();
                    return RedirectToAction("Index");
                }
                model.Categorias = new SelectList(dbContexto.Categories, "ID", "Name", producto.Category);

                TempData[Strings.KeyMensajeDeAccion] = "El producto se ha crado correctamente.";
                return RedirectToAction("Index");
            }
            else
            {
                model.Categorias = new SelectList(dbContexto.Categories, "Id", "Name");
            }
 
            return View(model);
        }       
             
        /// <summary>
        /// Metodo para editar un producto.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Editar(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product producto = dbContexto.Products.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            ProductViewModel model = new ProductViewModel();
            model.Categorias = new SelectList(dbContexto.Categories, "ID", "Name", producto.CategoryId);

            producto.Id=model.Id;
            producto.Name=model.Nombre;
            producto.Price=model.Precio;
            producto.Description=model.Descripcion;

            return View(model);
        }
            

        /// <summary>
        /// Metodo para editar un producto.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Name,Price,Description,LastUpdated,CategoryId,ImageUri")] Product product)
        {
            if (ModelState.IsValid)
            {
                dbContexto.Entry(product).State = EntityState.Modified;
                dbContexto.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(dbContexto.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        /// <summary>
        /// Metodo para borrar un producto.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product producto = dbContexto.Products.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        /// <summary>
        /// Metodo para comfirmar la eliminacion de un producto.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmacionBorrar(int id)
        {
            Product producto = dbContexto.Products.Find(id);
            dbContexto.Products.Remove(producto);
            dbContexto.SaveChanges();
            return RedirectToAction("Index");
        }
        

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
