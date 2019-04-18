using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UltimateOrb.Parsing.Text;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        internal static string ToString<TString>(TString input)
            where TString : IReadOnlyList<char> {
            if (typeof(TString) == typeof(StringAsCharList)) {
                return (StringAsCharList)(object)input;
            }
            {
                if (input is char[] array) {
                    return new string(array);
                }
            }
            {
                if (input is StringAsCharList wrapper) {
                    return wrapper;
                }
            }
            {
                var count = input.Count;
                var array = new char[count];
                for (var i = 0; count > i; ++i) {
                    array[i] = input[i];
                }
                return new string(array);
            }
        }
        
        public static RegexAsParser AsParser(this Regex regex) {
            return new RegexAsParser(regex);
        }
    }
}

namespace UltimateOrb.Parsing {


    public readonly struct RegexAsParser 
        : IParser<string> {

        private readonly Regex regex;

        public RegexAsParser(Regex regex) {
            this.regex = regex;
        }

        public IEnumerator<(string Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<char> {
            return (
                from a in (IEnumerable<Match>)regex.Matches(Combinators.ToString(input), position)
                select (a.Value, a.Index + a.Length)).GetEnumerator();
        }
    }
}