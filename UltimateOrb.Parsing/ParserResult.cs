using System;
using System.Collections.Generic;

namespace UltimateOrb.Parsing.Generic {

    /*
    public readonly partial struct ParserResult<T> : IEquatable<ParserResult<T>> {

        public readonly int Position;

        public readonly T Result;

        public ParserResult(int position, T result) {
            this.Position = position;
            this.Result = result;
        }

        public override bool Equals(object obj) {
            return obj is ParserResult<T> result && this.Equals(result);
        }

        public bool Equals(ParserResult<T> other) {
            return this.Position == other.Position &&
                   EqualityComparer<T>.Default.Equals(this.Result, other.Result);
        }

        public override int GetHashCode() {
            var hashCode = 921582846;
            hashCode = hashCode * -1521134295 + this.Position.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(this.Result);
            return hashCode;
        }

        public override string ToString() {
            return (this.Result, this.Position).ToString();
        }

        public static bool operator ==(ParserResult<T> left, ParserResult<T> right) {
            return left.Equals(right);
        }

        public static bool operator !=(ParserResult<T> left, ParserResult<T> right) {
            return !(left == right);
        }

        public void Deconstruct(out T result, out int position) {
            result = Result;
            position = Position;
        }
    }
    */
}
