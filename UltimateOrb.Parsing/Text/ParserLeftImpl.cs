using System.Collections.Generic;
using UltimateOrb.Parsing.Generic;
using UltimateOrb.Parsing.Text;

namespace UltimateOrb.Parsing {

    public readonly struct ParserLeftImpl
        : IParser<string> {

        private readonly string left;
        private readonly string right;

        public ParserLeftImpl(string left, string right) {
            this.left = left;
            this.right = right;
        }

        public IEnumerator<(string Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (p + left.Length + right.Length <= input.Count) {
                for (var i = 0; left.Length > i;) {
                    var ch1 = left[i++];
                    var ch2 = input[p++];
                    if (ch1 != ch2) {
                        yield break;
                    }
                }
                var pleft = p;
                for (var i = 0; right.Length > i;) {
                    var ch1 = right[i++];
                    var ch2 = input[p++];
                    if (ch1 != ch2) {
                        yield break;
                    }
                }
                yield return (this.left, pleft);
            }
        }
    }
}
