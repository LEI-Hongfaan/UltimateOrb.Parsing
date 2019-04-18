using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UltimateOrb.Parsing {
    using static ThrowHelper;

    public readonly partial struct Optional<T> : IEquatable<Optional<T>> {

        public readonly bool HasValue;

        public readonly T ValueOrDefault;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Optional(T value) {
            this.HasValue = true;
            this.ValueOrDefault = value;
        }

        public T Value {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                if (HasValue) {
                    return ValueOrDefault;
                }
                throw ThrowInvalidOperationException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator T(Optional<T> value) {
            if (value.HasValue) {
                return value.ValueOrDefault;
            }
            throw ThrowInvalidCastException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Optional<T>(T value) {
            return new Optional<T>(value);
        }

        public static bool operator !=(Optional<T> left, Optional<T> right) {
            return !(left == right);
        }

        public static bool operator ==(Optional<T> left, Optional<T> right) {
            return left.Equals(right);
        }

        public override bool Equals(object obj) {
            return obj is Optional<T> optional && this.Equals(optional);
        }

        public bool Equals(Optional<T> other) {
            return this.HasValue == other.HasValue &&
                   EqualityComparer<T>.Default.Equals(this.ValueOrDefault, other.ValueOrDefault);
        }

        public override int GetHashCode() {
            var hashCode = 1553832704;
            hashCode = hashCode * -1521134295 + this.HasValue.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(this.ValueOrDefault);
            return hashCode;
        }

        public override string ToString() {
            return HasValue ? $@"({ValueOrDefault})" : "()";
        }
    }
}
