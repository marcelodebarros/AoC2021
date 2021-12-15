using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day10
    {
        public long Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            long retVal = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                retVal += FindMismatchedBracket(line);
            }
            sr.Close();

            return retVal;
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            List<long> scores = new List<long>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                if (FindMismatchedBracket(line) == 0)
                {
                    scores.Add(CompleteTheBrackets(line));
                }
            }
            sr.Close();

            long[] arr = scores.ToArray();
            Array.Sort(arr);

            return arr[arr.Length / 2];
        }

        private long CompleteTheBrackets(string line)
        {
            Hashtable bracketToPenalty = new Hashtable();

            bracketToPenalty.Add(')', 1);
            bracketToPenalty.Add(']', 2);
            bracketToPenalty.Add('}', 3);
            bracketToPenalty.Add('>', 4);

            Hashtable bracketMapping = new Hashtable();

            bracketMapping.Add('(', ')');
            bracketMapping.Add('{', '}');
            bracketMapping.Add('[', ']');
            bracketMapping.Add('<', '>');

            Stack<char> stack = new Stack<char>();
            foreach (char c in line)
            {
                if (bracketMapping.ContainsKey(c))
                {
                    stack.Push(c);
                }
                else
                {
                    stack.Pop();
                }
            }

            long retVal = 0;
            while (stack.Count > 0)
            {
                retVal = 5 * retVal + (int)bracketToPenalty[(char)bracketMapping[stack.Pop()]];
            }

            return retVal;
        }

        private long FindMismatchedBracket(string line)
        {
            Hashtable bracketToPenalty = new Hashtable();

            bracketToPenalty.Add(')', 3);
            bracketToPenalty.Add(']', 57);
            bracketToPenalty.Add('}', 1197);
            bracketToPenalty.Add('>', 25137);

            Hashtable bracketMapping = new Hashtable();

            bracketMapping.Add('(', ')');
            bracketMapping.Add('{', '}');
            bracketMapping.Add('[', ']');
            bracketMapping.Add('<', '>');

            Stack<char> stack = new Stack<char>();
            foreach (char c in line)
            {
                if (bracketMapping.ContainsKey(c))
                {
                    stack.Push(c);
                }
                else
                {
                    char openBracket = stack.Pop();
                    if (c != (char)bracketMapping[openBracket])
                    {
                        return (int)bracketToPenalty[c];
                    }
                }
            }

            return 0L;
        }
    }
}
