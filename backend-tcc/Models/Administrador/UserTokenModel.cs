using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend_tcc.api.Models.Administrador
{
    public class UserTokenModel
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}