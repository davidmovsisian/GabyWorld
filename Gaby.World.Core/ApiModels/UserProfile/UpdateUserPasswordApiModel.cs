using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaby.World.Core.ApiModels.UserProfile
{
    public class UpdateUserPasswordApiModel
    {
        public string CurrentPassword {  get; set; }
        public string NewPassword { get; set; }
    }
}
