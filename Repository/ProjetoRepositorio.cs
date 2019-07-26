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
            public int Inserir (Projeto projeto)
            {
                SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO projetos(id,id_Cliente,nome,data_Criacao,data_finalizacao)
OUTPUT INSERTED.ID VALUES
(@ID,@ID_CLIENTE,@NOME,@DATA_CRIACAO,@DATA_FINALIZACAO)";
                comando.Parameters.AddWithValue("@ID", projeto.Id);
                comando.Parameters.AddWithValue("@ID_CLIENTE", projeto.Id_Cliente);
                comando.Parameters.AddWithValue("@NOME", projeto.Nome);
                comando.Parameters.AddWithValue("@DATA_CRIACAO", projeto.Data_Criacao);
                comando.Parameters.AddWithValue("@DATA_FINALIZACAO", projeto.Data_Finalizacao);
               
                int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
                return id;
            }


        public List<Projeto> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT 
projetos.id AS 'ProjetoId',
projetos.id_Cliente AS 'ProjetoId_Cliente',
projetos.Nome AS 'ProjetoNome',
projetos.Data_Criacao AS 'ProjetoData_Criacao',
projetos.Data_Finalizacao AS 'ProjetoData_Finalizacao',
FROM projetos
INNER JOIN categorias ON
    (projeto.categoria=Categoria.Id)";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<Projeto> projetos = new List<Projeto>();
            foreach (DataRow linha in tabela.Rows)
            {
                Projeto projeto = new Projeto();
                projeto.Id = Convert.ToInt32(linha["ProjetoId"]);
                projeto.Id_Cliente = Convert.ToInt32(linha["ProjetoId_Cliente"]);
                projeto.Nome = linha["ProjetoNome"].ToString();
                projeto.Data_Criacao = Convert.ToInt32(linha["ProjetoData_Criacao"]);
                projeto.Data_Finalizacao = Convert.ToInt32(linha["ProjetoData_Finalizacao"]);
               
                //projeto.Categoria = new Categoria();
                //projeto.Categoria.Nome = linha["CategoriaNome"].ToString();
                projetos.Add(projeto);
            }
            return projetos;

        }
    }
}
