
using System;
using System.Collections.Generic;

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct SingleCharIdentityParser<TChar>
        : IReversibleParser<TChar, TChar>
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

        public SingleCharIdentityParser<TChar> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TChar> IReversibleParser<TChar, TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct SingleCharConstParser<TChar, TResult>
        : IReversibleParser<TChar, TResult>
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

        public SingleCharConstParser<TChar, TResult> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TResult> IReversibleParser<TChar, TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct SingleCharParser<TChar, TResult>
        : IReversibleParser<TChar, TResult>
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

        public SingleCharParser<TChar, TResult> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TResult> IReversibleParser<TChar, TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct RangedCharIdentityParser<TChar>
        : IReversibleParser<TChar, TChar>
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

        public RangedCharIdentityParser<TChar> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TChar> IReversibleParser<TChar, TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct RangedCharConstParser<TChar, TResult>
        : IReversibleParser<TChar, TResult>
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

        public RangedCharConstParser<TChar, TResult> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TResult> IReversibleParser<TChar, TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct RangedCharParser<TChar, TResult>
        : IReversibleParser<TChar, TResult>
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

        public RangedCharParser<TChar, TResult> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TResult> IReversibleParser<TChar, TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct CharIdentityParser<TChar>
        : IReversibleParser<TChar, TChar>
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

        public CharIdentityParser<TChar> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TChar> IReversibleParser<TChar, TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct CharConstParser<TChar, TResult>
        : IReversibleParser<TChar, TResult>
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

        public CharConstParser<TChar, TResult> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TResult> IReversibleParser<TChar, TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct CharParser<TChar, TResult>
        : IReversibleParser<TChar, TResult>
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

        public CharParser<TChar, TResult> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TResult> IReversibleParser<TChar, TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct AnyCharIdentityParser<TChar>
        : IReversibleParser<TChar, TChar>
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

        public AnyCharIdentityParser<TChar> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TChar> IReversibleParser<TChar, TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct AnyCharConstParser<TChar, TResult>
        : IReversibleParser<TChar, TResult>
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

        public AnyCharConstParser<TChar, TResult> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TResult> IReversibleParser<TChar, TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly partial struct AnyCharParser<TChar, TResult>
        : IReversibleParser<TChar, TResult>
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

        public AnyCharParser<TChar, TResult> Reversed() {
            return this;
        }

        IReversibleParser<TChar, TResult> IReversibleParser<TChar, TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct SingleCharIdentityParser
        : IReversibleParser<char>
        , RegularExpressions.IRegularExpressionSource
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

        public string GetRegexPattern() {
            
            return new string(expected, 1);
        }

        public SingleCharIdentityParser Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, char> Generic.IReversibleParser<char, char>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<char> IReversibleParser<char>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct SingleCharConstParser<TResult>
        : IReversibleParser<TResult>
        , RegularExpressions.IRegularExpressionSource
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

        public string GetRegexPattern() {
            
            return new string(expected, 1);
        }

        public SingleCharConstParser<TResult> Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, TResult> Generic.IReversibleParser<char, TResult>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TResult> IReversibleParser<TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct SingleCharParser<TResult>
        : IReversibleParser<TResult>
        , RegularExpressions.IRegularExpressionSource
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

        public string GetRegexPattern() {
            
            return new string(expected, 1);
        }

        public SingleCharParser<TResult> Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, TResult> Generic.IReversibleParser<char, TResult>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TResult> IReversibleParser<TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct RangedCharIdentityParser
        : IReversibleParser<char>
        , RegularExpressions.IRegularExpressionSource
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

        public string GetRegexPattern() {
            var t = new char[] { '[', minExpected, '-', maxExpected, ']' };
            return new string(t);
        }

        public RangedCharIdentityParser Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, char> Generic.IReversibleParser<char, char>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<char> IReversibleParser<char>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct RangedCharConstParser<TResult>
        : IReversibleParser<TResult>
        , RegularExpressions.IRegularExpressionSource
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

        public string GetRegexPattern() {
            var t = new char[] { '[', minExpected, '-', maxExpected, ']' };
            return new string(t);
        }

        public RangedCharConstParser<TResult> Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, TResult> Generic.IReversibleParser<char, TResult>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TResult> IReversibleParser<TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct RangedCharParser<TResult>
        : IReversibleParser<TResult>
        , RegularExpressions.IRegularExpressionSource
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

        public string GetRegexPattern() {
            var t = new char[] { '[', minExpected, '-', maxExpected, ']' };
            return new string(t);
        }

        public RangedCharParser<TResult> Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, TResult> Generic.IReversibleParser<char, TResult>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TResult> IReversibleParser<TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct CharIdentityParser
        : IReversibleParser<char>
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

        public CharIdentityParser Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, char> Generic.IReversibleParser<char, char>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<char> IReversibleParser<char>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct CharConstParser<TResult>
        : IReversibleParser<TResult>
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

        public CharConstParser<TResult> Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, TResult> Generic.IReversibleParser<char, TResult>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TResult> IReversibleParser<TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct CharParser<TResult>
        : IReversibleParser<TResult>
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

        public CharParser<TResult> Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, TResult> Generic.IReversibleParser<char, TResult>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TResult> IReversibleParser<TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct AnyCharIdentityParser
        : IReversibleParser<char>
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

        public AnyCharIdentityParser Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, char> Generic.IReversibleParser<char, char>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<char> IReversibleParser<char>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct AnyCharConstParser<TResult>
        : IReversibleParser<TResult>
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

        public AnyCharConstParser<TResult> Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, TResult> Generic.IReversibleParser<char, TResult>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TResult> IReversibleParser<TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Text {

    public readonly partial struct AnyCharParser<TResult>
        : IReversibleParser<TResult>
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

        public AnyCharParser<TResult> Reversed() {
            return this;
        }

        Generic.IReversibleParser<char, TResult> Generic.IReversibleParser<char, TResult>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TResult> IReversibleParser<TResult>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct SingleCharIdentityParser<TChar>
        : IReversibleParser<TChar>
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

        public SingleCharIdentityParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct SingleCharConstParser<TChar>
        : IReversibleParser<TChar>
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

        public SingleCharConstParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct SingleCharParser<TChar>
        : IReversibleParser<TChar>
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

        public SingleCharParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct RangedCharIdentityParser<TChar>
        : IReversibleParser<TChar>
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

        public RangedCharIdentityParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct RangedCharConstParser<TChar>
        : IReversibleParser<TChar>
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

        public RangedCharConstParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct RangedCharParser<TChar>
        : IReversibleParser<TChar>
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

        public RangedCharParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct CharIdentityParser<TChar>
        : IReversibleParser<TChar>
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

        public CharIdentityParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct CharConstParser<TChar>
        : IReversibleParser<TChar>
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

        public CharConstParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct CharParser<TChar>
        : IReversibleParser<TChar>
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

        public CharParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct AnyCharIdentityParser<TChar>
        : IReversibleParser<TChar>
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

        public AnyCharIdentityParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct AnyCharConstParser<TChar>
        : IReversibleParser<TChar>
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

        public AnyCharConstParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}

namespace UltimateOrb.Parsing.Segments {

    public readonly partial struct AnyCharParser<TChar>
        : IReversibleParser<TChar>
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

        public AnyCharParser<TChar> Reversed() {
            return this;
        }

        Generic.IReversibleParser<TChar, (int Start, int Length)> Generic.IReversibleParser<TChar, (int Start, int Length)>.Reversed() {
            return this.Reversed();
        }

        IReversibleParser<TChar> IReversibleParser<TChar>.Reversed() {
            return this.Reversed();
        }
    }
}
