﻿using System;
using System.Reflection;

namespace gsm.IoC.Events
{
    public class DelegateReference : IDelegateReference
    {
        private readonly Delegate _delegate;
        private readonly WeakReference _weakReference;
        private readonly MethodInfo _method;
        private readonly Type _delegateType;

        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            if (@delegate == null) throw new ArgumentNullException(nameof(@delegate));

            if (keepReferenceAlive)
            {
                this._delegate = @delegate;
            }
            else
            {
                _weakReference = new WeakReference(@delegate.Target);
                _method = @delegate.GetMethodInfo();
                _delegateType = @delegate.GetType();
            }
        }

        public Delegate Target
        {
            get
            {
                if (_delegate != null)
                {
                    return _delegate;
                }
                else
                {
                    return TryGetDelegate();
                }
            }
        }

        private Delegate TryGetDelegate()
        {
            if (_method.IsStatic)
            {
                return _method.CreateDelegate(_delegateType, null);
            }
            object target = _weakReference.Target;
            if (target != null)
            {
                return _method.CreateDelegate(_delegateType, target);
            }
            return null;
        }
    }

}
