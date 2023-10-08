using Entities.Entidades.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.User
{
    public interface IAuthService
    {
        Task<(int, string)> Register(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}
