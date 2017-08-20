using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.vendas.Class
{
    public class ItemPedido
    {
        public int ItemPedidoID { get; set; }
        public int PedidoID { get; set; }
        public int ProdutoID { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal? Desconto { get; set; }
        public int LoteID { get; set; }
    }
}
