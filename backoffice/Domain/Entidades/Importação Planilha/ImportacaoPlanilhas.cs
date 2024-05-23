using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entidades.User;

namespace Domain.Entidades.Importação_Planilha
{
    public class ImportacaoPlanilhas
    {
        [Key]
        public int Id { get; set; }

        public int IntPlanilhaImportada { get; set; }

        public string Base64Planilha { get; set; }

        public int QtdRegistroPlanilha { get; set; }

        public int QtdRegistroBanco { get; set; }

        public int IndexUltimaLinha { get; set; }

        public bool DadosSalvo { get; set; }

        public DateTime DataProcessamento { get; set; }

        public bool Erro { get; set; }

        public string MenssagensProcessamento { get; set; }
        public string NomeCliente { get; set; }
        public DateTime? DataPlanilha { get; set; }
        public Guid? IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario User { get; set; }
    }
}
