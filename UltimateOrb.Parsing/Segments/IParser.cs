using System.Text;
using UltimateOrb.Parsing.Generic;

namespace UltimateOrb.Parsing.Segments {

    public partial interface IParser<TChar> : IParser<TChar, (int Start, int Length)> {
    }
}
