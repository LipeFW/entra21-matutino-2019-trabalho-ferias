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

    public class TarefaRepositorio
    {

        public int Inserir(Tarefa tarefa)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO  tarefas (id,id_Usuario_Responsavel,id_Projeto,id_Categoria,titulo,descricao,duracao)
            OUTPUT INSERTED.ID VALUES
            (@ID,@ID_USUARIO_RESPONSAVEL,@ID_PROJETO,@ID_CATEGORIA,@TITULO,@DESCRICAO,@DURACAO)";
            comando.Parameters.AddWithValue("@ID", tarefa.Id);
            comando.Parameters.AddWithValue("@ID_USUARIO_RESPONSAVEL", tarefa.IdUsuarioResponsavel);
            comando.Parameters.AddWithValue("@ID_PROJETO", tarefa.IdProjeto);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", tarefa.IdCategoria);
            comando.Parameters.AddWithValue("@TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("@DESCRICAO", tarefa.Descricao);
            comando.Parameters.AddWithValue("@DURACAO", tarefa.Duracao);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }


        public List<Tarefa> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT 
            tarefas.id AS 'TarefaId',
            tarefas.id_Usuario_Responsavel AS'TarefaId_Usuario_Responsavel',
            tarefas.id_Projeto AS 'TarefaId_Pojeto',
            tarefas.id_Categoria AS 'TarefaId_Categoria',
            tarefas.Titulo AS 'TarefaTitulo',
            tarefas.Descricao AS 'TarefaDescricao',
            tarefas.Duracao AS 'TarefaDuracao'
            FROM tarefas";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<Tarefa> tarefas = new List<Tarefa>();
            foreach (DataRow linha in tabela.Rows)
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Id = Convert.ToInt32(linha["TarefaId"]);
                tarefa.IdUsuarioResponsavel = Convert.ToInt32(linha["TarefaId_Usuario_Responsavel"]);
                tarefa.IdProjeto = Convert.ToInt32(linha["TarefaId_Projeto"]);
                tarefa.IdCategoria = Convert.ToInt32(linha["TarefaId_Categoria"]);
                tarefa.Titulo = linha["TarefaTitulo"].ToString();
                tarefa.Descricao = linha["TarefaDescricao"].ToString();
                tarefa.Duracao = Convert.ToDateTime(linha["TarefaDuracao"]);
                tarefas.Add(tarefa);
            }
            return tarefas;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"DELETE FROM tarefas WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Tarefa ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT * FROM tarefas WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            if (tabela.Rows.Count == 1)
            {
                DataRow linha = tabela.Rows[0];
                Tarefa tarefa = new Tarefa();
                tarefa.Id = Convert.ToInt32(linha["TarefaId"]);
                tarefa.IdUsuarioResponsavel = Convert.ToInt32(linha["TarefaId_Usuario_Responsavel"]);
                tarefa.IdProjeto = Convert.ToInt32(linha["TarefaId_Projeto"]);
                tarefa.IdCategoria = Convert.ToInt32(linha["TarefaId_Categoria"]);
                tarefa.Titulo = linha["TarefaTitulo"].ToString();
                tarefa.Descricao = linha["TarefaDescricao"].ToString();
                tarefa.Duracao = Convert.ToDateTime(linha["TarefaDuracao"]);
                return tarefa;
            }
            return null;
        }

        public bool Editar(Tarefa tarefa)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE tarefas SET id_usuario_responsavel = @ID_USUARIO_RESPONSAVEL, id_projeto = @ID_PROJETO, id_categoria = @ID_CATEGORIA, titulo = @TITULO, descricao = @DESCRICAO, duracao = @DURACAO";
            comando.Parameters.AddWithValue("@ID_USUARIO_RESPONSAVEL", tarefa.IdUsuarioResponsavel);
            comando.Parameters.AddWithValue("@ID_PROJETO", tarefa.IdProjeto);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", tarefa.IdCategoria);
            comando.Parameters.AddWithValue("@TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("@DESCRICAO", tarefa.Descricao);
            comando.Parameters.AddWithValue("@DURACAO", tarefa.Duracao);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
