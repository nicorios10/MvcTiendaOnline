using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace TiendaOnlineMvc.Models
{
    public enum EUserType
    {
        Admin=0,
        Guest=1
    }

    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "El e-mail no es valido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraceña es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme la nueva contraceña")]
        [Compare("Password", ErrorMessage = "Las contraceñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        public EUserType UserType { get; set; }


    }
}