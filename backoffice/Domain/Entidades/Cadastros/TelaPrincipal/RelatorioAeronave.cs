using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.TelaPrincipal
{
    public class RelatorioAeronave
    {
        public string NomeAeronave { get; set; }
        public decimal ExtensaoTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalHoras { get; set; }
    }
}
