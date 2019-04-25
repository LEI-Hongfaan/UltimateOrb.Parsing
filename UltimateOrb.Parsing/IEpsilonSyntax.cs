using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateOrb.Parsing {

    public interface IEpsilonSyntax
       : ISyntax {
    }

    public interface IEpsilonSyntax<TChar>
        : IEpsilonSyntax
        , ISyntax<TChar> {
    }

    public interface IEpsilonSyntax<TChar, TResult>
        : IEpsilonSyntax<TChar>
        , ISyntax<TChar, TResult> {
    }
}
