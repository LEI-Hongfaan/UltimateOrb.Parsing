using System;
using Xunit;
using FsCheck.Xunit;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UltimateOrb.Parsing.Text;
using static UltimateOrb.Parsing.Combinators;

namespace UltimateOrb.Parsing.Tests {
    using static UltimateOrb.Parsing.Combinators.Invariant;

    public class UnitTest1 {

        [Fact]
        public void Test1() {
            
        }

        [Property(MaxTest = 1000)]
        public bool TestChar0001(char expected, long result) {
            var parser = new SingleCharConstParser<long>(expected, result);
            var result_1 = parser.Parse(new string(expected, 1)).SingleResult();
            return result == result_1;
        }
    }
}
