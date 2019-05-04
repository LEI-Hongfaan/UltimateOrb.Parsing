using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectImpl<TChar, TSource, TResult> Select<TChar, TSource, TResult>(this IParser<TChar, TSource> source, Func<TSource, TResult> selector) {
            return new ParserSelectImpl<TChar, TSource, TResult>(source, selector);
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyWrapper<T, WithPositionT> WithPosition<T>(this T result) {
            return result;
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyWrapper<T, WithInputT> WithInput<T>(this T result) {
            return result;
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static Func<(TSource Result, int Position), (TResult Result, int Position)> ResultSelector<TSource, TResult>(this Func<TSource, int, (TResult Result, int Position)> selector) {
            return x => selector.Invoke(x.Result, x.Position);
        }


        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static Func<(TSource Result, int Position), (TResult Result, int Position)> ResultSelector<TSource, TResult>(this Func<TSource, TResult> selector) {
            return x => (selector.Invoke(x.Result), x.Position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectWithInputImpl<TChar, TSource, TResult> Select<TChar, TSource, TResult>(this ReadOnlyWrapper<IParser<TChar, TSource>, WithInputT> source, Func<(IReadOnlyList<TChar> Input, int Position), ReadOnlyWrapper<((IReadOnlyList<TChar> Input, int Position) Redirected, Func<(TSource Result, int Position), (TResult Result, int Position)> ResultSelector), WithInputT>> selector) {
            return new ParserSelectWithInputImpl<TChar, TSource, TResult>(source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectWithPositionImpl<TChar, TSource, TResult> Select<TChar, TSource, TResult>(this ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source, Func<(TSource Result, int Position), TResult> selector) {
            return new ParserSelectWithPositionImpl<TChar, TSource, TResult>(source.Value, r => (selector.Invoke(r), r.Position));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectWithPositionImpl<TChar, TSource, TResult> Select<TChar, TSource, TResult>(this ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source, Func<(TSource Result, int Position), ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> selector) {
            return new ParserSelectWithPositionImpl<TChar, TSource, TResult>(source.Value, r => selector.Invoke(r));
        }

        /*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectWithPositionImpl1<TChar, TSource, TResult> Select<TChar, TSource, TResult>(this IParser<TChar, TSource> source, Func<(TSource Result, int Position), ReadOnlyWrapper<TResult, WithPositionT>> selector) {
            return new ParserSelectWithPositionImpl1<TChar, TSource, TResult>(source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectWithPositionImpl1<TChar, TSource, TResult> Select<TChar, TSource, TResult>(this IParser<TChar, TSource> source, Func<(TSource Result, int Position), ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> selector) {
            return new ParserSelectWithPositionImpl1<TChar, TSource, TResult>(source, selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectWithPositionImpl<TChar, TSource, TResult> Select<TChar, TSource, TResult>(this IParser<TChar, TSource> source, Func<(TSource Result, int Position), (TResult Result, int Position)> selector) {
            return new ParserSelectWithPositionImpl<TChar, TSource, TResult>(source, selector);
        }
        */

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectWithPositionImpl<TChar, TSource, TResult> Select<TChar, TSource, TResult>(this IParser<TChar, TSource> source, Func<TSource, ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> selector) {
            return new ParserSelectWithPositionImpl<TChar, TSource, TResult>(source, r => selector.Invoke(r.Result));
        }
    }


    public readonly struct WithPositionT {
    }

    public readonly struct WithInputT {
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserSelectImpl<TChar, TSource, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TSource> source;

        private readonly Func<TSource, TResult> selector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectImpl(IParser<TChar, TSource> source, Func<TSource, TResult> selector) {
            this.source = source;
            this.selector = selector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var enumerator = source.Parse(input, position);
            for (; enumerator.MoveNext();) {
                var current = enumerator.Current;
                yield return (selector.Invoke(current.Result), current.Position);
            }
            enumerator.Dispose();
        }
    }

    public readonly struct ParserSelectWithPositionImpl1<TChar, TSource, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TSource> source;

        private readonly Func<(TSource Result, int Position), ReadOnlyWrapper<TResult, WithPositionT>> selector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectWithPositionImpl1(IParser<TChar, TSource> source, Func<(TSource Result, int Position), ReadOnlyWrapper<TResult, WithPositionT>> selector) {
            this.source = source;
            this.selector = selector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var enumerator = source.Parse(input, position);
            for (; enumerator.MoveNext();) {
                var current = enumerator.Current;
                yield return (selector.Invoke(current), current.Position);
            }
            enumerator.Dispose();
        }
    }

    public readonly struct ParserSelectWithPositionImpl<TChar, TSource, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TSource> source;

        private readonly Func<(TSource Result, int Position), (TResult Result, int Position)> selector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectWithPositionImpl(IParser<TChar, TSource> source, Func<(TSource Result, int Position), (TResult Result, int Position)> selector) {
            this.source = source;
            this.selector = selector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var enumerator = source.Parse(input, position);
            for (; enumerator.MoveNext();) {
                var current = enumerator.Current;
                yield return selector.Invoke(current);
            }
            enumerator.Dispose();
        }
    }



    public readonly struct ParserSelectWithInputImpl<TChar, TSource, TResult>
        : IParser<TChar, TResult> {

        private readonly ReadOnlyWrapper<IParser<TChar, TSource>, WithInputT> source;

        private readonly Func<(IReadOnlyList<TChar> Input, int Position), ReadOnlyWrapper<((IReadOnlyList<TChar> Input, int Position) Redirected, Func<(TSource Result, int Position), (TResult Result, int Position)> ResultSelector), WithInputT>> selector;

        public ParserSelectWithInputImpl(ReadOnlyWrapper<IParser<TChar, TSource>, WithInputT> source, Func<(IReadOnlyList<TChar> Input, int Position), ReadOnlyWrapper<((IReadOnlyList<TChar> Input, int Position) Redirected, Func<(TSource Result, int Position), (TResult Result, int Position)> ResultSelector), WithInputT>> selector) {
            this.source = source;
            this.selector = selector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var (redirected, resultSelecor) = selector.Invoke((input, position)).Value;
            var enumerator = source.Value.Parse(redirected.Input, redirected.Position);
            for (; enumerator.MoveNext();) {
                var current = enumerator.Current;
                yield return resultSelecor.Invoke(current);
            }
            enumerator.Dispose();
        }
    }
}
