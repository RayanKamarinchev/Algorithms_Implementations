using System;
using System.Linq;

namespace ColorEncodingSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //11:42 50min realno 25min
            int t = int.Parse(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                var firstSequence = Console.ReadLine().Split().Select(x =>
                {
                    if (x[0] == '(')
                    {
                        return (x.Substring(1, x.Length-2), false);
                    }
                    else
                    {
                        return (x, true);
                    }
                }).ToArray();
                var targetSequence = Console.ReadLine().Split();
                bool[,] matrix = new bool[firstSequence.Length + 1, targetSequence.Length + 1];
                matrix[0, 0] = true;

                for (int firstIndex = 0; firstIndex < firstSequence.Length; firstIndex++)
                {
                    for (int targetIndex = 0; targetIndex <= targetSequence.Length; targetIndex++)
                    {
                        if (!matrix[firstIndex, targetIndex])
                            continue;
                        if (!firstSequence[firstIndex].Item2)
                        {
                            matrix[firstIndex+1,  targetIndex] = true;
                        }

                        if (targetIndex < targetSequence.Length &&
                            firstSequence[firstIndex].Item1 == targetSequence[targetIndex])
                        {
                            matrix[firstIndex + 1, targetIndex + 1] = true;
                        }
                    }
                }

                Console.WriteLine(matrix[firstSequence.Length, targetSequence.Length] ? "true" : "false");
            }
        }
    }
}
