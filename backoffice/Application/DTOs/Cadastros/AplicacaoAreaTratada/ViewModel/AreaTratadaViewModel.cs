using Application.DTOs.Cadastros.DataFormat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel
{
    public class AreaTratadaViewModel
    {
        public int Id { get; set; }
        public string? UF { get; set; }
        public string? Cidade { get; set; }
        public string? Localizacao { get; set; }
        public string? Cultura { get; set; }
        public string? Extensao { get; set; }
        public string? CroquiArea { get; set; }
        public string? Gravacao { get; set; }
        public string? Marcadores { get; set; }

    }
}
