using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.estoque.Class
{
    public class Estoque
    {
        public int ProdutoID { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Disponivel { get; set; }
    }
}
