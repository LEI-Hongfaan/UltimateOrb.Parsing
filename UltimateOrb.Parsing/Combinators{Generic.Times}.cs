using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserTimesImpl<TChar, TResult> Times<TChar, TResult>(this IParser<TChar, TResult> parser, int occurrence) {
            return parser.Times(occurrence, occurrence);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserTimesImpl<TChar, TResult> Times<TChar, TResult>(this IParser<TChar, TResult> parser, int minOccurrence = 0, int maxOccurrence = Infinity) {
            return new ParserTimesImpl<TChar, TResult>(parser, minOccurrence, maxOccurrence);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserTimesWithAggregatingImpl<TChar, TSource, TAccumulate> Times<TChar, TSource, TAccumulate>(this IParser<TChar, TSource> parser, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, int minOccurrence = 0, int maxOccurrence = Infinity) {
            return new ParserTimesWithAggregatingImpl<TChar, TSource, TAccumulate>(parser, minOccurrence, maxOccurrence, seed, func);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserTimesWithAggregatingImpl<TChar, TSource, TAccumulate, TResult> Times<TChar, TSource, TAccumulate, TResult>(this IParser<TChar, TSource> parser, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, int minOccurrence = 0, int maxOccurrence = Infinity) {
            return new ParserTimesWithAggregatingImpl<TChar, TSource, TAccumulate, TResult>(parser, minOccurrence, maxOccurrence, seed, func, resultSelector);
        }
    }


}

namespace UltimateOrb.Parsing.Generic {
    using static Combinators;

    public readonly struct ParserTimesImpl<TChar, TResult>
        : IParser<TChar, ImmutableList<TResult>> {

        readonly IParser<TChar, TResult> parser;

        readonly int minOccurrence;

        readonly int maxOccurrence;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserTimesImpl(IParser<TChar, TResult> parser, int occurrence) {
            this.parser = parser;
            this.minOccurrence = occurrence;
            this.maxOccurrence = occurrence;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserTimesImpl(IParser<TChar, TResult> parser, int minOccurrence, int maxOccurrence = Infinity) {
            this.parser = parser;
            this.minOccurrence = minOccurrence;
            this.maxOccurrence = maxOccurrence;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(ImmutableList<TResult> Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            if (0 == minOccurrence) {
                yield return (ImmutableList<TResult>.Empty, position);
            }
            if (0 == maxOccurrence) {
                yield break;
            }
            var acc = ImmutableList.CreateBuilder<TResult>();
            var s = new Stack<IEnumerator<(TResult Result, int Position)>>();
            var enumerator = parser.Parse(input, position);
            for (var count = 0; ;) {
                System.Diagnostics.Debug.Assert(count == acc.Count);
                if (enumerator.MoveNext()) {
                    ++count;
                    var current = enumerator.Current;
                    acc.Add(current.Result);
                    System.Diagnostics.Debug.Assert(count == acc.Count);
                    if (minOccurrence <= count) {
                        yield return (acc.ToImmutable(), current.Position);
                    }
                    if (Infinity == maxOccurrence || maxOccurrence > count) {
                        s.Push(enumerator);
                        enumerator = parser.Parse(input, current.Position);
                        continue;
                    }
                    --count;
                    for (; enumerator.MoveNext();) {
                        current = enumerator.Current;
                        acc[count] = current.Result;
                        yield return (acc.ToImmutable(), current.Position);
                    }
                    acc.RemoveAt(count);
                }
                System.Diagnostics.Debug.Assert(count == s.Count);
                enumerator.Dispose();
                if (0 == count--) {
                    System.Diagnostics.Debug.Assert(0 == acc.Count);
                    yield break;
                }
                enumerator = s.Pop();
                acc.RemoveAt(count);
            }
        }
    }


    public readonly struct ParserTimesWithAggregatingImpl<TChar, TSource, TAccumulate>
        : IParser<TChar, TAccumulate> {

        private readonly IParser<TChar, TSource> parser;
        private readonly int minOccurrence;
        private readonly int maxOccurrence;
        private readonly TAccumulate seed;
        private readonly Func<TAccumulate, TSource, TAccumulate> func;

        public ParserTimesWithAggregatingImpl(IParser<TChar, TSource> parser, int minOccurrence, int maxOccurrence, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func) {
            this.parser = parser;
            this.minOccurrence = minOccurrence;
            this.maxOccurrence = maxOccurrence;
            this.seed = seed;
            this.func = func;
        }

        public IEnumerator<(TAccumulate Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            if (0 == minOccurrence) {
                yield return (seed, position);
            }
            if (0 == maxOccurrence) {
                yield break;
            }
            var acc = seed;
            var s = new Stack<(IEnumerator<(TSource Result, int Position)> Enumerator, TAccumulate Result)>();
            var enumerator = parser.Parse(input, position);
            for (var count = 0; ;) {
                if (enumerator.MoveNext()) {
                    ++count;
                    var current = enumerator.Current;
                    var t = func(acc, current.Result);
                    if (minOccurrence <= count) {
                        yield return (t, current.Position);
                    }
                    if (Infinity == maxOccurrence || maxOccurrence > count) {
                        s.Push((enumerator, acc));
                        acc = t;
                        enumerator = parser.Parse(input, current.Position);
                        continue;
                    }
                    --count;
                    for (; enumerator.MoveNext();) {
                        current = enumerator.Current;
                        yield return (func(acc, current.Result), current.Position);
                    }
                }
                System.Diagnostics.Debug.Assert(count == s.Count);
                enumerator.Dispose();
                if (0 == count--) {
                    yield break;
                }
                (enumerator, acc) = s.Pop();
            }
        }
    }

    public readonly struct ParserTimesWithAggregatingImpl<TChar, TSource, TAccumulate, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TSource> parser;
        private readonly int minOccurrence;
        private readonly int maxOccurrence;
        private readonly TAccumulate seed;
        private readonly Func<TAccumulate, TSource, TAccumulate> func;
        private readonly Func<TAccumulate, TResult> resultSelector;

        public ParserTimesWithAggregatingImpl(IParser<TChar, TSource> parser, int minOccurrence, int maxOccurrence, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector) {
            this.parser = parser;
            this.minOccurrence = minOccurrence;
            this.maxOccurrence = maxOccurrence;
            this.seed = seed;
            this.func = func;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            if (0 == minOccurrence) {
                yield return (resultSelector.Invoke(seed), position);
            }
            if (0 == maxOccurrence) {
                yield break;
            }
            var acc = seed;
            var s = new Stack<(IEnumerator<(TSource Result, int Position)> Enumerator, TAccumulate Result)>();
            var enumerator = parser.Parse(input, position);
            for (var count = 0; ;) {
                if (enumerator.MoveNext()) {
                    ++count;
                    var current = enumerator.Current;
                    var t = func(acc, current.Result);
                    if (minOccurrence <= count) {
                        yield return (resultSelector.Invoke(t), current.Position);
                    }
                    if (Infinity == maxOccurrence || maxOccurrence > count) {
                        s.Push((enumerator, acc));
                        acc = t;
                        enumerator = parser.Parse(input, current.Position);
                        continue;
                    }
                    --count;
                    for (; enumerator.MoveNext();) {
                        current = enumerator.Current;
                        yield return (resultSelector.Invoke(func(acc, current.Result)), current.Position);
                    }
                }
                System.Diagnostics.Debug.Assert(count == s.Count);
                enumerator.Dispose();
                if (0 == count--) {
                    yield break;
                }
                (enumerator, acc) = s.Pop();
            }
        }
    }
}
