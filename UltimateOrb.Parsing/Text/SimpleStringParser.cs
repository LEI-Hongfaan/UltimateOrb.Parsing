using System.Collections.Generic;

namespace UltimateOrb.Parsing.Text {

    public readonly struct SimpleStringIdentityParser
        : IParser<string> {
        private readonly string excepted;

        public SimpleStringIdentityParser(string excepted) {
            this.excepted = excepted;
        }

        public IEnumerator<(string Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (p + excepted.Length <= input.Count) {
                for (var i = 0; excepted.Length > i;) {
                    var ch1 = excepted[i++];
                    var ch2 = input[p++];
                    if (ch1 != ch2) {
                        yield break;
                    }
                }
                yield return (excepted, p);
            }
        }
    }

    public readonly struct SimpleStringConstParser<T>
        : IParser<T> {
        private readonly string excepted;
        private readonly T result;

        public SimpleStringConstParser(string excepted, T result) {
            this.excepted = excepted;
            this.result = result;
        }

        public IEnumerator<(T Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (p + excepted.Length <= input.Count) {
                for (var i = 0; excepted.Length > i;) {
                    var ch1 = excepted[i++];
                    var ch2 = input[p++];
                    if (ch1 != ch2) {
                        yield break;
                    }
                }
                yield return (result, p);
            }
        }
    }
}
