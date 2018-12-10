using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaOnlineMvc.DAL;
using TiendaOnlineMvc.Models;

namespace TiendaOnlineMvc.Controllers
{
    public class CheckoutController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}