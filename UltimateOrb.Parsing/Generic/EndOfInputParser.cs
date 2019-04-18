using System.Collections.Generic;

namespace UltimateOrb.Parsing.Generic {

    public readonly struct EndOfInputParser<TChar, TResult>
        : IParser<TChar, TResult> {

        private readonly TResult result;

        public EndOfInputParser(TResult result) : this() {
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            if (position == input.Count) {
                yield return (result, position);
            }
        }
    }
}
