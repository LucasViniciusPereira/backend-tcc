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
    public class PerguntaConfiguration : EntityTypeConfiguration<Pergunta>
    {
        public PerguntaConfiguration()
        {
            HasKey(c => c.PerguntaID);
            Property(c => c.PerguntaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Pergunta").MapInheritedProperties());
            
            HasRequired(c => c.ModeloQuestionario)
                .WithMany()
                .HasForeignKey(c => c.ModeloQuestionarioID);

            HasMany(c => c.Respostas)
                .WithOptional()
                .HasForeignKey(d => d.PerguntaID);
        }
    }
}
