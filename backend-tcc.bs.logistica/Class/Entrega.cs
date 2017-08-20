using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.logistica.Class
{
    public class Entrega
    {
        public int EntregaID { get; set; }
        public int VeiculoID { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public DateTime Data { get; set; }
    }
}
