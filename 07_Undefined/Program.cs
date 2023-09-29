using System;
using System.Collections.Generic;
using System.Linq;

namespace _07_Undefined
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //11:38 55min
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, int[]> owners = new Dictionary<string, int[]>();
            Dictionary<string, List<(int, int)>> pairs = new Dictionary<string, List<(int, int)>>();
            for (int i = 0; i < n; i++)
            {
                var line = Console.ReadLine().Split(new string[]{" -> "}, StringSplitOptions.None);
                owners.Add(line[0], line[1].Split(new string[]{", "}, StringSplitOptions.None)
                                           .Select(int.Parse)
                                           .OrderBy(x=>x)
                                           .ToArray());
            }
            List<(string, int)> leftovers = new List<(string, int)> ();
            List<(string, int)[]> lastPairs = new List<(string, int)[]> ();
            int sum = 0;
            foreach (var item in owners)
            {
                pairs.Add(item.Key, new List<(int, int)>());
                for (int i = 0; i < item.Value.Length/2; i++)
                {
                    sum += item.Value[i] - item.Value[item.Value.Length - 1 - i];
                    pairs[item.Key].Add((item.Value[i], item.Value[item.Value.Length-1-i]));
                }

                if (item.Value.Length % 2 != 0)
                {
                    leftovers.Add((item.Key, item.Value[item.Value.Length/2]));
                }
            }
            leftovers = leftovers.OrderBy(x => x.Item2).ToList();
            for (int i = 0; i < leftovers.Count / 2; i++)
            {
                if (String.Compare(leftovers[i].Item1, leftovers[leftovers.Count - 1 - i].Item1, StringComparison.Ordinal) > 0)
                {
                    lastPairs.Add(new (string, int)[] { leftovers[leftovers.Count - 1 - i], leftovers[i] });
                }
                else
                {
                    lastPairs.Add(new (string, int)[] { leftovers[i], leftovers[leftovers.Count - 1 - i] });
                }
            }
            int minVal = int.MaxValue;
            string minOwner = "";
            if (leftovers.Count % 2 != 0)
            {
                if (leftovers.Count==1)
                {
                    foreach (var item in pairs)
                    {
                        if (item.Value.Count == 0)
                        {
                            continue;
                        }
                        var last = item.Value.Last();
                        if (last.Item2 - last.Item1 < minVal)
                        {
                            minVal = last.Item2 - last.Item1;
                            minOwner = item.Key;
                        }
                    }
                    sum += minVal;
                    var least = pairs[minOwner].Last();
                    lastPairs.Add(new (string, int)[] { leftovers[leftovers.Count / 2], (minOwner, least.Item2) });
                    lastPairs.Add(new (string, int)[] { leftovers[leftovers.Count / 2], (minOwner, least.Item1) });
                    pairs[minOwner].RemoveAt(pairs[minOwner].Count - 1);
                }
                else
                {
                    var lastest = lastPairs.Last();
                    lastPairs.RemoveAt(lastPairs.Count-1);
                    lastPairs.Add(new (string, int)[] {lastest[1], leftovers[leftovers.Count / 2] });
                    lastPairs.Add(new (string, int)[] { leftovers[leftovers.Count / 2], lastest[0]});
                }
            }

            foreach (var pair in pairs)
            {
                if (pair.Value.Count == 0)
                {
                    Console.WriteLine($"{pair.Key} | none");
                }
                else
                {
                    Console.WriteLine(
                        $"{pair.Key} | {string.Join(", ", pair.Value.Select(x => x.Item2 + " <-> " + x.Item1))}");
                }
            }

            lastPairs = lastPairs.OrderByDescending(x => x[0].Item2 + x[1].Item2).ToList();
            foreach (var pair in lastPairs)
            {
                Console.WriteLine(
                    $"{pair[0].Item1}{pair[0].Item2} <-> {pair[1].Item1}{pair[1].Item2}");
            }

            Console.WriteLine(-sum);
        }
    }
}
