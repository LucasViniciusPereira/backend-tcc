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
    public class RespostaConfiguration : EntityTypeConfiguration<Resposta>
    {
        public RespostaConfiguration()
        {
            HasKey(c => c.RespostaID);
            Property(c => c.RespostaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Resposta"));

            HasRequired(c => c.Pergunta)
            .WithMany()
            .HasForeignKey(c => c.PerguntaID);
        }
    }
}
