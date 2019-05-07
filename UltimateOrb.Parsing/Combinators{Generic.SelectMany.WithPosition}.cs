using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UltimateOrb.Parsing.Generic;


namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectManyWithPositionImpl1<TChar, TSource, TCollection, TResult> SelectMany<TChar, TSource, TCollection, TResult>(this IParser<TChar, TSource> source, Func<TSource, IParser<TChar, TCollection>> collectionSelector, Func<TSource, TCollection, ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector) {
            return new ParserSelectManyWithPositionImpl1<TChar, TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserSelectManyWithPositionImpl1<TChar, TSource, TCollection, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TSource> source;

        private readonly Func<TSource, IParser<TChar, TCollection>> collectionSelector;

        private readonly Func<TSource, TCollection, ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectManyWithPositionImpl1(IParser<TChar, TSource> source, Func<TSource, IParser<TChar, TCollection>> collectionSelector, Func<TSource, TCollection, ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector) {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var source_enumerator = source.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collectionSelector.Invoke(source_current.Result).Parse(input, source_current.Position);
                for (; collection_enumerator.MoveNext();) {
                    var collection_current = collection_enumerator.Current;
                    yield return resultSelector.Invoke(source_current.Result, collection_current.Result).Value;
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }
}

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectManyWithPositionImpl2<TChar, TSource, TCollection, TResult> SelectMany<TChar, TSource, TCollection, TResult>(this ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source, Func<(TSource Result, int Position), IParser<TChar, TCollection>> collectionSelector, Func<(TSource Result, int Position), TCollection, TResult> resultSelector) {
            return new ParserSelectManyWithPositionImpl2<TChar, TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserSelectManyWithPositionImpl2<TChar, TSource, TCollection, TResult>
        : IParser<TChar, TResult> {

        private readonly ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source;

        private readonly Func<(TSource Result, int Position), IParser<TChar, TCollection>> collectionSelector;

        private readonly Func<(TSource Result, int Position), TCollection, TResult> resultSelector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectManyWithPositionImpl2(ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source, Func<(TSource Result, int Position), IParser<TChar, TCollection>> collectionSelector, Func<(TSource Result, int Position), TCollection, TResult> resultSelector) {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var source_enumerator = source.Value.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collectionSelector.Invoke(source_current).Parse(input, source_current.Position);
                for (; collection_enumerator.MoveNext();) {
                    var collection_current = collection_enumerator.Current;
                    yield return (resultSelector.Invoke(source_current, collection_current.Result), collection_current.Position);
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }
}

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectManyWithPositionImpl3<TChar, TSource, TCollection, TResult> SelectMany<TChar, TSource, TCollection, TResult>(this ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source, Func<(TSource Result, int Position), IParser<TChar, TCollection>> collectionSelector, Func<(TSource Result, int Position), TCollection, ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector) {
            return new ParserSelectManyWithPositionImpl3<TChar, TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserSelectManyWithPositionImpl3<TChar, TSource, TCollection, TResult>
        : IParser<TChar, TResult> {

        private readonly ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source;

        private readonly Func<(TSource Result, int Position), IParser<TChar, TCollection>> collectionSelector;

        private readonly Func<(TSource Result, int Position), TCollection, ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectManyWithPositionImpl3(ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source, Func<(TSource Result, int Position), IParser<TChar, TCollection>> collectionSelector, Func<(TSource Result, int Position), TCollection, ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector) {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var source_enumerator = source.Value.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collectionSelector.Invoke(source_current).Parse(input, source_current.Position);
                for (; collection_enumerator.MoveNext();) {
                    var collection_current = collection_enumerator.Current;
                    yield return resultSelector.Invoke(source_current, collection_current.Result).Value;
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }
}

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectManyWithPositionImpl4<TChar, TSource, TCollection, TResult> SelectMany<TChar, TSource, TCollection, TResult>(this IParser<TChar, TSource> source, Func<TSource, ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector, Func<TSource, (TCollection Result, int Position), TResult> resultSelector) {
            return new ParserSelectManyWithPositionImpl4<TChar, TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserSelectManyWithPositionImpl4<TChar, TSource, TCollection, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TSource> source;

        private readonly Func<TSource, ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector;

        private readonly Func<TSource, (TCollection Result, int Position), TResult> resultSelector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectManyWithPositionImpl4(IParser<TChar, TSource> source, Func<TSource, ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector, Func<TSource, (TCollection Result, int Position), TResult> resultSelector) {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var source_enumerator = source.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collectionSelector.Invoke(source_current.Result).Value.Parse(input, source_current.Position);
                for (; collection_enumerator.MoveNext();) {
                    var collection_current = collection_enumerator.Current;
                    yield return (resultSelector.Invoke(source_current.Result, collection_current), collection_current.Position);
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }
}

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectManyWithPositionImpl5<TChar, TSource, TCollection, TResult> SelectMany<TChar, TSource, TCollection, TResult>(this IParser<TChar, TSource> source, Func<TSource, ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector, Func<TSource, (TCollection Result, int Position), ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector) {
            return new ParserSelectManyWithPositionImpl5<TChar, TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserSelectManyWithPositionImpl5<TChar, TSource, TCollection, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TSource> source;

        private readonly Func<TSource, ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector;

        private readonly Func<TSource, (TCollection Result, int Position), ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectManyWithPositionImpl5(IParser<TChar, TSource> source, Func<TSource, ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector, Func<TSource, (TCollection Result, int Position), ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector) {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var source_enumerator = source.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collectionSelector.Invoke(source_current.Result).Value.Parse(input, source_current.Position);
                for (; collection_enumerator.MoveNext();) {
                    var collection_current = collection_enumerator.Current;
                    yield return resultSelector.Invoke(source_current.Result, collection_current).Value;
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }
}

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectManyWithPositionImpl6<TChar, TSource, TCollection, TResult> SelectMany<TChar, TSource, TCollection, TResult>(this ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source, Func<(TSource Result, int Position), ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector, Func<(TSource Result, int Position), (TCollection Result, int Position), TResult> resultSelector) {
            return new ParserSelectManyWithPositionImpl6<TChar, TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserSelectManyWithPositionImpl6<TChar, TSource, TCollection, TResult>
        : IParser<TChar, TResult> {

        private readonly ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source;

        private readonly Func<(TSource Result, int Position), ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector;

        private readonly Func<(TSource Result, int Position), (TCollection Result, int Position), TResult> resultSelector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectManyWithPositionImpl6(ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source, Func<(TSource Result, int Position), ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector, Func<(TSource Result, int Position), (TCollection Result, int Position), TResult> resultSelector) {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var source_enumerator = source.Value.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collectionSelector.Invoke(source_current).Value.Parse(input, source_current.Position);
                for (; collection_enumerator.MoveNext();) {
                    var collection_current = collection_enumerator.Current;
                    yield return (resultSelector.Invoke(source_current, collection_current), collection_current.Position);
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }
}

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectManyWithPositionImpl7<TChar, TSource, TCollection, TResult> SelectMany<TChar, TSource, TCollection, TResult>(this ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source, Func<(TSource Result, int Position), ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector, Func<(TSource Result, int Position), (TCollection Result, int Position), ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector) {
            return new ParserSelectManyWithPositionImpl7<TChar, TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserSelectManyWithPositionImpl7<TChar, TSource, TCollection, TResult>
        : IParser<TChar, TResult> {

        private readonly ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source;

        private readonly Func<(TSource Result, int Position), ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector;

        private readonly Func<(TSource Result, int Position), (TCollection Result, int Position), ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParserSelectManyWithPositionImpl7(ReadOnlyWrapper<IParser<TChar, TSource>, WithPositionT> source, Func<(TSource Result, int Position), ReadOnlyWrapper<IParser<TChar, TCollection>, WithPositionT>> collectionSelector, Func<(TSource Result, int Position), (TCollection Result, int Position), ReadOnlyWrapper<(TResult Result, int Position), WithPositionT>> resultSelector) {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var source_enumerator = source.Value.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collectionSelector.Invoke(source_current).Value.Parse(input, source_current.Position);
                for (; collection_enumerator.MoveNext();) {
                    var collection_current = collection_enumerator.Current;
                    yield return resultSelector.Invoke(source_current, collection_current).Value;
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }
}
