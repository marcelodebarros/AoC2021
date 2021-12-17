using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1 day1 = new Day1();
            Console.WriteLine("Day 1, Puzzle 1: {0}", day1.Puzzle1("input_day1_puzzle1.txt"));
            Console.WriteLine("Day 1, Puzzle 2: {0}", day1.Puzzle2("input_day1_puzzle2.txt"));

            Day2 day2 = new Day2();
            Console.WriteLine("Day 2, Puzzle 1: {0}", day2.Puzzle1("input_day2_puzzle1.txt"));
            Console.WriteLine("Day 2, Puzzle 2: {0}", day2.Puzzle2("input_day2_puzzle2.txt"));

            Day3 day3 = new Day3();
            Console.WriteLine("Day 3, Puzzle 1: {0}", day3.Puzzle1("input_day3_puzzle1.txt"));
            Console.WriteLine("Day 3, Puzzle 2: {0}", day3.Puzzle2("input_day3_puzzle2.txt"));

            Day4 day4 = new Day4();
            Console.WriteLine("Day 4, Puzzle 1: {0}", day4.Puzzle1("input_day4_puzzle1.txt"));
            Console.WriteLine("Day 4, Puzzle 2: {0}", day4.Puzzle2("input_day4_puzzle2.txt"));

            Day5 day5 = new Day5();
            Console.WriteLine("Day 5, Puzzle 1: {0}", day5.Puzzle1("input_day5_puzzle1.txt"));
            day5 = new Day5();
            Console.WriteLine("Day 5, Puzzle 2: {0}", day5.Puzzle2("input_day5_puzzle2.txt"));

            Day6 day6 = new Day6();
            Console.WriteLine("Day 6, Puzzle 1: {0}", day6.Puzzle1("input_day6_puzzle1.txt"));
            day6 = new Day6();
            Console.WriteLine("Day 6, Puzzle 2: {0}", day6.Puzzle2("input_day6_puzzle2.txt"));

            Day7 day7 = new Day7();
            Console.WriteLine("Day 7, Puzzle 1: {0}", day7.Puzzle1("input_day7_puzzle1.txt"));
            Console.WriteLine("Day 7, Puzzle 2: {0}", day7.Puzzle2("input_day7_puzzle2.txt"));

            Day8 day8 = new Day8();
            Console.WriteLine("Day 8, Puzzle 1: {0}", day8.Puzzle1("input_day8_puzzle1.txt"));
            Console.WriteLine("Day 8, Puzzle 2: {0}", day8.Puzzle2("input_day8_puzzle2.txt"));

            Day9 day9 = new Day9();
            Console.WriteLine("Day 9, Puzzle 1: {0}", day9.Puzzle1("input_day9_puzzle1.txt"));
            Console.WriteLine("Day 9, Puzzle 2: {0}", day9.Puzzle2("input_day9_puzzle2.txt"));

            Day10 day10 = new Day10();
            Console.WriteLine("Day 10, Puzzle 1: {0}", day10.Puzzle1("input_day10_puzzle1.txt"));
            Console.WriteLine("Day 10, Puzzle 2: {0}", day10.Puzzle2("input_day10_puzzle2.txt"));

            Day11 day11 = new Day11();
            Console.WriteLine("Day 11, Puzzle 1: {0}", day11.Puzzle1("input_day11_puzzle1.txt"));
            Console.WriteLine("Day 11, Puzzle 2: {0}", day11.Puzzle2("input_day11_puzzle2.txt"));

            Day12 day12 = new Day12();
            Console.WriteLine("Day 12, Puzzle 1: {0}", day12.Puzzle1("input_day12_puzzle1.txt"));
            Console.WriteLine("Day 12, Puzzle 2: {0}", day12.Puzzle2("input_day12_puzzle2.txt"));

            Day13 day13 = new Day13();
            Console.WriteLine("Day 13, Puzzle 1: {0}", day13.Puzzle1("input_day13_puzzle1.txt"));
            Console.WriteLine("Day 13, Puzzle 2: {0} (Solution = {1})", day13.Puzzle2("input_day13_puzzle2.txt"), "CPJBERUL");

            Day14 day14 = new Day14();
            Console.WriteLine("Day 14, Puzzle 1: {0}", day14.Puzzle1("input_day14_puzzle1.txt"));
            Console.WriteLine("Day 14, Puzzle 2: {0}", day14.Puzzle2("input_day14_puzzle2.txt"));

            Day15 day15 = new Day15();
            Console.WriteLine("Day 15, Puzzle 1: {0}", day15.Puzzle1("input_day15_puzzle1.txt"));
            Console.WriteLine("Day 15, Puzzle 2: {0}", day15.Puzzle2("input_day15_puzzle2.txt"));

            Day16 day16 = new Day16();
            Console.WriteLine("Day 16, Puzzle 1: {0}", day16.Puzzle1("input_day16_puzzle1.txt"));
            Console.WriteLine("Day 16, Puzzle 2: {0}", day16.Puzzle2("input_day16_puzzle2.txt"));

            Day17 day17 = new Day17();
            Console.WriteLine("Day 17, Puzzle 1: {0}", day17.Puzzle1("input_day17_puzzle1.txt"));
            Console.WriteLine("Day 17, Puzzle 2: {0}", day17.Puzzle2("input_day17_puzzle2.txt"));
        }
    }
}