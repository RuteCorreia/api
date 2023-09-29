using Entities.Entidades.Cadastros.Aeronaves;
using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.Cadastros.Executores;
using Entities.Entidades.Cadastros.Pistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.CombateIncendio
{
    public class CombateIncendio
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [ForeignKey("Executor")]
        public int? IdExecutor { get; set; }
        public bool OrgaoPublico_Privado { get; set; }
        public string? Aviso { get; set; }

        [ForeignKey("Aeronave")]
        public int? IdAeronave { get; set; }

        [ForeignKey("Pista")]
        public int? IdPista { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? HoraInicial { get; set; }
        public string? HorimetroAviao { get; set; }
        public string? LocalIncendioLat { get; set; }
        public string? LocalIncendioLon { get; set; }
        public string? Referencia { get; set; }
        public DateTime? HorarioFinalOperacao { get; set; }
        public string? HorimetroFinalOperacao { get; set; }
        public int? TotalAguaUtilizadaOperacao { get; set; }
        public string? CoordenadorBaseOperacionalNome { get; set; }
        public string? CoordenadorBaseOperacionalPosto { get; set; }
        public string? CoordenadorBaseOperacionalRE { get; set; }
        public string? CoordenadorBaseOperacionalAssinatura { get; set; }
        public string? ComandanteOcorrenciaNome { get; set; }
        public string? ComandanteOcorrenciaPosto { get; set; }
        public string? ComandanteOcorrenciaRE { get; set; }
        public string? ComandanteOcorrenciaAssinatura { get; set; }
        public string? ResponsavelOcorrenciaNome { get; set; }
        public string? ResponsavelOcorrenciaPosto { get; set; }
        public string? ResponsavelOcorrenciaRE { get; set; }
        public string? ResponsavelOcorrenciaAssinatura { get; set; }
        public virtual Empresa.Empresa Empresa { get; set; }
        public virtual Executor Executor { get; set; }
        public virtual Aeronave Aeronave { get; set; }
        public virtual Pista Pista { get; set; }
    }
}
