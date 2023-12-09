using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    public class Day9
    {
        private static string input = Input.input;

        public static int day9SolvePart1()
        {
            int result = 0;
            List<string> splitInput = input.Split("\r\n").ToList();

            foreach (var line in splitInput)
            {
                List<string> history = line.Split(" ").ToList();
                List<int> nHistory = new List<int>();
                foreach (var num in history)
                    nHistory.Add(int.Parse(num));

                List<List<int>> solverList = new List<List<int>>();
                solverList.Add(nHistory);

                bool solved = false;
                while (!solved)
                {
                    List<int> diffList = new List<int>();
                    List<int> currRow = solverList.Last();
                    for (int i = 1; i< currRow.Count; i++)
                        diffList.Add(currRow[i] - currRow[i-1]);

                    solverList.Add(diffList);

                    if (diffList.All(diff => diff == 0))
                        solved = true;
                    
                }

                foreach (var list in solverList)
                    result += list.Last();
            }

            return result;
        }

        public static int day9SolvePart2()
        {
            int result = 0;
            List<string> splitInput = input.Split("\r\n").ToList();

            foreach (var line in splitInput)
            {
                List<string> history = line.Split(" ").ToList();
                List<int> nHistory = new List<int>();
                foreach (var num in history)
                    nHistory.Add(int.Parse(num));

                List<List<int>> solverList = new List<List<int>>();
                solverList.Add(nHistory);

                bool solved = false;
                while (!solved)
                {
                    List<int> diffList = new List<int>();
                    List<int> currRow = solverList.Last();
                    for (int i = 1; i < currRow.Count; i++)
                        diffList.Add(currRow[i] - currRow[i - 1]);

                    solverList.Add(diffList);

                    if (diffList.All(diff => diff == 0))
                        solved = true;

                }
                int subresult = 0;
                for (int i = solverList.Count - 1; i >=0; i--)
                    subresult = solverList[i].First() - subresult;
                result += subresult;
            }

            return result;
        }
    }
}
