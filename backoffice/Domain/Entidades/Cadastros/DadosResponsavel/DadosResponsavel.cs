using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.DadosResponsavel
{
    public class DadosResponsavel
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string NomeCompleto { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public byte[] assinaturaResponsavel { get; set; }
    }
}
