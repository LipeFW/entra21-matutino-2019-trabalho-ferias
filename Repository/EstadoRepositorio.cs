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
   public class EstadoRepositorio
    {
        
            public int Inserir(Estado estado)
            {
                SqlCommand comando = Conexao.Conectar();
                comando.CommandText = @"INSERT INTO estados(nome,sigla)
OUTPUT INSERTED.ID
VALUES (@NOME,@SIGLA)";
                comando.Parameters.AddWithValue("@NOME", estado.Nome);
                comando.Parameters.AddWithValue("@SIGLA", estado.Sigla);
               
                int id = Convert.ToInt32(comando.ExecuteScalar());
                comando.Connection.Close();
                return id;
            }


            public List<Estado> ObterTodos()
            {
                SqlCommand comando = Conexao.Conectar();
                comando.CommandText = @"SELECT 
estados.id AS 'EstadoId',
estados.nome AS 'EstadoNome',
estados.sigla AS 'EstadoSigla'
FROM estados";

                DataTable tabela = new DataTable();
                tabela.Load(comando.ExecuteReader());
                List<Estado> estados = new List<Estado>();
                foreach (DataRow linha in tabela.Rows)
                {
                    Estado estado = new Estado();
                    estado.Id = Convert.ToInt32(linha["EstadoId"]);
                    estado.Nome = linha["EstadoNome"].ToString();
                    estado.Sigla = linha["EstadoSigla"].ToString();

                    //estado.Categoria = new Categoria();
                    //estado.Categoria.Nome = linha["CategoriaNome"].ToString();
                    estados.Add(estado);
                }
                return estados;

            }
        }
}
