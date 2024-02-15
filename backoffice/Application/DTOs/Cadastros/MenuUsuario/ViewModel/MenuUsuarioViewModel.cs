using Domain.Entidades.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.MenuUsuario.ViewModel
{
    public class MenuUsuarioViewModel
    {
        public int Id { get; set; }

        public string IdUsuario { get; set; }

        public int? IdMenu { get; set; }
    }
}
