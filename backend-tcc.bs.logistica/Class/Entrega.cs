using System;
using System.Collections.Generic;

namespace backend_tcc.bs.logistica.Class
{
    public class Entrega
    {
        public int EntregaID { get; set; }
        public int VeiculoID { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public DateTime Data { get; set; }

        public virtual ICollection<ItemEntrega> Itens { get; set; }
    }
}
