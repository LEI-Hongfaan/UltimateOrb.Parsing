using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UltimateOrb.Parsing {

    public readonly struct Union<T1, T2> : IEquatable<Union<T1, T2>> {

        public readonly int Case;

        private readonly object _Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Union(T1 value) : this() {
            Case = 1;
            _Value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Union(T2 value) : this() {
            Case = 2;
            _Value = value;
        }

        public T1 Value1 {

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                if (1 == Case) {
                    return (T1)_Value;
                }
                throw ThrowHelper.ThrowInvalidOperationException();
            }
        }

        public T2 Value2 {

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                if (2 == Case) {
                    return (T2)_Value;
                }
                throw ThrowHelper.ThrowInvalidOperationException();
            }
        }

        public override bool Equals(object obj) {
            return obj is Union<T1, T2> union && this.Equals(union);
        }

        public bool Equals(Union<T1, T2> other) {
            return this.Case == other.Case &&
                   EqualityComparer<object>.Default.Equals(this._Value, other._Value);
        }

        public override int GetHashCode() {
            var hashCode = 15302504;
            hashCode = hashCode * -1521134295 + this.Case.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(this._Value);
            return hashCode;
        }

        public override string ToString() {
            return 0 == Case ? null : $@"({_Value}:{Case})";
        }

        public static bool operator ==(Union<T1, T2> left, Union<T1, T2> right) {
            return left.Equals(right);
        }

        public static bool operator !=(Union<T1, T2> left, Union<T1, T2> right) {
            return !(left == right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Union<T1, T2>(T1 value) {
            return new Union<T1, T2>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Union<T1, T2>(T2 value) {
            return new Union<T1, T2>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator T1(Union<T1, T2> value) {
            if (1 == value.Case) {
                return (T1)value._Value;
            }
            throw ThrowHelper.ThrowInvalidCastException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator T2(Union<T1, T2> value) {
            if (2 == value.Case) {
                return (T2)value._Value;
            }
            throw ThrowHelper.ThrowInvalidCastException();
        }
    }
}
