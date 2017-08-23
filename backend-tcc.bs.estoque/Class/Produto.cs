using backend_tcc.bs.common.Class;

namespace backend_tcc.bs.estoque.Class
{
    public class Produto
    {
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public string UN { get; set; }
        public int FabricanteID { get; set; }
        public virtual Fabricante Fabricante { get; set; }
        public int? FornecedorID { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public decimal Preco { get; set; }
        //public virtual Estoque Estoque { get; set; }
    }
}
