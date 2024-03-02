using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel
{
    public class ManutencaoAeronaveViewModel
    {
        public int Id { get; set; }
        public int? IdAeronave { get; set; }
        public string? HorimetroInicial { get; set; }
        public string? HorasRevisao { get; set; }
        public string? HorasInspecao { get; set; }
        public byte[]? Documento { get; set; }
    }
}
