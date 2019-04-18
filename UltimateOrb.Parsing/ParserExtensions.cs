using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace UltimateOrb.Parsing {

    public static class ParserExtensions {

        private readonly struct EnumeratorAsEnumerable<T>
            : IEnumerable<T> {

            internal readonly IEnumerator<T> value;

            public EnumeratorAsEnumerable(IEnumerator<T> value) {
                this.value = value;
            }

            public IEnumerator<T> GetEnumerator() {
                return value;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
                return value;
            }
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> value) {
            return new EnumeratorAsEnumerable<T>(value);
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static bool Any<TResult>(this IEnumerator<(TResult Result, int Position)> parseResults) {
            return parseResults.AsEnumerable().Any();
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static TResult SingleResult<TResult>(this IEnumerator<(TResult Result, int Position)> parserResults) {
            return parserResults.AsEnumerable().Select(m => m.Result).Single();
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> Results<TResult>(this IEnumerator<(TResult Result, int Position)> parserResults) {
            return parserResults.AsEnumerable().Select(m => m.Result);
        }
    }
}
