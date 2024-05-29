using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.IdentificacaoAreaTratada
{
    public class IdentificacaoAreaTratada
    {
        public int Id { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Localizacao { get; set; }
        public string Cultura { get; set; }
        public string Extensao { get; set; }
        public string CroquiArea { get; set; }
    }
}
