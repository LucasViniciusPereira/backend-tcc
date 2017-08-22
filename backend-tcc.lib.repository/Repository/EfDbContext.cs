using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDNJR.DW.TCC.Domain.Entidades;
using WDNJR.DW.TCC.Domain.Repository.Configuration;

namespace WDNJR.DW.TCC.Domain.Repository
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(string cnnString) : base(cnnString)
        {

        }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Funcao> Funcao { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<Resposta> Resposta { get; set; }
        public DbSet<ModeloQuestionario> ModeloQuestionario { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Parametro> Parametro { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Questionario> Questionario { get; set; }
        public DbSet<QuestionarioResposta> QuestionarioResposta { get; set; }
        public DbSet<Avaliacao> Avaliacao { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UsuarioConfiguration());
            modelBuilder.Configurations.Add(new DepartamentoConfiguration());
            modelBuilder.Configurations.Add(new FuncaoConfiguration());
            modelBuilder.Configurations.Add(new FuncionarioConfiguration());
            modelBuilder.Configurations.Add(new ModeloQuestionarioConfiguration());
            modelBuilder.Configurations.Add(new PerguntaConfiguration());
            modelBuilder.Configurations.Add(new RespostaConfiguration());

            //modelBuilder.Configurations.Add(new ParametroConfiguration());
            modelBuilder.Configurations.Add(new Avaliacao360Configuration());
            modelBuilder.Configurations.Add(new QuestionarioConfiguration());
            modelBuilder.Configurations.Add(new QuestionarioRespostaConfiguration());

            modelBuilder.Entity<Funcionario>()
                .HasOptional(c => c.Gestor)
                .WithMany()
                .HasForeignKey(c => c.GestorID);

            modelBuilder.Entity<Funcionario>()
                .HasRequired(c => c.Funcao)
                .WithMany()
                .HasForeignKey(c => c.FuncaoID);

            modelBuilder.Entity<Funcionario>()
                .HasRequired(c => c.Departamento)
                .WithMany()
                .HasForeignKey(c => c.DepartamentoID);

        }
    }
}
