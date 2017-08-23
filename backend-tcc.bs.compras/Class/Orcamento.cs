using backend_tcc.bs.common.Class;
using System;
using System.Collections.Generic;

namespace backend_tcc.bs.compras.Class
{
    public class Orcamento
    {
        public int OrcamentoID { get; set; }
        public DateTime Data { get; set; }
        public int FornecedorID { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public decimal Valor { get; set; }

        public virtual ICollection<ItemOrcamento> Itens { get; set; }
    }
}
