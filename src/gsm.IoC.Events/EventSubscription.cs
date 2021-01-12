using System;
using System.Globalization;

using gsm.IoC.Events.Properties;

namespace gsm.IoC.Events
{
    public class EventSubscription : IEventSubscription
    {
        private readonly IDelegateReference _actionReference;

        public EventSubscription(IDelegateReference actionReference)
        {
            if (actionReference == null)
                throw new ArgumentNullException(nameof(actionReference));
            if (!(actionReference.Target is Action))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.InvalidDelegateRerefenceTypeException, typeof(Action).FullName), nameof(actionReference));

            _actionReference = actionReference;
        }

        public Action Action
        {
            get { return (Action)_actionReference.Target; }
        }

        public ISubscriptionToken SubscriptionToken { get; set; }


        public virtual Action<object[]> GetExecutionStrategy()
        {
            Action action = this.Action;
            if (action != null)
            {
                return arguments =>
                {
                    InvokeAction(action);
                };
            }
            return null;
        }

        public virtual void InvokeAction(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            action();
        }
    }

}
