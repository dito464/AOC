using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    public class Day5
    {
        public static long day5SolvePart1(string input)
        {
            List<string> splitInput = input.Split("\r\n\r\nseed-to-soil map:").ToList();
            List<string> seeds = splitInput[0].Remove(0, 6).Split(" ").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("soil-to-fertilizer map:").ToList();
            List<string> seedSoilMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("fertilizer-to-water map:").ToList();
            List<string> soilFertilzerMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("water-to-light map:").ToList();
            List<string> fertilzerWaterMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("light-to-temperature map:").ToList();
            List<string> waterLightMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("temperature-to-humidity map:").ToList();
            List<string> lightTempMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("humidity-to-location map:").ToList();
            List<string> tempHumMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            List<string> humLocMap = splitInput[1].Split("\r\n").Where(x => x != "").ToList();

            List<(long, long)> seedLocMap = new List<(long, long)> ();

            long min = long.MaxValue;

            foreach (var zSeed in seeds)
            {
                long seed = long.Parse(zSeed);
                long soil = mapFinder(seed,seedSoilMap);
                long fert = mapFinder(soil, soilFertilzerMap);
                long water = mapFinder(fert, fertilzerWaterMap);
                long light = mapFinder(water, waterLightMap);
                long temp = mapFinder(light, lightTempMap);
                long hum = mapFinder(temp, tempHumMap);
                long loc = mapFinder(hum, humLocMap);

                if (loc < min) min = loc;
                seedLocMap.Add((seed, loc));

            }
            return min;
        }

        public static long mapFinder(long input, List<string> mapping)
        {
            long result = input;
            bool foundResult = false;
            foreach (var item in mapping) 
            {
                if (foundResult)
                    break;

                List<string> splitItem = item.Split(" ").ToList();
                long dest = uint.Parse(splitItem[0]);
                long sourc = uint.Parse(splitItem[1]);
                long range = uint.Parse(splitItem[2]);

                if (input >= sourc && input <= range + sourc)
                {
                    result = dest + input - sourc ;
                    foundResult = true;
                }
            }
            return result;
        }

        public static long day5SolvePart2(string input)
        {
            Stopwatch timer = Stopwatch.StartNew();
            List<string> splitInput = input.Split("\r\n\r\nseed-to-soil map:").ToList();
            List<string> seeds = splitInput[0].Remove(0, 6).Split(" ").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("soil-to-fertilizer map:").ToList();
            List<string> seedSoilMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("fertilizer-to-water map:").ToList();
            List<string> soilFertilzerMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("water-to-light map:").ToList();
            List<string> fertilzerWaterMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("light-to-temperature map:").ToList();
            List<string> waterLightMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("temperature-to-humidity map:").ToList();
            List<string> lightTempMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            splitInput = splitInput[1].Split("humidity-to-location map:").ToList();
            List<string> tempHumMap = splitInput[0].Split("\r\n").Where(x => x != "").ToList();
            List<string> humLocMap = splitInput[1].Split("\r\n").Where(x => x != "").ToList();

            List<(long, long)> seedLocMap = new List<(long, long)>();

            long min = long.MaxValue;

            for (int i = 0; i < seeds.Count; i += 2)
            {
                bool processed = false;
                long seed = long.Parse(seeds[i]);
                long range = long.Parse(seeds[i + 1]);
                while (!processed)
                {
                    
                    long soil = mapFinder2(seed, range, seedSoilMap, out range);
                    long fert = mapFinder2(soil, range, soilFertilzerMap, out range);
                    long water = mapFinder2(fert, range, fertilzerWaterMap, out range);
                    long light = mapFinder2(water, range, waterLightMap, out range);
                    long temp = mapFinder2(light, range, lightTempMap, out range);
                    long hum = mapFinder2(temp, range, tempHumMap, out range);
                    long loc = mapFinder2(hum, range, humLocMap, out range);
                    if (loc < min) 
                        min = loc;

                    if (seed + range >= long.Parse(seeds[i]) + long.Parse(seeds[i + 1]))
                    {
                        processed = true;
                    }
                    seed = seed + range;
                    range = long.Parse(seeds[i]) + long.Parse(seeds[i+1]) - seed + 1;

                    
                }

            }
            timer.Stop();
            Console.WriteLine($"{timer.ElapsedMilliseconds} ms");
            return min;
        }

        public static long mapFinder2(long input,long inputRange, List<string> mapping, out long rangeChanger)
        {
            
            long result = input;
            bool foundResult = false;
            rangeChanger = inputRange;
            foreach (var item in mapping)
            {
                if (foundResult)
                    break;

                List<string> splitItem = item.Split(" ").ToList();
                long dest = uint.Parse(splitItem[0]);
                long sourc = uint.Parse(splitItem[1]);
                long range = uint.Parse(splitItem[2]);

                if (input >= sourc && input <= range + sourc - 1)
                {
                    result = dest + input - sourc;
                    rangeChanger = sourc + range - input  < inputRange ? sourc + range - input : inputRange;
                    foundResult = true;
                }
            }
            
            return result;
        }

    }
}
