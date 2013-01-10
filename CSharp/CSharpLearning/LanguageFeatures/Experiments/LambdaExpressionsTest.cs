using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LanguageFeatures.Experiments
{
    class Obj
    {
        public Obj(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    [TestFixture]
    class LambdaExpressionsTest
    {
        [Test]
        public void BasicLambdaExpression()
        {
            ICollection<Obj> objects = new List<Obj> {new Obj(1), new Obj(2)};
            const int treshold = 2;
            var filtered = objects.Where(x=>x.Id < treshold);

            Assert.AreEqual(1, filtered.Count());
            Assert.AreEqual(1, filtered.First().Id);
        }
    }
}
