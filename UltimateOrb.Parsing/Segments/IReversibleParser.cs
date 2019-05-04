namespace UltimateOrb.Parsing.Segments {

    public interface IReversibleParser<TChar>
        : Generic.IReversibleParser<TChar, (int Start, int Length)>
        , IParser<TChar> {

        new IReversibleParser<TChar> Reversed();
    }
}
