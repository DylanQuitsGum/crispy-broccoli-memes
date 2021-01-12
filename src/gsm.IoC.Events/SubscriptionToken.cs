using System;

namespace gsm.IoC.Events
{
    public class SubscriptionToken : ISubscriptionToken
    {
        public Guid Token { get; }

        private Action<ISubscriptionToken> _unsubscriptionAction;

        public SubscriptionToken(Action<ISubscriptionToken> unsubscriptionAction)
        {
            _unsubscriptionAction = unsubscriptionAction ?? throw new ArgumentNullException(nameof(unsubscriptionAction));
            Token = Guid.NewGuid();
        }

        public bool Equals(ISubscriptionToken other)
        {
            var equals = false;

            if (other != null)
            {
                equals = Equals(Token, other.Token);
            }

            return equals;
        }

        public override bool Equals(object obj)
        {
            var equals = ReferenceEquals(this, obj) || Equals((SubscriptionToken)obj);

            return equals;
        }

        protected bool Equals(SubscriptionToken other)
        {
            return Equals(_unsubscriptionAction, other._unsubscriptionAction) && Token.Equals(other.Token);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_unsubscriptionAction != null ? _unsubscriptionAction.GetHashCode() : 0) * 397) ^ Token.GetHashCode();
            }
        }

        public void Dispose()
        {
            if (_unsubscriptionAction != null)
            {
                _unsubscriptionAction(this);
                _unsubscriptionAction = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
