using System;
using System.Threading;

namespace PriceListWcfService.Utilities
{
    public class MutexLocker : IDisposable
    {
        private Mutex mMutex;

        public MutexLocker(String mutexName)
        {
            this.mMutex = new Mutex(false, mutexName);
            this.mMutex.WaitOne();
        }

        public void Dispose()
        {
            try
            {
                this.mMutex.ReleaseMutex();
            }
            finally
            {
                try
                {
                    this.mMutex.Dispose();
                }
                finally
                {
                    this.mMutex = null;
                }
            }
        }
    } // class MutexLocker
} // namespace PriceListWcfService.Utility
