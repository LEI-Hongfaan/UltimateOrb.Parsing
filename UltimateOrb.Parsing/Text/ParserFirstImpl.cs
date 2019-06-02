using System.Collections.Generic;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {
    using UltimateOrb.Parsing.Text;

    public readonly struct ParserFirstImpl<TResultFirst, TResultSecond>
        : IParser<TResultFirst> {

        private readonly IParser<char, TResultFirst> first;

        private readonly IParser<char, TResultSecond> second;

        public ParserFirstImpl(IParser<char, TResultFirst> first, IParser<char, TResultSecond> second) {
            this.first = first;
            this.second = second;
        }

        public IEnumerator<(TResultFirst Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var first_enumerator = first.Parse(input, position);
            for (; first_enumerator.MoveNext();) {
                var first_current = first_enumerator.Current;
                var second_enumerator = second.Parse(input, first_current.Position);
                for (; second_enumerator.MoveNext();) {
                    yield return (first_current.Result, second_enumerator.Current.Position);
                }
                second_enumerator.Dispose();
            }
            first_enumerator.Dispose();
        }
    }
}
