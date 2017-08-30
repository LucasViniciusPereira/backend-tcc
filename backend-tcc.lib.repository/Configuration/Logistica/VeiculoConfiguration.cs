using backend_tcc.bs.logistica.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Logistica
{
    public class VeiculoConfiguration : EntityTypeConfiguration<Veiculo>
    {
        public VeiculoConfiguration()
        {
            HasKey(c => c.VeiculoID);
            Property(c => c.VeiculoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Veiculo", "api"));
        }
    }
}
