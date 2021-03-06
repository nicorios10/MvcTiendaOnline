﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TiendaOnlineMvc.Utilities;

namespace TiendaOnlineMvc.Controllers
{
    public class UsuarioBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            //reviso la sesion.. Si es nueva, o no existe el usuario cargado en sesion, o el usuario no es Cliente, se lo expulsa.
            if (session.IsNewSession || Session[Strings.KeyCurrentUser] == null || ((Models.Customer)Session[Strings.KeyCurrentUser]).UserType != Models.EUserType.Guest)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Unauthorized, Strings.UIMessageUnauthorized);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "Controller", "Home" },
                        { "Action", "Index" },
                        { "Area", "" }
                    });
                }
            }
        }
    }
}