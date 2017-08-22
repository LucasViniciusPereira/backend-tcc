using backend_tcc.lib.repository.ef.Class;
using backend_tcc.lib.repository.ef.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace backend_tcc.lib.repository.ef.Audit
{
    /// <summary>
    /// Classe responsável por realizar auditoria das entidades
    /// </summary>
    public class AuditEntries<TDbContext> : IAuditorEntries where TDbContext : DbContext
    {
        public AuditEntries(GenericEntities<TDbContext> ctx)
        {
            this.Context = ctx;
        }

        /// <summary>
        /// Tipos que são auditáveis
        /// </summary>
        private static List<AuditableProperties> _TiposAuditaveis = null;

        /// <summary>
        /// Context Entities
        /// </summary>
        private GenericEntities<TDbContext> Context = null;

        /// <summary>
        /// Serializador JavaScript
        /// </summary>
        protected ISerializeJavaScript Serializer { get { return Context.Serializer; } }

        /// <summary>
        /// Pattern MonoState
        /// </summary>
        public List<AuditableProperties> TiposAuditaveis
        {
            get
            {
                return _TiposAuditaveis ?? (_TiposAuditaveis = GetTiposAuditaveis());
            }

        }

        public static string GetTableName(Type type, DbContext context)
        {
            try
            {
                var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

                var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

                var entityType = metadata.GetItems<EntityType>(DataSpace.OSpace).Single(e => objectItemCollection.GetClrType(e) == type);

                var entitySet = metadata.GetItems(DataSpace.CSpace).Where(x => x.BuiltInTypeKind == BuiltInTypeKind.EntityType).Cast<EntityType>().Single(x => x.Name == entityType.Name);

                var entitySetMappings = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace).Single().EntitySetMappings.ToList();



                EntitySet table;

                var mapping = entitySetMappings.SingleOrDefault(x => x.EntitySet.Name == entitySet.Name);

                if (mapping != null)
                {

                    table = mapping.EntityTypeMappings.Single().Fragments.Single().StoreEntitySet;

                }

                else
                {

                    mapping = entitySetMappings.SingleOrDefault(x => x.EntityTypeMappings.Where(y => y.EntityType != null).Any(y => y.EntityType.Name == entitySet.Name));

                    if (mapping != null)
                    {

                        table = mapping.EntityTypeMappings.Where(x => x.EntityType != null).Single(x => x.EntityType.Name == entityType.Name).Fragments.Single().StoreEntitySet;

                    }

                    else
                    {

                        var entitySetMapping = entitySetMappings.Single(x => x.EntityTypeMappings.Any(y => y.IsOfEntityTypes.Any(z => z.Name == entitySet.Name)));

                        table = entitySetMapping.EntityTypeMappings.First(x => x.IsOfEntityTypes.Any(y => y.Name == entitySet.Name)).Fragments.Single().StoreEntitySet;

                    }

                }



                return (string)table.MetadataProperties["Table"].Value ?? table.Name;


            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Recupera os tipos que serão auditados
        /// </summary>
        /// <returns></returns>
        private List<AuditableProperties> GetTiposAuditaveis()
        {
            try
            {
                List<AuditableProperties> tiposAuditaveis = new List<AuditableProperties>();

                //retorna todas as propriedades das entidades para descobrir qual é o schema padrão
                var ssSpaceSet = ((IObjectContextAdapter)Context).ObjectContext.MetadataWorkspace
                    .GetItems<EntityContainer>(DataSpace.SSpace).First()
                    .BaseEntitySets;

                foreach (var item in Context.GetType().GetProperties())
                {
                    // Using reflection.
                    var attrs = item.GetCustomAttributes(false);

                    foreach (Auditable attr in attrs.Where(d => d is Auditable))
                    {
                        try
                        {
                            Auditable a = (Auditable)attr;

                            //Verifica se utilizou Auditable em DbSet<T>
                            if (item.PropertyType.IsGenericType)
                            {
                                //Pega o T de DbSet<T>
                                var name = item.PropertyType.GetGenericArguments().First().Name;

                                var fullname = item.PropertyType.GetGenericArguments().First().AssemblyQualifiedName;

                                var type = Type.GetType(item.PropertyType.GetGenericArguments().First().AssemblyQualifiedName);

                                var cSpaceSet = ((IObjectContextAdapter)Context).ObjectContext.MetadataWorkspace
                                    .GetItems<EntityType>(DataSpace.CSpace)
                                    .ToList();


                                //Pega o schema table
                                var cspaceEntityType = cSpaceSet
                                    .First(meta => meta.Name == name);

                                var tableName = GetTableName(item.PropertyType.GetGenericArguments().First(), Context);

                                //Pega o schema table
                                var schema = ssSpaceSet
                                    .FirstOrDefault(meta => meta.ElementType.Name == tableName);

                                if (schema == null)
                                    //throw new ApplicationException(string.Format("Não foi possível encontrar schema ssSpaceSet para o elemento {0}", name));
                                    continue;

                                //monta tabela hash
                                tiposAuditaveis.Add(new AuditableProperties()
                                {
                                    TypeClass = a.Tipo,
                                    TableName = string.Format("{0}.{1}", schema.Schema, schema.Table),
                                    KeyNameSSpace = string.Join(",", schema.ElementType.KeyProperties),
                                    KeyNameCSpace = string.Join(",", cspaceEntityType.KeyProperties)
                                }
                                    );
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }

                return tiposAuditaveis;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GetKeyName(Type tipo)
        {
            if (tipo.BaseType == null)
                return null;

            string keyname = null;

            var key = tipo.GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0);

            if (key.Any() == false)
                keyname = GetKeyName(tipo.BaseType);

            return keyname;
        }

        /// <summary>
        /// Cria os objetos de Auditoria conforme as alterações realizadas em um DbEntityEntry
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<AuditLog> GetAuditRecordsForChange(Guid Transaction, DbEntityEntry dbEntry, string userId)
        {
            try
            {

                List<AuditLog> result = new List<AuditLog>();

                DateTimeOffset changeTime = DateTimeOffset.Now;

                var attribute = TiposAuditaveis.Single(c => c.TypeClass == dbEntry.GetPocoType());

                // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
                string tableName = attribute.TableName;

                //Get PkName
                string keyName = attribute.KeyNameSSpace; //.GetProperties().SingleOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;

                //Get PkValue            
                if (dbEntry.State == EntityState.Added || dbEntry.State == EntityState.Deleted)
                {
                    // For Inserts, just add the whole record
                    // If the entity implements IDescribableEntity, use the description from Describe(), otherwise use ToString()

                    var auditLog = new AuditLog(Transaction)
                    {
                        AuditLogID = Guid.NewGuid(),
                        UserID = userId,
                        EventDateUTC = changeTime,
                        TableName = tableName,
                        ColumnName = "*ALL",    // Or make it nullable, whatever you want                                
                    };

                    if (dbEntry.State == EntityState.Added)
                    {
                        auditLog.RecordID = dbEntry.CurrentValues.GetKeyValue(attribute);
                        auditLog.EventType = "A";//A-Added, D-Deleted
                        auditLog.NewValue = Serializer.Serialize(dbEntry.CurrentValues.ToObject());
                    }
                    else
                    {
                        auditLog.RecordID = dbEntry.OriginalValues.GetKeyValue(attribute);
                        auditLog.EventType = "D";//A-Added, D-Deleted
                        auditLog.NewValue = Serializer.Serialize(dbEntry.OriginalValues.ToObject());
                    }


                    result.Add(auditLog);
                }
                else if (dbEntry.State == EntityState.Modified)
                {
                    var recordID = dbEntry.CurrentValues.GetKeyValue(attribute);

                    foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                    {
                        // For updates, we only want to capture the columns that actually changed
                        if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
                        {
                            result.Add(new AuditLog(Transaction)
                            {
                                AuditLogID = Guid.NewGuid(),
                                UserID = userId,
                                EventDateUTC = changeTime,
                                EventType = "M",    // Modified
                                TableName = tableName,
                                RecordID = recordID,
                                ColumnName = propertyName,
                                OriginalValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
                                NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString()
                            }
                                );
                        }
                    }
                }
                // Otherwise, don't do anything, we don't care about Unchanged or Detached entities

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Cria a auditoria conforme o estado dos objetos do contexto
        /// </summary>
        /// <param name="userId"></param>
        public void Audit(string userId)
        {
            try
            {
                if (TiposAuditaveis.Any())
                {
                    Guid Transaction = Guid.NewGuid();

                    var result = Context.ChangeTracker
                        .Entries()
                        .Where(p => (p.State == EntityState.Added ||
                                    p.State == EntityState.Deleted ||
                                    p.State == EntityState.Modified) && TiposAuditaveis.IsAuditable(p))
                                    .ToList();

                    var auditEntries = result
                        .SelectMany(c => GetAuditRecordsForChange(Transaction, c, userId))
                        .ToList();

                    // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
                    foreach (var auditEntry in auditEntries)
                    {
                        // For each changed record, get the audit record entries and add them
                        Context.AuditLog.Add(auditEntry);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Falta configurar AuditLog. Sobrescreva o método ConfigureAudit para configuração do contexto do EF.", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
