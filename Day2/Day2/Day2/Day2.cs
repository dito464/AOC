using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Solver
{
    public class Day2
    {
        public static (int, int) day2Solve(string input)
        {

            Dictionary<string, int> maxAllowed = new Dictionary<string, int>() {
                {"red", 12}, {"blue", 14},{"green", 13}
                };
            int[] result = new int[2] {0,0};
            List<string> lines = input.Split("\r\nGame").ToList();
            lines[0] = lines[0].Substring(5);
            //lines.Remove(lines[0]);
            foreach (var line in lines)
            {
                Dictionary<string,int> largestFound = new Dictionary<string,int>() { 
                {"red", 0}, {"blue", 0},{"green", 0}
                };

                List<string> games = line.Split(':').ToList();
                int id = int.Parse(games[0]);
                List<string> splitGames = games[1].Split(';').ToList();
                bool possible = true;
                foreach (var game in splitGames)
                {
                    List<string> items = game.Split(",").ToList();
                    foreach (var item in items) 
                    {
                        List<string> values = item.Split(" ").ToList();
                        if (int.Parse(values[1]) > largestFound[values[2]])
                        {
                            largestFound[values[2]] = int.Parse(values[1]);
                        }
                    }
                    foreach(var key in largestFound.Keys) 
                    {
                        if (largestFound[key] > maxAllowed[key])
                            possible = false;
                    }

                }
                if (possible)
                    result[0] += id;
                result[1] += largestFound["red"] * largestFound["green"] * largestFound["blue"];
                
            }

            return (result[0],result[1]);

        }
    }
}
