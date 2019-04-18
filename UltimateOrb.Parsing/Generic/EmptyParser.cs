using System;
using System.Collections.Generic;

namespace UltimateOrb.Parsing.Generic {

    public readonly struct EmptyParser<TChar, TResult>
        : IParser<TChar, TResult> {

        private readonly TResult result;

        public EmptyParser(TResult result) : this() {
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            yield return (result, position);
        }
    }
}
