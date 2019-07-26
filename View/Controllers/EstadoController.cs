using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Repository;

namespace View.Controllers
{
    public class EstadoController : Controller
    {

        private EstadoRepositorio repository;

        public EstadoController()
        {
            repository = new EstadoRepositorio();
        }

        public ActionResult Index()
        {
            List<Estado> estados = repository.ObterTodos();
            ViewBag.Estados = estados;
            return View();
        }

        public ActionResult Cadastro()
        {
            EstadoRepositorio estadoRepositorio = new EstadoRepositorio();
            List<Estado> estados = estadoRepositorio.ObterTodos();

            ViewBag.Estados = estados;
            return View();
        }

        public ActionResult Store(string nome, string sigla)
        {
            Estado estado = new Estado();
            estado.Nome = nome;
            estado.Sigla = sigla;
            repository.Inserir(estado);

            return RedirectToAction("Index");
        }
    }
}