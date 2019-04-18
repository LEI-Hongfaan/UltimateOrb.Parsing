using System;
using System.Collections.Generic;

namespace UltimateOrb.Parsing.Generic {

    public readonly struct SingleCharIdentityParser<TChar>
        : IReversibleParser<TChar, TChar>
        where TChar : struct, IEquatable<TChar> {

        readonly TChar expected;

        public SingleCharIdentityParser(TChar expected) {
            this.expected = expected;
        }

        public IEnumerator<(TChar Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (expected.Equals(ch)) {
                    yield return (expected, p);
                }
            }
        }

        public SingleCharIdentityParser<TChar> Reversed() {
            return this;
        }

        IParser<TChar, TChar> IReversibleParser<TChar, TChar>.Reversed() {
            return this.Reversed();
        }
    }

    public readonly struct SingleCharConstParser<TChar, TResult>
        : IReversibleParser<TChar, TResult>
        where TChar : struct, IEquatable<TChar> {

        readonly TChar expected;
        readonly TResult result;

        public SingleCharConstParser(TChar expected, TResult result) {
            this.expected = expected;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (expected.Equals(ch)) {
                    yield return (result, p);
                }
            }
        }

        public SingleCharConstParser<TChar, TResult> Reversed() {
            return this;
        }

        IParser<TChar, TResult> IReversibleParser<TChar, TResult>.Reversed() {
            return this.Reversed();
        }
    }
}
