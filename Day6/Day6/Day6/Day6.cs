using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    public class Day6
    {
        static string input = @"Time:        59     70     78     78
Distance:   430   1218   1213   1276";

        public static int day6SolvePart1()
        {
            int result = 1;
            List<string> splitInput = input.Split("\r\n").ToList();
            List<int> time = splitInput[0].Remove(0,5).Split(" ").Where(x => x != "").Select(x => int.Parse(x)).ToList();
            List<int> distance = splitInput[1].Remove(0, 9).Split(" ").Where(x => x != "").Select(x => int.Parse(x)).ToList();
            List<(int, int)> races = new List<(int, int)>();
            for (int i = 0; i < time.Count; i++)
            {
                races.Add((time[i], distance[i]));
            }

            foreach(var race in races)
            {
                int subresult = 0;
                for (int i = 1;i < race.Item1; i++) 
                {
                    var test = i * (race.Item1 - i);
                    if (i * (race.Item1 - i) > race.Item2)
                    {
                        subresult = race.Item1 - i  - i + 1;
                        break;
                    }
                             
                }
                result *= subresult;
            }

            return result;
        }

        public static long day6SolvePart2()
        {
            Stopwatch timer = Stopwatch.StartNew();
            long result = 1;
            List<string> splitInput = input.Split("\r\n").ToList();
            List<string> times = splitInput[0].Remove(0, 5).Split(" ").Where(x => x != "").ToList();
            List<string> distances = splitInput[1].Remove(0, 9).Split(" ").Where(x => x != "").ToList();
            string ztime = "";
            string zdistance = "";

            for (int i = 0; i < times.Count; i++)
            {
                ztime += times[i];
                zdistance += distances[i];
            }

            long time = long.Parse(ztime);
            long distance = long.Parse(zdistance);


            for (int i = 1; i < time; i++)
            {
                if (i * (time - i) > distance)
                {
                    result = time - i - i + 1;
                    break;
                }
            }
            timer.Stop();
            Console.WriteLine($"{timer.ElapsedMilliseconds} ms");
            return result;
        }
    }
}
