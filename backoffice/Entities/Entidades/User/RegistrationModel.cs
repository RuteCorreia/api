using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.User
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "")]
        public string Username { get; set; }

        [Required(ErrorMessage = "")]
        public string Nome { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "")]
        public string Email { get; set; }

        [Required(ErrorMessage = "")]
        public string Senha { get; set; }
    }
}
