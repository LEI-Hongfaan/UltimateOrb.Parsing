using System;

namespace UltimateOrb.Parsing {

    internal static partial class ThrowHelper {

        public static InvalidCastException ThrowInvalidCastException() {
            throw new InvalidCastException();
        }

        public static InvalidOperationException ThrowInvalidOperationException() {
            throw new InvalidOperationException();
        }
    }
}
