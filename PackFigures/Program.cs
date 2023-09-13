using System;
using System.Collections.Generic;
using System.Linq;

namespace PackFigures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //9:38 - 10:20 10:25 - 10:42 10::44 - 11:30
            var input = Console.ReadLine().Split(' ').ToArray();
            List<IFigure> figs = new List<IFigure>();
            while (input[0] != "End")
            {
                IFigure fig;
                if (input[0] == "rectangle")
                {
                    fig = new Rect(input[1], int.Parse(input[2]), int.Parse(input[3]), int.Parse(input[4]),
                                   int.Parse(input[5]));
                }else if (input[0] == "square")
                {
                    fig = new Rect(input[1], int.Parse(input[2]), int.Parse(input[3]), int.Parse(input[4]) + int.Parse(input[2]),
                                   int.Parse(input[3]) - int.Parse(input[4]));
                }
                else
                {
                    fig = new Circle(input[1], int.Parse(input[2]), int.Parse(input[3]), int.Parse(input[4]));
                }
                figs.Add(fig);
                input = Console.ReadLine().Split(' ').ToArray();
            }
            
            
            for (int i = 0; i < figs.Count; i++)
            {
                for (int j = 0; j < figs.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (figs[i].isParentToFig(figs[j]))
                    {
                        figs[i].children.Add(figs[j]);
                    }
                }
            }
            Dictionary<string, (List<string>, int)> visited = new Dictionary<string, (List<string>, int)> ();
            (List<string>, int) inDepth(IFigure fig)
            {
                if (visited.TryGetValue(fig.Name, out var depth))
                {
                    return depth;
                }
                List<string> matches = new List<string>(){fig.Name};
                int max = -1;
                foreach (var child in fig.children)
                {
                    (List<string> matchList, int n) = inDepth(child);
                    if (max == n)
                    {
                        matches.AddRange(matchList);
                    }
                    else if (n > max)
                    {
                        matches.Clear();
                        matches.AddRange(matchList);
                        max = n;
                    }
                }

                if (max == -1)
                {
                    max = 0;
                }
                else
                {
                    matches = matches.Select(x => fig.Name + " < " + x).ToList();
                }
                visited.Add(fig.Name, (matches, max+1));
                return (matches, max+1);
            }

            foreach (var figure in figs)
            {
                inDepth(figure);
            }
            var res = visited.Aggregate((i1, i2) =>
            {
                if (i1.Value.Item2 > i2.Value.Item2)
                {
                    return i1;
                }
                if (i1.Value.Item2 == i2.Value.Item2)
                {
                    List<string> a = i1.Value.Item1.Union(i2.Value.Item1).ToList();
                    if (a.Count <= 1)
                    {
                        return i1;
                    }
                    string max = a[0];
                    int maxi = 0;
                    for (int i = 1; i < a.Count; i++)
                    {
                        if (max.CompareTo(a[i]) > 0)
                        {
                            max = a[i];
                            maxi = i;
                        }
                    }

                    if (maxi < i1.Value.Item1.Count)
                    {
                        return new KeyValuePair<string, (List<string>, int)>(
                            i1.Key, (new List<string>() { max }, i1.Value.Item2));
                    }
                    else
                    {
                        return new KeyValuePair<string, (List<string>, int)>(
                            i2.Key, (new List<string>() { max }, i2.Value.Item2));
                    }
                }
                return i2;
            });
            Console.WriteLine(res.Value.Item1.Min());
        }
    }

    interface IFigure
    {
        bool isParentToFig(IFigure figure);
        string Name { get; set; }
        bool IsCircle { get; set; }
        List<IFigure> children { get; set; }
    }
    class Circle : IFigure
    {
        public readonly int x;
        public readonly int y;
        public readonly int r;

        public Circle(string name, int x, int y, int r)
        {
            Name = name;
            this.x = x; this.y = y; this.r = r;
        }
        public bool isParentToFig(IFigure figu)
        {
            if (figu.IsCircle)
            {
                Circle fig = (Circle)figu;
                var dist = Math.Sqrt((fig.x - x) * (fig.x - x) + (fig.y - y) * (fig.y - y));
                return dist <= r - fig.r;
            }
            else
            {
                Rect fig = (Rect)figu;
                decimal up = (decimal)(fig.top - y) * (decimal)(fig.top - y);
                decimal down = (decimal)(fig.bottom - y) * (decimal)(fig.bottom - y);
                decimal left2 = (decimal)(x - fig.left) * (decimal)(x - fig.left);
                decimal right2 = (decimal)(x - fig.right) * (decimal)(x - fig.right);
                decimal distMax = Math.Max(Math.Max(Math.Max(left2 + up, left2 + down), right2 + up), right2 + down);
                return distMax <= (r * r);
            }
        }

        public string Name { get; set; }
        public bool IsCircle { get; set; } = true;
        public List<IFigure> children { get; set; } = new List<IFigure>();
    }

    class Rect : IFigure
    {
        public readonly int left;
        public readonly int top;
        public readonly int right;
        public readonly int bottom;

        public Rect(string name, int left, int top, int right, int bottom)
        {
            Name = name;
            this.left = left; this.top = top; this.right = right; this.bottom = bottom;
        }

        public bool isParentToFig(IFigure figu)
        {
            if (figu.IsCircle)
            {
                Circle fig = (Circle)figu;
                return (fig.x - fig.r + 0.01m >= left) && (fig.x + fig.r - 0.01m <= right)
                                                       && (fig.y + fig.r - 0.01m <= top) && (fig.y - fig.r + 0.01m >= bottom);
            }
            else
            {
                Rect fig = (Rect)figu;
                return bottom <= fig.bottom && left <= fig.left && top >= fig.top && right >= fig.right;
            }
        }

        public string Name { get; set; }
        public bool IsCircle { get; set; } = false;
        public List<IFigure> children { get; set; } = new List<IFigure>();
    }
}
