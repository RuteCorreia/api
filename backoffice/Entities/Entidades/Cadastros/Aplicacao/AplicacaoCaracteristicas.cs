using Entities.Entidades.Cadastros.Produtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class AplicacaoCaracteristicas
    {
        public int Id { get; set; }

        [ForeignKey("Aplicacao")]
        public int? IdAplicacao { get; set; }

        [ForeignKey("Produto")]
        public int? IdProduto { get; set; }

        [ForeignKey("Adjuvante")]
        public int? IdAdjuvante { get; set; }
        public string TipoDeServico { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Adjuvante.Adjuvante Adjuvante { get; set; }
    }
}
