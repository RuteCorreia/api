using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.ContratoPrestacaoServico
{
    public class ContratoPrestacaoServico
    {
        public int Id { get; set; }
        public string DistanciaPista { get; set; }
        public string Preco { get; set; }
        public string UnidadePreco { get; set; }
        public string Extensao { get; set; }
        public string ValorTotal { get; set; }
        public string Vencimento { get; set; }
        public string NomePiloto { get; set; }
        public string Executor { get; set; }
    }
}
