using System.Collections.Generic;

namespace UltimateOrb.Parsing {

    public interface ITermialSyntaxExpression
        : ISyntax
        , ISyntaxExpression {
    }

    public interface ITermialSyntaxExpression<TChar>
        : ITermialSyntaxExpression
        , ISyntax<TChar>
        , ISyntaxExpression<TChar> {
    }

    public interface ITermialSyntaxExpression<TChar, TResult>
       : ITermialSyntaxExpression<TChar>
       , ISyntax<TChar, TResult>
       , ISyntaxExpression<TChar, TResult> {
    }
}
