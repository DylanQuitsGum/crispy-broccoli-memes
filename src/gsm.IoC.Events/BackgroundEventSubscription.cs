using System;
using System.Threading.Tasks;

namespace gsm.IoC.Events
{
    public class BackgroundEventSubscription : EventSubscription
    {
        public BackgroundEventSubscription(IDelegateReference actionReference)
            : base(actionReference)
        {
        }

        public override void InvokeAction(Action action)
        {
            Task.Run(action);
        }
    }

}
