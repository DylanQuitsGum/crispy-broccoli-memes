using System;
using System.Threading;

namespace gsm.IoC.Events
{
    public interface IEventBase
    {
        bool Contains(ISubscriptionToken subscriptionToken);

        void Unsubscribe(ISubscriptionToken subscriptionToken);

        SynchronizationContext SynchronizationContext { get; set; }
    }
}
