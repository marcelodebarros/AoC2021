using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day1
    {
        public int Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();

            int previousNumber = Int32.MaxValue;
            int retVal = 0;
            while (!sr.EndOfStream)
            {
                int currentNumber = Int32.Parse(sr.ReadLine().Trim());
                if (currentNumber > previousNumber) retVal++;
                previousNumber = currentNumber;
            }
            sr.Close();

            return retVal;
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();

            List<long> list = new List<long>();

            while (!sr.EndOfStream)
            {
                long currentNumber = Int64.Parse(sr.ReadLine().Trim());
                list.Add(currentNumber);
            }
            sr.Close();

            int WINSIZE = 3;
            int retVal = 0;
            long currentSum = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (i < WINSIZE)
                {
                    currentSum += list[i];
                }
                else
                {
                    long nextSum = currentSum + list[i] - list[i - WINSIZE];
                    if (nextSum > currentSum) retVal++;
                    currentSum = nextSum;
                }
            }

            return retVal;
        }
    }
}
