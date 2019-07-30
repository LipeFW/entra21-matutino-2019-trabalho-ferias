using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cliente
    {
        public int Id;
        public int IdCidade;
        public string Nome;
        public string CPF;
        public DateTime DataNascimento;
        public int Numero;
        public string Complemento;
        public string Logradouro;
        public string CEP;

        public Cidade Cidade;

    }
}
