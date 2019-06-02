using System;
using System.Collections.Generic;
using UltimateOrb.Parsing.Text;

namespace UltimateOrb.Parsing {

    public readonly struct ParserAnyCharOfImpl
        : IParser<char> {

        private readonly char[] chars;

        public ParserAnyCharOfImpl(string chars) {
            var t = chars.ToCharArray();
            Array.Sort(t);
            this.chars = t;
        }

        public ParserAnyCharOfImpl(params char[] chars) {
            var t = chars;
            Array.Sort(t);
            this.chars = t;
        }

        public IEnumerator<(char Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    0 <= Array.BinarySearch(chars, ch)
                ) {
                    yield return (ch, p);
                }
            }
        }
    }
}
