using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.TelaPrincipal
{
    public class RelatorioAeronave
    {
        public string Aeronave { get; set; }
        public decimal ExtensaoTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalHoras { get; set; }
        public string Piloto { get; set; }
        public string Executor { get; set; }
    }
}
