using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaOnlineMvc.DAL;
using System.Data.Entity;
using TiendaOnlineMvc.ViewModels;
using TiendaOnlineMvc.Models;
using TiendaOnlineMvc.Managers;
using TiendaOnlineMvc.Utilities;

namespace TiendaOnlineMvc.Controllers
{
    public class HomeController : Controller
    {
        private TiendaOnlineMvcContext dbContexto = new TiendaOnlineMvcContext();

        // GET: Categories
        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel
            {
                LoginViewModel = new LoginViewModel()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            if (model.Email != null)
            {

                if (ModelState.IsValid) // this is check validity
                {
                    var usuario = dbContexto.Customers.Where(a => a.Email.Equals(model.Email) && a.Password.Equals(model.Password)).FirstOrDefault();

                    if (usuario != null)
                    {
                        Session[Strings.KeyCurrentUser] = usuario;

                        //si es administrador
                        if (usuario.UserType == EUserType.Admin)
                        {
                            return RedirectToAction("Index", "Products", new { area = "Admin" });
                        }
                        //sino, es un cliente
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }

                }
                return View(model);
            }
            else
            {
                IndexViewModel modelInicio = new IndexViewModel
                {
                    LoginViewModel = model,
                    
                };
                return View(modelInicio);
            }
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove(Strings.KeyCurrentUser);
            return RedirectToAction("Index");
        }
    }
}