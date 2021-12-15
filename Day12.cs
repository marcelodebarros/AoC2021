using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    class Day12
    {
        public long Puzzle1(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            Hashtable graph = new Hashtable();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                string[] parts = line.Split('-');
                if (!graph.ContainsKey(parts[0])) graph.Add(parts[0], new Hashtable());
                Hashtable connection1 = (Hashtable)graph[parts[0]];
                if (!connection1.ContainsKey(parts[1])) connection1.Add(parts[1], 0);
                if (!graph.ContainsKey(parts[1])) graph.Add(parts[1], new Hashtable());
                Hashtable connection2 = (Hashtable)graph[parts[1]];
                if (!connection2.ContainsKey(parts[0])) connection2.Add(parts[0], 0);
            }
            sr.Close();

            long retVal = 0;
            Hashtable visited = new Hashtable();
            visited.Add("start", true);
            PathsDFS(graph, "start", visited, ref retVal);
            return retVal;
        }

        public long Puzzle2(string inputFile)
        {
            FileInfo fi = new FileInfo(inputFile);
            StreamReader sr = fi.OpenText();
            Hashtable graph = new Hashtable();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                string[] parts = line.Split('-');
                if (!graph.ContainsKey(parts[0])) graph.Add(parts[0], new Hashtable());
                Hashtable connection1 = (Hashtable)graph[parts[0]];
                if (!connection1.ContainsKey(parts[1])) connection1.Add(parts[1], 0);
                if (!graph.ContainsKey(parts[1])) graph.Add(parts[1], new Hashtable());
                Hashtable connection2 = (Hashtable)graph[parts[1]];
                if (!connection2.ContainsKey(parts[0])) connection2.Add(parts[0], 0);
            }
            sr.Close();

            long retVal = 0;
            Hashtable visited = new Hashtable();
            visited.Add("start", true);
            PathsDFS2(graph, "start", visited, false, ref retVal);
            return retVal;
        }

        private void PathsDFS(Hashtable graph,
                              string currentNode,
                              Hashtable visited,
                              ref long numberPaths)
        {
            if (currentNode.Equals("end"))
            {
                numberPaths++;
                return;
            }

            if (graph.Contains(currentNode))
            {
                Hashtable connections = (Hashtable)graph[currentNode];
                foreach (string key in connections.Keys)
                {
                    if (!visited.ContainsKey(key))
                    {
                        if (key[0] >= 'a' && key[0] <= 'z')
                        {
                            visited.Add(key, true);
                        }

                        PathsDFS(graph, key, visited, ref numberPaths);

                        if (key[0] >= 'a' && key[0] <= 'z')
                        {
                            visited.Remove(key);
                        }
                    }
                }
            }
        }

        private void PathsDFS2(Hashtable graph,
                               string currentNode,
                               Hashtable visited,
                               bool smallVisited,
                               ref long numberPaths)
        {
            if (currentNode.Equals("end"))
            {
                numberPaths++;
                return;
            }

            if (graph.Contains(currentNode))
            {
                Hashtable connections = (Hashtable)graph[currentNode];
                foreach (string key in connections.Keys)
                {
                    if (key[0] < 'a' || key[0] > 'z')
                    {
                        PathsDFS2(graph, key, visited, smallVisited, ref numberPaths);
                    }
                    else if (smallVisited)
                    {
                        if (!visited.ContainsKey(key))
                        {
                            visited.Add(key, true);
                            PathsDFS2(graph, key, visited, smallVisited, ref numberPaths);
                            visited.Remove(key);
                        }
                    }
                    else
                    {
                        if (!key.Equals("start") && !key.Equals("end") && visited.ContainsKey(key))
                        {
                            PathsDFS2(graph, key, visited, true, ref numberPaths);
                        }
                        else if(!visited.ContainsKey(key))
                        {
                            visited.Add(key, true);
                            PathsDFS2(graph, key, visited, false, ref numberPaths);
                            visited.Remove(key);
                        }
                    }
                }
            }
        }
    }
}
