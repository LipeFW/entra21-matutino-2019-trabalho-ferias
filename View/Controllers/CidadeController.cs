using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Repository;

namespace View.Controllers
{
    public class CidadeController : Controller
    {

        private CidadeRepositorio repository;

        public CidadeController()
        {
            repository = new CidadeRepositorio();
        }

        public ActionResult Index()
        {
            List<Cidade> cidades = repository.ObterTodos();
            ViewBag.Cidades = cidades;
            return View();
        }

        public ActionResult Cadastro()
        {
            CidadeRepositorio cidadeRepositorio = new CidadeRepositorio();
            List<Cidade> cidades = cidadeRepositorio.ObterTodos();

            ViewBag.Cidades = cidades;
            return View();
        }

        public ActionResult Store(string nome, int habitantes, int estado)
        {
            Cidade cidade = new Cidade();
            cidade.Nome = nome;
            cidade.Numero_Habitantes = habitantes;
            cidade.Id_Estado = estado;
            repository.Inserir(cidade);

            return RedirectToAction("Index");
        }
    }
}