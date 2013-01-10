using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LanguageFeatures.Experiments
{
    [TestFixture]
    class LINQTest
    {
        private class Obj
        {
            public Obj(int number)
            {
                this.Number = number;
            }

            public int Number { get; set; }
            public int OtherNumber { get; set; }
        }

        private ICollection<Obj> getCollection()
        {
            return new List<Obj>
                       {new Obj(1) {OtherNumber = 1}, new Obj(2) {OtherNumber = 1}, new Obj(3) {OtherNumber = 2}};
        }

        [Test]
        public void LINQToObjectsIsCool()
        {
            var objects = getCollection();
            var filteredObjects = from obj in objects
                                  where obj.Number == 1
                                  select obj;

            Assert.AreEqual(1, filteredObjects.Count());
            Assert.AreEqual(1, filteredObjects.First().Number);

        }

        [Test]
        public void LINQTakeIsEquivalentToSQLTop()
        {
            var result = (from obj in getCollection() 
                          orderby obj.Number
                          select obj)
                          .Take(1); //Take returns the top x records

            Assert.AreEqual(1,result.Count());
            Assert.AreEqual(1, result.First().Number);

        }

        [Test]
        public void LINQSkipSupportsVeryEasyPaging()
        {
            var result = getCollection().OrderBy(x=>x.Number).Skip(1).Take(1); //skip skips the top x records
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(2, result.First().Number);
        }

        [Test]
        public void ProjectionsUsingAnonymousTypes()
        {
            ICollection<Obj> objects = getCollection();
            //the new {} is an anonymous type => you can leave out/add columns, but you lose typing.
            var x = objects.Select(o => new {o.Number});
            Assert.AreEqual(1, x.First().Number);
        }

        [Test]
        public void DynamicallyAddFiltersToAQuery()
        {
            var list = getCollection().AsQueryable();

            //Alter Where Condition
            list = list.Where(obj => obj.Number == 2);

            var filtered = list.ToList(); //LINQ query gets executed
            Assert.AreEqual(1, filtered.Count);
            Assert.AreEqual(2, filtered.First().Number);

        }

        [Test]
        public void FilterOnMultipleValuesUsingBooleanLogic()
        {
            var filtered = from obj in getCollection()
                           where obj.Number == 1 || obj.Number == 2
                           //you can use basic boolean logic if the filters are known
                           select obj;

            Assert.AreEqual(2, filtered.Count());
            Assert.IsTrue(filtered.Count(x => x.Number == 1 || x.Number == 2) == 2);
        }

        [Test]
        public void FilterUsingACollectionOfValues()
        {
            ICollection<int> filterValues = new List<int> {1, 3};

            var filtered = from obj in getCollection()
                           where filterValues.Contains(obj.Number)
                           //you can use collections if things get large
                           select obj;

            Assert.AreEqual(2, filtered.Count());
            Assert.IsTrue(filtered.Count(x => x.Number == 1 || x.Number == 3) == 2);
        }

        private ICollection<A> getNavigationObject()
        {
            return new List<A> {new A()};
        }

        [Test]
        public void TraverseUnaryAssociation()
        {
            var result = from a in getNavigationObject()
                         where a.Unary.Id == 1 //easy peazy object graph navigation
                         select a;

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void TraverseNaryAssociation()
        {
            var result = from a in getNavigationObject()
                         where a.Nary.All(x => x.Id == 1 || x.Id == 2)
                         select a;
            Assert.AreEqual(1, result.Count(), "Should have filtered the A out correctly");
        }
    }

    internal class A
    {
        public B Unary
        {
            get { return new B {Id = 1}; }
        }

        public ICollection<B> Nary
        {
            get { return new List<B> {new B {Id = 1}, new B {Id = 2}}; }
        }


    }

    internal class B
    {
        public int Id { get; set; }
    }
}

