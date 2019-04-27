
using System;
using System.Collections.Generic;

namespace UltimateOrb.Parsing.Generic {

    public readonly struct SingleCharIdentityParser<TChar>
        : IParser<TChar, TChar>
        where TChar : IEquatable<TChar>
    {

        readonly TChar expected;

        public SingleCharIdentityParser(
            TChar expected
        ) {
            this.expected = expected;
        }

        public IEnumerator<(TChar Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.Equals(expected)
                ) {
                    yield return (ch, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct SingleCharConstParser<TChar, TResult>
        : IParser<TChar, TResult>
        where TChar : IEquatable<TChar>
    {

        readonly TChar expected;

        readonly TResult result;

        public SingleCharConstParser(
            TChar expected
            , TResult result
        ) {
            this.expected = expected;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.Equals(expected)
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct SingleCharParser<TChar, TResult>
        : IParser<TChar, TResult>
        where TChar : IEquatable<TChar>
    {

        readonly TChar expected;

        readonly Converter<TChar, TResult> resultSelector;

        public SingleCharParser(
            TChar expected
            , Converter<TChar, TResult> resultSelector
        ) {
            this.expected = expected;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.Equals(expected)
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct RangedCharIdentityParser<TChar>
        : IParser<TChar, TChar>
        where TChar : IComparable<TChar>
    {

        readonly TChar minExpected;

        readonly TChar maxExpected;

        public RangedCharIdentityParser(
            TChar minExpected
            , TChar maxExpected
        ) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
        }

        public IEnumerator<(TChar Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.CompareTo(maxExpected) <= 0 && ch.CompareTo(minExpected) >= 0
                ) {
                    yield return (ch, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct RangedCharConstParser<TChar, TResult>
        : IParser<TChar, TResult>
        where TChar : IComparable<TChar>
    {

        readonly TChar minExpected;

        readonly TChar maxExpected;

        readonly TResult result;

        public RangedCharConstParser(
            TChar minExpected
            , TChar maxExpected
            , TResult result
        ) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.CompareTo(maxExpected) <= 0 && ch.CompareTo(minExpected) >= 0
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct RangedCharParser<TChar, TResult>
        : IParser<TChar, TResult>
        where TChar : IComparable<TChar>
    {

        readonly TChar minExpected;

        readonly TChar maxExpected;

        readonly Converter<TChar, TResult> resultSelector;

        public RangedCharParser(
            TChar minExpected
            , TChar maxExpected
            , Converter<TChar, TResult> resultSelector
        ) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.CompareTo(maxExpected) <= 0 && ch.CompareTo(minExpected) >= 0
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct CharIdentityParser<TChar>
        : IParser<TChar, TChar>
    {

        readonly Predicate<TChar> predicate;

        public CharIdentityParser(
            Predicate<TChar> predicate
        ) {
            this.predicate = predicate;
        }

        public IEnumerator<(TChar Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    predicate.Invoke(ch)
                ) {
                    yield return (ch, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct CharConstParser<TChar, TResult>
        : IParser<TChar, TResult>
    {

        readonly Predicate<TChar> predicate;

        readonly TResult result;

        public CharConstParser(
            Predicate<TChar> predicate
            , TResult result
        ) {
            this.predicate = predicate;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    predicate.Invoke(ch)
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct CharParser<TChar, TResult>
        : IParser<TChar, TResult>
    {

        readonly Predicate<TChar> predicate;

        readonly Converter<TChar, TResult> resultSelector;

        public CharParser(
            Predicate<TChar> predicate
            , Converter<TChar, TResult> resultSelector
        ) {
            this.predicate = predicate;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    predicate.Invoke(ch)
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct AnyCharIdentityParser<TChar>
        : IParser<TChar, TChar>
    {

        public IEnumerator<(TChar Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    true
                ) {
                    yield return (ch, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct AnyCharConstParser<TChar, TResult>
        : IParser<TChar, TResult>
    {


        readonly TResult result;

        public AnyCharConstParser(
            TResult result
        ) {
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    true
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct AnyCharParser<TChar, TResult>
        : IParser<TChar, TResult>
    {


        readonly Converter<TChar, TResult> resultSelector;

        public AnyCharParser(
            Converter<TChar, TResult> resultSelector
        ) {
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    true
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct SingleCharIdentityParser
        : IParser<char>
    {

        readonly char expected;

        public SingleCharIdentityParser(
            char expected
        ) {
            this.expected = expected;
        }

        public IEnumerator<(char Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.Equals(expected)
                ) {
                    yield return (ch, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct SingleCharConstParser<TResult>
        : IParser<TResult>
    {

        readonly char expected;

        readonly TResult result;

        public SingleCharConstParser(
            char expected
            , TResult result
        ) {
            this.expected = expected;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.Equals(expected)
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct SingleCharParser<TResult>
        : IParser<TResult>
    {

        readonly char expected;

        readonly Converter<char, TResult> resultSelector;

        public SingleCharParser(
            char expected
            , Converter<char, TResult> resultSelector
        ) {
            this.expected = expected;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.Equals(expected)
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct RangedCharIdentityParser
        : IParser<char>
    {

        readonly char minExpected;

        readonly char maxExpected;

        public RangedCharIdentityParser(
            char minExpected
            , char maxExpected
        ) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
        }

        public IEnumerator<(char Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.CompareTo(maxExpected) <= 0 && ch.CompareTo(minExpected) >= 0
                ) {
                    yield return (ch, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct RangedCharConstParser<TResult>
        : IParser<TResult>
    {

        readonly char minExpected;

        readonly char maxExpected;

        readonly TResult result;

        public RangedCharConstParser(
            char minExpected
            , char maxExpected
            , TResult result
        ) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.CompareTo(maxExpected) <= 0 && ch.CompareTo(minExpected) >= 0
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct RangedCharParser<TResult>
        : IParser<TResult>
    {

        readonly char minExpected;

        readonly char maxExpected;

        readonly Converter<char, TResult> resultSelector;

        public RangedCharParser(
            char minExpected
            , char maxExpected
            , Converter<char, TResult> resultSelector
        ) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.CompareTo(maxExpected) <= 0 && ch.CompareTo(minExpected) >= 0
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct CharIdentityParser
        : IParser<char>
    {

        readonly Predicate<char> predicate;

        public CharIdentityParser(
            Predicate<char> predicate
        ) {
            this.predicate = predicate;
        }

        public IEnumerator<(char Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    predicate.Invoke(ch)
                ) {
                    yield return (ch, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct CharConstParser<TResult>
        : IParser<TResult>
    {

        readonly Predicate<char> predicate;

        readonly TResult result;

        public CharConstParser(
            Predicate<char> predicate
            , TResult result
        ) {
            this.predicate = predicate;
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    predicate.Invoke(ch)
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct CharParser<TResult>
        : IParser<TResult>
    {

        readonly Predicate<char> predicate;

        readonly Converter<char, TResult> resultSelector;

        public CharParser(
            Predicate<char> predicate
            , Converter<char, TResult> resultSelector
        ) {
            this.predicate = predicate;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    predicate.Invoke(ch)
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct AnyCharIdentityParser
        : IParser<char>
    {

        public IEnumerator<(char Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    true
                ) {
                    yield return (ch, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct AnyCharConstParser<TResult>
        : IParser<TResult>
    {


        readonly TResult result;

        public AnyCharConstParser(
            TResult result
        ) {
            this.result = result;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    true
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly struct AnyCharParser<TResult>
        : IParser<TResult>
    {


        readonly Converter<char, TResult> resultSelector;

        public AnyCharParser(
            Converter<char, TResult> resultSelector
        ) {
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<char> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    true
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct SingleCharIdentityParser<TChar>
        : IParser<TChar>
        where TChar : IEquatable<TChar>
    {

        readonly TChar expected;

        public SingleCharIdentityParser(
            TChar expected
        ) {
            this.expected = expected;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.Equals(expected)
                ) {
                    yield return ((position, 1), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct SingleCharConstParser<TChar>
        : IParser<TChar>
        where TChar : IEquatable<TChar>
    {

        readonly TChar expected;

        readonly (int Start, int Length) result;

        public SingleCharConstParser(
            TChar expected
            , (int Start, int Length) result
        ) {
            this.expected = expected;
            this.result = result;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.Equals(expected)
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct SingleCharParser<TChar>
        : IParser<TChar>
        where TChar : IEquatable<TChar>
    {

        readonly TChar expected;

        readonly Converter<TChar, (int Start, int Length)> resultSelector;

        public SingleCharParser(
            TChar expected
            , Converter<TChar, (int Start, int Length)> resultSelector
        ) {
            this.expected = expected;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.Equals(expected)
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct RangedCharIdentityParser<TChar>
        : IParser<TChar>
        where TChar : IComparable<TChar>
    {

        readonly TChar minExpected;

        readonly TChar maxExpected;

        public RangedCharIdentityParser(
            TChar minExpected
            , TChar maxExpected
        ) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.CompareTo(maxExpected) <= 0 && ch.CompareTo(minExpected) >= 0
                ) {
                    yield return ((position, 1), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct RangedCharConstParser<TChar>
        : IParser<TChar>
        where TChar : IComparable<TChar>
    {

        readonly TChar minExpected;

        readonly TChar maxExpected;

        readonly (int Start, int Length) result;

        public RangedCharConstParser(
            TChar minExpected
            , TChar maxExpected
            , (int Start, int Length) result
        ) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
            this.result = result;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.CompareTo(maxExpected) <= 0 && ch.CompareTo(minExpected) >= 0
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct RangedCharParser<TChar>
        : IParser<TChar>
        where TChar : IComparable<TChar>
    {

        readonly TChar minExpected;

        readonly TChar maxExpected;

        readonly Converter<TChar, (int Start, int Length)> resultSelector;

        public RangedCharParser(
            TChar minExpected
            , TChar maxExpected
            , Converter<TChar, (int Start, int Length)> resultSelector
        ) {
            this.minExpected = minExpected;
            this.maxExpected = maxExpected;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    ch.CompareTo(maxExpected) <= 0 && ch.CompareTo(minExpected) >= 0
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct CharIdentityParser<TChar>
        : IParser<TChar>
    {

        readonly Predicate<TChar> predicate;

        public CharIdentityParser(
            Predicate<TChar> predicate
        ) {
            this.predicate = predicate;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    predicate.Invoke(ch)
                ) {
                    yield return ((position, 1), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct CharConstParser<TChar>
        : IParser<TChar>
    {

        readonly Predicate<TChar> predicate;

        readonly (int Start, int Length) result;

        public CharConstParser(
            Predicate<TChar> predicate
            , (int Start, int Length) result
        ) {
            this.predicate = predicate;
            this.result = result;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    predicate.Invoke(ch)
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct CharParser<TChar>
        : IParser<TChar>
    {

        readonly Predicate<TChar> predicate;

        readonly Converter<TChar, (int Start, int Length)> resultSelector;

        public CharParser(
            Predicate<TChar> predicate
            , Converter<TChar, (int Start, int Length)> resultSelector
        ) {
            this.predicate = predicate;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    predicate.Invoke(ch)
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct AnyCharIdentityParser<TChar>
        : IParser<TChar>
    {

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    true
                ) {
                    yield return ((position, 1), p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct AnyCharConstParser<TChar>
        : IParser<TChar>
    {


        readonly (int Start, int Length) result;

        public AnyCharConstParser(
            (int Start, int Length) result
        ) {
            this.result = result;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    true
                ) {
                    yield return (result, p);
                }
            }
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly struct AnyCharParser<TChar>
        : IParser<TChar>
    {


        readonly Converter<TChar, (int Start, int Length)> resultSelector;

        public AnyCharParser(
            Converter<TChar, (int Start, int Length)> resultSelector
        ) {
            this.resultSelector = resultSelector;
        }

        public IEnumerator<((int Start, int Length) Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var p = position;
            if (input.Count > p) {
                var ch = input[p++];
                if (
                    true
                ) {
                    yield return (resultSelector.Invoke(ch), p);
                }
            }
        }
    }
}
