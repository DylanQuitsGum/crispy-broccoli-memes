using System;
using System.Linq;

using gsm.IoC.Events.Properties;

namespace gsm.IoC.Events
{
    public class PubSubEvent : EventBase
    {
        public ISubscriptionToken Subscribe(Action action)
        {
            return Subscribe(action, ThreadOption.PublisherThread);
        }

        public ISubscriptionToken Subscribe(Action action, ThreadOption threadOption)
        {
            return Subscribe(action, threadOption, false);
        }

        public ISubscriptionToken Subscribe(Action action, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, ThreadOption.PublisherThread, keepSubscriberReferenceAlive);
        }

        public virtual ISubscriptionToken Subscribe(Action action, ThreadOption threadOption, bool keepSubscriberReferenceAlive)
        {
            IDelegateReference actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);

            EventSubscription subscription;
            switch (threadOption)
            {
                case ThreadOption.PublisherThread:
                    subscription = new EventSubscription(actionReference);
                    break;
                case ThreadOption.BackgroundThread:
                    subscription = new BackgroundEventSubscription(actionReference);
                    break;
                case ThreadOption.UIThread:
                    if (SynchronizationContext == null) throw new InvalidOperationException(Resources.EventAggregatorNotConstructedOnUIThread);
                    subscription = new DispatcherEventSubscription(actionReference, SynchronizationContext);
                    break;
                default:
                    subscription = new EventSubscription(actionReference);
                    break;
            }

            return InternalSubscribe(subscription);
        }


        public virtual void Publish()
        {
            InternalPublish();
        }

        public virtual void Unsubscribe(Action subscriber)
        {
            lock (EventSubscriptions)
            {
                IEventSubscription eventSubscription = EventSubscriptions.Cast<EventSubscription>().FirstOrDefault(evt => evt.Action == subscriber);
                if (eventSubscription != null)
                {
                    EventSubscriptions.Remove(eventSubscription);
                }
            }
        }

        public virtual bool Contains(Action subscriber)
        {
            IEventSubscription eventSubscription;

            lock (EventSubscriptions)
            {
                eventSubscription = EventSubscriptions.Cast<EventSubscription>().FirstOrDefault(evt => evt.Action == subscriber);
            }

            return eventSubscription != null;
        }
    }

}
