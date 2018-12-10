using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TiendaOnlineMvc.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [MaxLength(45, ErrorMessage = "El numero maximo de caracteres es 45")]
        public string Name { get; set; }

        public Decimal Price { get;set; }


        public string Description { get; set; }

        [Display(Name = "Updated At")]
        [Column(TypeName = "datetime2")]
        public DateTime LastUpdated { get; set; }

        public int CategoryId { get; set; }

        public string ImageUri { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }

    }    
}