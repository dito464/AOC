using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCDay1
{
    public class AOCDay1Class
    {
        public int AOC1(string input)
        {

            string[] lines = input.Split("\r\n");
            int result = 0;



            foreach (var line in lines)
            {
                int tens = 0;
                int ones = 0;

                for (int i = 0; i < line.Length; i++)
                {
                    bool found = false;
                    switch (line[i])
                    {
                        case '1': tens = 1; found = true; break;
                        case '2': tens = 2; found = true; break;
                        case '3': tens = 3; found = true; break;
                        case '4': tens = 4; found = true; break;
                        case '5': tens = 5; found = true; break;
                        case '6': tens = 6; found = true; break;
                        case '7': tens = 7; found = true; break;
                        case '8': tens = 8; found = true; break;
                        case '9': tens = 9; found = true; break;
                    }

                    if (i < line.Length - 2)
                    {
                        switch (line.Substring(i, 3))
                        {
                            case "one": tens = 1; found = true; break;
                            case "two": tens = 2; found = true; break;
                            case "six": tens = 6; found = true; break;
                        }
                    }

                    if (i < line.Length - 3)
                    {
                        switch (line.Substring(i, 4))
                        {
                            case "four": tens = 4; found = true; break;
                            case "five": tens = 5; found = true; break;
                            case "nine": tens = 9; found = true; break;
                        }
                    }

                    if (i < line.Length - 4)
                    {
                        switch (line.Substring(i, 5))
                        {
                            case "three": tens = 3; found = true; break;
                            case "seven": tens = 7; found = true; break;
                            case "eight": tens = 8; found = true; break;
                        }
                    }

                    if (found)
                    break;
                }

                for (int i = line.Length - 1; i >= 0; i--) 
                {
                    bool found = false;
                    switch (line[i])
                    {
                        case '1': ones = 1; found = true; break;
                        case '2': ones = 2; found = true; break;
                        case '3': ones = 3; found = true; break;
                        case '4': ones = 4; found = true; break;
                        case '5': ones = 5; found = true; break;
                        case '6': ones = 6; found = true; break;
                        case '7': ones = 7; found = true; break;
                        case '8': ones = 8; found = true; break;
                        case '9': ones = 9; found = true; break;
                    }

                    if (i > 1)
                    {
                        switch (line.Substring(i - 2, 3))
                        {
                            case "one": ones = 1; found = true; break;
                            case "two": ones = 2; found = true; break;
                            case "six": ones = 6; found = true; break;
                        }
                    }

                    if (i > 2)
                    {
                        switch (line.Substring(i - 3, 4))
                        {
                            case "four": ones = 4; found = true; break;
                            case "five": ones = 5; found = true; break;
                            case "nine": ones = 9; found = true; break;
                        }
                    }

                    if (i > 3)
                    {
                        switch (line.Substring(i - 4, 5))
                        {
                            case "three": ones = 3; found = true; break;
                            case "seven": ones = 7; found = true; break;
                            case "eight": ones = 8; found = true; break;
                        }
                    }

                    if (found)
                    break;
                }

                result += tens * 10 + ones;
            }

            return result;
        }
    }
}
