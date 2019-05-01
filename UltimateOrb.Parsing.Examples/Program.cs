using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UltimateOrb.Parsing.Text;
using static UltimateOrb.Parsing.Combinators;

namespace UltimateOrb.Parsing.Examples {
    using static UltimateOrb.Parsing.Combinators.Invariant;

    class Program {
        static void RunJsonExample() {
            var WhitespaceSequenceNillableIgnored =
                new CharIdentityParser(ch => "\t\n\r ".Contains(ch)).Times(default(VoidResult), (x, y) => default);
            // OneOf("\t\n\r ").Times();
            var escc = new[] { '"', '/', '\\', 'b', 'f', 'n', 'r', 't' };
            var unen = new[] { '"', '/', '\\', '\b', '\f', '\n', '\r', '\t' };

            var string_p =
                from _ in '"'.ToParserInvariant()
                from text in (
                     new CharIdentityParser(ch => ch != '"' && ch != '\\' && ch >= ' ').Or(
                         from __ in '\\'.ToParserInvariant()
                         from ue in
                             new CharParser<char>(
                                 ch => Array.BinarySearch(escc, ch) >= 0, ch => unen[Array.BinarySearch(escc, ch)]
                             ).Or(
                                 from ___ in 'u'.ToParserInvariant()
                                 from ch in new CharParser<int>(
                                     ch => '0' <= ch && ch <= '9' || 'A' <= ch && ch <= 'F' || 'a' <= ch && ch <= 'f',
                                     ch => 'A' <= ch ? ('a' <= ch ? ch - ('a' - 10) : ch - ('A' - 10)) : ch - '0'
                                 ).Times(0, (x, y) => (x << 4) | y, 4, 4)
                                 select unchecked((char)ch)
                             , x => x, x => x)
                         select ue
                     , x => x, x => x)
                ).Times(new StringBuilder(), (x, y) => x.Append(y), x => x.ToString(), 1, Infinity).Opt("")
                from __ in '"'.ToParserInvariant()
                select text;

            var integer_p =
                from neg in '-'.ToParserInvariant().Opt()
                from abs in DecimalDigit.Or(
                    from ahd in new RangedCharParser<double>('1', '9', ch => ch - '0')
                    from atl in DecimalDigit.Times(0.0, (x, y) => 10.0 * x + y, 1, Infinity)
                    select 10 * ahd + atl, x => x, x => x)
                select neg.HasValue ? -abs : abs;


            var number_p =
                from intl in integer_p
                from frac in (
                    from _ in '.'.ToParserInvariant()
                    let d = new StrongBox<double>(1.0)
                    from v in DecimalDigit.Times(0.0, (x, y) => x + (d.Value *= 0.1) * y, 1, Infinity)
                    select v
                ).Opt(0.0)
                from expn in (
                    from _ in 'E'.ToParserInvariant().Or('e'.ToParserInvariant())
                    from sign in Empty.Or('+'.ToParserInvariant()).Or('-'.ToParserInvariant())
                    from e in DecimalDigit.Times(0.0, (x, y) => 10.0 * x + y, 1, Infinity)
                    select Math.Pow(10, 2 == sign.Case ? -e : e)
                ).Opt(1.0)
                select (intl + frac) * expn;


            var value_p = default(Generic.IParser<char, object>);


            var element_p =
                from _ in WhitespaceSequenceNillableIgnored
                from value in value_p
                from __ in WhitespaceSequenceNillableIgnored
                select value;

            var member_p =
                from _ in WhitespaceSequenceNillableIgnored
                from key in string_p
                from __ in WhitespaceSequenceNillableIgnored
                from ___ in ':'.ToParserInvariant()
                from value in element_p
                select KeyValuePair.Create(key, value);

            var object_p =
                from _ in '{'.ToParserInvariant()
                from v in (
                    from hd in member_p
                    from tl in (
                        from _ in ','.ToParserInvariant()
                        from te in member_p
                        select te
                    ).Times(ImmutableDictionary.Create<string, object>(), (x, y) => x.Add(y.Key, y.Value))
                    select tl.Add(hd.Key, hd.Value)
                ).OrElseUntagged(
                    from __ in WhitespaceSequenceNillableIgnored
                    select ImmutableDictionary.Create<string, object>()
                )
                from __ in '}'.ToParserInvariant()
                select v;

            var array_p =
                from _ in '['.ToParserInvariant()
                from v in (
                    from hd in element_p
                    from tl in (
                        from _ in ','.ToParserInvariant()
                        from te in element_p
                        select te
                    ).Times()
                    select tl.Insert(0, hd)
                ).OrElseUntagged(
                    from __ in WhitespaceSequenceNillableIgnored
                    select ImmutableList.Create<object>()
                )
                from __ in ']'.ToParserInvariant()
                select v;

            var boolean_p =
                "true".ToParserInvariant().Or("false".ToParserInvariant(), x => true, x => false);

            value_p =
                object_p.Or(
                    "null".ToParserInvariant()
                , x => (object)x, x => (object)null).Or(
                    array_p.Or(
                        string_p
                    , x => (object)x, x => (object)x)
                , x => (object)x, x => (object)x).Or(
                    number_p.Or(
                        boolean_p
                    , x => (object)x, x => (object)x)
                , x => (object)x, x => (object)x);
            // Or(string_p, number_p, object_p, array_p, boolean_p, );
            var json_p =
                from v in element_p
                from _ in EndOfInput
                select v;


            {
                foreach (var item in json_p.Parse(
$@"true").Results()) {
                    Microsoft.FSharp.Core.PrintfModule.PrintFormatLine(
                        new Microsoft.FSharp.Core.PrintfFormat<Microsoft.FSharp.Core.FSharpFunc<object, Microsoft.FSharp.Core.Unit>, System.IO.TextWriter, Microsoft.FSharp.Core.Unit, Microsoft.FSharp.Core.Unit>("%A")).Invoke(item);
                }
            }

            {
                foreach (var item in json_p.Parse(
$@"true   ").Results()) {
                    Microsoft.FSharp.Core.PrintfModule.PrintFormatLine(
                        new Microsoft.FSharp.Core.PrintfFormat<Microsoft.FSharp.Core.FSharpFunc<object, Microsoft.FSharp.Core.Unit>, System.IO.TextWriter, Microsoft.FSharp.Core.Unit, Microsoft.FSharp.Core.Unit>("%A")).Invoke(item);
                }
            }

            {
                foreach (var item in json_p.Parse(
$@"{{}}").Results()) {
                    Microsoft.FSharp.Core.PrintfModule.PrintFormatLine(
                        new Microsoft.FSharp.Core.PrintfFormat<Microsoft.FSharp.Core.FSharpFunc<object, Microsoft.FSharp.Core.Unit>, System.IO.TextWriter, Microsoft.FSharp.Core.Unit, Microsoft.FSharp.Core.Unit>("%A")).Invoke(item);
                }
            }

            {
                Microsoft.FSharp.Core.PrintfModule.PrintFormatLine(
                    new Microsoft.FSharp.Core.PrintfFormat<Microsoft.FSharp.Core.FSharpFunc<object, Microsoft.FSharp.Core.Unit>, System.IO.TextWriter, Microsoft.FSharp.Core.Unit, Microsoft.FSharp.Core.Unit>("%A")).Invoke(
                    json_p.Parse(
$@"[ 1 ]").SingleResult());
            }

            {
                Microsoft.FSharp.Core.PrintfModule.PrintFormatLine(
                    new Microsoft.FSharp.Core.PrintfFormat<Microsoft.FSharp.Core.FSharpFunc<object, Microsoft.FSharp.Core.Unit>, System.IO.TextWriter, Microsoft.FSharp.Core.Unit, Microsoft.FSharp.Core.Unit>("%A")).Invoke(
                    json_p.Parse(
$@"{{ ""\/"": ""\\"" }}").SingleResult());
            }

            {
                var v = json_p.Parse(
$@"{{ ""Abc\u10Def""
: [3.14
] }}").Results();
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(v));
            }

            {
                var v = json_p.Parse(
$@"{{
  ""enn"": null,
  ""pi"": 3.14,
  ""list"": [ ""\/\t\\"", ""/\u10Abc"", true, false, [] ]
}}").SingleResult();
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(v));
            }

            {
                var jsonText =
$@"{{
  ""ss"": {{
    ""ss"": """",
    ""enn"" :{{}},
    ""pi"":0E5,
    ""list"": [
]
,
    ""srs"": ""s""
  }},
  ""enn"": null,
  ""pi"": 3.14,
  ""list"": [ ""\/\t\\"", ""/\u20aC♞"", true, false, [] ]
}}";
                var sw = new Stopwatch();
                sw.Start();
                var v = json_p.Parse(jsonText).SingleResult();
                sw.Stop();
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(v));
                var a = Newtonsoft.Json.Linq.JToken.FromObject(v);
                var b = Newtonsoft.Json.Linq.JToken.Parse(jsonText);
                var c = Newtonsoft.Json.Linq.JToken.DeepEquals(a, b);
                Console.WriteLine(c);
                Console.WriteLine(sw.Elapsed);
            }
        }

        static void Main(string[] args) {
            Console.OutputEncoding = Encoding.Unicode;
            {
                RunJsonExample();
            }
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
