using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using Model;

namespace View.Controllers
{
    public class TarefaController : Controller
    {

        private TarefaRepositorio repository;

        public TarefaController()
        {
            repository = new TarefaRepositorio();
        }

        public ActionResult Index()
        {
            List<Tarefa> tarefas = repository.ObterTodos();
            ViewBag.Tarefas = tarefas;
            return View();
        }

        public ActionResult Cadastro()
        {
            TarefaRepositorio tarefaRepositorio = new TarefaRepositorio();
            List<Tarefa> tarefas = tarefaRepositorio.ObterTodos();

            ViewBag.Tarefas = tarefas;
            return View();
        }

        public ActionResult Store(string titulo, int responsavel, int categoria, int projeto)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.Titulo = titulo;
            tarefa.IdUsuarioResponsavel = responsavel;
            tarefa.IdCategoria = categoria;
            tarefa.IdProjeto = projeto;
            repository.Inserir(tarefa);

            return RedirectToAction("Index");
        }
    }
}