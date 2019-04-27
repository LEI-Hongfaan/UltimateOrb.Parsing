namespace UltimateOrb.Parsing {

    public interface IParseResult {

        TResult GetResult<TResult>();

        int Position {
            get;
        }
    }

    public interface IParseResult<out TResult>
        : IParseResult {

        TResult Result {

            get;
        }
    }
}
