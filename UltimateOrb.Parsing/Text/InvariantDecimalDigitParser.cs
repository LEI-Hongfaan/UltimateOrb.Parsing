using System.Collections.Generic;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing.Text {

    public readonly struct InvariantDecimalDigitParser
        : IReversibleParser<int> {

        public IEnumerator<(int Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if ('0' <= ch && ch <= '9') {
                    yield return (ch - '0', p);
                }
            }
        }

        public InvariantDecimalDigitParser Reversed() {
            return this;
        }

        IReversibleParser<char, int> IReversibleParser<char, int>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<int> IReversibleParser<int>.Reversed() {
            return this.Reversed();
        }
    }
}
