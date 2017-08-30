using backend_tcc.bs.vendas.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Vendas
{
    public class PedidoConfiguration : EntityTypeConfiguration<Pedido>
    {
        public PedidoConfiguration()
        {
            HasKey(c => c.PedidoID);
            Property(c => c.PedidoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Pedido", "api"));

            HasRequired(c => c.Cliente)
                .WithMany()
                .HasForeignKey(f => f.ClienteID);

            HasMany(c => c.Itens)
                .WithOptional()
                .HasForeignKey(f => f.PedidoID);

        }
    }
}