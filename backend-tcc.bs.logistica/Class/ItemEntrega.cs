using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.logistica.Class
{
    public class ItemEntrega
    {
        public int ItemEntregaID { get; set; }
        public int EntregaID { get; set; }
        public int PedidoID { get; set; }
    }
}
