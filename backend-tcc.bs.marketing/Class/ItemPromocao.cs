using backend_tcc.bs.estoque.Class;

namespace backend_tcc.bs.marketing.Class
{
    public class ItemPromocao
    {
        public int ItemPromocaoID { get; set; }
        public int PromocaoID { get; set; }
        public virtual Promocao Promocao { get; set; }
        public int ProdutoID { get; set; }
        public virtual Produto Produto { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Valor { get; set; }
    }
}
