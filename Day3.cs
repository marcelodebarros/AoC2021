using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day3
    {
        public long Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();

            int[] countBitsFrequency = null;
            while (!sr.EndOfStream)
            {
                string bits = sr.ReadLine().Trim();

                if (countBitsFrequency == null)
                {
                    countBitsFrequency = new int[bits.Length];
                }

                for (int i = 0; i < bits.Length; i++)
                {
                    if (bits[i] == '1') countBitsFrequency[i]++;
                    else countBitsFrequency[i]--;
                }
            }
            sr.Close();

            long gamma = 0;
            long epsilon = 0;
            long pow = 1;
            for (int i = countBitsFrequency.Length - 1; i >= 0; i--)
            {
                if (countBitsFrequency[i] > 0)
                {
                    gamma += pow;
                }
                else
                {
                    epsilon += pow;
                }
                pow *= 2;
            }

            return gamma * epsilon;
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();

            List<string> listBits = new List<string>();
            while (!sr.EndOfStream)
            {
                string bits = sr.ReadLine().Trim();
                listBits.Add(bits);
            }
            sr.Close();

            string oxygen = FindRating(new List<string>(listBits), true);
            string co2 = FindRating(new List<string>(listBits), false);

            long longO2 = Convert.ToInt64(oxygen, 2);
            long longCO2 = Convert.ToInt64(co2, 2);

            return longO2 * longCO2;
        }

        private string FindRating(List<string> listBits, bool isOxygen)
        {
            List<string> bits = new List<string>(listBits);

            int index = 0;
            while (bits.Count > 1)
            {
                //Find counts
                int countIndex = 0;
                for (int i = 0; i < bits.Count; i++)
                {
                    if (bits[i][index] == '1') countIndex++;
                    else countIndex--;
                }

                List<string> copyBits = new List<string>();
                if (isOxygen)
                {
                    for (int i = 0; i < bits.Count; i++)
                    {
                        if (countIndex >= 0) //1
                        {
                            if (bits[i][index] == '1')
                            {
                                copyBits.Add(bits[i]);
                            }
                        }
                        else
                        {
                            if (bits[i][index] == '0')
                            {
                                copyBits.Add(bits[i]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < bits.Count; i++)
                    {
                        if (countIndex >= 0) //0
                        {
                            if (bits[i][index] == '0')
                            {
                                copyBits.Add(bits[i]);
                            }
                        }
                        else
                        {
                            if (bits[i][index] == '1')
                            {
                                copyBits.Add(bits[i]);
                            }
                        }
                    }
                }

                index++;
                bits = new List<string>(copyBits);
            }

            return bits[0];
        }
    }
}