using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day15
    {
        public long Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            List<string> cavern = new List<string>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                cavern.Add(line);
            }
            sr.Close();

            return ProcessCavernDijkstra(cavern);
            
            //return ProcessCavernDP(cavern); //The DP implementation here is actually wrong since it assumes down/right only
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            List<string> cavern = new List<string>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                cavern.Add(line);
            }
            sr.Close();

            return ProcessCavernDijkstra2(cavern);

            //return ProcessCavernDP2(cavern); //The DP implementation here is actually wrong since it assumes down/right only
        }

        public class CavernItem
        {
            public int row = 0;
            public int col = 0;
            public long key = 0;
            public long totalCost = 0;

            public CavernItem(int row,
                              int col,
                              long totalCost)
            {
                this.row = row;
                this.col = col;
                this.totalCost = totalCost;
                this.key = row * (1000000 + 7) + col;
            }
        }

        private long ProcessCavernDP2(List<string> cavern)
        {
            long[][] cost = new long[5 * cavern.Count][];
            for (int i = 0; i < 5 * cavern.Count; i++) cost[i] = new long[5 * cavern[0].Length];

            for (int r = 0; r < cavern.Count; r++)
            {
                for (int c = 0; c < cavern[r].Length; c++)
                {
                    cost[r][c] = (long)(cavern[r][c] - '0');
                }
            }

            for (int r = 0; r < cost.Length; r++)
            {
                for (int c = 0; c < cost[r].Length; c++)
                {
                    if (c - cavern[0].Length >= 0) cost[r][c] = cost[r][c - cavern[0].Length] + 1;
                    else if (r - cavern.Count >= 0) cost[r][c] = cost[r - cavern.Count][c] + 1;

                    if (cost[r][c] > 9) cost[r][c] = 1;
                }
            }

            long[][] dp = new long[5 * cavern.Count][];
            for (int i = 0; i < 5 * cavern.Count; i++) dp[i] = new long[5 * cavern[0].Length];

            dp[0][0] = 0;
            for (int row = 1; row < dp.Length; row++)
            {
                dp[row][0] = dp[row - 1][0] + cost[row][0];
            }
            for (int col = 1; col < dp[0].Length; col++)
            {
                dp[0][col] = dp[0][col - 1] + cost[0][col];
            }

            for (int row = 1; row < dp.Length; row++)
            {
                for (int col = 1; col < dp[0].Length; col++)
                {
                    dp[row][col] = Math.Min(dp[row - 1][col], dp[row][col - 1]) + cost[row][col];
                }
            }

            return dp[dp.Length - 1][dp[0].Length - 1];
        }

        private long ProcessCavernDP(List<string> cavern)
        {
            long[][] cost = new long[cavern.Count][];
            for (int i = 0; i < cavern.Count; i++) cost[i] = new long[cavern[i].Length];

            for (int r = 0; r < cavern.Count; r++)
            {
                for (int c = 0; c < cavern[r].Length; c++)
                {
                    cost[r][c] = (long)(cavern[r][c] - '0');
                }
            }

            long[][] dp = new long[cavern.Count][];
            for (int i = 0; i < cavern.Count; i++) dp[i] = new long[cavern[i].Length];

            dp[0][0] = 0;
            for (int row = 1; row < cavern.Count; row++)
            {
                dp[row][0] = dp[row - 1][0] + cost[row][0];
            }
            for (int col = 1; col < cavern[0].Length; col++)
            {
                dp[0][col] = dp[0][col - 1] + cost[0][col];
            }

            for (int row = 1; row < cavern.Count; row++)
            {
                for (int col = 1; col < cavern[0].Length; col++)
                {
                    dp[row][col] = Math.Min(dp[row - 1][col], dp[row][col - 1]) + cost[row][col];
                }
            }

            return dp[cavern.Count - 1][cavern[0].Length - 1];
        }

        private long ProcessCavernDijkstra2(List<string> cavern)
        {
            long[][] cost = new long[5 * cavern.Count][];
            for (int i = 0; i < 5 * cavern.Count; i++) cost[i] = new long[5 * cavern[0].Length];

            for (int r = 0; r < cavern.Count; r++)
            {
                for (int c = 0; c < cavern[r].Length; c++)
                {
                    cost[r][c] = (long)(cavern[r][c] - '0');
                }
            }

            for (int r = 0; r < cost.Length; r++)
            {
                for (int c = 0; c < cost[r].Length; c++)
                {
                    if (c - cavern[0].Length >= 0) cost[r][c] = cost[r][c - cavern[0].Length] + 1;
                    else if (r - cavern.Count >= 0) cost[r][c] = cost[r - cavern.Count][c] + 1;

                    if (cost[r][c] > 9) cost[r][c] = 1;
                }
            }

            PriorityQueue pQueue = new PriorityQueue(true);
            Hashtable visited = new Hashtable();
            CavernItem cavernItem = new CavernItem(0, 0, 0);
            pQueue.Enqueue(cavernItem, cavernItem.totalCost);

            while (pQueue.Count > 0)
            {
                double currentCost = 0;
                CavernItem currentItem = (CavernItem)pQueue.Dequeue(out currentCost);

                if (currentItem.row == cost.Length - 1 &&
                    currentItem.col == cost[0].Length - 1)
                {
                    return currentItem.totalCost;
                }

                if (!visited.ContainsKey(currentItem.key)) visited.Add(currentItem.key, true);

                int[] rd = { 1, -1, 0, 0 };
                int[] cd = { 0, 0, 1, -1 };

                for (int i = 0; i < rd.Length; i++)
                {
                    int newRow = currentItem.row + rd[i];
                    int newCol = currentItem.col + cd[i];

                    if (newRow >= 0 &&
                        newRow < cost.Length &&
                        newCol >= 0 &&
                        newCol < cost[0].Length)
                    {
                        CavernItem newCavernItem = new CavernItem(newRow, newCol, currentItem.totalCost + cost[newRow][newCol]);
                        if (!visited.ContainsKey(newCavernItem.key))
                        {
                            visited.Add(newCavernItem.key, true);
                            pQueue.Enqueue(newCavernItem, newCavernItem.totalCost);
                        }
                    }
                }
            }

            return -1;
        }

        private long ProcessCavernDijkstra(List<string> cavern)
        {
            long[][] cost = new long[cavern.Count][];
            for (int i = 0; i < cavern.Count; i++) cost[i] = new long[cavern[i].Length];

            for (int r = 0; r < cavern.Count; r++)
            {
                for (int c = 0; c < cavern[r].Length; c++)
                {
                    cost[r][c] = (long)(cavern[r][c] - '0');
                }
            }

            PriorityQueue pQueue = new PriorityQueue(true);
            Hashtable visited = new Hashtable();
            CavernItem cavernItem = new CavernItem(0, 0, 0);
            pQueue.Enqueue(cavernItem, cavernItem.totalCost);

            while (pQueue.Count > 0)
            {
                double currentCost = 0;
                CavernItem currentItem = (CavernItem)pQueue.Dequeue(out currentCost);

                if (currentItem.row == cavern.Count - 1 &&
                    currentItem.col == cavern[0].Length - 1)
                {
                    return currentItem.totalCost;
                }

                if (!visited.ContainsKey(currentItem.key)) visited.Add(currentItem.key, true);

                int[] rd = { 1, -1, 0, 0 };
                int[] cd = { 0, 0, 1, -1 };

                for (int i = 0; i < rd.Length; i++)
                {
                    int newRow = currentItem.row + rd[i];
                    int newCol = currentItem.col + cd[i];

                    if (newRow >= 0 &&
                        newRow < cavern.Count && 
                        newCol >= 0 &&
                        newCol < cavern[0].Length)
                    {
                        CavernItem newCavernItem = new CavernItem(newRow, newCol, currentItem.totalCost + cost[newRow][newCol]);
                        if (!visited.ContainsKey(newCavernItem.key))
                        {
                            visited.Add(newCavernItem.key, true);
                            pQueue.Enqueue(newCavernItem, newCavernItem.totalCost);
                        }
                    }
                }
            }

            return -1;
        }
    }
}
