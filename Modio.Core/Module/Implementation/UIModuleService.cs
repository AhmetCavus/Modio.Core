namespace Modio.Core.Module
{
    public abstract class UIModuleService : BaseModuleService
    {
        public override bool IsUI => true;
        public override bool IsWorker => false;
    }
}
