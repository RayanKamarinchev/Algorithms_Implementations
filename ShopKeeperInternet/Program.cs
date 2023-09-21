using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopKeeperInternet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //11:42
            static int pageFaults(int[] pages,
                   int n, int capacity)
            {
                // To represent set of current pages. 
                // We use an unordered_set so that 
                // we quickly check if a page is 
                // present in set or not
                HashSet<int> s = new HashSet<int>(capacity);

                // To store least recently used indexes
                // of pages.
                Dictionary<int,
                           int> indexes = new Dictionary<int,
                                                         int>();

                // Start from initial page
                int page_faults = 0;
                for (int i = 0; i < n; i++)
                {
                    // Check if the set can hold more pages
                    if (s.Count < capacity)
                    {
                        // Insert it into set if not present
                        // already which represents page fault
                        if (!s.Contains(pages[i]))
                        {
                            s.Add(pages[i]);

                            // increment page fault
                            page_faults++;
                        }

                        // Store the recently used index of
                        // each page
                        if (indexes.ContainsKey(pages[i]))
                            indexes[pages[i]] = i;
                        else
                            indexes.Add(pages[i], i);
                    }

                    // If the set is full then need to 
                    // perform lru i.e. remove the least 
                    // recently used page and insert
                    // the current page
                    else
                    {
                        // Check if current page is not 
                        // already present in the set
                        if (!s.Contains(pages[i]))
                        {
                            // Find the least recently used pages
                            // that is present in the set
                            int lru = int.MaxValue, val = int.MinValue;

                            foreach (int itr in s)
                            {
                                int temp = itr;
                                if (indexes[temp] < lru)
                                {
                                    lru = indexes[temp];
                                    val = temp;
                                }
                            }

                            // Remove the indexes page
                            s.Remove(val);

                            //remove lru from hashmap
                            indexes.Remove(val);

                            // insert the current page
                            s.Add(pages[i]);

                            // Increment page faults
                            page_faults++;
                        }

                        // Update the current page index
                        if (indexes.ContainsKey(pages[i]))
                            indexes[pages[i]] = i;
                        else
                            indexes.Add(pages[i], i);
                    }
                }
                return page_faults;
            }
            int[] storage = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int[] list = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int capacity = storage.Length;
            var lol = storage.ToList();
            lol.AddRange(list);
            Console.WriteLine(pageFaults(lol.ToArray(),
                                         lol.Count, capacity)-capacity);
        }
    }
}
