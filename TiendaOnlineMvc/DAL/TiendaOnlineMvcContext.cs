using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TiendaOnlineMvc.Models;

namespace TiendaOnlineMvc.DAL
{
    public class TiendaOnlineMvcContext : DbContext

    {
        //Constructor, indico la coneccion string
        public TiendaOnlineMvcContext() : base("DefaultConnection")
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }

        public DbSet<OrderedProduct> Orderedproducts { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    }
}
