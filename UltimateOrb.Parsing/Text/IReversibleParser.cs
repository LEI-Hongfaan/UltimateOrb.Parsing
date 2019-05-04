namespace UltimateOrb.Parsing.Text {

    public interface IReversibleParser<TResult>
        : Generic.IReversibleParser<char, TResult>
        , IParser<TResult> {

        new IReversibleParser<TResult> Reversed();
    }
}
