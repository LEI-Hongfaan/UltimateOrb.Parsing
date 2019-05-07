using System;
using System.Collections.Generic;
using System.Text;
using UltimateOrb.Parsing.Text;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        public static FuncAsParser<TResult> AsParser<TResult>(this Func<string, int, IEnumerator<(TResult Result, int Position)>> func) {
            return new FuncAsParser<TResult>(func);
        }

        public static FuncAsParser<TResult> ToParser<TResult>(this Func<string, int, IEnumerator<(TResult Result, int Position)>> func) {
            return func.AsParser();
        }

        public static EndOfInputParser<VoidResult> EndOfInput() {
            return new EndOfInputParser<VoidResult>();
        }

        public static EndOfInputParser<TResult> EndOfInput<TResult>(TResult result) {
            return new EndOfInputParser<TResult>(result);
        }

        public static SingleCharIdentityParser ToParserInvariant(this char expected) {
            return new SingleCharIdentityParser(expected);
        }

        public static SingleCharConstParser<TResult> ToParserInvariant<TResult>(this char expected, TResult result) {
            return new SingleCharConstParser<TResult>(expected, result);
        }

        public static SimpleStringIdentityParser ToParserInvariant(this string expected) {
            return new SimpleStringIdentityParser(expected);
        }

        public static SimpleStringConstParser<TResult> ToParserInvariant<TResult>(this string expected, TResult result) {
            return new SimpleStringConstParser<TResult>(expected, result);
        }

        public static ParserOneOfImpl OneOf(string chars) {
            return new ParserOneOfImpl(chars);
        }

    }

    
}
namespace UltimateOrb.Parsing {
    public readonly struct FuncAsParser<TResult>
        : IParser<TResult> {

        private readonly Func<string, int, IEnumerator<(TResult Result, int Position)>> func;

        public FuncAsParser(Func<string, int, IEnumerator<(TResult Result, int Position)>> func) {
            this.func = func;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<char> {
            return func(Combinators.ToString(input), position);
        }
    }
}
