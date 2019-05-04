using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using UltimateOrb.Parsing.Text;

using static UltimateOrb.Parsing.Combinators;

namespace UltimateOrb.Parsing.Tests {

    using static UltimateOrb.Parsing.Combinators.Invariant;

    public static partial class Json {

        #region JSON Parsers

        private static readonly Generic.ParserTimesWithAggregatingImpl<char, char, VoidResult> WhitespaceSequenceNillableIgnored =
            new CharIdentityParser(ch => "\t\n\r ".Contains(ch)).Times(default(VoidResult), (x, y) => default);

        // OneOf("\t\n\r ").Times();
        private static readonly char[] escc = new[] { '"', '/', '\\', 'b', 'f', 'n', 'r', 't' };

        private static readonly char[] unen = new[] { '"', '/', '\\', '\b', '\f', '\n', '\r', '\t' };

        private static readonly Generic.IParser<char, string> string_p =
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

        private static readonly Generic.ParserSelectManyImpl1<char, Optional<char>, double, double> integer_p =
            from neg in '-'.ToParserInvariant().Opt()
            from abs in DecimalDigit.Or(
                from ahd in new RangedCharParser<double>('1', '9', ch => ch - '0')
                from atl in DecimalDigit.Times(0.0, (x, y) => 10.0 * x + y, 1, Infinity)
                select 10 * ahd + atl, x => x, x => x)
            select neg.HasValue ? -abs : abs;

        private static readonly Generic.IParser<char, double> number_p =
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

        private static readonly Generic.ParserOrImpl<char, string, string, bool> boolean_p =
            "true".ToParserInvariant().Or("false".ToParserInvariant(), x => true, x => false);

        private static readonly Generic.ParserSelectImpl<char, string, object> null_p =
            from _ in "null".ToParserInvariant()
            select (object)null;

        private static Generic.IParser<char, object> GerJsonParserCore(int maxDepth = Infinity) {
            if (Infinity == maxDepth) {
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

                value_p =
                    object_p.Or(
                        null_p
                    , x => (object)x, x => (object)x).Or(
                        array_p.Or(
                            string_p
                        , x => (object)x, x => (object)x)
                    , x => (object)x, x => (object)x).Or(
                        number_p.Or(
                            boolean_p
                        , x => (object)x, x => (object)x)
                    , x => (object)x, x => (object)x);
                // Or(string_p, number_p, object_p, array_p, boolean_p, );

                return value_p;
            }
            if (0 == maxDepth) {
                return string_p.Or(
                        null_p
                    , x => (object)x, x => (object)x).Or(
                        number_p.Or(
                            boolean_p
                        , x => (object)x, x => (object)x)
                    , x => (object)x, x => (object)x);
            } else {
                var element_p =
                    from _ in WhitespaceSequenceNillableIgnored
                    from value in GerJsonParserCore(maxDepth - 1)
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

                return
                    object_p.Or(
                        null_p
                    , x => (object)x, x => (object)x).Or(
                        array_p.Or(
                            string_p
                        , x => (object)x, x => (object)x)
                    , x => (object)x, x => (object)x).Or(
                        number_p.Or(
                            boolean_p
                        , x => (object)x, x => (object)x)
                    , x => (object)x, x => (object)x);
            }
        }

        private static Generic.IParser<char, object> GerJsonParser(int maxDepth = Infinity) {
            return
                from _ in WhitespaceSequenceNillableIgnored
                from value in GerJsonParserCore(maxDepth)
                from __ in WhitespaceSequenceNillableIgnored
                from ___ in EndOfInput
                select value;
        }

        #endregion JSON Parsers
    }
}