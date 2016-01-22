using System.Collections.Generic;

namespace CodeWars.CanYouGetTheLoop
{
    public class Kata
    {
        public static long getLoopSize(LoopDetector.Node initialNode)
        {
            //Been a while since I wrote imperative code this ugly...
            ISet<LoopDetector.Node> visitedNotes = new HashSet<LoopDetector.Node>();
            var currentNode = initialNode;
            while (!visitedNotes.Contains(currentNode))
            {
                visitedNotes.Add(currentNode);
                currentNode = currentNode.next;
            }

            var endOfCycle = currentNode;
            currentNode = currentNode.next;
            var loopLength = 1;
            while (endOfCycle != currentNode)
            {
                loopLength++;
                currentNode = currentNode.next;
            }

            return loopLength;
        }
    }
}