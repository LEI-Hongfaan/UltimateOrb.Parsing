using System.Collections.Generic;

namespace UltimateOrb.Parsing.Generic {

    public partial interface IMemorizableParser<TChar, TResult>
        : IParser<TChar, TResult> {

        IEnumerator<(TResult Result, int Position)> Parse<TString, TCache>(TString input, int position, TCache cache)
            where TString : IReadOnlyList<TChar>
            where TCache : IDictionary<(int Id, int Position), (TResult Result, int Position)>;
    }
}
