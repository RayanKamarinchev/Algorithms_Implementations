namespace Softuniada
{
    using System;
    using System.Linq;

    class Program
    {
        //30min - kinda failed
        static void Main(string[] args)
        {
            int changesCount = int.Parse(Console.ReadLine());
            int[] changes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int startSpeed = int.Parse(Console.ReadLine());
            int maxSpeed = int.Parse(Console.ReadLine());

            bool[,] reachableSpeeds = new bool[changesCount + 1, maxSpeed + 1];
            reachableSpeeds[0, startSpeed] = true;

            for (int i = 0; i < changesCount; i++)
            {
                for (int j = 0; j <= maxSpeed; j++)
                {
                    if (reachableSpeeds[i, j])
                    {
                        var subtractedIndex = j - changes[i];
                        var addedIndex = j + changes[i];
                        var isSubtractedIndexInMatrix = subtractedIndex >= 0;
                        var isAddedIndexInMatrix = addedIndex <= maxSpeed;
                        var itemRow = i + 1;

                        if (isSubtractedIndexInMatrix)
                        {
                            reachableSpeeds[itemRow, subtractedIndex] = true;
                        }

                        if (isAddedIndexInMatrix)
                        {
                            reachableSpeeds[itemRow, addedIndex] = true;
                        }
                    }
                }
            }

            var found = false;

            for (int i = maxSpeed; i >= 0; i--)
            {
                if (reachableSpeeds[changesCount, i])
                {
                    Console.WriteLine(i);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine(-1);
            }
        }
    }
}