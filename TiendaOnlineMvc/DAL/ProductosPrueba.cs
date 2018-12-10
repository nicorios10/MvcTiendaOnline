using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiendaOnlineMvc.Models;

namespace TiendaOnlineMvc.DAL
{
    public class ProductoPrueba
    {
        public static List<Category> GetCategory()
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Id=1,
                    Name="Muebles"
                },
                new Category()
                {
                    Id=2,
                    Name="Tecnologia"
                }
            };
            return categories;
        }

        public static List<Product> GetProducts(TiendaOnlineMvcContext context)
        {
            List<Product> products = new List<Product>()
            {
                new Product
                {
                    Id=1,
                    Name="Mesa",
                    Description="De calidad",
                    CategoryId= context.Categories.Find(1).Id 
                },
                new Product
                {
                    Id=2,
                    Name="Sillaa",
                    Description="Para sentarse",
                    CategoryId= context.Categories.Find(1).Id
                },
                new Product
                {
                    Id=3,
                    Name="TV",
                    Description="Para ver",
                    CategoryId= context.Categories.Find(1).Id
                },
            };
            return products;
        }
    }
}