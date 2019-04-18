using System;
using System.Collections.Generic;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing.Text {

    public readonly struct EmptyParser<TResult>
        : IParser<TResult>
        , IReversibleParser<TResult> 
        , IMemorizableParser<TResult> {

        private readonly TResult result;

        public EmptyParser(TResult result) : this() {
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            yield return (result, position);
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString, TCache>(TString input, int position, TCache cache)
            where TString : IReadOnlyList<char>
            where TCache : IDictionary<(int Id, int Position), (TResult Result, int Position)> {
            yield return (result, position);
        }

        IParser<char, TResult> IReversibleParser<char, TResult>.Reversed() {
            throw new NotImplementedException();
        }
    }
}
