using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class AplicacaoLog
    {
        public int Id { get; set; }
        public int? IdAplicacao { get; set; }
        public DateTime? Data { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
