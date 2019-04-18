using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserOptImpl<TChar, TResult> Opt<TChar, TResult>(this IParser<TChar, TResult> parser) {
            return new ParserOptImpl<TChar, TResult>(parser);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserOptImpl2<TChar, TResult> Opt<TChar, TResult>(this IParser<TChar, TResult> parser, TResult defaultValue) {
            return new ParserOptImpl2<TChar, TResult>(parser, defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserOptImpl2<TChar, TSource, TResult> Opt<TChar, TSource, TResult>(this IParser<TChar, TSource> parser, Func<Optional<TSource>, TResult> selector) {
            return new ParserOptImpl2<TChar, TSource, TResult>(parser, selector);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct ParserOptImpl<TChar, TResult>
        : IParser<TChar, Optional<TResult>> {

        private readonly IParser<TChar, TResult> parser;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserOptImpl(IParser<TChar, TResult> parser) {
            this.parser = parser;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(Optional<TResult> Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            yield return (default, position);
            var enumerator = parser.Parse(input, position);
            for (; enumerator.MoveNext();) {
                yield return enumerator.Current;
            }
            enumerator.Dispose();
        }
    }

    public readonly partial struct ParserOptImpl2<TChar, TResult>
       : IParser<TChar, TResult> {

        private readonly IParser<TChar, TResult> parser;

        private readonly TResult defaultValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserOptImpl2(IParser<TChar, TResult> parser, TResult defaultValue) {
            this.parser = parser;
            this.defaultValue = defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            yield return (defaultValue, position);
            var enumerator = parser.Parse(input, position);
            for (; enumerator.MoveNext();) {
                yield return enumerator.Current;
            }
            enumerator.Dispose();
        }
    }

    public readonly partial struct ParserOptImpl2<TChar, TSource, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TSource> parser;

        private readonly Func<Optional<TSource>, TResult> selector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserOptImpl2(IParser<TChar, TSource> parser, Func<Optional<TSource>, TResult> selector) {
            this.parser = parser;
            this.selector = selector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            yield return (selector.Invoke(default), position);
            var enumerator = parser.Parse(input, position);
            for (; enumerator.MoveNext();) {
                var current = enumerator.Current;
                yield return (selector.Invoke(current.Result), current.Position);
            }
            enumerator.Dispose();
        }
    }
}