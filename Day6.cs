using System;
using System.Numerics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day6
    {
        public BigInteger Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            string line = sr.ReadLine().Trim();
            sr.Close();

            string[] parts = line.Split(',');
            List<int> fish = new List<int>();
            foreach (string part in parts) fish.Add(Int32.Parse(part));

            int NDAYS = 80;
            return LanternFishGrowth(fish, NDAYS);
        }

        public BigInteger Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            string line = sr.ReadLine().Trim();
            sr.Close();

            string[] parts = line.Split(',');
            List<int> fish = new List<int>();
            foreach (string part in parts) fish.Add(Int32.Parse(part));

            int NDAYS = 256;
            return LanternFishGrowth(fish, NDAYS);
        }

        private BigInteger LanternFishGrowth(List<int> fish, int nDays)
        {
            BigInteger[] timer = new BigInteger[9];

            foreach (int n in fish) timer[n]++;

            for (int i = 0; i < nDays; i++)
            {
                BigInteger timer0 = timer[0];
                for (int j = 0; j < timer.Length - 1; j++)
                    timer[j] = timer[j + 1];
                timer[6] += timer0;
                timer[8] = timer0;
            }

            BigInteger retVal = 0;
            foreach (BigInteger t in timer) retVal += t;

            return retVal;
        }
    }
}
