﻿using NMock;
using NUnit.Framework;

namespace Test.A_Templates
{
    [TestFixture]
    internal class Template
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            context = new MockFactory();
        }

        [TearDown]
        public void TearDown()
        {
            context.VerifyAllExpectationsHaveBeenMet();
        }

        #endregion

        private MockFactory context;

        [Test]
        public void TestName()
        {
            Assert.IsTrue(true);
        }
    }
}