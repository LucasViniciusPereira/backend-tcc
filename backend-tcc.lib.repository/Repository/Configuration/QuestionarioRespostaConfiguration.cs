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
    public class QuestionarioRespostaConfiguration : EntityTypeConfiguration<QuestionarioResposta>
    {
        public QuestionarioRespostaConfiguration()
        {
            HasKey(c => c.QuestionarioRespostaID);
            Property(c => c.QuestionarioRespostaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("QuestionarioResposta"));

            HasRequired(c => c.Questionario)
                .WithMany()
                .HasForeignKey(c => c.QuestionarioID);

            HasRequired(c => c.Pergunta)
                .WithMany()
                .HasForeignKey(c => c.PerguntaID);

            HasRequired(c => c.Resposta)
                .WithMany()
                .HasForeignKey(c => c.RespostaID);
        }
    }
}