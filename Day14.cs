using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day14
    {
        public long Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            Hashtable polymerTemplate = new Hashtable();
            string inputString = "";
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (String.IsNullOrEmpty(inputString))
                {
                    inputString = line;
                    continue;
                }
                else if (String.IsNullOrEmpty(line))
                {
                    continue;
                }
                string[] parts = line.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                if (!polymerTemplate.ContainsKey(parts[0].Trim())) polymerTemplate.Add(parts[0].Trim(), parts[1].Trim());
            }
            sr.Close();

            Hashtable retVal = ProcessPolymer(inputString, polymerTemplate, 10);

            int largest = Int32.MinValue;
            int smallest = Int32.MaxValue;
            foreach (char key in retVal.Keys)
            {
                largest = Math.Max(largest, (int)retVal[key]);
                smallest = Math.Min(smallest, (int)retVal[key]);
            }

            return largest - smallest;
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            Hashtable polymerTemplate = new Hashtable();
            string inputString = "";
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (String.IsNullOrEmpty(inputString))
                {
                    inputString = line;
                    continue;
                }
                else if (String.IsNullOrEmpty(line))
                {
                    continue;
                }
                string[] parts = line.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                if (!polymerTemplate.ContainsKey(parts[0].Trim())) polymerTemplate.Add(parts[0].Trim(), parts[1].Trim());
            }
            sr.Close();

            return ProcessPolymer2(inputString, polymerTemplate, 3);
        }

        private long ProcessPolymer2(string input, 
                                     Hashtable polymeterTemplate,                                     
                                     int steps)
        {
            Hashtable allPairs = new Hashtable();

            //Process the first step
            for (int i = 0; i < input.Length - 1; i++)
            {
                string key = input.Substring(i, 2);
                if (polymeterTemplate.ContainsKey(key))
                {
                    string val = (string)polymeterTemplate[key];
                    string pair1 = key[0].ToString() + val;
                    string pair2 = val + key[1].ToString();

                    string[] listOfPairs = { pair1, pair2 };
                    foreach (string singlePair in listOfPairs)
                    {
                        if (!allPairs.ContainsKey(singlePair)) allPairs.Add(singlePair, 0L);
                        allPairs[singlePair] = (long)allPairs[singlePair] + 1;
                    }
                }
            }

            //Now process the remaining steps
            //Big-O should be steps*allpairs
            for (int i = 1; i < steps; i++)
            {
                Hashtable allPairsClone = (Hashtable)allPairs.Clone();
                foreach (string pair in allPairsClone.Keys)
                {
                    if ((long)allPairsClone[pair] > 0)
                    {
                        long temp = (long)allPairsClone[pair];
                        //allPairs[pair] = 0L;

                        string val = (string)polymeterTemplate[pair];
                        string pair1 = pair[0].ToString() + val;
                        string pair2 = val + pair[1].ToString();

                        string[] listOfPairs = { pair1, pair2 };
                        foreach (string singlePair in listOfPairs)
                        {
                            if (!allPairs.ContainsKey(singlePair)) allPairs.Add(singlePair, 0L);
                            allPairs[singlePair] = (long)allPairs[singlePair] + temp;
                        }
                    }
                }
            }

            Hashtable allCounts = new Hashtable();
            foreach (string pair in allPairs.Keys)
            {
                if ((long)allPairs[pair] > 0)
                {
                    char letter = pair[0];
                    if (!allCounts.ContainsKey(letter)) allCounts.Add(letter, 0L);
                    allCounts[letter] = (long)allCounts[letter] + (long)allPairs[pair];
                }
            }
            if (!allCounts.ContainsKey(input[input.Length - 1])) allCounts.Add(input[input.Length - 1], 0L);
            allCounts[input[input.Length - 1]] = (long)allCounts[input[input.Length - 1]] + 1;

            long max = Int64.MinValue;
            long min = Int64.MaxValue;
            foreach (char c in allCounts.Keys)
            {
                max = Math.Max(max, (long)allCounts[c]);
                min = Math.Min(min, (long)allCounts[c]);
            }

            return max - min;
        }

        private Hashtable ProcessPolymer(string input,
                                         Hashtable polymerTemplate,
                                         int steps)
        {
            string str = input;
            string temp = "";
            for (int i = 0; i < steps; i++)
            {
                for (int j = 0; j < str.Length - 1; j++)
                {
                    string key = str.Substring(j, 2);
                    if (polymerTemplate.ContainsKey(key))
                    {
                        temp += str[j].ToString() + (string)polymerTemplate[key];
                    }
                    else
                    {
                        temp += str[j].ToString();
                    }
                }
                temp += str[str.Length - 1].ToString();
                str = temp;
                temp = "";
            }

            Hashtable retVal = new Hashtable();
            foreach (char c in str)
            {
                if (!retVal.ContainsKey(c)) retVal.Add(c, 0);
                retVal[c] = (int)retVal[c] + 1;
            }

            return retVal;
        }
    }
}
