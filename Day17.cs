using System;
using System.Numerics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day17
    {
        public int Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            string line = sr.ReadLine().Trim();
            sr.Close();

            int xl = 0;
            int xr = 0;
            int yl = 0;
            int yu = 0;
            ParseInput(line, ref xl, ref xr, ref yl, ref yu);

            Console.Write("Processing Day 17, Puzzle 1. Please be patient!!...");
            int maxy = 0;
            for (int x = -1000; x <= 1000; x++)
            {
                for (int y = -1000; y <= 1000; y++)
                {
                    int tempy = 0;
                    if (ProcessProbeLauncher(x, y, xl, xr, yl, yu, ref tempy))
                    {
                        maxy = Math.Max(maxy, tempy);
                    }
                }
            }
            Console.WriteLine("Done!!!");

            return maxy;
        }

        public int Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            string line = sr.ReadLine().Trim();
            sr.Close();

            int xl = 0;
            int xr = 0;
            int yl = 0;
            int yu = 0;
            ParseInput(line, ref xl, ref xr, ref yl, ref yu);

            Console.Write("Processing Day 17, Puzzle 2. Please be patient!!...");
            int numberResults = 0;
            for (int x = -1000; x <= 1000; x++)
            {
                for (int y = -1000; y <= 1000; y++)
                {
                    int tempy = 0;
                    if (ProcessProbeLauncher(x, y, xl, xr, yl, yu, ref tempy))
                    {
                        numberResults++;
                    }
                }
            }
            Console.WriteLine("Done!!!");

            return numberResults;
        }

        private Hashtable ReadResults(string fileName)
        {
            Hashtable results = new Hashtable();
            FileInfo fi = new FileInfo(fileName);
            StreamReader sr = fi.OpenText();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    string[] test = part.Trim().Split(',');
                    int x = Int32.Parse(test[0]);
                    int y = Int32.Parse(test[1]);
                    string key = "(" + x.ToString() + "," + y.ToString() + ")";
                    if (!results.ContainsKey(key)) results.Add(key, true);
                }
            }
            sr.Close();

            return results;
        }

        private bool ProcessProbeLauncher(int x0,
                                          int y0,
                                          int xl,
                                          int xr,
                                          int yl,
                                          int yu,
                                          ref int highy)
        {
            int x = 0;
            int y = 0;

            highy = -10000;

            for (; ; )
            {
                highy = Math.Max(highy, y);
                if (x >= xl && x <= xr && y >= yl && y <= yu)
                {
                    return true;
                }
                if (x > xr || y < yl) return false;

                x += x0;
                y += y0;

                x0 = (x0 > 0) ? (x0 - 1) : (x0 < 0 ? (x0 + 1) : x0);
                y0--;
            }
        }

        private void ParseInput(string input,
                                ref int xl,
                                ref int xr,
                                ref int yl,
                                ref int yu)
        {
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string xtemp = parts[2].Substring(2);
            xtemp = xtemp.Substring(0, xtemp.Length - 1);
            string[] xparts = xtemp.Split("..");
            xl = Int32.Parse(xparts[0]);
            xr = Int32.Parse(xparts[1]);

            string ytemp = parts[3].Substring(2);
            string[] yparts = ytemp.Split("..");
            yl = Int32.Parse(yparts[0]);
            yu = Int32.Parse(yparts[1]);
        }
    }
}
