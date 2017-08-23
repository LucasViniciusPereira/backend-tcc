using System.Collections.Generic;

namespace backend_tcc.bs.estoque.Class
{
    public class Estoque
    {
        public int ProdutoID { get; set; }
        public virtual Produto Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Bloqueado { get; set; }

        public virtual ICollection<Lote> Lotes { get; set; }
    }
}
