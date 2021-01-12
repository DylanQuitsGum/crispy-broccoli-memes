using System;

namespace gsm.IoC.Events
{
    public interface IEventSubscription
    {
        ISubscriptionToken SubscriptionToken { get; set; }

        Action<object[]> GetExecutionStrategy();
    }
}
