using NUnit.Framework;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class CircularBufferQueueTests
    {
        private CircularBufferQueue<int> queue;
        private readonly int[] sampleInputs = new int[] { 1, 2, 3 };

        [SetUp]
        public void Setup()
        {
            queue = new CircularBufferQueue<int>(3);
        }

        private void EnqueueSampleInputs()
        {
            foreach (var input in sampleInputs)
            {
                queue.Enqueue(input);
            }
        }

        [Test]
        public void TestEnqueueOneDequeueOne()
        {
            var input = 2;
            queue.Enqueue(input);
            var result = queue.Dequeue();
            Assert.That(result, Is.EqualTo(input));
        }

        [Test]
        public void TestEnqueueSomeDequeueOne()
        {
            EnqueueSampleInputs();
            var result = queue.Dequeue();
            Assert.That(result, Is.EqualTo(sampleInputs[0]));
        }

        [Test]
        public void TestEnqueueSomeDequeueSome()
        {
            EnqueueSampleInputs();
            queue.Dequeue();
            queue.Dequeue();
            var result = queue.Dequeue();
            Assert.That(result, Is.EqualTo(sampleInputs[2]));
        }

        [Test]
        public void TestOverflow()
        {
            EnqueueSampleInputs();
            try
            {
                queue.Enqueue(4);
                Assert.Fail();
            }
            catch (QueueOverflowException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void TestUnderflow()
        {
            try
            {
                queue.Dequeue();
                Assert.Fail();
            }
            catch (QueueUnderflowException)
            {
                Assert.Pass();
            }
        }
    }
}
