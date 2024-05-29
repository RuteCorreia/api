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
    public class AplicacaoCroquiImportacao
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AplicacaoCroqui")]
        public int? IdAplicacaoCroqui { get; set; }
        public string Arquivo { get; set; }

        [JsonIgnore]
        public virtual AplicacaoCroqui? AplicacaoCroqui { get; set; }
    }
}
