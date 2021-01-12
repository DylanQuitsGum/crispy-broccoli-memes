using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace gsm.IoC.Events
{
    public abstract class EventBase : IEventBase
    {
        private IList<IEventSubscription> _eventSubscriptions;
        protected IList<IEventSubscription> EventSubscriptions => _eventSubscriptions ??= new List<IEventSubscription>();

        protected virtual void InternalPublish(params object[] arguments)
        {
            var executionStrategies = PruneAndReturnStrategies();

            foreach (var executionStrategy in executionStrategies)
            {
                executionStrategy(arguments);
            }
        }

        protected virtual ISubscriptionToken InternalSubscribe(IEventSubscription eventSubscription)
        {
            if (eventSubscription == null) throw new ArgumentNullException(nameof(eventSubscription));

            eventSubscription.SubscriptionToken = new SubscriptionToken(Unsubscribe);

            lock (EventSubscriptions)
            {
                EventSubscriptions.Add(eventSubscription);
            }

            return eventSubscription.SubscriptionToken;
        }

        public virtual bool Contains(ISubscriptionToken subscriptionToken)
        {
            lock (EventSubscriptions)
            {
                var subscription = EventSubscriptions.FirstOrDefault(val => val.SubscriptionToken == subscriptionToken);
                return subscription != null;
            }
        }

        public virtual void Unsubscribe(ISubscriptionToken token)
        {
            var subscription = EventSubscriptions.FirstOrDefault(val => val.SubscriptionToken == token);

            if (subscription != null)
            {
                EventSubscriptions.Remove(subscription);
            }
        }

        private List<Action<object[]>> PruneAndReturnStrategies()
        {
            var strategies = new List<Action<object[]>>();

            lock (EventSubscriptions)
            {
                for (var i = EventSubscriptions.Count - 1; i >= 0; i--)
                {
                    Action<object[]> listItem = _eventSubscriptions[i].GetExecutionStrategy();

                    if (listItem == null)
                    {
                        _eventSubscriptions.RemoveAt(i);
                    }
                    else
                    {
                        strategies.Add(listItem);
                    }
                }
            }

            return strategies;
        }

        public SynchronizationContext SynchronizationContext { get; set; }
    }
}
