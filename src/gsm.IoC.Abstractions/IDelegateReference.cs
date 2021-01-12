using System;

namespace gsm.IoC.Events
{
    public interface IDelegateReference
    {
        Delegate Target { get; }
    }
}
