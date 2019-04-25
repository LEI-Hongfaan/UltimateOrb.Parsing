using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateOrb.Parsing {

    public interface IStringSyntax
         : ISyntax {
    }

    public interface IStringSyntax<TChar>
        : IStringSyntax
        , ISyntax<TChar> {
    }

    public interface IStringSyntax<TChar, TResult>
        : IStringSyntax<TChar>
        , ISyntax<TChar, TResult> {
    }
}
