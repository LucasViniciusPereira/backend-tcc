using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.compras.Class
{
    public class Orcamento
    {
        public int OrcamentoID { get; set; }
        public DateTime Data { get; set; }
        public int FornecedorID { get; set; }
        public decimal Valor { get; set; }
    }
}
