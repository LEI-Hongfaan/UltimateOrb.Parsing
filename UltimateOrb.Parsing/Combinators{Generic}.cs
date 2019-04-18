using System;
using System.Collections.Generic;
using System.Text;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        public static EndOfInputParser<TChar, Void> EndOfInput<TChar>() {
            return new EndOfInputParser<TChar, Void>(default);
        }

        public static EndOfInputParser<TChar, TResult> EndOfInput<TChar, TResult>(TResult result) {
            return new EndOfInputParser<TChar, TResult>(result);
        }

        public static SingleCharIdentityParser<TChar> ToParser<TChar>(this TChar expected)
            where TChar : struct, IEquatable<TChar> {
            return new SingleCharIdentityParser<TChar>(expected);
        }

        public static SingleCharConstParser<TChar, TResult> ToParser<TChar, TResult>(this TChar expected, TResult result)
            where TChar : struct, IEquatable<TChar> {
            return new SingleCharConstParser<TChar, TResult>(expected, result);
        }

        public static SimpleStringIdentityParser<TChar, IReadOnlyList<TChar>> ToParser<TChar>(this IReadOnlyList<TChar> expected)
            where TChar : struct, IEquatable<TChar> {
            return new SimpleStringIdentityParser<TChar, IReadOnlyList<TChar>>(expected);
        }

        public static SimpleStringConstParser<TChar, TResult> ToParser<TChar, TResult>(this IReadOnlyList<TChar> expected, TResult result)
            where TChar : struct, IEquatable<TChar> {
            return new SimpleStringConstParser<TChar, TResult>(expected, result);
        }

        public static SimpleStringIdentityParser<TChar, TString> ToParser<TChar, TString>(this TString expected)
            where TChar : struct, IEquatable<TChar>
            where TString : IReadOnlyList<TChar> {
            return new SimpleStringIdentityParser<TChar, TString>(expected);
        }

        public static SimpleStringConstParser<TChar, TResult> ToParser<TChar, TResult, TString>(this TString expected, TResult result)
            where TChar : struct, IEquatable<TChar>
            where TString : IReadOnlyList<TChar> {
            return new SimpleStringConstParser<TChar, TResult>(expected, result);
        }
    }
}
