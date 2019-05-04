using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserAnyImpl<TChar, TResult> Any<TChar, TResult>(this IParser<TChar, TResult> parser) {
            return new ParserAnyImpl<TChar, TResult>(parser);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserAnyImpl<TChar, TResult>
        : IParser<TChar, bool> {

        private readonly IParser<TChar, TResult> parser;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserAnyImpl(IParser<TChar, TResult> parser) {
            this.parser = parser;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(bool Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            {
                var result = false;
                var enumerator = parser.Parse(input, position);
                for (; enumerator.MoveNext();) {
                    result = true;
                    break;
                }
                yield return (result, position);
                enumerator.Dispose();
            }
        }
    }
}