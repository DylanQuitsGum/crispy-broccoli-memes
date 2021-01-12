using System;

namespace gsm.IoC.Events
{
    public interface IEventAggregator
    {
        TEventType GetEvent<TEventType>() where TEventType : IEventBase, new();
    }
}
