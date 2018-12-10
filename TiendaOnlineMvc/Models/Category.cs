using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaOnlineMvc.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "El nombre de la categoria es requerido")]
        [MaxLength(45,ErrorMessage = "El maximo de caractere de categoria deve ser 45")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } 
    }
}