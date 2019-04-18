using System;
using System.Text;
using UltimateOrb.Parsing.Text;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        internal static TResult Comma<T, TResult>(this T ignored, TResult value) {
            return value;
        }

        public static partial class Invariant {

            public static readonly AnyCharParser<char> AnyChar = new AnyCharParser<char>(x => x);

            public readonly static Generic.ParserTimesWithAggregatingImpl<char, char, (StringBuilder, int), string> AnyCharSequence = new AnyCharParser<char>(x => x).Times((new StringBuilder(), 0), (x, y) => (x.Item1.Length = x.Item2++).Comma(x.Item1.Append(y)).Comma(x), z => z.Item1.ToString(0, z.Item2), 1, Infinity);

            public readonly static Generic.ParserTimesWithAggregatingImpl<char, char, (StringBuilder, int), string> AnyCharSequenceNillable = new AnyCharParser<char>(x => x).Times((new StringBuilder(), 0), (x, y) => (x.Item1.Length = x.Item2++).Comma(x.Item1.Append(y)).Comma(x), z => z.Item1.ToString(0, z.Item2), 0, Infinity);

            public readonly static EndOfInputParser<VoidResult> EndOfInput = new EndOfInputParser<VoidResult>();

            public readonly static EmptyParser<VoidResult> Empty = new EmptyParser<VoidResult>();

            public readonly static EmptyParser<VoidResult> EndAtAny = new EmptyParser<VoidResult>();

            // public readonly static EmptyParser<Void> StartAtAny = new AnyCharParser<Void>(x => default).Times(default, (x, y) => default);
            public readonly static Generic.ParserTimesWithAggregatingImpl<char, VoidResult, VoidResult> StartAtAny = new AnyCharParser<VoidResult>(x => default).Times(default(VoidResult), (x, y) => default);

            public readonly static InvariantDecimalDigitParser DecimalDigit = new InvariantDecimalDigitParser();

            public readonly static Generic.ParserTimesImpl<char, int> DecimalDigitSequence = DecimalDigit.Times(1, Infinity);

            public readonly static Generic.ParserTimesImpl<char, int> DecimalDigitSequenceNillable = DecimalDigit.Times(0, Infinity);

            public static RangedCharIdentityParser Range(char minExpected, char maxExpected) {
                return new RangedCharIdentityParser(minExpected, maxExpected);
            }

            public static RangedCharConstParser<TResult> Range<TResult>(char minExpected, char maxExpected, TResult result) {
                return new RangedCharConstParser<TResult>(minExpected, maxExpected, result);
            }

            public static RangedCharParser<TResult> Range<TResult>(char minExpected, char maxExpected, Converter<char, TResult> resultSelector) {
                return new RangedCharParser<TResult>(minExpected, maxExpected, resultSelector);
            }
        }
    }
}
