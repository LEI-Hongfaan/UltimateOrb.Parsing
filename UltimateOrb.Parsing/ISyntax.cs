using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateOrb.Parsing {

    public static partial class SyntaxExtensions {

        public static ISyntaxExpression<TChar> GetDefination<TChar>(this ISyntax<TChar> syntax) {
            return syntax.GetDefination() as ISyntaxExpression<TChar>;
        }

        public static ISyntaxExpression<TChar> GetDefination<TChar, TResult>(this ISyntax<TChar, TResult> syntax) {
            return syntax.GetDefination() as ISyntaxExpression<TChar, TResult>;
        }
    }

    public interface ISyntax {

        ISyntaxExpression GetDefination();

        ISyntaxParser GetParser();
    }

    public interface ISyntax<in TChar>
        : ISyntax {
    }

    public interface ISyntax<in TChar, out TResult>
        : ISyntax<TChar> {
    }


    public interface ISyntaxParser {

        IParseResultCollection Parser<TChar, TString, TCache>(TString intput, int position, TCache cache)
            where TString : IReadOnlyList<TChar>
            where TCache : IDictionary<(int Id, int Position), IParseResultCollection>;
    }

    public interface IParseResultCollection
        : IEnumerable<IParseResult> {
    }

    public interface IParseResultCollection<out TResult>
        : IParseResultCollection
        , IEnumerable<IParseResult<TResult>> {

    }
}
