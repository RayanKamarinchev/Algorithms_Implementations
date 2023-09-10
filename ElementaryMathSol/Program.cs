public class ElementaryMath
{
    public static List<List<int>> AdjacencyList;
    public static int[] Positions;

    public static void Main(string[] args)
    {
        int numPairs = int.Parse(Console.ReadLine());

        Dictionary<long, int> uniqueAnswers = new Dictionary<long, int>();
        Dictionary<int, long> calculatedValues = new Dictionary<int, long>();

        Pair[] pairs = new Pair[numPairs];
        Positions = new int[numPairs * 3];
        Array.Fill(Positions, -1);

        AdjacencyList = new List<List<int>>();
        for (int i = 0; i < numPairs; i++)
        {
            AdjacencyList.Add(new List<int>(3));
        }

        for (int i = 0; i < numPairs; i++)
        {
            string[] data = Console.ReadLine().Split();
            long a = long.Parse(data[0]);
            long b = long.Parse(data[1]);
            pairs[i] = new Pair(a, b);
            AdjacencyList[i].Add(AddValue(a * b, uniqueAnswers, calculatedValues));
            AdjacencyList[i].Add(AddValue(a + b, uniqueAnswers, calculatedValues));
            AdjacencyList[i].Add(AddValue(a - b, uniqueAnswers, calculatedValues));
        }

        int numConnected = 0;
        for (int i = 0; i < numPairs; i++)
        {
            bool[] visited = new bool[numPairs * 3];
            numConnected = ConnectValue(visited, i) ? numConnected + 1 : 0;
        }

        if (numConnected < numPairs)
        {
            Console.WriteLine("impossible");
            return;
        }

        long[] answers = new long[numPairs];
        for (int i = 0; i < uniqueAnswers.Count; i++)
        {
            if (Positions[i] != -1)
            {
                answers[Positions[i]] = calculatedValues[i];
            }
        }

        for (int i = 0; i < numPairs; i++)
        {
            long a = pairs[i].A;
            long b = pairs[i].B;
            string op = " * ";

            if (a + b == answers[i])
            {
                op = " + ";
            }
            else if (a - b == answers[i])
            {
                op = " - ";
            }
            Console.WriteLine($"{a}{op}{b} = {answers[i]}");
        }
    }

    public static int AddValue(long value, Dictionary<long, int> uniqueAnswers, Dictionary<int, long> calculatedValues)
    {
        if (!uniqueAnswers.ContainsKey(value))
        {
            uniqueAnswers[value] = uniqueAnswers.Count;
            calculatedValues[uniqueAnswers[value]] = value;
        }
        return uniqueAnswers[value];
    }

    public static bool ConnectValue(bool[] visited, int current)
    {
        if (current == -1)
        {
            return true;
        }

        foreach (int position in AdjacencyList[current])
        {
            if (!visited[position])
            {
                visited[position] = true;
                if (ConnectValue(visited, Positions[position]))
                {
                    Positions[position] = current;
                    return true;
                }
            }
        }
        return false;
    }

    public class Pair
    {
        public long A;
        public long B;

        public Pair(long a, long b)
        {
            A = a;
            B = b;
        }
    }
}