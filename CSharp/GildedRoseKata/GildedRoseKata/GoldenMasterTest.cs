using System;
using System.IO;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace GildedRoseKata
{
    [TestFixture]
    [UseReporter(typeof (NUnitReporter))]
    public class GoldenMasterTest
    {
        [Test]
        public void ThirtyDays()
        {
            var fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));

            Program.Main(new string[] {});
            String output = fakeoutput.ToString();
            Approvals.Verify(output);
        }
    }
}