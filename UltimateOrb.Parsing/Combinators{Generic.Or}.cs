using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserOrImpl<TChar, TResult1, TResult2> Or<TChar, TResult1, TResult2>(this IParser<TChar, TResult1> parser1, IParser<TChar, TResult2> parser2) {
            return new ParserOrImpl<TChar, TResult1, TResult2>(parser1, parser2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserOrElseUntaggedImpl<TChar, TResult> OrElseUntagged<TChar, TResult>(this IParser<TChar, TResult> parser1, IParser<TChar, TResult> parser2) {
            return new ParserOrElseUntaggedImpl<TChar, TResult>(parser1, parser2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserOrImpl<TChar, TResult1, TResult2, TResult> Or<TChar, TResult1, TResult2, TResult>(this IParser<TChar, TResult1> parser1, IParser<TChar, TResult2> parser2, Func<TResult1, TResult> resultSelector1, Func<TResult2, TResult> resultSelector2) {
            return new ParserOrImpl<TChar, TResult1, TResult2, TResult>(parser1, parser2, resultSelector1 , resultSelector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserOrUntaggedImpl<TChar, TResult> OrUntagged<TChar, TResult>(this IParser<TChar, TResult> parser1, IParser<TChar, TResult> parser2) {
            return new ParserOrUntaggedImpl<TChar, TResult>(parser1, parser2);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserOrImpl<TChar, TResult1, TResult2>
        : IParser<TChar, Union2<TResult1, TResult2>> {

        private readonly IParser<TChar, TResult1> parser1;

        private readonly IParser<TChar, TResult2> parser2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserOrImpl(IParser<TChar, TResult1> parser1, IParser<TChar, TResult2> parser2) {
            this.parser1 = parser1;
            this.parser2 = parser2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(Union2<TResult1, TResult2> Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            {
                var enumerator = parser1.Parse(input, position);
                for (; enumerator.MoveNext();) {
                    yield return enumerator.Current;
                }
                enumerator.Dispose();
            }
            {
                var enumerator = parser2.Parse(input, position);
                for (; enumerator.MoveNext();) {
                    yield return enumerator.Current;
                }
                enumerator.Dispose();
            }
        }
    }

    public readonly struct ParserOrImpl<TChar, TResult1, TResult2, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TResult1> parser1;

        private readonly IParser<TChar, TResult2> parser2;

        private readonly Func<TResult1, TResult> resultSelector1;

        private readonly Func<TResult2, TResult> resultSelector2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserOrImpl(IParser<TChar, TResult1> parser1, IParser<TChar, TResult2> parser2, Func<TResult1, TResult> resultSelector1, Func<TResult2, TResult> resultSelector2) {
            this.parser1 = parser1;
            this.parser2 = parser2;
            this.resultSelector1 = resultSelector1;
            this.resultSelector2 = resultSelector2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            {
                var enumerator = parser1.Parse(input, position);
                for (; enumerator.MoveNext();) {
                    var current = enumerator.Current;
                    yield return (resultSelector1.Invoke( current.Result), current.Position);
                }
                enumerator.Dispose();
            }
            {
                var enumerator = parser2.Parse(input, position);
                for (; enumerator.MoveNext();) {
                    var current = enumerator.Current;
                    yield return (resultSelector2.Invoke(current.Result), current.Position);
                }
                enumerator.Dispose();
            }
        }
    }

    public readonly struct ParserOrUntaggedImpl<TChar, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TResult> parser1;

        private readonly IParser<TChar, TResult> parser2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserOrUntaggedImpl(IParser<TChar, TResult> parser1, IParser<TChar, TResult> parser2) {
            this.parser1 = parser1;
            this.parser2 = parser2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            {
                var enumerator = parser1.Parse(input, position);
                for (; enumerator.MoveNext();) {
                    yield return enumerator.Current;
                }
                enumerator.Dispose();
            }
            {
                var enumerator = parser2.Parse(input, position);
                for (; enumerator.MoveNext();) {
                    yield return enumerator.Current;
                }
                enumerator.Dispose();
            }
        }
    }

    public readonly struct ParserOrElseUntaggedImpl<TChar, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TResult> parser1;

        private readonly IParser<TChar, TResult> parser2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserOrElseUntaggedImpl(IParser<TChar, TResult> parser1, IParser<TChar, TResult> parser2) {
            this.parser1 = parser1;
            this.parser2 = parser2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var shortcut = false;
            {
                var enumerator1 = parser1.Parse(input, position);
                for (; enumerator1.MoveNext();) {
                    shortcut = true;
                    yield return enumerator1.Current;
                }
                enumerator1.Dispose();
            }
            if (!shortcut) {                
                var enumerator2 = parser2.Parse(input, position);
                for (; enumerator2.MoveNext();) {
                    yield return enumerator2.Current;
                }
                enumerator2.Dispose();
            }
        }
    }
}