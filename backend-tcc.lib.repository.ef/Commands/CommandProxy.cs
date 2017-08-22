using System;
using System.Diagnostics;

namespace backend_tcc.lib.repository.ef.Class
{
    /// <summary>
    /// Proxy para extender funcionalidade de CommandFluentAPI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommandProxy<T> : ICommand where T : CommandBase
    {
        #region Fields
        private CommandBase Command;
        public event EventHandler OnBegin;
        public event EventHandler OnEnd;
        #endregion

        #region Properties
        public TimeSpan ExecutionTime { get; private set; }
        #endregion

        #region Constructor
        public CommandProxy(CommandBase cmd)
        {
            this.Command = cmd;
        }
        #endregion

        #region Fluent Api Methods
        public CommandProxy<T> SetOnBegin(EventHandler e)
        {
            this.OnBegin = e;
            return this;
        }

        public CommandProxy<T> SetOnEnd(EventHandler e)
        {
            this.OnEnd = e;
            return this;
        }
        #endregion

        #region Methods
        public void Execute()
        {
            if (OnBegin != null)
                OnBegin(Command, EventArgs.Empty);

            Stopwatch watch = Stopwatch.StartNew();
            this.Command.Execute();
            watch.Stop();
            ExecutionTime = watch.Elapsed;

            if (OnEnd != null)
                OnEnd(Command, EventArgs.Empty);

        }

        public void Dispose()
        {
            this.Dispose();
        }
        #endregion
    }
}
