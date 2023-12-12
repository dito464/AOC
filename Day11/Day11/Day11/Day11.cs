using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    
    public class Day11
    {
        private static string[] splitInput = Input.input.Split("\r\n");
        
        public static long day11SolvePart1()
        {
            long result = 0;
            char[,] inputArray = new char[splitInput.Length,splitInput.Length];
            List<int> emptyRows = new List<int>();
            List<int> emptyCols = new List<int>();
            List<(int, int)> galaxies = new List<(int, int)>();
            for (int i = 0; i < splitInput.Length; i++)
            {
                bool emptyRow = true;
                char[] splitLine = splitInput[i].ToCharArray();
                for (int j = 0; j < splitInput.Length; j++)
                {
                    inputArray[i,j] = splitLine[j];
                    if (inputArray[i,j] == '#')
                    {
                        emptyRow = false;
                        galaxies.Add((i, j));
                    }
                }
                if (emptyRow)
                    emptyRows.Add(i);
            }

            for (int i = 0; i < splitInput.Length; i++)
            {
                bool emptyCol = true;
                for (int j = 0; j < splitInput.Length; j++)
                {
                    if (inputArray[j, i] == '#')
                    {
                        emptyCol = false;
                        break;
                    }
                }
                if (emptyCol)
                    emptyCols.Add(i);
            }

            for (int i = 0; i < galaxies.Count; i++)
            {
                long subresult = 0;
                for (int j = i+1; j < galaxies.Count; j++)
                {
                    int bigRow = galaxies[j].Item1 >= galaxies[i].Item1 ? galaxies[j].Item1 : galaxies[i].Item1;
                    int smallRow = galaxies[j].Item1 <= galaxies[i].Item1 ? galaxies[j].Item1 : galaxies[i].Item1;
                    int bigCol = galaxies[j].Item2 >= galaxies[i].Item2 ? galaxies[j].Item2 : galaxies[i].Item2;
                    int smallCol = galaxies[j].Item2 <= galaxies[i].Item2 ? galaxies[j].Item2 : galaxies[i].Item2;
                    long rowlength = bigRow - smallRow;
                    rowlength += emptyRows.Where(x => x < bigRow && x > smallRow).Count() * (1000000 - 1);
                    long colLength = bigCol - smallCol;
                    colLength += emptyCols.Where(x => x < bigCol && x > smallCol).Count() * (1000000 - 1);
                    subresult += rowlength + colLength;
                }
                result += subresult;
            }

            return result;
        }
    }
}
