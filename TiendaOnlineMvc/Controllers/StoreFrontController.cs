using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaOnlineMvc.DAL;
using TiendaOnlineMvc.Models;

namespace TiendaOnlineMvc.Controllers
{
    public class StoreFrontController : Controller
    {
        private TiendaOnlineMvcContext dbContexto = new TiendaOnlineMvcContext();
        
        /// <summary>
        /// StoreFront: Retorna los productos por categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {
            //si no se pasa ningun parametro
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category categoria = dbContexto.Categories.Find(id);
            //si el parametro que se paso no existe
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }
    }
}