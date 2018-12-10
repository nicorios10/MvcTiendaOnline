
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaOnlineMvc.DAL;

namespace TiendaOnlineMvc.Models
{
    public partial class ShoppingCart
    {
        TiendaOnlineMvcContext dbContexto = new TiendaOnlineMvcContext();

        public string ShoppingCartId { get; set; }

        public const string SesionCarroKey = "cartId";

        //HttpContextBase:Sirve como clase base para las clases
        //que contienen información específica de HTTP sobre una
        //solicitud HTTP individual.
        public static ShoppingCart ObtenerCarrito(HttpContextBase contexto)
        {
            var carro = new ShoppingCart();

            carro.ShoppingCartId = carro.ObtenerIdCarro(contexto);

            return carro;
        }

        //Método auxiliar para simplificar las llamadas de carrito de compras
        public static ShoppingCart ObtenerCarrito(Controller controller)
        {
            return ObtenerCarrito(controller.HttpContext);
        }

        public void AgregarAlCarro(Product producto)
        {
            // Obtiene las instancias de carrito y producto que machean
            var articuloDeCarrito = dbContexto.Carts.SingleOrDefault(c=>c.CartId == ShoppingCartId && c.ProductId == producto.Id);

            if (articuloDeCarrito == null)
            {
                // Crea un nuevo artículo de carrito si no existe ningún artículo de carrito.
                articuloDeCarrito = new Cart
                {
                    ProductId = producto.Id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                dbContexto.Carts.Add(articuloDeCarrito);
            }
            else
            {
                // Si un articulo ya existe en el carrito le agrega uno a la cantidad
                articuloDeCarrito.Count++;
            }

            dbContexto.SaveChanges();
        }

        public int RemoverDelCarro(int id)
        {
            // Obtiene el carrito
            var articuloDeCarrito = dbContexto.Carts.SingleOrDefault(cart => cart.CartId == ShoppingCartId && cart.ProductId == id);

            int recuentoDeProductos = 0;

            if (articuloDeCarrito != null)
            {
                if (articuloDeCarrito.Count > 1)
                {
                    articuloDeCarrito.Count--;
                    recuentoDeProductos = articuloDeCarrito.Count;
                }
                else
                {
                    dbContexto.Carts.Remove(articuloDeCarrito);
                }

                dbContexto.SaveChanges();
            }
            return recuentoDeProductos;
        }

        public void CarroVacio()
        {
            var articulosDelCarro = dbContexto.Carts.Where(carro => carro.CartId == ShoppingCartId);

            foreach (var articuloDeCarrito in articulosDelCarro)
            {
                dbContexto.Carts.Remove(articuloDeCarrito);
            }
            dbContexto.SaveChanges();
        }

        public List<Cart> ObtenerElementosDelCarro()
        {
            return dbContexto.Carts.Where(carro => carro.CartId == ShoppingCartId).ToList();
        }

        public int ObtenerConteo()
        {
            // Obtiene el recuento de cada artículo en el carro y los compara
            int? conteo =
                (from cartItems in dbContexto.Carts where cartItems.CartId == ShoppingCartId select (int?) cartItems.Count).Sum();
            // Devuelve 0 si todas las entradas son nulas.
            return conteo ?? 0;
        }

        public decimal ObtenerTotal()
        {
            // Multiplica el precio del producto por el conteo de ese producto para
            //obtener el precio actual de cada uno de esos productos en el carrito,suma
            // todos los totales del precio del producto para obtener el total del carrito
            decimal? total = (from cartItems in dbContexto.Carts
                where cartItems.CartId == ShoppingCartId
                select (int?) cartItems.Count*cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CrearOrden(CustomerOrder pedidoDelCliente)
        {
            decimal totalDelPedido = 0;

            var articulosDelCarro = ObtenerElementosDelCarro();

            //itera sobre los artículos en el carro, agregando los detalles del pedido para cada uno
            foreach (var articulo in articulosDelCarro)
            {
                var productoPedido = new OrderedProduct
                {
                    ProductId = articulo.ProductId,
                    CustomerOrderId = pedidoDelCliente.Id,
                    Quantity = articulo.Count
                };

                totalDelPedido += (articulo.Count*articulo.Product.Price);

                dbContexto.Orderedproducts.Add(productoPedido);
            }

            pedidoDelCliente.Amount = totalDelPedido;

            dbContexto.SaveChanges();

            CarroVacio();

            return pedidoDelCliente.Id;
        }

        public string ObtenerIdCarro(HttpContextBase contexto)
        {
            if (contexto.Session[SesionCarroKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(contexto.User.Identity.Name))
                {
                    contexto.Session[SesionCarroKey] = contexto.User.Identity.Name;
                }

                else
                {
                    Guid idTemporalDelCarro = Guid.NewGuid();
                    contexto.Session[SesionCarroKey] = idTemporalDelCarro.ToString();
                }
            }

            return contexto.Session[SesionCarroKey].ToString();
        }

        public void MigrarElCarro(string nombreDeUsuario)
        {
            var carroDeCompras = dbContexto.Carts.Where(c => c.CartId == ShoppingCartId);
            foreach (Cart elemento in carroDeCompras)
            {
                elemento.CartId = nombreDeUsuario;
            }

            dbContexto.SaveChanges();
        }

    }
}