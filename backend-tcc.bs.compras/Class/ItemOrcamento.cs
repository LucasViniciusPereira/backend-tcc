using backend_tcc.bs.estoque.Class;

namespace backend_tcc.bs.compras.Class
{
    public class ItemOrcamento
    {
        public int ItemOrcamentoID { get; set; }
        public int OrcamentoID { get; set; }
        public virtual Orcamento Orcamento { get; set; }
        public int ProdutoID { get; set; }
        public virtual Produto Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
