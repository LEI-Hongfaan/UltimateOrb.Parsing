namespace UltimateOrb.Parsing.Text {

    public interface IReversibleParser<T>
        : Generic.IReversibleParser<char, T>
        , IParser<T> {
    }
}
