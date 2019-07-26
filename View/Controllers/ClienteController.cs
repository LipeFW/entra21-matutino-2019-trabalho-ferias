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
            ClienteRepositorio clienteRepositorio = new ClienteRepositorio();
            List<Cliente> clientes = clienteRepositorio.ObterTodos();

            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult Store(string nome, string cpf, string cep, string complemento, DateTime nascimento, int cidade, string logradouro, int numero)
        {
            Cliente cliente = new Cliente();
            cliente.Nome = nome;
            cliente.CPF = cpf;
            cliente.CEP = cep;
            cliente.Complemento = complemento;
            cliente.Data_Nascimento = nascimento;
            cliente.Id_Cidade = cidade;
            cliente.Logradouro = logradouro;
            cliente.Numero = numero;
            repository.Inserir(cliente);

            return RedirectToAction("Index");
        }
    }
}