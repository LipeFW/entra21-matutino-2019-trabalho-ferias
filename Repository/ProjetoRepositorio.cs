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

    public class ProjetoRepositorio
    {
        public int Inserir(Projeto projeto)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO projetos
            (id_cliente, nome, data_criacao, data_finalizacao)
            OUTPUT INSERTED.ID VALUES
            (@ID_CLIENTE, @NOME, @DATA_CRIACAO, @DATA_FINALIZACAO)";
            comando.Parameters.AddWithValue("@ID_CLIENTE", projeto.IdCliente);
            comando.Parameters.AddWithValue("@NOME", projeto.Nome);
            comando.Parameters.AddWithValue("@DATA_CRIACAO", projeto.DataCriacao);
            comando.Parameters.AddWithValue("@DATA_FINALIZACAO", projeto.DataFinalizacao);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }


        public List<Projeto> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT 
            projetos.id AS 'ProjetoId',
            projetos.id_cliente AS 'ProjetoId_Cliente',
            projetos.nome AS 'ProjetoNome',
            projetos.data_criacao AS 'ProjetoData_Criacao',
            projetos.data_finalizacao AS 'ProjetoData_Finalizacao'
            FROM projetos";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Projeto> projetos = new List<Projeto>();
            foreach (DataRow linha in tabela.Rows)
            {
                Projeto projeto = new Projeto();
                projeto.Id = Convert.ToInt32(linha["ProjetoId"]);
                projeto.IdCliente = Convert.ToInt32(linha["ProjetoId_Cliente"]);
                projeto.Nome = linha["ProjetoNome"].ToString();
                projeto.DataCriacao = Convert.ToDateTime(linha["ProjetoData_Criacao"]);
                projeto.DataFinalizacao = Convert.ToDateTime(linha["ProjetoData_Finalizacao"]);
                projetos.Add(projeto);
            }
            return projetos;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"DELETE FROM projetos WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Projeto ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT 
            projetos.id AS 'ProjetoId',
            projetos.id_cliente AS 'ProjetoId_Cliente',
            projetos.nome AS 'ProjetoNome',
            projetos.data_criacao AS 'ProjetoData_Criacao',
            projetos.data_finalizacao AS 'ProjetoData_Finalizacao'
            FROM projetos WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            if (tabela.Rows.Count == 1)
            {
                DataRow linha = tabela.Rows[0];
                Projeto projeto = new Projeto();
                projeto.Id = Convert.ToInt32(linha["ProjetoId"]);
                projeto.IdCliente = Convert.ToInt32(linha["ProjetoId_Cliente"]);
                projeto.Nome = linha["ProjetoNome"].ToString();
                projeto.DataCriacao = Convert.ToDateTime(linha["ProjetoData_Criacao"]);
                projeto.DataFinalizacao = Convert.ToDateTime(linha["ProjetoData_Finalizacao"]);
                return projeto;
            }
            return null;
        }

        public bool Editar(Projeto projeto)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE projetos SET id_cliente = @ID_CLIENTE, nome = @NOME, data_criacao = @DATA_CRIACAO, data_finalizacao = @DATA_FINALIZACAO WHERE id = @ID  ";
            comando.Parameters.AddWithValue("@ID_CLIENTE", projeto.IdCliente);
            comando.Parameters.AddWithValue("@NOME", projeto.Nome);
            comando.Parameters.AddWithValue("@ID", projeto.Id);
            comando.Parameters.AddWithValue("@DATA_CRIACAO", projeto.DataCriacao);
            comando.Parameters.AddWithValue("@DATA_FINALIZACAO", projeto.DataFinalizacao);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

    }

}
