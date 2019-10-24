using System.Collections.Generic;
using System.Linq;
using FsCheck;
using Xunit;

namespace FsCheckDemo
{
    public class BasicDemo
    {
        [Fact]
        public void Verifying_A_Property()
        {
            Prop
                .ForAll(Arb.From<int[]>(), xs => xs.Reverse().Reverse().SequenceEqual(xs))
                .QuickCheckThrowOnFailure();
        }
    }
}