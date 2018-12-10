using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaOnlineMvc.Models
{
    [Bind(Exclude = "Id")]
    public class CustomerOrder
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El primer nombre es requerido")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El segundo nombre es requerido")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "La direccion es requerida")]
        [StringLength(70)]
        public string Address { get; set; }
        [Required(ErrorMessage = "La ciudad es requerida")]
        [StringLength(40)]
        public string City { get; set; }
        [Required(ErrorMessage = "El estado es requerido")]
        [StringLength(40)]
        public string State { get; set; }
        [Required(ErrorMessage = "El codigo postal es requerido")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "El pais es requerido")]
        [StringLength(40)]
        public string Country { get; set; }
        [Required(ErrorMessage = "El telefono es requerido")]
        [StringLength(24)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "El e-mail es requerido")]
        [DisplayName("Email")]

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "El e-mail no es valido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
     
        [ScaffoldColumn(false)]
        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }

        [ScaffoldColumn(false)]
        public Decimal Amount { get; set; }

        [ScaffoldColumn(false)]
        public string CustomerUserName { get; set; }

    }
}