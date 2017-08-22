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
    public class FuncaoConfiguration : EntityTypeConfiguration<Funcao>
    {
        public FuncaoConfiguration()
        {
            HasKey(c => c.FuncaoID);
            Property(c => c.FuncaoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Funcao"));
        }
    }
}
