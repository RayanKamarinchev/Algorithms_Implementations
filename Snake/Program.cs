using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Snake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //9:25 25min
            int n = int.Parse(Console.ReadLine());
            int x = 0, y = 0, z = 0;
            char[,,] cube = new char[n, n, n];
            for (int i = 0; i < n; i++)
            {
                var rows = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.None).Select(a => a.ToCharArray()).ToArray();
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (rows[j][k] == 's')
                        {
                            x = j;
                            y = i;
                            z = k;
                        }
                        cube[j, i, k] = rows[j][k];
                    }
                }
            }
            string direction = Console.ReadLine();
            string input ="";
            int steps = 0;
            int score = 0;
            Regex re = new Regex("(\\w*) in (\\d*) step");
            try
            {
                while (direction != "end")
                {
                    input = Console.ReadLine();
                    steps = int.Parse(re.Match(input).Groups[2].Value);
                    switch (direction)
                    {
                        case "forward":
                            for (int i = 0; i < steps; i++)
                            {
                                y--;
                                if (cube[x, y, z] == 'a')
                                {
                                    cube[x, y, z] = 'o';
                                    score++;
                                }
                            }
                            break;
                        case "backward":
                            for (int i = 0; i < steps; i++)
                            {
                                y++;
                                if (cube[x, y, z] == 'a')
                                {
                                    cube[x, y, z] = 'o';
                                    score++;
                                }
                            }
                            break;
                        case "down":
                            for (int i = 0; i < steps; i++)
                            {
                                x++;
                                if (cube[x, y, z] == 'a')
                                {
                                    cube[x, y, z] = 'o';
                                    score++;
                                }
                            }
                            break;
                        case "up":
                            for (int i = 0; i < steps; i++)
                            {
                                x--;
                                if (cube[x, y, z] == 'a')
                                {
                                    cube[x, y, z] = 'o';
                                    score++;
                                }
                            }
                            break;
                        case "left":
                            for (int i = 0; i < steps; i++)
                            {
                                z--;
                                if (cube[x, y, z] == 'a')
                                {
                                    cube[x, y, z] = 'o';
                                    score++;
                                }
                            }
                            break;
                        case "right":
                            for (int i = 0; i < steps; i++)
                            {
                                z++;
                                if (cube[x, y, z] == 'a')
                                {
                                    cube[x, y, z] = 'o';
                                    score++;
                                }
                            }
                            break;
                    }
                    direction = re.Match(input).Groups[1].Value;
                }
                Console.WriteLine("Points collected: " + score);
            }
            catch (Exception)
            {
                while (!input.Contains("end"))
                {
                    input = Console.ReadLine();
                }
                Console.WriteLine("Points collected: " + score);
                Console.WriteLine("The snake dies.");
            }
        }
    }
}
