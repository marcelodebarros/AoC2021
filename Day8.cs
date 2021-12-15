using System;
using System.Numerics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day8
    {
        public long Puzzle1(string inputFile)
        {
            string[] display = { 
                                "abcefg",
                                "cf",
                                "acdeg",
                                "acdfg",
                                "bcdf",
                                "abdfg",
                                "abdefg",
                                "acf",
                                "abcdefg",
                                "abcdfg"
                               };

            int[] target = { 1, 4, 7, 8 };
            Hashtable hash = new Hashtable();
            foreach (int t in target) hash.Add(display[t].Length, true);

            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            int count = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] parts = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string[] numbers = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string number in numbers)
                {
                    if (hash.ContainsKey(number.Length)) count++;
                }
            }
            sr.Close();

            return count;
        }

        public long Puzzle2(string inputFile)
        {
            string[] displayStr = {
                                    "abcefg",
                                    "cf",
                                    "acdeg",
                                    "acdfg",
                                    "bcdf",
                                    "abdfg",
                                    "abdefg",
                                    "acf",
                                    "abcdefg",
                                    "abcdfg"
                                  };

            Hashtable display = new Hashtable();
            for (int i = 0; i < displayStr.Length; i++)
            {
                display.Add(SortString(displayStr[i]), i);
            }

            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            long retVal = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] parts = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string[] input = parts[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] output = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                long total = 0;
                if (TryPermutations(input,
                                   output,
                                   "abcdefg",
                                   0,
                                   new Hashtable(),
                                   display,
                                   ref total))
                {
                    retVal += total;
                }
            }
            sr.Close();

            return retVal;
        }

        private string SortString(string s)
        {
            int[] count = new int[26];
            foreach (char c in s) count[(int)(c - 'a')]++;
            string retVal = "";
            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] > 0)
                {
                    retVal += new string((char)(i + 'a'), count[i]);
                }
            }

            return retVal;
        }

        private bool TryPermutations(string[] input,
                                     string[] output,
                                     string letters,
                                     int index,
                                     Hashtable map,
                                     Hashtable display,
                                     ref long retVal)
        {
            if (index >= letters.Length)
            {
                long sumWords = 0;
                if (MapWorks(input, output, map, display, ref sumWords))
                {
                    retVal = sumWords;
                    return true;
                }
                return false;
            }

            foreach (char c in letters)
            {
                if (!map.ContainsKey(c))
                {
                    map.Add(c, (char)(index + 'a'));
                    if (TryPermutations(input, output, letters, index + 1, map, display, ref retVal)) return true;
                    map.Remove(c);
                }
            }

            return false;
        }


        private bool MapWorks(string[] input,
                              string[] output,
                              Hashtable map,
                              Hashtable display,
                              ref long sumWords)
        {
            bool valid = true;
            foreach (string str in input)
            {
                string s = "";
                foreach (char c in str)
                {
                    s += ((char)map[c]).ToString();
                }
                s = SortString(s);
                if (!display.ContainsKey(s))
                {
                    valid = false;
                    break;
                }
            }

            if (!valid) return false;

            sumWords = 0;
            foreach (string str in output)
            {
                string s = "";
                foreach (char c in str)
                {
                    s += ((char)map[c]).ToString();
                }
                s = SortString(s);
                sumWords = 10 * sumWords + (int)display[s];
            }

            return true;
        }
    }
}
