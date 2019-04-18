using System.Diagnostics.Contracts;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace UltimateOrb.Parsing {

    public static partial class Extensions {

        [TargetedPatchingOptOutAttribute(null)]
        [ReliabilityContractAttribute(Consistency.WillNotCorruptState, Cer.Success)]
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        [PureAttribute()]
        public static Wrapper<T> ToWrapper<T>(this T value) {
            return new Wrapper<T>(value);
        }
    }

    public readonly partial struct Wrapper<T> {

        public readonly T Value;

        [TargetedPatchingOptOutAttribute(null)]
        [ReliabilityContractAttribute(Consistency.WillNotCorruptState, Cer.Success)]
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        [PureAttribute()]
        public Wrapper(T value) {
            this.Value = value;
        }

        [TargetedPatchingOptOutAttribute(null)]
        [ReliabilityContractAttribute(Consistency.WillNotCorruptState, Cer.Success)]
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        [PureAttribute()]
        public static implicit operator T(Wrapper<T> value) {
            return value.Value;
        }

        [TargetedPatchingOptOutAttribute(null)]
        [ReliabilityContractAttribute(Consistency.WillNotCorruptState, Cer.Success)]
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        [PureAttribute()]
        public static implicit operator Wrapper<T>(T value) {
            return new Wrapper<T>(value);
        }

        public override string ToString() {
            var value = this.Value;
            if (null != value) {
                return value.ToString();
            }
            return @"";
        }
    }
}
