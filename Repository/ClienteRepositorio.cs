﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public class ClienteRepositorio
    {
        public int Inserir(Cliente cliente)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO clientes (id_cidade,nome,cpf,data_nascimento,numero,complemento,logradouro,cep)
            OUTPUT INSERTED.ID VALUES
            (@ID_CIDADE,@NOME,@CPF,@DATA_NASCIMENTO,@NUMERO,@COMPLEMENTO,@LOGRADOURO,@CEP)";
            comando.Parameters.AddWithValue("@ID_CIDADE", cliente.IdCidade);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            comando.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
            comando.Parameters.AddWithValue("@CEP", cliente.CEP);


            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }


        public List<Cliente> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT 
            clientes.id AS 'ClienteId',
            clientes.id_Cidade AS 'ClienteId_Cidade',
            clientes.nome AS 'ClienteNome',
            clientes.CPF AS 'ClienteCPF',
            clientes.data_Nascimento AS 'ClienteData_Nascimento',
            clientes.numero AS 'ClienteNumero',
            clientes.complemento AS 'ClienteComplemento',
            clientes.logradouro AS 'ClienteLogradouro',
            clientes.CEP AS 'ClienteCEP'
            FROM clientes";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<Cliente> clientes = new List<Cliente>();

            foreach (DataRow linha in tabela.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(linha["ClienteId"]);
                cliente.IdCidade = Convert.ToInt32(linha["ClienteId_Cidade"]);
                cliente.Nome = linha["ClienteNome"].ToString();
                cliente.CPF = linha["ClienteCPF"].ToString();
                cliente.DataNascimento = Convert.ToDateTime(linha["ClienteData_Nascimento"]);
                cliente.Numero = Convert.ToInt32(linha["ClienteNumero"]);
                cliente.Complemento = linha["ClienteComplemento"].ToString();
                cliente.Logradouro = linha["ClienteLogradouro"].ToString();
                cliente.CEP = linha["ClienteCEP"].ToString();


                clientes.Add(cliente);
            }

            return clientes;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"DELETE FROM clientes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Cliente ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT clientes.id AS 'ClienteId',
clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCPF',
clientes.cep AS 'ClienteCEP',
clientes.numero AS 'ClienteNumero',
clientes.complemento AS 'ClienteComplemento',
clientes.logradouro AS 'ClienteLogradouro',
clientes.data_nascimento AS 'ClienteData_Nascimento',
clientes.id_cidade AS 'ClienteId_Cidade',
cidades.nome AS 'CidadeNome'
FROM clientes 
INNER JOIN cidades ON (clientes.id_cidade = cidades.id)
WHERE clientes.id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            if (tabela.Rows.Count == 1)
            {
                DataRow linha = tabela.Rows[0];
                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(linha["ClienteId"]);
                cliente.IdCidade = Convert.ToInt32(linha["ClienteId_Cidade"]);
                cliente.Nome = linha["ClienteNome"].ToString();
                cliente.CPF = linha["ClienteCPF"].ToString();
                cliente.DataNascimento = Convert.ToDateTime(linha["ClienteData_Nascimento"]);
                cliente.Numero = Convert.ToInt32(linha["ClienteNumero"]);
                cliente.Complemento = linha["ClienteComplemento"].ToString();
                cliente.Logradouro = linha["ClienteLogradouro"].ToString();
                cliente.CEP = linha["ClienteCEP"].ToString();
                return cliente;
            }
            return null;
        }

        public bool Editar(Cliente cliente)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE cidades SET id_cidade = @ID_CIDADE,nome = @NOME, cpf = @CPF, data_nascimento = @DATA_NASCIMENTO, numero = @NUMERO, complemento = @COMPLEMENTO, logradouro = @LOGRADOURO, cep = @CEP WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID_CIDADE", cliente.IdCidade);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            comando.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
            comando.Parameters.AddWithValue("@CEP", cliente.CEP);
            comando.Parameters.AddWithValue("@ID", cliente.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
