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
    public class QuestionarioConfiguration : EntityTypeConfiguration<Questionario>
    {
        public QuestionarioConfiguration()
        {
            HasKey(c => c.QuestionarioID);
            Property(c => c.QuestionarioID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map(c => c.ToTable("Questionario"));

            HasRequired(c => c.ModeloQuestionario)
                .WithMany()
                .HasForeignKey(c => c.ModeloQuestionarioID);

            HasRequired(c => c.Avaliacao)
                .WithMany()
                .HasForeignKey(c => c.AvaliacaoID);

            HasRequired(c => c.Avaliador)
                .WithMany()
                .HasForeignKey(c => c.AvaliadorID);

            HasRequired(c => c.Avaliado)
                .WithMany()
                .HasForeignKey(c => c.AvaliadoID);                       

            HasMany(c => c.Respostas)
                .WithOptional()
                .HasForeignKey(d => d.QuestionarioID);

            HasRequired(c => c.UsuarioFinalizacao)
                .WithMany()
                .HasForeignKey(c => c.UsuarioFinalizacaoID);

            Ignore(a => a.PontuacaoMedia);

            Ignore(a => a.PontuacaoTotal);
        }
    }
}