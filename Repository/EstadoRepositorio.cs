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
            comando.CommandText = @"INSERT INTO estados
            (nome,sigla)
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
            comando.Connection.Close();

            List<Estado> estados = new List<Estado>();
            foreach (DataRow linha in tabela.Rows)
            {
                Estado estado = new Estado();
                estado.Id = Convert.ToInt32(linha["EstadoId"]);
                estado.Nome = linha["EstadoNome"].ToString();
                estado.Sigla = linha["EstadoSigla"].ToString();

                estados.Add(estado);
            }
            return estados;

        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"DELETE FROM estados WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Estado ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT
            estados.id AS 'EstadoId',
            estados.nome AS 'EstadoNome',
            estados.sigla AS 'EstadoSigla'
            FROM estados WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            if (tabela.Rows.Count == 1)
            {
                DataRow linha = tabela.Rows[0];
                Estado estado = new Estado();
                estado.Id = Convert.ToInt32(linha["EstadoId"]);
                estado.Nome = linha["EstadoNome"].ToString();
                estado.Sigla = linha["EstadoSigla"].ToString();
                return estado;
            }
            return null;
        }

        public bool Editar(Estado estado)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE estados SET
            nome = @NOME,
            sigla = @SIGLA
            WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", estado.Nome);
            comando.Parameters.AddWithValue("@ID", estado.Id);
            comando.Parameters.AddWithValue("@SIGLA", estado.Sigla);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
