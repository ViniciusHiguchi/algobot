using System.Collections;
using UnityEngine.TestTools;

namespace ComputationalPuzzle.Tests
{
    public class SamplePlayModeTests
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator SamplePlayModeTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
