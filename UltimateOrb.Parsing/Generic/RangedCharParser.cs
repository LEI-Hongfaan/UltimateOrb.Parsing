using System;
using System.Collections.Generic;

namespace UltimateOrb.Parsing.Generic {

    public readonly struct RangedCharParser<TChar, TResult>
        : IParser<TChar, TResult>
        where TChar : struct, IComparable<TChar> {

        readonly TChar minExpected;
        readonly TChar maxExpected;
        readonly Converter<TChar, TResult> resultSelector;

        public RangedCharParser(TChar minExpected, TChar maxExpected, Converter<TChar, TResult> resultSelector) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<TChar> {
            var p = position;
            if (str.Count > p) {
                var ch = str[p++];
                if (minExpected.CompareTo(ch) <= 0 && ch.CompareTo(maxExpected) <= 0) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }

    public readonly struct RangedCharConstParser<TChar, TResult>
        : IParser<TChar, TResult>
        where TChar : struct, IComparable<TChar> {

        readonly TChar minExpected;
        readonly TChar maxExpected;
        readonly TResult result;

        public RangedCharConstParser(TChar minExpected, TChar maxExpected, TResult result) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<TChar> {
            var p = position;
            if (str.Count > p) {
                var ch = str[p++];
                if (minExpected.CompareTo(ch) <= 0 && ch.CompareTo(maxExpected) <= 0) {
                    yield return (result, p);
                }
            }
        }
    }

    public readonly struct RangedCharIdentityParser<TChar>
        : IParser<TChar, TChar>
        where TChar : struct, IComparable<TChar> {

        readonly TChar minExpected;
        readonly TChar maxExpected;

        public RangedCharIdentityParser(TChar minExpected, TChar maxExpected) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
        }

        public IEnumerator<(TChar Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<TChar> {
            var p = position;
            if (str.Count > p) {
                var ch = str[p++];
                if (minExpected.CompareTo(ch) <= 0 && ch.CompareTo(maxExpected) <= 0) {
                    yield return (ch, p);
                }
            }
        }
    }
}
