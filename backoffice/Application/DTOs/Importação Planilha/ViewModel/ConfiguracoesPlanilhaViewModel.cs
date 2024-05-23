using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Importação_Planilha.ViewModel
{
    public class ConfiguracoesPlanilhaViewModel
    {
        public string EnderecoPlanilha { get; set; }
        public long QtdRegistroPlanilha { get; set; }
        public long QtdRegistroBanco { get; set; }
        public bool DadosSalvos { get; set; }
        public int IdImportacao { get; set; }
        public int IdCliente { get; set; }
        public int? IndexLinhaUltimaCarga { get; set; }
        public string DataPlanilha { get; set; }
        public string PrazoServico { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Url { get; set; }
    }
}
