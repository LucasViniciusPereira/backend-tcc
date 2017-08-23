using backend_tcc.bs.vendas.Class;

namespace backend_tcc.bs.logistica.Class
{
    public class ItemEntrega
    {
        public int ItemEntregaID { get; set; }
        public int EntregaID { get; set; }
        public virtual Entrega Entrega { get; set; }
        public int PedidoID { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
