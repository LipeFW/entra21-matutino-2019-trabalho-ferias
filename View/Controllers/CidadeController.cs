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
            EstadoRepositorio estadoRepositorio = new EstadoRepositorio();
            List<Estado> estados = estadoRepositorio.ObterTodos();

            ViewBag.Estados = estados;
            return View();
        }

        public ActionResult Store(string nome, int habitantes, int estado)
        {
            Cidade cidade = new Cidade();
            cidade.Nome = nome;
            cidade.NumeroHabitantes = habitantes;
            cidade.IdEstado = estado;
            repository.Inserir(cidade);

            return RedirectToAction("Index");
        }
    }
}