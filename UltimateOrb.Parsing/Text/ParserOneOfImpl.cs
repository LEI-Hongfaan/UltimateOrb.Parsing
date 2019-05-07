using System.Collections.Generic;
using UltimateOrb.Parsing.Text;

namespace UltimateOrb.Parsing {

    public readonly struct ParserOneOfImpl
        : IParser<char> {

        private readonly string chars;

        public ParserOneOfImpl(string chars) {
            this.chars = chars;
        }

        public IEnumerator<(char Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    this.chars.Contains(ch.ToString())
                ) {
                    yield return (ch, p);
                }
            }
            
        }
    }
}
