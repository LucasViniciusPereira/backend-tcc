using System;
using System.Collections.Generic;

namespace backend_tcc.lib.repository.ef.Class
{
    /// <summary>
    /// Pattern Composite para execução de múltiplos comandos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CompositeFluentApi<T> : ICommand where T : CompositeFluentApi<T>
    {
        #region Fields
        private List<ICommand> LstCommand = new List<ICommand>();
        #endregion

        #region Methods Composite
        public void Add(CompositeFluentApi<T> command)
        {
            this.LstCommand.Add(command);
        }

        public void Dispose()
        {
            this.Dispose();
        }
        #endregion

        #region Methods
        public void Execute()
        {
            try
            {
                foreach (var cmd in LstCommand)
                {
                    cmd.Execute();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}