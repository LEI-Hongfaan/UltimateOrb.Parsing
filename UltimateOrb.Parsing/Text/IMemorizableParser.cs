namespace UltimateOrb.Parsing.Text {

    public partial interface IMemorizableParser<TResult>
        : Generic.IMemorizableParser<char, TResult>
        , IParser<TResult> {
    }
}
