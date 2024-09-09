using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.RelatorioBase
{
    public class RelatorioBaseViewModel
    {
        public int Id { get; set; }
        public string? NomeRelatorio { get; set; }
        public string? Base64Data { get; set; }
        public bool IsMapa { get; set; }
        public int? StatusEnvio { get; set; }
        public string? DataAlteracao { get; set; }
    }
}
