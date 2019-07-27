using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CidadeRepositorio
    {
        public int Inserir(Cidade cidade)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO cidades
(id_Estado,nome,numero_habitantes)
OUTPUT INSERTED.ID
VALUES (@ID_ESTADO,@NOME,@NUMERO_HABITANTES)";
            comando.Parameters.AddWithValue("@ID_ESTADO", cidade.IdEstado);
            comando.Parameters.AddWithValue("@NOME", cidade.Nome);
            comando.Parameters.AddWithValue("@NUMERO_HABITANTES", cidade.NumeroHabitantes);


            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }


        public List<Cidade> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT 
cidades.id AS 'CidadeId',
cidades.id_Estado AS 'CidadeId_Estado',
cidades.Numero_Habitantes AS 'CidadeNumero_Habitantes',
cidades.Nome AS 'CidadeNome'
FROM cidades";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            List<Cidade> cidades = new List<Cidade>();
            foreach (DataRow linha in tabela.Rows)
            {
                Cidade cidade = new Cidade();
                cidade.Id = Convert.ToInt32(linha["CidadeId"]);
                cidade.IdEstado = Convert.ToInt32(linha["CidadeId_Estado"]);
                cidade.NumeroHabitantes = Convert.ToInt32(linha["CidadeNumero_Habitantes"]);
                cidade.Nome = linha["CidadeNome"].ToString();


                cidades.Add(cidade);
            }
            return cidades;



        }
    }
}
