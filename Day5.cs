using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day5
    {
        private int LEN = 2000;
        private int[][] grid = null;

        public Day5()
        {
            grid = new int[LEN][];
            for (int i = 0; i < grid.Length; i++) grid[i] = new int[LEN];
        }
        public int Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            Hashtable retVal = new Hashtable();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                string[] parts = line.Split("->");

                string[] parts1 = parts[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] parts2 = parts[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                int beginRow = Int32.Parse(parts1[0]);
                int beginCol = Int32.Parse(parts1[1]);
                int endRow = Int32.Parse(parts2[0]);
                int endCol = Int32.Parse(parts2[1]);

                if (beginRow == endRow)
                {
                    for (int i = Math.Min(beginCol, endCol); i <= Math.Max(beginCol, endCol); i++)
                    {
                        grid[beginRow][i]++;
                        if (grid[beginRow][i] > 1)
                        {
                            int key = beginRow * (LEN + 7) + i;
                            if (!retVal.ContainsKey(key)) retVal.Add(key, true);
                        }
                    }
                }
                else if (beginCol == endCol)
                {
                    for (int i = Math.Min(beginRow, endRow); i <= Math.Max(beginRow, endRow); i++)
                    {
                        grid[i][beginCol]++;
                        if (grid[i][beginCol] > 1)
                        {
                            int key = i * (LEN + 7) + beginCol;
                            if (!retVal.ContainsKey(key)) retVal.Add(key, true);
                        }
                    }
                }
            }
            sr.Close();

            return retVal.Count;
        }

        public int Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            Hashtable retVal = new Hashtable();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                string[] parts = line.Split("->");

                string[] parts1 = parts[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] parts2 = parts[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                int beginRow = Int32.Parse(parts1[0]);
                int beginCol = Int32.Parse(parts1[1]);
                int endRow = Int32.Parse(parts2[0]);
                int endCol = Int32.Parse(parts2[1]);

                if (beginRow == endRow)
                {
                    for (int i = Math.Min(beginCol, endCol); i <= Math.Max(beginCol, endCol); i++)
                    {
                        grid[beginRow][i]++;
                        if (grid[beginRow][i] > 1)
                        {
                            int key = beginRow * (LEN + 7) + i;
                            if (!retVal.ContainsKey(key)) retVal.Add(key, true);
                        }
                    }
                }
                else if (beginCol == endCol)
                {
                    for (int i = Math.Min(beginRow, endRow); i <= Math.Max(beginRow, endRow); i++)
                    {
                        grid[i][beginCol]++;
                        if (grid[i][beginCol] > 1)
                        {
                            int key = i * (LEN + 7) + beginCol;
                            if (!retVal.ContainsKey(key)) retVal.Add(key, true);
                        }
                    }
                }
                else if (Math.Abs(beginRow - endRow) == Math.Abs(beginCol - endCol))
                {
                    int row = beginRow;
                    int col = beginCol;

                    while (row != endRow && col != endCol)
                    {
                        grid[row][col]++;
                        if (grid[row][col] > 1)
                        {
                            int key = row * (LEN + 7) + col;
                            if (!retVal.ContainsKey(key)) retVal.Add(key, true);
                        }

                        if (row < endRow) row++;
                        else row--;
                        if (col < endCol) col++;
                        else col--;
                    }

                    //Missing the very last point in the above loop
                    //Adding here
                    grid[endRow][endCol]++;
                    if (grid[endRow][endCol] > 1)
                    {
                        int key = endRow * (LEN + 7) + endCol;
                        if (!retVal.ContainsKey(key)) retVal.Add(key, true);
                    }
                }
            }
            sr.Close();

            return retVal.Count;
        }
    }
}
