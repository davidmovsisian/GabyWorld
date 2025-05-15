using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaby.World.Core.ApiModels.LoginRegister
{
    public class LoginCredentialsApiModel
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
