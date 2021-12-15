using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day2
    {
        public long Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();

            long horizontal = 0;
            long depth = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                string[] parts = line.Split(' ');
                long val = Int64.Parse(parts[1]);
                string direction = parts[0];

                switch (direction)
                {
                    case "forward":
                        horizontal += val;
                        break;
                    case "down":
                        depth += val;
                        break;
                    case "up":
                        depth -= val;
                        break;
                };
            }
            sr.Close();

            return horizontal * depth;
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();

            long horizontal = 0;
            long depth = 0;
            long aim = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                string[] parts = line.Split(' ');
                long val = Int64.Parse(parts[1]);
                string direction = parts[0];

                switch (direction)
                {
                    case "forward":
                        horizontal += val;
                        depth += (aim * val);
                        break;
                    case "down":
                        aim += val;
                        break;
                    case "up":
                        aim -= val;
                        break;
                };
            }
            sr.Close();

            return horizontal * depth;
        }
    }
}
