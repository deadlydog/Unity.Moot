using System;

namespace Assets.Game.Domain
{
    public class EnemyIdentifier : IEquatable<EnemyIdentifier>
    {
        private readonly Guid _value;

        private EnemyIdentifier(Guid value)
        {
            _value = value;
        }

        public EnemyIdentifier() : this(Guid.NewGuid()) {}
        
        public bool Equals(EnemyIdentifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EnemyIdentifier)obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(EnemyIdentifier left, EnemyIdentifier right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EnemyIdentifier left, EnemyIdentifier right)
        {
            return !Equals(left, right);
        }
    }
}