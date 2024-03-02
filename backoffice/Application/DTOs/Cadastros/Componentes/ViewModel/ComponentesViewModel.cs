using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Componentes.ViewModel
{
    public class ComponentesViewModel
    {
        public int Id { get; set; }
        public int? IdAeronave { get; set; }
        public string? NomeComponente { get; set; }
        public string? Grupo { get; set; }
        public string? PartNumber { get; set; }
        public string? SerialNumber { get; set; }
        public string? TBO { get; set; }
        public string? UltimaInspecao { get; set; }
        public string? PrazoParaInspecao { get; set; }
        public string? TSN { get; set; }
        public string? TSO { get; set; }
    }
}
