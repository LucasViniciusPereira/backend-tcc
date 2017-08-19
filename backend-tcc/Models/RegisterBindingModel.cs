using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend_tcc.Models
{
    public class RegisterBindingModel
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}