using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day10;

namespace Day10
{
    public class Day10
    {
        private static string input = Input.input;
        private static char[,] ArrayMaker(string input)
        {
            string[] rows = input.Split("\r\n");
            char[,] puzzArray = new char[rows[0].ToCharArray().Length, rows.Length];
            for (int j = 0; j < rows.Length; j++)
            {
                char[] cells = rows[j].ToCharArray();
                for (int i = 0; i < cells.Length; i++)
                {
                    puzzArray[i, j] = cells[i];
                }
            }
            return puzzArray;
        }

        static char[,] map = ArrayMaker(input);

        public static (int, int, int) Follow((int, int, int) location)
        {
            char pipe = map[location.Item1, location.Item2];
            int side = location.Item3;
            (int, int, int) result = (location.Item1, location.Item2, -1);

            switch (pipe)
            {
                case '.':
                    result = (location.Item1, location.Item2, -1);
                    break;

                case 'S':
                    result = (location.Item1, location.Item2, 0);
                    break;

                case '|':
                    if (side == 1)
                    {
                        result = (location.Item1, location.Item2 - 1, 1);
                    }
                    if (side == 3)
                    {
                        result = (location.Item1, location.Item2 + 1, 3);
                    }
                    break;

                case '-':
                    if (side == 2)
                    {
                        result = (location.Item1 + 1, location.Item2, 2);
                    }
                    if (side == 4)
                    {
                        result = (location.Item1 - 1, location.Item2, 4);
                    }
                    break;

                case 'L':
                    if (side == 3)
                    {
                        result = (location.Item1 + 1, location.Item2, 2);
                    }
                    if (side == 4)
                    {
                        result = (location.Item1, location.Item2 - 1, 1);
                    }
                    break;

                case 'J':
                    if (side == 3)
                    {
                        result = (location.Item1 - 1, location.Item2, 4);
                    }
                    if (side == 2)
                    {
                        result = (location.Item1, location.Item2 - 1, 1);
                    }
                    break;

                case '7':
                    if (side == 1)
                    {
                        result = (location.Item1 - 1, location.Item2, 4);
                    }
                    if (side == 2)
                    {
                        result = (location.Item1, location.Item2 + 1, 3);
                    }
                    break;

                case 'F':
                    if (side == 1)
                    {
                        result = (location.Item1 + 1, location.Item2, 2);
                    }
                    if (side == 4)
                    {
                        result = (location.Item1, location.Item2 + 1, 3);
                    }
                    break;
            }

            if(result.Item1 >=140 || result.Item2 >= 140 || result.Item2 < 0 || result.Item1 < 0)
                result = (location.Item1, location.Item2, -1);
            return result;
        }


        

        public static (int,int) day10SolvePart1()
        {
            int area = 0;
            
            int length = 0;
            (int, int) startPoint = (0, 0);
            bool breakFlag = false;

            for(int i = 0; i < 140; i++)
            {
                for (int j = 0; j < 140; j++)
                {
                    if (map[i, j] == 'S')
                    {
                        startPoint = (i, j);
                        breakFlag = true;
                        break;
                    }
                }
                if (breakFlag)
                    break;
            }

            for (int dir = 1; dir <= 4; dir++)
            {
                breakFlag = false;
                bool found = false;
                int subresult = 0;
                (int, int, int) location = (startPoint.Item1 + (-2 * ((dir - 1) % 2) + ((dir - 1) % 4)) % 2, startPoint.Item2 + (-2 * (dir % 2) + (dir % 4)) % 2, dir);
                List<(int,char)>[] columnHits = new List<(int,char)>[140];
                for (int i = 0; i < 140; i++)
                {
                    columnHits[i] = new List<(int, char)>();
                }

                if (location.Item1 < 140 && location.Item1 >= 0 && location.Item2 < 140 && location.Item2 >= 0)
                {
                    while (!breakFlag && !found)
                    {
                        columnHits[location.Item1].Add((location.Item2,map[location.Item1,location.Item2]));
                        location = Follow(location);
                        subresult++;
                        if (location.Item3 == 0)
                            found = true;
                        if (location.Item3 == -1)
                            breakFlag = true;
                    }
                }

                if (found)
                {
                    /* for (int i = 0; i < 140; i++)
                     {
                         List<int> hits = columnHits[i].OrderBy(x => x.Item1).ToList();
                         for (int j = 0; j < hits.Count - 1 ; j++)
                         {
                             if (hits[j].Item2 == 1)
                             {
                                 area += Math.Abs(hits[j].Item1 - hits[j + 1].Item1) - 1;
                             }
                         }
                     }*/
                    for (int i = 0; i < 140; i++)
                    {
                        List<(int,char)> hits = columnHits[i].OrderBy(x => x.Item1).ToList();
                        if (hits.Count > 0)
                        {
                            for (int j = hits[0].Item1 + 1; j < 140; j++)
                            {
                                double pipeCount = 0;

                                if (hits.Any(x => x.Item1 == j))
                                    continue;

                                for (int k = j; k < 140; k++)
                                    if (hits.Any(x => x.Item1 == k))
                                    {
                                        if (hits.Where(x => x.Item1 ==k).First().Item2 == 'L' || hits.Where(x => x.Item1 == k).First().Item2 == '7')
                                        {
                                            pipeCount += 0.5;
                                        }
                                        else if (hits.Where(x => x.Item1 == k).First().Item2 == 'F' || hits.Where(x => x.Item1 == k).First().Item2 == 'J')
                                        {
                                            pipeCount -= 0.5;
                                        }
                                        else
                                            pipeCount++;
                                    }
                                        
                                if (pipeCount % 2 != 0)
                                    area++;
                            }
                        }
                    }
                    length = subresult / 2;
                    break;

                }
            }
            

            return (length, area);
        }

        public static int day9SolvePart2()
        {
            int result = 0;

            

            return result;
        }
    }
}
