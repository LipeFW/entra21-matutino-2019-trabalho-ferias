using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Repository;

namespace View.Controllers
{
    public class ProjetoController : Controller
    {

        private ProjetoRepositorio repository;

        public ProjetoController()
        {
            repository = new ProjetoRepositorio();
        }

        public ActionResult Index()
        {
            List<Projeto> projetos = repository.ObterTodos();
            ViewBag.Projetos = projetos;
            return View();
        }

        public ActionResult Cadastro()
        {
            ProjetoRepositorio projetoRepositorio = new ProjetoRepositorio();
            List<Projeto> projetos = projetoRepositorio.ObterTodos();

            ViewBag.Projetos = projetos;
            return View();
        }

        public ActionResult Store(int cliente, string nome, DateTime criacao, DateTime finalizacao)
        {
            Projeto projeto = new Projeto();
            projeto.Nome = nome;
            projeto.DataCriacao = criacao;
            projeto.DataFinalizacao = finalizacao;  
            projeto.IdCliente = cliente;
            repository.Inserir(projeto);

            return RedirectToAction("Index");
        }
    }
}