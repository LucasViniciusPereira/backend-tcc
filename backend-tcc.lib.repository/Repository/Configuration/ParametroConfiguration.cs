using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDNJR.DW.TCC.Domain.Entidades;

namespace WDNJR.DW.TCC.Domain.Repository.Configuration
{
    public class ParametroConfiguration : EntityTypeConfiguration<Parametro>
    {
        public ParametroConfiguration()
        {
            HasKey(c => c.ParatametroID);
            Property(c => c.ParatametroID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Parametro"));
        }
    }
}
