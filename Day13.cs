using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day13
    {
        class Dot
        {
            public long row = 0;
            public long col = 0;
            public long key = 0;

            public Dot(long row, long col)
            {
                this.row = row;
                this.col = col;
                this.key = row * (10000 + 7) + col;
            }
        }
        public long Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            Hashtable points = new Hashtable();
            List<string> foldInstructions = new List<string>();
            bool readFoldInstructions = false;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (String.IsNullOrEmpty(line))
                {
                    readFoldInstructions = true;
                    continue;
                }
                else if (!readFoldInstructions)
                {
                    string[] parts = line.Split(',');
                    long col = Int64.Parse(parts[0]);
                    long row = Int64.Parse(parts[1]);
                    Dot dot = new Dot(row, col);
                    if (!points.ContainsKey(dot.key)) points.Add(dot.key, dot);
                }
                else
                {
                    foldInstructions.Add(line);
                }
            }
            sr.Close();

            foreach(string instruction in foldInstructions)
            {
                char axis = ' ';
                long val = 0;
                ParseFoldInstruction(instruction, ref axis, ref val);
                Fold(points, axis, val);
                break; //Puzzle 1 is just the first instruction!
            }
            return (long)points.Count;
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            Hashtable points = new Hashtable();
            List<string> foldInstructions = new List<string>();
            bool readFoldInstructions = false;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (String.IsNullOrEmpty(line))
                {
                    readFoldInstructions = true;
                    continue;
                }
                else if (!readFoldInstructions)
                {
                    string[] parts = line.Split(',');
                    long col = Int64.Parse(parts[0]);
                    long row = Int64.Parse(parts[1]);
                    Dot dot = new Dot(row, col);
                    if (!points.ContainsKey(dot.key)) points.Add(dot.key, dot);
                }
                else
                {
                    foldInstructions.Add(line);
                }
            }
            sr.Close();

            foreach (string instruction in foldInstructions)
            {
                char axis = ' ';
                long val = 0;
                ParseFoldInstruction(instruction, ref axis, ref val);
                Fold(points, axis, val);
            }
            DebugPrint(points);

            return (long)points.Count;
        }

        private void Fold(Hashtable points,
                          char axis,
                          long val)
        {
            Hashtable pointsCopy = (Hashtable)points.Clone();

            foreach (long key in pointsCopy.Keys)
            {
                Dot dot = (Dot)points[key];

                if (axis == 'x')
                {
                    if (dot.col > val)
                    {
                        Dot newDot = new Dot(dot.row, dot.col - 2 * (dot.col - val));
                        points.Remove(dot.key);
                        if (!points.ContainsKey(newDot.key)) points.Add(newDot.key, newDot);
                    }
                }
                else
                {
                    if (dot.row > val)
                    {
                        Dot newDot = new Dot(dot.row - 2 * (dot.row - val), dot.col);
                        points.Remove(dot.key);
                        if (!points.ContainsKey(newDot.key)) points.Add(newDot.key, newDot);
                    }
                }
            }
        }

        private void ParseFoldInstruction(string instruction,
                                          ref char axis,
                                          ref long val)
        {
            string[] parts = instruction.Split('=');
            val = Int64.Parse(parts[1]);
            string[] parts2 = parts[0].Split(' ');
            axis = parts2[parts2.Length - 1][0];
        }

        private void DebugPrint(Hashtable points)
        {
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 39; y++)
                {
                    long key = x * (10000 + 7) + y;
                    if (points.ContainsKey(key))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }                        
                }
                Console.WriteLine();
            }
        }
    }
}
