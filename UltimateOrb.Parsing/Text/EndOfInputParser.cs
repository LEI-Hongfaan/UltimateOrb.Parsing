using System;
using System.Collections.Generic;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing.Text {

    public readonly struct EndOfInputParser<TResult>
        : IParser<char, TResult> {

        private readonly TResult result;

        public EndOfInputParser(TResult result) : this() {
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            if (position == input.Count) {
                yield return (result, position);
            }
        }
    }
}
