using System.Collections.Generic;
using System.ComponentModel;

namespace KnockAdm
{
    /// <summary>
    /// Wraps generic type with an additional validation information
    /// </summary>
    public class Validated<T>
    {
        public Validated(T val)
        {
            Value = val;
        }

        /// <summary>
        /// Created object will be valid if conversion of string <paramref name="rawValue"/> to generic type will succeed
        /// </summary>
        public Validated(string rawValue)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.IsValid(rawValue))
            {
                IsValid = true;
                Value = (T) converter.ConvertFromString(rawValue);
            }
            else
            {
                IsValid = false;
                Value = default(T);
            }
        }

        public T Value { get; private set; }

        /// <summary>
        /// Equals true, if provided value was valid when object was created
        /// </summary>
        public bool IsValid { get; private set; }

        public static implicit operator Validated<T>(T val)
        {
            return new Validated<T>(val);
        }

        public static implicit operator Validated<T>(string rawValue)
        {
            var converter = TypeDescriptor.GetConverter(typeof (T));

            if (converter.IsValid(rawValue))
            {
                return new Validated<T>((T) converter.ConvertFromString(rawValue)) {IsValid = true};
            }
            return new Validated<T>(default(T)) {IsValid = false};
        }

        public static implicit operator T(Validated<T> val)
        {
            return val.Value;
        }

        public override string ToString()
        {
            return Value == null ? string.Empty : Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null && Value != null)
            {
                return false;
            }
            if (obj != null && obj.GetType() != typeof (T))
            {
                return false;
            }
            return EqualityComparer<T>.Default.Equals(Value, (T) obj);
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }
    }
}