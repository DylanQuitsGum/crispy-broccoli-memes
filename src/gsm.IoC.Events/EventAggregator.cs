using System;
using System.Threading;
using System.Collections.Generic;

namespace gsm.IoC.Events
{
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, IEventBase> events = new Dictionary<Type, IEventBase>();

        private readonly SynchronizationContext syncContext = SynchronizationContext.Current;

        public TEventType GetEvent<TEventType>() 
            where TEventType : IEventBase, new()
        {
            lock (events)
            {

                if (!events.TryGetValue(typeof(TEventType), out var existingEvent))
                {
                    var newEvent = new TEventType();
                    newEvent.SynchronizationContext = syncContext;
                    events[typeof(TEventType)] = newEvent;

                    return newEvent;
                }
                else
                {
                    return (TEventType)existingEvent;
                }
            }
        }
    }
}
