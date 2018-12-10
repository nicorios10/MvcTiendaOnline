using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace TiendaOnlineMvc.Areas.Admin.Models
{
    public class ProductViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public Decimal Precio { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Actualizacion { get; set; }

        [Display(Name = "Category")]
        public int CategoriaId { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set; }

        [Display(Name = "Imagen JPG")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Imagen { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImagenUri { get; set; }
    }
}