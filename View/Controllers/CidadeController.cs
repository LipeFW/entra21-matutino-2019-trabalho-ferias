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

        public ActionResult Editar(int id)
        {
            Cidade cidade = repository.ObterPeloId(id);
            ViewBag.Cidade = cidade;

            CidadeRepositorio cidadeRepositorio = new CidadeRepositorio();
            List<Cidade> cidades = cidadeRepositorio.ObterTodos();
            ViewBag.Cidades = cidades;

            return View();

        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id, string nome, int habitantes, string estado)
        {
            Cidade cidade = new Cidade();
            cidade.Id = id;
            cidade.Nome = nome;
            cidade.NumeroHabitantes = habitantes;
            cidade.Estado.Nome = estado;

            repository.Editar(cidade);
            return RedirectToAction("Index");
        }

    }
}