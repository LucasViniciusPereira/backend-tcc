using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.compras.Class
{
    public class ItemOrcamento
    {
        public int ItemOrcamentoID { get; set; }
        public int OrcamentoID { get; set; }
        public int ProdutoID { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
