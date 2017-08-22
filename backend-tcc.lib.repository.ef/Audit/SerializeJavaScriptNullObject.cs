using backend_tcc.lib.repository.ef.Interface;
using System;

namespace backend_tcc.lib.repository.ef.Audit
{
    /// <summary>
    /// Null Object pattern
    /// </summary>
    internal class SerializeJavaScriptNullObject : ISerializeJavaScript
    {
        private static SerializeJavaScriptNullObject instance = new SerializeJavaScriptNullObject();

        /// <summary>
        /// Pattern singleton
        /// </summary>
        public static SerializeJavaScriptNullObject Instance { get { return instance; } }

        public string Serialize(object obj)
        {
            throw new ApplicationException("Será necessário Atribuir a Propriedade Serializer de GenericEntities<TDbContext> com uma instância válida. Crie uma classe e Implemente a interface ISerializeJavaScript.");
        }
    }
}
