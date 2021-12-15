using System;
using System.Numerics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day7
    {
        public long Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            string line = sr.ReadLine().Trim();
            sr.Close();

            string[] parts = line.Split(',');
            List<int> list = new List<int>();
            foreach (string part in parts) list.Add(Int32.Parse(part));

            Hashtable bucket = new Hashtable();
            foreach (int n in list)
            {
                if (!bucket.ContainsKey(n)) bucket.Add(n, 0);
                bucket[n] = (int)bucket[n] + 1;
            }

            long minCost = Int64.MaxValue;
            foreach(int k1 in bucket.Keys)
            {
                long tempCost = 0;
                foreach (int k2 in bucket.Keys)
                {
                    tempCost += (long)Math.Abs(k2 - k1) * (int)bucket[k2];
                }
                minCost = Math.Min(minCost, tempCost);
            }

            return minCost;
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            string line = sr.ReadLine().Trim();
            sr.Close();

            string[] parts = line.Split(',');
            List<int> list = new List<int>();
            int max = 0;
            foreach (string part in parts)
            {
                int n = Int32.Parse(part);
                max = Math.Max(max, n);
                list.Add(n);
            }

            Hashtable bucket = new Hashtable();
            foreach (int n in list)
            {
                if (!bucket.ContainsKey(n)) bucket.Add(n, 0);
                bucket[n] = (int)bucket[n] + 1;
            }

            long minCost = Int64.MaxValue;
            for (int i = 0; i <= max; i++)
            {
                long tempCost = 0;
                foreach (int k2 in bucket.Keys)
                {
                    long n = (long)Math.Abs(k2 - i);
                    n = (n * (n + 1)) / 2;
                    tempCost += (n * (int)bucket[k2]);
                }
                minCost = Math.Min(minCost, tempCost);
            }

            return minCost;
        }
    }
}
