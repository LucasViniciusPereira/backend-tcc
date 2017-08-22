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
    public class PessoaConfiguration : EntityTypeConfiguration<Pessoa>
    {
        public PessoaConfiguration()
        {
            HasKey(c => c.PessoaID);
            Property(c => c.PessoaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            Map<Pessoa>(c => c.MapInheritedProperties())
            .ToTable("Pessoa");

            //Property(c => c.Endereco).IsOptional();

            Property(c => c.Endereco.Logradouro)
               .HasColumnName("Logradouro");

            Property(c => c.Endereco.EnderecoDsc)
               .HasColumnName("EnderecoDsc");

            Property(c => c.Endereco.Numero)
               .HasColumnName("Numero");

            Property(c => c.Endereco.Complemento)
               .HasColumnName("Complemento");

            Property(c => c.Endereco.CEP)
               .HasColumnName("CEP"); 

            Property(c => c.Endereco.Bairro)
               .HasColumnName("Bairro");

            Property(c => c.Endereco.Cidade)
               .HasColumnName("Cidade");

            Property(c => c.Endereco.UF)
               .HasColumnName("UF"); 
        }
    }
}
