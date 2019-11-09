using System;

namespace ShoppingBasket.SharedKernel
{
    public abstract class BaseEntity<TId, T> : BaseEntityEquality<TId, T> where T : BaseEntityEquality<TId, T>
    {
        public override bool Equals(T other) => this.Id.Equals(other.Id);

        protected BaseEntity() : base() { }

        public BaseEntity(TId id) : base(id) { }
    }
    public abstract class BaseEntityEquality<TId, T> where T : class
    {
        public TId Id { get; protected set; }

        protected BaseEntityEquality() { }

        protected BaseEntityEquality(TId id)
        {
            if (id == null || Object.Equals(id, default(TId))) throw new ArgumentException("Id must be non-null, non-default value.");
            Id = id;
        }

        public abstract bool Equals(T other);

        public override bool Equals(object obj)
        {
            T other = obj as T;
            return (other != null) ?
                this.Equals(other) :
                false;
        }

        public static bool operator ==(BaseEntityEquality<TId, T> one, BaseEntityEquality<TId, T> other) => one.Equals(other);
        public static bool operator !=(BaseEntityEquality<TId, T> one, BaseEntityEquality<TId, T> other) => !one.Equals(other);

        public override int GetHashCode() => Id.GetHashCode();
    }
}