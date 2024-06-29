using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.Contratante
{
    public class Contratante
    {
        public int Id { get; set; }
        public string TipoContratante { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string RG { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string? ContratanteRef { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }
    }
}
