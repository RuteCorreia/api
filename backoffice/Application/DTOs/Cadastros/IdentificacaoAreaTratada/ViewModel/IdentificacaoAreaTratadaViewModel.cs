using Application.DTOs.Cadastros.DataFormat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel
{
    public class IdentificacaoAreaTratadaViewModel
    {
        public int Id { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Localizacao { get; set; }
        public string Cultura { get; set; }
        public string Extensao { get; set; }
        public DataFormatViewModel CroquiArea { get; set; }
    }
}
