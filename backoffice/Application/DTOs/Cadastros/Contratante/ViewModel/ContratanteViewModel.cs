using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Contratante.ViewModel
{
    public class ContratanteViewModel
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
        public string ContratanteRef { get; set; }
    }
}
