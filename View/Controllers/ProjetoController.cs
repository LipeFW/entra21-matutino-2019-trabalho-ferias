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

        public ActionResult Store(string nome, DateTime criacao, DateTime finalizacao, int cliente)
        {
            Projeto projeto = new Projeto();
            projeto.Nome = nome;
            projeto.Data_Criacao = criacao;
            projeto.Data_Finalizacao = finalizacao;  
            projeto.Id_Cliente = cliente;
            repository.Inserir(projeto);

            return RedirectToAction("Index");
        }
    }
}