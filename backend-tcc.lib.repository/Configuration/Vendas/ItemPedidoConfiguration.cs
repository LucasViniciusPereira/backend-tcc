using backend_tcc.bs.vendas.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Vendas
{
    public class ItemPedidoConfiguration : EntityTypeConfiguration<ItemPedido>
    {
        public ItemPedidoConfiguration()
        {
            HasKey(c => c.ItemPedidoID);
            Property(c => c.ItemPedidoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("ItemPedido"));

            HasRequired(c => c.Pedido)
                .WithMany()
                .HasForeignKey(f => f.PedidoID);

            HasRequired(c => c.Produto)
                .WithMany()
                .HasForeignKey(f => f.ProdutoID);

            HasOptional(c => c.Lote)
                .WithMany()
                .HasForeignKey(f => f.LoteID);
        }
    }
}
