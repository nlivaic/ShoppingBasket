using System;
using System.Collections.Generic;
using System.Reflection;

namespace AddressBook.SharedKernel
{
    public abstract class BaseValueObject<T> where T : class
    {
        protected BaseValueObject() { }

        private IEnumerable<FieldInfo> GetFields()
        {
            List<FieldInfo> fields = new List<FieldInfo>();
            Type type = this.GetType();
            while (type != typeof(object))
            {
                fields.AddRange(type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
                type = type.BaseType;
            }
            return fields;
        }

        public override bool Equals(object obj)
        {
            T other = obj as T;
            if (other != null)
            {
                return this.Equals(other);
            }
            return false;
        }

        public abstract bool Equals(T other);

        public static bool operator ==(BaseValueObject<T> one, BaseValueObject<T> other) => one.Equals(other);
        public static bool operator !=(BaseValueObject<T> one, BaseValueObject<T> other) => !one.Equals(other);

        public override int GetHashCode()
        {
            int hashCode = 17;
            int multiplier = 59;
            IEnumerable<FieldInfo> fields = GetFields();

            foreach (FieldInfo property in fields)
            {
                object value = property.GetValue(this);
                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }
            return hashCode;
        }
    }
}