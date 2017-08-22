using System;

namespace backend_tcc.lib.repository.Class
{
    public abstract class Disposable : IDisposable
    {
        public bool isDisposed { get; private set; }

        ~Disposable()
        {
            Dispose(false);
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }

        protected virtual void DisposeCore()
        {
        }
    }
}
