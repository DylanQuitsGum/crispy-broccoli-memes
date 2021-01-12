using System;

namespace gsm.IoC.Events
{
    public interface ISubscriptionToken : IEquatable<ISubscriptionToken>, IDisposable
    {
        Guid Token { get; }
    }
}
