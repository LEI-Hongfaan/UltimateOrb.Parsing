using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UltimateOrb.Parsing.Text;
using static UltimateOrb.Parsing.Combinators;

namespace UltimateOrb.Parsing.Examples {
    using static UltimateOrb.Parsing.Combinators.Invariant;

    class Program {

        static void Main(string[] args) {
            {
                var parser =
                    from _1 in ' '.ToParser()
                    from _ in AnyChar.Times(0, 2)
                    from x in new Regex(@"\d{2,4}\b").AsParser()
                    from y in AnyChar.Times(2, 4)
                    select x + "_" + y;
                foreach (var v in parser.Parse(" 987 xy65 4321 z").Results()) {
                    Console.WriteLine(v);
                }
            }
            {
                var parser =
                    from x in DecimalDigit.Or('x'.ToParserInvariant()).Times()
                    let c = x.Count
                    from y in DecimalDigit.Or('y'.ToParserInvariant()).Times(c)
                    from z in DecimalDigit.Or('z'.ToParserInvariant()).Times(c)
                    from _ in EndOfInput
                    select
                    x.Where(s => 1 == s.Case).Select(s => s.Value1).Sum() +
                    y.Where(s => 1 == s.Case).Select(s => s.Value1).Sum() +
                    z.Where(s => 1 == s.Case).Select(s => s.Value1).Sum();
                foreach (var v in parser.Parse("9xxyyy87z").Results()) {
                    Console.WriteLine(v);
                }

            }
            {

                var parser =
                    from _ in StartAtAny
                    from x in AnyCharSequenceNillable
                    from y in DecimalDigit
                    select x + ":" + y;
                foreach (var v in parser.Parse("1abc2de456").Results()) {
                    Console.WriteLine(v);
                }

            }
            {
                var parser =
                    from x in DecimalDigitSequenceNillable
                    from y in (
                        from _ in '.'.ToParserInvariant()
                        from y in DecimalDigitSequenceNillable
                        select y
                    ).Opt()
                    from _ in EndOfInput
                    where x.Count > 0 || y.HasValue && y.Value.Count > 0
                    let i = x.Aggregate(0, (p, q) => 10 * p + q)
                    let k = new StrongBox<decimal>(1m)
                    select y.HasValue ? y.Value.Aggregate((decimal)i, (p, q) => p + q * (k.Value *= 0.1m)) : i;


                var samples = new[] {
                    "3.14",
                    ".123",
                    "6.",
                    "789",
                    ".0",
                    ".", // failed
                };
                foreach (var sample in samples) {
                    try {
                        var v = parser.Parse(sample).SingleResult();
                        Console.WriteLine(v);
                    } catch (InvalidOperationException ex) {
                        Console.WriteLine("Oops: {0}.", ex.Message);
                    }
                }
            }
        }
    }
}
