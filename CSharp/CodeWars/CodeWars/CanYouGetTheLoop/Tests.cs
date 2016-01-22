using NUnit.Framework;

namespace CodeWars.CanYouGetTheLoop
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void FourNodesWithLoopSize3()
        {
            var n1 = new LoopDetector.Node();
            var n2 = new LoopDetector.Node();
            var n3 = new LoopDetector.Node();
            var n4 = new LoopDetector.Node();
            n1.next = n2;
            n2.next = n3;
            n3.next = n4;
            n4.next = n2;
            Assert.AreEqual(3, Kata.getLoopSize(n1));
        }

        [Test]
        public void RandomChainNodesWithLoopSize30()
        {
            var n1 = LoopDetector.createChain(3, 30);
            Assert.AreEqual(30, Kata.getLoopSize(n1));
        }

        [Test]
        public void RandomLongChainNodesWithLoopSize1087()
        {
            var n1 = LoopDetector.createChain(3904, 1087);
            Assert.AreEqual(1087, Kata.getLoopSize(n1));
        }
    }

    public class LoopDetector
    {
        public class Node
        {
            public Node next;
        }

        public static Node createChain(int tailNodes, int nodesInTheLoop)
        {
            var startNode = new Node();
            var currentNode = startNode;
            while (tailNodes != 0)
            {
                tailNodes--;
                currentNode.next = new Node();
                currentNode = currentNode.next;
            }

            var startOfCycle = currentNode;
            while (nodesInTheLoop != 1)
            {
                nodesInTheLoop--;
                currentNode.next = new Node();
                currentNode = currentNode.next;
            }
            currentNode.next = startOfCycle;

            return startNode;
        }
    }
}