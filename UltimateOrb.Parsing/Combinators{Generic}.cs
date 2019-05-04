using System;
using System.Collections.Generic;
using System.Text;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        public static EndOfInputParser<TChar, VoidResult> EndOfInput<TChar>() {
            return new EndOfInputParser<TChar, VoidResult>(default);
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

        public static ParserWithInputInResultImpl<TChar, TResult> WithInputInResult<TChar, TResult>(this IParser<TChar, TResult> parser) {
            return new ParserWithInputInResultImpl<TChar, TResult>(parser);
        }

        public static IParser<TChar, TResult> WithoutInputInResult<TChar, TResult>(this IParser<TChar, (TResult Result, IReadOnlyList<TChar> Input)> parser) {
            return
                from t in parser
                select t.Result;
        }

        public static Segments.IParser<TChar> ToSegmentResultParser<TChar, TResult>(this IParser<TChar, TResult> parser) {
            return new Segments.ParserToSegmentResultImpl<TChar, TResult>(parser);
        }

        public static IParser<TChar, TResult> ToGenericParser<TChar, TResult>(this IParser<TChar, TResult> parser) {
            return parser;
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserWithInputInResultImpl<TChar, TResult>
        : IParser<TChar, (TResult Result, IReadOnlyList<TChar> Input)> {

        private readonly IParser<TChar, TResult> parser;

        public ParserWithInputInResultImpl(IParser<TChar, TResult> parser) {
            this.parser = parser;
        }

        public IEnumerator<((TResult Result, IReadOnlyList<TChar> Input) Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var enumerator = parser.Parse(input, position);
            IReadOnlyList<TChar> _input = input;
            for (; enumerator.MoveNext(); ) {
                var current = enumerator.Current;
                yield return ((current.Result, _input), current.Position);
            }
            enumerator.Dispose();
        }

        public IParser<TChar, T> Cast<T>() {
            {
                if (this is IParser<TChar, T> result) {
                    return result;
                }
            }
            {
                if (this.parser is IParser<TChar, T> result) {
                    return result;
                }
            }
            throw new InvalidCastException();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct ParserToSegmentResultImpl<TChar, TResult>
        : IParser<TChar, Wrapper<TResult>>
        , IParser<TChar> {

        private readonly IParser<TChar, TResult> parser;

        public ParserToSegmentResultImpl(IParser<TChar, TResult> parser) {
            this.parser = parser;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var enumerator = parser.Parse(input, position);
            IReadOnlyList<TChar> _input = input;
            for (; enumerator.MoveNext();) {
                var current = enumerator.Current;
                yield return ((position, current.Position - position), current.Position);
            }
            enumerator.Dispose();
        }

        IEnumerator<(Wrapper<TResult> Result, int Position)> IParser<TChar, Wrapper<TResult>>.Parse<TString>(TString input, int position) {
            var enumerator = parser.Parse(input, position);
            IReadOnlyList<TChar> _input = input;
            for (; enumerator.MoveNext();) {
                var current = enumerator.Current;
                yield return (current.Result, current.Position);
            }
            enumerator.Dispose();
        }
    }
}
