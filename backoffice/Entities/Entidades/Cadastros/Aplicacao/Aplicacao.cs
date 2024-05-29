using Entities.Entidades.Cadastros.Cliente;
using Entities.Entidades.Cadastros.Cultura;
using Entities.Entidades.Cadastros.Executores;
using Entities.Entidades.Cadastros.Pilotos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class Aplicacao
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }
        public string StatusEnvio { get; set; }

        [ForeignKey("Piloto")]
        public int? IdPiloto { get; set; }

        [ForeignKey("Executor")]
        public int? IdExecutor { get; set; }

        [ForeignKey("Cliente")]
        public int? IdCliente { get; set; }

        [ForeignKey("Cultura")]
        public int? IdCultura { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }
        [JsonIgnore]
        public virtual Piloto? Piloto { get; set; }
        [JsonIgnore]
        public virtual Executor? Executor { get; set; }
        [JsonIgnore]
        public virtual Cliente.Cliente? Cliente { get; set; }
        [JsonIgnore]
        public virtual Cultura.Cultura? Cultura { get; set; }

    }
}
