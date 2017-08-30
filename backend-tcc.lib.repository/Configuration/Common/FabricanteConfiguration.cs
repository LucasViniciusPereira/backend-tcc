using backend_tcc.bs.common.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Common
{
    public class FabricanteConfiguration : EntityTypeConfiguration<Fabricante>
    {
        public FabricanteConfiguration()
        {
            HasKey(c => c.FabricanteID);
            Property(c => c.FabricanteID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Fabricante", "api"));
        }
    }
}
