using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Activation
{
    public abstract class ActivationHandler
    {
        public abstract bool CanHandle(object args);

        public abstract Task HandleAsync(object args);
    }
}
