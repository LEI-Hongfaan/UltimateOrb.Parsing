using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing {

    public static partial class Combinators {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParserSelectManyImpl2<TChar, TSource, TCollection, TResult> SelectMany<TChar, TSource, TCollection, TResult>(this IParser<TChar, TSource> source, IParser<TChar, TCollection> collection, Func<TSource, TCollection, TResult> resultSelector)
            where TChar : struct, IEquatable<TChar> {
            return new ParserSelectManyImpl2<TChar, TSource, TCollection, TResult>(source, collection, resultSelector);
        }

        public static ParserSelectManyImpl1<TChar, TSource, TCollection, TResult> SelectMany<TChar, TSource, TCollection, TResult>(this IParser<TChar, TSource> source, Func<TSource, IParser<TChar, TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
            where TChar : struct, IEquatable<TChar> {
            return new ParserSelectManyImpl1<TChar, TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserSelectManyImpl2<TChar, TSource, TCollection, TResult>
        : IParser<TChar, TResult>
        where TChar : struct, IEquatable<TChar> {

        private readonly IParser<TChar, TSource> source;
        private readonly IParser<TChar, TCollection> collection;
        private readonly Func<TSource, TCollection, TResult> resultSelector;

        public ParserSelectManyImpl2(IParser<TChar, TSource> source, IParser<TChar, TCollection> collection, Func<TSource, TCollection, TResult> resultSelector) {
            this.source = source;
            this.collection = collection;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var source_enumerator = source.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collection.Parse(input, source_current.Position);
                for (; collection_enumerator.MoveNext();) {
                    var collection_current = collection_enumerator.Current;
                    yield return (resultSelector.Invoke(source_current.Result, collection_current.Result), collection_current.Position);
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }

    public readonly struct ParserSelectManyImpl1<TChar, TSource, TCollection, TResult>
          : IParser<TChar, TResult> {
        private readonly IParser<TChar, TSource> source;
        private readonly Func<TSource, IParser<TChar, TCollection>> collectionSelector;
        private readonly Func<TSource, TCollection, TResult> resultSelector;

        public ParserSelectManyImpl1(IParser<TChar, TSource> source, Func<TSource, IParser<TChar, TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector) {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position = 0) where TString : IReadOnlyList<TChar> {
            var source_enumerator = source.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collectionSelector.Invoke(source_current.Result).Parse(input, source_current.Position);
                for (; collection_enumerator.MoveNext();) {
                    var collection_current = collection_enumerator.Current;
                    yield return (resultSelector.Invoke(source_current.Result, collection_current.Result), collection_current.Position);
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }
}