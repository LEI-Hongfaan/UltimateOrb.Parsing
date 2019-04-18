using System;
using System.Collections.Generic;

namespace UltimateOrb.Parsing.Generic {

    public readonly struct SimpleStringIdentityParser<TChar, TExceptedString>
        : IParser<TChar, TExceptedString>
        where TChar : struct, IEquatable<TChar>
        where TExceptedString : IReadOnlyList<TChar> {

        private readonly TExceptedString excepted;

        public SimpleStringIdentityParser(TExceptedString excepted) {
            this.excepted = excepted;
        }

        public IEnumerator<(TExceptedString Result, int Position)> Parse<TString>(TString input, int position = 0)
            where TString : IReadOnlyList<TChar> {
            var p = position;
            var l = excepted.Count;
            if (input.Count <= p + l) {
                for (var i = 0; l > i;) {
                    var ch1 = excepted[i++];
                    var ch2 = input[p++];
                    if (!ch1.Equals(ch2)) {
                        yield break;
                    }
                }
                yield return (excepted, p);
            }
        }
    }

    public readonly struct SimpleStringConstParser<TChar, TResult>
        : IParser<TChar, TResult>
        where TChar : struct, IEquatable<TChar> {
        private readonly IReadOnlyList<TChar> excepted;
        private readonly TResult result;

        public SimpleStringConstParser(IReadOnlyList<TChar> excepted, TResult result) {
            this.excepted = excepted;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            var l = excepted.Count;
            if (input.Count <= p + l) {
                for (var i = 0; l > i;) {
                    var ch1 = excepted[i++];
                    var ch2 = input[p++];
                    if (!ch1.Equals(ch2)) {
                        yield break;
                    }
                }
                yield return (result, p);
            }
        }
    }
}
