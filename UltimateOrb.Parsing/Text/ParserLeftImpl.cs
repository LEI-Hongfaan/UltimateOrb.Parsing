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
            var parser1_enumerator = parser1.Parse(input, position);
            for (; parser1_enumerator.MoveNext();) {
                var right_start_position = parser1_enumerator.Current.Position;
                var parser2_enumerator = parser2.Parse(input, right_start_position);
                for (; parser2_enumerator.MoveNext();) {
                    yield return (parser1_enumerator.Current.Result, parser2_enumerator.Current.Position);
                }
                parser2_enumerator.Dispose();
            }
            parser1_enumerator.Dispose();
        }
    }
}
