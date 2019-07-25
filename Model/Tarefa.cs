using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tarefa
    {
        public int Id;
        public int Id_Usuario_Responsavel;
        public int Id_Projeto;
        public int Id_Categoria;
        public string Titulo;
        public string Descricao;
        public DateTime Duracao;

        public Tarefa tarefa;

    }
}
