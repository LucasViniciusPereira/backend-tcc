using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_tcc.bs.marketing.Class
{
    public class ItemPromocao
    {
        public int ItemPromocaoID { get; set; }
        public int PromocaoID { get; set; }
        public int ProdutoID { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Valor { get; set; }
    }
}
