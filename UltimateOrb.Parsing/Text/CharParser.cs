using System;
using System.Collections.Generic;

namespace UltimateOrb.Parsing.Text {

    public readonly struct CharParser<TResult>
        : IParser<TResult> {

        readonly Predicate<char> predicate;

        readonly Func<char, TResult> converter;

        public IEnumerator<(TResult Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<char> {
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
