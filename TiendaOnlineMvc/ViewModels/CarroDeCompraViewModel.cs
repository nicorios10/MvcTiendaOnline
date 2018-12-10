using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiendaOnlineMvc.Models;

namespace TiendaOnlineMvc.ViewModels
{
    public class CarroDeCompraViewModel
    {
        public List<Cart> ElementosDelCarro { get; set; }
        public decimal TotalDelCarro { get; set; }
    }
}