namespace ElementaryMath
{
    public class Index
    {
        public readonly int A;
        public readonly int B;
        public readonly int? N;
        public readonly int index;
        public readonly IndexType type;

        public Index(int a, int b, int i)
        {
            A = a;
            B = b;
            index = i;
            type = IndexType.Numbers;
        }

        public Index()
        {
            index = 0;
            type = IndexType.Source;
        }
        public Index(int n, int i)
        {
            index = i;
            N = n;
            type = IndexType.Result;
        }
        public Index(int i)
        {
            index = i;
            type = IndexType.Sink;
        }
    }
}
