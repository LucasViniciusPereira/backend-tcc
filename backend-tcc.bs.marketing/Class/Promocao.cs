using backend_tcc.bs.common.Class;
using System;
using System.Collections.Generic;

namespace backend_tcc.bs.marketing.Class
{
    public class Promocao
    {
        public int PromocaoID { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int? ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<ItemPromocao> Itens { get; set; }
    }
}
