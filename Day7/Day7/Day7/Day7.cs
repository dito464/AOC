using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    public class Day7
    {
        private static string input = Input.input;

        public static int day7SolverPart1()
        {
            int result = 0;
            
            List<string> lines = input.Split("\r\n").ToList();

            List<string> fives = new List<string>();
            List<string> fours = new List<string>();
            List<string> fulls = new List<string>();
            List<string> threes = new List<string>();
            List<string> twoPairs = new List<string>();
            List<string> onePairs = new List<string>();
            List<string> highs = new List<string>();

            foreach (var line in lines)
            {
                string[] parts = line.Split(" ");
                char[] unsortedHand = parts[0].ToArray();
                List<string> sortedHand = new List<string>();
                foreach(char ch in unsortedHand)
                    sortedHand.Add(ch.ToString());
                sortedHand.Sort(cardComparer);
                string sortedHandList = "";
                foreach (var ch in sortedHand)
                    sortedHandList += ch;

                    string points = parts[1];
                Dictionary<char,int> amount = new Dictionary<char,int>();
                char[] uniques = sortedHandList.Distinct().ToArray();
                
                if (uniques.Length == 1)
                {
                    fives.Add(line);
                    continue;
                }

                int jokers = 0;
                int four = 0;
                int three = 0;
                int two = 0;

                foreach (var cardType in uniques) 
                {
                    int counter = 0;
                    foreach (var handCard in unsortedHand)
                        if (handCard == cardType && handCard == 'J') jokers++;
                        else if (handCard == cardType) counter++;
                    if (counter == 4) four++;
                    if (counter == 3) three++;
                    if (counter == 2) two++;
                    if (cardType != 'J')
                        amount.Add(cardType, counter);
                }

                amount = amount.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                char most = amount.First().Key;
                amount[most] += jokers;
                int five = 0;
                four = 0;
                three = 0;
                two = 0;
                foreach(var entry in amount)
                {
                    if (entry.Value == 5) five++;
                    if (entry.Value == 4) four++;
                    if (entry.Value == 3) three++;
                    if (entry.Value == 2) two++;
                }
                string newhand = $"";
                string newline = "";
                foreach (var ch in unsortedHand)
                    newhand += ch.ToString();
                newline += $"{newhand} {points}";
                
                /*if (jokers == 4)
                {
                    fives.Add(newline);
                    continue;
                }
                if (jokers == 3)
                {
                    if (two > 0)
                    {
                        fives.Add(newline);
                        continue;
                    }
                    four++;
                }
                if (jokers == 2)
                {
                    if (three > 0)
                    {
                        fives.Add(newline);
                        continue;
                    }
                    if (two == 0)
                    {
                        four++;
                    }
                    else
                        three++;
                }
                if (jokers == 1)
                {
                    if (four > 0)
                    {
                        fives.Add(newline);
                        continue;
                    }
                    if (three > 0)
                    {
                        four++;
                    }
                    if (two == 0)
                    {
                        three++;
                    }
                    else
                        two++;
                }*/
                if (five > 0)
                {
                    fives.Add(newline);
                }
                else if (four > 0)
                {
                    fours.Add(newline);
                }
                else if (three > 0)
                {
                    if (two > 0)
                        fulls.Add(newline);

                    else
                        threes.Add(newline);
                }
                else if (two == 2)
                    twoPairs.Add(newline);
                else if (two == 1)
                    onePairs.Add(newline);
                else
                    highs.Add(newline);

                /*string newline = "";
                List<string> singles = new List<string>();
                List<string> doubles = new List<string>();
                amount = amount.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                foreach(var card in amount)
                {
                    if (amount[card.Key] > 2) 
                    {
                        for (int i = 0; i < amount[card.Key]; i++)
                            newline += card.Key;
                    }
                    else if (amount[card.Key] == 2)
                    {
                        doubles.Add(card.Key.ToString());
                        doubles.Add(card.Key.ToString());
                    }
                    else
                        singles.Add(card.Key.ToString());
                }
                singles.Sort(cardComparer);
                doubles.Sort(cardComparer);
                string sortedSpares = "";
                foreach (var item in doubles)
                    sortedSpares += item;
                foreach (var item in singles)
                    sortedSpares += item;
                

                newline += sortedSpares;
                newline += $" {points}";
                if (four > 0) fours.Add(newline);
                else if (three > 0)
                {
                    if (two > 0) fulls.Add(newline);
                    else threes.Add(newline);
                }
                else if (two == 2) twoPairs.Add(newline);
                else if (two == 1) onePairs.Add(newline);
                else highs.Add(newline);*/
            }


            fives.Sort(cardComparer);
            fours.Sort(cardComparer);
            fulls.Sort(cardComparer);
            threes.Sort(cardComparer);
            twoPairs.Sort(cardComparer);
            onePairs.Sort(cardComparer);
            highs.Sort(cardComparer);

            List<string> orderedHands = new List<string>();
            orderedHands.AddRange(fives);
            orderedHands.AddRange(fours);
            orderedHands.AddRange(fulls);
            orderedHands.AddRange(threes);
            orderedHands.AddRange(twoPairs);
            orderedHands.AddRange(onePairs);
            orderedHands.AddRange(highs);

            for (int i = 0; i < orderedHands.Count; i++)
            {
                string[] split = orderedHands[i].Split(" ");
                result += (1000 - i) * int.Parse(split[1]);
                
            }

            return result;
        }

        private static int cardComparer(string x, string y)
        {
            string ordering = "AKQT98765432J";
            int minLength = x.Length < y.Length ? x.Length : y.Length;
            for ( int i = 0; i< minLength; i++ )
            {
                int xIndex = ordering.IndexOf(x[i]);
                int yIndex = ordering.IndexOf( y[i]);
                int result = xIndex.CompareTo(yIndex);
                if (result != 0)
                    return result;
            }
            return x.Length.CompareTo(y.Length);
        }
    }
}
