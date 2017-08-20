using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.estoque.Class
{
    public class Lote
    {
        public int LoteID { get; set; }
        public string Codigo { get; set; }
        public DateTime Validade { get; set; }
        public int ProdutoID { get; set; }
        public decimal Quantidade { get; set; }
        public int FornecedorID { get; set; }
    }
}
