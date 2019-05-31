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
            var resultList = new List<(T1 Result, int Position)>();
            var result = false;
            var right_start_position = 0;
            for (; parser1_enumerator.MoveNext();) {
                right_start_position = parser1_enumerator.Current.Position;
                resultList.Add(parser1_enumerator.Current);
            }
            if (right_start_position > 0) {
                var parser2_enumerator = parser2.Parse(input, right_start_position);
                for (; parser2_enumerator.MoveNext();) {
                    result = true;
                    break;
                }
                parser2_enumerator.Dispose();
            }
            
            if (result) {
                foreach (var t in resultList) {
                    yield return (t.Result, t.Position);
                }
                /*parser1_enumerator.Reset();
                for (; parser1_enumerator.MoveNext();) {
                    yield return (parser1_enumerator.Current.Result, parser1_enumerator.Current.Position);
                }*/
            }
            parser1_enumerator.Dispose();
        }
    }
}
