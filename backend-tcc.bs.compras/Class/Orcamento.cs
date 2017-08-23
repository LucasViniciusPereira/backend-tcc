using backend_tcc.bs.common.Class;
using System;
using System.Collections.Generic;
using backend_tcc.bs.compras.Enumerators;

namespace backend_tcc.bs.compras.Class
{
    public class Orcamento
    {
        public int OrcamentoID { get; set; }
        public DateTime DataPedido { get; set; }
        public int FornecedorID { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public eSituacaoOrcamento SituacaoOrcamentoID { get; set; }
        public decimal Valor { get; set; }

        public virtual ICollection<ItemOrcamento> Itens { get; set; }
    }
}
