using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaOnlineMvc.ViewModels
{
    public class EliminarDelCarroViewModel
    {
        public string Mensaje { get; set; }
        public decimal TotalDelCarro { get; set; }
        public int ConteoDelCarro { get; set; }
        public int ConteoDeElementos { get; set; }
        public int BorrarId { get; set; }
    }
}