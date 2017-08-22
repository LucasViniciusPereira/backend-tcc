using System.Data.Entity.ModelConfiguration;
using backend_tcc.bs.administrador.Class;

namespace backend_tcc.lib.repository.Configuration
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            HasKey(c => c.UsuarioID);
            //Property(c => c.UsuarioID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Usuario"));       
        }
    }
}
