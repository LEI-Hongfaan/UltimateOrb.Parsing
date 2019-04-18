using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectImpl<TChar, TSource, TResult> Select<TChar, TSource, TResult>(this IParser<TChar, TSource> source, Func<TSource, TResult> selector) {
            return new ParserSelectImpl<TChar, TSource, TResult>(source, selector);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserSelectImpl<TChar, TSource, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TSource> source;

        private readonly Func<TSource, TResult> selector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectImpl(IParser<TChar, TSource> source, Func<TSource, TResult> selector) {
            this.source = source;
            this.selector = selector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var enumerator = source.Parse(input, position);
            for (; enumerator.MoveNext();) {
                var current = enumerator.Current;
                yield return (selector.Invoke(current.Result), current.Position);
            }
            enumerator.Dispose();
        }
    }
}