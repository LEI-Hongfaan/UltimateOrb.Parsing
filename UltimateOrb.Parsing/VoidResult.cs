using System;

namespace UltimateOrb {

    [Serializable]
    public readonly partial struct VoidResult
        : IEquatable<VoidResult>
        , IComparable<VoidResult> {

        public int CompareTo(VoidResult other) {
            return 0;
        }

        public override bool Equals(object obj) {
            return obj is VoidResult;
        }

        public bool Equals(VoidResult other) {
            return true;
        }

        public override int GetHashCode() {
            return 0x5d67b732;
        }

        public override string ToString() {
            return "()";
        }
    }
}
