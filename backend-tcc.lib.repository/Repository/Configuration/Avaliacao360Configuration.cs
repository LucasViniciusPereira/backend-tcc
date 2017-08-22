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
    public class AvaliacaoConfiguration : EntityTypeConfiguration<Avaliacao>
    {
        public AvaliacaoConfiguration()
        {
            HasKey(c => c.AvaliacaoID);
            Property(c => c.AvaliacaoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Map<Avaliacao>(c => c.MapInheritedProperties())
            .ToTable("Avaliacao");

            HasMany(c => c.Questionarios)
                .WithOptional()
                .HasForeignKey(d => d.AvaliacaoID);

            HasOptional(c => c.UsuarioFinalizacao)
                .WithMany()
                .HasForeignKey(f => f.UsuarioFinalizacaoID);

            Ignore(a => a.PontuacaoMedia);
            Ignore(a => a.TotalAvaliados);
            Ignore(a => a.AvaliacoesFinalizadas);

        }
    }

    public class Avaliacao360Configuration : AvaliacaoConfiguration
    {
        public Avaliacao360Configuration()
        {
            Map<AvaliacaoSAD360>(c => c.Requires("Tipo").HasValue(1));
        }
    }
}
