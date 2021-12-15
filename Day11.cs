using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day11
    {
        public long Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            List<string> list = new List<string>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                list.Add(line);
            }
            sr.Close();

            int[][] octopus = new int[list.Count][];
            for (int r = 0; r < octopus.Length; r++) octopus[r] = new int[list[r].Length];

            for (int r = 0; r < octopus.Length; r++)
            {
                for (int c = 0; c < octopus[r].Length; c++)
                {
                    octopus[r][c] = (int)(list[r][c] - '0');
                }
            }

            int stepsAllFlashes = 0;
            return ProcessFlashes(octopus, 100, ref stepsAllFlashes);
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            List<string> list = new List<string>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                list.Add(line);
            }
            sr.Close();

            int[][] octopus = new int[list.Count][];
            for (int r = 0; r < octopus.Length; r++) octopus[r] = new int[list[r].Length];

            for (int r = 0; r < octopus.Length; r++)
            {
                for (int c = 0; c < octopus[r].Length; c++)
                {
                    octopus[r][c] = (int)(list[r][c] - '0');
                }
            }

            int stepsAllFlashes = 0;
            ProcessFlashes(octopus, Int32.MaxValue, ref stepsAllFlashes);
            return stepsAllFlashes;
        }

        private int ProcessFlashes(int[][] octopus, int steps, ref int stepsAllFlashes)
        {
            int flashes = 0;
            for (int n = 0; n < steps; n++)
            {
                //Add 1:
                for (int r = 0; r < octopus.Length; r++)
                {
                    for (int c = 0; c < octopus[r].Length; c++)
                    {
                        octopus[r][c]++;
                    }
                }

                //Count the flashes
                bool hasFlashes = true;
                Hashtable visited = new Hashtable();
                int countFlashesOnStep = 0;
                while (hasFlashes)
                {
                    hasFlashes = false;
                    for (int r = 0; r < octopus.Length; r++)
                    {
                        for (int c = 0; c < octopus[r].Length; c++)
                        {
                            if (octopus[r][c] > 9)
                            {
                                int key = r * (octopus.Length + 7) + c;
                                if (!visited.ContainsKey(key))
                                {
                                    visited.Add(key, true);
                                    octopus[r][c] = 0;
                                    flashes++;

                                    countFlashesOnStep++;
                                    if (countFlashesOnStep == octopus.Length * octopus[r].Length)
                                    {
                                        stepsAllFlashes = n + 1;
                                        return 0; //Ignore the val
                                    }

                                    hasFlashes = true;

                                    for (int rd = -1; rd <= 1; rd++)
                                    {
                                        for (int cd = -1; cd <= 1; cd++)
                                        {
                                            if (rd == 0 && cd == 0) continue;

                                            int newRow = r + rd;
                                            int newCol = c + cd;
                                            if (newRow >= 0 &&
                                                newRow < octopus.Length &&
                                                newCol >= 0 &&
                                                newCol < octopus[newRow].Length)
                                            {
                                                int newKey = newRow * (octopus.Length + 7) + newCol;
                                                if (!visited.ContainsKey(newKey))
                                                {
                                                    octopus[newRow][newCol]++;
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return flashes;
        }

        private void PrintOctopus(int[][] octopus)
        {
            for (int r = 0; r < octopus.Length; r++)
            {
                for (int c = 0; c < octopus[r].Length; c++)
                {
                    Console.Write(octopus[r][c]);
                }
                Console.WriteLine();
            }
        }
    }
}
