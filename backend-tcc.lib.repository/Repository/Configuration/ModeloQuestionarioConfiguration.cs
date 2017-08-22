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
    public class ModeloQuestionarioConfiguration : EntityTypeConfiguration<ModeloQuestionario>
    {
        public ModeloQuestionarioConfiguration()
        {
            HasKey(c => c.ModeloQuestionarioID);
            Property(c => c.ModeloQuestionarioID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("ModeloQuestionario").MapInheritedProperties());

            HasMany(c => c.Perguntas)
                .WithOptional()
                .HasForeignKey(d => d.ModeloQuestionarioID);
        }
    }
}
