using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaOnlineMvc.DAL;
using TiendaOnlineMvc.Models;
using TiendaOnlineMvc.ViewModels;

namespace TiendaOnlineMvc.Controllers
{
    public class ShoppingCartController : Controller
    {
        private TiendaOnlineMvcContext dbContexto = new TiendaOnlineMvcContext();

        public ActionResult Index()
        {
            var carro = ShoppingCart.ObtenerCarrito(this.HttpContext);

            // Configuramos el ViewModel
            var viewModelCarro = new CarroDeCompraViewModel
            {
                ElementosDelCarro = carro.ObtenerElementosDelCarro(),
                TotalDelCarro = carro.ObtenerTotal()
            };
            // Devuelve la vista
            return View(viewModelCarro);
        }

        public ActionResult AgregarAlCarro(int id)
        {
            // Recupera el producto de la base de datos
            var productoAgregado = dbContexto.Products.Single(producto => producto.Id == id);

            // lo agrega al carro de compras
            var carro = ShoppingCart.ObtenerCarrito(this.HttpContext);

            carro.AgregarAlCarro(productoAgregado);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoverDelCarro(int id)
        {
            // Elimina el artículo del carrito
            var carro = ShoppingCart.ObtenerCarrito(this.HttpContext);

            // Obtiene el nombre del producto para mostrar la confirmación
            string nombreDelProducto = dbContexto.Carts.FirstOrDefault(producto => producto.ProductId == id).Product.Name;

            // Eliminar del carrito
            int recuentoDeProductos = carro.RemoverDelCarro(id);

            // Mostrar el mensaje de confirmación
            var resultados = new EliminarDelCarroViewModel
            {
                Mensaje = "El producto " + Server.HtmlEncode(nombreDelProducto) + " Fue removido del carro.",
                TotalDelCarro = carro.ObtenerTotal(),
                ConteoDelCarro = carro.ObtenerConteo(),
                ConteoDeElementos = recuentoDeProductos,
                BorrarId = id
            };

            return Json(resultados);
        }

        [ChildActionOnly]
        public ActionResult ResumenDeLaCompra()
        {
            var carro = ShoppingCart.ObtenerCarrito(this.HttpContext);

            ViewData["CartCount"] = carro.ObtenerConteo();
            return PartialView("_CartSummary");
        }

    }
}
