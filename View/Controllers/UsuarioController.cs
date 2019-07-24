using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            List<Usuario> usuarios = repository.ObterTodos();
            ViewBag.Usuarios = usuarios;
            return View();
        }
    }
}