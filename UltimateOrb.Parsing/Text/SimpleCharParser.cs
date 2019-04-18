using System.Collections.Generic;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing.Text {

    public readonly struct SingleCharIdentityParser
        : IReversibleParser<char> {

        readonly char expected;

        public SingleCharIdentityParser(char expected) {
            this.expected = expected;
        }

        public IEnumerator<(char Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (expected.Equals(ch)) {
                    yield return (expected, p);
                }
            }
        }

        public SingleCharIdentityParser Reversed() {
            return this;
        }

        IParser<char, char> IReversibleParser<char, char>.Reversed() {
            return this.Reversed();
        }
    }


    public readonly struct SingleCharConstParser<TResult>
        : IParser<TResult>
        , IReversibleParser<TResult>
        , IMemorizableParser<TResult> {

        readonly char expected;
        readonly TResult result;

        public SingleCharConstParser(char expected, TResult result) {
            this.expected = expected;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (expected == ch) {
                    yield return (result, p);
                }
            }
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString, TCache>(TString input, int position, TCache cache)
            where TString : IReadOnlyList<char>
            where TCache : IDictionary<(int Id, int Position), (TResult Result, int Position)> {
            return Parse(input, position);
        }

        public SingleCharConstParser<TResult> Reversed() {
            return this;
        }

        IParser<char, TResult> IReversibleParser<char, TResult>.Reversed() {
            return Reversed();
        }
    }
}
