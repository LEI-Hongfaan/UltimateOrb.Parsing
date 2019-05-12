using System.Collections.Generic;
using UltimateOrb.Parsing.Generic;
using UltimateOrb.Parsing.Text;

namespace UltimateOrb.Parsing {

    public readonly struct ParserLeftImpl<T1, T2>
        : IParser<T1> {

        private readonly IParser<char, T1> parser1;
        private readonly IParser<char, T2> parser2;

        public ParserLeftImpl(IParser<char, T1> parser1, IParser<char, T2> parser2) {
            this.parser1 = parser1;
            this.parser2 = parser2;
        }

        public IEnumerator<(T1 Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            return parser1.Parse(input, position);
        }
    }
}
