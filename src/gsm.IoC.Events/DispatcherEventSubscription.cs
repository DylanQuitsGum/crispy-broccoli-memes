using System;
using System.Threading;

namespace gsm.IoC.Events
{
    public class DispatcherEventSubscription : EventSubscription
    {
        private readonly SynchronizationContext syncContext;

        public DispatcherEventSubscription(IDelegateReference actionReference, SynchronizationContext context)
            : base(actionReference)
        {
            syncContext = context;
        }

        public override void InvokeAction(Action action)
        {
            syncContext.Post((o) => action(), null);
        }
    }
}
