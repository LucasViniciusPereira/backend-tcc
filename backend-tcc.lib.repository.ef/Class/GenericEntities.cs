using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using backend_tcc.lib.Class;
using backend_tcc.lib.repository.ef.Audit;
using backend_tcc.lib.repository.ef.Interface;
using backend_tcc.lib.repository.Interfaces;

namespace backend_tcc.lib.repository.ef.Class
{
    public abstract class GenericEntities<TDbContext> : DbContext, IDatabaseContext where TDbContext : DbContext
    {
        #region Constructors
        public GenericEntities(string ConnectionStringName)
            : base(ConnectionStringName)
        {
            Database.SetInitializer<TDbContext>(null);
            Serializer = SerializeJavaScriptNullObject.Instance;
            FactoryAudit = DefaultFactoryAudit.Instance;
        }
        #endregion

        #region Fields
        /// <summary>
        /// Armazena todos os repositórios criados
        /// </summary>
        protected List<Disposable> lstRepositoryDisposable = new List<Disposable>();
        #endregion

        #region Properties

        /// <summary>
        /// Identifica se o contexto foi descartado
        /// </summary>
        public bool Disposed { get; set; }

        public IFactoryAudit FactoryAudit { get; set; }

        /// <summary>
        /// Responsável por serializar o objeto que será auditado
        /// </summary>
        public ISerializeJavaScript Serializer { get; set; }

        /// <summary>
        /// Entidade responsável pela auditoria
        /// </summary>
        public DbSet<AuditLog> AuditLog { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Recupera uma nova instância de um repositório
        /// </summary>
        /// <typeparam name="T">Classe persistente</typeparam>
        /// <returns>Repositório tipado</returns>
        public virtual IRepository<T> GetRepository<T>() where T : class
        {
            var repository = new RepositoryGeneric<T>(this);

            lstRepositoryDisposable.Add(repository);

            return repository;
        }

        /// <summary>
        /// Atribui alguns comportamentos padrões
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Salva as alterações feitas no contexto
        /// </summary>
        /// <returns></returns>
        public int Commit(string UserID)
        {
            try
            {
                this.isDisposed();
                var result = this.SaveChanges(UserID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected int SaveChanges(string userId)
        {
            var Auditor = FactoryAudit.CreateAuditorEntries<TDbContext>(this);

            try
            {
                Auditor.Audit(userId);

                // Call the original SaveChanges(), which will save both the changes made and the audit records
                return base.SaveChanges();
            }
            catch (DbUpdateException db)
            {
                if (db.Entries.Any(c => c.Entity.GetType().Name == "AuditLog"))
                    throw new ApplicationException("Está faltando criar a tabela AuditLog", db);

                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Auditor = null;
            }
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException("O ID do Usuário deverá ser repassado no SaveChanges(string userID);");
        }

        /// <summary>
        /// Desfaz todas as alterações realizadas no contexto
        /// </summary>
        public void Rollback()
        {
            try
            {
                this.isDisposed();

                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            {
                                entry.CurrentValues.SetValues(entry.OriginalValues);
                                entry.State = EntityState.Unchanged;
                                break;
                            }
                        case EntityState.Deleted:
                            {
                                entry.State = EntityState.Unchanged;
                                break;
                            }
                        case EntityState.Added:
                            {
                                entry.State = EntityState.Detached;
                                break;
                            }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Descarta o objeto atual e todos os repositórios criados que ainda estão pendentes
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var item in lstRepositoryDisposable.Where(c => c.isDisposed == false))
                {
                    item.Dispose();
                }

                AuditLog = null;
                Serializer = null;
            }

            lstRepositoryDisposable.Clear();
            base.Dispose(disposing);
            this.Disposed = true;
        }

        /// <summary>
        /// Caso o contexto tenha sido descartado lança uma exceção informando que não pode mais realizar nenhuma operação
        /// </summary>
        private void isDisposed()
        {
            if (this.Disposed)
                throw new InvalidOperationException("O Contexto foi descartado");
        }
        #endregion
    }
}
