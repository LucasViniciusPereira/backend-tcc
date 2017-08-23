using backend_tcc.bs.common.Class;
using System;

namespace backend_tcc.bs.estoque.Class
{
    public class Lote
    {
        public int LoteID { get; set; }
        public string Codigo { get; set; }
        public DateTime? Validade { get; set; }
        public int ProdutoID { get; set; }
        public virtual Produto Produto { get; set; }
        public decimal Quantidade { get; set; }
        public int FabricanteID { get; set; }
        public virtual Fabricante Fabricante { get; set; }
    }
}
