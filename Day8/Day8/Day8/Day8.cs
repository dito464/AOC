using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    public class Day8
    {
        public static string input = Input.input;

        public static long day8SolvePart1()
        {
            long result = 0;
            List<string> splitInput = input.Split("\r\n\r\n").ToList();
            char[] instructions = splitInput[0].ToArray();
            List<string> locations = splitInput[1].Split("\r\n").ToList();
            Dictionary<string, (string, string)> movements = new Dictionary<string, (string, string)>();
            foreach (string location in locations)
            {
                List<string> splitLocation = location.Replace("=", "").Replace("(", "").Replace(")", "").Replace(",","").Split(" ").ToList();
                splitLocation.RemoveAt(1);
                movements.Add(splitLocation[0], (splitLocation[1], splitLocation[2]));
            }
            string currentLocation = "AAA";
            bool found = false;
            while (!found)
            {
                foreach (var dir in instructions)
                {
                    if (currentLocation == "ZZZ")
                    {
                        found = true;
                        break;
                    }

                    if (dir == 'R')
                    {
                        currentLocation = movements[currentLocation].Item2;
                    }
                    if (dir == 'L')
                    {
                        currentLocation = movements[currentLocation].Item1;
                    }
                    result++;
                }
            }


            return result;
        }

        public static long day8SolvePart2()
        {
            long result = 0;
            List<string> splitInput = input.Split("\r\n\r\n").ToList();
            char[] instructions = splitInput[0].ToArray();
            long instructLength = instructions.Length;
            List<string> locations = splitInput[1].Split("\r\n").ToList();
            Dictionary<string, (string, string)> movements = new Dictionary<string, (string, string)>();
            List<string> currentLocations = new List<string>();
            List<string> startLocations = new List<string>();
            foreach (string location in locations)
            {
                List<string> splitLocation = location.Replace("=", "").Replace("(", "").Replace(")", "").Replace(",", "").Split(" ").ToList();
                splitLocation.RemoveAt(1);
                movements.Add(splitLocation[0], (splitLocation[1], splitLocation[2]));
                if (splitLocation[0].EndsWith('A'))
                {
                    currentLocations.Add(splitLocation[0]);
                    startLocations.Add(splitLocation[0]);

                }
            }

            Dictionary<long, List<long>> endSteps = new Dictionary<long, List<long>>();
            Dictionary<long,List<long>> reccurrance = new Dictionary<long, List<long>>();
            long paths = currentLocations.Count;
            long stepCount = 0;
            for (long i = 0; i < paths; i++) 
            {
                endSteps.Add(i, new List<long>());
                reccurrance.Add(i, new List<long>());
            }
            bool somefound = false;
            long prevFound = 0;
            bool found = false;
            Dictionary<long, long> firstFound = new Dictionary<long, long>();

            while (!found)
            {
                if (endSteps.All(x => x.Value.Count > 1))
                {
                    found = true;
                }

                foreach (var dir in instructions)
                {
                    stepCount++;
                    for (int i = 0; i < paths; i++)
                    {
                        if (dir == 'R')
                        {
                            currentLocations[i] = movements[currentLocations[i]].Item2;
                        }
                        if (dir == 'L')
                        {
                            currentLocations[i] = movements[currentLocations[i]].Item1;
                        }

                        if (currentLocations[i].EndsWith('Z'))
                        {
                            endSteps[i].Add(stepCount);
                            if (endSteps[i].Count == 1)
                            {
                                firstFound.Add(i, stepCount);
                            }
                            if (endSteps[i].Count > 1)
                            {
                                reccurrance[i].Add(endSteps[i][endSteps[i].FindLastIndex(x => true)]- endSteps[i][endSteps[i].FindLastIndex(x => true)-1]);
                            }
                        }

                    }

                }
            }
            result = 1;
            List<int> factors = new List<int>();
            foreach (var rec in reccurrance) 
            {
                for (int i = 2; i <= rec.Value[0] / 2; i++)
                {
                    if (rec.Value[0] % i == 0)
                        factors.Add(i);
                }
            }
            factors = factors.Distinct().ToList();
            for (int j = 0; j < factors.Count; j++)
            {
                for (int i = 0; i < factors.Count; i++)
                {
                    if (factors[i] == factors[j])
                        break;
                    if (factors[i] % factors[j] == 0)
                    {
                        factors.Remove(factors[i]);
                        i--;
                        j--;
                    }
                }
            }
            foreach (var fac in factors)
            {
                result *= fac;
            }
            /*found = false;
            while (!found) 
            {
                firstFound = firstFound.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                for (long i = 0; i < paths; i++)
                {
                    if (i != firstFound.First().Key)
                    {
                        while (firstFound[i] <= firstFound.First().Value)
                        {
                            firstFound[i] += reccurrance[i][0];
                        }
                    }
                }
                if (firstFound.All(x => x.Value == firstFound[0]))
                {
                    found = true;
                    result = firstFound[0];
                }
            }*/
        

            return result;
        }
    }
}
