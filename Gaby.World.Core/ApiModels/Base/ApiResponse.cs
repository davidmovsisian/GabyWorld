using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaby.World.Core.ApiModels.Base
{
    public class ApiResponse
    {
        public string ErrorMessage { get; set; }

        public bool Successful => ErrorMessage == null;

        public object Response;
    }

    public class ApiResponse<T> : ApiResponse
    {
        public new T Response { get
            {
                return (T)base.Response;
            }
            set
            {
                Response = (T)value;
            }
        }
    }
}
