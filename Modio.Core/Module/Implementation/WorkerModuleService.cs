using System;

namespace Modio.Core.Module
{
    public abstract class WorkerModuleService : BaseModuleService
    {
        public event EventHandler<EventArgs> JobFinished;
        public event EventHandler<Exception> JobFailed;

        public override bool IsUI => false;
        public override bool IsWorker => true;

        public void DoJob()
        {
            try
            {
                OnDoJob();
                JobFinished?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception err)
            {
                JobFailed?.Invoke(this, err);
                throw;
            }
        }

        protected abstract void OnDoJob();
    }
}