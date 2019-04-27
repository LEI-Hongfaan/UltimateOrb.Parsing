using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateOrb.Parsing {

    public interface ICharSyntax
        : IStringSyntax {
    }

    public interface ICharSyntax<TChar>
        : ICharSyntax
        , IStringSyntax<TChar> {
    }

    public interface ICharSyntax<TChar, TResult>
        : ICharSyntax<TChar>
        , IStringSyntax<TChar, TResult> {
    }
}
