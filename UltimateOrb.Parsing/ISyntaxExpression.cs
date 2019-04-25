using System.Collections.Generic;

namespace UltimateOrb.Parsing {

    public static partial class SyntaxExtensions {

        public static IReadOnlyCollection<ISyntaxExpression<TChar>> GetSubexpressions<TChar>(this ISyntaxExpression<TChar> expression) {
            return expression.GetSubexpressions() as IReadOnlyCollection<ISyntaxExpression<TChar>>;
        }

        public static IReadOnlyCollection<ISyntax<TChar>> GetNextSyntaxes<TChar>(this ISyntaxExpression<TChar> expression) {
            return expression.GetNextSyntaxes() as IReadOnlyCollection<ISyntax<TChar>>;
        }

        public static IReadOnlyCollection<ITermialSyntaxExpression<TChar>> GetNextTerminalSyntaxes<TChar>(this ISyntaxExpression<TChar> expression) {
            return expression.GetNextTerminalSyntaxes() as IReadOnlyCollection<ITermialSyntaxExpression<TChar>>;
        }

        public static IReadOnlyCollection<ISyntax<TChar>> GetReferencedSyntaxes<TChar>(this ISyntaxExpression<TChar> expression) {
            return expression.GetReferencedSyntaxes() as IReadOnlyCollection<ISyntax<TChar>>;
        }

        public static IReadOnlyCollection<ITermialSyntaxExpression<TChar>> GetReferencedTerminalSyntaxes<TChar>(this ISyntaxExpression<TChar> expression) {
            return expression.GetReferencedTerminalSyntaxes() as IReadOnlyCollection<ITermialSyntaxExpression<TChar>>;
        }
    }


    public interface ISyntaxExpression {

        IReadOnlyCollection<ISyntaxExpression> GetSubexpressions();

        IReadOnlyCollection<ISyntax> GetNextSyntaxes();

        IReadOnlyCollection<ITermialSyntaxExpression> GetNextTerminalSyntaxes();

        IReadOnlyCollection<ISyntax> GetReferencedSyntaxes();

        IReadOnlyCollection<ITermialSyntaxExpression> GetReferencedTerminalSyntaxes();
    }

    public interface ISyntaxExpression<in TChar>
        : ISyntaxExpression {
    }

    public interface ISyntaxExpression<in TChar, out TResult>
        : ISyntaxExpression<TChar> {
    }
}
