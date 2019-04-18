using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UltimateOrb.Parsing.Text;
using static UltimateOrb.Parsing.Combinators;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserTimesImpl Times(this IParser<char> parser, int occurrence) {
            return parser.Times(occurrence, occurrence);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserTimesImpl Times(this IParser<char> parser, int minOccurrence = 0, int maxOccurrence = Infinity) {
            return new ParserTimesImpl(parser, minOccurrence, maxOccurrence);
        }


    }


}

namespace UltimateOrb.Parsing.Text {

    public readonly struct ParserTimesImpl
        : IParser<string> {

        private readonly IParser<char> parser;

        private readonly int minOccurrence;

        private readonly int maxOccurrence;

        public ParserTimesImpl(IParser<char> parser, int minOccurrence, int maxOccurrence) {
            this.parser = parser;
            this.minOccurrence = minOccurrence;
            this.maxOccurrence = maxOccurrence;
        }

        public IEnumerator<(string Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<char> {
            if (0 == minOccurrence) {
                yield return ("", position);
            }
            if (0 == maxOccurrence) {
                yield break;
            }
            var acc = new StringBuilder();
            var s = new Stack<IEnumerator<(char Result, int Position)>>();
            var enumerator = parser.Parse(input, position);
            for (var count = 0; ;) {
                if (enumerator.MoveNext()) {
                    ++count;
                    var current = enumerator.Current;
                    acc.Append(current.Result);
                    System.Diagnostics.Debug.Assert(count == acc.Length);
                    if (minOccurrence <= count) {
                        yield return (acc.ToString(), current.Position);
                    }
                    if (Infinity == maxOccurrence || maxOccurrence > count) {
                        s.Push(enumerator);
                        enumerator = parser.Parse(input, current.Position);
                        continue;
                    }
                    for (; enumerator.MoveNext();) {
                        current = enumerator.Current;
                        acc[acc.Length - 1] = current.Result;
                        yield return (acc.ToString(), current.Position);
                    }
                }

                --count;
                enumerator.Dispose();
                if (0 == count) {
                    yield break;
                }
                enumerator = s.Pop();
                --acc.Length;
            }
        }
    }
}
