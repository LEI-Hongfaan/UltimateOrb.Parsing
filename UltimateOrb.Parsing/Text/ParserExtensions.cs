using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace UltimateOrb.Parsing.Text {

    public static class ParserExtensions {

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        internal static StringAsCharList AsList(this string value) {
            return value;
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator<(TResult Result, int Position)> Parse<TResult>(this Generic.IParser<char, TResult> parser, string input, int position = 0) {
            return parser.Parse(input.AsList(), position);
        }
    }
}
