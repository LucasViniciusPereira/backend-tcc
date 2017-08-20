using backend_tcc.bs.vendas.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.vendas.Class
{
    public class Pedido
    {
        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public DateTime Data { get; set; }
        public DateTime? DataPrevEntrega { get; set; }
        public eSituacaoPedido SituacaoPedidoID { get; set; }
    }
}
