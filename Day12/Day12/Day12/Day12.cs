using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    public class Day12
    {
        private static string input = Input.input;
        public static long day12SolvePart1()
        {
            long result = 0;
            List<string> splitInput =   input.Split("\r\n").ToList();
            foreach (var line in splitInput)
            {
                string[] splitLine = line.Split(" ");
                char[] springs = splitLine[0].ToCharArray();
                string[] lengths = splitLine[1].Split(",");
                int spring = 0;
                int space = 0;
                int unknown = 0;

                for (int i = 0; i < springs.Length; i++)
                {
                    switch (springs[i])
                    {
                        case '?':
                            unknown++;
                            break;
                        case '#':
                            {
                                spring++;
                                break;
                            }
                        case '.':
                            {
                                space++;
                            }
                            break;
                    }
                }

                if (unknown == 0)
                {
                    result += 1;
                    continue;
                }
                Dictionary<(int, int, int), long> seen = new Dictionary<(int, int, int), long>();
                result += splitPath(0, 0, springs, 0, lengths, 0, seen, out seen);
                


               

            }
            return result;
        }

        public static long splitPath(int subresult, int index, char[] springs, int continuousSprings, string[] lengths, int lengthTracker, Dictionary<(int, int, int), long> inDic, out Dictionary<(int, int, int), long> outDic)
        {
            long result = subresult;
            long stashed;
            outDic = inDic;
            if (inDic.Count > 0 && inDic.TryGetValue((index,continuousSprings,lengthTracker),out stashed)) 
            {
                return stashed;
            }
            if (springs[index] == '?')
            {
                if (lengthTracker < lengths.Length && continuousSprings < int.Parse(lengths[lengthTracker]) && index < springs.Length - 1)
                {
                    result += splitPath(subresult, index + 1, springs, continuousSprings + 1, lengths, lengthTracker,inDic, out inDic);
                    /*else if (index == springs.Length - 1 && lengthTracker == lengths.Length - 1 && continuousSprings == int.Parse(lengths[lengthTracker]) - 1)
                        result++;*/
                }
                if (lengthTracker < lengths.Length - 1 && continuousSprings == int.Parse(lengths[lengthTracker]) && index < springs.Length - 1)
                {
                    result += splitPath(subresult, index + 1, springs, 0, lengths, lengthTracker + 1, inDic, out inDic);
                    
                    /*if (index == springs.Length - 1)
                    {
                        if (lengthTracker == lengths.Length)
                            result += 1;
                    }*/
                }
                if (lengthTracker == lengths.Length - 1 && continuousSprings == int.Parse(lengths[lengthTracker]) && index < springs.Length - 1)
                {
                    result += splitPath(subresult, index + 1, springs, 0, lengths, lengthTracker + 1, inDic, out inDic);
                }
                if (continuousSprings == 0 && index < springs.Length - 1)
                {
                    result += splitPath(subresult, index + 1, springs, 0, lengths, lengthTracker, inDic, out inDic);
                }
                if ( (index == springs.Length - 1) && (lengthTracker == lengths.Length - 1 && (continuousSprings == int.Parse(lengths[lengthTracker]) - 1 || continuousSprings == int.Parse(lengths[lengthTracker])) || (lengthTracker == lengths.Length && continuousSprings == 0)) ) 
                /*if (lengthTracker == lengths.Length - 1 && continuousSprings == int.Parse(lengths[lengthTracker]))*/
                    result++;

                outDic.Add((index, continuousSprings, lengthTracker), result);
                return result;

            }



            bool hitSplit = false;
            for (int i = index; i < springs.Length; i++)
            {

                if (hitSplit)
                    break;

                switch (springs[i])
                {
                    case '?':
                        result += splitPath(subresult, i, springs, continuousSprings, lengths, lengthTracker, inDic, out inDic);
                        hitSplit = true;
                        break;
                    case '#':
                        if (lengthTracker >= lengths.Length || continuousSprings >= int.Parse(lengths[lengthTracker]))
                            return result;
                        else 
                        { 
                            continuousSprings++;
                            break;
                        }
                    case '.':
                        /*if (lengthTracker < lengths.Length && continuousSprings < int.Parse(lengths[lengthTracker]))
                            return result;
                        else*/ if (lengthTracker < lengths.Length && continuousSprings == int.Parse(lengths[lengthTracker]))
                        {
                            lengthTracker++;
                            continuousSprings = 0;
                        }
                        else if (lengthTracker < lengths.Length && (continuousSprings > int.Parse(lengths[lengthTracker]) || (continuousSprings < int.Parse(lengths[lengthTracker]) && continuousSprings != 0) ) )
                            return 0;
                        break;
                }
            }

            if (!hitSplit && (lengthTracker == lengths.Length || lengthTracker == lengths.Length - 1 && continuousSprings == int.Parse(lengths[lengthTracker])))
            {
                result += 1;
            }

            return result;

        }

        public static long day12SolvePart2()
        {
            Stopwatch timer = Stopwatch.StartNew();
            long result = 0;
            List<string> splitInput = input.Split("\r\n").ToList();
            foreach (var line in splitInput)
            {         
                string[] splitLine = line.Split(" ");
                char[] origSprings = splitLine[0].ToCharArray();
                string[] origLengths = splitLine[1].Split(",");
                char[] springs = new char[splitLine[0].ToCharArray().Length * 5 + 4];
                string[] lengths = new string[splitLine[1].Split(",").Count() * 5];  
                for (int i = 0; i < 5; i++)
                {
                    int plus = 0;
                    for (int j = 0; j < origSprings.Count(); j++)
                    {
                        springs[i * origSprings.Count() + j + (i)] = origSprings[j];
                    }
                    if (i < 4)
                        springs[(i + 1) * origSprings.Count() + (i)] = '?';

                    for (int j = 0; j < origLengths.Count(); j++)
                    {
                        lengths[i * origLengths.Count() + j] = origLengths[j];
                    }
                }
                int spring = 0;
                int space = 0;
                int unknown = 0;

                for (int i = 0; i < springs.Length; i++)
                {
                    switch (springs[i])
                    {
                        case '?':
                            unknown++;
                            break;
                        case '#':
                            {
                                spring++;
                                break;
                            }
                        case '.':
                            {
                                space++;
                            }
                            break;
                    }
                }

                if (unknown == 0)
                {
                    result += 1;
                    continue;
                }

                Dictionary<(int, int, int), long> seen = new Dictionary<(int, int, int), long>();
                result += splitPath(0, 0, springs, 0, lengths, 0, seen, out seen);           
            }
            timer.Stop();
            Console.WriteLine($"step took {timer.ElapsedMilliseconds} ms");
            return result;
        }

      
    }
}
