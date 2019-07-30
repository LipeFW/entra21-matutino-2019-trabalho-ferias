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

        private UsuarioRepositorio repository;

        public UsuarioController()
        {
            repository = new UsuarioRepositorio();
        }

        public ActionResult Index()
        {
            List<Usuario> usuarios = repository.ObterTodos();
            ViewBag.Usuarios = usuarios;
            return View();
        }

        public ActionResult Cadastro()
        {
            //UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            //List<Usuario> usuarios = usuarioRepositorio.ObterTodos();

            //ViewBag.Usuarios = usuarios;
            return View();
        }

        public ActionResult Store(string nome, string login, string senha)
        {
            Usuario usuario = new Usuario();
            usuario.Nome = nome;
            usuario.Login = login;
            usuario.Senha = senha;
            repository.Inserir(usuario);

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Usuario usuario = repository.ObterPeloId(id);
            ViewBag.Usuario = usuario;

            //UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            //List<Usuario> usuarios = usuarioRepositorio.ObterTodos();
            //ViewBag.Usuarios = usuarios;

            return View();

        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id, string nome, string login, string senha)
        {
            Usuario usuario = new Usuario();
            usuario.Id = id;
            usuario.Nome = nome;
            usuario.Login = login;
            usuario.Senha = senha;

            repository.Editar(usuario);
            return RedirectToAction("Index");
        }
    }
}