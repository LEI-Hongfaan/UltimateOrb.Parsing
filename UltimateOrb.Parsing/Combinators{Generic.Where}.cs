using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserWhereImpl<TChar, TSource> Where<TChar, TSource>(this IParser<TChar, TSource> source, Func<TSource, bool> predicate) {
            return new ParserWhereImpl<TChar, TSource>(source, predicate);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserWhereImpl<TChar, TSource>
        : IParser<TChar, TSource> {
        private readonly IParser<TChar, TSource> source;
        private readonly Func<TSource, bool> predicate;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserWhereImpl(IParser<TChar, TSource> source, Func<TSource, bool> predicate) {
            this.source = source;
            this.predicate = predicate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TSource Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var enumerator = source.Parse(input, position);
            for (; enumerator.MoveNext();) {
                var current = enumerator.Current;
                if (predicate.Invoke(current.Result)) {
                    yield return current;
                }
            }
            enumerator.Dispose();
        }
    }
}