﻿namespace UltimateOrb.Parsing.Generic {

    public interface IReversibleParser<TChar, TResult>
        : IParser<TChar, TResult> {

        IReversibleParser<TChar, TResult> Reversed();
    }
}
