using System.ComponentModel.DataAnnotations;

namespace backend_tcc.bs.administrador.Class
{
    public class Usuario
    {        
        public string UsuarioID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
