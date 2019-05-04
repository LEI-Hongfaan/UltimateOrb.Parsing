using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb {

    public interface IReadOnlyListEnumerator<out T>
        : IEnumerator<T> {

        bool MovePrevious();

        bool MoveNext(int count);

        bool MovePrevious(int count);

        bool MoveTo(int index);
    }
}
namespace UltimateOrb.Parsing.Generic {
    public readonly struct InputReverse<TChar, TOutput>
             : IInputTransform<TChar, TOutput>
             where TOutput : IReadOnlyList<TChar> {

        public static readonly InputReverse<TChar, TOutput> Value = default;

        public int Inverse(TOutput input, int position) {
            return input.Count - position;
        }

        public (TOutput Input, int Position) Invoke<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            if (typeof(Text.StringAsCharList) == typeof(TString)) {
                var output = new ReversedReadOnlyList<TChar, IReadOnlyList<TChar>>(input, position);
                return ((TOutput)(object)output, input.Count - position);
            }
            {
                var output = new ReversedReadOnlyList<TChar, TString>(input, position);
                return ((TOutput)(object)output, input.Count - position);
            }
            
        }
    }

    public readonly struct ReversedReadOnlyList<T, TList>
        : IReadOnlyList<T>
        where TList : IReadOnlyList<T> {

        readonly TList input;

        public ReversedReadOnlyList(TList input, int position) : this() {
            this.input = input;
        }

        public T this[int index] {

            get => input[input.Count - 1 - index];
        }

        public int Count {

            get => input.Count;
        }

        public IEnumerator<T> GetEnumerator() {
            return input.Reverse().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }


}
namespace UltimateOrb.Parsing {

    public partial interface IInputTransform<in TChar, TOutput>
            where TOutput : IReadOnlyList<TChar> {

        (TOutput Input, int Position) Invoke<TString>(TString input, int position)
            where TString : IReadOnlyList<TChar>;

        int Inverse(TOutput input, int position);
    }

    public static partial class Combinators {


        public static IParser<TChar, TResult> WithInputTransform<TChar, TResult, TString, TTransform>(this IParser<TChar, TResult> parser, TTransform transform)
            where TString : IReadOnlyList<TChar>
            where TTransform : IInputTransform<TChar, TString> {
            return new ParserWithInputTransformImpl<TChar, TResult, TString, TTransform>(parser, transform);
        }
        public static IParser<TChar, TResult> AttachPositiveTo<TChar, TResult>(this IReversibleParser<TChar, VoidResult> lookbehindAssertion, IParser<TChar, TResult> capturing) {
            return
                from start in PerTypeValues<TChar>.Empty.WithPosition()
                from result in capturing.WithPosition()
                select (result, start.Position).WithPosition() into result
                from assert in lookbehindAssertion.Reversed().WithInputTransform<TChar, VoidResult, ReversedReadOnlyList<TChar, IReadOnlyList<TChar>>, InputReverse<TChar, ReversedReadOnlyList<TChar, IReadOnlyList<TChar>>>>(default).Any()
                where assert
                select result.WithPosition();
        }

        public static IParser<TChar, TResult> AttachNegativeTo<TChar, TResult>(this IReversibleParser<TChar, VoidResult> lookbehindAssertion, IParser<TChar, TResult> capturing) {
            return
                from start in PerTypeValues<TChar>.Empty.WithPosition()
                from result in capturing.WithPosition()
                select (result, start.Position).WithPosition() into result
                from assert in lookbehindAssertion.Reversed().WithInputTransform<TChar, VoidResult, ReversedReadOnlyList<TChar, IReadOnlyList<TChar>>, InputReverse<TChar, ReversedReadOnlyList<TChar, IReadOnlyList<TChar>>>>(default).Any()
                where !assert
                select result.WithPosition();
        }

        public static IParser<TChar, TResult> AttachPositiveTo<TChar, TAssert, TResult>(this IReversibleParser<TChar, TAssert> lookbehindAssertion, IParser<TChar, TResult> capturing) {
            return
                from start in PerTypeValues<TChar>.Empty.WithPosition()
                from result in capturing.WithPosition()
                select (result, start.Position).WithPosition() into result
                from assert in lookbehindAssertion.Reversed().WithInputTransform<TChar, TAssert, ReversedReadOnlyList<TChar, IReadOnlyList<TChar>>, InputReverse<TChar, ReversedReadOnlyList<TChar, IReadOnlyList<TChar>>>>(default).Any()
                where assert
                select result.WithPosition();
        }

        private static partial class PerTypeValues<TChar> {

            internal static IParser<TChar, VoidResult> Empty = new EmptyParser<TChar, VoidResult>(default);

            internal static IParser<TChar, int> CurrentPosition =
                from r in Empty.WithPosition()
                select r.Position;
        }

        public static IParser<TChar, TResult> AttachNegativeTo<TChar, TAssert, TResult>(this IReversibleParser<TChar, TAssert> lookbehindAssertion, IParser<TChar, TResult> capturing) {
            return
                from start in PerTypeValues<TChar>.Empty.WithPosition()
                from result in capturing.WithPosition()
                select (result, start.Position).WithPosition() into result
                from assert in lookbehindAssertion.Reversed().WithInputTransform<TChar, TAssert, ReversedReadOnlyList<TChar, IReadOnlyList<TChar>>, InputReverse<TChar, ReversedReadOnlyList<TChar, IReadOnlyList<TChar>>>>(default).Any()
                where !assert
                select result.WithPosition();
        }

        public static IParser<TChar, TResult> AttachPositive<TChar, TResult>(this IParser<TChar, TResult> capturing, IParser<TChar, VoidResult> lookaheadAssertion) {
            return
                from result in capturing
                from assert in lookaheadAssertion.Any()
                where assert
                select result;
        }

        public static IParser<TChar, TResult> AttachNegative<TChar, TResult>(this IParser<TChar, TResult> capturing, IParser<TChar, VoidResult> lookaheadAssertion) {
            return
                from result in capturing
                from assert in lookaheadAssertion.Any()
                where !assert
                select result;
        }

        public static ParserAttachPositiveImpl<TChar, TAssert, TResult> AttachPositive<TChar, TAssert, TResult>(this IParser<TChar, TResult> capturing, IParser<TChar, TAssert> lookaheadAssertion) {
            return new ParserAttachPositiveImpl<TChar, TAssert, TResult>(capturing, lookaheadAssertion);
        }

        public static ParserAttachNegativeImpl<TChar, TAssert, TResult> AttachNegative<TChar, TAssert, TResult>(this IParser<TChar, TResult> capturing, IParser<TChar, TAssert> lookaheadAssertion) {
            return new ParserAttachNegativeImpl<TChar, TAssert, TResult>(capturing, lookaheadAssertion);
        }
    }

    internal class ParserWithInputTransformImpl<TChar, TResult, TString, TTransform>
        : IParser<TChar, TResult>
        where TString : IReadOnlyList<TChar>
        where TTransform : IInputTransform<TChar, TString> {

        private IParser<TChar, TResult> parser;

        private TTransform transform;

        public ParserWithInputTransformImpl(IParser<TChar, TResult> parser, TTransform transform) {
            this.parser = parser;
            this.transform = transform;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString1>(TString1 input, int position) where TString1 : IReadOnlyList<TChar> {
            var transformed = transform.Invoke(input, position);
            var enumerator = this.parser.Parse(transformed.Input, transformed.Position);
            for ( ; enumerator.MoveNext(); ) {
                var current = enumerator.Current;
                yield return (current.Result, transform.Inverse(transformed.Input, current.Position));
            }
            enumerator.Dispose();
        }
    }
}

namespace UltimateOrb.Parsing.Generic {

    public readonly struct ParserAttachPositiveImpl<TChar, TAssert, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TResult> capturing;

        private readonly IParser<TChar, TAssert> lookaheadAssertion;

        public ParserAttachPositiveImpl(IParser<TChar, TResult> capturing, IParser<TChar, TAssert> lookaheadAssertion) {
            this.lookaheadAssertion = lookaheadAssertion;
            this.capturing = capturing;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var source = capturing;
            var collection = lookaheadAssertion;
            var source_enumerator = source.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collection.Parse(input, source_current.Position);
                if (collection_enumerator.Any()) {
                    yield return source_current;
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }


    public readonly struct ParserAttachNegativeImpl<TChar, TAssert, TResult>
        : IParser<TChar, TResult> {

        private readonly IParser<TChar, TResult> capturing;

        private readonly IParser<TChar, TAssert> lookaheadAssertion;

        public ParserAttachNegativeImpl(IParser<TChar, TResult> capturing, IParser<TChar, TAssert> lookaheadAssertion) {
            this.lookaheadAssertion = lookaheadAssertion;
            this.capturing = capturing;
        }

        public IEnumerator<(TResult Result, int Position)> Parse<TString>(TString input, int position) where TString : IReadOnlyList<TChar> {
            var source = capturing;
            var collection = lookaheadAssertion;
            var source_enumerator = source.Parse(input, position);
            for (; source_enumerator.MoveNext();) {
                var source_current = source_enumerator.Current;
                var collection_enumerator = collection.Parse(input, source_current.Position);
                if (!collection_enumerator.Any()) {
                    yield return source_current;
                }
                collection_enumerator.Dispose();
            }
            source_enumerator.Dispose();
        }
    }
}
