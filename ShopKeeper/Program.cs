using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ShopKeeper
{
    internal class Program
    {
        static Pair[] pr = new Pair[100000];

        // Pointer to the last index
        static int size = -1;
        // Function to insert a new element
        // into priority queue
        static void enqueue(int value, int priority)
        {
            // Increase the size
            size++;

            // Insert the element
            pr[size] = new Pair(value, priority);
        }

        // Function to check the top element
        static int peek()
        {
            int highestPriority = int.MinValue;
            int ind = -1;

            // Check for the element with
            // highest priority
            for (int i = 0; i <= size; i++)
            {

                // If priority is same choose
                // the element with the
                // highest value
                if (highestPriority == pr[i].nextIndex && ind > -1
                                                      && pr[ind].Value < pr[i].Value)
                {
                    highestPriority = pr[i].nextIndex;
                    ind = i;
                }
                else if (highestPriority < pr[i].nextIndex)
                {
                    highestPriority = pr[i].nextIndex;
                    ind = i;
                }
            }

            // Return position of the element
            return ind;
        }

        // Function to remove the element with
        // the highest priority
        static void dequeue()
        {
            // Find the position of the element
            // with highest priority
            int ind = peek();

            // Shift the element one index before
            // from the position of the element
            // with highest priority is found
            for (int i = ind; i < size; i++)
            {
                pr[i] = pr[i + 1];
            }

            // Decrease the size of the
            // priority queue by one
            size--;
        }

        static void Main(string[] args)
        {
            //11:00 50min blunder 25min

            var storage = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            var list = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int score = 0;
            int n = storage.Count;
            if (!storage.Contains(list[0]))
            {
                Console.WriteLine("impossible");
                return;
            }
            //8:42
            foreach (var item in storage)
            {
                int nextInd = 14000;
                for (int j = 0; j < list.Length; j++)
                {
                    if (list[j] == item)
                    {
                        nextInd = j;
                        break;
                    }
                }
                enqueue(item, nextInd);
            }
            for (int i = 0; i < list.Length; i++)
            {
                int nextInd = -1;
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[j] == list[i])
                    {
                        nextInd = j;
                        break;
                    }
                }

                bool can = false;
                for (int j = 0; j <= size; j++)
                {
                    if (pr[j].Value == list[i])
                    {
                        can = true;
                        break;
                    }
                }
                if (!can)
                {
                    dequeue();
                    enqueue(list[i], nextInd);
                    score++;
                }
                else
                {
                    pr.First(x => x.Value == list[i]).nextIndex = nextInd;
                }

                for (int j = 0; j <= size; j++)
                {
                    pr[j].nextIndex--;
                }
            }
            Console.WriteLine(score);
        }

        class Pair
        {
            public int Value { get; set; }
            public int nextIndex { get; set; }

            public Pair(int value, int nextIndex)
            {
                Value = value;
                this.nextIndex = nextIndex;
            }
        }
    }
}
