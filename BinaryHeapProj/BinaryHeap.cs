using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapProj
{
    public class BinaryHeap<T> where T : IComparable<T>
    {
        private List<T> heapList;
        private int size;
        public BinaryHeap()
        {
            heapList = new List<T>();
        }

        private void Swap(T item1, T item2)
        {
            (item1, item2) = (item2, item1);
        }

        private int GetParent(int i)
        {
            return (i - 1) / 2;
        }

        private int GetLeft(int i)
        {
            return i*2 + 1;
        }

        private int GetRight(int i)
        {
            return i * 2 + 2; 
        }

        public void Add(T item)
        {
            heapList.Add(item);
            size++;

            int i = size - 1;
            
            while (i !=0 && heapList[i].CompareTo(heapList[GetParent(i)]) > 0)
            {
                Swap(heapList[i], heapList[GetParent(i)]);
                i = GetParent(i);
            }
        }

        private int IndexOf(T item)
        {
            int i = 0;
            T currentItem = heapList[i];
            while (!currentItem.Equals(item))
            {
                if (item.CompareTo(currentItem) < 0)
                {
                    i = GetLeft(i);
                }
                else
                {
                    i = GetRight(i);
                }

                if (i >= size)
                {
                    return -1;
                }
                currentItem = heapList[i];
            }

            return i;
        }

        public void Remove(T item)
        {
            int i = IndexOf(item);
            if (i == -1)
            {
                return;
            }
            Swap(heapList[i], heapList[size-1]);

            while (i != 0 && heapList[i].CompareTo(heapList[GetParent(i)]) < 0)
            {
                Swap(heapList[i], heapList[GetParent(i)]);
                i = GetParent(i);
            }
        }
    }
}
