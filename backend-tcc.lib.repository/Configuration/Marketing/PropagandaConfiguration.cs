using backend_tcc.bs.marketing.Class;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace backend_tcc.lib.repository.Configuration.Marketing
{
    public class PropagandaConfiguration : EntityTypeConfiguration<Propaganda>
    {
        public PropagandaConfiguration()
        {
            HasKey(c => c.PropagandaID);
            Property(c => c.PropagandaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Propaganda", "api"));            
        }
    }
}