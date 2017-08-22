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
    public class DepartamentoConfiguration : EntityTypeConfiguration<Departamento>
    {
        public DepartamentoConfiguration()
        {
            HasKey(c => c.DepartamentoID);
            Property(c => c.DepartamentoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.MapInheritedProperties()).ToTable("Departamento");

            //HasRequired(c => c.LayoutArquivoECT)
            //    .WithMany()
            //    .HasForeignKey(c => c.LayoutArquivoIdECT);

            //Property(c => c.UsuarioCriacaoID)
            //    .HasMaxLength(40)
            //    .IsRequired();

        }
    }
}
