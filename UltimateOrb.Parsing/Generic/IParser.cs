using System.Collections.Generic;

namespace UltimateOrb.Parsing.Generic {

    public partial interface IParser<in TChar, TResult> {

        IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position)
            where TString : IReadOnlyList<TChar>;
    }
}