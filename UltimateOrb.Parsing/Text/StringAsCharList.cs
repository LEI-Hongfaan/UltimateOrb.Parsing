using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UltimateOrb.Parsing.Text {

    internal readonly struct StringAsCharList : IReadOnlyList<char> {

        public readonly string Value;

        public char this[int index] {

            [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
            get => Value[index];
        }

        public int Count {

            [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
            get => Value.Length;
        }

        public IEnumerator<char> GetEnumerator() {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }

        public static implicit operator string(StringAsCharList value) {
            return value.Value;
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public StringAsCharList(string value) {
            this.Value = value;
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StringAsCharList(string value) {
            return new StringAsCharList(value);
        }
    }
    
}
