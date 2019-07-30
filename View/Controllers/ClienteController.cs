using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Repository;

namespace View.Controllers
{
    public class ClienteController : Controller
    {

        private ClienteRepositorio repository;

        public ClienteController()
        {
            repository = new ClienteRepositorio();
        }

        public ActionResult Index()
        {
            List<Cliente> clientes = repository.ObterTodos();
            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult Cadastro()
        {
            CidadeRepositorio cidadeRepositorio = new CidadeRepositorio();
            List<Cidade> cidades = cidadeRepositorio.ObterTodos();

            ViewBag.Cidades = cidades;
            return View();
        }

        public ActionResult Store(string nome, string cpf, string cep, string complemento, DateTime nascimento, int cidade, string logradouro, int numero)
        {
            Cliente cliente = new Cliente();
            cliente.Nome = nome;
            cliente.CPF = cpf;
            cliente.CEP = cep;
            cliente.Complemento = complemento;
            cliente.DataNascimento = nascimento;
            cliente.IdCidade = cidade;
            cliente.Logradouro = logradouro;
            cliente.Numero = numero;
            repository.Inserir(cliente);

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Cliente cliente = repository.ObterPeloId(id);
            ViewBag.Cliente = cliente;

            ClienteRepositorio clienteRepositorio = new ClienteRepositorio();
            List<Cliente> clientes = clienteRepositorio.ObterTodos();
            ViewBag.Clientes = clientes;

            return View();

        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id, string nome, string cep, string cidade)
        {
            Cliente cliente = new Cliente();
            cliente.Id = id;
            cliente.Nome = nome;
            cliente.CEP = cep;
            cliente.Cidade = new Cidade();
            cliente.Cidade.Nome = cidade;

            repository.Editar(cliente);
            return RedirectToAction("Index");
        }
    }
}