namespace ProceduralToolkit.Services.Generators.FSM
{
    public class CircularBufferQueue<T>
    {
        private readonly T[] buffer;
        private int start;
        private int end;
        private bool emptyFlag = true;

        public CircularBufferQueue(int capacity)
        {
            buffer = new T[capacity];
        }

        private bool IsEmpty => (start == end) && (emptyFlag);
        private bool IsFull => (start == end) && (!emptyFlag);

        public void Enqueue(T value)
        {
            CheckFull();
            InsertValue(value);
        }

        private void CheckFull()
        {
            if (IsFull)
            {
                throw new QueueOverflowException();
            }
        }

        private void InsertValue(T value)
        {
            buffer[start] = value;
            IncrementCircularIndex(ref start);
            emptyFlag = false;
        }

        public T Dequeue()
        {
            CheckEmpty();
            return GetCurrentValue();
        }

        private void CheckEmpty()
        {
            if (IsEmpty)
            {
                throw new QueueUnderflowException();
            }
        }

        private T GetCurrentValue()
        {
            var result = buffer[end];
            IncrementCircularIndex(ref end);
            TryResetEmptyFlag();
            return result;
        }

        private void TryResetEmptyFlag()
        {
            if (start == end)
            {
                emptyFlag = true;
            }
        }

        private void IncrementCircularIndex(ref int index)
        {
            index = (index + 1) % buffer.Length;
        }
    }
}
