using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day4
    {
        public long Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();

            string draws = "";
            List<string> rows = new List<string>();
            List<Bingo> bingoCards = new List<Bingo>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (String.IsNullOrEmpty(line)) continue;
                if (String.IsNullOrEmpty(draws)) draws = line;
                else
                {
                    rows.Add(line);
                    if (rows.Count == 5)
                    {
                        Bingo bingo = new Bingo(new List<string>(rows));
                        bingoCards.Add(bingo);
                        rows = new List<string>();
                    }
                }
            }
            sr.Close();

            string[] parts = draws.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                int number = Int32.Parse(part);

                foreach (Bingo b in bingoCards)
                {
                    long result = b.Play(number);
                    if (result != -1)
                    {
                        return result;
                    }
                }
            }

            return -1;
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();

            string draws = "";
            List<string> rows = new List<string>();
            List<Bingo> bingoCards = new List<Bingo>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (String.IsNullOrEmpty(line)) continue;
                if (String.IsNullOrEmpty(draws)) draws = line;
                else
                {
                    rows.Add(line);
                    if (rows.Count == 5)
                    {
                        Bingo bingo = new Bingo(new List<string>(rows));
                        bingoCards.Add(bingo);
                        rows = new List<string>();
                    }
                }
            }
            sr.Close();

            Hashtable bingoCardWin = new Hashtable();
            string[] parts = draws.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                int number = Int32.Parse(part);

                for (int i = 0; i < bingoCards.Count; i++)
                {
                    if (!bingoCardWin.ContainsKey(i))
                    {
                        long result = bingoCards[i].Play(number);
                        if (result != -1)
                        {
                            if (bingoCardWin.Count == bingoCards.Count - 1)
                            {
                                return result;
                            }
                            bingoCardWin.Add(i, true);
                        }
                    }
                }
            }

            return -1;
        }
    }

    class Bingo
    {
        private Hashtable numberToRow = null;
        private Hashtable numberToCol = null;
        private Hashtable row = null;
        private Hashtable col = null;
        private int numberRows = 0;
        private int numberCols = 0;
        public Bingo(List<string> rows)
        {
            numberToRow = new Hashtable();
            numberToCol = new Hashtable();
            row = new Hashtable();
            col = new Hashtable();

            numberRows = rows.Count;

            for (int r = 0; r < rows.Count; r++)
            {
                string[] parts = rows[r].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                numberCols = parts.Length;

                for (int c = 0; c < parts.Length; c++)
                {
                    int number = Int32.Parse(parts[c]);

                    numberToRow.Add(number, r);
                    numberToCol.Add(number, c);
                }
            }
        }

        public long Play(int number)
        {
            bool win = false;
            if (numberToRow.ContainsKey(number))
            {
                int r = (int)numberToRow[number];
                if (!row.ContainsKey(r)) row.Add(r, 0);
                row[r] = (int)row[r] + 1;
                if ((int)row[r] == numberRows) win = true;
                numberToRow[number] = -1; //Marking
            }
            if (numberToCol.ContainsKey(number))
            {
                int c = (int)numberToCol[number];
                if (!col.ContainsKey(c)) col.Add(c, 0);
                col[c] = (int)col[c] + 1;
                if ((int)col[c] == numberCols) win = true;
                numberToCol[number] = -1; //Marking
            }

            long sum = -1;
            if (win)
            {
                sum = 0;
                foreach (int val in numberToRow.Keys)
                {
                    int cr = (int)numberToRow[val];
                    int cc = (int)numberToCol[val];

                    if (cr != -1 && cc != -1)
                    {
                        sum += val;
                    }
                }

                sum *= number;
            }

            return sum;
        }
    }
}