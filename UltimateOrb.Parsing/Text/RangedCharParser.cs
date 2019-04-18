using System;
using System.Collections.Generic;

namespace UltimateOrb.Parsing.Text {

    public readonly struct RangedCharParser<TResult>
        : IParser<TResult> {

        readonly char minExpected;
        readonly char maxExpected;
        readonly Converter<char, TResult> resultSelector;

        public RangedCharParser(char minExpected, char maxExpected, Converter<char, TResult> resultSelector) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<char> {
            var p = position;
            if (str.Count > p) {
                var ch = str[p++];
                if (minExpected <= ch && ch <= maxExpected) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }

    public readonly struct RangedCharConstParser<TResult>
        : IParser<TResult> {

        readonly char minExpected;
        readonly char maxExpected;
        readonly TResult result;

        public RangedCharConstParser(char minExpected, char maxExpected, TResult result) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<char> {
            var p = position;
            if (str.Count > p) {
                var ch = str[p++];
                if (minExpected <= ch && ch <= maxExpected) {
                    yield return (result, p);
                }
            }
        }
    }

    public readonly struct RangedCharIdentityParser
        : IParser<char> {

        readonly char minExpected;
        readonly char maxExpected;

        public RangedCharIdentityParser(char minExpected, char maxExpected) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
        }

        public IEnumerator<(char Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<char> {
            var p = position;
            if (str.Count > p) {
                var ch = str[p++];
                if (minExpected <= ch && ch <= maxExpected) {
                    yield return (ch, p);
                }
            }
        }
    }
}
