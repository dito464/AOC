using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Day4
    {

        public static int day4SolvePart1(string input)
        {
            int result = 0;
            List<string> messyLines = input.Split("\r\n").ToList();
            foreach(var messyLine in messyLines)
            {
                int subresult = 0;
                string cleanLine = messyLine.Remove(0, messyLine.IndexOf(":")+2);
                List<string> line  = cleanLine.Split("|").ToList();
                List<string> theirNumbers = line[0].Split(" ").ToList();
                theirNumbers = theirNumbers.Where(x => x != "").ToList();
                List<string> ourNumbers = line[1].Split(" ").ToList();
                ourNumbers = ourNumbers.Where(x => x != "").ToList();


                foreach (var number in ourNumbers) 
                { 
                    if(theirNumbers.Contains(number))
                    {
                        if (subresult == 0)
                            subresult = 1;
                        else 
                            subresult *= 2;
                    }
                }
                result += subresult;
            }

            return result;

        }

        public static int day4SolvePart2(string input)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            int result = 0;
            List<string> messyLines = input.Split("\r\n").ToList();
            List<List<List<string>>> formattedLines = new List<List<List<string>>>();
            List<int> countPerItem = new List<int>();
            List<int> cumCountPerItem = new List<int>();

            for (int i = 0; i < messyLines.Count; i++)
            {
                formattedLines.Add(new List<List<string>>());
                string cleanLine = messyLines[i].Remove(0, messyLines[i].IndexOf(":") + 2);
                List<string> line = cleanLine.Split("|").ToList();
                List<string> theirNumbers = line[0].Split(" ").ToList();
                theirNumbers = theirNumbers.Where(x => x != "").ToList();
                List<string> ourNumbers = line[1].Split(" ").ToList();
                ourNumbers = ourNumbers.Where(x => x != "").ToList();

                formattedLines[i].Add(theirNumbers);
                formattedLines[i].Add(ourNumbers);
            }

            for (int i = 0; i < formattedLines.Count; i++)
            {
                int localCount = 0;
                foreach (var number in formattedLines[i][1])
                    if (formattedLines[i][0].Contains(number))
                        localCount++;
                    
                countPerItem.Add(localCount);
            }

            for (int i = formattedLines.Count-1; i >=0 ; i--)
            {
                List<int> indexesToCount = new List<int>();
                int indexTracker = i;
                int subresult = 0;
                subresult++;
                for (int j = i + 1; j <= i + countPerItem[i]; j++)
                    indexesToCount.Add(j);
                foreach (var number in indexesToCount)
                    subresult += cumCountPerItem[formattedLines.Count - 1 - number];

                cumCountPerItem.Add(subresult);
                result += subresult;

            }

            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds.ToString());
            return result;

        }
    }

}
