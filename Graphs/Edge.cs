namespace Graphs
{
    public class Edge
    {
        public int Cost { get; set; }
        public int From { get; set; }
        public int To { get; set; }

        public Edge(int cost, int from, int to)
        {
            From = from;
            To = to;
            Cost = cost;
        }
    }
}
