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
        public static ParserOneOfOrImpl<TChar, TResult> OneOfOr<TChar, TResult>(this IParser<TChar, TResult> parser1, IParser<TChar, TResult> parser2) {
            return new ParserOneOfOrImpl<TChar, TResult>(parser1, parser2);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserOrImpl<TChar, TResult1, TResult2>
        : IParser<TChar, Union<TResult1, TResult2>> {

        private readonly IParser<TChar, TResult1> parser1;

        private readonly IParser<TChar, TResult2> parser2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserOrImpl(IParser<TChar, TResult1> parser1, IParser<TChar, TResult2> parser2) {
            this.parser1 = parser1;
            this.parser2 = parser2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(Union<TResult1, TResult2> Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
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

    public readonly struct ParserUnifiedOrImpl<TChar, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TResult> parser1;

        private readonly IParser<TChar, TResult> parser2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserUnifiedOrImpl(IParser<TChar, TResult> parser1, IParser<TChar, TResult> parser2) {
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

    public readonly struct ParserOneOfOrImpl<TChar, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TResult> parser1;

        private readonly IParser<TChar, TResult> parser2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserOneOfOrImpl(IParser<TChar, TResult> parser1, IParser<TChar, TResult> parser2) {
            this.parser1 = parser1;
            this.parser2 = parser2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            {
                var enumerator1 = parser1.Parse(input, position);
                var getOr1 = false;
                for (; enumerator1.MoveNext();) {
                    getOr1 = true;
                    yield return enumerator1.Current;
                }
                enumerator1.Dispose();

                if (!getOr1) {                
                    var enumerator2 = parser2.Parse(input, position);
                    for (; enumerator2.MoveNext();) {
                        yield return enumerator2.Current;
                    }
                    enumerator2.Dispose();
                }
            }
        }
    }
}