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
            public int Inserir (Tarefa tarefa)
            {
                SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO  tarefas (id,id_Usuario_Responsavel,id_Projeto,id_Categoria,titulo,descricao,duracao)
OUTPUT INSERTED.ID VALUES
(@ID,@ID_USUARIO_RESPONSAVEL,@ID_PROJETO,@ID_CATEGORIA,@TITULO,@DESCRICAO,@DURACAO)";
                comando.Parameters.AddWithValue("@ID", tarefa.Id);
                comando.Parameters.AddWithValue("@ID_USUARIO_RESPONSAVEL", tarefa.Id_Usuario_Responsavel);
                comando.Parameters.AddWithValue("@ID_PROJETO", tarefa.Id_Projeto);
                comando.Parameters.AddWithValue("@ID_CATEGORIA", tarefa.Id_Categoria);
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
FROM tarefas
INNER JOIN categorias ON
    (tarefa.id_categoria = categorias.id)";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<Tarefa> tarefas = new List<Tarefa>();
            foreach (DataRow linha in tabela.Rows)
            {
               Tarefa tarefa = new Tarefa();
                tarefa.Id = Convert.ToInt32(linha["TarefaId"]);
                tarefa.Id_Usuario_Responsavel = Convert.ToInt32(linha["TarefaId_Usuario_Responsavel"]);
                tarefa.Id_Projeto = Convert.ToInt32(linha["TarefaId_Projeto"]);
                tarefa.Id_Categoria = Convert.ToInt32(linha["TarefaId_Categoria"]);
                tarefa.Titulo= linha["TarefaTitulo"].ToString();
                tarefa.Descricao= linha["TarefaDescricao"].ToString();
                tarefa.Duracao= Convert.ToInt32(linha["TarefaDuracao"]);
               
               
                tarefas.Add(tarefa);
            }
            return tarefas;

        }
    }
}
