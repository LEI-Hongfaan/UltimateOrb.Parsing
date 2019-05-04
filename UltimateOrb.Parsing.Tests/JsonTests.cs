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
using System.IO;

namespace UltimateOrb.Parsing.Tests {
    using static UltimateOrb.Parsing.Combinators.Invariant;

    public static partial class Json {

        const string dir = "./../../../JSON Samples";

        [Fact]
        public static void TestPass2A() {
            var jsonText = File.ReadAllText(Path.Combine(dir, "pass2.json"));
            var json_p = GerJsonParser(20);
            var v = json_p.Parse(jsonText).SingleResult();
            var a = Newtonsoft.Json.Linq.JToken.FromObject(v);
            var b = Newtonsoft.Json.Linq.JToken.Parse(jsonText);
            Assert.True(Newtonsoft.Json.Linq.JToken.DeepEquals(a, b));
        }

        [Fact]
        public static void TestPass2B() {
            var jsonText = File.ReadAllText(Path.Combine(dir, "pass2.json"));
            var json_p = GerJsonParser();
            var v = json_p.Parse(jsonText).SingleResult();
            var a = Newtonsoft.Json.Linq.JToken.FromObject(v);
            var b = Newtonsoft.Json.Linq.JToken.Parse(jsonText);
            Assert.True(Newtonsoft.Json.Linq.JToken.DeepEquals(a, b));
        }

        [Fact]
        public static void TestPass3A() {
            var jsonText = File.ReadAllText(Path.Combine(dir, "pass3.json"));
            var json_p = GerJsonParser(20);
            var v = json_p.Parse(jsonText).SingleResult();
            var a = Newtonsoft.Json.Linq.JToken.FromObject(v);
            var b = Newtonsoft.Json.Linq.JToken.Parse(jsonText);
            Assert.True(Newtonsoft.Json.Linq.JToken.DeepEquals(a, b));
        }

        [Fact]
        public static void TestPass3B() {
            var jsonText = File.ReadAllText(Path.Combine(dir, "pass3.json"));
            var json_p = GerJsonParser();
            var v = json_p.Parse(jsonText).SingleResult();
            var a = Newtonsoft.Json.Linq.JToken.FromObject(v);
            var b = Newtonsoft.Json.Linq.JToken.Parse(jsonText);
            Assert.True(Newtonsoft.Json.Linq.JToken.DeepEquals(a, b));
        }

        [Fact]
        public static void TestFailA() {
            for (var i = 2; i <= 33; ++i) {
                TestFailAStub(i);
            }
        }

        [Fact]
        public static void TestPassA() {
            for (var i = 1; i <= 3; ++i) {
                TestPassAStub(i);
            }
            {
                TestPassAStub(1001);
            }
        }

        [Fact]
        public static void TestFailB() {
            for (var i = 2; i <= 33; ++i) {
                if (18 == i) {
                    // Skipping "Too deep".
                    continue;
                }
                TestFailBStub(i);
            }
        }

        [Fact]
        public static void TestPassB() {
            for (var i = 1; i <= 3; ++i) {
                TestPassBStub(i);
            }
            {
                TestPassBStub(1001);
                TestPassBStub(3018);
            }
        }

        internal static void TestFailAStub(object key) {
            var jsonText = File.ReadAllText(Path.Combine(dir, $@"fail{key}.json"));
            var json_p = GerJsonParser(19);
            Assert.False(json_p.Parse(jsonText).Any());
        }

        internal static void TestPassAStub(object key) {
            var jsonText = File.ReadAllText(Path.Combine(dir, $@"pass{key}.json"));
            var json_p = GerJsonParser(19);
            json_p.Parse(jsonText).SingleResult()?.GetHashCode();
        }

        internal static void TestFailBStub(object key) {
            var jsonText = File.ReadAllText(Path.Combine(dir, $@"fail{key}.json"));
            var json_p = GerJsonParser();
            Assert.False(json_p.Parse(jsonText).Any());
        }

        internal static void TestPassBStub(object key) {
            var jsonText = File.ReadAllText(Path.Combine(dir, $@"pass{key}.json"));
            var json_p = GerJsonParser();
            json_p.Parse(jsonText).SingleResult()?.GetHashCode();
        }
    }

    public static partial class UnclassifiedTests {

        [Property(MaxTest = 1000)]
        public static bool TestChar0001(char expected, long result) {
            var parser = new SingleCharConstParser<long>(expected, result);
            var result_1 = parser.Parse(new string(expected, 1)).SingleResult();
            return result == result_1;
        }
    }
}
