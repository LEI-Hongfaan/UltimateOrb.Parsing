using System;
using System.Collections.Generic;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing.Generic {

    public readonly struct CharParser<TChar, TResult>
        : IParser<TChar, TResult> {

        readonly Predicate<TChar> predicate;

        readonly Func<TChar, TResult> converter;

        public IEnumerator<(TResult Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<TChar> {
            var p = position;
            if (str.Count > p) {
                var ch = str[p++];
                if (predicate.Invoke(ch)) {
                    yield return (converter.Invoke(ch), p);
                }
            }
        }
    }
}
