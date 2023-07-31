namespace Graphs
{
    public class PriorityQueue<T>
    {
        private readonly List<T> _data;
        private readonly Comparison<T> _compare;

        public PriorityQueue()
            : this(Comparer<T>.Default)
        {
        }

        public PriorityQueue(IComparer<T> comparer)
            : this(comparer.Compare)
        {
        }

        public PriorityQueue(Comparison<T> comparison)
        {
            _data = new List<T>();
            _compare = comparison;
        }

        public void Enqueue(T item)
        {
            _data.Add(item);
            var childIndex = _data.Count - 1;

            while (childIndex > 0)
            {
                var parentIndex = (childIndex - 1) / 2;
                if (_compare(_data[childIndex],
                             _data[parentIndex])
                    >= 0)
                    break;

                T tmp = _data[childIndex];
                _data[childIndex] = _data[parentIndex];
                _data[parentIndex] = tmp;

                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            // assumes pq is not empty; up to calling code
            var lastElement = _data.Count - 1;

            var frontItem = _data[0];
            _data[0] = _data[lastElement];
            _data.RemoveAt(lastElement);

            --lastElement;

            var parentIndex = 0;
            while (true)
            {
                var childIndex = parentIndex * 2 + 1;
                if (childIndex > lastElement)
                    break; // End of tree

                var rightChild = childIndex + 1;
                if (rightChild <= lastElement
                    && _compare(_data[rightChild],
                                _data[childIndex])
                           < 0)
                    childIndex = rightChild;

                if (_compare(_data[parentIndex],
                             _data[childIndex])
                    <= 0)
                    break; // Correct position

                T tmp = _data[parentIndex];
                _data[parentIndex] = _data[childIndex];
                _data[childIndex] = tmp;

                parentIndex = childIndex;
            }

            return frontItem;
        }

        public T Peek()
        {
            T frontItem = _data[0];
            return frontItem;
        }

        public int Count
        {
            get { return _data.Count; }
        }
    }

}
