using Moq;
using System;
using System.IO;
using System.Linq;
using System.Text;
using PointOfSale;
using Xunit;

namespace Test.PointOfSale
{
    public class DisplayPriceTest
    {
        private readonly TextWriter _productionOut = Console.Out;

        [Fact]
        public void DisplaysTheFormattedPriceCorrectly()
        {
            var output = HijackOut();
            var price = new Mock<Price>();
            price
                .Setup(p => p.Formatted())
                .Returns("formatted price");

            new ConsoleDisplay().DisplayPrice(price.Object);

            Assert.Equal("formatted price", output.ToString().Lines().Single());

            RestoreOut();
        }

        private static StringBuilder HijackOut()
        {
            var output = new StringBuilder();
            Console.SetOut(new StringWriter(output));

            return output;
        }

        private void RestoreOut()
        {
            Console.SetOut(_productionOut);
        }
    }
}
