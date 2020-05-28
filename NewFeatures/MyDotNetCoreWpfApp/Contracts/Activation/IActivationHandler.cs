﻿using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Contracts.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle(object args);

        Task HandleAsync(object args);
    }

    // Extend this class to implement new ActivationHandlers
    public abstract class IActivationHandler<T> : IActivationHandler
        where T : class
    {
        // Override this method to add the activation logic in your activation handler
        protected abstract Task HandleInternalAsync(T args);

        public async Task HandleAsync(object args)
        {
            await HandleInternalAsync(args as T);
        }

        public bool CanHandle(object args)
        {
            // CanHandle checks the args is of type you have configured
            return args is T && CanHandleInternal(args as T);
        }

        // You can override this method to add extra validation on activation args
        // to determine if your ActivationHandler should handle this activation args
        protected virtual bool CanHandleInternal(T args)
        {
            return true;
        }
    }
}
