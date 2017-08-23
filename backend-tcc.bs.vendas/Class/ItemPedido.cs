using backend_tcc.bs.estoque.Class;

namespace backend_tcc.bs.vendas.Class
{
    public class ItemPedido
    {
        public int ItemPedidoID { get; set; }
        public int PedidoID { get; set; }
        public virtual Pedido Pedido { get; set; }
        public int ProdutoID { get; set; }
        public virtual Produto Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal? Desconto { get; set; }
        public int? LoteID { get; set; }
        public virtual Lote Lote { get; set; }
    }
}
