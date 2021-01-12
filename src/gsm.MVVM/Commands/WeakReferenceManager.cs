using System;
using System.Threading;
using System.Collections.Generic;

using gsm.MVVM.Extensions;

namespace gsm.MVVM.Commands
{
    internal class WeakReferenceManager
    {
        private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        private readonly List<WeakReference> _handlers = new List<WeakReference>();

        internal void CallHandlers(object sender)
        {
            foreach (var handler in _handlers)
            {
                Call(sender, handler.Target as EventHandler);
            }
        }

        private void Call(object sender, EventHandler handler)
        {
            if (_synchronizationContext != null && _synchronizationContext != SynchronizationContext.Current)
            {
                _synchronizationContext.Post((o) => handler?.Invoke(sender, EventArgs.Empty), null);
            }
            else
            {
                handler?.Invoke(sender, EventArgs.Empty);
            }
        }

        internal void Add(EventHandler handler)
        {
            _handlers.Add(new WeakReference(handler ?? throw new ArgumentNullException(nameof(handler))));
        }

        internal void Remove(EventHandler handler)
        {
            if (handler == null) return;

            _handlers.RemoveWhere(val => val.Target as EventHandler == handler);
        }
    }
}
