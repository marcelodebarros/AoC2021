using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day9
    {
        public int Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();

            List<string> heightmap = new List<string>();
            while (!sr.EndOfStream)
            {
                heightmap.Add(sr.ReadLine());
            }
            sr.Close();

            int retVal = 0;
            for (int r = 0; r < heightmap.Count; r++)
            {
                for (int c = 0; c < heightmap[r].Length; c++)
                {
                    bool isLowPoint = true;
                    if (r - 1 >= 0 && (int)(heightmap[r - 1][c] - '0') <= (int)(heightmap[r][c] - '0')) isLowPoint = false;
                    if (r + 1 < heightmap.Count && (int)(heightmap[r + 1][c] - '0') <= (int)(heightmap[r][c] - '0')) isLowPoint = false;
                    if (c - 1 >= 0 && (int)(heightmap[r][c - 1] - '0') <= (int)(heightmap[r][c] - '0')) isLowPoint = false;
                    if (c + 1 < heightmap[r].Length && (int)(heightmap[r][c + 1] - '0') <= (int)(heightmap[r][c] - '0')) isLowPoint = false;

                    if (isLowPoint) retVal += (int)(heightmap[r][c] - '0') + 1;
                }
            }

            return retVal;
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();

            List<string> heightmap = new List<string>();
            while (!sr.EndOfStream)
            {
                heightmap.Add(sr.ReadLine());
            }
            sr.Close();

            List<long> basins = new List<long>();
            for (int r = 0; r < heightmap.Count; r++)
            {
                for (int c = 0; c < heightmap[r].Length; c++)
                {
                    bool isLowPoint = true;
                    if (r - 1 >= 0 && (int)(heightmap[r - 1][c] - '0') <= (int)(heightmap[r][c] - '0')) isLowPoint = false;
                    if (r + 1 < heightmap.Count && (int)(heightmap[r + 1][c] - '0') <= (int)(heightmap[r][c] - '0')) isLowPoint = false;
                    if (c - 1 >= 0 && (int)(heightmap[r][c - 1] - '0') <= (int)(heightmap[r][c] - '0')) isLowPoint = false;
                    if (c + 1 < heightmap[r].Length && (int)(heightmap[r][c + 1] - '0') <= (int)(heightmap[r][c] - '0')) isLowPoint = false;

                    if (isLowPoint)
                    {
                        basins.Add(FindBasinSize(heightmap, r, c));
                    }
                }
            }

            long[] arr = basins.ToArray();
            Array.Sort(arr);

            long retVal = 1;
            for (int i = arr.Length - 1; i >= arr.Length - 3; i--) retVal *= arr[i];

            return retVal;
        }

        private int FindBasinSize(List<string> heightmap,
                                  int row,
                                  int col)
        {
            Queue<BasinQueueItem> queue = new Queue<BasinQueueItem>();
            BasinQueueItem queueItem = new BasinQueueItem(row, col);
            queue.Enqueue(queueItem);
            Hashtable visited = new Hashtable();
            visited.Add(queueItem.key, true);

            int count = 0;
            while (queue.Count > 0)
            {
                BasinQueueItem currentQueueItem = queue.Dequeue();
                count++;

                int[] rowDelta = { -1, 1, 0, 0 };
                int[] colDelta = { 0, 0, 1, -1 };

                for (int i = 0; i < rowDelta.Length; i++)
                {
                    int newRow = currentQueueItem.row + rowDelta[i];
                    int newCol = currentQueueItem.col + colDelta[i];

                    if (newRow >= 0 &&
                        newRow < heightmap.Count &&
                        newCol >= 0 &&
                        newCol < heightmap[newRow].Length &&
                        heightmap[newRow][newCol] != '9' && 
                        (int)(heightmap[newRow][newCol] - '0') > (int)(heightmap[currentQueueItem.row][currentQueueItem.col] - '0'))
                    {
                        BasinQueueItem newQueueItem = new BasinQueueItem(newRow, newCol);
                        if (!visited.ContainsKey(newQueueItem.key))
                        {
                            visited.Add(newQueueItem.key, true);
                            queue.Enqueue(newQueueItem);
                        }
                    }
                }
            }

            return count;
        }

        class BasinQueueItem
        {
            public int row = 0;
            public int col = 0;
            public long key = 0;

            public BasinQueueItem(int row, int col)
            {
                this.row = row;
                this.col = col;
                this.key = row * 1007 + col;
            }
        }
    }
}
