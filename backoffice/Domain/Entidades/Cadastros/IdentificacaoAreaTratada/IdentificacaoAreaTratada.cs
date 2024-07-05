using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        public string? GravacaoArea { get; set; }
        public string? Marcadores { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }
    }
}
