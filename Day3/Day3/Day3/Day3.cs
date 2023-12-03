using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    internal class Day3
    {
        public static int day3SolvePart1(string input)
        {

            int result = 0;
            List<string> lines = input.Split("\r\n").ToList();
            int numOfLines = lines.Count;
            int numOfCharsPerLine = lines[0].ToCharArray().Length;
            string[,] linesArray = new string[numOfLines,numOfCharsPerLine];

            List<string> ignoredChars = new List<string>() {"0","1","2","3","4","5","6","7","8","9","."};
            List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

            //Create The Array
            for (int i = 0; i < lines.Count; i++)
            {
                var chars = lines[i].ToCharArray();

                for (int j = 0; j < chars.Length; j++)
                {
                    linesArray[i, j] = chars[j].ToString();
                }

            }

            for (int i = 0; i < lines.Count; i++)
            {
                string currentNumberStr = "";

                for (int j = 0; j < numOfCharsPerLine; j++)
                {
                    
                    if (numbers.Contains(linesArray[i, j]))
                    {
                        currentNumberStr += linesArray[i, j];
                    }

                    if ((!numbers.Contains(linesArray[i, j]) || j == numOfCharsPerLine - 1) && ! (currentNumberStr == "")) 
                    {
                        bool adjacent = false;

                        if (numbers.Contains(linesArray[i, j]))
                        {
                            if (j - currentNumberStr.Length != -1)
                                if (!ignoredChars.Contains(linesArray[i, j - currentNumberStr.Length]))
                                    adjacent = true;
                        }
                        else
                        {
                            if (!ignoredChars.Contains(linesArray[i, j]))
                                adjacent = true;

                            if (j - currentNumberStr.Length - 1 != -1)
                                if (!ignoredChars.Contains(linesArray[i, j - currentNumberStr.Length - 1]))
                                    adjacent = true;
                        }    


                        

                        for (int k = j - currentNumberStr.Length - 1; k <= j; k++)
                        {
                            if (adjacent)
                                break;
                            if(k != -1 && k != numOfCharsPerLine)
                            {
                                if(i-1 != -1)
                                    if (!ignoredChars.Contains(linesArray[i - 1, k]))
                                        adjacent = true;
                                
                                if(i+1 != numOfLines)
                                    if (!ignoredChars.Contains(linesArray[i + 1, k]))
                                        adjacent = true;
                            }
                        }

                        if (adjacent)
                            result += int.Parse(currentNumberStr);
                        currentNumberStr = "";
                    }

                }

            }

            return result;

        }

        public static int day3SolvePart2(string input)
        {

            int result = 0;
            List<string> lines = input.Split("\r\n").ToList();
            int numOfLines = lines.Count;
            int numOfCharsPerLine = lines[0].ToCharArray().Length;
            string[,] linesArray = new string[numOfLines, numOfCharsPerLine];

            List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            //Create The Array
            for (int i = 0; i < lines.Count; i++)
            {
                var chars = lines[i].ToCharArray();

                for (int j = 0; j < chars.Length; j++)
                {
                    linesArray[i, j] = chars[j].ToString();
                }

            }

            for (int i = 0; i < lines.Count; i++)
            {
                

                for (int j = 0; j < numOfCharsPerLine; j++)
                {

                    if ("*".Contains(linesArray[i, j]))
                    {
                        string currentNumberStr1 = "";
                        string currentNumberStr2 = "";
                        int adjacentNumbers = 0;
                        int[] locations = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                        for (int k = i-1; k <= i+1; k++)
                        {
                            for( int l = j-1; l <= j+1; l++)
                            {
                                if(k != -1 && l != -1 && k != numOfLines && l != numOfCharsPerLine)
                                {
                                    if (l != j)
                                        if (numbers.Contains(linesArray[k, l]))
                                        {
                                            adjacentNumbers++;
                                            locations[3*(k-i) + (l-j) + 4 ] = 1;
                                        }

                                    if (l == j)
                                    {
                                        if (numbers.Contains(linesArray[k, l - 1]) && numbers.Contains(linesArray[k, l + 1]) && numbers.Contains(linesArray[k, l]))
                                        {
                                            adjacentNumbers--;
                                            locations[3 * (k - i) + (l - j) + 4 - 1] = 0;
                                        }

                                        if (!numbers.Contains(linesArray[k, l - 1]) && !numbers.Contains(linesArray[k, l + 1]) && numbers.Contains(linesArray[k, l])) 
                                        { 
                                            adjacentNumbers++;
                                            locations[3 * (k - i) + (l - j) + 4] = 1;
                                        }
                                            
                                    }
                                }
                            }
                        }

                        if (adjacentNumbers == 2)
                        {
                            

                            for (int k = i - 1; k <= i + 1; k++)
                            {
                                for (int l = j - 1; l <= j + 1; l++)
                                {
                                    int startOffset = 0;
                                    int endOffset = 0;

                                    if (locations[3 * (k - i) + (l - j) + 4] == 1)
                                    {
                                        for(int startFinder = l - 1; startFinder >= 0; startFinder--)
                                        {
                                            if (!numbers.Contains(linesArray[k, startFinder]))
                                                break;

                                            startOffset++;
                                        }
                                        for (int endFinder = l + 1; endFinder < numOfCharsPerLine; endFinder++)
                                        {
                                            if (!numbers.Contains(linesArray[k, endFinder]))
                                                break;

                                            endOffset++;
                                        }
                                        if(currentNumberStr1 == "") 
                                        {
                                            for (int numberTracker = l - startOffset; numberTracker <= l + endOffset; numberTracker++)
                                            {
                                                currentNumberStr1 += linesArray[k, numberTracker];
                                            }
                                        }
                                        else
                                        {
                                            for (int numberTracker = l - startOffset; numberTracker <= l + endOffset; numberTracker++)
                                            {
                                                currentNumberStr2 += linesArray[k, numberTracker];
                                            }
                                        }
                                    }
                                }
                            }

                            result += int.Parse(currentNumberStr1) * int.Parse(currentNumberStr2);

                        }

                    }

                }

            }

            return result;

        }
    }
}
