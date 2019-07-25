using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Repository
{
   public class UsuarioRepositorio
    {
        
        
            public int Inserir (Usuario usuario)
            {
                SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO usuarios (id,nome,login,senha)
OUTPUT INSERTED.ID VALUES
(@ID,@NOME,@LOGIN,SENHA)";
                comando.Parameters.AddWithValue("@ID", usuario.Id);
                comando.Parameters.AddWithValue("@NOME", usuario.Nome);
                comando.Parameters.AddWithValue("@LOGIN", usuario.Login);
                comando.Parameters.AddWithValue("@SENHA", usuario.Senha);
               
                int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
                return id;
            }


        public List<Usuario> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT 
usuarios.id AS 'UsuarioId',
usuarios.nome AS 'UsuarioNome',
usuarios.login AS 'UsuarioLogin',
usuarios.senha AS 'UsuarioSenha'
FROM usuarios
INNER JOIN categorias ON
    (Usuario .id_Categoria= categoria.Id)";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            List<Usuario> usuarios = new List<Usuario>();
            foreach (DataRow linha in tabela.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.Id = Convert.ToInt32(linha["UsuarioId"]);
                usuario.Nome = linha["UsuarioNome"].ToString();
                usuario.Login = linha["UsuarioLogin"].ToString();
                usuario.Senha = linha["UsuarioSenha"].ToString();
                
                usuarios.Add(usuario);
            }
            return usuarios;

        }
    }
}
