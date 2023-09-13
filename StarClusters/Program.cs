using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StarClusters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //11:15 - 11:44
            Regex re = new Regex("(\\S*) \\((\\d*), (\\d*)\\)");
            Dictionary<string, (int, int, int)> clusters = new Dictionary<string, (int, int, int)>();
            Dictionary<string, (int, int)> clusterStars = new Dictionary<string, (int, int)>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var match = re.Match(Console.ReadLine());
                clusterStars.Add(match.Groups[1].Value, (int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)));
                clusters.Add(match.Groups[1].Value, (int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), 1));
            }
            Regex st = new Regex("\\((\\d*), (\\d*)\\)");
            string input = Console.ReadLine();
            while (input != "end")
            {
                foreach (Match match in st.Matches(input))
                {
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);

                    int minDist = Int32.MaxValue;
                    string chosenCluster = "";
                    foreach (var star in clusterStars)
                    {
                        int dist = (star.Value.Item1 - x) * (star.Value.Item1 - x) +
                            (star.Value.Item2 - y) * (star.Value.Item2 - y);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            chosenCluster = star.Key;
                        }
                    }
                    clusters[chosenCluster] = (clusters[chosenCluster].Item1+x, clusters[chosenCluster].Item2+y, clusters[chosenCluster].Item3+1);
                }
                input = Console.ReadLine();
            }

            var a = clusters.OrderBy(x => x.Key);
            foreach (var r in a)
            {
                double realX = Math.Round(r.Value.Item1 * 1.0 / r.Value.Item3, MidpointRounding.ToEven);
                double realY = Math.Round(r.Value.Item2 * 1.0 / r.Value.Item3, MidpointRounding.ToEven);
                Console.WriteLine($"{r.Key} ({realX}, {realY}) -> {r.Value.Item3} stars");
            }
        }
    }
}
