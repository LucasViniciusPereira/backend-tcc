using backend_tcc.bs.common.Class;
using backend_tcc.bs.vendas.Enumerators;
using System;
using System.Collections.Generic;

namespace backend_tcc.bs.vendas.Class
{
    public class Pedido
    {
        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime? DataPrevEntrega { get; set; }
        public eSituacaoPedido SituacaoPedidoID { get; set; }
        public decimal Valor { get; set; }

        public virtual ICollection<ItemPedido> Itens { get; set; }
    }
}
