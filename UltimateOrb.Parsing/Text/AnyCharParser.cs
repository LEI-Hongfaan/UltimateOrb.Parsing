using System;
using System.Collections.Generic;

namespace UltimateOrb.Parsing.Text {

    public readonly struct AnyCharParser<TResult>
        : IParser<TResult> {

        readonly Converter<char, TResult> resultSelector;

        public AnyCharParser(Converter<char, TResult> resultSelector) {
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<char> {
            var p = position;
            if (str.Count > p) {
                var ch = str[p++];
                yield return (resultSelector.Invoke(ch), p);
            }
        }
    }

    public readonly struct AnyCharConstParser<TResult>
        : IParser<TResult> {

        readonly TResult result;

        public AnyCharConstParser(TResult result) {
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<char> {
            var p = position;
            if (str.Count > p) {
                var ch = str[p++];
                yield return (result, p);
            }
        }
    }

    public readonly struct AnyCharIdentityParser
        : IParser<char> {

        public IEnumerator<(char Result, int Position)> Parse<TList>(TList str, int position = 0) where TList : IReadOnlyList<char> {
            var p = position;
            if (str.Count > p) {
                var ch = str[p++];
                yield return (ch, p);
            }
        }
    }
}
